using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigStat.Common.Loaders
{
    //TODO: ez nem egy Loader
    /// <summary>
    /// Get the <see cref="Features.Image"/> of a <see cref="Signature"/> and save it as png file.
    /// </summary>
    public class ImageSaver
    {
        /// <summary>
        /// Saves a png image file to the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="signature">Input signature containing <see cref="Features.Image"/>.</param>
        /// <param name="path">Output file path of the png image.</param>
        public static void Save(Signature signature, string path)
        {
            Image<Rgba32> img = signature.GetFeature(Features.Image);
            img.SaveAsPng(File.Create(path));
        }
    }
}
