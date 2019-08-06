using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    public static class Geometry
    {

        public static double Euclidean(Point lhs, Point rhs)
        {
            double dx = (double)(rhs.X - lhs.X);
            double dy = (double)(rhs.Y - lhs.Y);
            return Math.Sqrt(dy * dy + dx * dx);
        }

        public readonly static Point Origo = new Point(0, 0);

        public readonly static PointF OrigoF = new PointF(0.0F, 0.0F);


        public static double Euclidean(PointF lhs, PointF rhs)
        {
            double dx = (double)(rhs.X - lhs.X);
            double dy = (double)(rhs.Y - lhs.Y);
            return Math.Sqrt(dy * dy + dx * dx);
        }

        public static double DiffAngle(double angle1, double angle2)
        {
            double dy = Math.Sin(angle1) - Math.Sin(angle2);
            double dx = Math.Cos(angle1) - Math.Cos(angle2);
            double dist = Math.Sqrt(dy * dy + dx * dx);
            return Math.Asin(dist / 2) * 2;
        }

        public static double DirectedDiffAngle(double angle1, double angle2)
        {
            double res = DiffAngle(angle1, angle2);
            double px = Math.Cos(angle1);
            double py = Math.Sin(angle1);
            double qx = Math.Cos(angle2);
            double qy = Math.Sin(angle2);
            return (double)(Math.Sign(py * qx - px * qy)) * res;
        }

        public static double Direction(this PointSection section)
        {
            double dx = (double)(section.End.X - section.Start.X);
            double dy = (double)(section.End.Y - section.Start.Y);
            return Math.Atan2(dy, dx);
        }

        public static int Lefter(Point lhs, Point rhs)
        {
            if (lhs.X == rhs.X)
                return 0;
            return lhs.X < rhs.X ? -1 : 1;
        }

    }
}
