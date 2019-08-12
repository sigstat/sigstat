using SigStat.FusionBenchmark.FusionFeatureExtraction;
using SigStat.FusionBenchmark.FusionMathHelper;
using System;
using System.Collections.Generic;
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

        public static List<double[]> GetXY(this List<Vertex> list)
        {
            List<double[]> res = new List<double[]>();
            foreach (var p in list)
            {
                double[] xy = new double[2];
                xy[0] = (double)p.PosF.X;
                xy[1] = (double)p.PosF.Y;
                res.Add(xy);
            }
            return res;
        }

        public static List<double[]> GetRelXY(this List<Vertex> list)
        {
            List<double[]> res = new List<double[]>();
            var start = list[0].PosF;
            foreach (var p in list)
            {
                double[] xy = new double[2];
                xy[0] = (double)(p.PosF.X - start.X);
                xy[1] = (double)(p.PosF.Y - start.Y);
                res.Add(xy);
            }
            return res;
        }

        public static List<double> GetDirections(this List<Vertex> list)
        {
            List<double> res = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                double newVal = DOSBasedExtract.MakeSection(list, i).Direction();
                res.Add(newVal);
            }
            return res;
        }

        public static List<double[]> GetDirectionsFeature(this List<Vertex> list)
        {
            List<double[]> res = new List<double[]>();

            List<double> direction0 = list.GetDirections();
            List<double> direction1 = direction0.Differentiate();
            for (int i = 0; i < list.Count; i++)
            {
                double[] newVal = new double[2];
                newVal[0] = direction0[i];
                newVal[1] = direction1[i];
                res.Add(newVal);
            }
            return res;
        }
    }
}
