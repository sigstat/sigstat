using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.FusionBenchmark.VisualHelpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.FusionDemos.PipelineBenchmarks
{

    public static class FusionVerifierBenchmark
    {

        public static FusionBenchmarkResults BenchMarkingWithAllSigners(bool isoptimal, DataSetLoader offlineLoader, DataSetLoader onlineLoader)
        {

            var offlineSigners = offlineLoader.EnumerateSigners().ToList();
            var onlineSigners = onlineLoader.EnumerateSigners().ToList();


            var offlinePipeline = FusionPipelines.GetOfflinePipeline();
            var onlinePipeline = FusionPipelines.GetOnlinePipeline();
            var fusionPipeline = FusionPipelines.GetFusionPipeline(onlineSigners, false, 0);

            var offOnSigners = new List<Signer>();
            var onOffSigners = new List<Signer>();

            Parallel.ForEach(offlineSigners, offSigner =>
            {
                try
                {
                    Console.WriteLine(offSigner.ID + " started at " + DateTime.Now.ToString("h:mm:ss tt"));
                    var onSigner = onlineSigners.Find(signer => signer.ID == offSigner.ID);
                    Parallel.ForEach(offSigner.Signatures, offSig =>
                    {
                        offlinePipeline.Transform(offSig);
                        var onSig = onSigner.Signatures.Find(sig => sig.ID == offSig.ID);
                        onSig.SetFeature<Image<Rgba32>>(FusionFeatures.Image, offSig.GetFeature(FusionFeatures.Image));
                        var onToOnPipeline = FusionPipelines.GetHackedOnToOnPipeline(offSig.GetFeature(FusionFeatures.Bounds));
                        onToOnPipeline.Transform(onSig);
                        onlinePipeline.Transform(onSig);
                    }
                    );
                    Parallel.ForEach(offSigner.Signatures, offSig =>
                    {
                        fusionPipeline.Transform(offSig);
                        onlinePipeline.Transform(offSig);
                    }
                    );

                    var offonSigner = FusionPipelines.GetMixedSigner(offSigner, onSigner);
                    var onoffSigner = FusionPipelines.GetMixedSigner(onSigner, offSigner);
                    offOnSigners.Add(offonSigner);
                    onOffSigners.Add(onoffSigner);
                    Console.WriteLine(offSigner.ID + " finished at " + DateTime.Now.ToString("h:mm:ss tt"));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            );


            var offoffBenchmark = FusionPipelines.GetBenchmark(offlineSigners, isoptimal);
            var offonBenchmark = FusionPipelines.GetBenchmark(offOnSigners, isoptimal);
            var onoffBenchamrk = FusionPipelines.GetBenchmark(onOffSigners, isoptimal);
            var ononBenchmark = FusionPipelines.GetBenchmark(onlineSigners, isoptimal);


            return new FusionBenchmarkResults
            {
                OffOffResults = offoffBenchmark.Execute(),
                OffOnResults = offonBenchmark.Execute(),
                OnOffResults = onoffBenchamrk.Execute(),
                OnOnResults = ononBenchmark.Execute()
            };

        }

    }
}
