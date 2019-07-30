using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    class Stroke : IVertices
    {
        public List<Vertex> Vertices { get; set; }

        public List<StrokeEdge> Neighbours { get; set; } 

        public List<StrokeEdge> InDirectNeighbours { get; set; }

        public Vertex Start() { return Vertices[0]; }

        public Vertex End() { return Vertices[Vertices.Count - 1];  }

        public void Add(Vertex p) { Vertices.Add(p); }

        public StrokeComponent Component { get; set; }

        public int Degree() { return Neighbours.Count;  }

        public int ID()
        {
            return Component.ID;
        }

        public Stroke() {
            Component = null;
            Vertices = new List<Vertex>();
            Neighbours = new List<StrokeEdge>();
            InDirectNeighbours = new List<StrokeEdge>();
        }

        public Stroke(StrokeComponent component)
        {
            Component = component;
            Vertices = new List<Vertex>();
            Neighbours = new List<StrokeEdge>();
            InDirectNeighbours = new List<StrokeEdge>();
        }

        public Stroke Sibling()
        {
            if (Component == null)
                throw new Exception();
            return Component.GetWithStart(this.End());
        }

        public void AddStroke(Stroke stroke) {
            foreach (var p in stroke.Vertices)
            {
                this.Vertices.Add(p);
            }
            this.Neighbours = stroke.Neighbours;
            this.InDirectNeighbours = stroke.InDirectNeighbours;
        }

        public void AddStrokeEdge(StrokeEdge edge)
        {
            foreach (var p in edge.Vertices)
            {
                this.Vertices.Add(p);
            }
            this.AddStroke(edge.ToStroke);
        }

        public Stroke ReversedClone() {
            Stroke res = new Stroke(Component);
            for (int i = Vertices.Count - 1; i >= 0; i--) {
                res.Add(Vertices[i]);
            }
            return res;
        }
    }
}
