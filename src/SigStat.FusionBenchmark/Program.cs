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

namespace SigStat.FusionBenchmark
{
    class Program
    {
        //Develop
        static void Main(string[] args)
        {
            StrokePairingExam.CalculateForID(new string[] {"001", "002", "003" });

            for (int i = 0; i < 10; i++)
                Console.ReadLine();
            Console.ReadLine();
        }
        


    }
}
