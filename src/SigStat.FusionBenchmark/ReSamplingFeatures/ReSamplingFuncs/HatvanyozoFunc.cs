using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.ReSamplingFeatures.FeatureExtractAlgorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.ReSamplingFeatures.ReSamplingFuncs
{
    class HatvanyozoFunc
    {
        public double Power { get; set; }

        public HatvanyozoFunc(double power)
        {
            Power = power;
        }

        public double Calculate(double val)
        {
            return Math.Pow(val, Power);
        }

    }
}
