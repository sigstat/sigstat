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
        // 7560 = 3*3*5*6*2*7*2
        // Sampling: 3
        // DB: 3
        // Translation: 5
        // U/Scaling: 6
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
            //l = Samplers(Databases(Filters(Rotations(Translations(Scalings(Interpolations(ResamplingTypes(l))))))));
            l = Samplers(Databases(Filters(Rotations(Translations(Scalings(Interpolations(ResamplingTypes(l))))))));
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

        private static List<BenchmarkConfig> Translations(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Translation = "None");
            List<string> es = new List<string>() { "CogToOriginX", "CogToOriginY", "CogToOriginXY", "BottomLeftToOrigin" };
            var ls = es.SelectMany(e => l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Translation = e
            })).ToList();
            l.AddRange(ls);
            return l;
        }

        private static List<BenchmarkConfig> Scalings(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Scaling = "None");
            List<string> es = new List<string>() { "X01", "Y01", "X01Y01", "X01Y0prop", "Y01X0prop" };
            var ls = es.SelectMany(e => l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Scaling = e
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

        public BenchmarkConfig() { }
        public BenchmarkConfig(BenchmarkConfig c)
        {
            Sampling = c.Sampling;
            Database = c.Database;
            Filter = c.Filter;
            Rotation = c.Rotation;
            Translation = c.Translation;
            Scaling = c.Scaling;
            ResamplingType = c.ResamplingType;
            Interpolation = c.Interpolation;
        }

        public string Sampling { get; set; }
        public string Database { get; set; }
        public string Filter { get; set; }
        public bool Rotation { get; set; }
        public string Translation { get; set; }
        public string Scaling { get; set; }
        public string ResamplingType { get; set; }
        public string Interpolation { get; set; }

    }
}
