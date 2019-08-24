using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    [JsonObject(MemberSerialization.OptOut)]
    internal class RealSpuriousSaver : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<Image<Rgba32>> InputImage { get; set; }

        [Input]
        public FeatureDescriptor<List<StrokeComponent>> InputComponents { get; set; }

        [Input]
        public FeatureDescriptor<List<ConnectionNode>> InputSpuriousComps { get; set; }

        [Input]
        public string InputBasePath { get; set; }

        [Input]
        public string InputFileName { get; set; }

        public void Transform(Signature signature)
        {
            string path = InputBasePath + "/" + signature.Signer.ID + "_" + signature.ID + InputFileName + ".png";
            var img = signature.GetFeature<Image<Rgba32>>(InputImage).Clone();
            var realStrokes = signature.GetFeature<List<StrokeComponent>>(InputComponents).GetAllStrokes();
            var spuriousStrokes = signature.GetFeature<List<ConnectionNode>>(InputSpuriousComps);
            foreach (var stroke in realStrokes)
            {
                stroke.ForEach(p => img.ReColour(p.Pos, Rgba32.Red));
            }
            int cnt = 0;
            foreach (var connectionNode in spuriousStrokes)
            {
                foreach (var p in connectionNode)
                {
                    img.ReColour(p.Pos, Rgba32.Green);
                }
            }
            img.SaveAsPng(File.Create(path));
        }
    }
}
