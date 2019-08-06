using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    [JsonObject(MemberSerialization.OptOut)]
    class VerticesSaver : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<Image<Rgba32>> InputImage { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputVertices { get; set; }

        [Input]
        public FeatureDescriptor<Point> InputCog { get; set; }

        [Input]
        public string InputBasePath { get; set; }

        [Input]
        public string InputFileName { get; set; }

        public void Transform(Signature signature)
        {
            string path = InputBasePath + "/" + signature.Signer.ID + "_" + signature.ID + InputFileName + ".png";
            var img = signature.GetFeature<Image<Rgba32>>(InputImage).Clone();
            var vertices = signature.GetFeature<List<Vertex>>(InputVertices);
            var endPoints = vertices.EndPoints();
            var crossingPoints = vertices.CrossingPoints();
            var cog = signature.GetFeature<Point>(FusionFeatures.Cog);
            foreach (var p in vertices)
            {
                img.ReColour(p, Rgba32.Red);
            }
            foreach (var p in endPoints)
            {
                img.ReColour(p, Rgba32.Yellow);
            }
            foreach (var p in crossingPoints)
            {
                img.ReColour(p, Rgba32.Green);
            }
            img.ReColour(cog, Rgba32.Blue);
            img.SaveAsPng(File.Create(path));
        }

    }
}
