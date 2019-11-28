using SigStat.FusionBenchmark.FusionFeatureExtraction;
using SigStat.FusionBenchmark.FusionMathHelper;
using SigStat.FusionBenchmark.LineTransforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public static class VerticesHelper
    {
        public static List<Vertex> EndPoints(this List<Vertex> vertices)
        {
            return vertices.FindAll(p => p.PointType == VertexType.Endpoint);
        }

        public static List<Vertex> CrossingPoints(this List<Vertex> vertices)
        {
            return vertices.FindAll(p => p.PointType == VertexType.CrossingPoint);
        }

        public static List<Vertex> ConnectionPoints(this List<Vertex> vertices)
        {
            return vertices.FindAll(p => p.PointType == VertexType.ConnectionPoint);
        }

        public static List<Vertex> StrokeEnds(this List<Vertex> vertices)
        {
            List<Vertex> res = new List<Vertex>();
            res.AddRange(vertices.EndPoints());
            res.AddRange(vertices.CrossingPoints());
            return res;
        }

        public static List<double> GetXs(this List<Vertex> list)
        {
            List<double> res = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                res.Add((double)list[i].PosF.X);
            }
            return res;
        }

        public static List<double> GetYs(this List<Vertex> list)
        {
            List<double> res = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                res.Add((double)list[i].PosF.Y);
            }
            return res;
        }

        private static List<double> GetOriginalXs(this List<Vertex> list)
        {
            List<double> res = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                res.Add((double)list[i].Pos.X);
            }
            return res;
        }

        private static List<double> GetOriginalYs(this List<Vertex> list)
        {
            List<double> res = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                res.Add((double)list[i].Pos.Y);
            }
            return res;
        }

        public static List<double[]> GetOriginalXYs(this List<Vertex> list)
        {
            var xs = list.GetOriginalXs();
            var ys = list.GetOriginalYs();
            return Translations.MergeLists(new List<double>[] { xs, ys });
        }

        public static List<double> GetDirections(this List<Vertex> list)
        {
            var pointFs = new List<PointF>();
            list.ForEach(vertex => pointFs.Add(vertex.Pos.ToPointF()));
            List<double> res = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                double newVal = DOSBasedAlgorithm.MakeSection(pointFs, i, 1, FusionPipelines.DOSConst).Direction;
                res.Add(newVal);
            }
            return res;
        }


        public static List<double[]> GetDtwPairingFeature(this List<Vertex> list, int inputScale = 1)
        {
            var xs = new List<double>();
            var ys = new List<double>();
            for (int i = 0, cnt = 0; i < list.Count; i++)
            {
                if (ScalePredicate(list, i, cnt, inputScale))
                {
                    xs.Add(list[i].PosF.X);
                    ys.Add(list[i].PosF.Y);
                    cnt = 0;
                }
                else
                {
                    cnt++;
                }
            }
            int n = xs.Count;
            if (n != xs.Count || n != ys.Count)
            {
                throw new Exception();
            }
            var directions = Translations.GetDirections(xs, ys);
            return Translations.MergeLists(new List<double>[]
                                                {
                                                    xs,
                                                    ys,
                                                    directions
                                                }
            );
            
        }

        private static bool ScalePredicate(List<Vertex> trajectory, int idx, int cnt, int inputScale)
        {
            return (cnt >= inputScale) ||
                    (idx == 0 || idx == trajectory.Count - 1) ||
                    (idx > 0 && !Vertex.AreNeighbours(trajectory[idx - 1], trajectory[idx]) &&
                                                    !trajectory[idx - 1].Equals(trajectory[idx])) ||
                    (idx < trajectory.Count - 1 && !Vertex.AreNeighbours(trajectory[idx], trajectory[idx + 1]) &&
                                                !trajectory[idx].Equals(trajectory[idx + 1]));
        }

    }
}
