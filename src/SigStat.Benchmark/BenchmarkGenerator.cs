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
            AddFeatureGroup(Features.X, Features.Y, Features.Pressure);
            AddFeatureGroup(Features.X, Features.Y);
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
