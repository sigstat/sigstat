using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.FusionBenchmark.ReSamplingFeatures.FeatureExtractAlgorithms
{
    class DerivatorAlgorithm:  ICalculate
    {
        
        public List<double> Values { get; set; }

        public int DiffIdx { get; set; }

        public Func<double, double, double> Functor { get; set; }

        public DerivatorAlgorithm(List<double> values, int diffIdx, Func<double, double, double> functor)
        {
            Values = values;
            DiffIdx = diffIdx;
            Functor = functor;
        }

        public List<double> Calculate()
        {
            return Calculate(Values, DiffIdx, Functor);
        }

        public static List<double> Calculate(List<double> values, int diffIdx, Func<double, double, double> functor)
        {
            var res = new double[values.Count];
            if (diffIdx == 0)
            {
                throw new Exception();
            }
            int fromIdx = Math.Max(-diffIdx, 0);
            int toIdx = Math.Min(values.Count - diffIdx, values.Count);
            for (int i = fromIdx; i < toIdx; i++)
            {
                res[i] = functor(values[Math.Max(i + diffIdx, i)], values[Math.Min(i + diffIdx, i)]);
            }

            for (int i = 0; i < fromIdx; i++)
            {
                res[i] = res[fromIdx] * (fromIdx - i);
            }
            for (int i = toIdx; i < values.Count; i++)
            {
                res[i] = res[toIdx] * (values.Count - toIdx);
            }
            
            return res.ToList();
        }
    }
}
