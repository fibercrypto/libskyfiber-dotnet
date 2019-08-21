namespace Skyapi.Model
{
    public class BlockchainMetadata
    {
        public BlockVerboseSchemaHeader Head { get; set; }
        public int Unspents { get; set; }
        public int Unconfirmed { get; set; }
        public string Time_Since_Last_Block { get; set; }
    }
}