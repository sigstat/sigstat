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
        public static readonly FeatureDescriptor<RectangleF> Bounds = FeatureDescriptor<RectangleF>.Descriptor("Bounds");
        public static readonly FeatureDescriptor<Rectangle> TrimmedBounds = FeatureDescriptor<Rectangle>.Descriptor("Trimmed bounds");

        public static readonly FeatureDescriptor<List<Loop>> Loop = FeatureDescriptor<List<Loop>>.Descriptor("Loop");
        public static readonly FeatureDescriptor<int> Dpi = FeatureDescriptor<int>.Descriptor("Dpi");
        public static readonly FeatureDescriptor<List<double>> X = FeatureDescriptor<List<double>>.Descriptor("X");
        public static readonly FeatureDescriptor<List<double>> Y = FeatureDescriptor<List<double>>.Descriptor("Y");
        public static readonly FeatureDescriptor<List<double>> T = FeatureDescriptor<List<double>>.Descriptor("T");
        public static readonly FeatureDescriptor<List<bool>> Button = FeatureDescriptor<List<bool>>.Descriptor("Button");
        public static readonly FeatureDescriptor<List<double>> Azimuth = FeatureDescriptor<List<double>>.Descriptor("Azimuth");
        public static readonly FeatureDescriptor<List<double>> Altitude = FeatureDescriptor<List<double>>.Descriptor("Altitude");
        public static readonly FeatureDescriptor<List<double>> Pressure = FeatureDescriptor<List<double>>.Descriptor("Pressure");
        public static readonly FeatureDescriptor<Image<Rgba32>> Image = FeatureDescriptor<Image<Rgba32>>.Descriptor("Image");
        public static readonly FeatureDescriptor<Point> Cog = FeatureDescriptor<Point>.Descriptor("Cog");

        public static readonly IReadOnlyList<FeatureDescriptor> All = 
            typeof(Features).GetFields(BindingFlags.Public | BindingFlags.Static).Select(fi => fi.GetValue(null)).OfType<FeatureDescriptor>().ToList();


    }
}
