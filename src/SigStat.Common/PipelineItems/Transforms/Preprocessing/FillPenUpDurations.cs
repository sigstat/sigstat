using Newtonsoft.Json;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// This transformation will fill "holes" in the "Time" feature by interpolating the last known
    /// feature values. 
    /// </summary>
    /// <seealso cref="SigStat.Common.PipelineBase" />
    /// <seealso cref="SigStat.Common.ITransformation" />
    [JsonObject(MemberSerialization.OptOut)]
    public class FillPenUpDurations : PipelineBase, ITransformation
    {
        /// <summary>
        /// Helper class for <see cref="FillPenUpDurations"/>
        /// </summary>
        public class TimeSlot
        {
            private double _startTime;
            private double _endTime;
            private bool isStartInitialized = false;
            private bool isEndInitialized = false;

            /// <summary>
            /// Gets or sets the start time of the slot
            /// </summary>
            public double StartTime
            {
                get => _startTime;
                set
                {
                    _startTime = value;
                    isStartInitialized = true;
                    if (isEndInitialized)
                    {
                        Length = _endTime - _startTime;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the end time of the slot
            /// </summary>
            public double EndTime
            {
                get => _endTime;
                set
                {
                    _endTime = value;
                    isEndInitialized = true;
                    if (isStartInitialized)
                    {
                        Length = _endTime - _startTime;
                    }
                }
            }

            /// <summary>
            /// Gets the length of the slot
            /// </summary>
            public double Length { get; private set; }
        }


        /// <summary>
        /// Gets or sets the feature representing the timestamps of an online signature
        /// </summary>
        [Input]        
        public FeatureDescriptor<List<double>> TimeInputFeature { get; set; } = Features.T;

        /// <summary>
        /// Gets or sets the features of an online signature that need to be altered
        /// </summary>
        [Input]
        public List<FeatureDescriptor<List<double>>> InputFeatures { get; set; }

        /// <summary>
        /// Gets or sets the feature representing the modified timestamps of an online signature
        /// </summary>
        [Output("FilledTime")]
        public FeatureDescriptor<List<double>> TimeOutputFeature { get; set; }

        /// <summary>
        /// Gets or sets the features of an online signature that were altered
        /// </summary>
        [Output]
        public List<FeatureDescriptor<List<double>>> OutputFeatures { get; set; }

        //public List<TimeSlot> TimeSlots { get; set; }

        /// <summary>
        /// An implementation of <see cref="IInterpolation"/>
        /// </summary>
        public Type InterpolationType { get; set; }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {

            if (InterpolationType == null)
            {
                throw new NullReferenceException("InterpolationType is not defined");
            }

            if (InputFeatures == null)
            {
                throw new NullReferenceException("Input features are not defined");
            }

            if (OutputFeatures == null)
            {
                throw new NullReferenceException("Output features are not defined");
            }


            var timeSlots = CalculateTimeSlots(signature);

            var timeSlotMedian = CalculateTimeSlotsMedian(timeSlots);

            var longTimeSlots = new List<TimeSlot>(timeSlots.Count);
            foreach (var ts in timeSlots)
            {
                if (ts.Length > timeSlotMedian)
                {
                    longTimeSlots.Add(ts);
                }
            }

            var originalTimeValues = new List<double>(signature.GetFeature(TimeInputFeature));
            var timeValues = new List<double>(signature.GetFeature(TimeInputFeature));

            var interpolation = (IInterpolation)Activator.CreateInstance(InterpolationType);

            for (int i = 0; i < InputFeatures.Count; i++)
            {
                var values = new List<double>(signature.GetFeature(InputFeatures[i]));
                interpolation.FeatureValues = new List<double>(values);
                interpolation.TimeValues = originalTimeValues;

                foreach (var ts in longTimeSlots)
                {
                    var actualTimestamp = ts.StartTime + timeSlotMedian;
                    int actualIndex = timeValues.IndexOf(ts.StartTime) + 1;
                    while (actualTimestamp < ts.EndTime)
                    {
                        if (i == 0) { timeValues.Insert(actualIndex, actualTimestamp); }
                        values.Insert(actualIndex, interpolation.GetValue(actualTimestamp));

                        actualTimestamp += timeSlotMedian;
                        actualIndex++;
                    }
                }

                if (i == 0) { signature.SetFeature(TimeOutputFeature, timeValues); }
                signature.SetFeature(OutputFeatures[i], values);
            }


        }

        private double CalculateTimeSlotsMedian(List<TimeSlot> timeSlots)
        {
            var timeSlotsLength = new List<double>(timeSlots.Count);
            foreach (var ts in timeSlots)
            {
                timeSlotsLength.Add(ts.Length);
            }
            timeSlotsLength.Sort();

            if (timeSlotsLength.Count % 2 == 0)
            {
                int i = (timeSlotsLength.Count / 2) - 1;
                return (timeSlotsLength[i] + timeSlotsLength[i + 1]) / 2;
            }
            else
            {
                int i = timeSlotsLength.Count / 2;
                return timeSlotsLength[i];
            }
        }

        private List<TimeSlot> CalculateTimeSlots(Signature signature)
        {
            var timesValues = signature.GetFeature(TimeInputFeature);
            var timeSlots = new List<TimeSlot>(timesValues.Count - 1);

            for (int i = 0; i < timesValues.Count - 1; i++)
            {
                timeSlots.Add(new TimeSlot() { StartTime = timesValues[i], EndTime = timesValues[i + 1] });
            }

            return timeSlots;
        }
    }
}
