using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Maps values of a feature to 0.0 - 1.0 range.
    /// <para>Pipeline Input type: List{double}</para>
    /// <para>Default Pipeline Output: (List{double}) NormalizationResult</para>
    /// </summary>
    /// <remarks> This is a specific case of the <see cref="Map"/> transform. </remarks>
    public class Normalize : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> Input { get; set; }

        [Output]
        public FeatureDescriptor<List<double>> Output { get; set; }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            var values = new List<double>(signature.GetFeature(Input));
            double min = values.Min();
            double max = values.Max();
            //min lesz 0, max lesz 1
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = (values[i] - min) / (max - min);//0-1
                Progress += 100 / values.Count;
            }

            signature.SetFeature(Output, values);
        }

    }
}
