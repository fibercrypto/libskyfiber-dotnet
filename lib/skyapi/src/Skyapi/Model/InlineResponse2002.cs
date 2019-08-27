/* 
 * Skycoin REST API.
 *
 * Skycoin is a next-generation cryptocurrency.
 *
 * The version of the OpenAPI document: 0.27.0
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
using OpenAPIDateConverter = Skyapi.Client.OpenAPIDateConverter;

namespace Skyapi.Model
{
    /// <summary>
    /// InlineResponse2002
    /// </summary>
    [DataContract]
    public partial class InlineResponse2002 :  IEquatable<InlineResponse2002>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineResponse2002" /> class.
        /// </summary>
        /// <param name="currentCoinhourSupply">CurrentCoinHourSupply is coins hours in non distribution addresses..</param>
        /// <param name="currentSupply">Coins distributed beyond the project..</param>
        /// <param name="lockedDistributionAddresses">Distribution addresses which are locked and do not count towards total supply..</param>
        /// <param name="maxSupply">MaxSupply is the maximum number of coins to be distributed ever..</param>
        /// <param name="totalCoinhourSupply">TotalCoinHourSupply is coin hours in all addresses including unlocked distribution addresses..</param>
        /// <param name="totalSupply">TotalSupply is CurrentSupply plus coins held by the distribution addresses that are spendable..</param>
        /// <param name="unlockedDistributionAddresses">Distribution addresses which count towards total supply..</param>
        public InlineResponse2002(string currentCoinhourSupply = default(string), string currentSupply = default(string), List<string> lockedDistributionAddresses = default(List<string>), string maxSupply = default(string), string totalCoinhourSupply = default(string), string totalSupply = default(string), List<string> unlockedDistributionAddresses = default(List<string>))
        {
            this.CurrentCoinhourSupply = currentCoinhourSupply;
            this.CurrentSupply = currentSupply;
            this.LockedDistributionAddresses = lockedDistributionAddresses;
            this.MaxSupply = maxSupply;
            this.TotalCoinhourSupply = totalCoinhourSupply;
            this.TotalSupply = totalSupply;
            this.UnlockedDistributionAddresses = unlockedDistributionAddresses;
        }
        
        /// <summary>
        /// CurrentCoinHourSupply is coins hours in non distribution addresses.
        /// </summary>
        /// <value>CurrentCoinHourSupply is coins hours in non distribution addresses.</value>
        [DataMember(Name="current_coinhour_supply", EmitDefaultValue=false)]
        public string CurrentCoinhourSupply { get; set; }

        /// <summary>
        /// Coins distributed beyond the project.
        /// </summary>
        /// <value>Coins distributed beyond the project.</value>
        [DataMember(Name="current_supply", EmitDefaultValue=false)]
        public string CurrentSupply { get; set; }

        /// <summary>
        /// Distribution addresses which are locked and do not count towards total supply.
        /// </summary>
        /// <value>Distribution addresses which are locked and do not count towards total supply.</value>
        [DataMember(Name="locked_distribution_addresses", EmitDefaultValue=false)]
        public List<string> LockedDistributionAddresses { get; set; }

        /// <summary>
        /// MaxSupply is the maximum number of coins to be distributed ever.
        /// </summary>
        /// <value>MaxSupply is the maximum number of coins to be distributed ever.</value>
        [DataMember(Name="max_supply", EmitDefaultValue=false)]
        public string MaxSupply { get; set; }

        /// <summary>
        /// TotalCoinHourSupply is coin hours in all addresses including unlocked distribution addresses.
        /// </summary>
        /// <value>TotalCoinHourSupply is coin hours in all addresses including unlocked distribution addresses.</value>
        [DataMember(Name="total_coinhour_supply", EmitDefaultValue=false)]
        public string TotalCoinhourSupply { get; set; }

        /// <summary>
        /// TotalSupply is CurrentSupply plus coins held by the distribution addresses that are spendable.
        /// </summary>
        /// <value>TotalSupply is CurrentSupply plus coins held by the distribution addresses that are spendable.</value>
        [DataMember(Name="total_supply", EmitDefaultValue=false)]
        public string TotalSupply { get; set; }

        /// <summary>
        /// Distribution addresses which count towards total supply.
        /// </summary>
        /// <value>Distribution addresses which count towards total supply.</value>
        [DataMember(Name="unlocked_distribution_addresses", EmitDefaultValue=false)]
        public List<string> UnlockedDistributionAddresses { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InlineResponse2002 {\n");
            sb.Append("  CurrentCoinhourSupply: ").Append(CurrentCoinhourSupply).Append("\n");
            sb.Append("  CurrentSupply: ").Append(CurrentSupply).Append("\n");
            sb.Append("  LockedDistributionAddresses: ").Append(LockedDistributionAddresses).Append("\n");
            sb.Append("  MaxSupply: ").Append(MaxSupply).Append("\n");
            sb.Append("  TotalCoinhourSupply: ").Append(TotalCoinhourSupply).Append("\n");
            sb.Append("  TotalSupply: ").Append(TotalSupply).Append("\n");
            sb.Append("  UnlockedDistributionAddresses: ").Append(UnlockedDistributionAddresses).Append("\n");
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
            return this.Equals(input as InlineResponse2002);
        }

        /// <summary>
        /// Returns true if InlineResponse2002 instances are equal
        /// </summary>
        /// <param name="input">Instance of InlineResponse2002 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InlineResponse2002 input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.CurrentCoinhourSupply == input.CurrentCoinhourSupply ||
                    (this.CurrentCoinhourSupply != null &&
                    this.CurrentCoinhourSupply.Equals(input.CurrentCoinhourSupply))
                ) && 
                (
                    this.CurrentSupply == input.CurrentSupply ||
                    (this.CurrentSupply != null &&
                    this.CurrentSupply.Equals(input.CurrentSupply))
                ) && 
                (
                    this.LockedDistributionAddresses == input.LockedDistributionAddresses ||
                    this.LockedDistributionAddresses != null &&
                    input.LockedDistributionAddresses != null &&
                    this.LockedDistributionAddresses.SequenceEqual(input.LockedDistributionAddresses)
                ) && 
                (
                    this.MaxSupply == input.MaxSupply ||
                    (this.MaxSupply != null &&
                    this.MaxSupply.Equals(input.MaxSupply))
                ) && 
                (
                    this.TotalCoinhourSupply == input.TotalCoinhourSupply ||
                    (this.TotalCoinhourSupply != null &&
                    this.TotalCoinhourSupply.Equals(input.TotalCoinhourSupply))
                ) && 
                (
                    this.TotalSupply == input.TotalSupply ||
                    (this.TotalSupply != null &&
                    this.TotalSupply.Equals(input.TotalSupply))
                ) && 
                (
                    this.UnlockedDistributionAddresses == input.UnlockedDistributionAddresses ||
                    this.UnlockedDistributionAddresses != null &&
                    input.UnlockedDistributionAddresses != null &&
                    this.UnlockedDistributionAddresses.SequenceEqual(input.UnlockedDistributionAddresses)
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
                if (this.CurrentCoinhourSupply != null)
                    hashCode = hashCode * 59 + this.CurrentCoinhourSupply.GetHashCode();
                if (this.CurrentSupply != null)
                    hashCode = hashCode * 59 + this.CurrentSupply.GetHashCode();
                if (this.LockedDistributionAddresses != null)
                    hashCode = hashCode * 59 + this.LockedDistributionAddresses.GetHashCode();
                if (this.MaxSupply != null)
                    hashCode = hashCode * 59 + this.MaxSupply.GetHashCode();
                if (this.TotalCoinhourSupply != null)
                    hashCode = hashCode * 59 + this.TotalCoinhourSupply.GetHashCode();
                if (this.TotalSupply != null)
                    hashCode = hashCode * 59 + this.TotalSupply.GetHashCode();
                if (this.UnlockedDistributionAddresses != null)
                    hashCode = hashCode * 59 + this.UnlockedDistributionAddresses.GetHashCode();
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
