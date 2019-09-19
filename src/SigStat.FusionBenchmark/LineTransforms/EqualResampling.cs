using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.LineTransforms
{
    public static class EqualResampling
    {
        public static List<PointF> Calculate(List<PointF> list, int equalCnt)
        {
            var res = new List<PointF>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i % equalCnt == 0)
                { res.Add(list[i]); }
            }
            return res;
        }

    }
}
