using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueProtocolEcho
{
    public class Config
    {
        public ushort Port            = 9192;
        public string MasterDataURL   = "https://datastore-main.aws.blue-protocol.com:7701";
        public string OverrideDir     = "";
        public bool SaveServerData    = true;
        public string SaveDataDir     = "DecryptedData";
        public string LocalizationStr = "en";
        public string AESKey          = "";
        public bool SaveDecryptedData = true;
    }
}
