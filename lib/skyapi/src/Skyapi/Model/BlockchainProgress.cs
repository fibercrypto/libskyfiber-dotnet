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
    public class BlockchainProgress : IEquatable<BlockchainProgress>, IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="highest"></param>
        /// <param name="peer"></param>
        public BlockchainProgress(int current = default(int), int highest = default(int),
            string[] peer = default(string[]))
        {
            Current = current;
            Highest = highest;
            Peer = peer;
        }

        /// <summary>
        /// Gets or Sets Current
        /// </summary>
        [DataMember(Name = "current", EmitDefaultValue = true)]
        public int Current { get; set; }

        /// <summary>
        /// Gets or Sets Highest
        /// </summary>
        [DataMember(Name = "highest", EmitDefaultValue = true)]
        public int Highest { get; set; }

        /// <summary>
        /// Gets or Sets Peer
        /// </summary>
        [DataMember(Name = "peer", EmitDefaultValue = true)]
        public string[] Peer { get; set; }

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
            sb.Append("class BlockchainProgress {\n");
            sb.Append("  confirmed: ").Append(Current).Append("\n");
            sb.Append("  predicted: ").Append(Highest).Append("\n");
            sb.Append("  addresses: ").Append(Peer).Append("\n");
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
            return Equals(input as BlockchainProgress);
        }

        /// <summary>
        /// Returns true if BlockchainProgress instances are equal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Equals(BlockchainProgress input)
        {
            if (input == null)
            {
                return false;
            }

            return Current.Equals(input.Current) && Highest.Equals(input.Highest) &&
                   (Peer == input.Peer || Peer != null) &&
                   Peer.SequenceEqual(input.Peer);
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
                if (Current != null)
                    hashCode = hashCode * 59 + Current.GetHashCode();
                if (Highest != null)
                    hashCode = hashCode * 59 + Highest.GetHashCode();
                if (Peer != null)
                    hashCode = hashCode * 59 + Peer.GetHashCode();
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