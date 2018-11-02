using SigStat.Common.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Generates an image feature out of a binary raster.
    /// Optionally, saves the image to a png file.
    /// Useful for debugging pipeline steps.
    /// <para>Pipeline Input type: bool[,]</para>
    /// <para>Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage</para>
    /// </summary>
    public class ImageGenerator : PipelineBase, ITransformation
    {
        private readonly bool writeToFile;
        Rgba32 fg;
        Rgba32 bg;

        /// <summary> Initializes a new instance of the <see cref="ImageGenerator"/> class with default settings: skip file writing, Blue ink on white paper. </summary>
        public ImageGenerator() : this(false, Rgba32.LightBlue, Rgba32.White) { }
        /// <summary> Initializes a new instance of the <see cref="ImageGenerator"/> class with default settings. </summary>
        /// <param name="writeToFile">Whether to save the generated image into a png file.</param>
        public ImageGenerator(bool writeToFile) : this(writeToFile, Rgba32.LightBlue, Rgba32.White) { }
        /// <summary> Initializes a new instance of the <see cref="ImageGenerator"/> class with specified settings. </summary>
        /// <param name="writeToFile">Whether to save the generated image into a png file.</param>
        /// <param name="foregroundColor">Ink color.</param>
        /// <param name="backgroundColor">Paper color.</param>
        public ImageGenerator(bool writeToFile, Rgba32 foregroundColor, Rgba32 backgroundColor)
        {
            this.writeToFile = writeToFile;
            fg = foregroundColor;
            bg = backgroundColor;
            //this.Output(Features.Image);
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            //default output is '{input}', '{input}Image'
            if (OutputFeatures == null)
            {
                OutputFeatures = new List<FeatureDescriptor> {
                    InputFeatures[0],
                    FeatureDescriptor.Get<Image<Rgba32>>(InputFeatures[0].Name + "Image")
                };
            }
            bool[,] b = signature.GetFeature<bool[,]>(InputFeatures[0]);
            int w = b.GetLength(0);
            int h = b.GetLength(1);

            //itt pl lehetne Dilate meg ilyesmi

            Image<Rgba32> img = new Image<Rgba32>(w, h);

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    img[x, h - 1 - y] = b[x, y] ? fg : bg;
                }
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
