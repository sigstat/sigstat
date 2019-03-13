using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SigStat.Common.Helpers
{
    public class BenchmarkConfig
    {
        // 32256 = 8*3*3*16*2*7*2
        // Features: 8
        // Sampling: 3
        // DB: 3
        // Translation+Scaling: 16
        // Rotation: 2
        // Resampling+Interpolation: 7
        // Filter: 2

        //minta
        //    Features: X, Y, P, a1, a2, XY, XYP, XYPA1A2
        //string config = "{sampling:  s1,s2,s3}" +
        //    " database: SVC2004, MCYT100" +
        //    "Filter: none, P" +
        //    "Rotation: true, false" +
        //    "translation: None, CogToOiriginX,CogToOriginY,CogToOiriginXY, BottomLeftToOrigin;" +
        //    "UniformScaling: None, X01Y0prop, Y01X0prop" +
        //    "Scaling: None, X01, Y01, X01Y01" +
        //    "ResamplingType: none, CubicTimeSlotLength, CubicSampleCount, CubicFillPenUp, LinearTimeSlotLength, LinearSampleCount, LinearFillPenUp
        //    "Interpolation: , }";

        public static BenchmarkConfig FromJsonString(string jsonString)
        {
            return JsonConvert.DeserializeObject<BenchmarkConfig>(jsonString);
        }

        public string ToShortString()
        {
            return string.Join("_", GetType().GetProperties().Select(pi => pi.GetValue(this)).Where(v=>v!=null).Select(v=>v.ToString()));
        }
        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public BenchmarkConfig FromJsonFile(string path)
        {
            return JsonConvert.DeserializeObject<BenchmarkConfig>(File.ReadAllText(path));
        }

        public static List<BenchmarkConfig> GenerateConfigurations()
        {
            List<BenchmarkConfig> l = new List<BenchmarkConfig>();
            l.Add(new BenchmarkConfig());
            l = Samplers(Databases(Filters(Rotations(TranslationScalings(Interpolations(ResamplingTypes(SetFeatures(l))))))));
            return l;
        }

        private static List<BenchmarkConfig> Samplers(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Sampling = "S1");
            var l2 = l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Sampling = "S2"
            });
            var l3 = l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Sampling = "S3"
            });
            l.AddRange(l2);
            l.AddRange(l3);
            return l;
        }

        private static List<BenchmarkConfig> Databases(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Database = "SCV2004");
            List<string> es = new List<string>() { "MCYT100", "..." };
            var ls = es.SelectMany(e => l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Database = e
            })).ToList();
            l.AddRange(ls);
            return l;
        }

        private static List<BenchmarkConfig> Filters(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Filter = "None");
            var l2 = l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Filter = "P"//
            });
            l.AddRange(l2);
            return l;
        }

        private static List<BenchmarkConfig> Rotations(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Rotation = false);
            var l2 = l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Rotation = true
            });
            l.AddRange(l2);
            return l;
        }

        private static List<BenchmarkConfig> TranslationScalings(List<BenchmarkConfig> l)
        {
            //jobb kezzel megadni az ertelmes parokat: 16 db van, osszes 30 helyett
            l.ForEach(c => c.TranslationScaling = ("None","None"));
            List<(string,string)> es = new List<(string, string)>() {
                ("None","X01Y0prop"),
                ("None","Y01X0prop"),
                ("None","X01"),
                ("None","Y01"),
                ("None","X01Y01"),

                ("CogToOriginX","None"),
                ("CogToOriginX","Y01"),

                ("CogToOriginY","None"),
                ("CogToOriginY","X01"),

                ("CogToOriginXY","None"),
                ("CogToOriginXY","X01"),
                ("CogToOriginXY","Y01"),

                ("BottomLeftToOrigin","None"),
                ("BottomLeftToOrigin","X01"),
                ("BottomLeftToOrigin","Y01"),
            };
            var ls = es.SelectMany(e => l.ConvertAll(c => new BenchmarkConfig(c)
            {
                TranslationScaling = e
            })).ToList();
            l.AddRange(ls);
            return l;
        }

        private static List<BenchmarkConfig> ResamplingTypes(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.ResamplingType = "None");
            List<string> es = new List<string>() { "TimeSlot", "SampleCount", "FillPenUp" };
            var ls = es.SelectMany(e => l.ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType = e
            })).ToList();
            l.AddRange(ls);
            return l;
        }

        private static List<BenchmarkConfig> Interpolations(List<BenchmarkConfig> l)
        {
            //csak ott kell interpolaciot allitani, ahol van resampling
            l.Where(c => c.ResamplingType != "None").ToList().ForEach(c => c.Interpolation = "Linear");
            List<string> es = new List<string>() { "Cubic" };
            var ls = es.SelectMany(e => l.Where(c => c.ResamplingType != "None").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                Interpolation = e
            })).ToList();
            l.AddRange(ls);
            return l;
        }

        private static List<BenchmarkConfig> SetFeatures(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Features = "XYPAzimuthAltitude");
            List<string> es = new List<string>() { "X", "Y", "P", "Azimuth", "Altitude", "XY", "XYP" };
            var ls = es.SelectMany(e => l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Features = e
            })).ToList();
            l.AddRange(ls);
            return l;
        }

        public BenchmarkConfig() { }
        public BenchmarkConfig(BenchmarkConfig c)
        {
            Sampling = c.Sampling;
            Database = c.Database;
            Filter = c.Filter;
            Rotation = c.Rotation;
            TranslationScaling = c.TranslationScaling;
            ResamplingType = c.ResamplingType;
            Interpolation = c.Interpolation;
            Features = c.Features;
        }

        public string Sampling { get; set; }
        public string Database { get; set; }
        public string Filter { get; set; }
        public bool Rotation { get; set; }
        public string ResamplingType { get; set; }
        public string Interpolation { get; set; }
        public string Features { get; set; }
        public (string Translation,string Scaling) TranslationScaling { get; set; }

    }
}
