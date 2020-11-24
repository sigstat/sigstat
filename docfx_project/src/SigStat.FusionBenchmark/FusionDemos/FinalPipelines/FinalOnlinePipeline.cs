using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.FusionDemos.FinalPipelines
{
    class FinalOnlinePipeline : PipelineBase
    {
        [Input]
        public DataSetLoader OnlineLoader { get; set; }

        [Input]
        public SequentialTransformPipeline OnlinePipeline { get; set; }

        [Input]
        public bool IsOptimal { get; set; }

        public BenchmarkResults Execute()
        {
            var onlineSigners = OnlineLoader.EnumerateSigners().ToList();
            Parallel.ForEach(onlineSigners, onSigner =>
            {
                try
                {
                    Console.WriteLine(onSigner.ID + " started at " + DateTime.Now.ToString("h:mm:ss tt"));
                    Parallel.ForEach(onSigner.Signatures, onSig => OnlinePipeline.Transform(onSig));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            );
            var benchmark = FusionPipelines.GetBenchmark(onlineSigners, IsOptimal);
            return benchmark.Execute();
        }
    }
}
