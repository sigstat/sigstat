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
        private List<IClassifier> Classifiers;
        private List<Tuple<DataSetLoader, Sampler>> Databases;
        private List<List<ITransformation>> TransformationGroups;
        private List<List<FeatureDescriptor>> FeatureGroups;

        public List<VerifierBenchmark> Generate()
        {
            return new List<VerifierBenchmark>();
        }
    }
}
