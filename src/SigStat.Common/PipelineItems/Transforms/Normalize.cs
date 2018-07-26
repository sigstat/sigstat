using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Similar to Map, but with 0 - 1 interval.
    /// </summary>
    public class Normalize : ITransformation
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
            double min = Double.PositiveInfinity;
            double max = Double.NegativeInfinity;
            values.ForEach((d) => {
                min = Math.Min(min, d);
                max = Math.Max(max, d);
            });

            //min lesz 0, max lesz 1
            for (int i = 0; i < values.Count; i++)
                values[i] = (values[i] - min) / (max - min);//0-1

            signature.SetFeature(f, values);
        }

    }
}
