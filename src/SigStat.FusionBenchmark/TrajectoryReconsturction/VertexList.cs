using System;
using System.Collections.Generic;
using System.Text;
using SigStat.FusionBenchmark.GraphExtraction;

namespace SigStat.FusionBenchmark.TrajectoryReconsturction
{
    class VertexList: List<Vertex>
    {
        public List<StrokeEdge> orderOfEdges; 

        public VertexList() : base() { }

        private void Add(List<Vertex> list) {
            foreach (var p in list)
            {
                this.Add(p);
            }
        }
        private void Add(Stroke stroke)
        {
            this.Add(stroke.Vertices);
        }
        public void Add(StrokeEdge edge)
        {
            if (orderOfEdges == null)
            {
                orderOfEdges = new List<StrokeEdge>();
                this.Add(edge.FromStroke.Vertices);
            }
            this.Add(edge.Vertices);
            this.Add(edge.ToStroke.Vertices);
            this.orderOfEdges.Add(edge);
        }

    }
}
