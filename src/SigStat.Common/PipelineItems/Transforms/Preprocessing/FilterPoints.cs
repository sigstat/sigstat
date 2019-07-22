using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public class FilterPoints : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> KeyFeatureInput { get; set; } = Features.Pressure;

        [Input]
        public List<FeatureDescriptor<List<double>>> InputFeatures { get; set; } // nem minden feature-t lehet filterezni

        [Output("FilterKeyFeatureOutput")]
        public FeatureDescriptor<List<double>> KeyFeatureOutput { get; set; }

        [Output]
        public List<FeatureDescriptor<List<double>>> OutputFeatures { get; set; }

        public int Percentile { get; set; } = 5;

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
