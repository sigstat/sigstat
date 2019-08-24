using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using SigStat.Common.Transforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.FusionBenchmark.FusionMathHelper;
using SigStat.FusionBenchmark.OfflineFeatureExtraction;
using SigStat.FusionBenchmark.VisualHelpers;
using SigStat.FusionBenchmark.VertexTransFormations;
using SigStat.FusionBenchmark.TrajectoryRecovery;
using SigStat.FusionBenchmark.FusionFeatureExtraction;
using SigStat.FusionBenchmark.Loaders;
using SigStat.Common.Algorithms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SigStat.Common.Framework.Samplers;
using SigStat.FusionBenchmark.OfflineVerifier;

namespace SigStat.FusionBenchmark
{
    class Program
    {
        //Develop
        static void Main(string[] args)
        {
            //Alapoktol(0);
            //Alapoktol(1);
            //Alapoktol(2);
            //Alapoktol(8);

            var results = BenchmarkingWithSignerIdx(0);
            ResultOut(results);
            //AlterBenchmark();
            for (int i = 0; i < 10; i++)
                Console.ReadLine();
            Console.ReadLine();
        }

        private static void Alapoktol(int idx)
        {
            Svc2004OfflineLoader offlineLoader = new Svc2004OfflineLoader(@"Databases\\SVC(40).zip".GetPath());
            List<Signer> allsigners = offlineLoader.EnumerateSigners().ToList();

            var signers = new List<Signer>();
            signers.Add(allsigners[idx]);

            var offlinepipeline = FusionPipelines.GetAlapoktol();
            offlinepipeline.Logger = new SimpleConsoleLogger();
            foreach (var signer in signers)
            {
                Parallel.ForEach(signer.Signatures, sig => offlinepipeline.Transform(sig));
            }
        }

        private static BenchmarkResults BenchmarkingWithSignerIdx(int idx)
        {

            Svc2004OnlineLoader onlineLoader = new Svc2004OnlineLoader(@"Databases\\Online\\SVC2004\\Task2.zip".GetPath(), true);
            var onlineSigners = new List<Signer>(onlineLoader.EnumerateSigners(p => true));
            Svc2004OfflineLoader offlineLoader = new Svc2004OfflineLoader(@"Databases\\SVC(40).zip".GetPath());
            var allSigners = new List<Signer>(offlineLoader.EnumerateSigners(p => true));

            var signers = new List<Signer>();
            signers.Add(allSigners[idx]);

            //var offlinepipeline = FusionPipelines.GetOfflinepipeline();
            var offlinepipeline = FusionPipelines.GetAlapoktol();
            var fusionPipeline = FusionPipelines.GetFusionPipeline(onlineSigners, true);

            foreach (var signer in signers)
            {
                Console.WriteLine(signer.ID + " signer started at " + DateTime.Now.ToString("h:mm:ss tt"));
                Parallel.ForEach(signer.Signatures, sig =>
                {
                    Console.WriteLine("PreProcessing of " + sig.ID + " signature started at " + DateTime.Now.ToString("h:mm:ss tt"));
                    offlinepipeline.Transform(sig);
                    var onlineTransform = FusionPipelines.GetOnlinePipeline(sig);
                    var onlinePair = onlineSigners.Find(onlineSigner => onlineSigner.ID == signer.ID).Signatures.Find(s => s.ID == sig.ID);
                    var img = sig.GetFeature<Image<Rgba32>>(FusionFeatures.Image);
                    onlinePair.SetFeature<Image<Rgba32>>(FusionFeatures.Image, sig.GetFeature<Image<Rgba32>>(FusionFeatures.Image).Clone());
                    onlineTransform.Transform(onlinePair);
                }
                );
                var spSaver = FusionPipelines.GetStrokePairSaver();
                Parallel.ForEach(signer.Signatures, sig =>
                {
                    Console.WriteLine("Processing of " + sig.ID + " signature started at " + DateTime.Now.ToString("h:mm:ss tt"));
                    fusionPipeline.Transform(sig);
                    spSaver.Transform(sig);
                }
                );

                var listWithOnlySigner = new List<Signer>();
                listWithOnlySigner.Add(signer);
                var signerBenchmark = FusionPipelines.GetVerifierBenchmark(listWithOnlySigner);
                ResultOut(signerBenchmark.Execute());
                var distviewer = FusionPipelines.GetDistanceMatrixViewer(signer.Signatures);
                distviewer.Calculate();
            }
            ///////////////////////////////////////////////////

            var benchmark = FusionPipelines.GetVerifierBenchmark(signers);
            return benchmark.Execute();
        }

        private static void ResultOut(BenchmarkResults results)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("--------------------------");
            }
            foreach (var result in results.SignerResults)
            {
                Console.WriteLine(result.Signer + ": " + result.Frr.ToString() +
                                    " " + result.Far.ToString() + " " + result.Aer.ToString());
            }
            Console.WriteLine(results.FinalResult.Frr + " " + results.FinalResult.Far + " " + +results.FinalResult.Aer);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("--------------------------");
            }
        }

        private static void TransformingDemoWithIdx(int n)
        {
            Svc2004OnlineLoader onlineLoader = new Svc2004OnlineLoader(@"Databases\\Online\\SVC2004\\Task2.zip".GetPath(), true);
            var onlineSigners = new List<Signer>(onlineLoader.EnumerateSigners(p => true));
            Svc2004OfflineLoader offlineLoader = new Svc2004OfflineLoader(@"Databases\\SVC(40).zip".GetPath());
            var allSigners = new List<Signer>(offlineLoader.EnumerateSigners(p => true));

            var signers = new List<Signer>();
            signers.Add(allSigners[n]);
           

            var offlinepipeline = FusionPipelines.GetOfflinepipeline();
            offlinepipeline.Logger = new SimpleConsoleLogger();
            var fusionPipeline = FusionPipelines.GetFusionPipeline(onlineSigners, true);
            fusionPipeline.Logger = new SimpleConsoleLogger();

            var pairingSaver = new StrokePairSaver
            {
                InputBasePath = @"VisualResults",
                InputFileName = @"sp",
                InputImage = FusionFeatures.Image,
                InputTrajectory = FusionFeatures.Trajectory
            };

            var xySaver = new XYSaver
            {
                InputButton = FusionFeatures.Button,
                InputX = FusionFeatures.X,
                InputY = FusionFeatures.Y,
                InputImage = FusionFeatures.Image,
                InputBasePath = @"VisualResults",
                InputFileName = "xy"
            };
            foreach (var signer in signers)
            {
                foreach (var sig in signer.Signatures)
                {
                    offlinepipeline.Transform(sig);
                    var onlineTransform = FusionPipelines.GetOnlinePipeline(sig);
                    onlineTransform.Logger = new SimpleConsoleLogger();
                    var onlinePair = onlineSigners.Find(onlineSigner => onlineSigner.ID == signer.ID).Signatures.Find(s => s.ID == sig.ID);
                    var img = sig.GetFeature<Image<Rgba32>>(FusionFeatures.Image);
                    onlinePair.SetFeature<Image<Rgba32>>(FusionFeatures.Image, sig.GetFeature<Image<Rgba32>>(FusionFeatures.Image).Clone());
                    onlineTransform.Transform(onlinePair);
                }
                
                foreach (var sig in signer.Signatures)
                {
                    fusionPipeline.Transform(sig);
                    pairingSaver.Transform(sig);
                }
            }
        }
        /*
        private static void AlterBenchmark()
        {

            Svc2004OnlineLoader onlineLoader = new Svc2004OnlineLoader(@"Databases\\Online\\SVC2004\\Task2.zip".GetPath(), true);
            var onlineSigners = new List<Signer>(onlineLoader.EnumerateSigners(p => true));
            Svc2004OfflineLoader offlineLoader = new Svc2004OfflineLoader(@"Databases\\SVC(40).zip".GetPath());
            var allSigners = new List<Signer>(offlineLoader.EnumerateSigners(p => true));

            var signers = new List<Signer>();
            signers = allSigners;

            var offlinepipeline = FusionPipelines.GetOfflinepipeline();

            foreach (var signer in signers)
            {
                Console.WriteLine(signer.ID + " signer started at " + DateTime.Now.ToString("h:mm:ss tt"));
                Parallel.ForEach(signer.Signatures, sig =>
                {
                    offlinepipeline.Transform(sig);
                    var onlineTransform = FusionPipelines.GetOnlinePipeline(sig);
                    var onlinePair = onlineSigners.Find(onlineSigner => onlineSigner.ID == signer.ID).Signatures.Find(s => s.ID == sig.ID);
                    var img = sig.GetFeature<Image<Rgba32>>(FusionFeatures.Image);
                    onlinePair.SetFeature<Image<Rgba32>>(FusionFeatures.Image, sig.GetFeature<Image<Rgba32>>(FusionFeatures.Image).Clone());
                    onlineTransform.Transform(onlinePair);
                }
                );

                var trainer = new PairingCostCalculator
                {
                    InputSigner = signer
                };
                trainer.Calculate();

            }
            ///////////////////////////////////////////////////

        }
        */

    }
}
