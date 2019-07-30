using SigStat.Common;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing;
using System.Text;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.FusionBenchmark.TrajectoryReconsturction;
using SigStat.Common.Pipeline;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    class TrajectorySaver : PipelineBase, ILoggerObject
    {
        [Input]
        public FeatureDescriptor<VertexList> InputTrajectory { get; set; }

        public void Save(Signature signature, string path)
        {
            var img = signature.GetFeature<Image<Rgba32>>(FusionFeatures.Image).Clone();
            var trajectory = signature.GetFeature<VertexList>(InputTrajectory);
            const uint startCol = 0xFF000000;
            //Rgba32 col = new Rgba32(startCol);
            int cnt = 0;
            Rgba32[] szinek = new Rgba32[6]
            {
                Rgba32.Red,
                Rgba32.Yellow,
                Rgba32.Green,
                Rgba32.Purple,
                Rgba32.Orange,
                Rgba32.Pink
            };
            foreach (var p in trajectory)
            {
                if (p.On)
                {
                    ColourVertex(img, szinek[cnt % szinek.Length], p);
                }
                else
                {
                    cnt++;
                }
                
            }
            img.SaveAsPng(File.Create(path));
        }

        private static void ColourVertex(Image<Rgba32> img, Rgba32 newCol, Vertex p)
        {
            img[p.Pos.X, img.Height - p.Pos.Y] = newCol;
        }
    }
}
