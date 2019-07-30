using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    class OffVertex
    {
        private static Vertex offVertex = new Vertex(-1, new Point(-1, -1), false);

        public static Vertex Get()
        {
            return offVertex;
        }
    }
}
