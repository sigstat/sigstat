using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Text;


using System.Linq;

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
        public int InputBaseSigIdx { get; set; }

        [Output("Trajectory")]
        public FeatureDescriptor<List<Vertex>> OutputTrajectory { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("DtwPairing - DtwCompare started");
            List<Stroke> strokes = signature.GetFeature<List<StrokeComponent>>(InputComponents).GetAllStrokes();
            var baseTrajectory = signature.Signer.Signatures[InputBaseSigIdx].GetFeature<List<Vertex>>(InputBaseTrajectory);

            List<Tuple<int, Stroke, double>> order = new List<Tuple<int, Stroke, double>>();
            foreach (var stroke in strokes)
            {
                order.Add(CalculateIdx(stroke, baseTrajectory, stroke.Count, InputJump));
            }
            signature["tmp"] = order.ToList();
            order.Sort( (Tuple<int, Stroke, double> p, Tuple<int, Stroke, double> q) =>
                              { return p.Item1 < q.Item1 ? -1 : 1; } );
            List<Stroke> strokeOrder = SelectStrokes(order);
            var trajectory = new List<Vertex>();
            strokeOrder.ForEach(stroke => trajectory.AddRange(stroke));
            signature.SetFeature<List<Vertex>>(OutputTrajectory, trajectory);
            this.LogInformation("DtwPairing - DtwCompare finished");
        }

        private List<Stroke> SelectStrokes(List<Tuple<int, Stroke, double>> order)
        {
            List<Stroke> res = new List<Stroke>();
            foreach (var tuple in order)
            {
                var sibling = order.Find(find => find.Item2 == tuple.Item2.Sibling);
                if (tuple.Item3 < sibling.Item3) { res.Add(tuple.Item2); }
            }
            return res; 
        }

        private double CalculateThres(List<Tuple<int, Stroke, double>> strokeOrder, int strokeCnt)
        {
            double minThres = 0.0;
            double maxThres = +10000.0;
            while (maxThres - minThres > 1.0)
            {
                double newThres = (minThres + maxThres) / 2; 
                int cnt = strokeOrder.FindAll(tuple => tuple.Item3 < newThres).Count;
                if (cnt < strokeCnt)    { minThres = newThres; }
                else                    { maxThres = newThres; }
            }
            double thres = (minThres + maxThres / 2);
            return thres;
        }

        private Tuple<int, Stroke, double> CalculateIdx(Stroke stroke, List<Vertex> trajectory, int window, int jump = 1)
        {
            int resIdx = int.MaxValue;
            double resVal = Double.MaxValue;
            for (int i = 0; i < trajectory.Count - window; i += jump)
            {
                double val = DtwPy.Dtw<double[]>(stroke.GetXY(), trajectory.GetRange(i, window).GetXY(),
                                                 DtwPy.EuclideanDistance);
                if (val < resVal)
                {
                    resVal = val;
                    resIdx = i;
                }
            }
            return new Tuple<int, Stroke, double>(resIdx, stroke, resVal);
        }
    }
}
