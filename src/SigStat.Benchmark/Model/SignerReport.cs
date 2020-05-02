using SigStat.Common.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Benchmark.Model
{
    public class BenchmarkReport
    {
        public string Config { get; set; }
        public List<SignerResults> SignerResults { get; set; }

    }
}
