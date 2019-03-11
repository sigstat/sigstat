using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public class ResampleTimeBased : PipelineBase, ITransformation
    {
        public double TimeSlot { get; set; } = 0;

        public IInterpolation Interpolation { get; set; }

        public List<double> ResampledTimestamps { get; private set; }

        [Input]
        public List<FeatureDescriptor<List<double>>> InputFeatures { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> OriginalTFeature { get; set; } = Features.T;

        [Output("ResampledTimestamps")]
        public FeatureDescriptor<List<double>> ResampledTFeature { get; set; }

        [Output]
        public List<FeatureDescriptor<List<double>>> OutputFeatures { get; set; }

        public void Transform(Signature signature)
        {
            if (TimeSlot <= 0)
            {
                throw new Exception("Time slot has to be positive");
            }

            if (InputFeatures == null)
            {
                throw new NullReferenceException("Input features are not defined");
            }

            if (OutputFeatures == null)
            {
                throw new NullReferenceException("Output features are not defined");
            }

            if (Interpolation == null)
            {
                throw new NullReferenceException("Interpolation is not defined");
            }

            var featureValues = new List<double>[InputFeatures.Count];

            var originalTimestamps = new List<double>(signature.GetFeature<List<double>>(OriginalTFeature));
            for (int i = 0; i < InputFeatures.Count; i++)
            {
                featureValues[i] = new List<double>(signature.GetFeature(InputFeatures[i]));
                signature.SetFeature(OutputFeatures[i], GenerateResampledValues(featureValues[i], originalTimestamps));
            }
            signature.SetFeature(ResampledTFeature, ResampledTimestamps);
        }

        private List<double> GenerateResampledValues(List<double> originalValues, List<double> originalTimestamps)
        {
            Interpolation.TimeValues = originalTimestamps;
            Interpolation.FeatureValues = originalValues;

            var minTimestamp = originalTimestamps.Min();
            var numOfSteps = (int)Math.Floor((originalTimestamps.Max() - minTimestamp) / TimeSlot);
            var maxTimestamp = minTimestamp + numOfSteps * TimeSlot;

            var resampledValues = new List<double>(numOfSteps + 1);

            if (ResampledTimestamps == null || ResampledTimestamps.Count != numOfSteps + 1)
            {
                ResampledTimestamps = new List<double>(numOfSteps + 1);

                double actualTimestamp = minTimestamp;
                while (actualTimestamp <= maxTimestamp)
                {
                    ResampledTimestamps.Add(actualTimestamp);
                    resampledValues.Add(Interpolation.GetValue(actualTimestamp));
                    actualTimestamp += TimeSlot;
                }
            }
            else
            {
                foreach (var ts in ResampledTimestamps)
                {
                    resampledValues.Add(Interpolation.GetValue(ts));
                }
            }

            return resampledValues;
        }
    }
}
