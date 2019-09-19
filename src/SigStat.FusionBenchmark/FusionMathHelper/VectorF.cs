using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    public struct VectorF
    {
        public float X { get; set; }

        public float Y { get; set; }

        public VectorF(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public VectorF(int x, int y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public VectorF(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public VectorF(PointF from, PointF to)
        {
            X = to.X - from.X;
            Y = to.Y - from.Y;
        }
    }
}
