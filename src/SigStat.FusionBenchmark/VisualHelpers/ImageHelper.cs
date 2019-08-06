using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using SigStat.FusionBenchmark.GraphExtraction;
using SixLabors.ImageSharp;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    public static class ImageHelper
    {
        public static void ReColour(this Image<Rgba32> img, Vertex vertex, Rgba32 newCol)
        {
            img.ReColour(vertex.Pos, newCol);
        }

        public static void ReColour(this Image<Rgba32> img, Point pos, Rgba32 newCol)
        {
            img[pos.X, img.Height - pos.Y] = newCol;
        }

        private static Rgba32[] colours = new Rgba32[]
                                            {
                                                Rgba32.LightGreen,
                                                Rgba32.Green,
                                                Rgba32.DarkGreen,
                                                Rgba32.Yellow,
                                                Rgba32.Orange,
                                                Rgba32.Red,
                                                Rgba32.DarkRed,
                                                Rgba32.DarkGray,
                                                Rgba32.Black,
                                            };

        public static void VariableColour(this Image<Rgba32> img, Point pos, int cnt)
        {
            img[pos.X, img.Height - pos.Y] = colours[cnt % colours.Length];
        }
    }
}
