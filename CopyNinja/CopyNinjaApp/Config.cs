using System.Collections.Generic;

namespace CopyNinjaApp
{
    public class Config
    {
        public string Url { get; set; }

        public string SkynetUrlPost { get; set; }

        public string SkynetUrlGet { get; set; }

        public string[] Peers { get; set; }
    }
}
