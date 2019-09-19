using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Text;


using System.Linq;
using SigStat.FusionBenchmark.FusionMathHelper;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.TrajectoryRecovery
{
    [JsonObject(MemberSerialization.OptOut)]
    class DtwPairing : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<StrokeComponent>> InputComponents { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputBaseTrajectory { get; set; }

        [Input]
        public int InputJump { get; set; }

        [Input]
        public double InputScaleRate { get; set; }

        [Input]
        public int InputWindowJump { get; set; }

        [Input]
        public int InputWindowFrom { get; set; }

        [Input]
        public int InputWindowTo { get; set; }

        [Input]
        public bool InputIsParallel { get; set; }

        [Output("Trajectory")]
        public FeatureDescriptor<List<Vertex>> OutputTrajectory { get; set; }

        [Output("StrokeMatches")]
        public FeatureDescriptor<List<Tuple<int, Stroke, double, int>>> OutputStrokeMatches { get; set; }
        
        private object o = new object();

        public void Transform(Signature signature)
        {
            this.LogInformation("DtwPairing - DtwCompare started");
            List<Stroke> strokes = signature.GetFeature<List<StrokeComponent>>(InputComponents).GetAllStrokes();
            var baseTrajectory = signature.GetFeature<List<Vertex>>(InputBaseTrajectory);

            List<Tuple<int, Stroke, double, int>> order = new List<Tuple<int, Stroke, double, int>>();

            if (InputIsParallel)
            {
                Parallel.ForEach(strokes, stroke =>
                {
                    var pairing = CalculatePairing(stroke, baseTrajectory);
                    lock (o)
                    {
                        order.Add(pairing);
                    }
                }
                );
            }
            else
            {
                strokes.ForEach(stroke =>
                {
                    var pairing = CalculatePairing(stroke, baseTrajectory);
                    order.Add(pairing);
                });
            }

            signature.SetFeature<List<Tuple<int, Stroke, double, int>>>(OutputStrokeMatches, order);
            order.Sort( (Tuple<int, Stroke, double, int> p, Tuple<int, Stroke, double,int> q) =>
                              { return p.Item1 + p.Item4 / 2 < q.Item1 + q.Item4 / 2 ? -1 : 1; } );
            List<Stroke> strokeOrder = SelectStrokes(order);
            var trajectory = new List<Vertex>();
            strokeOrder.ForEach(stroke => trajectory.AddRange(stroke));
            signature.SetFeature<List<Vertex>>(OutputTrajectory, trajectory);
            this.LogInformation("DtwPairing - DtwCompare finished");
        }

        private List<Stroke> SelectStrokes(List<Tuple<int, Stroke, double, int>> order)
        {
            List<Stroke> res = new List<Stroke>();
            foreach (var tuple in order)
            {
                var sibling = order.Find(find => find.Item2 == tuple.Item2.Sibling);
                if (tuple.Item3 < sibling.Item3) { res.Add(tuple.Item2); }
            }
            return res; 
        }


        private Tuple<int, Stroke, double, int> CalculatePairing(Stroke stroke, List<Vertex> trajectory)
        {
            Tuple<int, Stroke, double, int> res = null;
            for (int percent = InputWindowFrom; percent <= InputWindowTo; percent+= InputWindowJump)
            {
                var val = CalculateIdx(stroke, trajectory, CalcWindow(stroke, percent), InputJump);
                if (res == null || val.Item3 < res.Item3)
                {
                    res = val;
                }
            }
            return res;
        }

        private static int CalcWindow(Stroke stroke, int percent)
        {
            return Math.Max(stroke.Count * percent / 100, 1);
        }

        private Tuple<int, Stroke, double, int> CalculateIdx(Stroke stroke, List<Vertex> trajectory, int window, int jump = 1)
        {
            int resIdx = int.MaxValue;
            double resVal = Double.MaxValue;
            int inputScale = Math.Max((int)(trajectory.Count * InputScaleRate), 1);
            for (int i = 0; i < trajectory.Count - window; i += jump)
            {
                /*
                double valPosition = DtwPy.Dtw<double[]>(stroke.GetXY(), trajectory.GetRange(i, window).GetXY(),
                                                 DtwPy.EuclideanDistance);
                double valShape = DtwPy.Dtw<double[]>(stroke.GetDirectionsFeature(), trajectory.GetRange(i, window).GetDirectionsFeature(),
                                                Geometry.DiffVectorAngle);
                double val = CalculateMixedVal(valPosition, valShape);
                */
                double val = DtwPy.Dtw<double[]>(stroke.GetDtwPairingFeature(inputScale), 
                                                 trajectory.GetRange(i, window).GetDtwPairingFeature(inputScale),
                                                 DtwPairingDistance);
                if (val < resVal)
                {
                    resVal = val;
                    resIdx = i;
                }
            }
            return new Tuple<int, Stroke, double, int>(resIdx, stroke, resVal, window);
        }

        private double CalculateMixedVal(double valPosition, double valShape)
        {
            return valPosition * valShape;
        }

        public static double DtwPairingDistance(double[] vec1, double[] vec2)
        {
            if (vec1.Length != vec2.Length)
            {
                throw new ArgumentException();
            }
            int n = vec1.Length;
            double res = 0.0;
            for (int i = 0; i < 2; i++)
            {
                double dist = vec1[i] - vec2[i];
                res += dist * dist;
            }
            res = Math.Sqrt(res);
            for (int i = 2; i < n; i++)
            {
                res += Geometry.DiffAngle(vec1[i], vec2[i]) / Math.PI;
            }
            return res;
        }

    }
}
