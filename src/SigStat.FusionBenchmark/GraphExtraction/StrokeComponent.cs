using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    class StrokeComponent : ObjectWithID
    {
        public int ID { get; set; }

        public Stroke A { get; set; }

        public Stroke B { get; set; }

        public StrokeComponent(int id)
        {
            ID = id;
        }

        public Stroke GetWithStart(Vertex start)
        {
            if (A.Start().ID == start.ID)
                return A;
            if (B.Start().ID == start.ID)
                return B;
            throw new Exception();
        }

        public Stroke GetWithEnd(Vertex end)
        {
            if (A.End().ID == end.ID)
                return A;
            if (B.End().ID == end.ID)
                return B;
            throw new Exception();
        }
    }
}
