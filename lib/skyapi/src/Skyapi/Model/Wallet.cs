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
    public class Wallet : IEquatable<Wallet>, IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="meta"></param>
        /// <param name="entries"></param>
        public Wallet(WalletMeta meta = default(WalletMeta), List<WalletEntry> entries = default(List<WalletEntry>))
        {
            Meta = meta;
            Entries = entries;
        }

        /// <summary>
        /// Gets or Sets Meta
        /// </summary>
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public WalletMeta Meta { get; set; }

        /// <summary>
        /// Gets or Sets Entries
        /// </summary>
        [DataMember(Name = "entries", EmitDefaultValue = false)]
        public List<WalletEntry> Entries { get; set; }

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
            sb.Append("class Wallet {\n");
            sb.Append("  meta: ").Append(Meta).Append("\n");
            sb.Append("  entries: ").Append(Entries).Append("\n");
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
            return Equals(input as Wallet);
        }

        /// <summary>
        /// Returns true if Wallet instances are equal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Equals(Wallet input)
        {
            if (input == null)
            {
                return false;
            }

            return Meta != null && Meta.Equals(input.Meta) &&
                   (Entries == input.Entries || Entries != null) &&
                   Entries.SequenceEqual(input.Entries);
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
                if (Meta != null)
                    hashCode = hashCode * 59 + Meta.GetHashCode();
                if (Entries != null)
                    hashCode = hashCode * 59 + Entries.GetHashCode();
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