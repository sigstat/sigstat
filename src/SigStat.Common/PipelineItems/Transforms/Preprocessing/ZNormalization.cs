using Newtonsoft.Json;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Maps values of a feature to a specific range.
    /// <para>InputFeature: feature to be scaled.</para>
    /// <para>OutputFeature: output feature for scaled InputFeature</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ZNormalization : PipelineBase, ITransformation
    {
        /// <summary>
        /// Gets or sets the input feature.
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputFeature { get; set; }

        
       

        /// <summary>
        /// Gets or sets the output feature.
        /// </summary>
        [Output("Z-Normalized Feature")]
        public FeatureDescriptor<List<double>> OutputFeature { get; set; }


        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            if (InputFeature == null || OutputFeature == null)
                throw new InvalidOperationException("Input or output feature is null");

            List<double> values = new List<double>(signature.GetFeature<List<double>>(InputFeature).ToList());

            var stdDiviation = MathHelper.StdDiviation(values);
            var mean = values.Average();
            

            

            for (int i = 0; i < values.Count; i++)
            {
                values[i] = (values[i] - mean) / stdDiviation;

            }

            signature.SetFeature(OutputFeature, values);
        }
    }
}
