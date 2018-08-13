using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Similar to Map, but with 0 - 1 interval.
    /// </summary>
    public class Normalize : PipelineBase, ITransformation
    {

        public Normalize()
        {
            this.Output(FeatureDescriptor<List<double>>.Descriptor("NormalizationResult"));
        }

        public void Transform(Signature signature)
        {
            List<double> values = signature.GetFeature<List<double>>(InputFeatures[0]);

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
