using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Performs linear interpolation on the input
    /// </summary>
    /// <seealso cref="SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation" />
    [JsonObject(MemberSerialization.OptOut)]
    public class LinearInterpolation : IInterpolation
    {
        ///<inheritdoc/>
        public List<double> FeatureValues { get; set; }

        ///<inheritdoc/>
        public List<double> TimeValues { get; set; }

        /// <summary>
        /// Gets the interpolated value at a given timestamp
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">TimeValues is not initialized</exception>
        /// <exception cref="NullReferenceException">FeatureValues is not initialized</exception>
        /// <exception cref="ArgumentOutOfRangeException">The given timestamp is not in the range of TimeValues</exception>
        public double GetValue(double timestamp)
        {
            if (TimeValues == null)
                throw new InvalidOperationException("TimeValues is not initialized");

            if (FeatureValues == null)
                throw new InvalidOperationException("FeatureValues is not initialized");


            if (TimeValues.Contains(timestamp))
                return FeatureValues[TimeValues.IndexOf(timestamp)];


            double v0 = 0, v1 = 0, t0 = 0, t1 = 0;
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
                    isRangeFound = true;
                }
            }

            if (!isRangeFound)
                throw new ArgumentOutOfRangeException("The given timestamp is not in the range of TimeValues");

            return v0 + (timestamp - t0) * (v1 - v0) / (t1 - t0) ;
        }
    }
}
