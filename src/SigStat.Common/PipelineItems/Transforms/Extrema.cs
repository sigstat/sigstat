using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Find minimum and maximum values of given feature, add them as new features
    /// </summary>
    public class Extrema : ITransformation
    {
        private readonly FeatureDescriptor<List<double>> f;
        private readonly string minFeatureName;
        private readonly string maxFeatureName;

        public Extrema(FeatureDescriptor<List<double>> f, string minFeatureName, string maxFeatureName)
        {
            this.f = f;
            this.minFeatureName = minFeatureName;
            this.maxFeatureName = maxFeatureName;
        }

        public void Transform(Signature signature)
        {
            List<double> values = signature.GetFeature(f);
            //find min and max values
            double min = Double.PositiveInfinity;
            double max = Double.NegativeInfinity;
            values.ForEach((d) => {
                min = Math.Min(min, d);
                max = Math.Max(max, d);
            });

            signature[minFeatureName] = new List<double> { min };//proba: minden featureben lehessen több érték, akkor is ha csak 1-et tarolunk
            signature[maxFeatureName] = new List<double> { max };
        }
    }
}
