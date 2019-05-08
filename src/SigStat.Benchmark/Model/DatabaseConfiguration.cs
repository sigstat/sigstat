using SigStat.Common;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Benchmark.Model
{
    class DatabaseConfiguration
    {
        public DataSetLoader DataSetLoader { get; set; }
        public Sampler Sampler { get; set; }
    }
}
