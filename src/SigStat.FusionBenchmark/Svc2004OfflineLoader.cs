using SigStat.Common;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark
{
    // Svc2004Loader mintájára meg tudod írni
    class Svc2004OfflineLoader : DataSetLoader
    {
        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            throw new NotImplementedException();
        }
    }
}
