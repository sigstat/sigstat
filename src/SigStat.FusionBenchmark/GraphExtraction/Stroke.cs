using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public class Stroke : List<Vertex>
    {
        public StrokeComponent Component { get; set; }

        public Stroke(): base() {}

        public Stroke(List<Vertex> list) : base(list) {}

        public Vertex Start
        {
            get
            {
                return this[0];
            }
        }

        public Vertex End
        {
            get
            {
                return this[this.Count - 1];
            }
        }

        public Stroke Sibling
        {
            get
            {
                return Component.GetWithStart(this.End);
            }
        }

        public static Stroke CreateSibling(Stroke stroke)
        {
            var res = new Stroke(stroke);
            res.Reverse();
            return res;
        }

        public override bool Equals(object obj)
        {
            Stroke rhs = obj as Stroke;
            if (rhs.Count != this.Count)
            {
                return false;
            }
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] != rhs[i])
                {
                    return false;
                }
            }
            return true;
        }

        
    }
}
