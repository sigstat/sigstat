using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.ReSamplingFeatures.FeatureExtractAlgorithms
{
    class DirectionAlgorithm : ICalculate
    {
        public List<double> Xs { get; set; }
        
        public List<double> Ys { get; set; }

        public DirectionAlgorithm(List<double> xs, List<double> ys)
        {
            Xs = xs;
            Ys = ys;
        }

        public List<double> Calculate()
        {
            return Calculate(Xs, Ys);
        }

        public static List<double> Calculate(List<double> xs, List<double> ys)
        {
            var res = new List<double>();

            if (xs.Count != ys.Count)
            {
                throw new Exception();
            }

            for (int i = 0; i < xs.Count - 1; i++)
            {
                double dy = ys[i + 1] - ys[i];
                double dx = xs[i + 1] - xs[i];
                res.Add(Math.Atan2(dy, dx));
            }
            return res;
        }
    }
}
