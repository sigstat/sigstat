using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    class StandardNormalDistribution
    {
        private static double[] a = new double[]
                                    {
                                        1.0,
                                        0.196854,
                                        0.115194,
                                        0.000344,
                                        0.019527
                                    }; 

        public static double Phi(double x)
        {
            
            double polinomRes = 0.0;
            for (int i = a.Length - 1; i >= 0; i--)
            {
                polinomRes = polinomRes * Math.Abs(x) + a[i];
            }
            double res = 1 - 0.5 * (1 / Math.Pow( polinomRes, 4));
            if (x < 0.0)
            {
                res = 1 - res;
            }
            return res;
        }
        
    }
}
