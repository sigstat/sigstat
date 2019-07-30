using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    class NormalDistribution
    { 
        public double Phi(double x, double sigma, double mean = 0.0)
        {
            x = (x - mean) / sigma;
            return StandardNormalDistribution.Phi(x);
        }
    }
}
