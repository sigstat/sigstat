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
            for (int i = 0; i < values.Count; i++)
            {
                res.Add(values[Math.Min(i + diffIdx, values.Count - 1)] - values[i]);
            }
            return res;
        }
    }
}
