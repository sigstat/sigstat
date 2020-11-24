using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public class StrokeComponent
    {
        public List<Stroke> Strokes { get; private set; }

        public StrokeComponent(Stroke stroke)
        {
            Strokes = new List<Stroke>();
            Strokes.Add(stroke);
            Strokes.Add(Stroke.CreateSibling(stroke));
            InitializeStrokes();
        }

        public StrokeComponent(Stroke stroke1, Stroke stroke2)
        {
            Strokes = new List<Stroke>();
            Strokes.Add(stroke1);
            Strokes.Add(stroke2);
            InitializeStrokes();
        }

        private void InitializeStrokes()
        {
            foreach (var stroke in Strokes)
            {
                stroke.Component = this;
            }
        }

        public Stroke GetWithStart(Vertex start)
        {
            return Strokes.Find(stroke => stroke.Start == start || Vertex.AreNeighbours(start, stroke.Start));
        }

        public Stroke GetWithEnd(Vertex end)
        {
            return Strokes.Find(stroke => stroke.End == end || Vertex.AreNeighbours(end, stroke.End));
        }
    }
}
