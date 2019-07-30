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

namespace SigStat.FusionBenchmark.GraphExtraction
{
    class StrokeEdgeExtraction : PipelineBase, ITransformation
    {

        [Input]
        public FeatureDescriptor<StrokeCollection> InputStrokes { get; set; }

        [Input]
        public FeatureDescriptor<VertexCollection> InputConnects { get; set; }

        [Output("Strokes")]
        public FeatureDescriptor<StrokeCollection> OutputStrokes { get; set; }

        [Output("StrokeEdgeList")]
        public FeatureDescriptor<List<StrokeEdge>> OutputStrokeEdgeList { get; set; }

        [Output("EndPoints")]
        public FeatureDescriptor<VertexCollection> OutputEndPoints { get; set; }

        [Output("CrossingPoints")]
        public FeatureDescriptor<VertexCollection> OutputCrossingPoints { get; set; }

        [Output("InDirectStrokeEdgeList")]
        public FeatureDescriptor<List<StrokeEdge>> OutputInDirectList { get; set; }

        [Output("NullStroke")]
        public FeatureDescriptor<Stroke> OutputNullStroke { get; set; }


        public void Transform(Signature signature)
        {
            this.LogInformation("StrokeEdgeExtraction -  transform started");
            var strokes = signature.GetFeature<StrokeCollection>(InputStrokes);
            var connects = signature.GetFeature<VertexCollection>(InputConnects);
            var endPoints = new VertexCollection();
            var crossingPoints = new VertexCollection();
            var strokeEdgeList = new List<StrokeEdge>();
            var inDirectList = new List<StrokeEdge>();
            TreeBuild(connects);
            foreach (var s in strokes.GetAllStrokes())
            {
                foreach (var t in strokes.GetAllStrokes())
                {
                    var edge = AreNeighbours(s, t);
                    if (edge != null)
                    {
                        strokeEdgeList.Add(edge);
                        s.Neighbours.Add(edge);
                    }
                }
            }
            foreach (var edge in strokeEdgeList)
            {
                MakeStrokeEdge(connects, edge);
            }
            foreach (var stroke in strokes.GetAllStrokes())
            {
                if (stroke.Degree() == 0)
                {
                    endPoints.Add(stroke.End());
                }
                else
                {   
                    crossingPoints.Add(stroke.End());            
                }
                var edge = new StrokeEdge(stroke, stroke.Sibling());
                edge.Vertices = new List<Vertex>();
                stroke.InDirectNeighbours.Add(edge);
                inDirectList.Add(edge);
            }
            foreach (var s in strokes.GetAllStrokes())
            {
                foreach (var t in strokes.GetAllStrokes())
                {
                    if (s.ID() != t.ID() &&
                        endPoints.Contains(s.End()) && endPoints.Contains(t.Start()))
                    {
                        var edge = new OffStrokeEdge(s, t);
                        s.InDirectNeighbours.Add(edge);
                        inDirectList.Add(edge);
                    }
                }
            }

            var nullStroke = new Stroke();
            nullStroke.Vertices.Add(OffVertex.Get());
            foreach (var stroke in strokes.GetAllStrokes())
            {
                if (endPoints.Contains(stroke.Start()))
                {
                    nullStroke.InDirectNeighbours.Add(new StrokeEdge(nullStroke, stroke));
                }
            }
            CheckEdges(strokeEdgeList);
            CheckEdges(inDirectList);
            this.LogInformation(strokeEdgeList.Count.ToString() + " strokeedges were found.");
            this.LogInformation(inDirectList.Count.ToString() + " \"indirect\" strokeedges were found.");
            this.LogInformation(endPoints.Count.ToString() + " endpoints were found.");
            this.LogInformation(crossingPoints.Count.ToString() + " crossingPoints were found.");

            signature.SetFeature<StrokeCollection>(OutputStrokes, strokes);
            signature.SetFeature<List<StrokeEdge>>(OutputStrokeEdgeList, strokeEdgeList);
            signature.SetFeature<List<StrokeEdge>>(OutputInDirectList, inDirectList);
            signature.SetFeature<VertexCollection>(OutputEndPoints, endPoints);
            signature.SetFeature<VertexCollection>(OutputCrossingPoints, crossingPoints);
            signature.SetFeature<Stroke>(OutputNullStroke, nullStroke);
            this.LogInformation("StrokeEdgeExtraction - transform finished");
        }

        static private void MakeStrokeEdge(VertexCollection connects, StrokeEdge strokeEdge)
        {
            if (strokeEdge.FromVertex.ID == strokeEdge.ToVertex.ID)
            {
                return;
            }

            foreach (var p in connects.Values)
            {
                p.Parent = null;
            }
            Queue<Vertex> bfsFifo = new Queue<Vertex>();
            bfsFifo.Enqueue(strokeEdge.FromVertex);
            strokeEdge.FromVertex.Parent = strokeEdge.FromVertex;
            while (bfsFifo.Count > 0)
            {
                Vertex p = bfsFifo.Dequeue();
                foreach (var q in p.Neighbours)
                {
                    if (q.Parent == null && connects.ContainsKey(q.ID))
                    {
                        q.Parent = p;
                        bfsFifo.Enqueue(q);
                        if (q.ID == strokeEdge.ToVertex.ID)
                        {
                            strokeEdge.Vertices = BackTrackList(strokeEdge.FromVertex, strokeEdge.ToVertex);
                            return;
                        }
                    }
                }
            }
            throw new Exception();
        }

        static private List<Vertex> BackTrackList(Vertex fromVertex, Vertex toVertex)
        {
            var list = new List<Vertex>();
            if (toVertex.ID == fromVertex.ID)
                return list;
            for (var p = toVertex; p.Parent.ID != fromVertex.ID; p = p.Parent)
                list.Add(p.Parent);
            return list;
        }

        public static void TreeBuild(VertexCollection connects)
        {
            foreach (var p in connects.Values)
            {
                p.Parent = p;
            }
            foreach (var p in connects.Values)
            {
                foreach (var q in p.Neighbours)
                {
                    if (connects.Contains(q))
                    {
                        var pAnc = FindAncestor(p);
                        var qAnc = FindAncestor(q);
                        if (pAnc.ID != qAnc.ID)
                        {
                            qAnc.Parent = pAnc;
                        }
                    }
                }
            }
        }

        static public Vertex FindAncestor(Vertex p)
        {
            if (p.ID == p.Parent.ID)
                return p;
            return (p.Parent = FindAncestor(p.Parent));
        }

        static private StrokeEdge AreNeighbours(Stroke s, Stroke t)
        {
            if (s.ID() != t.ID() &&
                FindAncestor(s.End()).ID == FindAncestor(t.Start()).ID)
            {
                return new StrokeEdge(s, t);   
            }
            return null;
        }

        static private void CheckEdges(List<StrokeEdge> edgeList)
        {
            foreach (var edge in edgeList)
            {
                if (edge.FromStroke.End().ID != edge.FromVertex.ID || edge.ToStroke.Start().ID != edge.ToVertex.ID)
                {
                    throw new Exception();
                }
            }
        }
    }
}
