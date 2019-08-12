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

namespace SigStat.FusionBenchmark
{
    class Program
    {
        //Develop
        static void Main(string[] args)
        {
            
            Svc2004OnlineLoader onlineLoader = new Svc2004OnlineLoader(@"Databases\\Online\\SVC2004\\Task2.zip".GetPath(), true);
            var onlineSigners = new List<Signer>(onlineLoader.EnumerateSigners(p => true));
            Svc2004OfflineLoader loader = new Svc2004OfflineLoader(@"Databases\\SVC(40).zip".GetPath());
            var signers = new List<Signer>(loader.EnumerateSigners(p => true));
            

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
                new BoundsOfflineExtract {
                    InputVertices = FusionFeatures.Vertices,
                    OutputBounds = FusionFeatures.Bounds
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
                }/*,
                new MarosAlgorithm {
                    InputVertices = FusionFeatures.Vertices,
                    InputComponents = FusionFeatures.Components,
                    OutputBaseTrajectory = FusionFeatures.BaseTrajectory
                },
                new DtwPairing {
                    InputBaseTrajectory = FusionFeatures.BaseTrajectory,
                    InputComponents = FusionFeatures.Components,
                    InputJump = 3,
                    InputWindowFrom = 50,
                    InputWindowTo = 130,
                    InputWindowJump = 5,
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
                new StrokePairSaver {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.Trajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "sp"
                },
                new StrokeSaver {
                    InputComponents = FusionFeatures.Components,
                    InputImage = FusionFeatures.Image,
                    InputBasePath = @"VisualResults",
                    InputFileName = "stroke"
                },
                new TrajectorySaver {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.Trajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "traj"
                },
                new TrajectorySaver {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.BaseTrajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "remade"
                }*/
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

            /*int idx = 0;

            var onlineSig = onlineSigners.Find(signer => signer.ID == "001").Signatures.Find(sig => sig.ID == "001");
            var offlineSig = signers[idx].Signatures[0];

            Console.WriteLine("{0} {1}", onlineSig.Signer.ID, onlineSig.ID);
            var xs = onlineSig.GetFeature<List<double>>(FusionFeatures.X);
            var ys = onlineSig.GetFeature<List<double>>(FusionFeatures.Y);
            var ts = onlineSig.GetFeature<List<double>>(FusionFeatures.T);
            var bs = onlineSig.GetFeature<List<bool>>(FusionFeatures.Button);
            
            for (int i = 0; i < xs.Count; i++)
            {
                Console.WriteLine("{0} {1} {2} {3}", ts[i], xs[i], ys[i], bs[i]);
            }
            */
            int idx = 1;
            foreach (var sig in signers[idx].Signatures)
            {
                offlinepipeline.Transform(sig);
                var onlineTransform = new SequentialTransformPipeline
                {
                    new OnlineToOfflineFeature
                    {
                        InputX = FusionFeatures.X,
                        InputY = FusionFeatures.Y,
                        InputButton = FusionFeatures.Button,
                        InputT = FusionFeatures.T,
                        InputGoalBounds = sig.GetFeature<RectangleF>(FusionFeatures.Bounds),
                        OutputVertices = FusionFeatures.Vertices,
                        OutputBaseTrajectory = FusionFeatures.BaseTrajectory
                    },
                    new CogExtraction
                    {
                        InputVertices = FusionFeatures.Vertices,
                        OutputCog = FusionFeatures.Cog,
                    },
                    new VertexTranslate
                    {
                        InputCog = FusionFeatures.Cog,
                        InputVertices = FusionFeatures.Vertices,
                        OutputVertices = FusionFeatures.Vertices,
                    },
                    new VerticesSaver
                    {
                        InputBasePath = @"Visualresults",
                        InputFileName = "online",
                        InputCog = FusionFeatures.Cog,
                        InputImage = FusionFeatures.Image,
                        InputVertices = FusionFeatures.Vertices
                    },
                    new TrajectorySaver
                    {
                        InputBasePath = @"Visualresults",
                        InputFileName = "onlinetraj",
                        InputImage = FusionFeatures.Image,
                        InputTrajectory = FusionFeatures.BaseTrajectory
                    }
                };
                onlineTransform.Logger = new SimpleConsoleLogger();
                var onlinePair = onlineSigners.Find(signer => signer.ID == sig.Signer.ID).Signatures.Find(s => s.ID == sig.ID);
                var img = sig.GetFeature<Image<Rgba32>>(FusionFeatures.Image);
                onlinePair.SetFeature<Image<Rgba32>>(FusionFeatures.Image, sig.GetFeature<Image<Rgba32>>(FusionFeatures.Image).Clone());
                onlineTransform.Transform(onlinePair);
            }
            var fusionPipeline = new SequentialTransformPipeline
            {
                new ChooseOnlineBase
                {
                    InputBaseTrajectory = FusionFeatures.BaseTrajectory,
                    OnlineSigners = onlineSigners,
                    InputID = "001",
                    OutputBaseTrajectory = FusionFeatures.BaseTrajectory
                },
                new DtwPairing {
                    InputBaseTrajectory = FusionFeatures.BaseTrajectory,
                    InputComponents = FusionFeatures.Components,
                    InputJump = 4,
                    InputWindowFrom = 50,
                    InputWindowTo = 130,
                    InputWindowJump = 15,
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
                new StrokePairSaver {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.Trajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "sp"
                },
                new StrokeSaver {
                    InputComponents = FusionFeatures.Components,
                    InputImage = FusionFeatures.Image,
                    InputBasePath = @"VisualResults",
                    InputFileName = "stroke"
                },
                new TrajectorySaver {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.Trajectory,
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
            fusionPipeline.Logger = new SimpleConsoleLogger();
            for (int i = 0; i < 10; i++)
                Console.WriteLine("********************************");
            foreach (var sig in signers[idx].Signatures)
            {
                fusionPipeline.Transform(sig);
            }

            Console.ReadLine();

        }

        private static BenchmarkResults BenchmarkingWithPipeline(SequentialTransformPipeline pipeline)
        {
            Svc2004OfflineLoader loader = new Svc2004OfflineLoader(@"Databases/SVC(40).zip".GetPath());
            //Svc2004OnlineLoader onlineLoader = new Svc2004OnlineLoader(@"Databases/SVC(40).zip".GetPath());
            //Előfeldolgozásban mindent kézzel összeraknék



            var benchmark = new VerifierBenchmark
            {
                Loader = loader,
                Logger = new SimpleConsoleLogger(),
                Verifier = new Verifier
                {
                    Pipeline = new SequentialTransformPipeline(),
                    Classifier = new DtwClassifier(DtwPy.EuclideanDistance)
                    {
                        Features = new List<FeatureDescriptor> { Features.X, Features.Y }
                    }
                },
                //Sampler = new SVC2004Sampler1(),
            };
            //Svc2004OfflineLoader loader = new Svc2004OfflineLoader(@"Databases/SVC(40).zip".GetPath());
            //var benchmark = new VerifierBenchmark
            //{
            //    Loader = loader,
            //    Logger = new SimpleConsoleLogger(),
            //    Verifier = new Verifier
            //    {
            //        Pipeline = pipeline,
            //        Classifier = new DtwClassifier(DtwPy.EuclideanDistance)
            //        {
            //            Features = new List<FeatureDescriptor> { FusionFeatures.X, FusionFeatures.Y }
            //        }
            //    },
            //    //Sampler = new SVC2004Sampler1(),
            //};
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
