using System.Collections.Generic;

namespace Skyapi.Model
{
    public class Wallet
    {
        public Meta Meta { get; set; }
        public List<Entry> Entries { get; set; }
    }

    public class Entry
    {
        public string Address { get; set; }
        public string public_key { get; set; }
    }

    public class Meta
    {
        public string coin { get; set; }
        public string filename { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public string version { get; set; }
        public string crypto_type { get; set; }
        public string timestamp { get; set; }
        public bool encrypted { get; set; }
    }
}