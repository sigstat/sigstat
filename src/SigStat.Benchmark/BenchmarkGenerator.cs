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

namespace SigStat.Benchmark
{
    class BenchmarkGenerator
    {
        static CloudBlobContainer Container;
        static CloudQueue Queue;
        static DirectoryInfo OutputDirectory;

        public static List<IClassifier> Classifiers = new List<IClassifier>();
        public static List<Tuple<DataSetLoader, Sampler>> Databases = new List<Tuple<DataSetLoader, Sampler>>();
        public static List<List<ITransformation>> TransformationGroups = new List<List<ITransformation>>();
        public static List<List<FeatureDescriptor>> FeatureGroups = new List<List<FeatureDescriptor>>();

        public static List<VerifierBenchmark> Benchmarks = new List<VerifierBenchmark>();

        public static void Compose()
        {
            AddClassifier(new OptimalDtwClassifier());
            AddDatabase(new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true), new SVC2004Sampler1());
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
        }

        public static void AddClassifier(IClassifier classifier)
        {
            Classifiers.Add(classifier);
        }

        public static void AddDatabase(DataSetLoader datasetloader, Sampler sampler)
        {
            Databases.Add(new Tuple<DataSetLoader, Sampler>(datasetloader, sampler));
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
                                Loader = database.Item1,
                                Verifier = new Verifier()
                                {
                                    Pipeline = new SequentialTransformPipeline()
                                    {
                                        Items = transformationgroup
                                    },
                                    Classifier = new OptimalDtwClassifier()
                                    {
                                        Sampler = database.Item2,
                                        Features = featuregroup
                                    }

                                },
                                Sampler = database.Item2,
                                Logger = new SimpleConsoleLogger()
                            };

                            Benchmarks.Add(benchmark);
                        }
                    }
                }
            }
        }

        internal static async Task RunAsync(string outputDir)
        {
            OutputDirectory = Directory.CreateDirectory(outputDir);

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
                Console.WriteLine($"There are {Queue.ApproximateMessageCount} jobs pending in the queue. Should I clear them? [Y|N]");
                if (Console.ReadKey(true).Key != ConsoleKey.Y)
                {
                    Console.WriteLine("Aborting");
                    return;
                }
                await Queue.ClearAsync();
            }

            BenchmarkGenerator.Compose();

            Console.WriteLine("Generating benchmark configurations");
            BenchmarkGenerator.GenerateBenchmarks();

            Console.WriteLine($"Enqueueing {Benchmarks.Count} combinations");
            for (int i = 0; i < Benchmarks.Count; i++)
            {
                Console.WriteLine($"{i + 1}/{Benchmarks.Count}");
                SerializationHelper.JsonSerializeToFile<VerifierBenchmark>(Benchmarks[i], OutputDirectory + $@"\Benchmark_{i + 1}.json");
                await Queue.AddMessageAsync(new CloudQueueMessage(SerializationHelper.JsonSerialize<VerifierBenchmark>(Benchmarks[i])));
            }
            Console.WriteLine("Ready");
        }
    }
}
