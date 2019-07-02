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

        public static IEnumerable<VerifierBenchmark> EnumerateBenchmarks()
        {
            foreach (var config in BenchmarkConfig.GenerateConfigurations())
            {
                yield return BenchmarkBuilder.Build(config, DatabasePath);

            }
        }


        static string DatabasePath;
        //convention: SigComp11_Dutch.zip.zip, MCYT100.zip, SVC2004.zip, SigWiComp2013_Japanese.zip, SigWiComp2015_German.zip

        #region proba
        //        public static void Compose()
        //        {
        //            AddClassifier(new OptimalDtwClassifier(Accord.Math.Distance.Euclidean));
        //            AddClassifier(new OptimalDtwClassifier(Accord.Math.Distance.Manhattan));
        //            AddClassifier(new DtwClassifier(Accord.Math.Distance.Euclidean));
        //            AddClassifier(new DtwClassifier(Accord.Math.Distance.Manhattan));

        //            AddDatabase(new SigComp11DutchLoader(Path.Combine(DatabasePath, "dutch.zip"), true), new DutchSampler1());
        //            AddDatabase(new SigComp11DutchLoader(Path.Combine(DatabasePath, "dutch.zip"), true), new DutchSampler2());
        //            AddDatabase(new SigComp11DutchLoader(Path.Combine(DatabasePath, "dutch.zip"), true), new DutchSampler3());
        //            AddDatabase(new SigComp11DutchLoader(Path.Combine(DatabasePath, "dutch.zip"), true), new DutchSampler4());

        //            AddFeatureGroup(Features.X);
        //            AddFeatureGroup(Features.Y);
        //            AddFeatureGroup(Features.Pressure);
        //            AddFeatureGroup(Features.X, Features.Y);
        //            AddFeatureGroup(Features.Y, Features.Y, Features.Pressure);


        //            AddTransformationGroup(null,
        //                new NormalizeRotation() { InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY = Features.Y });

        //            AddTransformationGroup(null,
        //                new FillPenUpDurations() { InterpolationType = typeof(CubicInterpolation), TimeInputFeature = Features.T, TimeOutputFeature = Features.T },
        //                new FillPenUpDurations() { InterpolationType = typeof(LinearInterpolation), TimeInputFeature = Features.T, TimeOutputFeature = Features.T },
        //                new ResampleSamplesCountBased() { NumOfSamples = 50, InterpolationType = typeof(CubicInterpolation), OriginalTFeature = Features.T, ResampledTFeature = Features.T },
        //                new ResampleSamplesCountBased() { NumOfSamples = 100, InterpolationType = typeof(CubicInterpolation), OriginalTFeature = Features.T, ResampledTFeature = Features.T },
        //                new ResampleSamplesCountBased() { NumOfSamples = 500, InterpolationType = typeof(CubicInterpolation), OriginalTFeature = Features.T, ResampledTFeature = Features.T },
        //                new ResampleSamplesCountBased() { NumOfSamples = 1000, InterpolationType = typeof(CubicInterpolation), OriginalTFeature = Features.T, ResampledTFeature = Features.T },
        //                new ResampleSamplesCountBased() { NumOfSamples = 50, InterpolationType = typeof(LinearInterpolation), OriginalTFeature = Features.T, ResampledTFeature = Features.T },
        //                new ResampleSamplesCountBased() { NumOfSamples = 100, InterpolationType = typeof(LinearInterpolation), OriginalTFeature = Features.T, ResampledTFeature = Features.T },
        //                new ResampleSamplesCountBased() { NumOfSamples = 500, InterpolationType = typeof(LinearInterpolation), OriginalTFeature = Features.T, ResampledTFeature = Features.T },
        //                new ResampleSamplesCountBased() { NumOfSamples = 1000, InterpolationType = typeof(LinearInterpolation), OriginalTFeature = Features.T, ResampledTFeature = Features.T },
        //                new FilterPoints() { KeyFeatureInput = Features.Pressure, KeyFeatureOutput = Features.Pressure }
        //                );

        //            AddTransformationGroup(
        //                // (None, None)
        //                null,
        //                // (CogToOriginXY, None)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X }, new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y } },
        //                // (CogToOriginX, None)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X } },
        //                // (CogToOriginY, None)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y } },
        //                // (BottomLeftToOrigin, None)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.X }, new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.Y } },
        //                //(CogToOriginXY, Y01)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X }, new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y } },
        //                //(CogToOriginX, Y01)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X }, new Scale() { InputFeature = Features.Y, NewMinValue = 0, NewMaxValue = 0, OutputFeature = Features.Y }},
        //                //(BottomLeftToOrigin, Y01)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.X }, new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.Y }, new Scale() { InputFeature = Features.Y, NewMinValue = 0, NewMaxValue = 0, OutputFeature = Features.Y } },
        //                // (None, Y01)
        //                new SequentialTransformPipeline() { new Scale() { InputFeature = Features.Y, NewMinValue = 0, NewMaxValue = 0, OutputFeature = Features.Y } },
        //                // (None, X01)
        //                new SequentialTransformPipeline() { new Scale() { InputFeature = Features.X, NewMinValue = 0, NewMaxValue = 0, OutputFeature = Features.X } },
        //                // (CogToOriginXY, X01)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X }, new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y }, new Scale() { InputFeature = Features.X, NewMinValue = 0, NewMaxValue = 0, OutputFeature = Features.X } },
        //                // (CogToOriginY, X01)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y }, new Scale() { InputFeature = Features.X, NewMinValue = 0, NewMaxValue = 0, OutputFeature = Features.X } },
        //                // (BottomLeftToOrigin, X01)
        //                new SequentialTransformPipeline() { new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.X }, new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.Y }, new Scale() { InputFeature = Features.X, NewMinValue = 0, NewMaxValue = 0, OutputFeature = Features.X } },
        //                // (None, X01Y01)
        //                new SequentialTransformPipeline() { new Scale() { InputFeature = Features.X, NewMinValue = 0, NewMaxValue = 0, OutputFeature = Features.X }, new Scale() { InputFeature = Features.Y, NewMinValue = 0, NewMaxValue = 0, OutputFeature = Features.Y } },
        //                // (None, Y01X0prop)
        //                new SequentialTransformPipeline() { new UniformScale() { BaseDimension = Features.X, ProportionalDimension = Features.Y, NewMinBaseValue = 0, NewMaxBaseValue = 0, NewMinProportionalValue = 0, BaseDimensionOutput = Features.X, ProportionalDimensionOutput = Features.Y }},
        //                // (None, X01Y0prop)
        //                new SequentialTransformPipeline() { new UniformScale() { BaseDimension = Features.Y, ProportionalDimension = Features.X, NewMinBaseValue = 0, NewMaxBaseValue = 0, NewMinProportionalValue = 0, BaseDimensionOutput = Features.Y, ProportionalDimensionOutput = Features.X }}

        //                );
        //        }

        //        public static void AddClassifier(IClassifier classifier)
        //        {
        //            Classifiers.Add(classifier);
        //        }

        //        public static void AddDatabase(DataSetLoader datasetloader, Sampler sampler)
        //        {
        //            Databases.Add(new DatabaseConfiguration() { DataSetLoader = datasetloader, Sampler = sampler });
        //        }

        //        public static void AddTransformationGroup(params ITransformation[] transformations)
        //        {
        //            TransformationGroups.Add(new List<ITransformation>(transformations));
        //        }

        //        public static void AddFeatureGroup(params FeatureDescriptor[] featuredescriptors)
        //        {
        //            FeatureGroups.Add(new List<FeatureDescriptor>(featuredescriptors));
        //        }

        //        public static void GenerateBenchmarks()
        //        {


        //            //var pipelines = new List<SequentialTransformPipeline>();
        //            //foreach (var transformationgroup in TransformationGroups)
        //            //{
        //            //    foreach (var transformation in transformationgroup)
        //            //    {

        //            //    }
        //            //}


        //            //        foreach (var database in Databases)
        //            //{
        //            //    foreach (var classifier in Classifiers)
        //            //    {
        //            //        foreach (var transformationgroup in TransformationGroups)
        //            //        {
        //            //            foreach (var transformation in transformationgroup)
        //            //            {
        //            //                foreach (var featuregroup in FeatureGroups)
        //            //                {
        //            //                    var benchmark = new VerifierBenchmark()
        //            //                    {
        //            //                        Loader = database.DataSetLoader,
        //            //                        Verifier = new Verifier()
        //            //                        {
        //            //                            Pipeline = new SequentialTransformPipeline()
        //            //                            {
        //            //                                Items = transformationgroup
        //            //                            },
        //            //                            Classifier = InitClassifier(classifier, featuregroup, database.Sampler)

        //            //                        },
        //            //                        Sampler = database.Sampler,
        //            //                        Logger = new SimpleConsoleLogger()
        //            //                    };

        //            //                    var trX = transformationgroup.OfType<TranslatePreproc>().SingleOrDefault(t => t.InputFeature == Features.X);
        //            //                    var trY = transformationgroup.OfType<TranslatePreproc>().SingleOrDefault(t => t.InputFeature == Features.Y);
        //            //                    var scX = transformationgroup.OfType<Scale>().SingleOrDefault(s => s.InputFeature == Features.X);
        //            //                    var scY = transformationgroup.OfType<Scale>().SingleOrDefault(s => s.InputFeature == Features.Y);
        //            //                    var ucX = transformationgroup.OfType<UniformScale>().SingleOrDefault(s => s.BaseDimension == Features.X);
        //            //                    var ucY = transformationgroup.OfType<UniformScale>().SingleOrDefault(s => s.BaseDimension == Features.Y);


        //            //                    string tr = "None";
        //            //                    if (trX != null && trX.GoalOrigin == OriginType.CenterOfGravity && trY != null && trY.GoalOrigin == OriginType.CenterOfGravity) tr = "CogToOriginXY";
        //            //                    else if (trX != null && trX.GoalOrigin == OriginType.CenterOfGravity && trY == null) tr = "CogToOriginX";
        //            //                    else if (trY != null && trY.GoalOrigin == OriginType.CenterOfGravity && trX == null) tr = "CogToOriginY";
        //            //                    else if (trX != null && trX.GoalOrigin == OriginType.Minimum && trY != null && trY.GoalOrigin == OriginType.Minimum) tr = "BottomLeftToOrigin";

        //            //                    string sc = "None";
        //            //                    if (ucX != null) sc = "X01Y0prop";
        //            //                    else if (ucY != null) sc = "Y01X0prop";
        //            //                    else if (scX != null && scY != null) sc = "X01Y01";
        //            //                    else if (scX != null) sc = "X01";
        //            //                    else if (scY != null) sc = "Y01";
        //            //                    string translationScaling = $"({tr}, {sc})";


        //            //                    benchmark.Parameters = new List<KeyValuePair<string, string>>
        //            //                {//TODO: ezek mik legyenek
        //            //                    new KeyValuePair<string, string>("Classifier", classifier.GetType().Name.Replace("Classifier","")),
        //            //                    new KeyValuePair<string, string>("Sampling", "S"+database.Sampler.GetType().Name.Last()),
        //            //                    new KeyValuePair<string, string>("Database", database.DataSetLoader.GetType().Name.Replace("Loader","").ToUpperInvariant()),
        //            //                    new KeyValuePair<string, string>("Rotation",  transformationgroup.Any(tg=>tg is NormalizeRotation).ToString()),
        //            //                    new KeyValuePair<string, string>("Translation_Scaling",  translationScaling),
        //            //                    new KeyValuePair<string, string>("ResamplingType_Filter",  transformationgroup.OfType<FillPenUpDurations>().Any() ? "FillPenUp" : transformationgroup.OfType<ResampleSamplesCountBased>().Any()?"SampleCount":transformationgroup.OfType<FilterPoints>().Any()?"P":"none"),
        //            //                    new KeyValuePair<string, string>("ResamplingParam", (transformationgroup.OfType<ResampleSamplesCountBased>().Select(s=>(int?)s.NumOfSamples).SingleOrDefault()?? 0).ToString() ),
        //            //                    new KeyValuePair<string, string>("Interpolation",   (transformationgroup.OfType<FillPenUpDurations>().SingleOrDefault()?.InterpolationType.Name ?? transformationgroup.OfType<ResampleSamplesCountBased>().SingleOrDefault()?.InterpolationType.Name ?? "").Replace("Interpolation", "")),
        //            //                    new KeyValuePair<string, string>("Features",  featuregroup.Count==1? featuregroup[0].Key: featuregroup.Count==2? "XY": featuregroup.Count ==3?"XYP":"XYPAzimuthAltitude"),
        //            //                    new KeyValuePair<string, string>("Distance",  (classifier as IDistanceClassifier).DistanceFunction  == Accord.Math.Distance.Manhattan ?"Manhattan": "Euclidean" ),
        //            //                    new KeyValuePair<string, string>("Transforms", string.Join( ",", transformationgroup.Select(t=>t.GetType().Name))),
        //            //                    new KeyValuePair<string, string>("Features", string.Join( ",", featuregroup.Select(f=>f.Key))),
        //            //                };
        //            //                    Benchmarks.Add(benchmark);
        //            //                }

        //            //            }
        //            //        }
        //            //    }
        //            //}
        //        }

        //        /*
        //(CogToOriginXY, None)
        //(CogToOriginX, None)
        //(CogToOriginY, None)
        //(BottomLeftToOrigin, None)
        //(None, None)

        //(CogToOriginXY, Y01)
        //(CogToOriginX, Y01)
        //(BottomLeftToOrigin, Y01)
        //(None, Y01)
        //(None, X01)
        //(CogToOriginXY, X01)
        //(CogToOriginY, X01)
        //(BottomLeftToOrigin, X01)
        //(None, X01Y01)
        //(None, Y01X0prop)
        //(None, X01Y0prop)
        //         */


        //        private static IClassifier InitClassifier(IClassifier classifier, List<FeatureDescriptor> features, Sampler sampler)
        //        {
        //            if (classifier is DtwClassifier)
        //            {
        //                var c = classifier as DtwClassifier;
        //                c.Features = features;
        //                return c;
        //            }
        //            if (classifier is OptimalDtwClassifier)
        //            {
        //                var c = classifier as OptimalDtwClassifier;
        //                c.Features = features;
        //                c.Sampler = sampler;
        //                return c;
        //            }
        //            if (classifier is WeightedClassifier)
        //            {
        //                return classifier;
        //            }
        //            return new DtwClassifier();
        //        }
        #endregion

        internal static async Task RunAsync(string outputDir, string databasePath)
        {
            OutputDirectory = Directory.CreateDirectory(outputDir);

            DatabasePath = databasePath?? Environment.GetEnvironmentVariable("SigStatDB");

            //BenchmarkGenerator.Compose();

            Console.WriteLine("Generating benchmark configurations...");
            //BenchmarkGenerator.GenerateBenchmarks();

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
                    Console.WriteLine("Enumerating combinations");
                    var count = EnumerateBenchmarks().Count();
                    Console.WriteLine($"Writing {count} combinations to disk...");
                    int i = 0;
                    foreach (var benchmark in EnumerateBenchmarks())
                    {
                        var filename = $"{i}.json";
                        var fullfilename = Path.Combine(OutputDirectory.ToString(), filename);
                        SerializationHelper.JsonSerializeToFile<VerifierBenchmark>(benchmark, fullfilename);
                        i++;
                        if (i%100==0)
                            Console.WriteLine($"{i}/{count}");

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
