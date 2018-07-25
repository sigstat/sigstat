using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems
{
    public class Map : ITransformation
    {
        private readonly double v0;
        private readonly double v1;
        private readonly FeatureDescriptor<List<double>> f;

        public Map(double minval, double maxval, FeatureDescriptor<List<double>> f)
        {
            this.v0 = minval;
            this.v1 = maxval;
            
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

            //min lesz v0, max lesz v1
            values.ForEach((d) => {
                double t = (d - min) / (max - min);//0-1
                d = (1.0 - t) * v0 + t * v1;//lerp
            });

            signature.SetFeature(f, values);

        }
    }
}
