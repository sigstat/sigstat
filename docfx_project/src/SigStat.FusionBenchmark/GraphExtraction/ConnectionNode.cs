using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public class ConnectionNode: HashSet<Vertex>
    {
        public int Degree(List<Stroke> strokes)
        {
            int res = 0;
            strokes.ForEach(stroke =>
            {
                if (this.Contains(stroke.Start)) { res++; }
            }
            );
            return res;
        }

        public List<Stroke> OutStrokes(List<Stroke> strokes)
        {
            List<Stroke> res = new List<Stroke>();
            strokes.ForEach(stroke =>
            {
                if (this.Contains(stroke.Start)) { res.Add(stroke); }
            }
            );
            return res;            
        }

        public List<Stroke> InStrokes(List<Stroke> strokes)
        {
            List<Stroke> res = new List<Stroke>();
            strokes.ForEach(stroke =>
            {
                if (this.Contains(stroke.End)) { res.Add(stroke); }
            }
            );
            return res;
        }

    }
}
