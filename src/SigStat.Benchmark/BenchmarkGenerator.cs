using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Benchmark
{
    class BenchmarkGenerator
    {
        private HashSet<IClassifier> Classifiers;
        private HashSet<Sampler> Samplers;
        private HashSet<DataSetLoader> Loaders;
        private HashSet<ITransformation> Transformations;
        private HashSet<IInterpolation> Interpolations;
        private HashSet<FeatureDescriptor> Features;
        private HashSet<string> Distance;

        public List<VerifierBenchmark> generate()
        {
            return new List<VerifierBenchmark>();
        }
    }
}
