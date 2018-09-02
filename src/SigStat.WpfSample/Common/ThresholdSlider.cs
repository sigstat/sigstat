using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Common
{
    public class ThresholdSlider
    {
        public double InitalThreshold { get; set; }
        public double StepValue { get; set; }
        public double NextThreshold { get; private set; }
        public int MaxNumberOfSteps { get; private set; }
        public double MaxThresholdValue { get; private set; }

        double numOfSteps = 0;

        public ThresholdSlider(double initThreshold, double stepValue, int maxNumOfSteps)
        {
            InitalThreshold = initThreshold;
            NextThreshold = initThreshold;
            StepValue = stepValue;
            MaxNumberOfSteps = maxNumOfSteps;
            MaxThresholdValue = initThreshold + maxNumOfSteps * stepValue;
        }

        public ThresholdSlider(double initThreshold, double stepValue, double maxThresholdValue)
        {
            InitalThreshold = initThreshold;
            NextThreshold = initThreshold;
            StepValue = stepValue;
            MaxThresholdValue = maxThresholdValue;
            MaxNumberOfSteps = (int)Math.Floor((maxThresholdValue - initThreshold) / stepValue);
        }

        public double GetNextThreshold()
        {
            if (numOfSteps >= MaxNumberOfSteps || NextThreshold >= MaxThresholdValue)
                throw new Exception("Out of the given range");

            double actualThreshold = NextThreshold;

            NextThreshold += StepValue;
            numOfSteps++;

            return actualThreshold;
        }
    }
}
