using SigStat.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace SigStat.Sample
{
    class Program
    {
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

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static void SignatureDemo()
        {
            MySignature sig = new MySignature() { ID = "Demo", Origin = Origin.Original };

            // Generikus függvény + Típus
            sig["Loop"] = new List<Loop>();
            sig["X"] = new List<double>();
            sig["Bounds"] = new RectangleF();

            var f1 = sig["Loop"];
            sig.GetFeature<List<double>>("X");
            sig.GetFeature<RectangleF>("Bounds");
            sig.GetFeatures<Loop>();







            Loop loop = sig.GetFeature<Loop>(1);
            List<Loop> loops = sig.GetFeatures<Loop>();

            // Tulajdonságokkal + ID
            sig["Loops"] = new List<Loop>();
            loops = (List<Loop>)sig["Loops"];

            // Generikus függvény + ID
            sig.SetFeature(new List<Loop>());
            loops = sig.GetFeature(Features.Loop);

            // Erősen típusos burkolóval
            sig.Loops = new List<Loop>();
            loops = sig.Loops;

            foreach (var descriptor in sig.GetFeatureDescriptors())
            {
                Console.WriteLine(descriptor.Name);
                Console.WriteLine(descriptor.Key);
                if (descriptor.IsCollection)
                {
                    Console.WriteLine(sig[descriptor]);
                }
                else
                {
                    foreach (var item in (IEnumerable)sig[descriptor])
                    {
                        Console.WriteLine(item);
                    }
                }
            }

        }

        static void OfflineVerifierDemo()
        {
            //var verifier = new Verifier()
            //{
            //    FeatureExtraction =
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
            //var verifier = new Verifier()
            //{
            //    FeatureExtraction =
            //    {
            //        new Normalization(0,1, ()=>X),
            //        new Normalization(0,1, ()=>Y),
            //        new Normalization(0,1, ()=>Pressure),
            //        new Normalization(0,1, ()=>Altitude),
            //        new AlignmentNormalization(Alignment.Origin),
            //        new Paper13FeatureExtractor(),
            //    },
            //    Classifier = new MyScoreFusionClassifier
            //    {
            //        {
            //            new MultiDimensionDtwClassifier(), 0.2
            //        },
            //        {
            //            new MultiDimensionKolmogorovSmirnovClassifier
            //            {
            //                Features = {"X", "Y" },
            //                ThresholdStrategy = ThresholdStrategies.AveragePlusDeviance
            //            },
            //            0,8
            //        }
            //    }



            //};

            //List<Signature> references = new List<Signature>();
            //Signature questioned = new Signature();
            //verifier.Train(references);
            //verifier.Test(questioned);
        }

        static async Task OnlineVerifierBenchmark()
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
        }

        public void Log(string message)
        {

        }

        public void Progress(int progrees)
        {

        }
    }
}
