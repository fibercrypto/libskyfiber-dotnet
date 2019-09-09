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
    public class UnspentOutput : IEquatable<UnspentOutput>, IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="time"></param>
        /// <param name="blockSeq"></param>
        /// <param name="srcTx"></param>
        /// <param name="address"></param>
        /// <param name="coins"></param>
        /// <param name="hours"></param>
        /// <param name="calculatedHours"></param>
        public UnspentOutput(string hash, ulong time, ulong blockSeq, string srcTx, string address, string coins,
            ulong hours, ulong calculatedHours)
        {
            Hash = hash;
            Time = time;
            BlockSeq = blockSeq;
            SrcTx = srcTx;
            Address = address;
            Coins = coins;
            Hours = hours;
            CalculatedHours = calculatedHours;
        }

        /// <summary>
        /// Gets or Sets Hash
        /// </summary>
        [DataMember(Name = "hash", EmitDefaultValue = false)]
        public string Hash { get; set; }

        /// <summary>
        /// Gets or Sets Time
        /// </summary>
        [DataMember(Name = "time", EmitDefaultValue = false)]
        public ulong Time { get; set; }

        /// <summary>
        /// Gets or Sets BlockSeq
        /// </summary>
        [DataMember(Name = "block_seq", EmitDefaultValue = false)]
        public ulong BlockSeq { get; set; }

        /// <summary>
        /// Gets or Sets SrcTx
        /// </summary>
        [DataMember(Name = "src_tx", EmitDefaultValue = false)]
        public string SrcTx { get; set; }

        /// <summary>
        /// Gets or Sets Address
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
        public ulong Hours { get; set; }

        /// <summary>
        /// Gets or Sets CalculatedHours
        /// </summary>
        [DataMember(Name = "calculated_hours", EmitDefaultValue = false)]
        public ulong CalculatedHours { get; set; }

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
            sb.Append("class UnspentOutput {\n");
            sb.Append("  hash: ").Append(Hash).Append("\n");
            sb.Append("  time: ").Append(Time).Append("\n");
            sb.Append("  block_seq: ").Append(BlockSeq).Append("\n");
            sb.Append("  src_tx: ").Append(SrcTx).Append("\n");
            sb.Append("  address: ").Append(Address).Append("\n");
            sb.Append("  coins: ").Append(Coins).Append("\n");
            sb.Append("  hours: ").Append(Hours).Append("\n");
            sb.Append("  calculated_hours: ").Append(CalculatedHours).Append("\n");
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
            return Equals(input as UnspentOutput);
        }


        /// <summary>
        /// Returns true if Address instances are equal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Equals(UnspentOutput input)
        {
            if (input == null)
            {
                return false;
            }

            return Hash != null && Hash.Equals(input.Hash) &&
                   Time == input.Time &&
                   BlockSeq == input.BlockSeq &&
                   SrcTx.Equals(input.SrcTx) && SrcTx != null &&
                   Address.Equals(input.Address) && Address != null &&
                   Coins.Equals(input.Coins) && Coins != null &&
                   Hours.Equals(input.Hours) && Hours == input.Hours &&
                   CalculatedHours.Equals(input.CalculatedHours) && CalculatedHours == input.CalculatedHours;
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
                if (Hash != null)
                    hashCode = hashCode * 59 + Hash.GetHashCode();
                if (Time != null)
                    hashCode = hashCode * 59 + Time.GetHashCode();
                if (BlockSeq != null)
                    hashCode = hashCode * 59 + BlockSeq.GetHashCode();
                if (SrcTx != null)
                    hashCode = hashCode * 59 + SrcTx.GetHashCode();
                if (Address != null)
                    hashCode = hashCode * 59 + Address.GetHashCode();
                if (Coins != null)
                    hashCode = hashCode * 59 + Coins.GetHashCode();
                if (Hours != null)
                    hashCode = hashCode * 59 + Hours.GetHashCode();
                if (CalculatedHours != null)
                    hashCode = hashCode * 59 + CalculatedHours.GetHashCode();
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