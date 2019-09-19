using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    public struct PointFSection
    {
        public PointF Start { get; set; }

        public PointF End { get; set; }

        public PointFSection(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }

        public double Length
        {
            get
            {
                return Geometry.Euclidean(Start, End);
            }
        }

        public double Direction
        {
            get
            {
                double dy = End.Y - Start.Y;
                double dx = End.X - Start.X;
                return Math.Atan2(dy, dx);
            }
        }
    }
}
