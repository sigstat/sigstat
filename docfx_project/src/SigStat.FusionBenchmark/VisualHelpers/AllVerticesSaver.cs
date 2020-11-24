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
    class AllVerticesSaver : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<Image<Rgba32>> InputImage { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputAreaOfStrokes { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputContour { get; set; }

        [Input]
        public string InputBasePath { get; set; }

        [Input]
        public string InputFileName { get; set; }

        public void Transform(Signature signature)
        {
            string path = InputBasePath + "/" + signature.Signer.ID + "_" + signature.ID + InputFileName + ".png";
            var img = signature.GetFeature<Image<Rgba32>>(InputImage).Clone();
            var areaOfStrokes = signature.GetFeature<List<Vertex>>(InputAreaOfStrokes);
            var contour = signature.GetFeature<List<Vertex>>(InputContour);
            
            foreach (var p in areaOfStrokes)
            {
                img.ReColour(p, Rgba32.Red);
            }
            foreach (var p in contour)
            {
                img.ReColour(p, Rgba32.Black);
            }
           
            img.SaveAsPng(File.Create(path));
        }
    }
}
