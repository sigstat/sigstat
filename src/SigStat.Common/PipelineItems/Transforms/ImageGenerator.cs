using SigStat.Common.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    /// <summary>
    /// Binary raster -> Image type feature.
    /// Useful for debugging pipeline steps.
    /// Output: {Inpu}t, {Input}Image
    /// </summary>
    public class ImageGenerator : PipelineBase, ITransformation
    {
        private readonly bool writeToFile;
        Rgba32 fg;
        Rgba32 bg;

        public ImageGenerator() : this(false, Rgba32.LightBlue, Rgba32.White)
        {
        }
        public ImageGenerator(bool writeToFile) : this(writeToFile, Rgba32.LightBlue, Rgba32.White)
        {
        }
        public ImageGenerator(bool writeToFile, Rgba32 foregroundColor, Rgba32 backgroundColor)
        {
            this.writeToFile = writeToFile;
            fg = foregroundColor;
            bg = backgroundColor;
            //this.Output(Features.Image);
        }

        public void Transform(Signature signature)
        {
            //default output is '{input}', '{input}Image'
            if (OutputFeatures == null)
                OutputFeatures = new List<FeatureDescriptor> {
                    InputFeatures[0],
                    FeatureDescriptor<Image<Rgba32>>.Descriptor(InputFeatures[0].Name + "Image")
                };

            bool[,] b = signature.GetFeature<bool[,]>(InputFeatures[0]);
            int w = b.GetLength(0);
            int h = b.GetLength(1);

            //itt pl lehetne Dilate meg ilyesmi

            Image<Rgba32> img = new Image<Rgba32>(w, h);

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                    img[x, h-1-y] = b[x, y] ? fg : bg;
                Progress = (int)(x / (double)w * 95);
            }

            signature.SetFeature(OutputFeatures[1], img);

            if(writeToFile)
            {
                string signerString = (signature.Signer!=null) ? signature.Signer.ID : "Null";
                string filename = $"U_{signerString}_S_{signature.ID?? "Null"}_{OutputFeatures[1].Name}.png";
                img.SaveAsPng(File.Create(filename));
                Log(LogLevel.Info, $"Image saved: {filename}");
            }

            Progress = 100;
            Log(LogLevel.Info, $"Image generation from binary raster done.");
        }
    }
}
