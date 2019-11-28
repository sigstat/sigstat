using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionMathHelper;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.LineTransforms
{
    public static class DOSBasedAlgorithm 
    {

        public static List<double> Calculate(List<PointF> pointFs, int diffIdx, double length)
        {
            var res = new List<double>();
            for (int i = 0; i < pointFs.Count; i++)
            {
                res.Add(CalculateCurv(pointFs, i, diffIdx, length));
            }
            return res;
        }

        private static double CalculateCurv(List<PointF> pointFs, int idx, int diffIdx, double length)
        {
            var sects = MakeSections(pointFs, idx, diffIdx, length);
            return Math.PI - Geometry.DiffAngle(sects.Item1.Direction, sects.Item2.Direction);
        }

        private static Tuple<PointFSection, PointFSection> MakeSections(List<PointF> pointFs, int idx, int diffIdx, double length)
        {
            var sectForward = MakeSection(pointFs, idx, diffIdx, length);
            var sectBack = MakeSection(pointFs, idx, -diffIdx, length);
            return new Tuple<PointFSection, PointFSection>(sectForward, sectBack);
        }

        public static PointFSection MakeSection(List<PointF> pointFs, int idx, int idxPlus, double length)
        {
            if (idxPlus == 0)
            {
                throw new Exception();
            }
            double sum = 0.0;
            int i;
            for (i = idx + idxPlus; sum < length && pointFs.IsIdxValid(i); i += idxPlus)
            {
                sum += Geometry.Euclidean(pointFs[i], pointFs[i - idxPlus]);
            }
            if (!pointFs.IsIdxValid(i))
            {
                i -= idxPlus;
            }
            return new PointFSection(pointFs[idx], pointFs[i]);
        }
    }
}
