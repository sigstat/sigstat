using Newtonsoft.Json;
using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// This transformation fills gaps of online signature by interpolating the last known
    /// feature values. Gaps should be represented in the signature with two zero pressure border points.
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
            private bool isStartInitialized;
            private bool isEndInitialized;

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

            /// <summary>
            /// This indicates whether the pen touches the paper during the time slot
            /// </summary>
            public bool PenDown { get; set; }
        }


        /// <summary>
        /// Gets or sets the feature representing the timestamps of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> TimeInputFeature { get; set; } = Features.T;

        /// <summary>
        /// Gets or sets the feature representing pressure in an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> PressureInputFeature { get; set; } = Features.Pressure;

        /// <summary>
        /// Gets or sets the feature representing the type of the points in an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> PointTypeInputFeature { get; set; } = Features.PointType;

        /// <summary>
        /// Gets or sets the features of an online signature that need to be altered
        /// </summary>
        [Input]
        public List<FeatureDescriptor<List<double>>> InputFeatures { get; set; } = new List<FeatureDescriptor<List<double>>>();

        /// <summary>
        /// Gets or sets the feature representing the modified timestamps of an online signature
        /// </summary>
        [Output("FilledTime")]
        public FeatureDescriptor<List<double>> TimeOutputFeature { get; set; }

        /// <summary>
        /// Gets or sets the feature representing the modified pressure values of an online signature
        /// </summary>
        [Output]
        public FeatureDescriptor<List<double>> PressureOutputFeature { get; set; }

        /// <summary>
        /// Gets or sets the feature representing the modified point type values in an online signature
        /// </summary>
        [Output]
        public FeatureDescriptor<List<double>> PointTypeOutputFeature { get; set; }

        /// <summary>
        /// Gets or sets the features of an online signature that were altered
        /// </summary>
        [Output]
        public List<FeatureDescriptor<List<double>>> OutputFeatures { get; set; } = new List<FeatureDescriptor<List<double>>>();

      

        /// <summary>
        /// An implementation of <see cref="IInterpolation"/>
        /// </summary>
        public Type InterpolationType { get; set; }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {

            if (InterpolationType == null)
            {
                throw new InvalidOperationException("InterpolationType is not defined");
            }

            if (InputFeatures == null)
            {
                throw new InvalidOperationException("Input features are not defined");
            }

            if (OutputFeatures == null)
            {
                throw new InvalidOperationException("Output features are not defined");
            }

            var originalTimeValues = new List<double>(signature.GetFeature(TimeInputFeature));
            var timeValues = new List<double>(signature.GetFeature(TimeInputFeature));
            var pressureValues = new List<double>(signature.GetFeature(PressureInputFeature));
            var pointTypeValues = new List<double>(signature.GetFeature(PointTypeInputFeature));

            var timeSlots = CalculateTimeSlots(signature);
            var timeSlotMedian = timeSlots.Where(ts => ts.PenDown == true).Select(ts => ts.Length).Median();
            var penUpTimeSlots = timeSlots.Where(ts => ts.PenDown == false && ts.Length > timeSlotMedian).ToList();

            if (penUpTimeSlots.Count != 0)
            {
                var interpolation = (IInterpolation)Activator.CreateInstance(InterpolationType);

                for (int i = 0; i < InputFeatures.Count; i++)
                {
                    if (InputFeatures[i] == TimeInputFeature || InputFeatures[i] == PressureInputFeature || InputFeatures[i] == PointTypeInputFeature)
                        continue;
                    var values = new List<double>(signature.GetFeature(InputFeatures[i]));
                    interpolation.FeatureValues = new List<double>(values);
                    interpolation.TimeValues = new List<double>(originalTimeValues);

                    foreach (var ts in penUpTimeSlots)
                    {
                        var actualTimestamp = ts.StartTime + timeSlotMedian;
                        int actualIndex = timeValues.IndexOf(ts.StartTime) + 1;

                        while (actualTimestamp < ts.EndTime)
                        {
                            if (i == 0)
                            {
                                timeValues.Insert(actualIndex, actualTimestamp);
                                pressureValues.Insert(actualIndex, 0);
                                pointTypeValues.Insert(actualIndex, 0);
                            }
                            values.Insert(actualIndex, interpolation.GetValue(actualTimestamp));

                            actualTimestamp += timeSlotMedian;
                            actualIndex++;
                        }

                    }

                    if (i == 0)
                    {
                        signature.SetFeature(TimeOutputFeature, timeValues);
                        signature.SetFeature(PressureOutputFeature, pressureValues);
                        signature.SetFeature(PointTypeOutputFeature, pointTypeValues);
                    }
                    signature.SetFeature(OutputFeatures[i], values); //TODO: make sure InputFeatures-OutputFeatures are in pairs
                }
            }


        }


        private List<TimeSlot> CalculateTimeSlots(Signature signature)
        {
            var pressureValues = signature.GetFeature(PressureInputFeature);
            var timesValues = signature.GetFeature(TimeInputFeature);
            var pointTypeValues = signature.GetFeature(PointTypeInputFeature);

            if (pressureValues.Count != timesValues.Count)
                throw new ArgumentException($"The length of {nameof(PressureInputFeature)} and {nameof(TimeInputFeature)} are not the same.");
            if (pressureValues.Count != pointTypeValues.Count)
                throw new ArgumentException($"The length of {nameof(PressureInputFeature)} and {nameof(PointTypeInputFeature)} are not the same.");



            return timesValues
                .Select((t, i) => new TimeSlot
                {
                    StartTime = timesValues[i > 0 ? i - 1 : 0],
                    EndTime = t,
                    PenDown = (
                        pointTypeValues[i > 0 ? i - 1 : 0] == 1
                    || (pointTypeValues[i > 0 ? i - 1 : 0] == 3 && pressureValues[i] > 0)
                    || (pointTypeValues[i > 0 ? i - 1 : 0] == 0 && pressureValues[i] > 0)
                    )
                }).Skip(1).ToList();
        }
    }
}
