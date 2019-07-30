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

namespace SigStat.FusionBenchmark.VisualHelpers
{
    class StrokeSaver
    {

        public static void Save(Signature signature, string path)
        {
            var img = signature.GetFeature<Image<Rgba32>>(FusionFeatures.Image).Clone();
            var components = signature.GetFeature<StrokeCollection>(FusionFeatures.Strokes);
            var connects = signature.GetFeature<VertexCollection>(FusionFeatures.Connects);
            var endPoints = signature.GetFeature<VertexCollection>(FusionFeatures.EndPoints);
            var crossingPoints = signature.GetFeature<VertexCollection>(FusionFeatures.CrossingPoints);
            var edgeList = signature.GetFeature<List<StrokeEdge>>(FusionFeatures.StrokeEdgeList);
            foreach (var comp in components.Values)
            {
                foreach (var p in comp.A.Vertices)
                {
                    ColourVertex(img, Rgba32.Red, p);
                }
            }
            foreach (var p in connects.Values)
            {
                ColourVertex(img, Rgba32.Yellow, p);
            }
            
            foreach (var edge in edgeList)
            {
                foreach (var p in edge.Vertices)
                {
                    ColourVertex(img, Rgba32.Green, p);
                }
            }
            foreach (var p in endPoints.Values)
            {
                ColourVertex(img, Rgba32.Orange, p);
            }
            foreach (var p in crossingPoints.Values)
            {
                ColourVertex(img, Rgba32.Purple, p);
            }
            
            img.SaveAsPng(File.Create(path));
        }

        private static void ColourVertex(Image<Rgba32> img, Rgba32 newCol, Vertex p)
        {
            img[p.Pos.X, img.Height - p.Pos.Y] = newCol;
        }

    }
}
