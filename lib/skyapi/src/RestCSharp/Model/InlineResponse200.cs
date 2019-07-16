/* 
 * Skycoin REST API.
 *
 * Skycoin is a next-generation cryptocurrency.
 *
 * OpenAPI spec version: 0.25.1
 * Contact: contact@skycoin.net
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = RestCSharp.Client.OpenAPIDateConverter;

namespace RestCSharp.Model
{
    /// <summary>
    /// InlineResponse200
    /// </summary>
    [DataContract]
    public partial class InlineResponse200 :  IEquatable<InlineResponse200>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineResponse200" /> class.
        /// </summary>
        /// <param name="hours">hours.</param>
        /// <param name="coins">coins.</param>
        /// <param name="uxid">uxid.</param>
        /// <param name="ownerAddress">ownerAddress.</param>
        /// <param name="spentBlockSeq">spentBlockSeq.</param>
        /// <param name="spentTx">spentTx.</param>
        /// <param name="time">time.</param>
        /// <param name="srcBlockSeq">srcBlockSeq.</param>
        /// <param name="srcTx">srcTx.</param>
        public InlineResponse200(long? hours = default(long?), int? coins = default(int?), string uxid = default(string), string ownerAddress = default(string), int? spentBlockSeq = default(int?), string spentTx = default(string), long? time = default(long?), long? srcBlockSeq = default(long?), string srcTx = default(string))
        {
            this.Hours = hours;
            this.Coins = coins;
            this.Uxid = uxid;
            this.OwnerAddress = ownerAddress;
            this.SpentBlockSeq = spentBlockSeq;
            this.SpentTx = spentTx;
            this.Time = time;
            this.SrcBlockSeq = srcBlockSeq;
            this.SrcTx = srcTx;
        }
        
        /// <summary>
        /// Gets or Sets Hours
        /// </summary>
        [DataMember(Name="hours", EmitDefaultValue=false)]
        public long? Hours { get; set; }

        /// <summary>
        /// Gets or Sets Coins
        /// </summary>
        [DataMember(Name="coins", EmitDefaultValue=false)]
        public int? Coins { get; set; }

        /// <summary>
        /// Gets or Sets Uxid
        /// </summary>
        [DataMember(Name="uxid", EmitDefaultValue=false)]
        public string Uxid { get; set; }

        /// <summary>
        /// Gets or Sets OwnerAddress
        /// </summary>
        [DataMember(Name="owner_address", EmitDefaultValue=false)]
        public string OwnerAddress { get; set; }

        /// <summary>
        /// Gets or Sets SpentBlockSeq
        /// </summary>
        [DataMember(Name="spent_block_seq", EmitDefaultValue=false)]
        public int? SpentBlockSeq { get; set; }

        /// <summary>
        /// Gets or Sets SpentTx
        /// </summary>
        [DataMember(Name="spent_tx", EmitDefaultValue=false)]
        public string SpentTx { get; set; }

        /// <summary>
        /// Gets or Sets Time
        /// </summary>
        [DataMember(Name="time", EmitDefaultValue=false)]
        public long? Time { get; set; }

        /// <summary>
        /// Gets or Sets SrcBlockSeq
        /// </summary>
        [DataMember(Name="src_block_seq", EmitDefaultValue=false)]
        public long? SrcBlockSeq { get; set; }

        /// <summary>
        /// Gets or Sets SrcTx
        /// </summary>
        [DataMember(Name="src_tx", EmitDefaultValue=false)]
        public string SrcTx { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InlineResponse200 {\n");
            sb.Append("  Hours: ").Append(Hours).Append("\n");
            sb.Append("  Coins: ").Append(Coins).Append("\n");
            sb.Append("  Uxid: ").Append(Uxid).Append("\n");
            sb.Append("  OwnerAddress: ").Append(OwnerAddress).Append("\n");
            sb.Append("  SpentBlockSeq: ").Append(SpentBlockSeq).Append("\n");
            sb.Append("  SpentTx: ").Append(SpentTx).Append("\n");
            sb.Append("  Time: ").Append(Time).Append("\n");
            sb.Append("  SrcBlockSeq: ").Append(SrcBlockSeq).Append("\n");
            sb.Append("  SrcTx: ").Append(SrcTx).Append("\n");
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
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as InlineResponse200);
        }

        /// <summary>
        /// Returns true if InlineResponse200 instances are equal
        /// </summary>
        /// <param name="input">Instance of InlineResponse200 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InlineResponse200 input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Hours == input.Hours ||
                    (this.Hours != null &&
                    this.Hours.Equals(input.Hours))
                ) && 
                (
                    this.Coins == input.Coins ||
                    (this.Coins != null &&
                    this.Coins.Equals(input.Coins))
                ) && 
                (
                    this.Uxid == input.Uxid ||
                    (this.Uxid != null &&
                    this.Uxid.Equals(input.Uxid))
                ) && 
                (
                    this.OwnerAddress == input.OwnerAddress ||
                    (this.OwnerAddress != null &&
                    this.OwnerAddress.Equals(input.OwnerAddress))
                ) && 
                (
                    this.SpentBlockSeq == input.SpentBlockSeq ||
                    (this.SpentBlockSeq != null &&
                    this.SpentBlockSeq.Equals(input.SpentBlockSeq))
                ) && 
                (
                    this.SpentTx == input.SpentTx ||
                    (this.SpentTx != null &&
                    this.SpentTx.Equals(input.SpentTx))
                ) && 
                (
                    this.Time == input.Time ||
                    (this.Time != null &&
                    this.Time.Equals(input.Time))
                ) && 
                (
                    this.SrcBlockSeq == input.SrcBlockSeq ||
                    (this.SrcBlockSeq != null &&
                    this.SrcBlockSeq.Equals(input.SrcBlockSeq))
                ) && 
                (
                    this.SrcTx == input.SrcTx ||
                    (this.SrcTx != null &&
                    this.SrcTx.Equals(input.SrcTx))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Hours != null)
                    hashCode = hashCode * 59 + this.Hours.GetHashCode();
                if (this.Coins != null)
                    hashCode = hashCode * 59 + this.Coins.GetHashCode();
                if (this.Uxid != null)
                    hashCode = hashCode * 59 + this.Uxid.GetHashCode();
                if (this.OwnerAddress != null)
                    hashCode = hashCode * 59 + this.OwnerAddress.GetHashCode();
                if (this.SpentBlockSeq != null)
                    hashCode = hashCode * 59 + this.SpentBlockSeq.GetHashCode();
                if (this.SpentTx != null)
                    hashCode = hashCode * 59 + this.SpentTx.GetHashCode();
                if (this.Time != null)
                    hashCode = hashCode * 59 + this.Time.GetHashCode();
                if (this.SrcBlockSeq != null)
                    hashCode = hashCode * 59 + this.SrcBlockSeq.GetHashCode();
                if (this.SrcTx != null)
                    hashCode = hashCode * 59 + this.SrcTx.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}