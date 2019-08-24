using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Skyapi.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Outputs : IEquatable<Outputs>, IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="headerOutputs"></param>
        /// <param name="incoming"></param>
        /// <param name="outgoing"></param>
        public Outputs(BlockVerboseSchemaHeader header = default(BlockVerboseSchemaHeader),
            List<object> headerOutputs = default(List<object>), List<object> incoming = default(List<object>),
            List<object> outgoing = default(List<object>))
        {
            Header = header;
            HeaderOutputs = headerOutputs;
            Incoming = incoming;
            Outgoing = outgoing;
        }

        /// <summary>
        /// Gets or Sets Header
        /// </summary>
        [DataMember(Name = "head", EmitDefaultValue = false)]
        public BlockVerboseSchemaHeader Header { get; set; }

        /// <summary>
        /// Gets or Sets OutputsHeader
        /// </summary>
        [DataMember(Name = "outgoing_outputs", EmitDefaultValue = false)]
        public List<object> Outgoing { get; set; }

        /// <summary>
        /// Gets or Sets OutputsHeader
        /// </summary>
        [DataMember(Name = "incoming_outputs", EmitDefaultValue = false)]
        public List<object> Incoming { get; set; }

        /// <summary>
        /// Gets or Sets OutputsHeader
        /// </summary>
        [DataMember(Name = "head_outputs", EmitDefaultValue = false)]
        public List<object> HeaderOutputs { get; set; }


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
            return Equals(input as Outputs);
        }

        /// <summary>
        /// Returns true if Address instances are equal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Equals(Outputs input)
        {
            if (input == null)
            {
                return false;
            }

            return Header != null && Header.Equals(input.Header) &&
                   (HeaderOutputs == input.HeaderOutputs || HeaderOutputs != null) &&
                   (Incoming == input.Incoming || Incoming != null) &&
                   (Outgoing == input.Outgoing || Outgoing != null);
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
                if (Header != null)
                    hashCode = hashCode * 59 + Header.GetHashCode();
                if (HeaderOutputs != null)
                    hashCode = hashCode * 59 + HeaderOutputs.GetHashCode();
                if (Incoming != null)
                    hashCode = hashCode * 59 + Incoming.GetHashCode();
                if (Outgoing != null)
                    hashCode = hashCode * 59 + Outgoing.GetHashCode();
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