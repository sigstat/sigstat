using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.ReSamplingFeatures.FeatureExtractAlgorithms
{
    class FuncTwoAlgorithm : ICalculate
    {
        public List<List<double>> ValLists { get; set; }

        public Func<double, double, double> Functor { get; set; }

        public FuncTwoAlgorithm(List<List<double>> valLists, Func<double, double, double> functor)
        {
            ValLists = valLists;
            Functor = functor;
        }

        public FuncTwoAlgorithm(List<double> list1, List<double> list2, Func<double, double, double> functor)
        {
            ValLists = new List<List<double>>() { list1, list2 };
            Functor = functor;
        }

        public List<double> Calculate()
        {
            return Calculate(ValLists, Functor);
        }

        public static List<double> Calculate(List<List<double>> valLists, Func<double, double, double> functor)
        {
            List<double> res = new List<double>();
            if (valLists.Count != 2)
            {
                throw new Exception();
            }
            int n = valLists[0].Count;
            foreach (var list in valLists)
            {
                if (list.Count != n)
                {
                    throw new Exception();
                }
            }

            for (int i = 0; i < n; i++)
            {
                res.Add(functor(valLists[0][i], valLists[1][i]));
            }
            return res;
        }
    }
}
