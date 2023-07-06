﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FesteLOC
{
    public class Config
    {
        public ushort Port            = 9192;
        public string MasterDataURL   = "https://datastore-main.aws.blue-protocol.com:7701"; //"http://localhost:5130";
        public string OverrideDir     = "";
        public bool SaveServerData    = false;
        public string SaveDataDir     = "";
        public string LocalizationStr = "";
        public string AESKey          = "";
        public bool SaveDecryptedData = false;
    }
}
