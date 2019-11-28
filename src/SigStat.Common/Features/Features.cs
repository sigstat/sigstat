using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using SixLabors.Primitives;

namespace SigStat.Common
{
    /// <summary>
    /// Standard set of features.
    /// </summary>
    
    public static class Features
    {

        /// <summary>
        /// Actual bounds of the signature
        /// </summary>
        public static readonly FeatureDescriptor<SizeF> Size = FeatureDescriptor.Get<SizeF>("Size");
        /// <summary>
        /// Represents the main body of the signature <see cref="BasicMetadataExtraction"/>
        /// </summary>
        public static readonly FeatureDescriptor<Rectangle> TrimmedBounds = FeatureDescriptor.Get<Rectangle>("Trimmed bounds");

        /// <summary>
        /// Dots per inch
        /// </summary>
        public static readonly FeatureDescriptor<int> Dpi = FeatureDescriptor.Get<int>("Dpi");
        /// <summary>
        /// X coordinates of an online signature as a function of <see cref="T"/>
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> X = FeatureDescriptor.Get<List<double>>("X");
        /// <summary>
        /// Y coordinates of an online signature as a function of <see cref="T"/>
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Y = FeatureDescriptor.Get<List<double>>("Y");
        /// <summary>
        /// Timestamps for online signatures
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> T = FeatureDescriptor.Get<List<double>>("T");
        /// <summary>
        /// Pen position of an online signature as a function of <see cref="T"/>
        /// </summary>
        public static readonly FeatureDescriptor<List<bool>> Button = FeatureDescriptor.Get<List<bool>>("Button");
        /// <summary>
        /// Azimuth of an online signature as a function of <see cref="T"/>
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Azimuth = FeatureDescriptor.Get<List<double>>("Azimuth");
        /// <summary>
        /// Altitude of an online signature as a function of <see cref="T"/>
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Altitude = FeatureDescriptor.Get<List<double>>("Altitude");
        /// <summary>
        /// Pressure of an online signature as a function of <see cref="T"/>
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Pressure = FeatureDescriptor.Get<List<double>>("Pressure");
        /// <summary>
        /// The visaul representation of a signature
        /// </summary>
        public static readonly FeatureDescriptor<Image<Rgba32>> Image = FeatureDescriptor.Get<Image<Rgba32>>("Image");
        /// <summary>
        /// Center of gravity in a signature
        /// </summary>
        public static readonly FeatureDescriptor<Point> Cog = FeatureDescriptor.Get<Point>("Cog");

        /// <summary>
        /// Returns a readonly list of all <see cref="FeatureDescriptor"/>s defined in <see cref="Features"/>
        /// </summary>
        public static readonly IReadOnlyList<FeatureDescriptor> All = 
            typeof(Features)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(fi => fi.GetValue(null)).OfType<FeatureDescriptor>().ToList();


    }
}
