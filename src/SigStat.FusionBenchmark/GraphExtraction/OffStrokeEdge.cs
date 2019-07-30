using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    class OffStrokeEdge: StrokeEdge
    {
        public OffStrokeEdge(Stroke s, Stroke t) : base(s, t)
        {
            Vertices = new List<Vertex>();
            Vertices.Add(OffVertex.Get());
        }
    }
}
