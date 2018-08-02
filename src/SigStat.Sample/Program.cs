using SigStat.Common;
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
using System.Threading.Tasks;

namespace SigStat.Sample
{
    class Program
    {
        public static class MyFeatures
        {

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
            //OfflineVerifierDemo();
            OnlineVerifierDemo();
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
            sig["X"] = new List<double>() { 1,2,3};
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
            //var verifier = new Verifier()
            //{
            //    Pipeline = 
            //    {
            //        new RealisticImageGeneration(),
            //        new BasicMetadataExtraction(),
            //        new BaselineExtraction(),
            //        new LoopExtraction()
            //    },
            //    Classifier = new StatisticalFeatureClassifier()
            //    {
            //        Features = new[] { s => s.BaseLinse, s => s.Loops, nameof(MySignature.Loops) }, //!!!!
            //        Mapping = new HungarianMapping()
            //    },
            //};

            //List<Signature> references = new List<Signature>();
            //Signature questioned = new Svc2004Loader().LoadSignature("questioned.txt");
            //verifier.Train(references);
            //verifier.Test(questioned);
        }

        static void OnlineVerifierDemo()
        {
            var timer1 = FeatureDescriptor<DateTime>.Descriptor("Timer1");

            var verifier = new Verifier()
            {
                TransformPipeline = new SequentialTransformPipeline
                {
                    new TimeMarkerStart(timer1),
                    /*new ParallelTransformPipeline
                    {//pl. ezt a kettot tudjuk parhuzamositani, mert egymastol fuggetlenek
                        new Map(10,20, Features.X),
                        new Normalize(Features.Y),
                    },
                    new Translate(0.5,0.1),
                    new Addition
                    {
                        (Features.X, -0.5)
                    },*/
                    new Normalize(Features.Pressure),
                    new CentroidTranslate(),//ez egy sequential pipeline leszarmazott, hogy epitkezni tudjunk az elemekbol
                    new TimeReset(),//^
                    new TangentExtraction(),
                    /*new AlignmentNormalization(Alignment.Origin),
                    new Paper13FeatureExtractor(),*/
                    new TimeMarkerStop(timer1),
                    new LogMarker(Log, timer1)
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
            Svc2004Loader loader = new Svc2004Loader(@"D:\AutSoft\SigStat\projekt\AH_dotNet\AH_dotNet\Assets\online_signatures\", true);
            var  signers = new List<Signer>(loader.EnumerateSigners(signer1));

            List<Signature> references = signers[0].Signatures.GetRange(0,10);
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
                Loader = new Svc2004Loader(@"D:\AutSoft\SigStat\projekt\AH_dotNet\AH_dotNet\Assets\online_signatures\", true),
                Verifier = Verifier.BasicVerifier,
                SampleSelectionStrategy = (10, 10, 10),
                Log = Log,
                Progress = Progress
            };
            //var result = await benchmark.ExecuteAsync();
            var result = benchmark.Execute();
            /*foreach (var signerResults in result.SignerResults)
            {
                Console.WriteLine($"{signerResults.Signer}");
                Console.WriteLine($"FRR: {signerResults.FRR}");
                Console.WriteLine($"FAR: {signerResults.FAR}");
                Console.WriteLine($"AER: {signerResults.AER}");
            }*/
            Console.WriteLine("Final results: ");
            Console.WriteLine($"FRR: {result.FinalResult.Frr}");
            Console.WriteLine($"FAR: {result.FinalResult.Far}");
            Console.WriteLine($"AER: {result.FinalResult.Aer}");
        }

        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void Progress(int progress)
        {
            Console.WriteLine($"Progress: {progress}%");
        }
    }
}
