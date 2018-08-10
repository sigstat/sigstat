using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigStat.Common.Loaders
{
    //TODO: ez nem egy Loader
    public class ImageSaver
    {
        public static void Save(Signature signature, string path)
        {
            Image<Rgba32> img = signature.GetFeature(Features.Image);
            img.SaveAsPng(File.Create(path));
        }
    }
}
