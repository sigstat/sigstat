using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public static class ListHelper
    { 
        public static bool IsIdxValid<T>(this List<T> list, int idx)
        {
            return 0 <= idx && idx < list.Count;
        }
    }
}
