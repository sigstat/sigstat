using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    public static class PointFHelper
    {
        public static PointF ToPointF(this Point point)
        {
            return new PointF((float) point.X, (float) point.Y);
        }

        public static List<PointF> ToPointFList(this List<Point> list)
        {
            var res = new List<PointF>();
            foreach (var point in list)
            {
                res.Add(point.ToPointF());
            }
            return res;
        }

        public static double DistanceFrom(this PointF pointF, StraightLineF lineF)
        {
            return Math.Abs( (double) (lineF.A * pointF.X + lineF.B * pointF.Y + lineF.C)
                                    /Math.Sqrt((double)(lineF.A * lineF.A + lineF.B * lineF.B)));
        }
    }
}
