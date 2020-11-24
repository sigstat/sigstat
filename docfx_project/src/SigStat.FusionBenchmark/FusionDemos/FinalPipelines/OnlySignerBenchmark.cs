using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionDemos.FinalPipelines
{
    public static class OnlySignerBenchmark
    {
        public static BenchmarkResults BenchmarkWithSigner(bool isOptimal, Signer signer)
        {
            var benchmark = FusionPipelines.GetBenchmark(new List<Signer> { signer }, isOptimal);
            var results = benchmark.Execute();
            Resultout(results, signer);
            return results;
        }

        private static void Resultout(BenchmarkResults result, Signer signer)
        {
            Console.WriteLine(signer.ID + " " + result.FinalResult.Frr.ToString() + " " + result.FinalResult.Far.ToString() + " " + result.FinalResult.Aer.ToString());
        }
    }
}
