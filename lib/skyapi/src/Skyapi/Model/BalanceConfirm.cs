using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Skyapi.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BalanceConfirm : IEquatable<BalanceConfirm>, IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coins"></param>
        /// <param name="hours"></param>
        public BalanceConfirm(long coins = default(long), long hours = default(long))
        {
            Coins = coins;
            Hours = hours;
        }

        /// <summary>
        /// Gets or Sets Coins
        /// </summary>
        [DataMember(Name = "coins", EmitDefaultValue = true)]
        public long Coins { get; set; }

        /// <summary>
        /// Gets or Sets Coins
        /// </summary>
        [DataMember(Name = "hours", EmitDefaultValue = true)]
        public long Hours { get; set; }

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
            sb.Append("class BalanceConfirm {\n");
            sb.Append("  coins: ").Append(Coins).Append("\n");
            sb.Append("  hours: ").Append(Hours).Append("\n");
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
            return Equals(input as BalanceConfirm);
        }

        /// <summary>
        /// Returns true if BalanceConfirm instances are equal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Equals(BalanceConfirm input)
        {
            if (input == null)
            {
                return false;
            }

            return Coins.Equals(input.Coins) &&
                   Hours == input.Hours;
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
                if (Coins != null)
                    hashCode = hashCode * 59 + Coins.GetHashCode();
                if (Hours != null)
                    hashCode = hashCode * 59 + Hours.GetHashCode();
                return hashCode;
            }
        }


        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}