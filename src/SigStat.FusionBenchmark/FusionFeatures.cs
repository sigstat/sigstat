using SigStat.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Linq;
using SigStat.FusionBenchmark.GraphExtraction;

namespace SigStat.FusionBenchmark
{
    class FusionFeatures
    {

        /// <summary>
        /// Actual bounds of the signature
        /// </summary>
        public static readonly FeatureDescriptor<RectangleF> Bounds = FeatureDescriptor.Get<RectangleF>("Bounds");

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
        /// Binarized, thinned visual representation of a signature
        /// </summary>
        public static readonly FeatureDescriptor<bool[,]> Skeleton = FeatureDescriptor.Get<bool[,]>("Skeleton");

        /// <summary>
        /// Vertices, 1 pixel in skeleton -> 1 vertex in vertices
        /// </summary>
        public static readonly FeatureDescriptor<List<Vertex>> Vertices  = FeatureDescriptor.Get<List<Vertex>>("Vertices");

        /// <summary>
        /// Vertices, 1 pixel in skeleton -> 1 vertex in vertices
        /// </summary>
        public static readonly FeatureDescriptor<List<StrokeComponent>> Components = FeatureDescriptor.Get<List<StrokeComponent>>("Components");

        /// <summary>
        /// Center of gravity in a signature
        /// </summary>
        public static readonly FeatureDescriptor<Point> Cog = FeatureDescriptor.Get<Point>("Cog");

        /// <summary>
        /// BaseTrajectory
        /// </summary>
        public static readonly FeatureDescriptor<List<Vertex>> BaseTrajectory = FeatureDescriptor.Get<List<Vertex>>("BaseTrajectory");

        /// <summary>
        /// Trajectory
        /// </summary>
        public static readonly FeatureDescriptor<List<Vertex>> Trajectory = FeatureDescriptor.Get<List<Vertex>>("Trajectory");

        /// <summary>
        /// Curvature
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Curvature = FeatureDescriptor.Get<List<double>>("Curvature");

        /// <summary>
        /// Curvature
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Directions = FeatureDescriptor.Get<List<double>>("Directions");

        /// <summary>
        /// StrokaMatches from DtwPairing
        /// </summary>
        public static readonly FeatureDescriptor<List<Tuple<int, Stroke, double, int>>> StrokeMatches = FeatureDescriptor.Get<List<Tuple<int, Stroke, double, int>>>("StrokeMatches");

        public static readonly FeatureDescriptor<List<double>> Tangent = FeatureDescriptor.Get<List<double>>("Tangent");

        /// <summary>
        /// Returns a readonly list of all <see cref="FeatureDescriptor"/>s defined in <see cref="Features"/>
        /// </summary>
        public static readonly IReadOnlyList<FeatureDescriptor> All =
            typeof(Features)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(fi => fi.GetValue(null)).OfType<FeatureDescriptor>().ToList();

    }
}
