using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Skyapi.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Balance : IEquatable<Balance>, IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="confirmed"></param>
        /// <param name="predicted"></param>
        /// <param name="addresses"></param>
        public Balance(BalanceConfirm confirmed = default(BalanceConfirm),
            BalancePredict predicted = default(BalancePredict),
            Dictionary<string, BalancePair> addresses = default(Dictionary<string, BalancePair>))
        {
            Confirmed = confirmed;
            Predicted = predicted;
            Addresses = addresses;
        }

        /// <summary>
        /// Gets or Sets Confirmed
        /// </summary>
        [DataMember(Name = "confirmed", EmitDefaultValue = true)]
        public BalanceConfirm Confirmed { get; set; }

        /// <summary>
        /// Gets or Sets Predicted
        /// </summary>
        [DataMember(Name = "predicted", EmitDefaultValue = true)]
        public BalancePredict Predicted { get; set; }

        /// <summary>
        /// Gets or Sets Addresses
        /// </summary>
        [DataMember(Name = "addresses", EmitDefaultValue = true)]
        public Dictionary<string, BalancePair> Addresses { get; set; }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Balance {\n");
            sb.Append("  confirmed: ").Append(Confirmed).Append("\n");
            sb.Append("  predicted: ").Append(Predicted).Append("\n");
            sb.Append("  addresses: ").Append(Addresses).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return Equals(input as Balance);
        }

        /// <summary>
        /// Returns true if Balance instances are equal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Equals(Balance input)
        {
            if (input == null)
            {
                return false;
            }

            return Confirmed != null && Confirmed.Equals(input.Confirmed) &&
                   Predicted != null && Predicted.Equals(input.Predicted) &&
                   (Addresses == input.Addresses || Addresses != null) &&
                   Addresses.SequenceEqual(input.Addresses);
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                if (Confirmed != null)
                    hashCode = hashCode * 59 + Confirmed.GetHashCode();
                if (Predicted != null)
                    hashCode = hashCode * 59 + Predicted.GetHashCode();
                if (Addresses != null)
                    hashCode = hashCode * 59 + Addresses.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}