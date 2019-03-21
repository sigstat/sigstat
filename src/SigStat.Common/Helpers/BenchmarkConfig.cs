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
        //most 1 signer samplerrel: 7168 

        // 21504 = 2*8*3*16*2*14
        // Classifiers: 2
        // Features: 8
        // Sampling: 3
        // Translation+Scaling: 16
        // Rotation: 2
        // DB+Resampling+Filter+Interpolation: 14

        //eredeti minta
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
            return string.Join("_", GetType().GetProperties().Select(pi => pi.GetValue(this)).Select(v=>v?.ToString() ?? ""));
        }

        public IEnumerable<KeyValuePair<string,string>> ToKeyValuePairs()
        {
            return GetType().GetProperties().Select(pi => new KeyValuePair<string, string>(pi.Name, pi.GetValue(this)?.ToString() ?? ""));
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
            l = Classifiers(Samplers(Interpolations(ResamplingTypes_Filters(Databases(Rotations(Translations_Scalings(SetFeatures(l))))))));
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
            l.ForEach(c => c.Database = "SVC2004");
            List<string> es = new List<string>() { "MCYT100" };
            var ls = es.SelectMany(e => l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Database = e
            })).ToList();
            l.AddRange(ls);
            return l;
        }

        private static List<BenchmarkConfig> Classifiers(List<BenchmarkConfig> l)
        {

            l.ForEach(c => c.Classifier = "OptimalDtw");
            //var l2 = l.ConvertAll(c => new BenchmarkConfig(c)
            //{
            //    Classifier = "Dtw"
            //});
            //l.AddRange(l2);
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

        private static List<BenchmarkConfig> Translations_Scalings(List<BenchmarkConfig> l)
        {
            //jobb kezzel megadni az ertelmes parokat: 16 db van, osszes 30 helyett
            l.ForEach(c => c.Translation_Scaling = ("None","None"));
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
                Translation_Scaling = e
            })).ToList();
            l.AddRange(ls);
            return l;
        }

        private static List<BenchmarkConfig> ResamplingTypes_Filters(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.ResamplingType_Filter = "None");

            //db tol fugg, hogy milyen resampling/filter kell
            //svc: None, SampleCount, FillPenUp
            //mcyt: None, SampleCount, Filter

            //downsample svc
            var ls = l.Where(c => c.Database == "SVC2004").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "SampleCount",
                ResamplingParam = 125
            });
            l.AddRange(ls);

            //upsample svc
            var ls2 = l.Where(c => c.Database == "SVC2004" && c.ResamplingType_Filter == "SampleCount").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingParam = 500
            });
            l.AddRange(ls2);

            //downsample mcyt
            var ls3 = l.Where(c => c.Database == "MCYT100").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "SampleCount",
                ResamplingParam = 50
            });
            l.AddRange(ls3);

            //upsample mcyt
            var ls4 = l.Where(c => c.Database == "MCYT100" && c.ResamplingType_Filter == "SampleCount").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingParam = 200
            });
            l.AddRange(ls4);

            var ls5 = l.Where(c => c.ResamplingType_Filter == "None" && c.Database == "SVC2004").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "FillPenUp"
            });
            l.AddRange(ls5);

            var ls6 = l.Where(c => c.ResamplingType_Filter == "None" && c.Database == "MCYT100").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "P"
            });
            l.AddRange(ls6);

            return l;
        }

        private static List<BenchmarkConfig> Interpolations(List<BenchmarkConfig> l)
        {
            //csak ott kell interpolaciot allitani, ahol van resampling
            l.Where(c => c.ResamplingType_Filter == "SampleCount" || c.ResamplingType_Filter == "FillPenUp").ToList().ForEach(c => c.Interpolation = "Linear");
            List<string> es = new List<string>() { "Cubic" };
            var ls = es.SelectMany(e => l.Where(c => c.ResamplingType_Filter == "SampleCount" || c.ResamplingType_Filter == "FillPenUp").ToList().ConvertAll(c => new BenchmarkConfig(c)
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
            Classifier = c.Classifier;
            Sampling = c.Sampling;
            Database = c.Database;
            Rotation = c.Rotation;
            Translation_Scaling = c.Translation_Scaling;
            ResamplingType_Filter = c.ResamplingType_Filter;
            ResamplingParam = c.ResamplingParam;
            Interpolation = c.Interpolation;
            Features = c.Features;
        }

        public string Classifier { get; set; }
        public string Sampling { get; set; }
        public string Database { get; set; }
        public bool Rotation { get; set; }
        public (string Translation, string Scaling) Translation_Scaling { get; set; }
        public string ResamplingType_Filter { get; set; }
        public double ResamplingParam { get; set; }
        public string Interpolation { get; set; }
        public string Features { get; set; }

    }
}
