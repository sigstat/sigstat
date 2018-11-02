using SigStat.Common.Helpers;
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
        /// <summary> Initializes a new instance of the <see cref="Map"/> class with specified settings. </summary>
        public Normalize()
        {
            this.Output(FeatureDescriptor.Get<List<double>>("NormalizationResult"));
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            List<double> values = signature.GetFeature<List<double>>(InputFeatures[0]).ToList();

            //find min and max values
            double min = values.Min();
            double max = values.Max();

            //min lesz 0, max lesz 1
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = (values[i] - min) / (max - min);//0-1
                Progress += 100 / values.Count;
            }

            signature.SetFeature(OutputFeatures[0], values);
        }

    }
}
