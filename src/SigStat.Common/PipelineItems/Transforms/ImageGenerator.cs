using SigStat.Common.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    /// <summary>
    /// Binarized -> Image feature transzformacio
    /// </summary>
    public class ImageGenerator : PipelineBase, ITransformation
    {
        Rgba32 fg;
        Rgba32 bg;

        public ImageGenerator()
        {
            fg = Rgba32.LightBlue;
            bg = Rgba32.White;
            this.Output(Features.Image);
        }

        public ImageGenerator(Rgba32 foregroundColor, Rgba32 backgroundColor)
        {
            fg = foregroundColor;
            bg = backgroundColor;
            this.Output(Features.Image);
        }

        public void Transform(Signature signature)
        {
            bool[,] b = signature.GetFeature<bool[,]>(InputFeatures[0]);
            int w = b.GetLength(0);
            int h = b.GetLength(1);

            //itt pl lehetne Dilate meg ilyesmi

            Image<Rgba32> img = new Image<Rgba32>(w, h);

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                    img[x, y] = b[x, y] ? fg : bg;
                Progress = (int)(x / (double)w * 95);
            }

            signature.SetFeature(OutputFeatures[0], img);
            Progress = 100;
            Log(LogLevel.Info, "Image generation from binary raster done.");
        }
    }
}
