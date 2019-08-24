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
        public static Tuple<double, double> MyRange = new Tuple<double, double>(0.0, 2.0);

        public static readonly int strokeConnectMaxLength = 10;

        public static readonly int scalingConst = 7;

        public static List<FeatureDescriptor> MyFeatures = new List<FeatureDescriptor>
        {
            FusionFeatures.X,
            FusionFeatures.Y,
            FusionFeatures.Directions
        };

        public static Func<double[], double[], double> MyDistFunc = DtwPairing.DtwPairingDistance;

        public static SequentialTransformPipeline GetOfflinepipeline()
        {
            return new SequentialTransformPipeline
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
                new VertexNormalization
                {
                    InputVertices = FusionFeatures.Vertices,
                    InputRange = MyRange,
                    OutputVertices = FusionFeatures.Vertices
                },
                new CogExtraction {
                    InputVertices = FusionFeatures.Vertices,
                    OutputCog = FusionFeatures.Cog
                },
                new StrokeExtract {
                    InputVertices = FusionFeatures.Vertices,
                    OutputComponents = FusionFeatures.Components
                }
            };
        }


        public static SequentialTransformPipeline GetFusionPipeline(List<Signer> onlineSigners, bool pairingIsParallel)
        {
            return new SequentialTransformPipeline
            {
                new ChooseOnlineBase
                {
                    InputBaseTrajectory = FusionFeatures.BaseTrajectory,
                    OnlineSigners = onlineSigners,
                    InputID = "001",
                    OutputBaseTrajectory = FusionFeatures.BaseTrajectory
                },
                new DtwPairing
                {
                    InputBaseTrajectory = FusionFeatures.BaseTrajectory,
                    InputComponents = FusionFeatures.Components,
                    InputJump = 4,
                    InputScale = 7 ,
                    InputWindowFrom = 50,
                    InputWindowTo = 130,
                    InputWindowJump = 15,
                    InputIsParallel = pairingIsParallel,
                    OutputTrajectory = FusionFeatures.Trajectory,
                    OutputStrokeMatches = FusionFeatures.StrokeMatches
                },
                new OfflineToOnlineFeature
                {
                    InputTrajectory = FusionFeatures.Trajectory,
                    InputScale = 7,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y,
                    OutputButton = FusionFeatures.Button
                },
                new TangentExtraction
                {
                    X = FusionFeatures.X,
                    Y = FusionFeatures.Y,
                    OutputTangent = FusionFeatures.Tangent
                },
                new DirectionExtraction
                {
                    InputX = FusionFeatures.X,
                    InputY = FusionFeatures.Y,
                    OutputDirections = FusionFeatures.Directions
                },
                new DerivatorExtract
                {
                    InputSequence = FusionFeatures.Directions,
                    OutputDiffSequence = FusionFeatures.Curvature
                },/*
                new CogShiftTransform
                {
                    InputX = FusionFeatures.X,
                    InputY = FusionFeatures.Y,
                    InputCog = FusionFeatures.Cog,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y
                },*/
                new NormalizeToRange
                {
                    Input = FusionFeatures.X,
                    InputRange = MyRange,
                    Output = FusionFeatures.X
                },
                new NormalizeToRange
                {
                    Input = FusionFeatures.Y,
                    InputRange = MyRange,
                    Output = FusionFeatures.Y
                }
            };
        }

        public static SequentialTransformPipeline GetOnlinePipeline(Signature offlineSignaturePair)
        {
            return new SequentialTransformPipeline
            {
                new OnlineToOfflineFeature
                {
                    InputX = FusionFeatures.X,
                    InputY = FusionFeatures.Y,
                    InputButton = FusionFeatures.Button,
                    InputT = FusionFeatures.T,
                    InputGoalBounds = offlineSignaturePair.GetFeature<RectangleF>(FusionFeatures.Bounds),
                    OutputVertices = FusionFeatures.Vertices,
                    OutputBaseTrajectory = FusionFeatures.BaseTrajectory
                },
                new VertexNormalization
                {
                    InputVertices = FusionFeatures.Vertices,
                    InputRange = MyRange,
                    OutputVertices = FusionFeatures.Vertices
                },
                new CogExtraction
                {
                    InputVertices = FusionFeatures.Vertices,
                    OutputCog = FusionFeatures.Cog,
                }
            };
        }

        public static SequentialTransformPipeline GetOfflineSavers()
        {
            return new SequentialTransformPipeline {
                new VerticesSaver
                {
                InputBasePath = @"VisualResults",
                InputFileName = "vertices",
                InputImage = FusionFeatures.Image,
                InputCog = FusionFeatures.Cog,
                InputVertices = FusionFeatures.Vertices
                },
                new StrokePairSaver
                {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.Trajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "sp"
                },
                new StrokeSaver
                {
                    InputComponents = FusionFeatures.Components,
                    InputImage = FusionFeatures.Image,
                    InputBasePath = @"VisualResults",
                    InputFileName = "stroke"
                },
                new TrajectorySaver
                {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.Trajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "traj"
                },
                new TrajectorySaver
                {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.BaseTrajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "remade"
                }
            };
        }

        public static SequentialTransformPipeline GetOnlineSavers()
        {
            return new SequentialTransformPipeline {
                new VerticesSaver
                {
                InputBasePath = @"VisualResults",
                InputFileName = "vertices",
                InputImage = FusionFeatures.Image,
                InputCog = FusionFeatures.Cog,
                InputVertices = FusionFeatures.Vertices
                },
                new TrajectorySaver
                {
                    InputImage = FusionFeatures.Image,
                    InputTrajectory = FusionFeatures.BaseTrajectory,
                    InputBasePath = @"VisualResults",
                    InputFileName = "remade"
                }
            };
        }

        public static VerifierBenchmark GetVerifierBenchmark(List<Signer> signers)
        {
            var loader = new MemoryLoader { Signers = signers };
            return new VerifierBenchmark
            {
                Loader = loader,
                Logger = new SimpleConsoleLogger(),
                Verifier = new Verifier
                {
                    Pipeline = new SequentialTransformPipeline(),
                    Classifier = new OptimalDtwClassifier(MyDistFunc)
                    {
                        Features = MyFeatures,
                        Sampler = new FirstNSampler(10)
                    }
                },
                Sampler = new FirstNSampler(10)
            };
        }

        public static DistanceMatrixViewer GetDistanceMatrixViewer(List<Signature> signatures)
        {
            return new DistanceMatrixViewer 
            {
                InputFeatures = MyFeatures,
                InputFunc = MyDistFunc,
                InputSignatures = signatures
            };
        }

        public static StrokePairSaver GetStrokePairSaver()
        {
            return new StrokePairSaver
            {
                InputBasePath = @"VisualResults",
                InputFileName = "sp",
                InputStrokeMatches = FusionFeatures.StrokeMatches,
                InputImage = FusionFeatures.Image,
                InputTrajectory = FusionFeatures.Trajectory
            };
        }

        public static SequentialTransformPipeline GetAlapoktol()
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
                    OutputAreaOfStrokes = FusionFeatures.AreaOfStrokes,
                    OutputContour = FusionFeatures.Contour,
                    OutputWidthOfPen = FusionFeatures.WidthOfPen
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
                new VertexNormalization
                {
                    InputVertices = FusionFeatures.Vertices,
                    InputRange = MyRange,
                    OutputVertices = FusionFeatures.Vertices
                },
                new CogExtraction
                {
                    InputVertices = FusionFeatures.Vertices,
                    OutputCog = FusionFeatures.Cog
                },/*
                new AllVerticesSaver
                {
                    InputFileName = "all",
                    InputBasePath = @"VisualResults",
                    InputAreaOfStrokes = FusionFeatures.AreaOfStrokes,
                    InputContour = FusionFeatures.Contour,
                    InputImage = FusionFeatures.Image
                },*/
                new StrokeExtract
                {
                    InputVertices = FusionFeatures.Vertices,
                    OutputComponents = FusionFeatures.Components
                },/*
                new StrokeSaver
                {
                    InputFileName = "stroke",
                    InputBasePath = @"VisualResults",
                    InputImage = FusionFeatures.Image,
                    InputComponents = FusionFeatures.Components
                },*/
                new StrokeEliminating
                {
                    InputComponent = FusionFeatures.Components,
                    InputContour = FusionFeatures.Contour,
                    InputWidthOfPen = FusionFeatures.WidthOfPen,
                    OutputComponent = FusionFeatures.Components,
                    OutputSpuriousComps = FusionFeatures.SpuriousComps
                },
                new RealSpuriousSaver
                {
                    InputFileName = "rs",
                    InputBasePath = @"VisualResults",
                    InputImage = FusionFeatures.Image,
                    InputSpuriousComps = FusionFeatures.SpuriousComps,
                    InputComponents = FusionFeatures.Components
                },
                new StrokeMerging
                {
                    InputWidthOfPen = FusionFeatures.WidthOfPen,
                    InputComponent = FusionFeatures.Components,
                    InputConnectionNodes = FusionFeatures.SpuriousComps,
                    OutputComponents = FusionFeatures.Components
                },
                new StrokeSaver
                {
                    InputFileName = "merged",
                    InputBasePath = @"VisualResults",
                    InputComponents = FusionFeatures.Components,
                    InputImage = FusionFeatures.Image
                }
            };
        }

    }
}
