using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.ReSamplingFeatures.FeatureExtractAlgorithms
{
    class FuncOneAlgorithm : ICalculate
    {
        public List<double> Values { get; set; }

        public Func<double, double> Functor { get; set; }

        public FuncOneAlgorithm(List<double> values, Func<double, double> functor)
        {
            Values = values;
            Functor = functor;
        }

        public List<double> Calculate()
        {
            var res = new List<double>();
            foreach (var value in Values)
            {
                res.Add(Functor(value));
            }
            return res;
        }

        public static List<double> Calculate(List<double> values, Func<double, double> functor)
        {
            var res = new List<double>();
            foreach (var value in values)
            {
                res.Add(functor(value));
            }
            return res;
        }
    }
}
