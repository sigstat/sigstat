using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionMathHelper;
using SigStat.FusionBenchmark.FusionFeatureExtraction;

namespace SigStat.FusionBenchmark.TrajectoryRecovery
{
    [JsonObject(MemberSerialization.OptOut)]
    class MarosAlgorithm : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<Vertex>> InputVertices { get; set; }

        [Input]
        public FeatureDescriptor<List<StrokeComponent>> InputComponents { get; set; }

        [Output("BaseTrajectory")]
        public FeatureDescriptor<List<Vertex>> OutputBaseTrajectory { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("MarosAlgorithm - transform started");
            var strokes = signature.GetFeature<List<StrokeComponent>>(InputComponents).GetAllStrokes();
            var vertices = signature.GetFeature<List<Vertex>>(InputVertices);
            var trajectory = new List<Vertex>();

            var strokeEnds = new List<Vertex>();
            var endPoints = vertices.EndPoints();
            endPoints.Sort((p,q) => Geometry.Lefter(p.Pos, q.Pos));
            var crossingPoints = vertices.CrossingPoints();
            crossingPoints.Sort((p, q) => Geometry.Lefter(p.Pos, q.Pos));
            strokeEnds.AddRange(endPoints);
            strokeEnds.AddRange(crossingPoints);
            var isIn = new HashSet<StrokeComponent>();
            foreach (var end in strokeEnds)
            {
                Stroke actualStroke = strokes.Find(stroke => stroke.Start == end && !isIn.Contains(stroke.Component));
                while (actualStroke != null)
                {
                    trajectory.AddRange(actualStroke);
                    isIn.Add(actualStroke.Component);
                    actualStroke = FindNextStroke(actualStroke, strokes, isIn);
                }
            }
            signature.SetFeature<List<Vertex>>(OutputBaseTrajectory, trajectory);
            this.LogInformation(trajectory.Count.ToString() + " vertices - " + isIn.Count.ToString() + " components");
            this.LogInformation("MarosAlgorithm - transfrom finished");
        }

        private Stroke FindNextStroke(Stroke actualStroke, List<Stroke> strokes, HashSet<StrokeComponent> isIn)
        {
            Stroke res = null;
            double minVal = Double.MaxValue;
            double actualAngle = DOSBasedExtract.MakeSectionReverse(actualStroke, actualStroke.Count - 1).Direction();
            List<Stroke> neighbours = strokes.FindAll(stroke => actualStroke.IsNeighbour(stroke) && !isIn.Contains(stroke.Component));
            foreach (var stroke in neighbours)
            {
                double strokeAngle = DOSBasedExtract.MakeSection(stroke, 0).Direction();
                double val = Geometry.DiffAngle(actualAngle, strokeAngle);
                if (val < minVal)
                {
                    minVal = val;
                    res = stroke; 
                }
            }
            return res;
        }
           
    }
}
