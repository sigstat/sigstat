using SigStat.Common.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Benchmark.Model
{
    public class BenchmarkReport
    {
        public string Config { get; set; }
        public string Split { get; set; }
        public string Database { get; set; }
        public string Distance { get; set; }


        public List<SignerResults> SignerResults { get; set; }

    }
}
