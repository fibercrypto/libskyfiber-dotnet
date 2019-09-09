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
    public class TransactionInput : IEquatable<TransactionInput>, IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uxId"></param>
        /// <param name="address"></param>
        /// <param name="coins"></param>
        /// <param name="hours"></param>
        /// <param name="calculatedHours"></param>
        /// <param name="time"></param>
        /// <param name="block"></param>
        /// <param name="txId"></param>
        public TransactionInput(string uxId = default, string address = default,
            string coins = default, string hours = default, string calculatedHours = default,
            long time = default, long block = default, string txId = default)
        {
            UxId = uxId;
            Address = address;
            Coins = coins;
            Hours = hours;
            CalculatedHours = calculatedHours;
            Time = time;
            Block = block;
            TxId = txId;
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
        /// Gets or Sets CalculatedHours
        /// </summary>
        [DataMember(Name = "calculated_hours", EmitDefaultValue = false)]
        public string CalculatedHours { get; set; }

        /// <summary>
        /// Gets or Sets Time
        /// </summary>
        [DataMember(Name = "timestamp", EmitDefaultValue = false)]
        public long Time { get; set; }

        /// <summary>
        /// Gets or Sets Block
        /// </summary>
        [DataMember(Name = "block", EmitDefaultValue = false)]
        public long Block { get; set; }

        /// <summary>
        /// Gets or Sets TxId
        /// </summary>
        [DataMember(Name = "txid", EmitDefaultValue = false)]
        public string TxId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TransactionInput {\n");
            sb.Append("  uxid: ").Append(UxId).Append("\n");
            sb.Append("  address: ").Append(Address).Append("\n");
            sb.Append("  coins: ").Append(Coins).Append("\n");
            sb.Append("  hours: ").Append(Hours).Append("\n");
            sb.Append("  calculated_hours: ").Append(CalculatedHours).Append("\n");
            sb.Append("  timestamp: ").Append(Time).Append("\n");
            sb.Append("  block: ").Append(Block).Append("\n");
            sb.Append("  txid: ").Append(TxId).Append("\n");
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
        /// <param name="input">TransactionInput to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return Equals(input as TransactionInput);
        }
        
        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Equals(TransactionInput input)
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
                    input.Address != null &&
                    Address.SequenceEqual(input.Address)
                ) &&
                (
                    Coins == input.Coins ||
                    (Coins != null &&
                     Coins.Equals(input.Coins)) &&
                    Coins.SequenceEqual(input.Coins)
                ) &&
                (
                    Hours == input.Hours ||
                    Hours != null &&
                    input.Hours != null &&
                    Hours.SequenceEqual(input.Hours)
                ) &&
                (
                    CalculatedHours == input.CalculatedHours ||
                    CalculatedHours != null &&
                    input.CalculatedHours != null &&
                    CalculatedHours.SequenceEqual(input.CalculatedHours)
                ) &&
                (
                    Time == input.Time ||
                    (Time.Equals(input.Time))
                ) &&
                (
                    Block == input.Block ||
                    (Block.Equals(input.Block))
                ) &&
                (
                    TxId == input.TxId ||
                    (TxId != null &&
                     TxId.Equals(input.TxId))
                );
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
                hashCode = (hashCode * 397) ^ (CalculatedHours != null ? CalculatedHours.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Time.GetHashCode();
                hashCode = (hashCode * 397) ^ Block.GetHashCode();
                hashCode = (hashCode * 397) ^ (TxId != null ? TxId.GetHashCode() : 0);
                return hashCode;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}