using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Transforms;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SigStat.Common.Test.Loader
{
    [TestClass]
    public class MCYTLoaderTest
    {
        [TestMethod]
        public void TestLoadSignature()
        {

            //            //public static void LoadSignature(Signature signature, MemoryStream stream, bool standardFeature)

            //            string path = @"Databases\Offline\Generated\";
            //            Directory.CreateDirectory(path);

            //            //Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            //            MCYTLoader loader = new MCYTLoader(@"Databases\Online\MCYT100\MCYT_Signature_100.zip", true);

            //            List<Signer> signers = loader.EnumerateSigners(null).ToList();
            //            //  var signers = new List<Signer>(loader.EnumerateSigners());
            //            var pipeline = new SequentialTransformPipeline
            //            {
            //                new UniformScale() { BaseDimension=SigStat.Common.Features.X, ProportionalDimension = SigStat.Common.Features.Y, BaseDimensionOutput=SigStat.Common.Features.X, ProportionalDimensionOutput=SigStat.Common.Features.Y },
            //                new Normalize() { Input=SigStat.Common.Features.Pressure, Output=SigStat.Common.Features.Pressure },
            //                new Normalize() { Input=SigStat.Common.Features.Altitude, Output=SigStat.Common.Features.Altitude },
            //                new Map(0, Math.PI/2) { Input=SigStat.Common.Features.Altitude, Output=SigStat.Common.Features.Altitude },
            //                new Map(0, 2*Math.PI) { Input=SigStat.Common.Features.Azimuth, Output=SigStat.Common.Features.Azimuth },
            //                new RealisticImageGenerator(1280, 720),
            //            };
            //            pipeline.Logger = new SimpleConsoleLogger();

            //            for (int i = 0; i < signers.Count; i++)
            //            {
            //                for (int j = 0; j < signers[i].Signatures.Count; j++)
            //                {
            //                    pipeline.Transform(signers[i].Signatures[j]);
            //                    ImageSaver.Save(signers[i].Signatures[j], path + $"U{i + 1}S{j + 1}.png");
            //                    ProgressSecondary(null, (int)(j / (double)signers[i].Signatures.Count * 100.0));
            //                }
            //                Console.WriteLine($"Signer{signers[i].ID} ({i + 1}/{signers.Count}) signature images generated.");
            //                ProgressPrimary(null, (int)(i / (double)signers.Count * 100.0));

            //            }
        }


    }
}
    
