using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Skyapi.Model
{
    [DataContract]
    public class WalletEntry : IEquatable<WalletEntry>, IValidatableObject
    {
        public WalletEntry(string address = default(string), string publicKey = default(string))
        {
            Address = address;
            PublicKey = publicKey;
        }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [DataMember(Name = "Address", EmitDefaultValue = true)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets PublicKey
        /// </summary>
        [DataMember(Name = "public_key", EmitDefaultValue = false)]
        public string PublicKey { get; set; }

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
            sb.Append("class WalletMeta {\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  public_key: ").Append(PublicKey).Append("\n");
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
            return Equals(input as WalletEntry);
        }

        /// <summary>
        /// Returns true if WalletMeta instances are equal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Equals(WalletEntry input)
        {
            if (input == null)
            {
                return false;
            }

            return (
                       Address == input.Address ||
                       Address != null &&
                       Address.Equals(input.Address)
                   ) &&
                   (
                       PublicKey == input.PublicKey ||
                       PublicKey != null &&
                       PublicKey.Equals(input.PublicKey)
                   );
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
                if (Address != null)
                    hashCode = hashCode * 59 + Address.GetHashCode();
                if (PublicKey != null)
                    hashCode = hashCode * 59 + PublicKey.GetHashCode();
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