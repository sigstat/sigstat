using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public class FillPenUpDurations : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<bool>> PenUpInputFeature { get; set; } = Features.Button;

        [Input]
        public FeatureDescriptor<List<double>> TimeInputFeature { get; set; } = Features.T;

        [Input]
        public List<FeatureDescriptor<List<double>>> InputFeatures { get; set; }

        [Output("FilledTime")]
        public FeatureDescriptor<List<double>> TimeOutputFeature { get; set; }

        [Output]
        public List<FeatureDescriptor<List<double>>> OutputFeatures { get; set; }

        public List<PenUpDuration> PenUpDurations { get; set; }

        public int FillUpTimeSlot { get; set; } = 0;

        public IInterpolation Interpolation { get; set; }

        public void Transform(Signature signature)
        {
            if (FillUpTimeSlot == 0)
            {
                throw new Exception("Time slot between samples filled up has to be greater than 0");
            }

            if (Interpolation == null)
            {
                throw new NullReferenceException("Interpolation is not defined");
            }

            if (InputFeatures == null)
            {
                throw new NullReferenceException("Input features are not defined");
            }

            if (OutputFeatures == null)
            {
                throw new NullReferenceException("Output features are not defined");
            }


            if (PenUpDurations == null)
            {
                PenUpDurations = CalculatePenUpDurations(signature);
            }

            var avgPenUpDuration = CalculateAveragePenUpDuration(PenUpDurations);

            var longPenUpDurations = new List<PenUpDuration>(PenUpDurations.Count);
            foreach (var penUpDuration in PenUpDurations)
            {
                if (penUpDuration.Length > avgPenUpDuration)
                {
                    longPenUpDurations.Add(penUpDuration);
                }
            }

            var timeValues = new List<double>(signature.GetFeature(TimeInputFeature));

            for (int i = 0; i < InputFeatures.Count; i++)
            {
                var values = new List<double>(signature.GetFeature(InputFeatures[i]));
                Interpolation.FeatureValues = new List<double>(values);
                Interpolation.TimeValues = new List<double>(timeValues);

                foreach (var penUpDuration in longPenUpDurations)
                {
                    var actualTimestamp = penUpDuration.StartTime + FillUpTimeSlot;
                    int actualIndex = timeValues.IndexOf(penUpDuration.StartTime) + 1;
                    while (actualTimestamp < penUpDuration.EndTime)
                    {
                        if (i == 0) { timeValues.Insert(actualIndex, actualTimestamp); }
                        values.Insert(actualIndex, Interpolation.GetValue(actualTimestamp));

                        actualTimestamp += FillUpTimeSlot;
                        actualIndex++;
                    }
                }

                if (i == 0) { signature.SetFeature(TimeOutputFeature, timeValues); }
                signature.SetFeature(OutputFeatures[i], values);
            }


        }

        private double CalculateAveragePenUpDuration(List<PenUpDuration> penUpDurations)
        {
            double sum = 0;
            foreach (var pud in penUpDurations)
            {
                sum += pud.Length;
            }

            return sum / penUpDurations.Count;
        }

        //TODO: erősen SCV2004 felpítésre támaszkodó
        private List<PenUpDuration> CalculatePenUpDurations(Signature signature)
        {
            var penUpDurations = new List<PenUpDuration>();
            var buttonStatuses = signature.GetFeature(PenUpInputFeature);
            var timesValues = signature.GetFeature(TimeInputFeature);

            for (int i = 1; i < buttonStatuses.Count; i++)
            {
                if (!buttonStatuses[i])
                {
                    penUpDurations.Add(new PenUpDuration() { StartTime = timesValues[i - 1], EndTime = timesValues[i] });
                }
            }

            return penUpDurations;
        }
    }
}
