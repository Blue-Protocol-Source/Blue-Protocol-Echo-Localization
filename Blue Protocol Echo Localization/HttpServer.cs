using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Blue_Protocol_Echo_Localization.LocalizationFileLayout;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Blue_Protocol_Echo_Localization
{
    public class HttpServer
    {
        private HttpListener HttpHandler;
        private Task HttpListenTask;
        private Config Cfg;
        static readonly HttpClient HttpClient;

        static HttpServer()
        {
            Environment.SetEnvironmentVariable("DOTNET_SYSTEM_NET_DISABLEIPV6", "1");

            HttpClient = new HttpClient(new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.All,
                UseProxy = true,
                Proxy = WebRequest.GetSystemWebProxy(),
            });
        }

        public void Init(Config cfg)
        {
            Cfg = cfg;
            HttpHandler = new HttpListener();
            HttpHandler.Prefixes.Add($"http://localhost:{Cfg.Port}/");
            HttpHandler.Start();

            HttpListenTask = Task.Factory.StartNew(ListenForRequest, TaskCreationOptions.LongRunning);
        }

        public async Task<string> GetRegion()
        {
            try
            {
                var url = "https://freeipapi.com/api/json";
                var region = await HttpClient.GetFromJsonAsync<RegionData>(url).ContinueWith(async json =>
                {
                    var region = (await json).countryCode;
                    return region;
                });

                return region.Result;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"Error getting region: {ex}");
                return "Error";
            }
        }

        private async void ListenForRequest()
        {
            while (true)
            {
                var context = await HttpHandler.GetContextAsync();
                var req = context.Request;

                var headers = new Dictionary<string, string>(req.Headers.Count);
                foreach (var key in req.Headers.AllKeys)
                {
                    headers.Add(key, req.Headers[key]);
                }

                var reqData = new byte[req.ContentLength64];
                req.InputStream.Read(reqData, 0, reqData.Length);

                OnRequest(headers, req.Url, reqData, context.Response, req.HttpMethod);
            }
        }

        private async Task OnRequest(Dictionary<string, string> headers, Uri uri, byte[] reqData, HttpListenerResponse resp, string method)
        {
            Console.Out.WriteLine($"Captured request to [{uri}]");
            //PrintRequest(headers, uri, reqData);
            var masterData = await ForwardRequest(headers, uri, reqData, method);

            if (masterData != null)
            {
                resp.Headers.Clear();
                foreach (var header in masterData.Headers)
                {
                    if (header.Key == "Connection")
                    {
                        resp.KeepAlive = header.Value.First() == "keep-alive" ? true : false;
                    }
                    else
                    {
                        resp.AddHeader(header.Key, header.Value.First());
                    }

                    Debug.WriteLine($"{header.Key}: {header.Value.First()}");
                }

                resp.StatusCode = (int)masterData.StatusCode;
                resp.ContentType = masterData.Content.Headers.ContentType?.MediaType ?? resp.ContentType;
                resp.Headers.Set(HttpResponseHeader.LastModified, masterData.Content.Headers.LastModified?.DateTime.ToString("r"));
                resp.Headers.Set(HttpResponseHeader.Server, ""); // http listen will always add its silly string onto this unless its empty
                resp.Headers.Set(HttpResponseHeader.Date, masterData.Headers.Date?.ToString("r") ?? resp.Headers["Date"]);


                var respData = masterData.Content.ReadAsByteArrayAsync().Result;
                if (uri.PathAndQuery == "/apiext/texts?locale=ja_JP" && File.Exists(GetLocFilePath()))
                {
                    Console.Out.WriteLine($"Request content detected to be overriden with local data file");

                    if (Cfg.AESKey != "")
                    {
                        var mergedLocData = MergeLocalizationData(DecryptResp(masterData), LoadTranslationString());
                        respData = Encoding.UTF8.GetBytes(mergedLocData);
                    }
                    else
                    {
                        respData = LoadTranslation();
                    }

                    resp.Headers.Remove("x-amz-meta-x-sb-iv");
                    resp.Headers.Remove("x-amz-meta-x-sb-rawdatasize");
                    resp.Headers.Remove("x-amz-server-side-encryption");
                }

                resp.OutputStream.Write(respData);

                resp.OutputStream.Flush();
                resp.OutputStream.Close();
                Console.Out.WriteLine($"Sent response for [{uri}]");
                return;
            }

            Console.Out.WriteLine($"ERROR: masterData was null for [{uri}]!!!");
        }

        private async Task<HttpResponseMessage> ForwardRequest(Dictionary<string, string> headers, Uri uri, byte[] reqData, string method)
        {
            var reqMsg = new HttpRequestMessage
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri($"{Cfg.MasterDataURL}{uri.PathAndQuery}"),
                Headers = { },
                Content = new ByteArrayContent(reqData)
            };

            var excludeHeaders = new string[] { "Content-Length", "Content-Type", "Host", "Connection" };
            foreach (var header in headers)
            {
                if (!excludeHeaders.Contains(header.Key))
                {
                    reqMsg.Headers.Add(header.Key, header.Value);
                }
            }

            if (headers.TryGetValue("Content-Type", out string contentType))
            {
                reqMsg.Content.Headers.Add("Content-Type", contentType);
            }

            try
            {
                var response = await HttpClient.SendAsync(reqMsg);

                if (method == "GET")
                {
                    SaveResponseData(response);
                }
                else if (method == "POST")
                {
                    SaveResponsePostData(response);
                    SavePostRequestData(reqData, response.RequestMessage.RequestUri.AbsolutePath, response.Headers.Date.Value);
                }

                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }

        private string GetLocFilePath() => Path.Combine(Cfg.OverrideDir.TrimEnd('/', '\\'), "Localizations", Cfg.LocalizationStr, $"loc.json");

        private byte[] LoadTranslation()
        {
            var path = GetLocFilePath();
            var text = File.ReadAllBytes(path);
            return text;
        }

        private string LoadTranslationString()
        {
            var path = GetLocFilePath();
            var text = File.ReadAllText(path);
            return text;
        }

        private string MergeLocalizationData(string sourceLocData, string overrideLocData)
        {
            List<LocCategory> sourceData = JsonSerializer.Deserialize<List<LocCategory>>(sourceLocData);
            List<LocCategory> updateData = JsonSerializer.Deserialize<List<LocCategory>>(overrideLocData);
            foreach (var CategoryItem in updateData)
            {
                var matchedCategory = sourceData.FirstOrDefault(x => x.name == CategoryItem.name);
                if (matchedCategory != null)
                {
                    foreach (var TextItem in CategoryItem.texts)
                    {
                        var matchedText = matchedCategory.texts.FirstOrDefault(x => x.id == TextItem.id);
                        if (matchedText != null)
                        {
                            matchedText.text = TextItem.text;
                        }
                    }
                }
            }

            var mergedData = JsonSerializer.Serialize(sourceData, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            return mergedData;
        }

        private void SaveResponsePostData(HttpResponseMessage resp)
        {
            if (!Cfg.SaveServerData)
            {
                return;
            }

            var pathVersioned = Path.Combine(Cfg.SaveDataDir, "POST", resp.RequestMessage.RequestUri.AbsolutePath.TrimStart('/', '\\'), resp.Headers.Date.Value.ToString("yyyy_MM_dd-HH_mm_ss"), $"{Path.GetFileName(resp.RequestMessage.RequestUri.AbsolutePath)}.json");

            // Only save files that don't already exist for this version
            if (!File.Exists(pathVersioned))
            {
                SavePostData(pathVersioned, resp);
            }

            bool saveLatest = true;
            if (saveLatest)
            {
                var pathLatest = Path.Combine(Cfg.SaveDataDir, "POST", "latest", $"{resp.RequestMessage.RequestUri.AbsolutePath.TrimStart('/', '\\')}.json");
                SavePostData(pathLatest, resp);
            }
        }

        private void SavePostData(string path, HttpResponseMessage resp)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (Cfg.SaveDecryptedData)
            {
                SaveDecryptedPostData(path, resp);
                Console.Out.WriteLine($"Saved decrypted data to {path.Replace(Cfg.SaveDataDir, "")}");
            }
            else
            {
                string encryptedFilePath = string.Concat(Path.ChangeExtension(path, ""), "_enc.bin");
                File.WriteAllText(encryptedFilePath, Encoding.UTF8.GetString(resp.Content.ReadAsByteArrayAsync().Result));
                Console.Out.WriteLine($"Saved encrypted data to {encryptedFilePath.Replace(Cfg.SaveDataDir, "")}");
            }
        }

        private void SaveDecryptedPostData(string path, HttpResponseMessage resp)
        {
            var data = DecryptPostResp(resp);
            if (data.StartsWith('[') || data.StartsWith('{'))
            {
                var niceJson = JsonSerializer.Serialize(JsonDocument.Parse(data), new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                File.WriteAllText(path, niceJson);
            }
            else
            {
                File.WriteAllText(path, data);
            }
        }

        private void SavePostRequestData(byte[] reqData, string requestUriAbsolutePath, DateTimeOffset DateHeader)
        {
            if (!Cfg.SaveServerData)
            {
                return;
            }

            var pathVersioned = Path.Combine(Cfg.SaveDataDir, "POST", requestUriAbsolutePath.TrimStart('/', '\\'), DateHeader.ToString("yyyy_MM_dd-HH_mm_ss"), $"req-{Path.GetFileName(requestUriAbsolutePath)}.json");

            // Only save files that don't already exist for this version
            if (!File.Exists(pathVersioned))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(pathVersioned));
                File.WriteAllText(pathVersioned, Encoding.UTF8.GetString(reqData));
                Console.Out.WriteLine($"Saved post request data to {pathVersioned.Replace(Cfg.SaveDataDir, "")}");
            }

            bool saveLatest = true;
            if (saveLatest)
            {
                var pathLatest = Path.Combine(Cfg.SaveDataDir, "POST", "latest", $"req-{requestUriAbsolutePath.TrimStart('/', '\\')}.json");
                Directory.CreateDirectory(Path.GetDirectoryName(pathLatest));
                File.WriteAllText(pathLatest, Encoding.UTF8.GetString(reqData));
                Console.Out.WriteLine($"Saved post request data to {pathVersioned.Replace(Cfg.SaveDataDir, "")}");
            }
        }

        private string DecryptPostResp(HttpResponseMessage resp)
        {
            var iv_header = resp.Headers.FirstOrDefault(header => header.Key.EndsWith("x-sb-iv", StringComparison.CurrentCultureIgnoreCase));

            if (iv_header.Key == null)
            {
                return Encoding.UTF8.GetString(resp.Content.ReadAsByteArrayAsync().Result);
            }

            var iv = Convert.FromBase64String(iv_header.Value.FirstOrDefault());
            var data = resp.Content.ReadAsByteArrayAsync().Result;
            var dataStr = Encoding.UTF8.GetString(data);
            var decrypted = AES.Decrypt(Convert.FromBase64String(dataStr), Convert.FromHexString(Cfg.AESKey), iv);

            return decrypted;
        }

        private void SaveResponseData(HttpResponseMessage resp)
        {
            if (!Cfg.SaveServerData)
            {
                return;
            }

            var pathVersioned = Path.Combine(Cfg.SaveDataDir, resp.RequestMessage.RequestUri.AbsolutePath.TrimStart('/', '\\'), resp.Headers.Date.Value.ToString("yyyy_MM_dd-HH_mm_ss"), $"{Path.GetFileName(resp.RequestMessage.RequestUri.AbsolutePath)}.json");

            // Only save files that don't already exist for this version
            if (!File.Exists(pathVersioned))
            {
                SaveData(pathVersioned, resp);
            }

            bool saveLatest = true;
            if (saveLatest)
            {
                var pathLatest = Path.Combine(Cfg.SaveDataDir, "latest", $"{resp.RequestMessage.RequestUri.AbsolutePath.TrimStart('/', '\\')}.json");
                SaveData(pathLatest, resp);
            }
        }

        private void SaveData(string path, HttpResponseMessage resp)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (Cfg.SaveDecryptedData)
            {
                SaveDecryptedData(path, resp);
                Console.Out.WriteLine($"Saved decrypted data to {path.Replace(Cfg.SaveDataDir, "")}");
            }
            else
            {
                string encryptedFilePath = string.Concat(Path.ChangeExtension(path, ""), "_enc.bin");
                File.WriteAllText(encryptedFilePath, Encoding.UTF8.GetString(resp.Content.ReadAsByteArrayAsync().Result));
                Console.Out.WriteLine($"Saved encrypted data to {encryptedFilePath.Replace(Cfg.SaveDataDir, "")}");
            }
        }

        private void SaveDecryptedData(string path, HttpResponseMessage resp)
        {
            var data = DecryptResp(resp);
            var niceJson = JsonSerializer.Serialize(JsonDocument.Parse(data), new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(path, niceJson);
        }

        private string DecryptResp(HttpResponseMessage resp)
        {
            var iv_header = resp.Headers.FirstOrDefault(header => header.Key.EndsWith("x-sb-iv", StringComparison.CurrentCultureIgnoreCase));

            if (iv_header.Key == null)
            {
                return "";
            }

            var iv = Convert.FromBase64String(iv_header.Value.FirstOrDefault());
            var data = resp.Content.ReadAsByteArrayAsync().Result;
            var dataStr = Encoding.UTF8.GetString(data);
            var decrypted = AES.Decrypt(Convert.FromBase64String(dataStr), Convert.FromHexString(Cfg.AESKey), iv);

            return decrypted;
        }

        private void PrintRequest(Dictionary<string, string> headers, Uri uri, ReadOnlySpan<byte> reqData)
        {
            Debug.WriteLine("---- Request ----");
            Debug.WriteLine($"Url Path: {uri.PathAndQuery}");
            foreach (var header in headers)
            {
                Debug.WriteLine($"{header.Key}: {header.Value}");
            }
        }
    }

    public class RegionData
    {
        public string countryCode { get; set; }
    }
}
