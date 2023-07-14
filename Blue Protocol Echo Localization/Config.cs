using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blue_Protocol_Echo_Localization
{
    public class Config
    {
        public ushort Port { get; set; } = 9192;
        public string MasterDataURL { get; set; } = "https://datastore-main.aws.blue-protocol.com:7701";
        public string OverrideDir { get; set; } = "";
        public bool SaveServerData { get; set; } = true;
        public string SaveDataDir { get; set; } = "";
        public string LocalizationStr { get; set; } = "en";
        public string AESKey { get; set; } = "";
        public bool SaveDecryptedData { get; set; } = true;

        private static string ConfigName => "settings.json";
        public static Config Load(string path = null)
        {
            var fullPath = path == null ? ConfigName : Path.Combine(path, ConfigName);
            if (File.Exists(fullPath))
            {
                var txt = File.ReadAllText(fullPath);
                var data = JsonSerializer.Deserialize<Config>(txt);
                return data;
            }
            else
            {
                Console.Out.WriteLine($"Couldn't load config at: {fullPath}, using defaults.");
                return new Config();
            }
        }

        public void Save(string path = null)
        {
            var fullPath = path == null ? ConfigName : Path.Combine(path, ConfigName);
            var jsonText = JsonSerializer.Serialize(this, new JsonSerializerOptions()
            {
                WriteIndented = true,
            });

            try
            {
                File.WriteAllText(fullPath, jsonText);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"Error saving config at: {fullPath}, {ex}");
            }
        }
    }
}
