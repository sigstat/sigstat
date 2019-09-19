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
using SigStat.FusionBenchmark.FusionDemos;
using SigStat.FusionBenchmark.FusionDemos.PipelineBenchmarks;

namespace SigStat.FusionBenchmark
{
    class Program
    {
        //Develop
        static void Main(string[] args)
        {
            //ResultsToTxt(HackedOnlineOnlineBenchmark.BenchmarkWithAllSigners(true,
            //                                    FusionPipelines.GetOfflineLoader(),
            //                                    FusionPipelines.GetOnlineLoader()),
            //            "hackedonon_svc_09_18");
            //ResultsToTxt(MarosBenchmark.BenchmarkingWithAllSigners(true,
            //                            new BiosecureIDOfflineLoader(@"Databases/BiosecureID/OfflineSynthetic/Ienhanced")),
            //                            //FusionPipelines.GetOfflineLoader()),
            //            "maros_biosecure_09_18");
            ResultsToTxt(FusionVerifierBenchmark.BenchMarkingWithAllSigners(true,
                                                                    FusionPipelines.GetOfflineLoader(),
                                                                    FusionPipelines.GetOnlineLoader()),
                         "fusion_svc_09_18");
            //FusionVerifierBenchmark.BenchMarkingWithAllSigners(true, 
            //    new BiosecureIDOfflineLoader(@"Databases/BiosecureID/OfflineSynthetic/Ienhanced"),
            //    new BiosecureIDOnlineLoader(@"Databases/BiosecureID/OnlineRealTxt"));
            //var results = OnlineOnlineBenchmark.BenchMarkWithAllSigners(true, new BiosecureIDOnlineLoader(@"Databases/BiosecureID/OnlineRealTxt"));
            
            for (int i = 0; i < 10; i++)
                Console.ReadLine();
            Console.ReadLine();
        }

        public static void Resultout(BenchmarkResults results, string resultInfo = "")
        {
            Console.WriteLine(resultInfo);
            foreach (var result in results.SignerResults)
            {
                Console.WriteLine(result.Signer + " {0} {1} {2}", result.Frr, result.Far, result.Aer);
            }
            Console.WriteLine("Avg {0} {1} {2}", results.FinalResult.Frr, results.FinalResult.Far, results.FinalResult.Aer);
        }

        private static void Resultout(FusionBenchmarkResults fusionResults, string resultInfo = "")
        {
            Console.WriteLine(resultInfo);
            Resultout(fusionResults.OffOffResults, "offoff");
            Resultout(fusionResults.OffOnResults, "offon");
            Resultout(fusionResults.OnOffResults, "onoff");
            Resultout(fusionResults.OnOnResults, "onon");
        }

        public static void ResultsToTxt(BenchmarkResults results, string fileName)
        {
            TxtHelper.Save(TxtHelper.BenchmarkResToLines(results), fileName);
        }

        private static void ResultsToTxt(FusionBenchmarkResults fusionResults, string fileName)
        {
            TxtHelper.Save(TxtHelper.BenchmarkResToLines(fusionResults.OffOffResults), fileName + "offoff");
            TxtHelper.Save(TxtHelper.BenchmarkResToLines(fusionResults.OffOnResults), fileName + "offon");
            TxtHelper.Save(TxtHelper.BenchmarkResToLines(fusionResults.OnOffResults), fileName + "onoff");
            TxtHelper.Save(TxtHelper.BenchmarkResToLines(fusionResults.OnOnResults), fileName + "onon");
        }

    }
}
