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

        [Input]
        public List<FeatureDescriptor> InputFeatures { get; set; }

        [Output("ResampledTimestamps")]
        public List<double> ResampledTimestamps { get; set; }

        [Output]
        public List<FeatureDescriptor> OutputFeatures { get; set; }

        public void Transform(Signature signature)
        {
            if (TimeSlot <= 0)
                throw new Exception("Time slot has to be positive");

            if (InputFeatures == null)
                throw new NullReferenceException("Input features are not defined");

            if (OutputFeatures == null)
                throw new NullReferenceException("Output features are not defined");

            var featureValues = new List<double>[InputFeatures.Count];

            for (int i = 0; i < InputFeatures.Count; i++)
            {
                featureValues[i] = new List<double>(signature.GetFeature<List<double>>(InputFeatures[i]));

                var originalTimestamps = signature.GetFeature(Features.T);
                signature.SetFeature(OutputFeatures[i], GenerateResampledValues(featureValues[i], originalTimestamps));
            }
        }

        private List<double> GenerateResampledValues(List<double> originalValues, List<double> originalTimestamps)
        {
            Interpolation = new LinearInterpolation()
            {
                TimeValues = originalTimestamps,
                FeatureValues = originalValues
            };

            var minTimestamp = originalTimestamps.Min();
            var numOfSteps = (int)Math.Round((originalTimestamps.Max() - minTimestamp) / TimeSlot, MidpointRounding.AwayFromZero);
            var maxTimestamp = minTimestamp + numOfSteps * TimeSlot;

            var resampledValues = new List<double>(numOfSteps + 1);

            double actualTimestamp = minTimestamp;
            while (actualTimestamp <= maxTimestamp)
            {
                ResampledTimestamps.Add(actualTimestamp);
                resampledValues.Add(Interpolation.GetValue(actualTimestamp));
                actualTimestamp += TimeSlot;
            }

            return resampledValues;
        }
    }
}
