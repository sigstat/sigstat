using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    public static class VectorFHelper
    {
        public static double Length(this VectorF v)
        {
            return Math.Sqrt((double)(v.X * v.X + v.Y * v.Y));
        }

        public static VectorF UnitVector(this VectorF v)
        {
            float length = (float)v.Length();
            return new VectorF(v.X / length, v.Y / length);
        }

        public static VectorF Rotate(this VectorF v, double phi)
        {
            float newX = (float)((double)v.X * Math.Cos(phi) - (double)v.Y * Math.Sin(phi));
            float newY = (float)((double)v.X * Math.Sin(phi) + (double)v.Y * Math.Cos(phi));
            return new VectorF(newX, newY);
        }

        public static VectorF Multiply(this VectorF v, float lambda)
        {
            return new VectorF(v.X * lambda, v.Y * lambda);
        }

        public static VectorF Multiply(this VectorF v, double lambda)
        {
            return v.Multiply((float)lambda);
        }
    }
}
