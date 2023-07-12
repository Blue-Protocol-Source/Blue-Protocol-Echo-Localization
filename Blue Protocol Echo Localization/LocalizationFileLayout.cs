using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue_Protocol_Echo_Localization
{
    public class LocalizationFileLayout
    {
        public class LocCategory
        {
            public string name { get; set; }
            public List<LocText> texts { get; set; }
        }

        public class LocText
        {
            public int id { get; set; }
            public string text { get; set; }
        }
    }
}
