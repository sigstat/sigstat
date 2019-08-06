using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class StrokeExtract : PipelineBase, ITransformation
    { 
        [Input]
        public FeatureDescriptor<List<Vertex>> InputVertices { get; set; }

        [Output("Components")]
        public FeatureDescriptor<List<StrokeComponent>> OutputComponents { get; set; }

        private readonly static int minCircleLength = 8;

        private readonly static int minStrokeLength = 2;

        public void Transform(Signature signature)
        {
            this.LogInformation("StrokeExtraction - transform started");
            var vertices = signature.GetFeature<List<Vertex>>(InputVertices);
            var strokeEnds = vertices.StrokeEnds();
            var components = new List<StrokeComponent>();
            foreach (var end in strokeEnds)
            {
                HashSet<Vertex> isIn = new HashSet<Vertex>();
                components.GetAllStrokes().ForEach(stroke => stroke.ForEach(p => isIn.Add(p)));
                strokeEnds.ForEach(p => isIn.Remove(p));
                Traverse(end, new Stroke(), isIn, components);
            }
            signature.SetFeature<List<StrokeComponent>>(OutputComponents, components);
            this.LogInformation(components.Count.ToString() + " components were extracted");
            this.LogInformation("StrokeExtraction - transform finished");
        }

        private void Traverse(Vertex vertex, Stroke actualStroke, HashSet<Vertex> isIn, List<StrokeComponent> components)
        {
            
            isIn.Add(vertex);
            actualStroke.Add(vertex);
            if (vertex.PointType != VertexType.ConnectionPoint && actualStroke.Count > 1)
            {
                AddStroke(new Stroke(actualStroke), components);
                actualStroke.RemoveAt(actualStroke.Count - 1);
                return;
            }
            foreach (var neighbour in vertex.Neighbours)
            {
                ///Visszaeres a kezdobe -> tobbi crossing es endpointba ujra eljuthat
                if (actualStroke.Count == 1)
                {
                    components.GetAllStrokes().ForEach(stroke => isIn.Remove(stroke.Start));
                    isIn.Add(vertex);
                }

                if (!isIn.Contains(neighbour))
                {
                    Traverse(neighbour, actualStroke, isIn, components);
                }
                else if (neighbour == actualStroke[0])
                {
                    actualStroke.Add(neighbour);
                    AddCircle(new Stroke(actualStroke), components);
                    actualStroke.RemoveAt(actualStroke.Count - 1);
                }
            }
            actualStroke.RemoveAt(actualStroke.Count - 1);
        }

        private static void AddCircle(Stroke newStroke, List<StrokeComponent> components)
        {
            if (newStroke.Count > minCircleLength)
            {
                components.Add(new StrokeComponent(newStroke));
            }
        }

        private void AddStroke(Stroke newStroke, List<StrokeComponent> components)
        {
            if (newStroke.Count > minStrokeLength)
            {
                components.Add(new StrokeComponent(newStroke));
            }
        }
    }
}
