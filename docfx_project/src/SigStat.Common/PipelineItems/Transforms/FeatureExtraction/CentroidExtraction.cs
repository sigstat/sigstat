using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;
using Newtonsoft.Json;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Extracts the Centroid (aka. Center Of Gravity) of the input features.
    /// <para> Default Pipeline Output: (List{double}) Centroid. </para>
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class CentroidExtraction : PipelineBase, ITransformation
    {
        /// <summary>
        /// List of features to process
        /// </summary>
        [Input]
        public List<FeatureDescriptor<List<double>>> Inputs { get; set; }

        /// <summary>
        /// List of centroid values
        /// </summary>
        [Output("Centroid")]
        public FeatureDescriptor<List<double>> OutputCentroid { get; set; }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            List<double> c = new List<double>(Inputs.Count);
            foreach (var f in Inputs)
            {
                var values = signature.GetFeature(f);
                double avg = values.Average();
                c.Add(avg);
            }

            signature.SetFeature(OutputCentroid, c);
            this.LogInformation("Centroid extraction done.");
        }
    }
}
