
using OfficeOpenXml;
using SigStat.Common;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using SigStat.Common.PipelineItems.Transforms.Raster;
using SigStat.Common.Transforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static SigStat.Common.Loaders.MCYTLoader;
using static SigStat.Common.Loaders.SigComp11ChineseLoader;
using static SigStat.Common.Loaders.SigComp13JapaneseLoader;
using SixLabors.Primitives;
using System.Drawing;
using OfficeOpenXml.Drawing.Chart;
using SigStat.Common.Logging;
using SigStat.Common.Algorithms.Distances;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

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

        class MySignature : Signature
        {
            public List<Loop> Loops { get { return GetFeature(MyFeatures.Loop); } set { SetFeature(MyFeatures.Loop, value); } }
            public SixLabors.Primitives.SizeF Size { get { return GetFeature(Features.Size); } set { SetFeature(Features.Size, value); } }

            public bool[,] Binarized { get { return GetFeature(MyFeatures.Binarized); } set { SetFeature(MyFeatures.Binarized, value); } }
            public List<double> Tangent { get { return GetFeature(MyFeatures.Tangent); } set { SetFeature(MyFeatures.Tangent, value); } }

        }

        public static void Main(string[] args)
        {
            Console.WriteLine("SigStat library sample");

            //OCJKNNTest();
            //signerSampleRateBasedVerifier();

            // SamplingRateTestCompilation();
            //SignatureDemo();
            //TransformationPipeline();
            //Classifier();
            //OnlineToImage();
            //DatabaseLoaderDemo();
            //GenerateOfflineDatabase();
            //OfflineVerifierDemo();
            //OnlineVerifierDemo();
            // SignatureToImageTesting();
            //OnlineRotationBenchmarkDemo();
            //SampleRateTestingDemo();
            // DtwWindowTest();
            //OnlineVerifierBenchmarkDemo();
            //PreprocessingBenchmarkDemo();
            //TestPreprocessingTransformations();
            //JsonSerializeSignature();
            //JsonSerializeOnlineVerifier();
            //JsonSerializeOnlineVerifierBenchmark();
            //ClassificationBenchmark();
            RealisticImageRendering2Demo();
            Console.WriteLine("Press <<Enter>> to exit.");
            Console.ReadLine();

        }

        private static void OCJKNNTest()
        {
            // j, k, threshold, reference count, database (1-2)


            int row = 1;
            int col = 1;

            var excel = new ExcelPackage();
            var sheet = excel.Workbook.Worksheets.Add("benchmark");

            double threshold = 1.2;
            int referenceCount = 10;

            var databaseDir = Environment.GetEnvironmentVariable("SigStatDB");
            var fileLoader = new Svc2004Loader(Path.Combine(databaseDir, "SVC2004.zip"), true);
            //var fileLoader = new SigComp11DutchLoader(Path.Combine(databaseDir, "SigComp11_Dutch.zip"), true);

            var memoryLoader = new MemoryDataSetLoader(fileLoader.EnumerateSigners()); // cache the database in memory to avoid (slow) disc operations

            var sampler = new FirstNSampler(referenceCount);

            var features = new List<FeatureDescriptor> { Features.X, Features.Y, Features.Pressure };
            Console.WriteLine($"Executing initial benchmark");

            // Calculate all SignerModels and store them for future use
            var initialBenchmark = new VerifierBenchmark()
            {
                Loader = memoryLoader,
                Sampler = sampler,
                Verifier = new Verifier()
                {
                    Pipeline = {
                                new ZNormalization() { InputFeature = Features.X, OutputFeature = Features.X },
                                new ZNormalization() { InputFeature = Features.Y, OutputFeature = Features.Y },
                                new ZNormalization() { InputFeature = Features.Pressure, OutputFeature = Features.Pressure }
                            },
                    Classifier = new OneClassNearestNeighborClassifier(referenceCount, referenceCount - 1, threshold, new DtwDistance(new ManhattanDistance())) { Features = features }
                }
            };
            initialBenchmark.Logger = new SimpleConsoleLogger(LogLevel.Debug);
            var initialResults = initialBenchmark.Execute(true);
            var signerModels = initialResults.SignerResults.Select(sr => sr.Model).ToList(); // the signer models contain the precalculated DTW distances. We are going to reuse them to reduce further calculation costs

            for (int j = 1; j <= referenceCount; j++)
            {
                sheet.Cells[row + j, col].Value = j;
                sheet.Cells[row + j, col + referenceCount + 3].Value = j;
                sheet.Cells[row + referenceCount + 3 + j, col].Value = j;
            }
            for (int k = 1; k <= referenceCount - 1; k++)
            {
                sheet.Cells[row, col + k].Value = k;
                sheet.Cells[row + referenceCount + 3, col + k].Value = k;
                sheet.Cells[row, col + referenceCount + 3 + k].Value = k;
            }
            sheet.Cells[row, col].Value = "AER";
            sheet.Cells[row + referenceCount + 3, col].Value = "FAR";
            sheet.Cells[row, col + referenceCount + 3].Value = "FRR";


            // Fixed parameter
            for (int j = 1; j <= referenceCount; j++)
            {
                for (int k = 1; k <= referenceCount - 1; k++)
                {
                    Console.WriteLine($"Executing benchmarki for J: {j} K: {k}");
                    var benchmark = new VerifierBenchmark()
                    {
                        SignerModels = signerModels,
                        Loader = memoryLoader,
                        Sampler = sampler,
                        Verifier = new Verifier()
                        {
                            Classifier = new OneClassNearestNeighborClassifier(j, k, threshold, new DtwDistance(new ManhattanDistance())) { Features = features }
                        }
                    };

                    var results = benchmark.Execute(true);

                    sheet.Cells[row + j, col + k].Value = results.FinalResult.Aer;
                    sheet.Cells[row + referenceCount + 3 + j, col + k].Value = results.FinalResult.Far;
                    sheet.Cells[row + j, col + referenceCount + 3 + k].Value = results.FinalResult.Frr;

                }
            }

            excel.SaveAs(new FileInfo("results.xlsx"));
        }


        //*********************************************************************//
        //----------------------------------------------------------------//
        private static void signerSampleRateBasedVerifier()
        {
            var p2 = new ExcelPackage();
            var compareSheet = p2.Workbook.Worksheets.Add("Compare results");
            int totalSamples = 7;
            int e = 1;
            for (int samples = 6; samples < totalSamples; samples++)
            {

                var p = new ExcelPackage();


                VerifierBenchmark b = new VerifierBenchmark();
                Verifier v = new Verifier();
                List<SequentialTransformPipeline> pipeline_ = new List<SequentialTransformPipeline>();

                SequentialTransformPipeline pip0 = new SequentialTransformPipeline();
                pip0.Items.Add(new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y });
                pip0.Items.Add(new Scale() { InputFeature = Features.X, OutputFeature = Features.X });
                pip0.Items.Add(new Scale() { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });
                pip0.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X, OutputFeature = Features.X });
                pip0.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y, OutputFeature = Features.Y });
                pip0.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });

                SequentialTransformPipeline pip1 = new SequentialTransformPipeline();
                pip1.Items.Add(new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y });
                pip1.Items.Add(new Scale() { InputFeature = Features.X, OutputFeature = Features.X });
                pip1.Items.Add(new Scale() { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });


                SequentialTransformPipeline pip2 = new SequentialTransformPipeline();
                pip2.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X, OutputFeature = Features.X });
                pip2.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y, OutputFeature = Features.Y });
                pip2.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });

                SequentialTransformPipeline pip3 = new SequentialTransformPipeline();
                pip3.Add(new ZNormalization() { InputFeature = Features.X, OutputFeature = Features.X });
                pip3.Add(new ZNormalization() { InputFeature = Features.Y, OutputFeature = Features.Y });
                pip3.Add(new ZNormalization() { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });

                List<OptimalDtwClassifier> optimalClassifiers = new List<OptimalDtwClassifier>();
                List<DtwClassifier> classifiers = new List<DtwClassifier>();
                int cla = 0;
                //////////////////////////////////////////////////////////////////////////////// classifier
                optimalClassifiers.Add(new OptimalDtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.X },
                    Sampler = new FirstNSampler(10)
                });

                optimalClassifiers.Add(new OptimalDtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.Y },
                    Sampler = new FirstNSampler(10)
                });

                optimalClassifiers.Add(new OptimalDtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.Pressure },
                    Sampler = new FirstNSampler(10),
                });
                optimalClassifiers.Add(new OptimalDtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.X, Features.Y },
                    Sampler = new FirstNSampler(10)

                });

                optimalClassifiers.Add(new OptimalDtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure },
                    Sampler = new FirstNSampler(10)
                });

                classifiers.Add(new DtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.X }
                });

                classifiers.Add(new DtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.Y }
                });

                classifiers.Add(new DtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.Pressure }
                });
                classifiers.Add(new DtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.X, Features.Y }

                });

                classifiers.Add(new DtwClassifier()
                {
                    DistanceFunction = Accord.Math.Distance.Euclidean,
                    Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                });



                // pipeline_.Add(pip0);
                // pipeline_.Add(pip1);
                // pipeline_.Add(pip2);
                pipeline_.Add(pip3);
                int samplingrate = 100;

                var databaseDir2 = Environment.GetEnvironmentVariable("SigStatDB");



                List<string> databases = new List<string>();
                int q = 0;
                foreach (string fileName_ in Directory.GetFiles(databaseDir2, "*.zip"))
                {
                    cla = 0;

                    string[] str = fileName_.Split("\\");
                    databases.Add(str[4]);
                    Console.WriteLine(databases[q]);
                    string dataBaseName = str[4];
                    string database = str[4];

                    ////////////////////////////////////// loader
                    if (dataBaseName == "SVC2004.zip")
                    {
                        b.Loader = new Svc2004Loader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 100;


                    }

                    else if (dataBaseName == "MCYT100.zip")
                    {
                        b.Loader = new MCYTLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 100;



                    }
                    else if (dataBaseName == "SigComp11_Dutch.zip")
                    {
                        b.Loader = new SigComp11DutchLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 200;

                    }
                    else if (dataBaseName == "SigComp11Chinese.zip")
                    {
                        b.Loader = new SigComp11ChineseLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 200;


                    }
                    else if (dataBaseName == "SigWiComp2013_Japanese.zip")
                    {
                        b.Loader = new SigComp13JapaneseLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 200;



                    }
                    else
                    {
                        b.Loader = new SigComp15GermanLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 75;
                    }

                    var loader2 = b.Loader;
                    var loader3 = b.Loader;


                    foreach (DtwClassifier classifier in classifiers)
                    {
                        foreach (SequentialTransformPipeline pip in pipeline_)
                        {
                            var data = p.Workbook.Worksheets.Add("Data");
                            var Sheet2 = p.Workbook.Worksheets.Add("Results");







                            List<Signer> signers = loader2.EnumerateSigners(null).ToList();



                            foreach (Signer s2 in signers)
                            {
                                var Sheet = p.Workbook.Worksheets.Add($"Signer({s2.ID})");
                                Sheet.Cells[1, 4].Value = "s";
                                Sheet.Cells[1, 5].Value = "SamplingFrequency";
                                Sheet.Cells[1, 6].Value = "FRR";
                                Sheet.Cells[1, 7].Value = "FAR";
                                Sheet.Cells[1, 8].Value = "AER(Testing)";
                                Sheet.Cells[1, 9].Value = "bestFrr";
                                Sheet.Cells[1, 10].Value = "bestStep";
                                Sheet.Cells[1, 11].Value = "bestRate";
                                Sheet.Cells[1, 13].Value = "N_Frr";
                                Sheet.Cells[1, 14].Value = "N_Far";
                                Sheet.Cells[1, 15].Value = "N_Aer(Regular)";


                            }

                            var samplerate = new List<SampleRateResults>();
                            int s = 1;
                            int n = 20;
                            for (s = 1; s <= n; s = s + 1)
                            {
                                data.Cells[1, 2].Value = $"s= {n}";
                                Console.WriteLine("s= " + s);
                                var benchmark = new VerifierBenchmark();


                                benchmark.Loader = loader2;

                                benchmark.Verifier = new Verifier();
                                pip.Items.Insert(0, new SampleRate() { samplerate = s, InputX = Features.X, InputY = Features.Y, InputP = Features.Pressure, OutputX = Features.X, OutputY = Features.Y, OutputP = Features.Pressure });
                                benchmark.Verifier.Pipeline = pip;

                                benchmark.Verifier.Classifier = classifier;


                                benchmark.Sampler = new TestingSampler(samples);
                                benchmark.Logger = new SimpleConsoleLogger();

                                data.Cells[2, 2].Value = $"N samples= {samples}";

                                benchmark.ProgressChanged += ProgressPrimary;

                                var result = benchmark.Execute(true);

                                int ii = 0;
                                foreach (Signer sig in signers)
                                {

                                    //   DtwSignerModel signerModel = (DtwSignerModel)result.SignerResults.Single(e => e.Signer == sig.ID).Model;
                                    //  double th = signerModel.Threshold;

                                    if (signers.Single(o => o.ID == sig.ID).bestFrr > result.SignerResults.Single(e2 => e2.Signer == sig.ID).Frr && signers.Single(o => o.ID == sig.ID).bestFrr != 0)
                                    {

                                        signers.Single(o => o.ID == sig.ID).bestFrr = result.SignerResults.Single(e3 => e3.Signer == sig.ID).Frr;
                                        signers.Single(o => o.ID == sig.ID).bestSampleRate = samplingrate / s;
                                        signers.Single(o => o.ID == sig.ID).bestStep = s;
                                    }

                                    var Sheet = p.Workbook.Worksheets.Single(se => se.Name == $"Signer({sig.ID})");

                                    Sheet.Cells[s + 1, 4].Value = s;
                                    Sheet.Cells[s + 1, 5].Value = samplingrate / s;
                                    Sheet.Cells[s + 1, 6].Value = result.SignerResults.Single(z => z.Signer == sig.ID).Frr;
                                    Sheet.Cells[s + 1, 7].Value = result.SignerResults.Single(z => z.Signer == sig.ID).Far;
                                    Sheet.Cells[s + 1, 8].Value = result.SignerResults.Single(z => z.Signer == sig.ID).Aer;
                                    Sheet.Cells[s + 1, 9].Value = sig.bestFrr;
                                    Sheet.Cells[s + 1, 10].Value = sig.bestStep;
                                    Sheet.Cells[s + 1, 11].Value = sig.bestSampleRate;
                                    ii++;
                                }




                                pip.Items.RemoveAt(0);

                            }

                            // TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
                            /*
                            samplerate = new List<SampleRateResults>();
                            s = 1;
                            n = 20;
                            for (s = 1; s <= n; s = s + 1)
                            {
                                Console.WriteLine("s= " + s);
                                var benchmark = new VerifierBenchmark();


                                benchmark.Loader = loader3;

                                benchmark.Verifier = new Verifier();
                                pip.Items.Insert(0, new SampleRate() { samplerate = s, InputX = Features.X, InputY = Features.Y, InputP = Features.Pressure, OutputX = Features.X, OutputY = Features.Y, OutputP = Features.Pressure });
                                benchmark.Verifier.Pipeline = pip;

                                benchmark.Verifier.Classifier = classifier;


                                benchmark.Sampler = new FirstNSampler(10);
                                benchmark.Logger = new SimpleConsoleLogger();



                                benchmark.ProgressChanged += ProgressPrimary;

                                var result = benchmark.Execute(true);

                                int ii = 0;
                                foreach (Signer sig in signers)
                                {

                                    var Sheet = p.Workbook.Worksheets.Single(se => se.Name == $"Signer({sig.ID})");


                                    Sheet.Cells[s + 1, 13].Value = result.SignerResults.Single(z => z.Signer == sig.ID).Frr;
                                    Sheet.Cells[s + 1, 14].Value = result.SignerResults.Single(z => z.Signer == sig.ID).Far;
                                    Sheet.Cells[s + 1, 15].Value = result.SignerResults.Single(z => z.Signer == sig.ID).Aer;

                                    ii++;
                                }




                                pip.Items.RemoveAt(0);

                            }
                            */

                            //TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT



                            //*******************************************************************************
                            //---------------------------------------------------------------------
                            // ====================================================================

                            int i = 0;

                            double avg = 0;
                            int s4 = 1;
                            foreach (Signer signer in signers)
                            {
                                Console.WriteLine("Signer:  " + signer.ID);

                                var benchmark = new VerifierBenchmark();



                                if (dataBaseName == "SVC2004.zip")
                                    b.Loader = new Svc2004Loader(Path.Combine(databaseDir2, dataBaseName), true, s2 => s2.ID == signer.ID);

                                else if (dataBaseName == "MCYT100.zip")
                                    b.Loader = new MCYTLoader(Path.Combine(databaseDir2, dataBaseName), true, s2 => s2.ID == signer.ID);


                                else if (dataBaseName == "SigComp11_Dutch.zip")
                                    b.Loader = new SigComp11DutchLoader(Path.Combine(databaseDir2, dataBaseName), true, s2 => s2.ID == signer.ID);


                                else if (dataBaseName == "SigComp11Chinese.zip")
                                    b.Loader = new SigComp11ChineseLoader(Path.Combine(databaseDir2, dataBaseName), true, s2 => s2.ID == signer.ID);

                                else if (dataBaseName == "SigWiComp2013_Japanese.zip")
                                    b.Loader = new SigComp13JapaneseLoader(Path.Combine(databaseDir2, dataBaseName), true, s2 => s2.ID == signer.ID);

                                else
                                    b.Loader = new SigComp15GermanLoader(Path.Combine(databaseDir2, dataBaseName), true, s2 => s2.ID == signer.ID);

                                benchmark.Loader = b.Loader;

                                benchmark.Verifier = new Verifier();
                                benchmark.Verifier.Pipeline = pip;
                                pip.Items.Insert(0, new SampleRate() { samplerate = signer.bestStep, InputX = Features.X, InputY = Features.Y, InputP = Features.Pressure, OutputX = Features.X, OutputY = Features.Y, OutputP = Features.Pressure });

                                benchmark.Verifier.Classifier = optimalClassifiers[cla];

                                benchmark.Sampler = new FirstNSampler(10);
                                benchmark.Logger = new SimpleConsoleLogger();



                                benchmark.ProgressChanged += ProgressPrimary;



                                var result = benchmark.Execute(true);

                                avg = avg + result.SignerResults.Single(z => z.Signer == signer.ID).Aer;

                                i++;
                                pip.Items.RemoveAt(0);
                                var Sheet = p.Workbook.Worksheets.Single(se => se.Name == $"Signer({signer.ID})");
                                Sheet.Cells[1, 16].Value = "Result after testing";
                                Sheet.Cells[2, 16].Value = result.SignerResults.Single(z => z.Signer == signer.ID).Aer;
                                Sheet.Cells[3, 16].Value = "Best step";
                                Sheet.Cells[4, 16].Value = signer.bestStep;
                                Sheet.Cells[5, 16].Value = "Best samplerate";
                                Sheet.Cells[6, 16].Value = signer.bestSampleRate;
                                Sheet.Cells[7, 16].Value = "Best frr";
                                Sheet.Cells[8, 16].Value = signer.bestFrr;

                                Sheet2.Cells[s4 + 1, 12].Value = result.SignerResults.Single(z => z.Signer == signer.ID).Frr;
                                Sheet2.Cells[s4 + 1, 13].Value = result.SignerResults.Single(z => z.Signer == signer.ID).Far;
                                Sheet2.Cells[s4 + 1, 14].Value = result.SignerResults.Single(z => z.Signer == signer.ID).Aer;
                                s4++;
                            }

                            avg = avg / signers.Count;
                            compareSheet.Cells[2, 2].Value = "iteration";
                            compareSheet.Cells[2, 3].Value = "Dep";
                            compareSheet.Cells[2, 4].Value = "Normal";
                            compareSheet.Cells[2, 5].Value = "Improvement";

                            compareSheet.Cells[e + 2, 3].Value = avg;

                            Sheet2.Cells[s4 + 1, 14].Value = avg;
                            // ===================================================================


                            var benchmark2 = new VerifierBenchmark();


                            benchmark2.Loader = loader2;


                            benchmark2.Verifier = new Verifier();
                            benchmark2.Verifier.Pipeline = pip;


                            benchmark2.Verifier.Classifier = optimalClassifiers[cla];


                            benchmark2.Sampler = new FirstNSampler(10);
                            benchmark2.Logger = new SimpleConsoleLogger();



                            benchmark2.ProgressChanged += ProgressPrimary;
                            var result2 = benchmark2.Execute(true);

                            Console.WriteLine("AER is: " + result2.FinalResult.Aer);
                            compareSheet.Cells[e + 2, 4].Value = result2.FinalResult.Aer;
                            double improvement = (((result2.FinalResult.Aer) - avg));
                            compareSheet.Cells[e + 2, 5].Value = improvement * 100;

                            if (improvement > 0)
                                compareSheet.Cells[e + 2, 6].Value = "Positive";
                            else if (improvement == 0)
                                compareSheet.Cells[e + 2, 6].Value = "-";
                            else
                                compareSheet.Cells[e + 2, 6].Value = "N";

                            s4 = 1;
                            foreach (Signer sig in signers)
                            {



                                Sheet2.Cells[1, 4].Value = "ID";
                                Sheet2.Cells[1, 5].Value = "SamplingFrequency";
                                Sheet2.Cells[1, 6].Value = "FRR";
                                Sheet2.Cells[1, 7].Value = "FAR";
                                Sheet2.Cells[1, 8].Value = "AER (normal)";
                                Sheet2.Cells[1, 9].Value = "";
                                Sheet2.Cells[1, 10].Value = "bestStep";
                                Sheet2.Cells[1, 11].Value = "bestRate";
                                Sheet2.Cells[1, 12].Value = "Frr";
                                Sheet2.Cells[1, 13].Value = "Far";
                                Sheet2.Cells[1, 14].Value = "Aer(after)";
                                Sheet2.Cells[s4 + 1, 4].Value = sig.ID;
                                Sheet2.Cells[s4 + 1, 5].Value = samplingrate;
                                Sheet2.Cells[s4 + 1, 6].Value = result2.SignerResults.Single(z => z.Signer == sig.ID).Frr;
                                Sheet2.Cells[s4 + 1, 7].Value = result2.SignerResults.Single(z => z.Signer == sig.ID).Far;
                                Sheet2.Cells[s4 + 1, 8].Value = result2.SignerResults.Single(z => z.Signer == sig.ID).Aer;
                                Sheet2.Cells[s4 + 1, 9].Value = "";
                                Sheet2.Cells[s4 + 1, 10].Value = sig.bestStep;
                                Sheet2.Cells[s4 + 1, 11].Value = sig.bestSampleRate;

                                s4++;

                            }
                            Sheet2.Cells[s4 + 1, 8].Value = result2.FinalResult.Aer;


                            string str2 = null;
                            if (pip == pip0)
                                str2 = "ST";
                            else if (pip == pip1)
                                str2 = "S";
                            else if (pip == pip2)
                                str2 = "T";
                            else str2 = "Z";
                            string features = null;
                            foreach (FeatureDescriptor f in classifier.Features)
                            {
                                string[] f2 = f.ToString().Split("(");
                                features = features + f2[0];
                            }
                            compareSheet.Cells[e + 2, 2].Value = $"{ database}";
                            compareSheet.Cells[e + 2, 7].Value = samples;
                            compareSheet.Cells[e + 2, 8].Value = features;
                            compareSheet.Cells[e + 2, 9].Value = $"{str2}";
                            compareSheet.Cells[2, 9].Value = "Pre-processing";
                            compareSheet.Cells[2, 7].Value = "Samples";
                            compareSheet.Cells[2, 8].Value = "Features";
                            e++;
                            p.SaveAs(new FileInfo($"C:/Users/Saleem/OneDrive/Mohammad/Ideas/samplerateDep/{database}_{str2}_{features}_{samples}.xlsx"));
                            p2.SaveAs(new FileInfo($"C:/Users/Saleem/OneDrive/Mohammad/Ideas/samplerateDep/compare.xlsx"));
                            p = new ExcelPackage();
                        }
                        cla++;
                    }
                }
            }


        }

        //---------------------------------------------------------------//



        private static void SamplingRateTestCompilation()
        {
            //NOTES: ********************
            // 

            //**************************



            var p = new ExcelPackage();
            ExcelWorksheet resultsSheet2;
            int c = 1;
            int steps = 20;
            using (p)
            {
                ExcelWorksheet resultsSheet;
                var databaseDir2 = Environment.GetEnvironmentVariable("SigStatDB");
                int samplingrate = 0;

                List<string> databases = new List<string>();
                foreach (string fileName_ in Directory.GetFiles(databaseDir2, "*.zip"))
                {
                    string[] str = fileName_.Split("\\");
                    databases.Add(str[4]);
                    Console.WriteLine(str[4]);
                    string dataBaseName = str[4];

                    VerifierBenchmark b = new VerifierBenchmark();
                    Verifier v = new Verifier();

                    ////////////////////////////////////// loader
                    if (dataBaseName == "SVC2004.zip")
                    {
                        b.Loader = new Svc2004Loader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 100;
                        steps = 20;
                        c = 1;
                        p = new ExcelPackage();
                        resultsSheet2 = p.Workbook.Worksheets.Add("Summary");
                        resultsSheet2.Cells[1, 1].Value = "Compilation";
                        resultsSheet2.Cells[1, 2].Value = "Best Freq";
                        resultsSheet2.Cells[1, 3].Value = "Avg point";
                        resultsSheet2.Cells[1, 4].Value = "AER: Best";
                        resultsSheet2.Cells[1, 5].Value = "AER: Lowest";
                        resultsSheet2.Cells[1, 6].Value = "AER: Device";

                    }

                    else if (dataBaseName == "MCYT100.zip")
                    {
                        b.Loader = new MCYTLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 100;
                        steps = 20;
                        c = 1;
                        p = new ExcelPackage();
                        resultsSheet2 = p.Workbook.Worksheets.Add("Summary");
                        resultsSheet2.Cells[1, 1].Value = "Compilation";
                        resultsSheet2.Cells[1, 2].Value = "Best Freq";
                        resultsSheet2.Cells[1, 3].Value = "Avg point";
                        resultsSheet2.Cells[1, 4].Value = "AER: Best";
                        resultsSheet2.Cells[1, 5].Value = "AER: Lowest";
                        resultsSheet2.Cells[1, 6].Value = "AER: Device";
                    }
                    else if (dataBaseName == "SigComp11_Dutch.zip")
                    {
                        b.Loader = new SigComp11DutchLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 200;
                        steps = 40;
                        c = 1;
                        p = new ExcelPackage();
                        resultsSheet2 = p.Workbook.Worksheets.Add("Summary");
                        resultsSheet2.Cells[1, 1].Value = "Compilation";
                        resultsSheet2.Cells[1, 2].Value = "Best Freq";
                        resultsSheet2.Cells[1, 3].Value = "Avg point";
                        resultsSheet2.Cells[1, 4].Value = "AER: Best";
                        resultsSheet2.Cells[1, 5].Value = "AER: Lowest";
                        resultsSheet2.Cells[1, 6].Value = "AER: Device";
                    }
                    else if (dataBaseName == "SigComp11Chinese.zip")
                    {
                        b.Loader = new SigComp11ChineseLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 200;
                        steps = 40;
                        c = 1;
                        p = new ExcelPackage();
                        resultsSheet2 = p.Workbook.Worksheets.Add("Summary");
                        resultsSheet2.Cells[1, 1].Value = "Compilation";
                        resultsSheet2.Cells[1, 2].Value = "Best Freq";
                        resultsSheet2.Cells[1, 3].Value = "Avg point";
                        resultsSheet2.Cells[1, 4].Value = "AER: Best";
                        resultsSheet2.Cells[1, 5].Value = "AER: Lowest";
                        resultsSheet2.Cells[1, 6].Value = "AER: Device";
                    }
                    else if (dataBaseName == "SigWiComp2013_Japanese.zip")
                    {
                        b.Loader = new SigComp13JapaneseLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 200;
                        steps = 100;
                        c = 1;
                        p = new ExcelPackage();
                        resultsSheet2 = p.Workbook.Worksheets.Add("Summary");
                        resultsSheet2.Cells[1, 1].Value = "Compilation";
                        resultsSheet2.Cells[1, 2].Value = "Best Freq";
                        resultsSheet2.Cells[1, 3].Value = "Avg point";
                        resultsSheet2.Cells[1, 4].Value = "AER: Best";
                        resultsSheet2.Cells[1, 5].Value = "AER: Lowest";
                        resultsSheet2.Cells[1, 6].Value = "AER: Device";


                    }
                    else
                    {
                        b.Loader = new SigComp15GermanLoader(Path.Combine(databaseDir2, dataBaseName), true);
                        samplingrate = 75;
                        p = new ExcelPackage();
                        steps = 20;
                        c = 1;
                        resultsSheet2 = p.Workbook.Worksheets.Add("Summary");
                        resultsSheet2.Cells[1, 1].Value = "Compilation";
                        resultsSheet2.Cells[1, 1].Style.Font.Bold = true;
                        resultsSheet2.Cells[1, 1].Style.Font.Size = 14;

                        resultsSheet2.Row(1).Style.Font.Size = 14;
                        resultsSheet2.Row(1).Style.Font.Bold = true;


                        for (int i = 1; i < 42; i++)
                        {
                            for (int j = 1; j < 7; j++)
                            {
                                resultsSheet2.Column(j).AutoFit();
                                resultsSheet2.Column(j).BestFit = true;
                                if (i % 2 == 0)
                                {
                                    resultsSheet2.Cells[i, j].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.DarkGray;
                                    resultsSheet2.Cells[i, j].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                    resultsSheet2.Cells[i, j].Style.WrapText = true;
                                }
                                else
                                {
                                    resultsSheet2.Cells[i, j].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.DarkGray;
                                    resultsSheet2.Cells[i, j].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                }
                            }
                        }

                        resultsSheet2.Cells[1, 2].Value = "Best Freq";
                        resultsSheet2.Cells[1, 3].Value = "Avg point";
                        resultsSheet2.Cells[1, 4].Value = "AER: Best";
                        resultsSheet2.Cells[1, 5].Value = "AER: Lowest";
                        resultsSheet2.Cells[1, 6].Value = "AER: Device";

                    }

                    //////////////////////////////////////
                    ///
                    //////////////////////////////////////////////////////////////////////////////////////////////// preprocessing

                    List<SequentialTransformPipeline> pipeline_ = new List<SequentialTransformPipeline>();

                    SequentialTransformPipeline pip0 = new SequentialTransformPipeline();
                    pip0.Items.Add(new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y });
                    pip0.Items.Add(new Scale() { InputFeature = Features.X, OutputFeature = Features.X });
                    pip0.Items.Add(new Scale() { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });
                    pip0.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X, OutputFeature = Features.X });
                    pip0.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y, OutputFeature = Features.Y });
                    pip0.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });

                    SequentialTransformPipeline pip1 = new SequentialTransformPipeline();
                    pip1.Items.Add(new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y });
                    pip1.Items.Add(new Scale() { InputFeature = Features.X, OutputFeature = Features.X });
                    pip1.Items.Add(new Scale() { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });


                    SequentialTransformPipeline pip2 = new SequentialTransformPipeline();
                    pip2.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X, OutputFeature = Features.X });
                    pip2.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y, OutputFeature = Features.Y });
                    pip2.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });

                    SequentialTransformPipeline pip3 = new SequentialTransformPipeline();
                    pip3.Add(new ZNormalization() { InputFeature = Features.X, OutputFeature = Features.X });
                    pip3.Add(new ZNormalization() { InputFeature = Features.Y, OutputFeature = Features.Y });
                    pip3.Add(new ZNormalization() { InputFeature = Features.Pressure, OutputFeature = Features.Pressure });


                    pipeline_.Add(pip0);
                    pipeline_.Add(pip1);
                    pipeline_.Add(pip2);
                    pipeline_.Add(pip3);


                    //////////////////////////////////////////////////////////////////////////////////////////////////
                    ///
                    foreach (SequentialTransformPipeline pipl in pipeline_)
                    {



                        List<OptimalDtwClassifier> classifiers = new List<OptimalDtwClassifier>();

                        //////////////////////////////////////////////////////////////////////////////// classifier
                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new EvenNSampler(10),
                            // Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                            //Features = new List<FeatureDescriptor>() { Features.X, Features.Y}
                            Features = new List<FeatureDescriptor>() { Features.X }
                        });

                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new FirstNSampler(10),
                            Features = new List<FeatureDescriptor>() { Features.X }
                        });

                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new EvenNSampler(10),
                            Features = new List<FeatureDescriptor>() { Features.Y }
                        });
                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new FirstNSampler(10),
                            Features = new List<FeatureDescriptor>() { Features.Y }
                        });
                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new FirstNSampler(10),
                            Features = new List<FeatureDescriptor>() { Features.Pressure }
                        });
                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new EvenNSampler(10),
                            Features = new List<FeatureDescriptor>() { Features.Pressure }
                        });
                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new EvenNSampler(10),
                            Features = new List<FeatureDescriptor>() { Features.X, Features.Y }
                        });
                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new FirstNSampler(10),
                            Features = new List<FeatureDescriptor>() { Features.X, Features.Y }
                        });
                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new EvenNSampler(10),
                            Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                        });
                        classifiers.Add(new OptimalDtwClassifier()
                        {
                            DistanceFunction = Accord.Math.Distance.Euclidean,
                            WarpingWindowLength = 2000,
                            Sampler = new FirstNSampler(10),
                            Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                        });


                        /////////////////////////////////////////////////////////////////////////////////////
                        foreach (OptimalDtwClassifier cla in classifiers)
                        {

                            v.Logger = new SimpleConsoleLogger();
                            v.Classifier = cla;

                            /////////////////////////////////////////////// sampler
                            if (cla.Sampler is EvenNSampler)
                                b.Sampler = new EvenNSampler(10);
                            else
                                b.Sampler = new FirstNSampler(10);
                            ///////////////////////////////////////////////




                            var samplerate = new List<SampleRateResults>();
                            int s = 1;
                            int bestFreq = 0;
                            double avgPoints = 0;
                            double bestAER = 100;
                            double lowestFreqAER = 0;
                            double deviceFreqAER = 0;
                            for (s = 1; s <= steps; s = s + 1)
                            {
                                pipl.Items.Insert(0, new SampleRate() { samplerate = s, InputX = Features.X, InputY = Features.Y, InputP = Features.Pressure, OutputX = Features.X, OutputY = Features.Y, OutputP = Features.Pressure });
                                v.Pipeline = pipl;
                                b.Verifier = v;

                                b.ProgressChanged += ProgressPrimary;

                                var result = b.Execute(true);


                                List<Signer> signers = b.Loader.EnumerateSigners(null).ToList();
                                double avg = 0;
                                int ii = 0;
                                foreach (Signer sig in signers)
                                {
                                    avg = avg + SignerStatisticsHelper.GetPointsAvg(sig);
                                    ii++;
                                }
                                avg = avg / signers.Count();


                                var obj = new SampleRateResults();
                                obj.step = s; obj.AER = result.FinalResult.Aer; obj.samplerate = samplingrate / s;
                                obj.pointsAvg = avg / obj.step; // obj.AER2 = result.FinalResult.Aer;
                                samplerate.Add(obj);

                                if (result.FinalResult.Aer < bestAER)
                                {
                                    bestFreq = samplingrate / s;
                                    avgPoints = avg / s;
                                    bestAER = result.FinalResult.Aer;
                                }

                                if (s == 1)
                                {
                                    deviceFreqAER = result.FinalResult.Aer;
                                }
                                if (s == steps)
                                {
                                    lowestFreqAER = result.FinalResult.Aer;
                                }

                                pipl.Items.RemoveAt(0);

                            }
                            string pre = "M";

                            if (b.Verifier.Pipeline == pip0)
                            {
                                pre = "_S_T";
                            }


                            if (b.Verifier.Pipeline == pip1)
                            {
                                pre = "_S";
                            }

                            if (b.Verifier.Pipeline == pip2)
                            {
                                pre = "_T";
                            }
                            if (b.Verifier.Pipeline == pip3)
                            {
                                pre = "_Z";
                            }

                            if (b.Sampler is EvenNSampler)
                                pre = pre + "_EVEN_";
                            else
                                pre = pre + "_FIRST_";

                            foreach (FeatureDescriptor f in cla.Features)
                            {
                                string[] f2 = f.ToString().Split("(");
                                pre = pre + f2[0];
                            }


                            string dBaseName = null;

                            if (dataBaseName == "SVC2004.zip")
                            {
                                dBaseName = "SVC";
                            }

                            else if (dataBaseName == "MCYT100.zip")
                            {
                                dBaseName = "MCYT";
                            }
                            else if (dataBaseName == "SigComp11_Dutch.zip")
                            {
                                dBaseName = "Dut";
                            }
                            else if (dataBaseName == "SigComp11Chinese.zip")
                            {
                                dBaseName = "Chi";
                            }
                            else if (dataBaseName == "SigWiComp2013_Japanese.zip")
                            {
                                dBaseName = "Jap";
                            }
                            else
                            {
                                dBaseName = "Ger";

                            }

                            resultsSheet = p.Workbook.Worksheets.Add(dBaseName + pre);
                            resultsSheet.InsertTable(1, 1, samplerate);

                            Console.WriteLine(c);
                            c++;
                            resultsSheet2.Cells[c, 1].Value = $"{pre}";
                            resultsSheet2.Cells[c, 2].Value = bestFreq;
                            resultsSheet2.Cells[c, 3].Value = avgPoints;
                            resultsSheet2.Cells[c, 4].Value = bestAER;
                            resultsSheet2.Cells[c, 5].Value = lowestFreqAER;
                            resultsSheet2.Cells[c, 6].Value = deviceFreqAER;
                            resultsSheet2.Cells[1, 1].Style.Font.Bold = true;
                            resultsSheet2.Cells[1, 1].Style.Font.Size = 14;

                            resultsSheet2.Row(1).Style.Font.Size = 14;
                            resultsSheet2.Row(1).Style.Font.Bold = true;


                            for (int i = 1; i < 42; i++)
                            {
                                for (int j = 1; j < 7; j++)
                                {
                                    resultsSheet2.Column(j).AutoFit();
                                    resultsSheet2.Column(j).BestFit = true;
                                    if (i % 2 == 0)
                                    {
                                        resultsSheet2.Cells[i, j].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.DarkGray;
                                        resultsSheet2.Cells[i, j].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                        resultsSheet2.Cells[i, j].Style.WrapText = true;
                                    }
                                    else
                                    {
                                        resultsSheet2.Cells[i, j].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.DarkGray;
                                        resultsSheet2.Cells[i, j].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                    }
                                }
                            }
                            for (int i = 1; i < 7; i++)
                                resultsSheet2.Column(i).AutoFit();



                            // B1:B21,D1:D21
                            InsertLineChart(resultsSheet, resultsSheet.Cells["B2:C21"], 2, 6, "Points", "Sample frequency", "AER", resultsSheet.Cells["C1:D1"], 400, 300, $"{dBaseName}_{pre}");
                            InsertLineChart(resultsSheet, resultsSheet.Cells["D2:E21"], 2, 16, "Frequency", "Points average", "AER", resultsSheet.Cells["B1:D1"], 400, 300, $"{dBaseName}_{pre}");


                        }
                    }

                    string[] str2 = dataBaseName.Split(".");
                    p.SaveAs(new FileInfo($"C:/Users/Saleem/OneDrive/Mohammad/Ideas/SampleRate/compilation/{str2[0]}.xlsx"));


                }





            }
        }

        public static void InsertLineChart(ExcelWorksheet ws, ExcelRange range, int row, int col, string name, string xLabel = null, string yLabel = null, ExcelRange SerieLabels = null, int width = -1, int height = -1, string title = null)
        {

            var chart = (ExcelScatterChart)ws.Drawings.AddChart(name, OfficeOpenXml.Drawing.Chart.eChartType.XYScatterLines);
            //set the chart's position
            chart.SetPosition(row, 0, col, 0);
            //x axis range
            var xRange = ws.Cells[range.Start.Row, range.Start.Column, range.End.Row, range.Start.Column];

            //Load series to the graph
            for (int i = range.Start.Column + 1, j = 0; i <= range.End.Column; i++, j++)
            {
                var series = chart.Series.Add(ws.Cells[range.Start.Row, i, range.End.Row, i], xRange);
                series.HeaderAddress = ws.Cells[SerieLabels.Start.Row, SerieLabels.Start.Column + j];

                series.Fill.Color = System.Drawing.Color.Blue;
                series.Border.LineCap = OfficeOpenXml.Drawing.eLineCap.Round;

            }

            //If title is explicitly defined, set it, else it is the same as the graph's id
            chart.Title.Text = (title == null) ? name : title;




            //set axis labels
            chart.YAxis.Title.Text = yLabel;
            chart.XAxis.Title.Text = xLabel;
            //Format graph to look aesthetic
            chart.Legend.Position = eLegendPosition.Bottom;
            //chart.DataLabel.ShowLeaderLines = true;
            chart.YAxis.MajorGridlines.Fill.Color = System.Drawing.Color.LightGray;
            chart.YAxis.Border.Fill.Color = System.Drawing.Color.Transparent;
            chart.XAxis.Border.Fill.Color = System.Drawing.Color.Transparent;
            chart.XAxis.MajorGridlines.Fill.Color = System.Drawing.Color.LightGray;
            chart.XAxis.Title.Font.Size = 14;
            chart.YAxis.Title.Font.Size = 14;
            chart.RoundedCorners = false;
            // chart.Legend.Remove();
            //If chart's size is defined (and is valid) set the size
            if (width > 0 && height > 0)
            {
                chart.SetSize(width, height);
            }
            // chart.Style = eChartStyle.Style1;

        }


        private static void DtwWindowTest()
        {
            var p = new ExcelPackage();
            string fileName = "Japanese";



            using (p)
            {

                var resultsSheet = p.Workbook.Worksheets.Add($"Results");
                resultsSheet.Cells[1, 1].Value = "Window";
                resultsSheet.Cells[1, 2].Value = "AER";
                var samplerate = new List<SampleRateResults>();
                int i = 1;
                int w = 2000;
                for (i = 1; i <= 40; i = i + 1)
                {
                    var databaseDir = Environment.GetEnvironmentVariable("SigStatDB");
                    var benchmark = new VerifierBenchmark()
                    {
                        //mcyt100 SigComp11_Dutch SigComp11Chinese SigWiComp2015_German SigWiComp2013_Japanese
                        Loader = new SigComp13JapaneseLoader(Path.Combine(databaseDir, "SigWiComp2013_Japanese.zip"), true),


                        Verifier = new Verifier()
                        {
                            Pipeline = new SequentialTransformPipeline
                    {
           // new SampleRate(){samplerate=s,InputX=Features.X, InputY=Features.Y, InputP = Features.Pressure, OutputX = Features.X, OutputY=Features.Y,OutputP=Features.Pressure},
           //  new SampleRate(){samplerate=s,InputX=Features.X, InputY=Features.Y, InputP = Features.Pressure, OutputX = Features.X, OutputY=Features.Y},

       //      new OrthognalRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},
              new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                 new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
                      new TranslatePreproc(OriginType.CenterOfGravity){InputFeature = Features.X, OutputFeature = Features.X},
           new TranslatePreproc(OriginType.CenterOfGravity){InputFeature = Features.Y, OutputFeature = Features.Y},
            // new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},
         //   new ZNormalization(){InputFeature = Features.X, OutputFeature = Features.X},
          //   new ZNormalization(){InputFeature = Features.Y, OutputFeature = Features.Y},
       //      new ZNormalization(){InputFeature = Features.Pressure, OutputFeature = Features.Pressure},
       //      new ZNormalization(){InputFeature = Features.Pressure, OutputFeature = Features.Pressure}
       
                        }
                        ,
                            Classifier = new OptimalDtwClassifier()
                            {
                                DistanceFunction = Accord.Math.Distance.Euclidean,
                                WarpingWindowLength = w / i,
                                Sampler = new FirstNSampler(10),
                                // Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                                Features = new List<FeatureDescriptor>() { Features.X, Features.Y }
                            }
                        },
                        Sampler = new FirstNSampler(10),
                        Logger = new SimpleConsoleLogger(),

                    };

                    benchmark.ProgressChanged += ProgressPrimary;

                    var result = benchmark.Execute(true);

                    resultsSheet.Cells[i + 1, 1].Value = w / i;
                    resultsSheet.Cells[i + 1, 2].Value = result.FinalResult.Aer;


                }
                fileName = fileName + "_S_T";
                // fileName = fileName + "_S";
                // fileName = fileName + "_Z";
                // fileName = fileName + "_T";

                //  fileName = fileName + "_EVEN";
                fileName = fileName + "_First";
                Console.WriteLine(fileName);
                p.SaveAs(new FileInfo($"C:/Users/Saleem/OneDrive/Mohammad/Ideas/SampleRate/window/{fileName}.xlsx"));
            }
        }

        private static void SignatureToImageTesting()
        {
            var databaseDir = Environment.GetEnvironmentVariable("SigStatDB");
            Svc2004Loader Loader = new Svc2004Loader(Path.Combine(databaseDir, "SVC2004.zip"), true);
            Signature s1 = Loader.EnumerateSigners(p => (p.ID == "05")).ToList()[0].Signatures[10];//signer 10, signature 10
            Signature s2 = s1;

            var tfs = new SequentialTransformPipeline
            {
                new ParallelTransformPipeline
                {

                   new Normalize() { Input = Features.X, Output = Features.X },
                   new Normalize() { Input = Features.Y, Output = Features.Y },
                },
                new RealisticImageGenerator(1280, 720)
            };
            tfs.Logger = new SimpleConsoleLogger();
            tfs.Transform(s1);

            ImageSaver.Save(s1, $"{s1.Signer.ID }_{s1.ID }_0BeforeTest.png");

            var tfs2 = new SequentialTransformPipeline
            {
                new ParallelTransformPipeline
                {

                   new Normalize() { Input = Features.X, Output = Features.X },
                   new Normalize() { Input = Features.Y, Output = Features.Y },
                   new OrthognalRotation() {InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y}
                },
                new RealisticImageGenerator(1280, 720)
            };
            tfs2.Logger = new SimpleConsoleLogger();
            tfs2.Transform(s2);
            ImageSaver.Save(s2, $"{s2.Signer.ID }_{s2.ID }_AfterNormalizationTest.png");
            int i = 0;

            foreach (double y in s1.GetFeature(Features.Y))
            {
                Console.WriteLine("Y1=  " + y + "  Y2= " + s2.GetFeature(Features.Y)[i] + " dif=  " + (s1.GetFeature(Features.Y)[i] - s2.GetFeature(Features.Y)[i]));
                i++;
            }


        }





        private static void RenderDatabase()
        {
            SigComp11DutchLoader loader = new SigComp11DutchLoader("Dutch_renamed.zip", true);
            var signatures = loader.EnumerateSigners().Take(10);
            List<Signer> signers = loader.EnumerateSigners(null).ToList();

            var pipeline = new SequentialTransformPipeline
            {
                new UniformScale() { BaseDimension=Features.X, ProportionalDimension = Features.Y, BaseDimensionOutput=Features.X, ProportionalDimensionOutput=Features.Y },
                new Normalize() { Input=Features.Pressure, Output=Features.Pressure },
                new RealisticImageGenerator(1280, 720),
            };
            pipeline.Logger = new SimpleConsoleLogger();

            foreach (var signer in signers)
            {
                foreach (var signature in signer.Signatures)
                {
                    pipeline.Transform(signature);
                    ImageSaver.Save(signature, signature.ID + ".png");
                }
            }
            return;
        }

        private static void PreprocessingBenchmarkDemo()
        {
            ////var config = BenchmarkConfig.FromJsonFile(path);
            BenchmarkConfig config = new BenchmarkConfig()
            {
                Classifier = "OptimalDtw",
                Sampling = "S1",
                Database = "GERMAN",
                Rotation = true,
                Translation_Scaling = ("BottomLeftToOrigin", "None"),
                ResamplingType_Filter = "P_FillPenUp",
                ResamplingParam = 0,
                Interpolation = "Linear",
                Features = "P",
                Distance = "Euclidean"
            };
            //var configs = BenchmarkConfig.GenerateConfigurations();
            //var myConfig = configs.Single(s => s.ToShortString() == config.ToShortString());
            ////var benchmarks = configs.Select(c => BenchmarkBuilder.Build(c)).ToList();


            //var benchmark = BenchmarkBuilder.Build(myConfig);
            //benchmark.Logger = new SimpleConsoleLogger(Microsoft.Extensions.Logging.LogLevel.Trace);
            //benchmark.Execute(false);
            //benchmark.Dump("tmp.xlsx", config.ToKeyValuePairs());


            var configs = BenchmarkConfig.GenerateConfigurations();
            var myConfig = configs.Single(s => s.ToShortString() == config.ToShortString());
            //var benchmarks = configs.Select(c => BenchmarkBuilder.Build(c)).ToList();


            var benchmark = BenchmarkBuilder.Build(myConfig);
            SerializationHelper.JsonSerializeToFile(benchmark, @"VerifierBenchmarkSerialized.txt");
            benchmark = SerializationHelper.DeserializeFromFile<VerifierBenchmark>(@"VerifierBenchmarkSerialized.txt");

            benchmark.Logger = new SimpleConsoleLogger(Microsoft.Extensions.Logging.LogLevel.Trace);
            benchmark.Execute(false);
            benchmark.Dump("tmp.xlsx", config.ToKeyValuePairs());


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

            // Define complex feature values
            var loops = new List<Loop>() { new Loop(1, 1), new Loop(3, 3) };

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

        static void DatabaseLoaderDemo()
        {
            var databaseDir = Environment.GetEnvironmentVariable("SigStatDB");
            //Load signatures from local database
            //SigComp15GermanLoader loader = new SigComp15GermanLoader(Path.Combine(databaseDir, "SigWiComp2015_German.zip").GetPath(), true);
            //SigComp15GermanLoader loader = new SigComp15GermanLoader(@"Databases\SigWiComp2015_German.zip".GetPath(), true);
            //SigComp11ChineseLoader loader = new SigComp11ChineseLoader(Path.Combine(databaseDir, "SigComp11Chinese.zip").GetPath(), true);
            SigComp13JapaneseLoader loader = new SigComp13JapaneseLoader(Path.Combine(databaseDir, "SigWiComp2013_Japanese.zip").GetPath(), true);
            var signers = loader.EnumerateSigners().ToList();
            Console.WriteLine($"{signers.Count} signers loaded with {signers.SelectMany(s => s.Signatures).Count()} signatures");
        }

        static void LoadSignaturesFromDatabase(out List<Signature> genuines, out Signature challenge)
        {
            //Load signatures from local database
            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true);
            var signer = new List<Signer>(loader.EnumerateSigners(p => p.ID == "01"))[0];//Load the first signer only
            genuines = signer.Signatures.Where(s => s.Origin == Origin.Genuine).ToList();
            challenge = genuines[0];
        }

        static void TransformationPipeline()
        {
            Signature offlineSignature = ImageLoader.LoadSignature(@"Databases\Offline\Images\004_e_001.png".GetPath());

            // Initialize a transformation using object initializer
            var resize = new Resize()
            {
                Width = 60,
                InputImage = Features.Image,
                OutputImage = Features.Image
            };
            // Perform transformation
            resize.Transform(offlineSignature);

            // Initialize a transformation using fluent syntax
            var binarization = new Binarization()
            {
                InputImage = Features.Image,
                OutputMask = MyFeatures.Binarized
            };

            // Perform transformation
            binarization.Transform(offlineSignature);

            // Consume results
            var binaryImage = offlineSignature.GetFeature(MyFeatures.Binarized);
            WriteToConsole(binaryImage);

            // Create signature with online features
            Signature sig = new Signature("Demo");
            sig.SetFeature(Features.X, new List<double> { 1, 1, 2, 2, 2, 2, 1, 1 });
            var x = sig.GetFeature(Features.X);
            Console.WriteLine(string.Join(", ", x));

            // Connect a series of transformations into a pipeline
            SequentialTransformPipeline pipeline = new SequentialTransformPipeline()
            {
                new Multiply(2) { InputList = Features.X },
                new AddConst(3), // no need to specify input, it will work with the output of the previous transformation
            };

            pipeline.Transform(sig);
            Console.WriteLine(string.Join(", ", x));
        }

        static void Classifier()
        {
            LoadSignaturesFromDatabase(out var genuines, out var challenge);

            IClassifier classifier = new DtwClassifier()
            {
                Features = { Features.X, Features.Y, Features.T }
            };
            ISignerModel model = classifier.Train(genuines);//Train on genuine signatures
            var result = classifier.Test(model, challenge);

            Console.WriteLine("Classification result: " + (result > 0.5 ? "Genuine" : "Forged"));
        }


        /// <summary>
        /// Read signature from image, extract features, generate new image
        /// </summary>
        static void OfflineVerifierDemo()
        {
            var verifier = new Verifier(new SimpleConsoleLogger())
            {
                Pipeline = new SequentialTransformPipeline
                {
                    new Binarization(){
                        InputImage = Features.Image
                    },
                    new Trim(5),
                    new ImageGenerator(true),
                    new HSCPThinning(),
                    new ImageGenerator(true),
                    new OnePixelThinning() { Output = MyFeatures.Skeleton },
                    new ImageGenerator(true),
                    //new BaselineExtraction(),
                    //new LoopExtraction(),
                    new EndpointExtraction(),
                    new ComponentExtraction(5) { Skeleton = MyFeatures.Skeleton },
                    new ComponentSorter(),
                    new ComponentsToFeatures(),
                    new ParallelTransformPipeline
                    {
                        new Normalize() { Input = Features.X },
                        new Normalize() { Input = Features.Y }
                    },
                    new ApproximateOnlineFeatures(),
                    new RealisticImageGenerator(1280, 720),
                },
                Classifier = new WeightedClassifier()
            };

            Signature s1 = ImageLoader.LoadSignature(@"Databases\Offline\Images\U1S1.png".GetPath());
            s1.Origin = Origin.Genuine;
            Signer s = new Signer();
            s.Signatures.Add(s1);

            verifier.Train(new List<Signature> { s1 });

            //TODO: ha mar Verifier demo, akkor Test()-et is hasznaljuk..
            ImageSaver.Save(s1, @"GeneratedOfflineImage.png");
        }

        static void OnlineVerifierDemo()
        {
            var timer1 = FeatureDescriptor.Register("Timer1", typeof(DateTime));//TODO: <DateTime> template-tel mukodjon

            var verifier = new Verifier(new SimpleConsoleLogger())
            {
                Pipeline = new SequentialTransformPipeline
                {
                    new ParallelTransformPipeline
                    {
                        new Normalize() { Input = Features.Pressure },
                        new Map(0, 1) { Input = Features.X },
                        new Map(0, 1) { Input = Features.Y },
                        //new TimeReset(),
                    },
                    //new CentroidTranslate(),//is a sequential pipeline of other building blocks
                    //new TangentExtraction(),
                    /*new AlignmentNormalization(Alignment.Origin),
                    new Paper13FeatureExtractor(),*/

        },
                Classifier = new DtwClassifier()
                {
                    Features = { Features.Pressure }
                }
                //Classifier = new WeightedClassifier
                //{
                //    {
                //        (new DtwClassifier(Accord.Math.Distance.Manhattan)
                //        {
                //            Features = { Features.X, Features.Y }
                //        },
                //           0.15)
                //    },
                //    {
                //        (new DtwClassifier(){
                //            Features = { Features.Pressure }
                //        }, 0.3)
                //    },
                //    {
                //        (new DtwClassifier(){
                //            Features = { MyFeatures.Tangent }
                //        }, 0.55)
                //    },
                //    //{
                //    //    (new MultiDimensionKolmogorovSmirnovClassifier
                //    //    {
                //    //        Features = {"X", "Y" },
                //    //        ThresholdStrategy = ThresholdStrategies.AveragePlusDeviance
                //    //    },
                //    //    0.8)
                //    //}
                //}
            };

            Svc2004Loader loader = new Svc2004Loader(@"Databases\ger.rar".GetPath(), true);
            var signers = new List<Signer>(loader.EnumerateSigners(p => p.ID == "01"));//Load the first signer only

            List<Signature> references = signers[0].Signatures.GetRange(0, 10);
            verifier.Train(references);

            Signature questioned1 = signers[0].Signatures[0];
            Signature questioned2 = signers[0].Signatures[25];
            bool isGenuine1 = verifier.Test(questioned1) > 0.5;//true
            bool isGenuine2 = verifier.Test(questioned2) > 0.5;//false
        }

        static void OnlineRotationBenchmarkDemo()
        {

            var databaseDir = Environment.GetEnvironmentVariable("SigStatDB");
            var benchmark = new VerifierBenchmark()
            {

                //Loader = new SigComp11ChineseLoader(Path.Combine(databaseDir, "SigComp11Chinese.zip"), true),
                Loader = new Svc2004Loader(Path.Combine(databaseDir, "SVC2004.zip"), true),

                Verifier = new Verifier()
                {
                    Pipeline = new SequentialTransformPipeline
                    {
                                             //  new OrthognalRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},


                             new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                               new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
                            new TranslatePreproc(OriginType.CenterOfGravity){InputFeature = Features.X, OutputFeature = Features.X},
                            new TranslatePreproc(OriginType.CenterOfGravity){InputFeature = Features.Y, OutputFeature = Features.Y},
                      //new NormalizeRotationForX(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},

                    new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},


                        }
                ,
                    Classifier = new OptimalDtwClassifier()
                    {
                        DistanceFunction = Accord.Math.Distance.Euclidean,
                        Sampler = new FirstNSampler(10),
                        Features = new List<FeatureDescriptor>() { Features.X }
                    }
                },
                Sampler = new FirstNSampler(10),
                Logger = new SimpleConsoleLogger(),
            };


            benchmark.ProgressChanged += ProgressPrimary;
            //benchmark.Verifier.ProgressChanged += ProgressSecondary;

            //var result = benchmark.Execute(true);

            //benchmark.Dump("results.xlsx", new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("chinese11", "scale, COG") });
            //using (var p = new ExcelPackage())
            //{
            //    var summarySheet = p.Workbook.Worksheets.Add("Summary");
            //    summarySheet.Cells[2, 2].Value = "Preprocessing benchmark";
            //    summarySheet.Cells[3, 2].Value = DateTime.Now.ToString();

            //    var resultsSheet = p.Workbook.Worksheets.Add("Results");
            //   resultsSheet.InsertTable(1, 1, result.SignerResults);

            //    resultsSheet.InsertTable(1, 1, new object[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

            //    p.SaveAs(new FileInfo("Report.xlsx"));
            //}

            var signers = new Svc2004Loader(Path.Combine(databaseDir, "SVC2004.zip"), true).EnumerateSigners().ToList();
            //signers[2].Signatures;
            SignatureHelper.SaveImage(signers[1].Signatures[2], "signature1-2.png");


            //foreach (var signerResult in result.SignerResults)
            //{
            //    Console.WriteLine($"{signerResult.Signer} {signerResult.Aer}");
            //}
            //Console.WriteLine($"AER: {result.FinalResult.Aer}");

        }

        static void OnlineVerifierBenchmarkDemo()
        {
            var databaseDir = Environment.GetEnvironmentVariable("SigStatDB");
            var benchmark = new VerifierBenchmark()
            {
                Loader = new SigComp13JapaneseLoader(Path.Combine(databaseDir, "SigWiComp2013_Japanese.zip").GetPath(), true),
                Verifier = new Verifier()
                {
                    Pipeline = new SequentialTransformPipeline
                    {
                        //new TranslatePreproc(OriginType.CenterOfGravity) {InputFeature = Features.X, OutputFeature=Features.X },
                        //new TranslatePreproc(OriginType.CenterOfGravity) {InputFeature = Features.Y, OutputFeature=Features.Y },
                        //new UniformScale() {BaseDimension = Features.X, ProportionalDimension = Features.Y, BaseDimensionOutput = Features.X, ProportionalDimensionOutput = Features.Y},
                        //new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},

                        // new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                        //   new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
                        //new ResampleSamplesCountBased() {
                        //    InputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                        //    OutputFeatures = new List<FeatureDescriptor<List<double>>>() {Features.X, Features.Y, Features.Pressure},
                        //    InterpolationType = typeof(CubicInterpolation),
                        //    NumOfSamples = 500,
                        //    OriginalTFeature = Features.T,
                        //    ResampledTFeature = Features.T,
                        //},
                        //new FilterPoints() { KeyFeatureInput = Features.Pressure, KeyFeatureOutput = Features.Pressure,
                        //InputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y },
                        //OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y }},
                        //new FillPenUpDurations()
                        //{
                        //    InputFeatures = new List<FeatureDescriptor<List<double>>>(){ Features.X, Features.Y, Features.Pressure },
                        //    OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                        //    InterpolationType = typeof(CubicInterpolation),
                        //    TimeInputFeature =Features.T,
                        //    TimeOutputFeature = Features.T
                        //}
                    }
                ,
                    Classifier = new OptimalDtwClassifier()
                    {
                        Sampler = new FirstNSampler(10),
                        Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                    }
                },
                Sampler = new FirstNSampler(10),
                Logger = new SimpleConsoleLogger(),
            };

            benchmark.ProgressChanged += ProgressPrimary;
            //benchmark.Verifier.ProgressChanged += ProgressSecondary;

            var result = benchmark.Execute(true);

            Console.WriteLine($"AER: {result.FinalResult.Aer}");
        }

        static void OnlineToImage()
        {
            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true);
            Signature s1 = loader.EnumerateSigners(p => (p.ID == "10")).ToList()[0].Signatures[10];//signer 10, signature 10

            var tfs = new SequentialTransformPipeline
            {
                new ParallelTransformPipeline
                {
                    new Normalize() { Input = Features.X, Output = Features.X },
                    new Normalize() { Input = Features.Y, Output = Features.Y }
                },
                new RealisticImageGenerator(1280, 720)
            };
            tfs.Logger = new SimpleConsoleLogger();
            tfs.Transform(s1);

            ImageSaver.Save(s1, @"GeneratedOnlineImage.png");
        }

        static void GenerateOfflineDatabase()
        {
            string path = @"Databases\Offline\Generated\".GetPath();
            Directory.CreateDirectory(path);

            //Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            MCYTLoader loader = new MCYTLoader(@"Databases\Online\MCYT100\MCYT_Signature_100.zip".GetPath(), true);

            List<Signer> signers = loader.EnumerateSigners(null).ToList();

            var pipeline = new SequentialTransformPipeline
            {
                new UniformScale() { BaseDimension=Features.X, ProportionalDimension = Features.Y, BaseDimensionOutput=Features.X, ProportionalDimensionOutput=Features.Y },
                new Normalize() { Input=Features.Pressure, Output=Features.Pressure },
                new Normalize() { Input=Features.Altitude, Output=Features.Altitude },
                new Map(0, Math.PI/2) { Input=Features.Altitude, Output=Features.Altitude },
                new Map(0, 2*Math.PI) { Input=Features.Azimuth, Output=Features.Azimuth },
                new RealisticImageGenerator(1280, 720),
            };
            pipeline.Logger = new SimpleConsoleLogger();

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

        static void ClassificationBenchmark()
        {
            var benchmark = new VerifierBenchmark()
            {
                Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true),
                Verifier = new Verifier()
                {
                    Pipeline = new SequentialTransformPipeline {
                        new Scale() {InputFeature = Features.X, OutputFeature= Features.X },
                        new Scale() {InputFeature = Features.Y, OutputFeature= Features.Y },

                        new FilterPoints() { KeyFeatureInput = Features.Pressure, KeyFeatureOutput = Features.Pressure,
                        InputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y },
                        OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y }},
                    },

                    Classifier = new DtwClassifier()
                    {
                        Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure },
                        MultiplicationFactor = 1.7
                    }
                },
                Sampler = new UniversalSampler(3, 10),
                Logger = new SimpleConsoleLogger(),
            };

            benchmark.ProgressChanged += ProgressPrimary;
            //benchmark.Verifier.ProgressChanged += ProgressSecondary;

            var result = benchmark.Execute(true);

            Console.WriteLine($"AER: {result.FinalResult.Aer}");

        }

        //static void TestPreprocessingTransformations()
        //{
        //    Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true);
        //    var signer = new List<Signer>(loader.EnumerateSigners(p => p.ID == "32"))[0];//Load the first signer only

        //    Signature signature = signer.Signatures[13];

        //    string selectedTransformation = "Translate";
        //    //string selectedTransformation = "UniformScale";
        //    //string selectedTransformation = "Scale";
        //    //string selectedTransformation = "NormalizeRotation";
        //    //string selectedTransformation = "ResampleTimeBased";
        //    //string selectedTransformation = "ResampleSamplesCountBased";
        //    //string selectedTransformation = "FillPenUpDurations";
        //    //string selectedTransformation = "FilterPoints";

        //    if (selectedTransformation == "Translate")
        //    {
        //        var originalValues = signature.GetFeature(Features.X);

        //        new TranslatePreproc(OriginType.CenterOfGravity)
        //        {
        //            InputFeature = Features.X,
        //            OutputFeature = FeatureDescriptor.Get<List<double>>(OriginType.CenterOfGravity + "TranslationResult"),
        //        }.Transform(signature);
        //        var cogValues = signature.GetFeature<List<double>>(OriginType.CenterOfGravity + "TranslationResult");

        //        new TranslatePreproc(OriginType.Minimum)
        //        {
        //            InputFeature = Features.X,
        //            OutputFeature = FeatureDescriptor.Get<List<double>>(OriginType.Minimum + "TranslationResult")
        //        }.Transform(signature);
        //        var minValues = signature.GetFeature<List<double>>(OriginType.Minimum + "TranslationResult");

        //        new TranslatePreproc(OriginType.Maximum)
        //        {
        //            InputFeature = Features.X,
        //            OutputFeature = FeatureDescriptor.Get<List<double>>(OriginType.Maximum + "TranslationResult")
        //        }.Transform(signature);
        //        var maxValues = signature.GetFeature<List<double>>(OriginType.Maximum + "TranslationResult");

        //        new TranslatePreproc(100.0)
        //        {
        //            InputFeature = Features.X,
        //            OutputFeature = FeatureDescriptor.Get<List<double>>(OriginType.Predefined + "TranslationResult")
        //        }.Transform(signature);
        //        var predefValues = signature.GetFeature<List<double>>(OriginType.Predefined + "TranslationResult");

        //        string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
        //        StreamWriter sw = new StreamWriter(outputFileName);
        //        sw.WriteLine("Original ; COG translated ; Min translated ; Max translated ; 100.0 translated");
        //        for (int i = 0; i < signature.GetFeature(Features.X).Count; i++)
        //        {
        //            sw.WriteLine($"{originalValues[i]};{cogValues[i]};{minValues[i]};{maxValues[i]};{predefValues[i]}");
        //        }
        //        sw.Close();

        //        //TODO: .net core-ban ez így nem működik
        //        //Process.Start(outputFileName);
        //    }
        //    else if (selectedTransformation == "UniformScale")
        //    {
        //        var originalXValues = signature.GetFeature(Features.X);
        //        var originalYValues = signature.GetFeature(Features.Y);


        //        new UniformScale()
        //        {
        //            BaseDimension = Features.X,
        //            ProportionalDimension = Features.Y,
        //            NewMinBaseValue = 100,
        //            NewMaxBaseValue = 200,
        //            NewMinProportionalValue = 150,
        //            BaseDimensionOutput = FeatureDescriptor.Get<List<double>>("UniformScalingResultBaseDim"),
        //            ProportionalDimensionOutput = FeatureDescriptor.Get<List<double>>("UniformScalingResultProportionalDim")
        //        }.Transform(signature);
        //        var defScaledXValues = new List<double>(signature.GetFeature<List<double>>("UniformScalingResultBaseDim"));
        //        var defScaledYValues = new List<double>(signature.GetFeature<List<double>>("UniformScalingResultProportionalDim"));


        //        new UniformScale
        //        {
        //            BaseDimension = Features.X,
        //            ProportionalDimension = Features.Y,
        //            BaseDimensionOutput = FeatureDescriptor.Get<List<double>>("UniformScalingResultBaseDim"),
        //            ProportionalDimensionOutput = FeatureDescriptor.Get<List<double>>("UniformScalingResultProportionalDim")
        //        }.Transform(signature);
        //        var autoScaledXValues = new List<double>(signature.GetFeature<List<double>>("UniformScalingResultBaseDim"));
        //        var autoScaledYValues = new List<double>(signature.GetFeature<List<double>>("UniformScalingResultProportionalDim"));

        //        string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
        //        StreamWriter sw = new StreamWriter(outputFileName);
        //        sw.WriteLine("OriginalX; OriginalY ; DefScaledX ; DefScaledY ; AutoScaledX; AutoScaledY");
        //        for (int i = 0; i < signature.GetFeature(Features.X).Count; i++)
        //        {
        //            sw.WriteLine($"{originalXValues[i]};{originalYValues[i]};{defScaledXValues[i]};{defScaledYValues[i]};{autoScaledXValues[i]}; {autoScaledYValues[i]}");
        //        }
        //        sw.Close();

        //        //Process.Start(outputFileName);
        //    }
        //    else if (selectedTransformation == "Scale")
        //    {
        //        var originalXValues = signature.GetFeature(Features.X);
        //        new Scale()
        //        {
        //            InputFeature = Features.X,
        //            NewMinValue = 100,
        //            NewMaxValue = 500,
        //            OutputFeature = FeatureDescriptor.Get<List<double>>("ScalingResult")
        //        }.Transform(signature);
        //        var defScaledXValues = new List<double>(signature.GetFeature<List<double>>("ScalingResult"));

        //        new Scale
        //        {
        //            InputFeature = Features.X,
        //            OutputFeature = FeatureDescriptor.Get<List<double>>("ScalingResult")
        //        }.Transform(signature);
        //        var autoScaledXValues = new List<double>(signature.GetFeature<List<double>>("ScalingResult"));

        //        string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
        //        StreamWriter sw = new StreamWriter(outputFileName);
        //        sw.WriteLine("OriginalX;  DefScaledX ;  AutoScaledX");
        //        for (int i = 0; i < signature.GetFeature(Features.X).Count; i++)
        //        {
        //            sw.WriteLine($"{originalXValues[i]};{defScaledXValues[i]};{autoScaledXValues[i]}");
        //        }
        //        sw.Close();

        //        //Process.Start(outputFileName);
        //    }
        //    else if (selectedTransformation == "NormalizeRotation")
        //    {
        //        ////Deform
        //        //var xValues = signature.GetFeature(Features.X);
        //        //var yValues = signature.GetFeature(Features.Y);

        //        //double cosa = 1 / Math.Sqrt(2);
        //        //double sina = 1 / Math.Sqrt(2);

        //        //for (int i = 0; i < xValues.Count; i++)
        //        //{
        //        //    double x = xValues[i];
        //        //    double y = yValues[i];
        //        //    xValues[i] = x * cosa - y * sina;
        //        //    yValues[i] = x * sina + y * cosa;
        //        //}


        //        //var originalTValues = new List<double>(signature.GetFeature(Features.T));
        //        var originalXValues = new List<double>(signature.GetFeature(Features.X));
        //        var originalYValues = new List<double>(signature.GetFeature(Features.Y));


        //        var tfsOriginal = new SequentialTransformPipeline
        //        {

        //            new UniformScale() {
        //                 BaseDimension = Features.X,
        //                 ProportionalDimension = Features.Y,
        //                 BaseDimensionOutput = Features.X,
        //                 ProportionalDimensionOutput =Features.Y
        //             },
        //            new RealisticImageGenerator(1280, 720)
        //        };
        //        tfsOriginal.Logger = new SimpleConsoleLogger();
        //        tfsOriginal.Transform(signature);
        //        //var imggen = new RealisticImageGenerator(1280, 720)
        //        //{

        //        //    Logger = new SimpleConsoleLogger()
        //        //};
        //        //imggen.Transform(signature);
        //        ImageSaver.Save(signature, @"GeneratedOnlineImageBase.png");


        //        signature.SetFeature(Features.X, new List<double>(originalXValues));
        //        signature.SetFeature(Features.Y, new List<double>(originalYValues));

        //        new NormalizeRotation()
        //        {
        //        }.Transform(signature);
        //        var rotatedXValues = new List<double>(signature.GetFeature(Features.X));
        //        var rotatedYValues = new List<double>(signature.GetFeature(Features.Y));

        //        var tfsRotated = new SequentialTransformPipeline
        //        {

        //            new UniformScale() {
        //                 BaseDimension = Features.X,
        //                 ProportionalDimension = Features.Y,
        //                 BaseDimensionOutput = Features.X,
        //                 ProportionalDimensionOutput =Features.Y
        //             },
        //            new RealisticImageGenerator(1280, 720)
        //        };
        //        tfsRotated.Logger = new SimpleConsoleLogger();
        //        tfsRotated.Transform(signature);

        //        //imggen.Transform(signature);
        //        ImageSaver.Save(signature, @"GeneratedOnlineImageRotated.png");

        //        string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
        //        StreamWriter sw = new StreamWriter(outputFileName);
        //        sw.WriteLine("OriginalX; OriginalY; RotatedX ; RotatedY");
        //        for (int i = 0; i < signature.GetFeature(Features.X).Count; i++)
        //        {
        //            sw.WriteLine($"{originalXValues[i]};{originalYValues[i]};{rotatedXValues[i]};{rotatedYValues[i]}");
        //        }
        //        sw.Close();

        //        //Process.Start(outputFileName);
        //    }
        //    else if (selectedTransformation == "ResampleTimeBased")
        //    {

        //        var imggen = new RealisticImageGenerator(1280, 720)
        //        {

        //            Logger = new SimpleConsoleLogger()
        //        };
        //        imggen.Transform(signature);
        //        ImageSaver.Save(signature, @"GeneratedOnlineImageBaseSampled.png");

        //        List<FeatureDescriptor<List<double>>> features = new List<FeatureDescriptor<List<double>>>(
        //            new FeatureDescriptor<List<double>>[]
        //            {
        //                Features.X, Features.Y, Features.Pressure, Features.Azimuth, Features.Altitude
        //            });

        //        var originalTimestamps = new List<double>(signature.GetFeature(Features.T));
        //        var originalValues = new List<double>[features.Count];
        //        for (int i = 0; i < features.Count; i++)
        //        {
        //            originalValues[i] = new List<double>(signature.GetFeature(features[i]));
        //        }

        //        var resampler = new ResampleTimeBased()
        //        {
        //            InputFeatures = features,
        //            OutputFeatures = features,
        //            TimeSlot = 20,
        //            InterpolationType = typeof(CubicInterpolation)
        //            //Interpolation = new LinearInterpolation()
        //        };
        //        resampler.Transform(signature);

        //        //kisebb timeslotra mint az eredeti nem meg mert a penupot is nézi a kirajzoló
        //        imggen.Transform(signature);
        //        ImageSaver.Save(signature, @"GeneratedOnlineImageResampled.png");

        //        var resampledValues = new List<double>[features.Count];
        //        for (int i = 0; i < features.Count; i++)
        //        {
        //            resampledValues[i] = new List<double>(signature.GetFeature(features[i]));
        //        }

        //        string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
        //        StreamWriter sw = new StreamWriter(outputFileName);
        //        sw.WriteLine("OriginalT; OriginalX; OriginalY ; OriginalP ; OriginalAz; OriginalAl ;" +
        //            "ResampledT; ResampledX; ResampledY ; ResampledP ; ResampledAz; ResampledAl ");
        //        var min = originalTimestamps.Count <= resampler.ResampledTimestamps.Count ? originalTimestamps.Count : resampler.ResampledTimestamps.Count;
        //        var max = originalTimestamps.Count <= resampler.ResampledTimestamps.Count ? resampler.ResampledTimestamps.Count : originalTimestamps.Count;
        //        var isOriginalMin = originalTimestamps.Count <= resampler.ResampledTimestamps.Count ? true : false;
        //        for (int i = 0; i < max; i++)
        //        {
        //            if (i < min)
        //            {
        //                sw.WriteLine(
        //                    $"{originalTimestamps[i]}; {originalValues[0][i]};{originalValues[1][i]};{originalValues[2][i]};{originalValues[3][i]};{originalValues[4][i]};" +
        //                    $"{resampler.ResampledTimestamps[i]}; {resampledValues[0][i]};{resampledValues[1][i]};{resampledValues[2][i]};{resampledValues[3][i]};{resampledValues[4][i]}");
        //            }
        //            else if (isOriginalMin)
        //            {
        //                sw.WriteLine(
        //                    $" \"\";\"\";\"\";\"\";\"\";\"\"; " +
        //                    $"{resampler.ResampledTimestamps[i]}; {resampledValues[0][i]};{resampledValues[1][i]};{resampledValues[2][i]};{resampledValues[3][i]};{resampledValues[4][i]}");
        //            }
        //            else
        //            {
        //                sw.WriteLine(
        //                   $"{originalTimestamps[i]}; {originalValues[0][i]};{originalValues[1][i]};{originalValues[2][i]};{originalValues[3][i]};{originalValues[4][i]};" +
        //                    $"\"\";\"\";\"\";\"\";\"\";\"\" ");

        //            }
        //        }
        //        sw.Close();

        //    }
        //    else if (selectedTransformation == "ResampleSamplesCountBased")
        //    {
        //        List<FeatureDescriptor<List<double>>> features = new List<FeatureDescriptor<List<double>>>(
        //            new FeatureDescriptor<List<double>>[]
        //            {
        //                Features.X, Features.Y, Features.Pressure, Features.Azimuth, Features.Altitude
        //            });

        //        var originalTimestamps = new List<double>(signature.GetFeature(Features.T));
        //        var originalValues = new List<double>[features.Count];
        //        for (int i = 0; i < features.Count; i++)
        //        {
        //            originalValues[i] = new List<double>(signature.GetFeature(features[i]));
        //        }

        //        var resampler = new ResampleSamplesCountBased()
        //        {
        //            InputFeatures = features,
        //            OutputFeatures = features,
        //            NumOfSamples = 500,
        //            //Interpolation = new LinearInterpolation()
        //            InterpolationType = typeof(CubicInterpolation)
        //        };
        //        resampler.Transform(signature);

        //        var resampledValues = new List<double>[features.Count];
        //        for (int i = 0; i < features.Count; i++)
        //        {
        //            resampledValues[i] = new List<double>(signature.GetFeature(features[i]));
        //        }

        //        string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
        //        StreamWriter sw = new StreamWriter(outputFileName);
        //        sw.WriteLine("OriginalT; OriginalX; OriginalY ; OriginalP ; OriginalAz; OriginalAl ;" +
        //            "ResampledT; ResampledX; ResampledY ; ResampledP ; ResampledAz; ResampledAl ");
        //        var min = originalTimestamps.Count <= resampler.ResampledTimestamps.Count ? originalTimestamps.Count : resampler.ResampledTimestamps.Count;
        //        var max = originalTimestamps.Count <= resampler.ResampledTimestamps.Count ? resampler.ResampledTimestamps.Count : originalTimestamps.Count;
        //        var isOriginalMin = originalTimestamps.Count <= resampler.ResampledTimestamps.Count ? true : false;
        //        for (int i = 0; i < max; i++)
        //        {
        //            if (i < min)
        //            {
        //                sw.WriteLine(
        //                    $"{originalTimestamps[i]}; {originalValues[0][i]};{originalValues[1][i]};{originalValues[2][i]};{originalValues[3][i]};{originalValues[4][i]};" +
        //                    $"{resampler.ResampledTimestamps[i]}; {resampledValues[0][i]};{resampledValues[1][i]};{resampledValues[2][i]};{resampledValues[3][i]};{resampledValues[4][i]}");
        //            }
        //            else if (isOriginalMin)
        //            {
        //                sw.WriteLine(
        //                    $" \"\";\"\";\"\";\"\";\"\";\"\"; " +
        //                    $"{resampler.ResampledTimestamps[i]}; {resampledValues[0][i]};{resampledValues[1][i]};{resampledValues[2][i]};{resampledValues[3][i]};{resampledValues[4][i]}");
        //            }
        //            else
        //            {
        //                sw.WriteLine(
        //                   $"{originalTimestamps[i]}; {originalValues[0][i]};{originalValues[1][i]};{originalValues[2][i]};{originalValues[3][i]};{originalValues[4][i]};" +
        //                    $"\"\";\"\";\"\";\"\";\"\";\"\" ");

        //            }
        //        }
        //        sw.Close();

        //    }
        //    else if (selectedTransformation == "FillPenUpDurations")
        //    {
        //        List<FeatureDescriptor<List<double>>> features = new List<FeatureDescriptor<List<double>>>(
        //            new FeatureDescriptor<List<double>>[]
        //            {
        //                Features.X, Features.Y, Features.Pressure, Features.Azimuth, Features.Altitude
        //            });

        //        var originalTimestamps = new List<double>(signature.GetFeature(Features.T));
        //        var originalValues = new List<double>[features.Count];
        //        for (int i = 0; i < features.Count; i++)
        //        {
        //            originalValues[i] = new List<double>(signature.GetFeature(features[i]));
        //        }

        //        var filler = new FillPenUpDurations()
        //        {
        //            InputFeatures = features,
        //            OutputFeatures = features,
        //            //Interpolation = new LinearInterpolation(),
        //            InterpolationType = typeof(CubicInterpolation)
        //        };
        //        filler.Transform(signature);

        //        var filledTimestamps = new List<double>(signature.GetFeature(filler.TimeOutputFeature));
        //        var filledValues = new List<double>[features.Count];
        //        for (int i = 0; i < features.Count; i++)
        //        {
        //            filledValues[i] = new List<double>(signature.GetFeature(features[i]));
        //        }

        //        string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
        //        StreamWriter sw = new StreamWriter(outputFileName);
        //        sw.WriteLine("OriginalT; OriginalX; OriginalY ; OriginalP ; OriginalAz; OriginalAl ;" +
        //            "FilledT; FilledX; FilledY ; FilledP ; FilledAz; FilledAl ");
        //        var originalCount = signature.GetFeature(filler.TimeInputFeature).Count;
        //        for (int i = 0; i < filledTimestamps.Count; i++)
        //        {
        //            if (i < originalCount)
        //            {
        //                sw.WriteLine(
        //                    $"{originalTimestamps[i]}; {originalValues[0][i]};{originalValues[1][i]};{originalValues[2][i]};{originalValues[3][i]};{originalValues[4][i]};" +
        //                    $"{filledTimestamps[i]}; {filledValues[0][i]};{filledValues[1][i]};{filledValues[2][i]};{filledValues[3][i]};{filledValues[4][i]}");
        //            }
        //            else
        //            {
        //                sw.WriteLine(
        //                    $"\"\";\"\";\"\";\"\";\"\";\"\"; " +
        //                    $"{filledTimestamps[i]}; {filledValues[0][i]};{filledValues[1][i]};{filledValues[2][i]};{filledValues[3][i]};{filledValues[4][i]}");
        //            }

        //        }
        //        sw.Close();
        //    }
        //    else if (selectedTransformation == "FilterPoints")
        //    {
        //        List<FeatureDescriptor<List<double>>> features = new List<FeatureDescriptor<List<double>>>(
        //            new FeatureDescriptor<List<double>>[]
        //            {
        //                Features.X, Features.Y, Features.Azimuth, Features.Altitude
        //            });

        //        var originalPressureValues = new List<double>(signature.GetFeature(Features.Pressure));
        //        var originalValues = new List<double>[features.Count];
        //        for (int i = 0; i < features.Count; i++)
        //        {
        //            originalValues[i] = new List<double>(signature.GetFeature(features[i]));
        //        }

        //        var filter = new FilterPoints()
        //        {
        //            InputFeatures = features,
        //            OutputFeatures = features,
        //            KeyFeatureInput = Features.Pressure,
        //            KeyFeatureOutput = Features.Pressure
        //        };
        //        filter.Transform(signature);

        //        var filteredPressureValues = new List<double>(signature.GetFeature(filter.KeyFeatureInput));
        //        var filteredValues = new List<double>[features.Count];
        //        for (int i = 0; i < features.Count; i++)
        //        {
        //            filteredValues[i] = new List<double>(signature.GetFeature(features[i]));
        //        }

        //        string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
        //        StreamWriter sw = new StreamWriter(outputFileName);
        //        sw.WriteLine("OriginalP; OriginalX; OriginalY ; OriginalAz; OriginalAl ;" +
        //            "FilteredP ; FilteredX; FilteredY ;  FilteredAz; FilteredAl ");
        //        var filteredCount = filteredPressureValues.Count;
        //        for (int i = 0; i < originalPressureValues.Count; i++)
        //        {
        //            if (i < filteredCount)
        //            {
        //                sw.WriteLine(
        //                    $"{originalPressureValues[i]}; {originalValues[0][i]};{originalValues[1][i]};{originalValues[2][i]};{originalValues[3][i]};" +
        //                    $"{filteredPressureValues[i]}; {filteredValues[0][i]};{filteredValues[1][i]};{filteredValues[2][i]};{filteredValues[3][i]}");
        //            }
        //            else
        //            {
        //                sw.WriteLine(
        //                    $"{originalPressureValues[i]}; {originalValues[0][i]};{originalValues[1][i]};{originalValues[2][i]};{originalValues[3][i]};" +
        //                    $"\"\";\"\";\"\";\"\";\"\";\"\" ");
        //            }

        //        }
        //        sw.Close();
        //    }
        //}

        static void JsonSerializeSignature()
        {
            Signature sig = new Signature();
            sig.ID = "Demo";
            sig.Origin = Origin.Genuine;
            sig.Signer = new Signer()
            {
                ID = "S05"
            };
            //sig.Signer.Signatures.Add(sig);
            FeatureDescriptor<int> heightDescriptor = FeatureDescriptor.Get<int>("Height");
            //var method = typeof(FeatureDescriptor).GetMethod("Get<>", BindingFlags.Public | BindingFlags.Static);
            //var genMethod = method.MakeGenericMethod(type);
            //genMethod.Invoke()


            sig.SetFeature(heightDescriptor, 4);
            var loops = new List<Loop>() { new Loop(1, 1), new Loop(3, 3) };
            System.Drawing.RectangleF bound = new System.Drawing.RectangleF(10, 10, 5, 3);
            loops[0].Bounds = bound;
            sig.SetFeature(MyFeatures.Loop, loops);

            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true);
            var signer = loader.EnumerateSigners(p => p.ID == "01").First();//Load the first signer only
            var signature = signer.Signatures[0];
            signature.Signer.Signatures = null;

            //Serialize to a string
            SerializationHelper.JsonSerializeToFile(sig, @"SignaturSerialized.txt");

            //Deserialize from a string
            Signature desirializedSig = SerializationHelper.DeserializeFromFile<Signature>(@"SignaturSerialized.txt");

            /*foreach (var descriptor in desirializedSig.GetFeatureDescriptors())
            {
                if (!descriptor.IsCollection)
                {
                    Console.WriteLine($"{descriptor.Name}: {desirializedSig[descriptor]}");
                }
                else
                {
                    Console.WriteLine($"{descriptor}:");
                    var items = (IList)desirializedSig[descriptor];
                    for (int i = 0; i < items.Count; i++)
                    {
                        Console.WriteLine($" {i}.) {items[i]}");
                    }
                }
            }*/
        }

        static void JsonSerializeOnlineVerifier()
        {
            var onlineverifier = new Verifier(new SimpleConsoleLogger())
            {
                Pipeline = new SequentialTransformPipeline
                {
                    new ParallelTransformPipeline
                    {
                        new Normalize() { Input = Features.Pressure },
                        new Map(0, 1) { Input = Features.X },
                        new Map(0, 1) { Input = Features.Y },
                        //new TimeReset(),
                    },
                    //new CentroidTranslate(),//is a sequential pipeline of other building blocks
                    new TangentExtraction(),
                    /*new AlignmentNormalization(Alignment.Origin),
                    new Paper13FeatureExtractor(),*/

                },
                Classifier = new OptimalDtwClassifier()
                {
                    Sampler = new FirstNSampler(10),
                    Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                }
            };

            string path = @"OnlineVerifier.json";
            string path3 = @"OnlineVerifier3.json";

            //File serialization example
            SerializationHelper.JsonSerializeToFile(onlineverifier, path);

            Verifier deserializedOV = SerializationHelper.DeserializeFromFile<Verifier>(path);

        }
        static void JsonSerializeOnlineVerifierBenchmark()
        {
            var benchmark = new VerifierBenchmark()
            {
                Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true),
                Verifier = new Verifier()
                {
                    Pipeline = new SequentialTransformPipeline
                    {
                        new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},

                        new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                             new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
                              new FillPenUpDurations()
                              {
                                  InputFeatures = new List<FeatureDescriptor<List<double>>>(){ Features.X, Features.Y, Features.Pressure },
                                  OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                                  InterpolationType = typeof(CubicInterpolation),
                                  TimeInputFeature =Features.T,
                                  TimeOutputFeature = Features.T
                              }
                    }
                    ,
                    Classifier = new OptimalDtwClassifier()
                    {
                        Sampler = new FirstNSampler(10),
                        Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                    }
                },
                Sampler = new FirstNSampler(10),
                Logger = new SimpleConsoleLogger(),
            };

            benchmark.ProgressChanged += ProgressPrimary;
            //benchmark.Verifier.ProgressChanged += ProgressSecondary;

            //var result = benchmark.Execute(true);

            //Console.WriteLine($"AER: {result.FinalResult.Aer}");
            SerializationHelper.JsonSerializeToFile(benchmark, @"VerifierBenchmarkSerialized.txt");
            //SerializationHelper.JsonSerializeToFile<BenchmarkResults>(result, @"BenchmarkResultSerialized.txt");
            VerifierBenchmark deserializedBM = SerializationHelper.DeserializeFromFile<VerifierBenchmark>(@"VerifierBenchmarkSerialized.txt");
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
                    Console.Write(arr[x, arr.GetLength(1) - y - 1] ? "." : " ");
                }
                Console.WriteLine();
            }
        }

        static void RealisticImageRendering2Demo()
        {
            // Load signatures from local database
            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true);

            // Load the first signer only
            var signer = loader.EnumerateSigners(p => p.ID == "02").First();
            var signature = signer.Signatures[1];


            var imageFeature = FeatureDescriptor.Get<Image<Rgba32>>("RealisticImage");

            var dpi = FeatureDescriptor.Get<int>("Dpi");

            signature.SetFeature(dpi, 300);

            RealisticImageGenerator2 generator = new RealisticImageGenerator2(50, 0.5, 96)
            {
                X = Features.X,
                Y = Features.Y,
                T = Features.T,
                Pressure = Features.Pressure,
                PenDown = Features.PenDown,
                Dpi = Features.Dpi,
                OutputImage = imageFeature
            };

            generator.Transform(signature);

            var img = signature.GetFeature(imageFeature);
            img.SaveAsBmp(new FileStream("tmp.bmp", FileMode.Create));

        }
    }
}
