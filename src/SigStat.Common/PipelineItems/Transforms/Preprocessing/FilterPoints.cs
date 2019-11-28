using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Removes samples based on a criteria from online signature time series
    /// </summary>
    /// <seealso cref="SigStat.Common.PipelineBase" />
    /// <seealso cref="SigStat.Common.ITransformation" />
    public class FilterPoints : PipelineBase, ITransformation
    {
        /// <summary>
        /// <see cref="FeatureDescriptor"/> that controls the removal of samples (e.g. <see cref="Features.Pressure"/>)
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> KeyFeatureInput { get; set; } = Features.Pressure;

        /// <summary>
        /// <see cref="FeatureDescriptor"/> list of all features to resample
        /// </summary>
        [Input]
        public List<FeatureDescriptor<List<double>>> InputFeatures { get; set; }

        /// <summary>
        /// Resampled output for <see cref="FeatureDescriptor"/> that controls the removal of samples (e.g. <see cref="Features.Pressure"/>)
        /// </summary>
        [Output("FilterKeyFeatureOutput")]
        public FeatureDescriptor<List<double>> KeyFeatureOutput { get; set; }

        /// <summary>
        /// Resampled output for all input features
        /// </summary>
        [Output]
        public List<FeatureDescriptor<List<double>>> OutputFeatures { get; set; }

        /// <summary>
        /// The lowes percentile of the <see cref="KeyFeatureInput"/> will be removed during filtering
        /// </summary>
        public int Percentile { get; set; } = 5;

        ///<inheritdoc/>
        public void Transform(Signature signature)
        {
            var keyFeatureValues = new List<double>(signature.GetFeature(KeyFeatureInput));

            //var filterValue = CalculatePercentile(Percentile, keyFeatureValues);
            var filterValue = 0;

            var indexes = new List<int>();
            for (int i = keyFeatureValues.Count-1; i > 0; i--)
            {
                if(keyFeatureValues[i] <= filterValue)
                {
                    keyFeatureValues.RemoveAt(i);
                    indexes.Add(i);
                }
            }
            signature.SetFeature(KeyFeatureOutput, keyFeatureValues);

            for (int i = 0; i < InputFeatures.Count; i++)
            {
                var values = new List<double>(signature.GetFeature(InputFeatures[i]));
                foreach (var index in indexes)
                {
                    values.RemoveAt(index);
                }
                signature.SetFeature(OutputFeatures[i], values);
            }

        }

        private double CalculatePercentile(int percentile, List<double> values)
        {
            var min = 0;// values.Min();
            var rangeLength = values.Max() - min;
            var unitLength = rangeLength / 100.0;
            return min + unitLength * percentile;
        }
    }
}
