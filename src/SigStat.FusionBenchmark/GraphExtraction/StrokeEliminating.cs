using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionMathHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class StrokeEliminating : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<double> InputWidthOfPen { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputContour { get; set; }

        [Input]
        public FeatureDescriptor<List<StrokeComponent>> InputComponent { get; set; }

        [Output("Components")]
        public FeatureDescriptor<List<StrokeComponent>> OutputComponent { get; set; }

        [Output("SpuriousSegments")]
        public FeatureDescriptor<List<ConnectionNode>> OutputSpuriousComps { get; set; }

        private static readonly double k1 = 4.0;

        private static readonly double k2 = 1.5;

        private static readonly double multiplyWidth = 0.60;

        public void Transform(Signature signature)
        {
            this.LogInformation("StrokeEliminating - transform started");
            var origComponents = signature.GetFeature<List<StrokeComponent>>(InputComponent);
            var contour = signature.GetFeature<List<Vertex>>(InputContour);
            double widthOfPen = signature.GetFeature<double>(InputWidthOfPen);
            var components = new List<StrokeComponent>();
            var spuriosComponents = new List<ConnectionNode>();

            foreach (var comp in origComponents)
            {
                if (IsComponentReal(comp, contour, widthOfPen)) { components.Add(comp);  }
                else { spuriosComponents.Add(comp); }
            }

            AddEnds(spuriosComponents, origComponents.GetAllStrokes());
            
            signature.SetFeature<List<StrokeComponent>>(OutputComponent, components);
            signature.SetFeature<List<ConnectionNode>>(OutputSpuriousComps, spuriosComponents);
            this.LogInformation("Real: {0}", components.Count);
            this.LogInformation("Spurious: {0}", spuriosComponents.Count);
            this.LogInformation("StrokeEliminating - transform finished");
        }

        private void AddEnds(List<ConnectionNode> spuriosComponents, List<Stroke> strokes)
        {
            foreach (var stroke in strokes)
            {
                spuriosComponents.Add(stroke.Start);
                spuriosComponents.Add(stroke.End);
            }
        }

        private static bool IsComponentReal(StrokeComponent comp, List<Vertex> contour, double widthOfPen)
        {
            var stroke = comp.Strokes[0];

            double thresReal = k1 * widthOfPen;
            double thresSpurious = k2 * widthOfPen;

            if ((double)stroke.Count < thresSpurious)
            {
                return false;
            }
 
            if ((double)stroke.Count > thresReal)
            {
                return true;
            }

            double thresSum = multiplyWidth * widthOfPen * stroke.Count;

            double maxDis = double.MinValue;
            double sum = 0.0;
            foreach (var vertex in stroke)
            {
                double dis = CalculateDis(vertex, contour);
                maxDis = Math.Max(maxDis, dis);
                sum += dis;
            }
            if (sum < thresSum && maxDis < widthOfPen)
            {
                return true;
            }
            return false;
        }

        private static double CalculateDis(Vertex vertex, List<Vertex> contour)
        {
            double res = double.MaxValue;
            foreach (var p in contour)
            {
                res = Math.Min(Geometry.Euclidean(vertex.Pos, p.Pos), res);
            }
            return res;
        }

        

    }
}
