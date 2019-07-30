using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using Newtonsoft.Json;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.FusionBenchmark.FusionFeatureExtraction;

namespace SigStat.FusionBenchmark.TrajectoryReconsturction
{
    class TrajectoryRemake : PipelineBase, ILoggerObject
    {
        [Input]
        public FeatureDescriptor<StrokeCollection> InputStrokes { get; set; }

        [Input]
        public FeatureDescriptor<VertexList> InputTrajectory { get; set; }

        [Output("Trajectory")]
        public FeatureDescriptor<VertexList> OutputTrajectory { get; set; }

        private static int plusPoints(int length)
        {
            return Math.Min(length / 3, 15);
        }

        public void Remake(Signature sig, Signature baseSig)
        {
            this.LogInformation(sig.ID + " based " + baseSig.ID + "started");
            var sigStrokes = sig.GetFeature<StrokeCollection>(InputStrokes);
            var baseTrajectory = baseSig.GetFeature<VertexList>(InputTrajectory);
            var trajectory = new VertexList();
            bool[] isInTrajectory = new bool[sigStrokes.Count];
            isInTrajectory.Initialize();
            Queue<double[]> sigQueue = new Queue<double[]>();
            Queue<double[]> baseSigQueue = new Queue<double[]>();
            int idx = 0;
            int cnt = 0;
            Stroke actualStroke = null;
            while (cnt < sigStrokes.Count && idx < baseTrajectory.Count)
            {

                if (actualStroke == null)
                {
                    double minValue = Double.MaxValue;
                    Stroke minStroke = null;
                    foreach (var stroke in sigStrokes.GetAllStrokes())
                    {
                        var sequence1 = new Queue<double[]>(sigQueue);
                        var sequence2 = new Queue<double[]>(baseSigQueue);
                        AddListToQueue(sequence1, PointsToList(stroke.Vertices, 0, 0 + LengthOfAdd(stroke)));
                        AddListToQueue(sequence2, PointsToList(baseTrajectory, idx, idx + LengthOfAdd(stroke)));
                        double strokeVal = TrajDtw.Dtw(sequence1, sequence2, TrajDtw.Optimal());
                        if (strokeVal < minValue)
                        {
                            minStroke = stroke;
                            minValue = strokeVal;
                        }
                    }
                    AddListToQueue(sigQueue, minStroke.Vertices);
                    AddListToQueue(baseSigQueue, PointsToList(baseTrajectory, idx, idx + LengthOfAdd(minStroke)));
                    idx += LengthOfAdd(minStroke);
                    TrajDtw.Dtw(sigQueue, baseSigQueue, TrajDtw.Optimal());
                    actualStroke = minStroke;

                }
                else
                {
                    StrokeEdge minEdge = null;
                    double minValue = Double.MaxValue;
                    foreach (var edge in actualStroke.Neighbours)
                    {
                        var sequence1 = new Queue<double[]>(sigQueue);
                        var sequence2 = new Queue<double[]>(baseSigQueue);
                        //AddEdgeToQueue(sequence1, edge);
                        AddListToQueue(sequence1, PointsToList(edge.ToStroke.Vertices, 0, LengthOfAdd(edge)));
                        AddListToQueue(sequence2, PointsToList(baseTrajectory, idx, idx + LengthOfAdd(edge)));
                        double edgeVal = TrajDtw.Dtw(sequence1, sequence2, TrajDtw.Optimal());
                        if (edgeVal < minValue)
                        {
                            minEdge = edge;
                            minValue = edgeVal;
                        }
                    }
                    foreach (var edge in actualStroke.InDirectNeighbours)
                    {
                        var sequence1 = new Queue<double[]>(sigQueue);
                        var sequence2 = new Queue<double[]>(baseSigQueue);
                        //AddEdgeToQueue(sequence1, edge);
                        AddListToQueue(sequence1, PointsToList(edge.ToStroke.Vertices, 0, LengthOfAdd(edge)));
                        AddListToQueue(sequence2, PointsToList(baseTrajectory, idx, idx + LengthOfAdd(edge)));
                        double edgeVal = TrajDtw.Dtw(sequence1, sequence2, TrajDtw.Optimal());
                        if (edgeVal < minValue)
                        {
                            minEdge = edge;
                            minValue = edgeVal;
                        }
                    }
                    AddEdgeToQueue(sigQueue, minEdge);
                    AddListToQueue(baseSigQueue, PointsToList(baseTrajectory, idx, idx + LengthOfAdd(minEdge)));
                    idx += LengthOfAdd(minEdge);
                    TrajDtw.Dtw(sigQueue, baseSigQueue, TrajDtw.Optimal());
                    trajectory.Add(minEdge);
                    actualStroke = minEdge.ToStroke;

                }
                
                while (baseSigQueue.Count == 0 && idx < baseTrajectory.Count && sigQueue.Count > 0)
                {
                    AddListToQueue(baseSigQueue, PointsToList(baseTrajectory, idx, idx + plusPoints(sigQueue.Count)));
                    idx += plusPoints(sigQueue.Count);
                    TrajDtw.Dtw(sigQueue, baseSigQueue, TrajDtw.Optimal());
                }
                cnt++;
            }
            sig.SetFeature<VertexList>(OutputTrajectory, trajectory);

        }

        private static void AddEdgeToQueue(Queue<double[]> queue, StrokeEdge edge)
        {
            AddPointsToQueue(queue, edge.Vertices);
            AddPointsToQueue(queue, edge.ToStroke.Vertices);
        }

        private static void AddListToQueue(Queue<double[]> queue, List<Vertex> vertices)
        {
            AddPointsToQueue(queue, vertices);
        }

        private static void AddPointsToQueue(Queue<double[]> queue, List<Vertex> vertices)
        {
            double lastAngle = 0.0;
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].On)
                {
                    double[] val = new double[3];
                    val[0] = vertices[i].PosD.X;
                    val[1] = vertices[i].PosD.Y;
                    val[2] = (i + 5) < vertices.Count ? CalculateNormalizedAngle(vertices[i + 5], vertices[i]) : lastAngle;
                    lastAngle = val[2];
                    queue.Enqueue(val);
                }
            }
        }

        private static List<Vertex> PointsToList(List<Vertex> origList, int fromIdx, int toIdx)
        {
            var res = new List<Vertex>();
            toIdx = Math.Min(origList.Count, toIdx);
            for (int i = fromIdx; i < toIdx; i++)
            {
                res.Add(origList[i]);
            }
            return res;
        }

        private static double CalculateNormalizedAngle(Vertex p, Vertex q)
        {
            double dy = p.Pos.Y - q.Pos.Y;
            double dx = p.Pos.X - q.Pos.X;
            return Math.Atan2(dy, dx) / (5 * Math.PI);
        }

        private static int LengthOfAdd(StrokeEdge edge)
        {
            return (edge.Vertices.Count + edge.ToStroke.Vertices.Count) * 2 / 3;
        }

        private static int LengthOfAdd(Stroke stroke)
        {
            return (stroke.Vertices.Count) * 2 / 3;
        }
    }
}
