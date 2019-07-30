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
using SigStat.FusionBenchmark.TrajectoryReconsturction;

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

        public static readonly FeatureDescriptor<VertexCollection> Vertices = FeatureDescriptor.Get<VertexCollection>("Vertices");

        public static readonly FeatureDescriptor<VertexCollection> EndPoints = FeatureDescriptor.Get<VertexCollection>("EndPoints");

        public static readonly FeatureDescriptor<VertexCollection> CrossingPoints = FeatureDescriptor.Get<VertexCollection>("CrossingPoints");

        public static readonly FeatureDescriptor<VertexCollection> Connects = FeatureDescriptor.Get<VertexCollection>("Connects");

        public static readonly FeatureDescriptor<StrokeCollection> Strokes = FeatureDescriptor.Get<StrokeCollection>("Strokes");

        public static readonly FeatureDescriptor<List<StrokeEdge>> StrokeEdgeList = FeatureDescriptor.Get<List<StrokeEdge>>("StrokeEdgeList");

        public static readonly FeatureDescriptor<List<StrokeEdge>> InDirectStrokeEdgeList = FeatureDescriptor.Get<List<StrokeEdge>>("InDirectStrokeEdgeList");

        public static readonly FeatureDescriptor<VertexList> BaseTrajectory = FeatureDescriptor.Get<VertexList>("BaseTrajectory");

        public static readonly FeatureDescriptor<VertexList> Trajectory = FeatureDescriptor.Get<VertexList>("Trajectory");

        public static readonly FeatureDescriptor<List<double>> Curvature = FeatureDescriptor.Get<List<double>>("Curvature");

        public static readonly FeatureDescriptor<Stroke> NullStroke = FeatureDescriptor.Get<Stroke>("NullStroke");

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
