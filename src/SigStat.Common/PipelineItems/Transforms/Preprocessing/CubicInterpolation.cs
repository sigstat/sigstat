using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Cubic interpolation algorithm
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class CubicInterpolation : IInterpolation
    {
        /// <summary>
        /// FeatureValues
        /// </summary>
        public List<double> FeatureValues { get; set; }

        /// <summary>
        /// TimeValues
        /// </summary>
        public List<double> TimeValues { get; set; }

        /// <summary>Gets the value.</summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">List of timestamps is null
        /// or
        /// List of feature values is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">The given timestamp is not in the range of TimeValues</exception>
        public double GetValue(double timestamp)
        {
            if (TimeValues == null)
                throw new InvalidOperationException("List of timestamps is null");

            if (FeatureValues == null)
                throw new InvalidOperationException("List of feature values is null");


            if (TimeValues.Contains(timestamp))
                return FeatureValues[TimeValues.IndexOf(timestamp)];


            double v0 = 0, v1 = 0, t0 = 0, t1 = 0;
            double v_1 = 0, t_1 = 0, v2 = 0, t2 = 0;
            bool isRangeFound = false;
            bool isCubic = false;
            //TODO: itt még lehetne teljesítményt optimalizálni
            for (int i = 0; i < TimeValues.Count - 1 && !isRangeFound; i++)
            {
                if (TimeValues[i] < timestamp && TimeValues[i + 1] > timestamp)
                {
                    t0 = TimeValues[i];
                    t1 = TimeValues[i + 1];
                    v0 = FeatureValues[i];
                    v1 = FeatureValues[i + 1];
                    if (i > 1 && i < TimeValues.Count - 2)
                    {
                        v_1 = FeatureValues[i - 1];
                        t_1 = TimeValues[i - 1];
                        v2 = FeatureValues[i + 2];
                        t2 = TimeValues[i + 2];
                        isCubic = true;
                    }

                    isRangeFound = true;
                }
            }

            if (!isRangeFound)
                throw new ArgumentOutOfRangeException("The given timestamp is not in the range of TimeValues");

            if(isCubic)
            {
                double t = (timestamp - t0) / (t1 - t0);
                double tSquare = t * t;
                double t3 = t * t * t;

                //expanded
                var h00 = 2 * t3 - 3 * tSquare + 1;
                var h10 = t3 - 2 * tSquare + t;
                var h01 = -2 * t3 + 3 * tSquare;
                var h11 = t3 - tSquare;
#pragma warning disable S125
                //factorized
                //var h00 = (1 + 2 * t) * (1 - t) * (1 - t);
                //var h10 = t * (1 - t) * (1 - t);
                //var h01 = t * t * (3 - 2 * t);
                //var h11 = t * t * (t - 1);
#pragma warning restore S125
                var m0 = (1 / 2.0) * ((v1 - v0) / (t1 - t0) + (v0 - v_1) / (t0 - t_1));
                var m1 = (1 / 2.0) * ((v2 - v1) / (t2 - t1) + (v1 - v0) / (t1 - t0));

                var scale = t1 - t0;

                return h00 * v0 + h10 * scale * m0 + h01 * v1 + h11 * scale * m1;
            }
            else
            {
                return v0 + (timestamp - t0) * (v1 - v0) / (t1 - t0);
            }

        }

    }
}

