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
        public static readonly FeatureDescriptor<List<double>> FODX = FeatureDescriptor.Get<List<double>>("FODX");
        public static readonly FeatureDescriptor<List<double>> FODY = FeatureDescriptor.Get<List<double>>("FODY");
        public static readonly FeatureDescriptor<List<double>> FODAzimuth = FeatureDescriptor.Get<List<double>>("FODAzimuth");
        public static readonly FeatureDescriptor<List<double>> FODAltitude = FeatureDescriptor.Get<List<double>>("FODAltitude");
        public static readonly FeatureDescriptor<List<double>> FODPressure = FeatureDescriptor.Get<List<double>>("FODPressure");
        public static readonly FeatureDescriptor<List<double>> SODX = FeatureDescriptor.Get<List<double>>("SODX");
        public static readonly FeatureDescriptor<List<double>> SODY = FeatureDescriptor.Get<List<double>>("SODY");
        public static readonly FeatureDescriptor<List<double>> SineMeasure = FeatureDescriptor.Get<List<double>>("SineMeasure");
        public static readonly FeatureDescriptor<List<double>> CosineMeasure = FeatureDescriptor.Get<List<double>>("CosineMeasure");
        public static readonly FeatureDescriptor<List<double>> LengthBasedFO = FeatureDescriptor.Get<List<double>>("LengthBasedFO");
        public static readonly FeatureDescriptor<List<double>> LengthBasedSO = FeatureDescriptor.Get<List<double>>("LengthBasedSO");
        public static readonly FeatureDescriptor<List<double>> Velocity = FeatureDescriptor.Get<List<double>>("Velocity");
        public static readonly FeatureDescriptor<List<double>> Acceleration = FeatureDescriptor.Get<List<double>>("Acceleration");

        public static readonly IReadOnlyList<FeatureDescriptor> All =
           typeof(DerivedSvc2004Features).GetFields(BindingFlags.Public | BindingFlags.Static).Select(fi => fi.GetValue(null)).OfType<FeatureDescriptor>().ToList();
    }
}
