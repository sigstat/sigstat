using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using SigStat.FusionBenchmark.FusionFeatureExtraction;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.FusionBenchmark.LineTransforms;
using SigStat.FusionBenchmark.TrajectoryRecovery;
using SigStat.FusionBenchmark.VertexTransFormations;
using SigStat.FusionBenchmark.VisualHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.FusionDemos.FinalPipelines
{
    public static class FinalFusionPipelines
    {
        public static Tuple<double, double> MyPairingRange = new Tuple<double, double>(0.0, 2.0);

        public static Tuple<double, double> MyClassifierRange = new Tuple<double, double>(0.0, 1.0);

        public static double DtwPairingScaling = 0.004166;

        public static double OffToOnScaling = 0.004166;

        public static int NumOfRef = 10;

        public static List<FeatureDescriptor> MyFeatures = new List<FeatureDescriptor>()
                {
                    FusionFeatures.X,
                    FusionFeatures.Y
                };

        public static Func<double[], double[], double> MyFunc = DtwPy.EuclideanDistance;

        public static Sampler MySampler = new FirstNSampler(10);

        public static SequentialTransformPipeline GetOfflineSavers()
        {
            return new SequentialTransformPipeline {
                new StrokeSaver
                {
                    InputBasePath = @"VisualResults",
                    InputComponents = FusionFeatures.Components,
                    InputFileName = "st",
                    InputImage = FusionFeatures.Image
                }, 
                new StrokePairSaver
                {
                    InputBasePath = @"VisualResults",
                    InputImage = FusionFeatures.Image,
                    InputFileName = "sp",
                    InputStrokeMatches = FusionFeatures.StrokeMatches,
                    InputTrajectory = FusionFeatures.Trajectory
                },
                new RealSpuriousSaver {
                    InputBasePath = @"VisualResults",
                    InputFileName = "rs",
                    InputComponents = FusionFeatures.Components,
                    InputImage = FusionFeatures.Image,
                    InputSpuriousComps = FusionFeatures.SpuriousComps
                }
            };
        }

        public static SequentialTransformPipeline GetOfflinePipelineAlap()
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

        public static SequentialTransformPipeline GetOfflinePipelineMerging()
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
                new StrokeMerging
                {
                    InputComponent = FusionFeatures.Components,
                    InputConnectionNodes = FusionFeatures.SpuriousComps,
                    InputWidthOfPen = FusionFeatures.WidthOfPen,
                    OutputComponents = FusionFeatures.Components
                },
                new VertexNormalization
                {
                    InputRange = MyPairingRange,
                    InputVertices = FusionFeatures.Vertices,
                    OutputVertices = FusionFeatures.Vertices
                }
            };
        }

        public static SequentialTransformPipeline GetOfflinePipelineMaros()
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
                new StrokeMerging
                {
                    InputComponent = FusionFeatures.Components,
                    InputConnectionNodes = FusionFeatures.SpuriousComps,
                    InputWidthOfPen = FusionFeatures.WidthOfPen,
                    OutputComponents = FusionFeatures.Components
                },
                new MarosAlgorithm
                {
                    InputComponents = FusionFeatures.Components,
                    OutputTrajectory = FusionFeatures.Trajectory
                },
                new OfflineToOnlineFeature
                {
                    InputTrajectory = FusionFeatures.Trajectory,
                    InputScaleRate = OffToOnScaling,
                    OutputButton = FusionFeatures.Button,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y
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
                    InputScaleRate = OffToOnScaling,
                    InputTrajectory = FusionFeatures.Trajectory,
                    OutputButton = FusionFeatures.Button,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y
                }
            };
        }

        public static SequentialTransformPipeline GetOnlinePipeline1()
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

        public static SequentialTransformPipeline GetOnlinePipeline2()
        {
            return new SequentialTransformPipeline
            {
                new CogExtract
                {
                    InputX = FusionFeatures.X,
                    InputY = FusionFeatures.Y,
                    OutputCog = FusionFeatures.Cog
                },
                new CogShiftTransform
                {
                    InputX = FusionFeatures.X,
                    InputY = FusionFeatures.Y,
                    InputCog = FusionFeatures.Cog,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y,
                }
            };
        }

        public static SequentialTransformPipeline GetFusionPipeline(List<Signer> onlineSigners, bool isParallel, int baseSigInputCntID)
        {
            return new SequentialTransformPipeline
            {
                new ChooseOnlineBase
                {
                    OnlineSigners = onlineSigners,
                    InputBaseTrajectory = FusionFeatures.Trajectory,
                    InputCntID = baseSigInputCntID,
                    OutputBaseTrajectory = FusionFeatures.BaseTrajectory
                },
                new DtwPairing
                {
                    InputBaseTrajectory = FusionFeatures.BaseTrajectory,
                    InputComponents = FusionFeatures.Components,
                    InputIsParallel = isParallel,
                    InputScaleRate = DtwPairingScaling,
                    InputWindowFrom = 50,
                    InputWindowJump = 10,
                    InputWindowTo = 130,
                    OutputStrokeMatches = FusionFeatures.StrokeMatches,
                    OutputTrajectory = FusionFeatures.Trajectory
                },
                new OfflineToOnlineFeature
                {
                    InputTrajectory = FusionFeatures.Trajectory,
                    OutputButton = FusionFeatures.Button,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y,
                    InputScaleRate = OffToOnScaling 
                }
            };
        }

        public static SequentialTransformPipeline EmptyPipeline()
        {
            return new SequentialTransformPipeline();
        }

    }
}
