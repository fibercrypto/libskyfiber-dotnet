using System.Collections.Generic;

namespace Skyapi.Model
{
    public class Balance
    {
        public Dictionary<string, BalancePair> Addresses { get; set; }
        public Confirm Confirmed { get; set; }
        public Predict Predicted { get; set; }
    }
}