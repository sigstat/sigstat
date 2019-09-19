using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    [JsonObject(MemberSerialization.OptOut)]
    public class XYSaver : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<Image<Rgba32>> InputImage { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; }

        [Input]
        public FeatureDescriptor<List<bool>> InputButton { get; set; }

        [Input]
        public string InputBasePath { get; set; }

        [Input]
        public string InputFileName { get; set; }

        public void Transform(Signature signature)
        {
            string path = InputBasePath + "/" + signature.Signer.ID + "_" + signature.ID + InputFileName + ".png";
            var img = new Image<Rgba32>(signature.GetFeature<Image<Rgba32>>(InputImage).Width, signature.GetFeature<Image<Rgba32>>(InputImage).Height);
            var xs = signature.GetFeature<List<double>>(InputX);
            var ys = signature.GetFeature<List<double>>(InputY);
            var bs = signature.GetFeature<List<bool>>(InputButton);
            int n = xs.Count;
            if (n != xs.Count || n != ys.Count || n != bs.Count)
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < n; i++)
            {
                img.ReColour(new Point((int)xs[i], (int)ys[i]), Rgba32.Red);
            }

            img.SaveAsPng(File.Create(path));
        }
    }
}
