using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.ReSamplingFeatures.ReSamplingFuncs
{
    class EuclideanFunc
    {
        public static double Calculate(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
