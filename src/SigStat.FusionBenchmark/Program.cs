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
using SigStat.Common.Algorithms;

namespace SigStat.FusionBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Svc2004OfflineLoader loader = new Svc2004OfflineLoader(@"Databases/SVC(40).zip".GetPath());
            var offlinepipeline = new SequentialTransformPipeline
            {
                new Binarization {
                    InputImage = FusionFeatures.Image,
                    OutputMask = FusionFeatures.Skeleton
                },
                new HSCPThinning {
                    Input = FusionFeatures.Skeleton,
                    Output = FusionFeatures.Skeleton
                },
                new OnePixelThinning {
                    Input = FusionFeatures.Skeleton,
                    Output = FusionFeatures.Skeleton
                },
                new VertexExtract {
                    InputSkeleton = FusionFeatures.Skeleton,
                    OutputVertices = FusionFeatures.Vertices
                },
                new CogExtraction {
                    InputVertices = FusionFeatures.Vertices,
                    OutputCog = FusionFeatures.Cog
                },
                new VertexTranslate {
                    InputVertices = FusionFeatures.Vertices,
                    InputCog = FusionFeatures.Cog
                },
                new StrokeExtract {
                    InputVertices = FusionFeatures.Vertices,
                    OutputComponents = FusionFeatures.Components
                },
                new MarosAlgorithm {
                    InputVertices = FusionFeatures.Vertices,
                    InputComponents = FusionFeatures.Components,
                    OutputBaseTrajectory = FusionFeatures.BaseTrajectory
                },
                new DtwPairing {
                    InputBaseTrajectory = FusionFeatures.BaseTrajectory,
                    InputComponents = FusionFeatures.Components,
                    InputJump = 10,
                    InputBaseSigIdx = 0,
                    OutputTrajectory = FusionFeatures.Trajectory
                },
                new FusionFeatureTransform {
                    InputTrajectory = FusionFeatures.Trajectory,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y,
                    OutputButton = FusionFeatures.Button
                },
                new VerticesSaver {
                    InputBasePath = @"VisualResults",
                    InputFileName = "vertices",
                    InputImage = FusionFeatures.Image,
                    InputCog = FusionFeatures.Cog,
                    InputVertices = FusionFeatures.Vertices
                },
                new StrokeSaver {
                    InputComponents = FusionFeatures.Components,
                    InputImage = FusionFeatures.Image,
                    InputBasePath = @"VisualResults",
                    InputFileName = "stroke"
                },
                new TrajectorySaver {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.BaseTrajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "traj"
                },
                new TrajectorySaver {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.BaseTrajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "remade"
                }
            };
            offlinepipeline.Logger = new SimpleConsoleLogger();
            
            var marospipeline = new SequentialTransformPipeline
            {
                new Binarization {
                    InputImage = FusionFeatures.Image,
                    OutputMask = FusionFeatures.Skeleton
                },
                new HSCPThinning {
                    Input = FusionFeatures.Skeleton,
                    Output = FusionFeatures.Skeleton
                },
                new OnePixelThinning {
                    Input = FusionFeatures.Skeleton,
                    Output = FusionFeatures.Skeleton
                },
                new VertexExtract {
                    InputSkeleton = FusionFeatures.Skeleton,
                    OutputVertices = FusionFeatures.Vertices
                },
                new CogExtraction {
                    InputVertices = FusionFeatures.Vertices,
                    OutputCog = FusionFeatures.Cog
                },
                new VertexTranslate {
                    InputVertices = FusionFeatures.Vertices,
                    InputCog = FusionFeatures.Cog
                },
                new StrokeExtract {
                    InputVertices = FusionFeatures.Vertices,
                    OutputComponents = FusionFeatures.Components
                },
                new MarosAlgorithm {
                    InputVertices = FusionFeatures.Vertices,
                    InputComponents = FusionFeatures.Components,
                    OutputBaseTrajectory = FusionFeatures.BaseTrajectory
                },
                new FusionFeatureTransform {
                    InputTrajectory = FusionFeatures.BaseTrajectory,
                    OutputButton = FusionFeatures.Button,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y
                }
            };
            marospipeline.Logger = new SimpleConsoleLogger();

            //var offline = BenchmarkingWithPipeline(offlinepipeline);
            //var maros = BenchmarkingWithPipeline(marospipeline);

            //Console.WriteLine("Offline");
            //ResultOut(offline);
            //Console.WriteLine("Maros");
            //ResultOut(maros);

            int idx = 17;
            var signers = new List<Signer>(loader.EnumerateSigners(p => true));
            foreach (var sig in signers[idx].Signatures)
            {
                offlinepipeline.Transform(sig);
            }
            
            Console.ReadLine();
            
        }

        private static BenchmarkResults BenchmarkingWithPipeline(SequentialTransformPipeline pipeline)
        {
            Svc2004OfflineLoader loader = new Svc2004OfflineLoader(@"Databases/SVC(40).zip".GetPath());
            var benchmark = new VerifierBenchmark
            {
                Loader = loader,
                Logger = new SimpleConsoleLogger(),
                Verifier = new Verifier
                {
                    Pipeline = pipeline,
                    Classifier = new DtwClassifier(DtwPy.EuclideanDistance)
                    {
                        Features = new List<FeatureDescriptor> { FusionFeatures.X, FusionFeatures.Y }
                    }
                },
                Sampler = new SVC2004Sampler1(),
            };
            return benchmark.Execute();
        }

        private static void ResultOut(BenchmarkResults results)
        {
            foreach (var result in results.SignerResults)
            {
                Console.WriteLine(result.Signer + ": " + result.Frr.ToString() +
                                    " " + result.Far.ToString() + " " + result.Aer.ToString());
            }
            Console.WriteLine(results.FinalResult.Aer + " " + results.FinalResult.Far + " " + results.FinalResult.Frr);
        }

        
    }
}
