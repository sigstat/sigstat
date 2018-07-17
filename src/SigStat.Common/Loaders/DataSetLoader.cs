using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Loaders
{
    public abstract class DataSetLoader : IDataSetLoader 
    {
        public abstract IEnumerable<Signer> EnumerateSigners(Predicate<string> signerFilter = null);

    }
}
