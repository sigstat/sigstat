using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Linq;

namespace SigStat.Common
{
    /// <summary>
    /// Standard set of features.
    /// </summary>
    public static class Features
    {
        public static readonly FeatureDescriptor<RectangleF> Bounds = FeatureDescriptor.Get<RectangleF>("Bounds");
        public static readonly FeatureDescriptor<Rectangle> TrimmedBounds = FeatureDescriptor.Get<Rectangle>("Trimmed bounds");

        public static readonly FeatureDescriptor<int> Dpi = FeatureDescriptor.Get<int>("Dpi");
        public static readonly FeatureDescriptor<List<double>> X = FeatureDescriptor.Get<List<double>>("X");
        public static readonly FeatureDescriptor<List<double>> Y = FeatureDescriptor.Get<List<double>>("Y");
        public static readonly FeatureDescriptor<List<double>> T = FeatureDescriptor.Get<List<double>>("T");
        public static readonly FeatureDescriptor<List<bool>> Button = FeatureDescriptor.Get<List<bool>>("Button");
        public static readonly FeatureDescriptor<List<double>> Azimuth = FeatureDescriptor.Get<List<double>>("Azimuth");
        public static readonly FeatureDescriptor<List<double>> Altitude = FeatureDescriptor.Get<List<double>>("Altitude");
        public static readonly FeatureDescriptor<List<double>> Pressure = FeatureDescriptor.Get<List<double>>("Pressure");
        public static readonly FeatureDescriptor<Image<Rgba32>> Image = FeatureDescriptor.Get<Image<Rgba32>>("Image");
        public static readonly FeatureDescriptor<Point> Cog = FeatureDescriptor.Get<Point>("Cog");

        public static readonly IReadOnlyList<FeatureDescriptor> All = 
            typeof(Features).GetFields(BindingFlags.Public | BindingFlags.Static).Select(fi => fi.GetValue(null)).OfType<FeatureDescriptor>().ToList();


    }
}
