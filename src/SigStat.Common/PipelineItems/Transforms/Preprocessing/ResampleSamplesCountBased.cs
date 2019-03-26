using Newtonsoft.Json;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ResampleSamplesCountBased : PipelineBase, ITransformation
    {
        public int NumOfSamples { get; set; } = 0;

        public Type InterpolationType { get; set; }

        //public List<double> ResampledTimestamps { get; private set; }

        [Input]
        [JsonProperty]
        public List<FeatureDescriptor<List<double>>> InputFeatures { get; set; }

        [Input]
        [JsonProperty]
        public FeatureDescriptor<List<double>> OriginalTFeature { get; set; } = Features.T;

        [Output("ResampledTimestamps")]
        public FeatureDescriptor<List<double>> ResampledTFeature { get; set; }

        [Output]
        public List<FeatureDescriptor<List<double>>> OutputFeatures { get; set; }

        public void Transform(Signature signature)
        {
            if (NumOfSamples <= 0)
            {
                throw new Exception("Number of samples has to be positive");
            }

            if (InputFeatures == null)
            {
                throw new NullReferenceException("Input features are not defined");
            }

            if (OutputFeatures == null)
            {
                throw new NullReferenceException("Output features are not defined");
            }

            if (InterpolationType == null)
            {
                throw new NullReferenceException("Interpolation is not defined");
            }

            var featureValues = new List<double>[InputFeatures.Count];

            var originalTimestamps = new List<double>(signature.GetFeature(OriginalTFeature));
            var resampledTimestamps = GenerateNewTimestamps(originalTimestamps);

            for (int i = 0; i < InputFeatures.Count; i++)
            {
                featureValues[i] = new List<double>(signature.GetFeature(InputFeatures[i]));
                signature.SetFeature(OutputFeatures[i], GenerateResampledValues(featureValues[i], originalTimestamps, resampledTimestamps));
            }
            signature.SetFeature(ResampledTFeature, resampledTimestamps);
        }

        private List<double> GenerateResampledValues(List<double> originalValues, List<double> originalTimestamps, List<double> resampledTimestamps)
        {
            var interpolation = (IInterpolation)Activator.CreateInstance(InterpolationType);
            interpolation.TimeValues = originalTimestamps;
            interpolation.FeatureValues = originalValues;

            var resampledValues = new List<double>(NumOfSamples);

            foreach (var ts in resampledTimestamps)
            {
                resampledValues.Add(interpolation.GetValue(ts));
            }

            return resampledValues;
        }

        private List<double> GenerateNewTimestamps(List<double> originalTimestamps)
        {
            var minTimestamp = originalTimestamps.Min();
            double timeSlot = (originalTimestamps.Max() - minTimestamp) / (NumOfSamples - 1);
            var resampledTimestamps = new List<double>();

            double actualTimestamp = minTimestamp;
            int stepCount = 0;
            while (stepCount < NumOfSamples - 1)
            {
                resampledTimestamps.Add(actualTimestamp);
                actualTimestamp += timeSlot;
                stepCount++;
            }

            return resampledTimestamps;
        }
    }
}
