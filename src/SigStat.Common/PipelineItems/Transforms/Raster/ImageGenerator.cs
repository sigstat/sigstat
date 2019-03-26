using SigStat.Common.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;
using Newtonsoft.Json;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Generates an image feature out of a binary raster.
    /// Optionally, saves the image to a png file.
    /// Useful for debugging pipeline steps.
    /// <para>Pipeline Input type: bool[,]</para>
    /// <para>Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageGenerator : PipelineBase, ITransformation
    {
        [Input]
        [JsonProperty]
        public FeatureDescriptor<bool[,]> Input { get; set; }
        [Output("Binarized")]
        public FeatureDescriptor<bool[,]> Output { get; set; }

        [Output("Image")]
        public FeatureDescriptor<Image<Rgba32>> OutputImage { get; set; }

        public bool WriteToFile { get; set; }

        public Rgba32 ForegroundColor { get; set; }
        public Rgba32 BackgroundColor { get; set; }

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
            this.WriteToFile = writeToFile;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            //default output is '{input}', '{input}Image'
            Output = Input;
            if (OutputImage == null)
            {
                OutputImage = FeatureDescriptor<Image<Rgba32>>.Get(Input.Name + "Image");//TODO: <T> template-es Register()
            }

            bool[,] b = signature.GetFeature(Input);
            int w = b.GetLength(0);
            int h = b.GetLength(1);

            //itt pl lehetne Dilate meg ilyesmi

            Image<Rgba32> img = new Image<Rgba32>(w, h);

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    img[x, h - 1 - y] = b[x, y] ? ForegroundColor : BackgroundColor;
                }
                Progress = (int)(x / (double)w * 95);
            }

            signature.SetFeature(OutputImage, img);

            if (WriteToFile)
            {
                string signerString = (signature.Signer != null) ? signature.Signer.ID : "Null";
                string filename = $"{signature.ID ?? "Null"}_{Input.Name}.png";
                img.SaveAsPng(File.Create(filename));
                this.LogInformation($"Image saved: {filename}");
            }

            Progress = 100;
            this.LogInformation($"Image generation from binary raster done.");
        }
    }
}
