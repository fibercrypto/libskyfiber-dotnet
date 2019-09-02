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
    public class WalletMeta : IEquatable<WalletMeta>, IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="id"></param>
        /// <param name="label"></param>
        /// <param name="type"></param>
        /// <param name="version"></param>
        /// <param name="cryptoType"></param>
        /// <param name="timestamp"></param>
        /// <param name="encrypted"></param>
        /// <param name="bip44Coin"></param>
        /// <param name="xpub"></param>
        public WalletMeta(string coin = default(string), string id = default(string), string label = default(string),
            string type = default(string), string version = default(string), string cryptoType = default(string),
            long timestamp = default(long), bool encrypted = default(bool), int bip44Coin = default(int),
            string xpub = default(string))
        {
            Coin = coin;
            Id = id;
            Label = label;
            Type = type;
            Version = version;
            CryptoType = cryptoType;
            Timestamp = timestamp;
            Encrypted = encrypted;
            Bip44Coin = bip44Coin;
            Xpub = xpub;
        }

        /// <summary>
        /// Gets or Sets Coin
        /// </summary>
        [DataMember(Name = "coin", EmitDefaultValue = true)]
        public string Coin { get; set; }

        /// <summary>
        /// Gets or Sets ID
        /// </summary>
        [DataMember(Name = "filename", EmitDefaultValue = true)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Label
        /// </summary>
        [DataMember(Name = "label", EmitDefaultValue = true)]
        public string Label { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = true)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets Version
        /// </summary>
        [DataMember(Name = "version", EmitDefaultValue = true)]
        public string Version { get; set; }

        /// <summary>
        /// Gets or Sets CryptoType
        /// </summary>
        [DataMember(Name = "crypto_type", EmitDefaultValue = true)]
        public string CryptoType { get; set; }

        /// <summary>
        /// Gets or Sets Timestamp
        /// </summary>
        [DataMember(Name = "timestamp", EmitDefaultValue = true)]
        public long Timestamp { get; set; }

        /// <summary>
        /// Gets or Sets Encrypted
        /// </summary>
        [DataMember(Name = "encrypted", EmitDefaultValue = true)]
        public bool Encrypted { get; set; }

        /// <summary>
        /// Gets or Sets Bip44Coin
        /// </summary>
        [DataMember(Name = "bip44_coin", EmitDefaultValue = false)]
        public int Bip44Coin { get; set; }

        /// <summary>
        /// Gets or Sets Xpub
        /// </summary>
        [DataMember(Name = "xpub", EmitDefaultValue = false)]
        public string Xpub { get; set; }

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
            sb.Append("  coin: ").Append(Coin).Append("\n");
            sb.Append("  filename: ").Append(Id).Append("\n");
            sb.Append("  label: ").Append(Label).Append("\n");
            sb.Append("  type: ").Append(Type).Append("\n");
            sb.Append("  version: ").Append(Version).Append("\n");
            sb.Append("  crypto_type: ").Append(CryptoType).Append("\n");
            sb.Append("  timestamp: ").Append(Timestamp).Append("\n");
            sb.Append("  encrypted: ").Append(Encrypted).Append("\n");
            sb.Append("  bip44_coin: ").Append(Bip44Coin).Append("\n");
            sb.Append("  xpub: ").Append(Xpub).Append("\n");
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
            return Equals(input as WalletMeta);
        }

        /// <summary>
        /// Returns true if WalletMeta instances are equal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Equals(WalletMeta input)
        {
            if (input == null)
            {
                return false;
            }

            return (
                       Coin == input.Coin ||
                       Coin != null &&
                       Coin.Equals(input.Coin)
                   ) &&
                   (
                       Id == input.Id ||
                       Id != null &&
                       Id.Equals(input.Id)
                   ) &&
                   (
                       Label == input.Label ||
                       Label != null &&
                       input.Label != null
                   ) &&
                   (
                       Timestamp == input.Timestamp ||
                       Timestamp.Equals(input.Timestamp)
                   ) &&
                   (
                       Type == input.Type ||
                       (Type != null &&
                        Type.Equals(input.Type))
                   ) &&
                   (
                       Version == input.Version ||
                       Version != null &&
                       Version.Equals(input.Version)
                   ) &&
                   (
                       CryptoType == input.CryptoType ||
                       CryptoType != null &&
                       CryptoType.Equals(input.CryptoType)
                   ) &&
                   (
                       Encrypted == input.Encrypted ||
                       Encrypted.Equals(input.Encrypted)
                   ) &&
                   (
                       Bip44Coin == input.Bip44Coin ||
                       Bip44Coin.Equals(input.Bip44Coin)
                   ) &&
                   (
                       Xpub == input.Xpub ||
                       Xpub != null &&
                       Xpub.Equals(input.Xpub)
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
                if (Coin != null)
                    hashCode = hashCode * 59 + Coin.GetHashCode();
                if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                if (Label != null)
                    hashCode = hashCode * 59 + Label.GetHashCode();
                if (Timestamp != null)
                    hashCode = hashCode * 59 + Timestamp.GetHashCode();
                if (Type != null)
                    hashCode = hashCode * 59 + Type.GetHashCode();
                if (Version != null)
                    hashCode = hashCode * 59 + Version.GetHashCode();
                if (CryptoType != null)
                    hashCode = hashCode * 59 + CryptoType.GetHashCode();
                if (Encrypted != null)
                    hashCode = hashCode * 59 + Encrypted.GetHashCode();
                if (Bip44Coin != null)
                    hashCode = hashCode * 59 + Bip44Coin.GetHashCode();
                if (Xpub != null)
                    hashCode = hashCode * 59 + Xpub.GetHashCode();
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