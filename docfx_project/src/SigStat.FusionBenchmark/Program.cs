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
using SigStat.FusionBenchmark.ReSamplingFeatures;
using SigStat.FusionBenchmark.FusionDemos.ReSamplingBenchmarks;
using SigStat.FusionBenchmark.FusionDemos.FinalPipelines;

namespace SigStat.FusionBenchmark
{
    class Program
    {
        //Develop
        static void Main(string[] args)
        {
            try
            {
                string svcOn = "Databases/Online/SVC2004/Task2";
                string svcOff = "Databases/SVC (40)";
                string bioOn = "Databases/BiosecureID/OnlineRealTxt";
                string bioOff = "Databases/BiosecureID/OfflineSynthetic/Ienhanced";

                var svcOnLoader = FusionPipelines.GetSVCOnlineLoader(svcOn);
                var svcOffLoader = FusionPipelines.GetSVCOfflineLoader(svcOff);
                var bioOnLoader = FusionPipelines.GetBiosecureIDOnlineLoader(bioOn);
                var bioOffLoader = FusionPipelines.GetBiosecureIDOfflineLoader(bioOff);

                
                List<Tuple<DataSetLoader, DataSetLoader>> onOffLoaders = new List<Tuple<DataSetLoader, DataSetLoader>>()
                {
                    new Tuple<DataSetLoader, DataSetLoader>(bioOnLoader, bioOffLoader),
                    new Tuple<DataSetLoader, DataSetLoader>(svcOnLoader, svcOffLoader)
                };


                List<SequentialTransformPipeline> offlinePipelines1 = new List<SequentialTransformPipeline>()
                {
                    FinalFusionPipelines.GetOfflinePipelineAlap(),
                    FinalFusionPipelines.GetOfflinePipelineMerging()
                };

                List<SequentialTransformPipeline> onlinePipelines1 = new List<SequentialTransformPipeline>()
                {
                    FinalFusionPipelines.GetOnlinePipeline1(),
                    FinalFusionPipelines.GetOnlinePipeline2()
                };
                //Console.WriteLine("MarosFusion---------------------------------");
                //foreach (var loaderTuple in onOffLoaders)
                //{
                //    foreach (var onPipe in onlinePipelines1)
                //    {
                //        try
                //        {
                //            var benchmark = new MarosFinalFusionPipelines()
                //            {
                //                InputBaseSigInputCntID = 0,
                //                OfflineLoader = loaderTuple.Item2,
                //                MarosPipeline = FinalFusionPipelines.GetOfflinePipelineMaros(),
                //                OnlineLoader = loaderTuple.Item1,
                //                OnlinePipeline = onPipe,
                //                IsOptimal = true
                //            };
                //            Resultout(benchmark.Execute());
                //        }
                //        catch (Exception e)
                //        {
                //            Console.WriteLine(e.ToString());
                //        }
                //    }
                //}
                //Console.WriteLine("Maros---------------------------------");
                //foreach (var loaderTuple in onOffLoaders) {
                //    foreach (var onPipe in onlinePipelines1)
                //    {
                //        try
                //        {
                //            var benchmark = new MarosPipelineBenchmark()
                //            {
                //                InputBaseSigInputCntID = 0,
                //                OfflineLoader = loaderTuple.Item2,
                //                MarosPipeline = FinalFusionPipelines.GetOfflinePipelineMaros(),
                //                OnlineLoader = loaderTuple.Item1,
                //                OnlinePipeline = onPipe,
                //                IsOptimal = true
                //            };
                //            Resultout(benchmark.Execute());
                //        }
                //        catch (Exception e)
                //        {
                //            Console.WriteLine(e.ToString());
                //        }
                //    }
                //}
                //Console.WriteLine("Fusion---------------------------------");
                //foreach (var loaderTuple in onOffLoaders)
                //{
                //    foreach (var offPipe in offlinePipelines1)
                //    {
                //        foreach (var onPipe in onlinePipelines1)
                //        {
                //            try
                //            {
                //                var benchmark = new FusionPipelineBenchmark()
                //                {
                //                    InputBaseSigInputCntID = 0,
                //                    OfflineLoader = loaderTuple.Item2,
                //                    OfflinePipeline = offPipe,
                //                    OnlineLoader = loaderTuple.Item1,
                //                    OnlinePipeline = onPipe,
                //                    IsOptimal = true
                //                };
                //                Resultout(benchmark.Execute());
                //            }
                //            catch(Exception e)
                //            {
                //                Console.WriteLine(e.ToString());
                //            }
                //        }
                //    }
                //}
                Console.WriteLine("Online----------------------------");
                foreach (var loaderTuple in onOffLoaders)
                {
                    foreach (var onPipe in onlinePipelines1)
                    {
                        var benchmark = new FinalOnlinePipeline
                        {
                            IsOptimal = true,
                            OnlineLoader = loaderTuple.Item1,
                            OnlinePipeline = onPipe
                        };
                        Resultout(benchmark.Execute());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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
