using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using SigStat.FusionBenchmark.GraphExtraction;

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

        public static double DiffVectorAngle(double[] aVec1, double[] aVec2)
        {
            if (aVec1.Length != aVec2.Length)
            {
                throw new Exception();
            }
            double res = 0.0;
            int n = aVec1.Length;
            for (int i = 0; i < n; i++)
            {
                res += DiffAngle(aVec1[i], aVec2[i]);
            }
            return res;
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

        public static double StartDirection(this Stroke stroke, int diffIdx)
        {
            PointSection sect = new PointSection(stroke.Start.Pos, stroke[Math.Min(stroke.Count - 1, diffIdx)].Pos);
            return sect.Direction();
        }

        public static double EndDirection(this Stroke stroke, int diffIdx)
        {
            PointSection sect = new PointSection(stroke[Math.Max(0, stroke.Count - 1 - diffIdx)].Pos,
                                                 stroke[stroke.Count - 1].Pos);
            return sect.Direction();
        }

        public static double DiffOfStrokes(Stroke fromStroke, Stroke toStroke)
        {
            PointSection fromSect = new PointSection(
                                            fromStroke[Math.Max(0, fromStroke.Count - FusionPipelines.scalingConst)].Pos,
                                            fromStroke[fromStroke.Count - 1].Pos
                                                    );
            double fromAngle = Direction(fromSect);
            PointSection toSect = new PointSection(
                                            toStroke[0].Pos,
                                            toStroke[Math.Min(toStroke.Count, FusionPipelines.scalingConst)].Pos
                                                    );
            double toAngle = Direction(toSect);
            return DiffAngle(fromAngle, toAngle);
        }

    }
}
