using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class OnlineToOfflineFeature : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputT { get; set; }

        [Input]
        public FeatureDescriptor<List<bool>> InputButton { get; set; }

        [Input]
        public RectangleF InputGoalBounds { get; set; }

        [Output("Vertices")]
        public FeatureDescriptor<List<Vertex>> OutputVertices { get; set; }

        [Output("BaseTrajectory")]
        public FeatureDescriptor<List<Vertex>> OutputBaseTrajectory { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("OnlineToOfflineFeature - transform started");
            var xs = signature.GetFeature<List<double>>(InputX).ToList();
            var ys = signature.GetFeature<List<double>>(InputY).ToList();
            var bs = signature.GetFeature<List<bool>>(InputButton).ToList();
            var ts = signature.GetFeature<List<double>>(InputT).ToList();

            RectangleF presentBounds = FindBounds(xs, ys, bs);
            

            xs = NormalizeList(xs, (double)(-presentBounds.X), (double)(InputGoalBounds.Width / presentBounds.Width) );
            ys = NormalizeList(ys, (double)(-presentBounds.Y), (double)(InputGoalBounds.Height / presentBounds.Height));
            
            ShiftList(xs, (double)InputGoalBounds.X);
            ShiftList(ys, (double)InputGoalBounds.Y);
            var vertices = new List<Vertex>();
            int n = xs.Count;
            for (int i = 0; i < n - 1; i++)
            {
                if (bs[i])
                {
                    vertices.AddRange(MakeVertexLine(
                                                    new Vertex(new Point((int)xs[i], (int)ys[i]), bs[i]) { Rutovitz = 2 },
                                                    new Vertex(new Point((int)xs[i + 1], (int)ys[i + 1]), bs[i + 1]) { Rutovitz = 2 }
                                                )
                                );
                }
            }
            vertices.Add(new Vertex(new Point((int)xs[n-1], (int)ys[n-1]), bs[n-1]) { Rutovitz = 2 });
            signature.SetFeature<List<Vertex>>(OutputVertices, vertices);
            signature.SetFeature<List<Vertex>>(OutputBaseTrajectory, new List<Vertex>(vertices));
            this.LogInformation("{0} vertices made", vertices.Count);
            this.LogInformation("OnlineToOfflineFeature - transform finished");
        }

        private static List<double> NormalizeList(List<double> list, double shift,  double mulRate)
        {
            List<double> newList = new List<double>(list);
            ShiftList(newList, shift);
            MulRateList(newList, mulRate);
            return newList;
        }

        private static void ShiftList(List<double> list, double shift)
        {
            for (int i = 0; i < list.Count; i++) { list[i] += shift; }
        }

        private static void MulRateList(List<double> list, double mulRate)
        {
            for (int i = 0; i < list.Count; i++) { list[i] *= mulRate; }
        }

        private static RectangleF FindBounds(List<double> xs, List<double> ys, List<bool> bs)
        {
            if (xs.Count != ys.Count || ys.Count != bs.Count)
            {
                throw new IndexOutOfRangeException();
            }
            int n = xs.Count;
            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;
            for (int i = 0; i < n; i++)
            {
                if (bs[i])
                {
                    minX = Math.Min(xs[i], minX);
                    minY = Math.Min(ys[i], minY);
                    maxX = Math.Max(xs[i], maxX);
                    maxY = Math.Max(ys[i], maxY);
                }
            }
            float width = (float)(maxX - minX);
            float height = (float)(maxY - minY);
            return new RectangleF((float)minX, (float)minY, width, height); 
        }

        private static List<Vertex> MakeVertexLine(Vertex fromVertex, Vertex toVertex)
        {
            List<Vertex> res = new List<Vertex>();
            res.Add(fromVertex);
            if (toVertex == null || !toVertex.On)
            {
                return res;
            }
            double angle = Math.Atan2(toVertex.Pos.Y - fromVertex.Pos.Y, toVertex.Pos.X - fromVertex.Pos.X);
            PointF vector = new PointF((float)Math.Cos(angle), (float)Math.Sin(angle));
            PointF iterPos = new PointF((float)fromVertex.Pos.X, (float)fromVertex.Pos.Y);
            //Console.WriteLine("{0} {1} -> {2} {3}", fromVertex.Pos.X, fromVertex.Pos.Y, toVertex.Pos.X, toVertex.Pos.Y);
            while (!Vertex.AreNeighbours(toVertex, res.Last()) && !toVertex.Equals(res.Last()))
            {
                //Console.WriteLine("{0} - {1}",iterPos.X, iterPos.Y);
                iterPos.X += vector.X;
                iterPos.Y += vector.Y;
                var newVertex = new Vertex( new Point((int)iterPos.X,(int)iterPos.Y) ) { Rutovitz = 2 };
                if (!res.Last().Equals(newVertex)) 
                {
                    res.Add(newVertex);
                }
            }

            return res;
        }


    }
}
