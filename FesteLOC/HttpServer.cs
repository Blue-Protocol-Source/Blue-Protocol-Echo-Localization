using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FesteLOC
{
    public class HttpServer
    {
        private HttpListener HttpHandler;
        private Task HttpListenTask;
        private Config Cfg;
        static readonly HttpClient HttpClient = new HttpClient(new HttpClientHandler()
        {
            AllowAutoRedirect      = true,
            AutomaticDecompression = DecompressionMethods.All
        });

        public void Init(Config cfg)
        {
            Cfg         = cfg;
            HttpHandler = new HttpListener();
            HttpHandler.Prefixes.Add($"http://localhost:{Cfg.Port}/");
            HttpHandler.Start();

            HttpListenTask = Task.Factory.StartNew(ListenForRequest, TaskCreationOptions.LongRunning);
        }

        private async void ListenForRequest()
        {
            while (true)
            {
                var context = await HttpHandler.GetContextAsync();
                var req     = context.Request;

                var headers = new Dictionary<string, string>(req.Headers.Count);
                foreach (var key in req.Headers.AllKeys)
                    headers.Add(key, req.Headers[key]);

                var reqData = new byte[req.ContentLength64];
                req.InputStream.Read(reqData, 0, reqData.Length);

                //if (req.HttpMethod == "GET")
                {
                    await OnRequest(headers, req.Url, reqData, context.Response, req.HttpMethod);
                }

                await Task.Delay(50);
            }
        }

        private async Task OnRequest(Dictionary<string, string> headers, Uri uri, byte[] reqData, HttpListenerResponse resp, string method)
        {
            //PrintRequest(headers, uri, reqData);
            var masterData = await ForwardRequest(headers, uri, reqData, method);

            if (masterData != null)
            {
                if (uri.PathAndQuery == "/apiext/texts?locale=ja_JP")
                {

                }

                resp.Headers.Clear();
                foreach (var header in masterData.Headers)
                {
                    if (header.Key == "Content-Type")
                        resp.ContentType = header.Value.ToString();
                    else
                    {
                        resp.AddHeader(header.Key, header.Value.First());
                    }

                    Debug.WriteLine($"{header.Key}: {header.Value.First()}");
                }

                var respData = masterData.Content.ReadAsByteArrayAsync().Result;
                resp.ContentLength64 = respData.Length;
                resp.ContentType     = masterData.Content.Headers.ContentType.MediaType;
                resp.StatusCode      = (int)masterData.StatusCode;
                resp.SendChunked     = masterData.Headers.TransferEncodingChunked ?? false;
                resp.OutputStream.Write(respData);
                resp.OutputStream.Flush();
                resp.OutputStream.Close();

                //resp.Headers.Clear();
            }
        }

        private async Task<HttpResponseMessage> ForwardRequest(Dictionary<string, string> headers, Uri uri, byte[] reqData, string method)
        {
            var reqMsg = new HttpRequestMessage
            {
                Method     = method == "GET" ? HttpMethod.Get : HttpMethod.Post,
                RequestUri = new Uri($"{Cfg.MasterDataURL}{uri.PathAndQuery}"),
                Headers    = { },
                Content    = new ByteArrayContent(reqData)
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
                reqMsg.Content.Headers.Add("Content-Type", contentType);

            try
            {
                var response = await HttpClient.SendAsync(reqMsg);

                if (method == "GET")
                    SaveDecryptedData(response);

                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }

        private void SaveDecryptedData(HttpResponseMessage resp)
        {
            if (!Cfg.SaveDecryptedData)
                return;

            var path = Path.Combine(Cfg.DecryptedDir, resp.Headers.Date.Value.ToString("yyyy_MM_dd-HH_mm_ss"), $"{resp.RequestMessage.RequestUri.AbsolutePath.TrimStart('/', '\\')}.json");
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            var data = DecryptResp(resp);
            var niceJson = JsonSerializer.Serialize(JsonDocument.Parse(data), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(path, niceJson);
        }

        private string DecryptResp(HttpResponseMessage resp)
        {
            var iv        = Convert.FromBase64String(resp.Headers.GetValues("x-amz-meta-x-sb-iv").FirstOrDefault());
            var data      = resp.Content.ReadAsByteArrayAsync().Result;
            var dataStr   = Encoding.UTF8.GetString(data);
            var decrypted = AES.Decrypt(Convert.FromBase64String(dataStr), Convert.FromHexString(Cfg.AESKey), iv);

            return decrypted;
        }

        private void PrintRequest(Dictionary<string, string> headers, Uri uri, ReadOnlySpan<byte> reqData)
        {
            Debug.WriteLine("---- Request ----");
            Debug.WriteLine($"Url Path: {uri.PathAndQuery}");
            foreach (var header in headers)
                Debug.WriteLine($"{header.Key}: {header.Value}");
        }
    }
}
