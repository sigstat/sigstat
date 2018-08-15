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
            public static FeatureDescriptor<bool[,]> Binarized = FeatureDescriptor<bool[,]>.Descriptor("Binarized");
        }
        struct OnlinePoint { public int X; public int Y; public int Pressure; }
        class MySignature : Signature
        {
            public List<Loop> Loops { get { return (List<Loop>)this["Loop"]; } set { this["Loop"] = value; } }

            public List<Loop> Loops2 { get { return GetFeature<List<Loop>>(Features.Loop.Key); } set { this[Features.Loop.Key] = value; } }
            // Preferált:
            public List<Loop> Loops3 { get { return GetFeature(Features.Loop); } set { SetFeature(Features.Loop, value); } }

            public List<Loop> Loops4 { get { return GetFeatures<Loop>(); } set { SetFeatures(value); } }


            public List<OnlinePoint> OnlinePoints { get { return GetFeature<List<OnlinePoint>>(); } set { SetFeature(value); } }
            public List<OnlinePoint> OnlinePoints2 { get { return GetFeatures<OnlinePoint>(); } set { SetFeatures(value); } }

            public RectangleF Bounds { get { return GetFeature(Features.Bounds); } set { SetFeature(Features.Bounds, value); } }



        }

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello");
            //SignatureDemo();
            //OnlineToImage();
            //OfflineVerifierDemo();
            //OnlineVerifierDemo();
            await OnlineVerifierBenchmarkDemo();
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



            Loop loop = sig.GetFeature<Loop>(1);
            List<Loop> loops = sig.GetFeatures<Loop>();

            // Tulajdonságokkal + ID
            sig["Loop"] = new List<Loop>();
            loops = (List<Loop>)sig["Loop"];

            // Generikus függvény + ID
            sig.SetFeatures(new List<Loop>());
            loops = sig.GetFeature(Features.Loop);
            loops = sig.GetFeatures<Loop>();
            //var loop6 = sig.GetFeature<Loop>(6);

            // Erősen típusos burkolóval
            sig.Loops = sampleLoops;
            loops = sig.Loops;

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

        static void OfflineVerifierDemo()
        {
            //Task.Factory.StartNew<int>(null)
            //    .ContinueWith(t=>()=> t.Result/2);
            //    .ContinueWith(t => () => t.Result / 2);


            //var pipeline = new SequentialTransformPipeline().Append( new Binarization().Append())

            var verifier = new Verifier()
            {
                Logger = new Logger(LogLevel.Debug, new FileStream($@"OfflineDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create), LogConsole),
                TransformPipeline = new SequentialTransformPipeline
                {
                    new Binarization().Input(Features.Image),
                    new Trim(5),
                    new PrepareForThinning(),
                    new ImageGenerator(true),
                    new HSCPThinning(),
                    new ImageGenerator(true),
                    new OnePixelThinning().Output(FeatureDescriptor<bool[,]>.Descriptor("Skeleton")),//output Skeletonba, mert az Extraction onnan szedi
                    new ImageGenerator(true),
                    //Baseline ide
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

                    /*new BasicMetadataExtraction(),
                    new BaselineExtraction(),
                    new LoopExtraction()*/
                },
                ClassifierPipeline = new DTWClassifier()
            };

            Signature s1 = new Signature();
            ImageLoader.LoadSignature(s1, @"Databases\Offline\Images\U1S1.png");
            s1.Origin = Origin.Genuine;
            Signer s = new Signer();
            s.Signatures.Add(s1);

            verifier.Train(s);

            ImageSaver.Save(s1,  @"GeneratedOfflineImage.png");

        }

        static void OnlineVerifierDemo()
        {
            var timer1 = FeatureDescriptor<DateTime>.Descriptor("Timer1");

            var verifier = new Verifier()
            {
                Logger = new Logger(LogLevel.Debug, new FileStream($@"OnlineDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create), LogConsole),
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
                    new CentroidTranslate(),//ez egy sequential pipeline leszarmazott, hogy epitkezni tudjunk az elemekbol
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

            bool signer1(string p)
            { return p == "01"; }
            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            var signers = new List<Signer>(loader.EnumerateSigners(signer1));

            List<Signature> references = signers[0].Signatures.GetRange(0, 10);
            verifier.Train(references);

            Signature questioned1 = signers[0].Signatures[0];
            Signature questioned2 = signers[0].Signatures[25];
            bool isGenuine1 = verifier.Test(questioned1);//true
            bool isGenuine2 = verifier.Test(questioned2);//false
        }

        static async Task OnlineVerifierBenchmarkDemo()
        {

            var benchmark = new VerifierBenchmark()
            {
                Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true),
                Verifier = Verifier.BasicVerifier,
                Sampler = Sampler.BasicSampler,
                Logger = new Logger(LogLevel.Debug, new FileStream($@"OnlindeBenchmark_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create)),
            };

            benchmark.ProgressChanged += ProgressBenchmark;
            benchmark.Verifier.ProgressChanged += ProgressVerifier;

            //var result = await benchmark.ExecuteAsync();
            var result = benchmark.ExecuteParallel();

            //result.SignerResults...
            Console.WriteLine($"AER: {result.FinalResult.Aer}");
        }

        static void OnlineToImage()
        {
            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            Signature s1 = loader.EnumerateSigners(p=>(p=="10")).ToList()[0].Signatures[10];//signer 10, signature 10

            var tfs = new SequentialTransformPipeline
            {
                new ParallelTransformPipeline
                {
                    new Normalize().Input(Features.X),
                    new Normalize().Input(Features.Y)
                },
                /*new BinaryRasterizer(400, 300, 2),
                new ImageGenerator()*/
                new RealisticImageGenerator(1280, 720)
            };
            tfs.Logger = new Logger(LogLevel.Debug, new FileStream($@"OnlineToImageDemo_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", FileMode.Create), LogConsole);
            tfs.ProgressChanged += ProgressBenchmark;
            tfs.Transform(s1);

            ImageSaver.Save(s1, @"GeneratedOnlineImage.png");
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
