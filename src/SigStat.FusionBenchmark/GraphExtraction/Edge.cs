using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    class Edge
    {
        public Vertex FromVertex { get; set; }

        public Vertex ToVertex { get; set; }

        public Edge(Vertex fromVertex, Vertex toVertex)
        {
            FromVertex = fromVertex;
            ToVertex = toVertex;
        }
    }
}
