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
using SigStat.FusionBenchmark.VertexTransFormations;
using SigStat.FusionBenchmark.VisualHelpers;
using SigStat.FusionBenchmark.TrajectoryReconsturction;
using SigStat.FusionBenchmark.FusionFeatureExtraction;
using SigStat.FusionBenchmark.FusionMathHelper;

namespace SigStat.FusionBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Svc2004OfflineLoader loader = new Svc2004OfflineLoader(@"Databases/SVC(40).zip".GetPath());
            loader.Logger = new SimpleConsoleLogger();
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
                new PointClassification
                {
                    InputVertices = FusionFeatures.Vertices,
                    OutputEndPoints = FusionFeatures.EndPoints,
                    OutputCrossingPoints = FusionFeatures.CrossingPoints
                },
                new StrokeExtraction
                {
                    InputVertices = FusionFeatures.Vertices,
                    InputEndPoints = FusionFeatures.EndPoints,
                    InputCrossingPoints = FusionFeatures.CrossingPoints,
                    OutputStrokes = FusionFeatures.Strokes,
                    OutputConnects = FusionFeatures.Connects
                },
                new StrokeEdgeExtraction
                {
                    InputStrokes = FusionFeatures.Strokes,
                    InputConnects = FusionFeatures.Connects,
                    OutputStrokes = FusionFeatures.Strokes,
                    OutputStrokeEdgeList = FusionFeatures.StrokeEdgeList,
                    OutputEndPoints = FusionFeatures.EndPoints,
                    OutputCrossingPoints = FusionFeatures.CrossingPoints,
                    OutputInDirectList = FusionFeatures.InDirectStrokeEdgeList,
                    OutputNullStroke = FusionFeatures.NullStroke
                },
                new RelativePosition
                {
                    InputVertices = FusionFeatures.Vertices,
                    OutputVertices = FusionFeatures.Vertices
                }
                ,
                new VertexNormalization
                {
                    InputVertices = FusionFeatures.Vertices,
                    OutputVertices = FusionFeatures.Vertices
                },
                new MarosAlgorithm
                {
                    InputEnds = FusionFeatures.EndPoints,
                    InputStrokes = FusionFeatures.Strokes,
                    InputNullStroke = FusionFeatures.NullStroke,
                    OutputTrajectory = FusionFeatures.BaseTrajectory
                }/*,
                new XYFeatureFromTrajectory
                {
                    InputTrajectory = FusionFeatures.Trajectory,
                    OutputX = FusionFeatures.X,
                    OutputY = FusionFeatures.Y
                },
                new DOSBasedFeature {
                    InputX = FusionFeatures.X,
                    InputY = FusionFeatures.Y,
                    InputButton = FusionFeatures.Button,
                    OutputCurvature = FusionFeatures.Curvature
                }/*,
                /*new Normalize
                {
                    Input = FusionFeatures.X,
                    Output = FusionFeatures.X
                },
                new Normalize
                {
                    Input = FusionFeatures.Y,
                    Output = FusionFeatures.Y
                },
                new TangentExtraction
                {
                    X = FusionFeatures.X,
                    Y = FusionFeatures.Y,
                    OutputTangent = FusionFeatures.Tangent
                }*/
            };


            /*var benchmark = new VerifierBenchmark
            {
                Loader = loader,
                Logger = new SimpleConsoleLogger(),
                Verifier = new Verifier
                {
                    Pipeline = offlinepipeline,
                    Classifier = new DtwClassifier{
                            Features = { FusionFeatures.Curvature }
                    }
                },
                Sampler = new SVC2004Sampler1(),
            };*/
            /*var results = benchmark.Execute();
            foreach (var result in results.SignerResults)
            {
                Console.WriteLine(result.Signer + ": " + result.Frr.ToString() +
                                    " " + result.Far.ToString() + " " + result.Aer.ToString() );
            }
            Console.WriteLine(results.FinalResult.Aer + " " + results.FinalResult.Far + " " + results.FinalResult.Frr);
            */
            List<Signer> signers = loader.EnumerateSigners(p => true).ToList();
            offlinepipeline.Logger = new SimpleConsoleLogger();
            /*Signature sig = signers[0].Signatures[0];
            offlinepipeline.Transform(sig);
            StrokeSaver.Save(sig, @"VisualResults/00000grafosproba.png");
            TrajectorySaver.Save(sig, @"VisualResults/000000trajectoryproba.png");
            var list = sig.GetFeature<List<double>>(FusionFeatures.Curvature);
            foreach (var p in list)
            {
                Console.WriteLine(p);
            }*/
            /*foreach (var signer in signers)
            {
                Console.WriteLine(signer.ID);
                foreach (var sig in signer.Signatures)
                {
                    offlinepipeline.Transform(sig);
                    TrajectorySaver.Save(sig, @"VisualResults/" + signer.ID + "gr" + sig.ID + "Tr.png");
                    StrokeSaver.Save(sig, @"VisualResults/" + signer.ID + "gr" + sig.ID + "St.png");
                }
            }*/

            /**/
            //var baseSig = signers[0].Signatures[3];
            /*var trRemaker = new TrajectoryRemake()
            {
                InputStrokes = FusionFeatures.Strokes,
                InputTrajectory = FusionFeatures.BaseTrajectory,
                OutputTrajectory = FusionFeatures.Trajectory
            };
            trRemaker.Logger = new SimpleConsoleLogger();
            */
            var dtwTrRemaker = new DtwBasedTrajectoryRemake()
            {
                InputStrokes = FusionFeatures.Strokes,
                InputNullStroke = FusionFeatures.NullStroke,
                InputTrajectory = FusionFeatures.BaseTrajectory,
                OutputTrajectory = FusionFeatures.Trajectory
            };
            dtwTrRemaker.Logger = new SimpleConsoleLogger();
            int idx = 0;
            foreach (var sig in signers[idx].Signatures)
            {
                offlinepipeline.Transform(sig);
                /*var vertices = sig.GetFeature<VertexCollection>(FusionFeatures.Vertices);
                foreach (var p in vertices.Values)
                {
                    foreach (var d in p.RelPos)
                    {
                        Console.Write(d.ToString() + " ");
                    }
                    Console.WriteLine();
                }*/
            }
            var marosSaver = new TrajectorySaver() { InputTrajectory = FusionFeatures.BaseTrajectory };
            foreach (var sig in signers[idx].Signatures)
            {
                marosSaver.Save(sig, @"VisualResults/" + sig.ID + "based0000.png");
            }
            var trajSaver = new TrajectorySaver() { InputTrajectory = FusionFeatures.Trajectory };
            foreach (var baseSig in signers[idx].Signatures)
            {
                if (baseSig.Origin == Origin.Genuine)
                {
                    foreach (var sig in signers[idx].Signatures)
                    {
                        if (sig.Origin == Origin.Genuine)
                        {
                            dtwTrRemaker.Remake(sig, baseSig);
                            trajSaver.Save(sig, @"VisualResults/" + baseSig.ID + "based" + sig.ID + ".png");
                        }
                    }
                }
            }
            Console.ReadLine();
            
        }

    }
}
