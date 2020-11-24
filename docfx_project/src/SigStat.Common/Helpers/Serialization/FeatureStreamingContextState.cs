using System.Collections.Generic;

namespace SigStat.Common.Helpers.Serialization
{
    /// <summary>
    /// SerializationContext for serializing SigStat objects
    /// </summary>
    public class FeatureStreamingContextState
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FeatureStreamingContextState(bool compactFeatures)
        {
            CompactFeatures = compactFeatures;
        }

        /// <summary>
        /// A list of already serialized FeatureDescriptor keys
        /// </summary>
        public List<string> KnownFeatureKeys { get; set; } = new List<string>();

        public bool CompactFeatures { get; set; }
    }
}
