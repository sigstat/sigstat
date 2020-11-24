using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.Common.Pipeline;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.FusionDemos.FinalPipelines
{
    class MarosPipelineBenchmark : PipelineBase
    {
        [Input]
        public DataSetLoader OnlineLoader { get; set; }

        [Input]
        public DataSetLoader OfflineLoader { get; set; }

        [Input]
        public SequentialTransformPipeline MarosPipeline { get; set; }

        [Input]
        public SequentialTransformPipeline OnlinePipeline { get; set; }

        [Input]
        public bool IsOptimal { get; set; }

        [Input]
        public int InputBaseSigInputCntID { get; set; }

        public FusionBenchmarkResults Execute()
        {
            var offlineSigners = OfflineLoader.EnumerateSigners().ToList();
            var onlineSigners = OnlineLoader.EnumerateSigners().ToList();

            var offOnSigners = new List<Signer>();
            var onOffSigners = new List<Signer>();

            var fusionPipeline = FinalFusionPipelines.GetFusionPipeline(onlineSigners, true, InputBaseSigInputCntID);
            foreach (var offSigner in offlineSigners)
            {
                try
                {
                    Console.WriteLine(offSigner.ID + " started at " + DateTime.Now.ToString("h:mm:ss tt"));
                    var onSigner = onlineSigners.Find(signer => signer.ID == offSigner.ID);
                    Parallel.ForEach(offSigner.Signatures, offSig =>
                    {
                        try
                        {
                            MarosPipeline.Transform(offSig);
                            var onSig = onSigner.Signatures.Find(sig => sig.ID == offSig.ID);
                            onSig.SetFeature<Image<Rgba32>>(FusionFeatures.Image, offSig.GetFeature(FusionFeatures.Image));
                            var onToOnPipeline = FinalFusionPipelines.GetHackedOnToOnPipeline(offSig.GetFeature(FusionFeatures.Bounds));
                            onToOnPipeline.Transform(onSig);
                            OnlinePipeline.Transform(onSig);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
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

            var offoffBenchmark = FusionPipelines.GetBenchmark(offlineSigners, IsOptimal);
            var offonBenchmark = FusionPipelines.GetBenchmark(offOnSigners, IsOptimal);
            var onoffBenchamrk = FusionPipelines.GetBenchmark(onOffSigners, IsOptimal);
            var ononBenchmark = FusionPipelines.GetBenchmark(onlineSigners, IsOptimal);


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
