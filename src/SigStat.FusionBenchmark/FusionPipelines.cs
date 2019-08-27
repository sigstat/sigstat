using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Transforms;
using SigStat.FusionBenchmark.FusionFeatureExtraction;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.FusionBenchmark.Loaders;
using SigStat.FusionBenchmark.OfflineFeatureExtraction;
using SigStat.FusionBenchmark.TrajectoryRecovery;
using SigStat.FusionBenchmark.VertexTransFormations;
using SigStat.FusionBenchmark.VisualHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark
{
    public class FusionPipelines
    {

        public static Tuple<double, double> MyPairingRange = new Tuple<double, double>(0.0, 2.0);

        public static Tuple<double, double> MyClassifierRange = new Tuple<double, double>(0.0, 1.0);


        public static int DtwpairingJump = 4;

        public static int DtwPairingScaling = 5;

        public static int OffToOnScaling = 5;

        public static int NumOfRef = 10;

        public static List<FeatureDescriptor> MyFeatures = new List<FeatureDescriptor>()
                {
                    FusionFeatures.X,
                    FusionFeatures.Y
                };

        public static Func<double[], double[], double> MyFunc = DtwPy.EuclideanDistance;

        public static SequentialTransformPipeline GetOfflinePipeline()
        {
            return new SequentialTransformPipeline
            {
                new Binarization
                {
                    InputImage = FusionFeatures.Image,
                    OutputMask = FusionFeatures.Skeleton
                },
                new PreVertexExtract
                {
                    InputSkeleton = FusionFeatures.Skeleton,
                    OutputContour = FusionFeatures.Contour,
                    OutputWidthOfPen = FusionFeatures.WidthOfPen,
                },
                new HSCPThinning
                {
                    Input = FusionFeatures.Skeleton,
                    Output = FusionFeatures.Skeleton
                },
                new OnePixelThinning
                {
                    Input = FusionFeatures.Skeleton,
                    Output = FusionFeatures.Skeleton
                },
                new VertexExtract
                {
                    InputSkeleton = FusionFeatures.Skeleton,
                    OutputVertices = FusionFeatures.Vertices
                },
                new BoundsOfflineExtract
                {
                    InputVertices = FusionFeatures.Vertices,
                    OutputBounds = FusionFeatures.Bounds
                },
                new StrokeExtract
                {
                    InputVertices = FusionFeatures.Vertices,
                    OutputComponents = FusionFeatures.Components
                },
                new StrokeEliminating
                {
                    InputComponent = FusionFeatures.Components,
                    InputContour = FusionFeatures.Contour,
                    InputWidthOfPen = FusionFeatures.WidthOfPen,
                    OutputComponent = FusionFeatures.Components,
                    OutputSpuriousComps = FusionFeatures.SpuriousComps
                },
                new VertexNormalization
                {
                    InputRange = MyPairingRange,
                    InputVertices = FusionFeatures.Vertices,
                    OutputVertices = FusionFeatures.Vertices
                }
            };
        }

        public static SequentialTransformPipeline GetOnlineToOfflinePipeline(RectangleF goalBounds)
        {
            return new SequentialTransformPipeline
            {
                new OnlineToOfflineFeature
                {
                    InputButton = FusionFeatures.Button,
                    InputX = FusionFeatures.X,
                    InputY = FusionFeatures.Y,
                    InputT = FusionFeatures.T,
                    InputGoalBounds = goalBounds,
                    OutputBaseTrajectory = FusionFeatures.Trajectory,
                    OutputVertices = FusionFeatures.Vertices
                },
                new VertexNormalization
                {
                    InputRange = MyPairingRange,
                    InputVertices = FusionFeatures.Vertices,
                    OutputVertices = FusionFeatures.Vertices
                }
            };
        }

        public static SequentialTransformPipeline GetFusionPipeline(List<Signer> onlineSigners, bool isParallel, string baseSigID = "001")
        {
            return new SequentialTransformPipeline
            {
                new ChooseOnlineBase
                {
                    OnlineSigners = onlineSigners,
                    InputBaseTrajectory = FusionFeatures.Trajectory,
                    InputID = baseSigID,
                    OutputBaseTrajectory = FusionFeatures.BaseTrajectory
                },
                new DtwPairing
                {
                    InputBaseTrajectory = FusionFeatures.BaseTrajectory,
                    InputComponents = FusionFeatures.Components,
                    InputIsParallel = isParallel,
                    InputJump = DtwpairingJump,
                    InputScale = DtwPairingScaling,
                    InputWindowFrom = 50,
                    InputWindowJump = 10,
                    InputWindowTo = 130,
                    OutputStrokeMatches = FusionFeatures.StrokeMatches,
                    OutputTrajectory = FusionFeatures.Trajectory
                },
                new OfflineToOnlineFeature
                {
                    InputTrajectory = FusionFeatures.Trajectory,
                    InputScale = OffToOnScaling,
                    OutputButton = FusionFeatures.Button,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y
                }
            };
        }

        public static SequentialTransformPipeline GetOnlinePipeline()
        {
            return new SequentialTransformPipeline
            {
                new NormalizeToRange
                {
                    Input = FusionFeatures.X,
                    Output = FusionFeatures.X,
                    InputRange = MyClassifierRange
                },
                new NormalizeToRange
                {
                    Input = FusionFeatures.Y,
                    Output = FusionFeatures.Y,
                    InputRange = MyClassifierRange
                }
            };
        }

        public static SequentialTransformPipeline GetHackedOfflinePipeline()
        {
            return new SequentialTransformPipeline
            {
                new Binarization
                {
                    InputImage = FusionFeatures.Image,
                    OutputMask = FusionFeatures.Skeleton
                },
                new HSCPThinning
                {
                    Input = FusionFeatures.Skeleton,
                    Output = FusionFeatures.Skeleton
                },
                new OnePixelThinning
                {
                    Input = FusionFeatures.Skeleton,
                    Output = FusionFeatures.Skeleton
                },
                new VertexExtract
                {
                    InputSkeleton = FusionFeatures.Skeleton,
                    OutputVertices = FusionFeatures.Vertices
                },
                new BoundsOfflineExtract
                {
                    InputVertices = FusionFeatures.Vertices,
                    OutputBounds = FusionFeatures.Bounds
                }
            };
        }

        public static SequentialTransformPipeline GetHackedOnToOnPipeline(RectangleF goalBounds)
        {
            return new SequentialTransformPipeline
            {
                new OnlineToOfflineFeature
                {
                    InputButton = FusionFeatures.Button,
                    InputX = FusionFeatures.X,
                    InputY = FusionFeatures.Y,
                    InputT = FusionFeatures.T,
                    InputGoalBounds = goalBounds,
                    OutputBaseTrajectory = FusionFeatures.Trajectory,
                    OutputVertices = FusionFeatures.Vertices
                },
                new VertexNormalization
                {
                    InputRange = MyPairingRange,
                    InputVertices = FusionFeatures.Vertices,
                    OutputVertices = FusionFeatures.Vertices
                },
                new OfflineToOnlineFeature
                {
                    InputScale = FusionPipelines.OffToOnScaling,
                    InputTrajectory = FusionFeatures.Trajectory,
                    OutputButton = FusionFeatures.Button,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y
                },
            };
        }

        public static SequentialTransformPipeline GetOffToOnTransform()
        {
            return new SequentialTransformPipeline {
                new OfflineToOnlineFeature
                {
                    InputScale = FusionPipelines.OffToOnScaling,
                    InputTrajectory = FusionFeatures.Trajectory,
                    OutputButton = FusionFeatures.Button,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y
                }
            };
        }

        public static Svc2004OfflineLoader GetOfflineLoader(string path = @"Databases\\SVC(40).zip")
        {
            return new Svc2004OfflineLoader(path.GetPath());
        }

        public static Svc2004OnlineLoader GetOnlineLoader(string path = @"Databases\\Online\\SVC2004\\Task2.zip")
        {
            return new Svc2004OnlineLoader(path.GetPath(), true);
        }

        public static VerifierBenchmark GetBenchmark(List<Signer> signers, bool isOptimal)
        {
            if (isOptimal)
            {
                return GetOptimalBenchmark(signers);
            }
            return GetPureBenchmark(signers);
        }

        public static VerifierBenchmark GetPureBenchmark(List<Signer> signers)
        {
            var loader = new MemoryLoader() { Signers = signers };
            return new VerifierBenchmark
            {
                Loader = loader,
                Sampler = new FirstNSampler(NumOfRef),
                Verifier = new Verifier
                {
                    Pipeline = new SequentialTransformPipeline(),
                    Classifier = new DtwClassifier(MyFunc)
                    {
                        Features = MyFeatures
                    }
                }
            };
        }

        public static VerifierBenchmark GetOptimalBenchmark(List<Signer> signers)
        {
            var loader = new MemoryLoader() { Signers = signers };
            return new VerifierBenchmark
            {
                Loader = loader,
                Sampler = new FirstNSampler(NumOfRef),
                Verifier = new Verifier
                {
                    Pipeline = new SequentialTransformPipeline(),
                    Classifier = new OptimalDtwClassifier(MyFunc)
                    {
                        Sampler = new FirstNSampler(NumOfRef),
                        Features = MyFeatures
                    }
                }
            };
        }

        public static DistanceMatrixViewer GetDistanceMatrixViewer(List<Signature> onlineSignatures, List<Signature> offlineSignatures)
        {
            var signatures = new List<Signature>();
            signatures.AddRange(onlineSignatures);
            signatures.AddRange(offlineSignatures);
            return new DistanceMatrixViewer
            {
                InputFeatures = MyFeatures,
                InputFunc = MyFunc,
                InputSignatures = signatures
            };
        }

    }
}
