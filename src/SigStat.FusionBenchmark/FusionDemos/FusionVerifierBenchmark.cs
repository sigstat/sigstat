using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionDemos
{
    public class FusionBenchmarkResults
    {
        public BenchmarkResults OffOffResults { get; set; }
        public BenchmarkResults OffOnResults { get; set; }
        public BenchmarkResults OnOffResults { get; set; }
        public BenchmarkResults OnOnResults { get; set; }
    }
}
