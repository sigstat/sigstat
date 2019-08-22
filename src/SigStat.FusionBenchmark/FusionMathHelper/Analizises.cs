using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    public static class Analizises
    {
        public static List<double> Differentiate(this List<double> values, int diffIdx = 1)
        {
            List<double> res = new List<double>();
            int n = values.Count;
            for (int i = 0; i < n - diffIdx; i++)
            {
                res.Add(values[i + diffIdx] - values[i]);
            }
            for (int i = n - diffIdx; i < n; i++)
            {
                res.Add(values[n - 1] - values[i]);
            }
            if (values.Count != res.Count)
            {
                throw new Exception();
            }
            return res;
        }
    }
}
