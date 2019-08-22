using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    class DistFunc
    {
        List<Func<double, double, double>> Functions { get; set; }

        public DistFunc()
        {
            Functions = null;
        }

        public DistFunc(List<Func<double, double, double>> functions)
        {
            Functions = functions;
        }

        public double Functor (double[] vec1, double[] vec2)
        {
            if (vec1.Length != vec2.Length || Functions.Count != vec1.Length)
            {
                throw new ArgumentException();
            }
            int n = vec1.Length;
            double res = 0.0;
            for (int i = 0; i < n; i++)
            {
                res += Functions[i](vec1[i], vec2[i]);
            }
            return res;
        }
    }
}
