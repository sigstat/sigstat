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
using SigStat.Common.Algorithms;
using SigStat.FusionBenchmark.VertexTransFormations;

namespace SigStat.FusionBenchmark.TrajectoryReconsturction
{
    class DtwBasedTrajectoryRemake : PipelineBase, ILoggerObject
    {
        [Input]
        public FeatureDescriptor<StrokeCollection> InputStrokes { get; set; }

        [Input]
        public FeatureDescriptor<Stroke> InputNullStroke { get; set; }

        [Input]
        public FeatureDescriptor<VertexList> InputTrajectory { get; set; }

        [Output("Trajectory")]
        public FeatureDescriptor<VertexList> OutputTrajectory { get; set; }

        private bool[] IsInTrajectory { get; set; }
        private VertexList BaseTrajectory { get; set; }
        private VertexList Trajectory { get; set; }
        private StrokeCollection SigStrokes { get; set; }
        private int SigCnt { get; set; }
        private int BaseIdx { get; set; }

        public void Remake(Signature sig, Signature baseSig)
        {
            this.LogInformation("DtwBasedtrajectoryRemake - " + sig.ID + " based " + baseSig.ID + "started");
            SigStrokes = sig.GetFeature<StrokeCollection>(InputStrokes);
            BaseTrajectory = baseSig.GetFeature<VertexList>(InputTrajectory);
            Trajectory = new VertexList();
            IsInTrajectory = new bool[SigStrokes.Count];

            SigCnt = 0;
            BaseIdx = 0;
            int nextIdx = 0;

            Stroke actualStroke = sig.GetFeature<Stroke>(InputNullStroke);
            while (SigCnt < SigStrokes.Count && BaseIdx < BaseTrajectory.Count)
            {
                StrokeEdge nextEdge = FindNextEdge(actualStroke);
                actualStroke = nextEdge.ToStroke;
                Trajectory.Add(nextEdge);

                int newNextIdx = BaseIdx + FindMatching(nextEdge);
                BaseIdx = nextIdx;
                nextIdx = newNextIdx;
                if (!IsInTrajectory[actualStroke.ID()])
                {
                    IsInTrajectory[actualStroke.ID()] = true;
                    SigCnt++;
                }
                Console.WriteLine("**********");
                Console.WriteLine(actualStroke.ID());
                Console.WriteLine(actualStroke.Vertices.Count.ToString() + " " + (nextIdx - BaseIdx).ToString());
                Console.WriteLine("**********");
            }
            sig.SetFeature<VertexList>(OutputTrajectory, Trajectory);
            Console.WriteLine(SigCnt);
            this.LogInformation("DtwBasedtrajectoryRemake - finished");

        }

        private StrokeEdge FindNextEdge(Stroke actualStroke)
        {
            StrokeEdge minEdge = null;
            double minValue = Double.MaxValue;
            foreach (var edge in actualStroke.Neighbours)
            {
                //if (!IsInTrajectory[edge.ToStroke.ID()])
                //{
                    double val = ExamineEdge(edge);
                    if (val < minValue)
                    {
                        minValue = val;
                        minEdge = edge;
                    }
                //}
            }
            foreach (var edge in actualStroke.InDirectNeighbours)
            {
                //if (!IsInTrajectory[edge.ToStroke.ID()])
                //{
                    double val = ExamineEdge(edge) * 1.5;
                    if (val < minValue)
                    {
                        minValue = val;
                        minEdge = edge;
                    }
                //}
            }
            return minEdge;
        }

        private double ExamineEdge(StrokeEdge edge)
        {
            var queue1 = MakeQueueFromEdge(edge);
            double minIdxVal = Double.MaxValue;
            int minIdx = 0;
            for (int i = Math.Max(queue1.Count * 2 / 3, StrokeExtraction.minLength); i < queue1.Count * 3 /2; i += 3 * StrokeExtraction.minLength)
            {
                var queue2 = MakeQueueFromList(BaseTrajectory, BaseIdx, i);
                double val = DtwPy.Dtw(queue1.ToArray(), queue2.ToArray(), DtwPy.EuclideanDistance) / (queue1.Count + queue2.Count);
                if (val < minIdxVal)
                {
                    minIdxVal = val;
                    minIdx = i;
                }
            }
            Console.WriteLine(edge.ToStroke.ID().ToString() + " " + minIdxVal.ToString());
            return minIdxVal;
        }

        private Queue<double[]> MakeQueueFromEdge(StrokeEdge edge)
        {
            VertexList list = new VertexList();
            list.Add(edge);
            return MakeQueueFromList(list, 0, list.Count);
        }

        private Queue<double[]> MakeQueueFromList(VertexList list, int idx, int plusIdx)
        {
            Queue<double[]> res = new Queue<double[]>();
            for (int i = idx; (i < idx + plusIdx) && (i < list.Count); i++)
            {
                if (list[i].On)
                {
                    double[] val = new double[2];
                    val[0] = list[i].PosD.X;
                    val[1] = list[i].PosD.Y;
                    //val[2] = CalculateAngle(list, i) * 20.0;
                    //for (int relIdx = 0; relIdx < RelativePosition.areas; relIdx++)
                    //{
                    //    val[2 + relIdx] = list[i].RelPos[relIdx];
                    //}
                    res.Enqueue(val);
                }
            }
            return res;
        }

        private double CalculateAngle(VertexList list, int idx)
        {
            int endIdx;
            for (endIdx = idx; (endIdx - idx <= StrokeExtraction.minLength) && (endIdx < list.Count) && (list[endIdx].On); endIdx++)
                ;
            if (endIdx == list.Count || !list[endIdx].On)
            {
                endIdx--;
            }
            for (; (endIdx - idx <= StrokeExtraction.minLength) && (idx >= 0) && (list[idx].On); idx--)
                ;
            if (idx < 0 || !list[idx].On)
            {
                idx++;
            }
            double dy = list[endIdx].Pos.Y - list[idx].Pos.Y;
            double dx = list[endIdx].Pos.X - list[idx].Pos.X;
            return Math.Atan2(dy, dx) /  Math.PI;
        }

        private int FindMatching(StrokeEdge edge)
        {
            var queue1 = MakeQueueFromEdge(edge);
            double minIdxVal = Double.MaxValue;
            int minIdx = 0;
            for (int i = Math.Max(queue1.Count * 2 / 3, StrokeExtraction.minLength); i < queue1.Count * 3 / 2; i += StrokeExtraction.minLength)
            {
                var queue2 = MakeQueueFromList(BaseTrajectory, BaseIdx, i);
                double val = DtwPy.Dtw(queue1.ToArray(), queue2.ToArray(), DtwPy.EuclideanDistance) / (queue1.Count + queue2.Count);
                if (val < minIdxVal)
                {
                    minIdxVal = val;
                    minIdx = queue2.Count;
                }
            }
            return minIdx;
        }

    }
}
