using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.FusionBenchmark.ReSamplingFeatures;
using SigStat.FusionBenchmark.VisualHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.FusionBenchmark.FusionDemos.ReSamplingBenchmarks
{
    public static class ReSamplingExtractions
    { 
        public static Tuple<List<double>, List<List<double>>> Calculate(DataSetLoader loader)
        {
            var signers = loader.EnumerateSigners().ToList();
            var resampler = new ReSamplingFeatureExtraction()
            {
                InputButton = FusionFeatures.Button,
                InputX = FusionFeatures.X,
                InputY = FusionFeatures.Y
            };
            var results = resampler.Calculate(signers);
            TxtHelper.Save(TxtHelper.ReSamplingResultsToLines(results), "resamplingdataset");
            return results;
        }
    }
}
