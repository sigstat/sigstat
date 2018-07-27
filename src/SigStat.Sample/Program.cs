using SigStat.Common;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
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
            public List<Loop> Loops3 { get { return GetFeature(Features.Loop); } set { SetFeature(Features.Loop, value); } }

            public List<Loop> Loops4 { get { return GetFeatures<Loop>(); } set { SetFeatures(value); } }


            public List<OnlinePoint> OnlinePoints { get { return GetFeature<List<OnlinePoint>>(); } set { SetFeature(value); } }
            public List<OnlinePoint> OnlinePoints2 { get { return GetFeatures<OnlinePoint>(); } set { SetFeatures(value); } }

            public RectangleF Bounds { get { return GetFeature(Features.Bounds); } set { SetFeature(Features.Bounds, value); } }



        }

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello");
            SignatureDemo();
            OfflineVerifierDemo();
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

            var f1 = sig["Loop"];
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
            var verifier = new Verifier()
            {
                TransformPipeline = new SequentialTransformPipeline
                {
                    new ParallelTransformPipeline
                    {//pl. ezt a kettot tudjuk parhuzamositani, mert egymastol fuggetlenek
                        new Map(10,20, Features.X),
                        new Normalize(Features.Y),
                    },
                    new Translate
                    {
                        (Features.X, 0.5),
                        (Features.Y, -2.0)
                    },
                    new CentroidTranslate()//ez egy sequential pipeline leszarmazott, hogy epitkezni tudjunk az elemekbol
                    
                    //new Common.PipelineItems.Normalize(Features.Pressure),
                    //new Common.PipelineItems.Normalize(Features.Altitude),
                    /*new AlignmentNormalization(Alignment.Origin),
                    new Paper13FeatureExtractor(),*/
                },
                ClassifierPipeline = new WeightedClassifier
                {
                    {
                        (new DTWClassifier(Accord.Math.Distance.Manhattan){
                            Features.X,
                            Features.Y
                        }, 0.8)
                    },
                    {
                        (new DTWClassifier(){
                            Features.Pressure
                        }, 0.2)
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

            List<Signature> references = new List<Signature>();
            Signature questioned = new Signature();
            verifier.Train(references);
            bool isGenuine = verifier.Test(questioned);
        }

        static Task OnlineVerifierBenchmarkDemo()
        {
            //var benchmark = new VerifierBenchmark()
            //{
            //    Loader = new Svc2004Loader(),
            //    Verifier = new BasicVerifier(),
            //    SampleSelectionStrategy = new ConstantSelectionStrategy(
            //           s => s.Signatures.Where(s => s.IsOriginal).Take(10),
            //           s => s.Signatures.Where(s => s.IsOriginal).Take(10),
            //           s => s.Signatures.Where(s => !s.IsOriginal).Take(10)),
            //    Logger = Log,
            //    Progress = Progress
            //};
            //var result = await benchmark.ExecuteAsync();
            //foreach (var signerResults in result.SignerResults)
            //{
            //    Console.WriteLine($"{signerResults.Signer} AER: {signerResults.Aer}");
            //}
            //Console.WriteLine($"AER: {result.Aer}");
            return Task.CompletedTask;
        }

        public void Log(string message)
        {

        }

        public void Progress(int progrees)
        {

        }
    }
}
