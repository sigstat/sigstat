using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Markers;
using SigStat.Common.Transforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SigStat.Sample
{
    class Program
    {

        public static class MyFeatures
        {
            public static readonly FeatureDescriptor<List<Loop>> Loop = FeatureDescriptor.Get<List<Loop>>("Loop");
            public static FeatureDescriptor<bool[,]> Binarized = FeatureDescriptor.Get<bool[,]>("Binarized");
            public static FeatureDescriptor<bool[,]> Skeleton = FeatureDescriptor.Get<bool[,]>("Skeleton");
            public static FeatureDescriptor<List<double>> Tangent = FeatureDescriptor.Get<List<double>>("Tangent");
        }

        struct OnlinePoint { public int X; public int Y; public int Pressure; }

        class MySignature : Signature
        {
            public List<Loop> Loops { get { return GetFeature(MyFeatures.Loop); } set { SetFeature(MyFeatures.Loop, value); } }
            public RectangleF Bounds { get { return GetFeature(Features.Bounds); } set { SetFeature(Features.Bounds, value); } }

            public bool[,] Binarized { get { return GetFeature(MyFeatures.Binarized); } set { SetFeature(MyFeatures.Binarized, value); } }
            public List<double> Tangent { get { return GetFeature(MyFeatures.Tangent); } set { SetFeature(MyFeatures.Tangent, value); } }

        }

        public static void Main(string[] args)
        {
            Console.WriteLine("SigStat library sample");

            //SignatureDemo();
            TransformationPipeline();
            //OnlineToImage();
            //GenerateOfflineDatabase();
            //OfflineVerifierDemo();
            //OnlineVerifierDemo();
            //OnlineVerifierBenchmarkDemo();
            Console.WriteLine("Press <<Enter>> to exit.");
            Console.ReadLine();

        }

        public static void SignatureDemo()
        {
            // Create a signature instance and initialize main properties
            Signature sig = new Signature();
            sig.ID = "Demo";
            sig.Origin = Origin.Genuine;

            // Set/Get feature value, using a string key
            sig["Height"] = 5;
            var height = (int)sig["Height"];

            // Set/Get feature value, using a generic method and a sting key
            sig.SetFeature<int>("Height", 5);
            height = sig.GetFeature<int>("Height");

            // Register a feature descriptor
            FeatureDescriptor<int> heightDescriptor = FeatureDescriptor.Get<int>("Height");

            // Set/Get feature value, using a generic feature descriptor
            // Note that no casting is required!
            sig.SetFeature(heightDescriptor, 5);
            height = sig.GetFeature(heightDescriptor);

            // Define more complex feature values
            var seriesX = new List<double> { 1, 2, 1, 1, 2, 3, 4, 5 };
            var loops = new List<Loop>() { new Loop() { Center = new PointF(1, 1) }, new Loop() { Center = new PointF(3, 3) } };

            // Reusing feature descriptors (see MyFeatures class for details)
            sig.SetFeature(MyFeatures.Loop, loops);
            loops = sig.GetFeature(MyFeatures.Loop);

            // Wrap features into properties, see (MySignature class for details)
            MySignature mySig = new MySignature();
            mySig.Loops = loops;
            loops = mySig.Loops;

            // Enumerate all the features in a signature
            foreach (var descriptor in sig.GetFeatureDescriptors())
            {
                if (!descriptor.IsCollection)
                {
                    Console.WriteLine($"{descriptor.Name}: {sig[descriptor]}");
                }
                else
                {
                    Console.WriteLine($"{descriptor}:");
                    var items = (IList)sig[descriptor];
                    for (int i = 0; i < items.Count; i++)
                    {
                        Console.WriteLine($" {i}.) {items[i]}");
                    }
                }
            }

        }

        static void TransformationPipeline()
        {
            Signature signature = ImageLoader.LoadSignature(@"Databases\Offline\Images\004_e_001.png");
            
            // Initialize a transformation using object initializer
            var resize = new Resize()
            {
                Width = 40,
                InputFeatures = { Features.Image },
                OutputFeatures = { Features.Image }
            };
            // Perform transformation
            resize.Transform(signature);

            // Initialize a transformation using fluent syntax
            var binarization = new Binarization().Input(Features.Image).Output(MyFeatures.Binarized);

            // Perform transformation
            binarization.Transform(signature);

            // Consume results
            var binaryImage = signature.GetFeature(MyFeatures.Binarized);
            WriteToConsole(binaryImage);

        }

      

        /// <summary>
        /// Read signature from image, extract features, generate new image
        /// </summary>
        static void OfflineVerifierDemo()
        {

            Logger debugLogger = new Logger(
                LogLevel.Debug,
                new FileStream($@"Logs\OfflineDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create),
                LogConsole);

            var verifier = new Verifier()
            {
                Logger = debugLogger,
                TransformPipeline = new SequentialTransformPipeline
                {
                    new Binarization().Input(Features.Image),
                    new Trim(5),
                    new PrepareForThinning(),
                    new ImageGenerator(true),
                    new HSCPThinning(),
                    new ImageGenerator(true),
                    new OnePixelThinning().Output(MyFeatures.Skeleton),//output Skeletonba, mert az Extraction onnan szedi
                    new ImageGenerator(true),
                    //new BaselineExtraction(),
                    //new LoopExtraction(),
                    new EndpointExtraction(),
                    new ComponentExtraction(5),
                    new ComponentSorter(),
                    new ComponentsToFeatures(),
                    new ParallelTransformPipeline
                    {
                        new Normalize().Input(Features.X),
                        new Normalize().Input(Features.Y)
                    },
                    new ApproximateOnlineFeatures(),
                    new RealisticImageGenerator(1280, 720),
                },
                ClassifierPipeline = new DTWClassifier()
            };
            verifier.ProgressChanged += ProgressPrimary;

            Signature s1 = ImageLoader.LoadSignature(@"Databases\Offline\Images\U1S1.png");
            s1.Origin = Origin.Genuine;
            Signer s = new Signer();
            s.Signatures.Add(s1);

            verifier.Train(s);

            //TODO: ha mar Verifier demo, akkor Test()-et is hasznaljuk..
            ImageSaver.Save(s1, @"GeneratedOfflineImage.png");
            debugLogger.Stop();
        }

        static void OnlineVerifierDemo()
        {
            Logger debugLogger = new Logger(
                LogLevel.Debug,
                new FileStream($@"Logs\OnlineDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create),
                LogConsole);

            var timer1 = FeatureDescriptor.Get<DateTime>("Timer1");

            var verifier = new Verifier()
            {
                Logger = debugLogger,
                TransformPipeline = new SequentialTransformPipeline
                {
                    new TimeMarkerStart().Output(timer1),
                    new ParallelTransformPipeline
                    {
                        new Normalize().Input(Features.Pressure),
                        new Map(0, 1).Input(Features.X),
                        new Map(0, 1).Input(Features.Y),
                        new TimeReset(),
                    },
                    new CentroidTranslate(),//is a sequential pipeline of other building blocks
                    new TangentExtraction(),
                    /*new AlignmentNormalization(Alignment.Origin),
                    new Paper13FeatureExtractor(),*/
                    new TimeMarkerStop().Output(timer1),
                    new LogMarker(LogLevel.Info).Input(timer1),
                },
                ClassifierPipeline = new WeightedClassifier
                {
                    {
                        (new DTWClassifier(Accord.Math.Distance.Manhattan){
                            Features.X,
                            Features.Y
                        }, 0.15)
                    },
                    {
                        (new DTWClassifier(){
                            Features.Pressure
                        }, 0.3)
                    },
                    {
                        (new DTWClassifier(){
                            FeatureDescriptor.Get<List<double>>("Tangent")//Features.Tangent
                        }, 0.55)
                    },
                    //{
                    //    (new MultiDimensionKolmogorovSmirnovClassifier
                    //    {
                    //        Features = {"X", "Y" },
                    //        ThresholdStrategy = ThresholdStrategies.AveragePlusDeviance
                    //    },
                    //    0.8)
                    //}
                }
            };
            verifier.ProgressChanged += ProgressPrimary;

            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            var signers = new List<Signer>(loader.EnumerateSigners(p => p.ID == "01"));//Load the first signer only

            List<Signature> references = signers[0].Signatures.GetRange(0, 10);
            verifier.Train(references);

            Signature questioned1 = signers[0].Signatures[0];
            Signature questioned2 = signers[0].Signatures[25];
            bool isGenuine1 = verifier.Test(questioned1);//true
            bool isGenuine2 = verifier.Test(questioned2);//false
            debugLogger.Stop();
        }

        static void OnlineVerifierBenchmarkDemo()
        {
            Logger debugLogger = new Logger(
                LogLevel.Debug,
                new FileStream($@"Logs\OnlineBenchmark_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create),
                LogConsole);

            var benchmark = new VerifierBenchmark()
            {
                Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true),
                Verifier = Verifier.BasicVerifier,
                Sampler = Sampler.BasicSampler,
                Logger = debugLogger,
            };

            benchmark.ProgressChanged += ProgressPrimary;
            benchmark.Verifier.ProgressChanged += ProgressSecondary;

            //var result = await benchmark.ExecuteAsync();
            var result = benchmark.ExecuteParallel();

            debugLogger.Stop();

            //result.SignerResults...
            Console.WriteLine($"AER: {result.FinalResult.Aer}");
        }

        static void OnlineToImage()
        {
            Logger debugLogger = new Logger(
                LogLevel.Debug,
                new FileStream($@"Logs\OnlineToImageDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create),
                LogConsole);

            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            Signature s1 = loader.EnumerateSigners(p => (p.ID == "10")).ToList()[0].Signatures[10];//signer 10, signature 10

            var tfs = new SequentialTransformPipeline
            {
                new ParallelTransformPipeline
                {
                    new Normalize().Input(Features.X),
                    new Normalize().Input(Features.Y)
                },
                new RealisticImageGenerator(1280, 720)
            };
            tfs.Logger = debugLogger;
            tfs.ProgressChanged += ProgressPrimary;
            tfs.Transform(s1);

            ImageSaver.Save(s1, @"GeneratedOnlineImage.png");
            debugLogger.Stop();
        }

        static void GenerateOfflineDatabase()
        {
            string path = @"Databases\Offline\Generated\";
            Directory.CreateDirectory(path);

            Logger warnLogger = new Logger(
                LogLevel.Warn,
                new FileStream($@"Logs\OfflineDatabaseDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create),
                LogConsole);

            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            List<Signer> signers = loader.EnumerateSigners(null).ToList();

            var pipeline = new SequentialTransformPipeline
            {
                new ParallelTransformPipeline
                {
                    new Normalize().Input(Features.X),
                    new Normalize().Input(Features.Y)
                },
                new ApproximateOnlineFeatures(),
                new RealisticImageGenerator(1280, 720),
            };
            pipeline.Logger = warnLogger;//only log warnings and errors

            for (int i = 0; i < signers.Count; i++)
            {
                for (int j = 0; j < signers[i].Signatures.Count; j++)
                {
                    pipeline.Transform(signers[i].Signatures[j]);
                    ImageSaver.Save(signers[i].Signatures[j], path + $"U{i + 1}S{j + 1}.png");
                    ProgressSecondary(null, (int)(j / (double)signers[i].Signatures.Count * 100.0));
                }
                Console.WriteLine($"Signer{signers[i].ID} ({i + 1}/{signers.Count}) signature images generated.");
                ProgressPrimary(null, (int)(i / (double)signers.Count * 100.0));
            }


        }

        public static void LogConsole(LogLevel l, string message)
        {
            switch (l)
            {
                case LogLevel.Fatal:
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                default:
                    break;
            }
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ProgressConsole(object sender, int progress)
        {
            Console.WriteLine($"{sender.ToString()} Progress: {progress}%");
        }

        static int primaryP = 0;
        static int secondaryP = 0;
        public static void ProgressPrimary(object sender, int progress)
        {
            primaryP = progress;
            SetTitleProgress();
        }
        public static void ProgressSecondary(object sender, int progress)
        {
            secondaryP = progress;
            SetTitleProgress();
        }
        public static void SetTitleProgress()
        {
            Console.Title = $"Progress: {primaryP}% | Sub-progress: {secondaryP}%";
        }
        private static void WriteToConsole(bool[,] arr)
        {
            for (int y = 0; y < arr.GetLength(1); y += 2)
            {
                for (int x = 0; x < arr.GetLength(0); x++)
                {
                    Console.Write(arr[x, arr.GetLength(1) - y - 1] ? "o" : " ");
                }
                Console.WriteLine();
            }
        }
    }
}
