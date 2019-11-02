using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.ReSamplingFeatures.FeatureExtractAlgorithms
{
    public static class JustOnAlgorithm
    {
        public static List<List<double>> Calculate(List<List<double>> lists, List<bool> bs)
        {
            var resLists = new List<List<double>>();
            lists.ForEach(list => resLists.Add(JustOnAlgorithm.Calculate(list, bs)));
            return resLists;
        }

        public static List<double> Calculate(List<double> list, List<bool> bs)
        {
            if (list.Count != bs.Count - 1)
            {
                throw new Exception();
            }
            var res = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                if (bs[i] && bs[i + 1])
                {
                    res.Add(list[i]);
                }
            }
            return res;
        }
    }
}
