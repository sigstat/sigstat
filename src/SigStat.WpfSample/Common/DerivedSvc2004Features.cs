using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Common
{
    public static class DerivableSvc2004Features
    {
        public static readonly FeatureDescriptor X = Features.X;
        public static readonly FeatureDescriptor Y = Features.Y;
        public static readonly FeatureDescriptor Azimuth = Features.Azimuth;
        public static readonly FeatureDescriptor Altitude = Features.Altitude;
        public static readonly FeatureDescriptor Pressure = Features.Pressure;

        public static readonly IReadOnlyList<FeatureDescriptor> All =
           typeof(DerivableSvc2004Features).GetFields(BindingFlags.Public | BindingFlags.Static).Select(fi => fi.GetValue(null)).OfType<FeatureDescriptor>().ToList();
    }

    public static class DerivedSvc2004Features
    {
        public static readonly FeatureDescriptor<List<double>> FODX = FeatureDescriptor<List<double>>.Descriptor("FODX");
        public static readonly FeatureDescriptor<List<double>> FODY = FeatureDescriptor<List<double>>.Descriptor("FODY");
        public static readonly FeatureDescriptor<List<double>> FODAzimuth = FeatureDescriptor<List<double>>.Descriptor("FODAzimuth");
        public static readonly FeatureDescriptor<List<double>> FODAltitude = FeatureDescriptor<List<double>>.Descriptor("FODAltitude");
        public static readonly FeatureDescriptor<List<double>> FODPressure = FeatureDescriptor<List<double>>.Descriptor("FODPressure");
        public static readonly FeatureDescriptor<List<double>> SODX = FeatureDescriptor<List<double>>.Descriptor("SODX");
        public static readonly FeatureDescriptor<List<double>> SODY = FeatureDescriptor<List<double>>.Descriptor("SODY");
        public static readonly FeatureDescriptor<List<double>> SineMeasure = FeatureDescriptor<List<double>>.Descriptor("SineMeasure");
        public static readonly FeatureDescriptor<List<double>> CosineMeasure = FeatureDescriptor<List<double>>.Descriptor("CosineMeasure");
        public static readonly FeatureDescriptor<List<double>> LengthBasedFO = FeatureDescriptor<List<double>>.Descriptor("LengthBasedFO");
        public static readonly FeatureDescriptor<List<double>> LengthBasedSO = FeatureDescriptor<List<double>>.Descriptor("LengthBasedSO");
        public static readonly FeatureDescriptor<List<double>> Velocity = FeatureDescriptor<List<double>>.Descriptor("Velocity");
        public static readonly FeatureDescriptor<List<double>> Acceleration = FeatureDescriptor<List<double>>.Descriptor("Acceleration");

        public static readonly IReadOnlyList<FeatureDescriptor> All =
           typeof(DerivedSvc2004Features).GetFields(BindingFlags.Public | BindingFlags.Static).Select(fi => fi.GetValue(null)).OfType<FeatureDescriptor>().ToList();
    }
}
