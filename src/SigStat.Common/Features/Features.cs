using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.Common
{
    public static class Features
    {
        public static readonly FeatureDescriptor<RectangleF> Bounds = new FeatureDescriptor<RectangleF>("Bounds", "Bounds");
        public static readonly FeatureDescriptor<List<Loop>> Loop = new FeatureDescriptor<List<Loop>>("Loop", "Loop");
        public static readonly FeatureDescriptor<List<double>> X = new FeatureDescriptor<List<double>>("X(t)", "X");
        public static readonly FeatureDescriptor<List<double>> Y = new FeatureDescriptor<List<double>>("Y(t)", "Y");
        public static readonly FeatureDescriptor<List<double>> T = new FeatureDescriptor<List<double>>("t", "Svc2004.t");
        public static readonly FeatureDescriptor<List<double>> Button = new FeatureDescriptor<List<double>>("Button(t)", "Button");
        public static readonly FeatureDescriptor<List<double>> Azimuth = new FeatureDescriptor<List<double>>("Azimuth(t)", "Azimuth");
        public static readonly FeatureDescriptor<List<double>> Altitude = new FeatureDescriptor<List<double>>("Altitude(t)", "Altitude");
        public static readonly FeatureDescriptor<List<double>> Pressure = new FeatureDescriptor<List<double>>("Pressure(t)", "Pressure");

    }
}
