using System.Collections.Generic;

namespace Skyapi.Model
{
    public class Balance
    {
        //Buscar forma de poner un valor por defecto a esta clase.
        public Confirm Confirmed { get; set; }
        public Predict Predicted { get; set; }
        public Dictionary<string, BalancePair> Addresses { get; set; }
    }
}