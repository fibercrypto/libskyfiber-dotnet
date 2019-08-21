namespace Skyapi.Model
{
    public class Health
    {
        public BlockchainMetadata Blockchain { get; set; }
        public InlineResponse2005 Version { get; set; }
        public string Coin { get; set; }
        public string User_Agent { get; set; }
        public int Open_Connections { get; set; }
        public int Outgoing_Connections { get; set; }
        public int Incoming_Connections { get; set; }
        public string Uptime { get; set; }
        public bool CSRF_Enabled { get; set; }
        public bool Header_Check_Enabled { get; set; }
        public bool Csp_Enabled { get; set; }
        public bool Wallet_API_Enabled { get; set; }
        public bool GUI_Enabled { get; set; }
        public object User_Verify_Transaction { get; set; }
        public object Unconfirmed_Verify_Transaction { get; set; }
        public long Started_At { get; set; }
    }
}