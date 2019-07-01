using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.Common.Pipeline;
using SigStat.Common.Model;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Helpers;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;
using System.IO;
using SigStat.Benchmark.Model;
using System.Linq;

namespace SigStat.Benchmark
{
    class BenchmarkGenerator
    {
        static CloudBlobContainer Container;
        static CloudQueue Queue;
        static DirectoryInfo OutputDirectory;

        public static List<IClassifier> Classifiers = new List<IClassifier>();
        public static List<DatabaseConfiguration> Databases = new List<DatabaseConfiguration>();
        public static List<List<ITransformation>> TransformationGroups = new List<List<ITransformation>>();
        public static List<List<FeatureDescriptor>> FeatureGroups = new List<List<FeatureDescriptor>>();

        public static List<VerifierBenchmark> Benchmarks = new List<VerifierBenchmark>();

        static string DatabasePath = "/home/1/sigstat/databases/";
        //convention: dutch.zip, MCYT100.zip, SVC2004.zip

        public static void Compose()
        {
            AddClassifier(new OptimalDtwClassifier());
            AddDatabase(new Svc2004Loader(Path.Combine(DatabasePath, "SVC2004.zip"), true), new SVC2004Sampler1());
            AddTransformationGroup(
                new NormalizeRotation() { InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY = Features.Y },

                new Scale() { InputFeature = Features.X, OutputFeature = Features.X },
                new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y },
                new FillPenUpDurations()
                {
                    InputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                    OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                    InterpolationType = typeof(CubicInterpolation),
                    TimeInputFeature = Features.T,
                    TimeOutputFeature = Features.T
                }
            );
            AddTransformationGroup(
                new Scale() { InputFeature = Features.X, OutputFeature = Features.X },
                new FillPenUpDurations()
                {
                    InputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                    OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                    InterpolationType = typeof(CubicInterpolation),
                    TimeInputFeature = Features.T,
                    TimeOutputFeature = Features.T
                }
            );
            AddTransformationGroup(
                new Scale() { InputFeature = Features.X, OutputFeature = Features.X }
            );
            AddTransformationGroup(
                new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y }
            );
            AddTransformationGroup(
                new Scale() { InputFeature = Features.X, OutputFeature = Features.X },
                new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y }
            );
            AddFeatureGroup(Features.X, Features.Y, Features.Pressure);
            AddFeatureGroup(Features.X, Features.Y);
            AddFeatureGroup(Features.Pressure);
            AddFeatureGroup(Features.X, Features.Pressure);
            AddFeatureGroup(Features.Y, Features.Pressure);
            AddFeatureGroup(Features.Pressure);
        }

        public static void AddClassifier(IClassifier classifier)
        {
            Classifiers.Add(classifier);
        }

        public static void AddDatabase(DataSetLoader datasetloader, Sampler sampler)
        {
            Databases.Add(new DatabaseConfiguration() { DataSetLoader = datasetloader, Sampler = sampler });
        }

        public static void AddTransformationGroup(params ITransformation[] transformations)
        {
            TransformationGroups.Add(new List<ITransformation>(transformations));
        }

        public static void AddFeatureGroup(params FeatureDescriptor[] featuredescriptors)
        {
            FeatureGroups.Add(new List<FeatureDescriptor>(featuredescriptors));
        }

        public static void GenerateBenchmarks()
        {
            foreach (var database in Databases)
            {
                foreach (var classifier in Classifiers)
                {
                    foreach (var transformationgroup in TransformationGroups)
                    {
                        foreach (var featuregroup in FeatureGroups)
                        {
                            var benchmark = new VerifierBenchmark()
                            {
                                Loader = database.DataSetLoader,
                                Verifier = new Verifier()
                                {
                                    Pipeline = new SequentialTransformPipeline()
                                    {
                                        Items = transformationgroup
                                    },
                                    Classifier = InitClassifier(classifier, featuregroup, database.Sampler)

                                },
                                Sampler = database.Sampler,
                                Logger = new SimpleConsoleLogger()
                            };

                            var trX = featuregroup.OfType<TranslatePreproc>().SingleOrDefault(t => t.InputFeature == Features.X);
                            var trY = featuregroup.OfType<TranslatePreproc>().SingleOrDefault(t => t.InputFeature == Features.Y);
                            var scX = featuregroup.OfType<Scale>().SingleOrDefault(s => s.InputFeature == Features.X);
                            var scY = featuregroup.OfType<Scale>().SingleOrDefault(s => s.InputFeature == Features.X);
                            var ucX = featuregroup.OfType<UniformScale>().SingleOrDefault(s => s.BaseDimension == Features.X);
                            var ucY = featuregroup.OfType<UniformScale>().SingleOrDefault(s => s.BaseDimension == Features.Y);


                            string tr = "None";
                            if (trX != null && trX.GoalOrigin == OriginType.CenterOfGravity && trY != null && trY.GoalOrigin == OriginType.CenterOfGravity) tr = "CogToOriginXY";
                            else if (trX != null && trX.GoalOrigin == OriginType.CenterOfGravity && trY == null) tr = "CogToOriginX";
                            else if (trY != null && trY.GoalOrigin == OriginType.CenterOfGravity && trX == null) tr = "CogToOriginY";
                            else if (trX != null && trX.GoalOrigin == OriginType.Minimum && trY != null && trY.GoalOrigin == OriginType.Minimum) tr = "BottomLeftToOrigin";

                            string sc = "None";
                            if (ucX != null) sc = "X01Y0prop";
                            else if (ucY != null) sc = "Y01X0prop";
                            else if (scX != null && scY != null) sc = "X01Y01";
                            else if (scX != null) sc = "X01";
                            else if (scY != null) sc = "Y01";
                            string translationScaling = $"({tr}, {sc})";
                            /*
(CogToOriginXY, None)
(CogToOriginX, None)
(CogToOriginY, None)
(BottomLeftToOrigin, None)
(None, None)

(CogToOriginXY, Y01)
(CogToOriginX, Y01)
(BottomLeftToOrigin, Y01)
(None, Y01)
(None, X01)
(CogToOriginXY, X01)
(CogToOriginY, X01)
(BottomLeftToOrigin, X01)
(None, X01Y01)
(None, Y01X0prop)
(None, X01Y0prop)
                             */


                            benchmark.Parameters = new List<KeyValuePair<string, string>>
                            {//TODO: ezek mik legyenek
                                new KeyValuePair<string, string>("Classifier", classifier.GetType().Name.Replace("Classifier","")),
                                new KeyValuePair<string, string>("Sampling", "S"+database.Sampler.GetType().Name.Last()),
                                new KeyValuePair<string, string>("Database", database.DataSetLoader.GetType().Name.Replace("Loader","")),
                                new KeyValuePair<string, string>("Rotation",  transformationgroup.Any(tg=>tg is NormalizeRotation).ToString()),
                                new KeyValuePair<string, string>("Translation_Scaling",  translationScaling),
                                new KeyValuePair<string, string>("ResamplingType_Filter",  transformationgroup.OfType<FillPenUpDurations>().Any() ? "FillPenUp" : transformationgroup.OfType<ResampleSamplesCountBased>().Any()?"SampleCount":transformationgroup.OfType<FilterPoints>().Any()?"P":"none"),
                                new KeyValuePair<string, string>("ResamplingParam", (transformationgroup.OfType<ResampleSamplesCountBased>().Select(s=>(int?)s.NumOfSamples).SingleOrDefault()?? 0).ToString() ),
                                new KeyValuePair<string, string>("Interpolation",   (transformationgroup.OfType<FillPenUpDurations>().SingleOrDefault()?.InterpolationType.Name ?? transformationgroup.OfType<ResampleSamplesCountBased>().SingleOrDefault()?.InterpolationType.Name ?? "").Replace("Interpolation", "")),
                                new KeyValuePair<string, string>("Features",  featuregroup.Count==1? featuregroup[0].Key: featuregroup.Count==2? "XY": featuregroup.Count ==3?"XYP":"XYPAzimuthAltitude"),
                                new KeyValuePair<string, string>("Distance",  (classifier as IDistanceClassifier).DistanceFunction  == Accord.Math.Distance.Manhattan ?"Manhattan": "Euclidean" ),
                                new KeyValuePair<string, string>("Transforms", string.Join( ",", transformationgroup.Select(t=>t.GetType().Name))),
                                new KeyValuePair<string, string>("Features", string.Join( ",", featuregroup.Select(f=>f.Key))),
                            };
                            Benchmarks.Add(benchmark);
                        }
                    }
                }
            }
        }

        private static IClassifier InitClassifier(IClassifier classifier, List<FeatureDescriptor> features, Sampler sampler)
        {
            if (classifier is DtwClassifier)
            {
                var c = classifier as DtwClassifier;
                c.Features = features;
                return c;
            }
            if (classifier is OptimalDtwClassifier)
            {
                var c = classifier as OptimalDtwClassifier;
                c.Features = features;
                c.Sampler = sampler;
                return c;
            }
            if (classifier is WeightedClassifier)
            {
                return classifier;
            }
            return new DtwClassifier();
        }

        internal static async Task RunAsync(string outputDir, string databasePath)
        {
            OutputDirectory = Directory.CreateDirectory(outputDir);
            DatabasePath = databasePath;

            BenchmarkGenerator.Compose();

            Console.WriteLine("Generating benchmark configurations...");
            BenchmarkGenerator.GenerateBenchmarks();

            await SaveResults();
        }

        internal static async Task SaveResults()
        {
            try
            {
                if (Program.Offline)
                {
                    //clear previously leftover configs
                    foreach (var f in OutputDirectory.EnumerateFiles())
                        f.Delete();

                    //save locally
                    Console.WriteLine($"Writing {Benchmarks.Count} combinations to disk...");
                    for (int i = 0; i < Benchmarks.Count; i++)
                    {
                        var filename = $"{i}.json";
                        var fullfilename = Path.Combine(OutputDirectory.ToString(), filename);
                        SerializationHelper.JsonSerializeToFile<VerifierBenchmark>(Benchmarks[i], fullfilename);
                    }
                }
                else
                {
                    Console.WriteLine("Initializing container: " + Program.Experiment);
                    var blobClient = Program.Account.CreateCloudBlobClient();
                    Container = blobClient.GetContainerReference(Program.Experiment);
                    await Container.CreateIfNotExistsAsync();

                    Console.WriteLine("Initializing queue: " + Program.Experiment);
                    var queueClient = Program.Account.CreateCloudQueueClient();
                    Queue = queueClient.GetQueueReference(Program.Experiment);
                    await Queue.CreateIfNotExistsAsync();

                    await Queue.FetchAttributesAsync();
                    if ((Queue.ApproximateMessageCount ?? 0) > 0)
                    {
                        if (!Console.IsInputRedirected)
                        {
                            Console.WriteLine($"There are {Queue.ApproximateMessageCount} jobs pending in the queue. Should I clear them? [Y|N]");
                            if (Console.ReadKey(true).Key != ConsoleKey.Y)
                            {
                                await Queue.ClearAsync();
                            }
                        }
                        else
                            await Queue.ClearAsync();//clear queue by default
                    }

                    Console.WriteLine($"Enqueueing {Benchmarks.Count} combinations...");

                    for (int i = 0; i < Benchmarks.Count; i++)
                    {
                        if (i % 100 == 0)
                            Console.WriteLine($"{i}/{Benchmarks.Count}");

                        var filename = $"Benchmark_{i}.json";
                        var fullfilename = Path.Combine(OutputDirectory.ToString(), filename);

                        var blob = Container.GetBlockBlobReference($"Benchmarks/{filename}");
                        await blob.DeleteIfExistsAsync();
                        await blob.UploadFromFileAsync(fullfilename);

                        await Queue.AddMessageAsync(new CloudQueueMessage(SerializationHelper.JsonSerialize<VerifierBenchmark>(Benchmarks[i])));

                    }
                }
                Console.WriteLine($"Ready.");
            }
            catch (Exception e)
            {
                File.WriteAllText("log.txt", e.ToString());
                Console.WriteLine("Something went wrong.\r\n" + e);
                return;
            }
        }
    }
}
