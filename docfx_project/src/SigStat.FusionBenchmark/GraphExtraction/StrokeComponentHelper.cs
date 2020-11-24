using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public static class StrokeComponentHelper
    {
        public static List<Stroke> GetAllStrokes(this List<StrokeComponent> components)
        {
            List<Stroke> res = new List<Stroke>();
            foreach (var comp in components)
            {
                res.AddRange(comp.Strokes);
            }
            return res;
        }
    }
}
