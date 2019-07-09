using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SigStat.Common.Helpers
{
    //TODO: refactor
    public class BenchmarkConfig
    {
        // Phase2:
        // - Több jellemzőkombináció
        // - 

        public static BenchmarkConfig FromJsonString(string jsonString)
        {
            return JsonConvert.DeserializeObject<BenchmarkConfig>(jsonString);
        }

        public string ToShortString()
        {
            return string.Join("_", GetType().GetProperties().Select(pi => pi.GetValue(this)).Select(v => v?.ToString() ?? ""));
        }

        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            return new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("Translation", this.Translation_Scaling.Translation), new KeyValuePair<string, string>("Scaling", this.Translation_Scaling.Scaling) }
                    .Concat(GetType().GetProperties().Select(pi => new KeyValuePair<string, string>(pi.Name, pi.GetValue(this)?.ToString() ?? "")));
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
            l = Classifiers(Samplers(Interpolations(ResamplingTypes_Filters(SetFeatures(Databases(Rotations(Translations_Scalings(Distances(l)))))))));
            return l;
        }

        private static List<BenchmarkConfig> Samplers(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Sampling = "S1");

            var multipleSamplerConfigs = l.Where(c => c.Database != "GERMAN").ToList();

            var l2 = multipleSamplerConfigs.ConvertAll(c => new BenchmarkConfig(c)
            {
                Sampling = "S2"
            });
            var l3 = multipleSamplerConfigs.ConvertAll(c => new BenchmarkConfig(c)
            {
                Sampling = "S3"
            });
            var l4 = multipleSamplerConfigs.ConvertAll(c => new BenchmarkConfig(c)
            {
                Sampling = "S4"
            });
            l.AddRange(l2);
            l.AddRange(l3);
            l.AddRange(l4);

            return l;
        }

        private static List<BenchmarkConfig> Databases(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Database = "GERMAN");
            //List<string> es = new List<string>() { "MCYT100", "DUTCH", "CHINESE", "JAPANESE", "SVC2004" };
            //var ls = es.SelectMany(e => l.ConvertAll(c => new BenchmarkConfig(c)
            //{
            //    Database = e
            //})).ToList();
            //l.AddRange(ls);
            return l;
        }

        private static List<BenchmarkConfig> Classifiers(List<BenchmarkConfig> l)
        {

            l.ForEach(c => c.Classifier = "OptimalDtw");
            var l2 = l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Classifier = "Dtw"
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

        private static List<BenchmarkConfig> Distances(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Distance = "Manhattan");
            var l2 = l.ConvertAll(c => new BenchmarkConfig(c)
            {
                Distance = "Euclidean"
            });
            l.AddRange(l2);
            return l;
        }

        private static List<BenchmarkConfig> Translations_Scalings(List<BenchmarkConfig> l)
        {
            //TODO: Add X01Y01 ==> CogToOrigin

            //jobb kezzel megadni az ertelmes parokat: 16 db van, osszes 30 helyett
            l.ForEach(c => c.Translation_Scaling = ("None", "None"));
            List<(string, string)> es = new List<(string, string)>() {
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
            //dutch: None, SampleCount, Filter

            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "SampleCount",
                ResamplingParam = 50
            }));
            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "SampleCount",
                ResamplingParam = 100
            }));
            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "SampleCount",
                ResamplingParam = 500
            }));

            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "SampleCount",
                ResamplingParam = 1000
            }));


            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None" && c.Database == "SVC2004").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "FillPenUp"
            }));

            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None" && c.Database == "MCYT100").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "P"
            }));
            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None" && c.Database == "MCYT100").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "P_FillPenUp"
            }));

            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None" && c.Database == "DUTCH").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "P"
            }));
            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None" && c.Database == "DUTCH").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "P_FillPenUp"
            }));

            //ez helyes?
            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None" && c.Database == "GERMAN").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "P"
            }));
            l.AddRange(l.Where(c => c.ResamplingType_Filter == "None" && c.Database == "GERMAN").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                ResamplingType_Filter = "P_FillPenUp"
            }));

            return l;
        }

        private static List<BenchmarkConfig> Interpolations(List<BenchmarkConfig> l)
        {
            //csak ott kell interpolaciot allitani, ahol van resampling
            l.Where(c => c.ResamplingType_Filter == "SampleCount" || c.ResamplingType_Filter == "FillPenUp" || c.ResamplingType_Filter == "P_FillPenUp").ToList().ForEach(c => c.Interpolation = "Linear");
            List<string> es = new List<string>() { "Cubic" };
            var ls = es.SelectMany(e => l.Where(c => c.ResamplingType_Filter == "SampleCount" || c.ResamplingType_Filter == "FillPenUp" || c.ResamplingType_Filter == "P_FillPenUp").ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                Interpolation = e
            })).ToList();
            l.AddRange(ls);
            return l;
        }

        private static List<BenchmarkConfig> SetFeatures(List<BenchmarkConfig> l)
        {
            l.ForEach(c => c.Features = "XYP");

            var xypConfigs = l.Where(c => c.Database == "DUTCH" || c.Database == "GERMAN" || c.Database == "CHINESE");//ezeknek nincs azimuth, altitudeja (1.0 mind)

           List<string> es1 = new List<string>() { "X", "Y", "P", "XY", "Azimuth", "Altitude", "XYPAzimuthAltitude" };
            var ls1 = es1.SelectMany(e => l.Except(xypConfigs).ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                Features = e
            })).ToList();

            List<string> es2 = new List<string>() { "X", "Y", "P", "XY" };
            var ls2 = es2.SelectMany(e => xypConfigs.ToList().ConvertAll(c => new BenchmarkConfig(c)
            {
                Features = e
            })).ToList();


            l.AddRange(ls1);
            l.AddRange(ls2);



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
            Distance = c.Distance;
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
        public string Distance { get; set; }

    }
}
