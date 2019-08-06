using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    public struct PointSection
    {
        public Point Start { get; set; }

        public Point End { get; set; }

        public PointSection(Point start, Point end)
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
    }
}
