using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    class StrokeEdge : Edge, IVertices
    {
        public Stroke FromStroke { get; set; }

        public Stroke ToStroke { get; set;}

        public List<Vertex> Vertices { get; set; }

        public StrokeEdge(Stroke toStroke) : base(null, toStroke.Start())
        {
            FromStroke = null;
            ToStroke = toStroke;
            Vertices = new List<Vertex>();
        }

        public StrokeEdge(Stroke fromStroke, Stroke toStroke) : base(fromStroke.End(), toStroke.Start())
        {
            FromStroke = fromStroke;
            ToStroke = toStroke;
            Vertices = new List<Vertex>();
        }
    }
}
