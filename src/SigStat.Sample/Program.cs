using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Markers;
using SigStat.Common.PipelineItems.Transforms;
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
            //Define the features of your signature model
            public static FeatureDescriptor<bool[,]> Binarized = FeatureDescriptor<bool[,]>.Descriptor("Binarized");
            public static FeatureDescriptor<bool[,]> Skeleton = FeatureDescriptor<bool[,]>.Descriptor("Skeleton");
            public static FeatureDescriptor<List<double>> Tangent = FeatureDescriptor<List<double>>.Descriptor("Tangent");
        }

        struct OnlinePoint { public int X; public int Y; public int Pressure; }

        class MySignature : Signature
        {
            //public List<Loop> Loops { get { return (List<Loop>)this["Loop"]; } set { this["Loop"] = value; } }

            //public List<Loop> Loops2 { get { return GetFeature<List<Loop>>(Features.Loop.Key); } set { this[Features.Loop.Key] = value; } }
            
            // Preferált:
            public List<Loop> Loops3 { get { return GetFeature(Features.Loop); } set { SetFeature(Features.Loop, value); } }

            //public List<Loop> Loops4 { get { return GetFeatures<Loop>(); } set { SetFeatures(value); } }


            //public List<OnlinePoint> OnlinePoints { get { return GetFeature<List<OnlinePoint>>(); } set { SetFeature(value); } }
            //public List<OnlinePoint> OnlinePoints2 { get { return GetFeatures<OnlinePoint>(); } set { SetFeatures(value); } }

            public RectangleF Bounds { get { return GetFeature(Features.Bounds); } set { SetFeature(Features.Bounds, value); } }

            public bool[,] Binarized { get { return GetFeature(MyFeatures.Binarized); } set { SetFeature(MyFeatures.Binarized, value); } }
            public List<double> Tangent { get { return GetFeature(MyFeatures.Tangent); } set { SetFeature(MyFeatures.Tangent, value); } }

        }

        public static async Task Main(string[] args)
        {
            Console.WriteLine("SigStat library sample");
            int menuitem = 0;
            do
            {
                Console.WriteLine("Choose a demo: ");
                Console.WriteLine(" - 1. Signature demo");
                Console.WriteLine(" - 2. Online Signature -> Image");
                Console.WriteLine(" - 3. Offline Verifier");
                Console.WriteLine(" - 4. Online Verifier");
                Console.WriteLine(" - 5. Online Benchmark");
                Console.WriteLine(" - 0. Exit");
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out menuitem))
                    menuitem = 0;
                Console.WriteLine();
                switch (menuitem)
                {
                    case 1:
                        SignatureDemo();
                        break;
                    case 2:
                        OnlineToImage();
                        break;
                    case 3:
                        OfflineVerifierDemo();
                        break;
                    case 4:
                        OnlineVerifierDemo();
                        break;
                    case 5:
                        await OnlineVerifierBenchmarkDemo();
                        break;
                    default:
                        break;
                }
            } while (menuitem != 0);
            Console.WriteLine("Done. Press any key to continue!");
            Console.ReadKey();
        }

        public static void SignatureDemo()
        {
            MySignature sig = new MySignature() { ID = "Demo", Origin = Origin.Genuine };
            var sampleLoops = new List<Loop>() { new Loop() { Center = new PointF(1, 1) }, new Loop() { Center = new PointF(3, 3) } };

            // Generikus függvény + Típus
            sig["Loop"] = sampleLoops;
            sig["X"] = new List<double>() { 1, 2, 3 };
            sig["Bounds"] = new RectangleF();

            var f1 = (List<Loop>)sig["Loop"];
            sig.GetFeature<List<double>>("X");
            sig.GetFeature<RectangleF>("Bounds");
            //var xt = sig.GetFeature(Features.X);
            sig.GetFeatures<Loop>();

            

            //Loop loop = sig.GetFeature<Loop>(1);
            List<Loop> loops = sig.GetFeatures<Loop>();

            // Tulajdonságokkal + ID
            sig["Loop"] = new List<Loop>();
            loops = (List<Loop>)sig["Loop"];

            // Generikus függvény + ID
            //sig.SetFeatures(new List<Loop>());
            //loops = sig.GetFeature(Features.Loop);
            //loops = sig.GetFeatures<Loop>();
            //var loop6 = sig.GetFeature<Loop>(6);

            // Erősen típusos burkolóval
            //sig.Loops = sampleLoops;
            //loops = sig.Loops;

            foreach (var descriptor in sig.GetFeatureDescriptors())
            {
                Console.WriteLine($"Name: {descriptor.Name}, Key: {descriptor.Key}");
                if (!descriptor.IsCollection)
                {
                    Console.WriteLine(sig[descriptor]);
                }
                else
                {
                    var items = (IList)sig[descriptor];
                    for (int i = 0; i < items.Count; i++)
                    {
                        Console.WriteLine($" {i}.) {items[i]}");
                    }
                }
            }

        }

        /// <summary>
        /// Read signature from image, extract features, generate new image
        /// </summary>
        static void OfflineVerifierDemo()
        {
            Logger debugLogger = new Logger(
                LogLevel.Debug,
                new FileStream($@"OfflineDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create),
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
            verifier.ProgressChanged += ProgressVerifier;

            Signature s1 = new Signature();
            ImageLoader.LoadSignature(s1, @"Databases\Offline\Images\U1S1.png");
            s1.Origin = Origin.Genuine;
            Signer s = new Signer();
            s.Signatures.Add(s1);

            verifier.Train(s);

            //TODO: ha mar Verifier demo, akkor Test()-et is hasznaljuk..
            ImageSaver.Save(s1,  @"GeneratedOfflineImage.png");
            debugLogger.Stop();
        }

        static void OnlineVerifierDemo()
        {
            Logger debugLogger = new Logger(
                LogLevel.Debug,
                new FileStream($@"OnlineDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create),
                LogConsole);

            var timer1 = FeatureDescriptor<DateTime>.Descriptor("Timer1");

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
                    new LogMarker(LogLevel.Info).Input(timer1)
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
                            FeatureDescriptor<List<double>>.Descriptor("Tangent")//Features.Tangent
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
            verifier.ProgressChanged += ProgressVerifier;

            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            var signers = new List<Signer>(loader.EnumerateSigners(p=>p=="01"));//Load the first signer only

            List<Signature> references = signers[0].Signatures.GetRange(0, 10);
            verifier.Train(references);

            Signature questioned1 = signers[0].Signatures[0];
            Signature questioned2 = signers[0].Signatures[25];
            bool isGenuine1 = verifier.Test(questioned1);//true
            bool isGenuine2 = verifier.Test(questioned2);//false
            debugLogger.Stop();
        }

        static async Task OnlineVerifierBenchmarkDemo()
        {
            Logger debugLogger = new Logger(
                LogLevel.Debug,
                new FileStream($@"OnlineBenchmark_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create),
                LogConsole);

            var benchmark = new VerifierBenchmark()
            {
                Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true),
                Verifier = Verifier.BasicVerifier,
                Sampler = Sampler.BasicSampler,
                Logger = debugLogger,
            };

            benchmark.ProgressChanged += ProgressBenchmark;
            benchmark.Verifier.ProgressChanged += ProgressVerifier;

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
                new FileStream($@"OnlineToImageDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create),
                LogConsole);

            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            Signature s1 = loader.EnumerateSigners(p=>(p=="10")).ToList()[0].Signatures[10];//signer 10, signature 10

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
            tfs.ProgressChanged += ProgressBenchmark;
            tfs.Transform(s1);

            ImageSaver.Save(s1, @"GeneratedOnlineImage.png");
            debugLogger.Stop();
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

        static int benchmarkP = 0;
        static int verifierP = 0;
        public static void ProgressBenchmark(object sender, int progress)
        {
            benchmarkP = progress;
            SetTitleProgress();
        }
        public static void ProgressVerifier(object sender, int progress)
        {
            verifierP = progress;
            SetTitleProgress();
        }
        public static void SetTitleProgress()
        {
            Console.Title = $"Benchmark: {benchmarkP}% | Verifier: {verifierP}%";
        }
    }
}
