using SigStat.FusionBenchmark.FusionMathHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.LineTransforms
{
    public static class PseudoVelocityAlgorithm
    {
        private readonly static double m = 1.0;

        private readonly static double dosM = 0.33333;

        public static List<PointF> Calculate(List<PointF> pointFs, List<double> dosList, double vMax, double deltaT = 1.0)
        {
            var res = new List<PointF>();
            res.Add(pointFs[0]);
            double sumT = 0.0; 
            for (int i = 1; i < pointFs.Count; i++)
            {
                if (sumT >= deltaT)
                {
                    res.Add(pointFs[i]);
                    sumT = 0.0;
                }
                double dos = dosList[i];
                double v = vMax * Math.Exp(Math.Abs(Math.Tan(dos)) * (-m));
                sumT += Geometry.Euclidean(pointFs[i-1], pointFs[i]) / v;
            }
            return res;
        }

    }
}
