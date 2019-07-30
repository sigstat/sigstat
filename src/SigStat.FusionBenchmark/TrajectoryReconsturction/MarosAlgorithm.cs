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
    class MarosAlgorithm : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<StrokeCollection> InputStrokes { get; set; }

        [Input]
        public FeatureDescriptor<VertexCollection> InputEnds { get; set; }

        [Input]
        public FeatureDescriptor<Stroke> InputNullStroke { get; set; }

        [Output("Trajectory")]
        public FeatureDescriptor<VertexList> OutputTrajectory { get; set; }

        
        public void Transform(Signature signature)
        {
       
            this.LogInformation("MarosAlgorithm - transform started.");
            var strokes = signature.GetFeature<StrokeCollection>(InputStrokes);
            bool[] isInTrajectory = new bool[strokes.Count];
            for (int i = 0; i < isInTrajectory.Length; i++) isInTrajectory[i] = false;
            var trajectory = new VertexList();
            Stroke actualStroke = signature.GetFeature<Stroke>(InputNullStroke);
            for (int cnt = 0; cnt < strokes.Count; cnt++)
            {
                StrokeEdge nextStrokeEdge = FindNextStrokeEdge(actualStroke, strokes, isInTrajectory);
                trajectory.Add(nextStrokeEdge);
                actualStroke = nextStrokeEdge.ToStroke;
                isInTrajectory[actualStroke.Component.ID] = true;
            }
            signature.SetFeature<VertexList>(OutputTrajectory, trajectory);
            this.LogInformation("Result of process: " + trajectory.Count.ToString() + " vertices in trajectory.");
            this.LogInformation("MarosAlgorithm - transform finished");
        }

        private StrokeEdge FindNextStrokeEdge(Stroke actualStroke, StrokeCollection strokes, bool[] isInTrajectory)
        {
            StrokeEdge nextStrokeEdge = null;
            double minAngle = Double.MaxValue;
            foreach (var edge in actualStroke.Neighbours)
            {
                if (!isInTrajectory[edge.ToStroke.Component.ID])
                {
                    double angle = CalculateAngle(edge);
                    if (Math.Abs(angle) < minAngle)
                    {
                        minAngle = Math.Abs(angle);
                        nextStrokeEdge = edge;
                    }
                }
            }
            if (nextStrokeEdge != null)
                return nextStrokeEdge;
            var comparer = new LeftComparer();
            foreach (var edge in actualStroke.InDirectNeighbours)
            {
                if (!isInTrajectory[edge.ToStroke.ID()] &&
                    (nextStrokeEdge == null || comparer.Compare(edge.ToVertex, nextStrokeEdge.ToVertex) == 1))
                {
                    nextStrokeEdge = edge;
                }
            }
            if (nextStrokeEdge != null)
                return nextStrokeEdge;
            foreach (var stroke in strokes.GetAllStrokes())
            {
                if (!isInTrajectory[stroke.ID()])
                {
                    return new OffStrokeEdge(actualStroke, stroke);
                }
            }
            if (nextStrokeEdge == null)
                throw new Exception();
            return nextStrokeEdge;
        }

        private double CalculateAngle(StrokeEdge edge)
        {
            double res = 0.0;
            Tuple<Section,Section> sections = FindSections(edge.FromStroke, edge.ToStroke);
            res = DOSBasedFeature.CalculateDiffAngle(sections.Item1, sections.Item2);
            return res;
        }

        private Tuple<Section, Section> FindSections(Stroke firstStroke, Stroke secondStroke)
        {
            const double w = 10.0;
            var start = firstStroke.End();
            var end = firstStroke.End();
            for (int i = firstStroke.Vertices.Count - 1; 
                 (i >= 0) && Euclidean(start.Pos, end.Pos) < w; i--)
            {
                start = firstStroke.Vertices[i];
            }
            Section firstSection = new Section(new PointD(start.Pos.X, start.Pos.Y),
                                              new PointD(end.Pos.X, end.Pos.Y));
            start = secondStroke.Start();
            end = secondStroke.Start();
            for (int i = 0; (i < secondStroke.Vertices.Count) && (Euclidean(start.Pos, end.Pos) < w);
                     i++)
            {
                end = secondStroke.Vertices[i];
            }
            Section secondSection = new Section(new PointD(start.Pos.X, start.Pos.Y),
                                              new PointD(end.Pos.X, end.Pos.Y));

            return new Tuple<Section, Section>(firstSection, secondSection);
        }

        private double Euclidean(Point first, Point second)
        {
            double dy = first.Y - second.Y;
            double dx = first.X - second.X;
            return Math.Sqrt(dy * dy + dx * dx);
        }

        /*private Stroke FindFirstStroke(Signature signature, StrokeCollection strokes)
        {
            var endPoints = signature.GetFeature<VertexCollection>(InputEnds);
            Vertex leftest = null;
            var comparer = new LeftComparer();
            foreach (var p in endPoints.Values)
            {
                if (leftest == null || comparer.Compare(p, leftest) == 1)
                {
                    leftest = p;
                }
            }

            foreach (var stroke in strokes.GetAllStrokes())
            {
                if (stroke.Start().ID == leftest.ID)
                {
                    return stroke;
                }
            }
            throw new Exception();
        }*/


        struct LeftComparer : IComparer<Vertex>
        {
            public int Compare(Vertex lhs, Vertex rhs)
            {
                return (lhs.Pos.X < rhs.Pos.X) || (lhs.Pos.X == rhs.Pos.X && lhs.Pos.Y < rhs.Pos.Y) ? 1 : 0;
            }
        }

    }
}
