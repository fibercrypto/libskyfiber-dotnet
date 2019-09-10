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
    public class TransactionOutput : IEquatable<TransactionOutput>, IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uxId"></param>
        /// <param name="address"></param>
        /// <param name="coins"></param>
        /// <param name="hours"></param>
        public TransactionOutput(string uxId = default(string), string address = default(string), string coins = default(string),
            string hours = default(string))
        {
            UxId = uxId;
            Address = address;
            Coins = coins;
            Hours = hours;
        }

        /// <summary>
        /// Gets or Sets UxId
        /// </summary>
        [DataMember(Name = "uxid", EmitDefaultValue = false)]
        public string UxId { get; set; }

        /// <summary>
        /// Gets or Sets address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets Coins
        /// </summary>
        [DataMember(Name = "coins", EmitDefaultValue = false)]
        public string Coins { get; set; }

        /// <summary>
        /// Gets or Sets Hours
        /// </summary>
        [DataMember(Name = "hours", EmitDefaultValue = false)]
        public string Hours { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TransactionOutput {\n");
            sb.Append("  uxid: ").Append(UxId).Append("\n");
            sb.Append("  address: ").Append(Address).Append("\n");
            sb.Append("  coins: ").Append(Coins).Append("\n");
            sb.Append("  hours: ").Append(Hours).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">TransactionOutput to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return Equals(input as TransactionOutput);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Equals(TransactionOutput input)
        {
            if (input == null)
                return false;

            return
                (
                    UxId == input.UxId ||
                    (UxId != null &&
                     UxId.Equals(input.UxId))
                ) &&
                (
                    Address == input.Address ||
                    Address != null &&
                    input.Address != null
                ) && (
                    Coins == input.Coins ||
                    (Coins != null &&
                     Coins.Equals(input.Coins)) &&
                    Coins.SequenceEqual(input.Coins
                    )
                ) &&
                (
                    Hours == input.Hours ||
                    Hours != null &&
                    input.Hours != null);
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (UxId != null ? UxId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Address != null ? Address.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Coins != null ? Coins.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Hours != null ? Hours.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}