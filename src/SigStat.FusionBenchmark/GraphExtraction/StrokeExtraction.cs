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
    class StrokeExtraction: PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<VertexCollection> InputVertices { get; set; }

        [Input]
        public FeatureDescriptor<VertexCollection> InputEndPoints { get; set; }

        [Input]
        public FeatureDescriptor<VertexCollection> InputCrossingPoints { get; set; }

        [Output("Strokes")]
        public FeatureDescriptor<StrokeCollection> OutputStrokes { get; set; }

        [Output("Connects")]
        public FeatureDescriptor<VertexCollection> OutputConnects { get; set; }

        public readonly static int minLength = 5;

        private VertexCollection vertices;
        private Stroke actualStroke;
        private StrokeCollection components;
        private VertexCollection connects;


        public void Transform(Signature signature)
        {
            this.LogInformation("TraverseStrokes - GraphBased transform started.");
            vertices = signature.GetFeature<VertexCollection>(InputVertices);
            var endPoints = signature.GetFeature<VertexCollection>(InputEndPoints);
            var crossingPoints = signature.GetFeature<VertexCollection>(InputCrossingPoints);
            foreach (var p in vertices)
            {
                p.Value.Parent = null;
            }
            components = new StrokeCollection();
            connects = new VertexCollection();
            foreach (var p in endPoints.Values)
            {
                actualStroke = new Stroke();
                if (p.Parent == null)
                    Traverse(p);
            }
            foreach (var p in crossingPoints.Values)
            {
                actualStroke = new Stroke();
                if (p.Parent == null)
                    Traverse(p);
            }
            foreach (var stroke in components.GetAllStrokes())
            {
                connects.Add(stroke.Start());
                connects.Add(stroke.End());
            }
            signature.SetFeature<StrokeCollection>(OutputStrokes, components);
            signature.SetFeature<VertexCollection>(OutputConnects, connects);
            signature.SetFeature<VertexCollection>(InputEndPoints, null);
            this.LogInformation("During the process " + components.Count.ToString() + " components found.");
            this.LogInformation("TraverseStrokes - GraphBased transform finished.");
        }

        private void Traverse(Vertex p)
        {
            if (p.Parent == null)
                p.Parent = p;
            actualStroke.Add(p);
            if (p.Degree() != 2)
            {
                AddStroke();
                actualStroke.Add(p);
            }

            foreach (var q in p.Neighbours)
            {
                if (q.Parent != null)
                {
                    if (q.ID != p.Parent.ID)
                    {
                        actualStroke.Add(q);
                        AddStroke();
                        actualStroke.Add(p);
                    }
                }
                else
                {
                    q.Parent = p;
                    Traverse(q);
                }
            }
        }

        private void AddStroke()
        {
            if (actualStroke.Vertices.Count < minLength)
            {
                connects.Add(actualStroke.Vertices);
            }
            else
            {
                var newComponent = new StrokeComponent(components.Count);
                actualStroke.Component = newComponent;
                newComponent.A = actualStroke;
                newComponent.B = actualStroke.ReversedClone();
                components.Add(newComponent);
            }
            actualStroke = new Stroke();
        }
    }
}
