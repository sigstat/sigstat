using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public class CubicInterpolation : IInterpolation
    {
        public List<double> FeatureValues { get; set; }
        public List<double> TimeValues { get; set; }

        public double GetValue(double timestamp)
        {
            if (TimeValues == null)
                throw new NullReferenceException("List of timestamps is null");

            if (FeatureValues == null)
                throw new NullReferenceException("List of feature values is null");


            if (TimeValues.Contains(timestamp))
                return FeatureValues[TimeValues.IndexOf(timestamp)];


            double v0 = 0, v1 = 0, t0 = 0, t1 = 0;
            double v_1 = 0, t_1 = 0, v2 = 0, t2 = 0;
            bool isRangeFound = false;
            //TODO: itt még lehetne teljesítményt optimalizálni
            for (int i = 0; i < TimeValues.Count - 1 && !isRangeFound; i++)
            {
                if (TimeValues[i] < timestamp && TimeValues[i + 1] > timestamp)
                {
                    t0 = TimeValues[i];
                    t1 = TimeValues[i + 1];
                    v0 = FeatureValues[i];
                    v1 = FeatureValues[i + 1];
                    v_1 = i > 0 ? FeatureValues[i - 1] : FeatureValues[i];
                    t_1 = i > 0 ? TimeValues[i - 1] : TimeValues[i]; //TODO: itt még lehet 0-val osztás miatt baj
                    v2 = i < TimeValues.Count - 2 ? FeatureValues[i + 1] : FeatureValues[i];
                    t_1 = i > TimeValues.Count - 2 ? TimeValues[i + 1] : TimeValues[i]; //TODO: itt még lehet 0-val osztás miatt baj
                    isRangeFound = true;
                }
            }

            if (!isRangeFound)
                throw new ArgumentOutOfRangeException("The given timestamp is not in the range of TimeValues");

            double t = (timestamp - t0) / (t1 - t0);
            double tSquare = t * t;
            double t3 = t * t * t;

            var h00 = 2 * t3 - 3 * tSquare + 1;
            var h10 = t3 - 2 * tSquare + t;
            var h01 = -2 * t3 + 3 * tSquare;
            var h11 = t3 - tSquare;

            var m0 = (1 / 2.0) * ((v1 - v0) / (t1 - t0) + (v0 - v_1) / (t0 - t_1));
            var m1 = (1 / 2.0) * ((v2 - v1) / (t2 - t1) + (v1 - v0) / (t1 - t0));

            var scale = t1 - t0;

            return h00 * v0 + h10 * scale * m0 + h01 * v1 + h11 * scale * m1;
        }

    }
}

