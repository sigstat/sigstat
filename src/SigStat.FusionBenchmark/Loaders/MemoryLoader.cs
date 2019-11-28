using SigStat.Common;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.Loaders
{
    public class MemoryLoader : DataSetLoader
    {
        public List<Signer> Signers { get;set; }
        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            return Signers;
        }
    }
}
