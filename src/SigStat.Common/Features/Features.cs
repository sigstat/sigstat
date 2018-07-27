using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.Common
{
    public static class Features
    {
        public static readonly FeatureDescriptor<RectangleF> Bounds = new FeatureDescriptor<RectangleF>("Bounds", "Bounds");
        public static readonly FeatureDescriptor<Rectangle> TrimmedBounds = new FeatureDescriptor<Rectangle>("Trimmed bounds", "TrimmedBounds");

        public static readonly FeatureDescriptor<List<Loop>> Loop = new FeatureDescriptor<List<Loop>>("Loop", "Loop");
        public static readonly FeatureDescriptor<int> Dpi = new FeatureDescriptor<int>("DPI", "Dpi");
        public static readonly FeatureDescriptor<List<double>> X = new FeatureDescriptor<List<double>>("X(t)", "X");
        public static readonly FeatureDescriptor<List<double>> Y = new FeatureDescriptor<List<double>>("Y(t)", "Y");
        public static readonly FeatureDescriptor<List<double>> T = new FeatureDescriptor<List<double>>("t", "Svc2004.t");
        public static readonly FeatureDescriptor<List<int>> Button = new FeatureDescriptor<List<int>>("Button(t)", "Button");
        public static readonly FeatureDescriptor<List<int>> Azimuth = new FeatureDescriptor<List<int>>("Azimuth(t)", "Azimuth");
        public static readonly FeatureDescriptor<List<int>> Altitude = new FeatureDescriptor<List<int>>("Altitude(t)", "Altitude");
        public static readonly FeatureDescriptor<List<int>> Pressure = new FeatureDescriptor<List<int>>("Pressure(t)", "Pressure");
        public static readonly FeatureDescriptor<Image<Rgba32>> Image = new FeatureDescriptor<Image<Rgba32>>("Image", "Image");
        public static readonly FeatureDescriptor<Point> Cog = new FeatureDescriptor<Point>("Center of gratvity", "Cog");


    }
}
