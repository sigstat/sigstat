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
        private readonly FeatureDescriptor<List<double>> f;

        public Normalize(FeatureDescriptor<List<double>> f)
        {
            this.f = f;
        }

        public void Transform(Signature signature)
        {
            var values = signature.GetFeature(f);

            //find min and max values
            double min = values.Min();
            double max = values.Max();

            //min lesz 0, max lesz 1
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = (values[i] - min) / (max - min);//0-1
                Progress += 100 / values.Count;
            }

            signature[f] = values;
        }

    }
}
