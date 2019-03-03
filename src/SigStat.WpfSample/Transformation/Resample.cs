using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Transformation
{
    /// <summary>
    /// Transformation resampling the values along a defined timeslot lentgh to get uniform timeslots between values.
    /// </summary>
    public class Resample : PipelineBase, ITransformation
    {
        public int TimeSlot { get; set; }
        private readonly Func<int, List<double>, List<double>> doubleInterpolation;
        private readonly Func<int, List<int>, List<int>> integerInterpolation;

        /// <summary> Initializes a new instance of the <see cref="Map"/> class with specified settings. </summary>
        public Resample(int timeSlot, Func<int, List<double>, List<double>> interpolation)
        {
            TimeSlot = timeSlot;
            this.doubleInterpolation = interpolation;
        }

        /// <summary> Initializes a new instance of the <see cref="Map"/> class with specified settings. </summary>
        public Resample(int timeSlot, Func<int, List<int>, List<int>> interpolation)
        {
            TimeSlot = timeSlot;
            this.integerInterpolation = interpolation;
        }

        /// <inheritdoc/> 
        public void Transform(Signature signature)
        {
            //var outputValues = 
        }
    }
}
