using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    public class Section
    {
        public PointD Start { get; set; }

        public PointD End { get; set; }

        public Section(PointD start, PointD end)
        {
            Start = start;
            End = end;
        }

        public double Length()
        {
            return Math.Sqrt((End.X - Start.X) * (End.X - Start.X)
                             + (End.Y - Start.Y) * (End.Y - Start.Y));
        }
    }
}
