using SigStat.FusionBenchmark.FusionMathHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.LineTransforms
{
    public static class LineFittingAlgorithm
    {
        public static List<PointF> Calculate(List<PointF> list)
        {
            StraightLineF lineF = new StraightLineF(list.First(), list.Last());
            double s = Geometry.Euclidean(list.First(), list.Last());
            double m = (double)lineF.M;
            double phi = Math.Atan(m);
            double phiMax = CalculatePhiMax(phi, s);
            double dTol = s * phiMax * 7.0;
            List<double> ds = CalculateDs(list);
            double dMax = ds.Max();
            var idxMax = ds.FindIndex(d => d == dMax);
            List<PointF> res = new List<PointF>();
            //Console.WriteLine("{0} {1}", dMax, dTol);
            if (dMax < dTol)
            {
                res.Add(list.First());
                res.Add(list.Last());
            }
            else
            {
                res.AddRange(Calculate(list.GetRange(0, idxMax + 1)));
                res.AddRange(Calculate(list.GetRange(idxMax, list.Count - idxMax)));
            }
            res = ClearRedundants(res);
            return res;
        }

        private static List<PointF> ClearRedundants(List<PointF> list)
        {
            var res = new List<PointF>();
            res.Add(list.First());
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].X != list[i - 1].X || list[i].Y != list[i - 1].Y)
                {
                    res.Add(list[i]);
                }
            }
            return res;
        }

        private static List<double> CalculateDs(List<PointF> list)
        {
            var res = new List<double>();
            foreach (var pointF in list)
            {
                double newVal = Math.Abs( (double)(pointF.X * (list.First().Y - list.Last().Y))
                                        + (double)(pointF.Y * (list.Last().X - list.First().X))
                                        + (double)(list.Last().Y * list.First().X) 
                                        - (double)(list.First().Y * list.Last().X));
                res.Add(newVal);
            }
            return res;
        }

        private static double CalculatePhiMax(double phi, double s)
        {
            double[] A = new double[]
                {
                    Math.Abs(Math.Sin(phi) + Math.Cos(phi)),
                    Math.Abs(Math.Sin(phi) - Math.Cos(phi))
                };
            double[] B = new double[]
                {
                    Math.Abs(Math.Sin(phi) + Math.Cos(phi)),
                    Math.Abs(Math.Sin(phi) - Math.Cos(phi)),
                    Math.Abs(-Math.Sin(phi) + Math.Cos(phi)),
                    Math.Abs(-Math.Sin(phi) - Math.Cos(phi))
                };

            double res = double.MinValue;
            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < B.Length; j++)
                {
                    double newVal = (A[i] * Math.Abs(s*s - s*B[j] + B[j]*B[j])) / (s*s*s);
                    res = Math.Max(res, newVal);
                }
            }
            return res;
        }
    }
}
