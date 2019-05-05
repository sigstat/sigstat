using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.IO;

namespace SigStat.Common.Test.Loader
{
    [TestClass]
    public class MCYTLoaderTest
    {
        [TestMethod]
        public void TestLoadSignature()
        {
            //public static void LoadSignature(Signature signature, MemoryStream stream, bool standardFeature)

            /*
            string path = @"Databases\Offline\Generated\";
            Directory.CreateDirectory(path);

            //Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            MCYTLoader loader = new MCYTLoader(@"Databases\Online\MCYT100\MCYT_Signature_100.zip", true);

            List<Signer> signers = loader.EnumerateSigners(null).ToList();
            //  var signers = new List<Signer>(loader.EnumerateSigners());
            var pipeline = new SequentialTransformPipeline
            {
                new UniformScale() { BaseDimension=Features.X, ProportionalDimension = Features.Y, BaseDimensionOutput=Features.X, ProportionalDimensionOutput=Features.Y },
                new Normalize() { Input=Features.Pressure, Output=Features.Pressure },
                new Normalize() { Input=Features.Altitude, Output=Features.Altitude },
                new Map(0, Math.PI/2) { Input=Features.Altitude, Output=Features.Altitude },
                new Map(0, 2*Math.PI) { Input=Features.Azimuth, Output=Features.Azimuth },
                new RealisticImageGenerator(1280, 720),
            };
            pipeline.Logger = new SimpleConsoleLogger();

            for (int i = 0; i < signers.Count; i++)
            {
                for (int j = 0; j < signers[i].Signatures.Count; j++)
                {
                    pipeline.Transform(signers[i].Signatures[j]);
                    ImageSaver.Save(signers[i].Signatures[j], path + $"U{i + 1}S{j + 1}.png");
                    ProgressSecondary(null, (int)(j / (double)signers[i].Signatures.Count * 100.0));
                }
                Console.WriteLine($"Signer{signers[i].ID} ({i + 1}/{signers.Count}) signature images generated.");
                ProgressPrimary(null, (int)(i / (double)signers.Count * 100.0));

                */
            }
        }


}
    
