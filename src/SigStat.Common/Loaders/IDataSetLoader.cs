using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Loaders
{
    public interface IDataSetLoader
    {
        IEnumerable<Signer> EnumerateSigners(Predicate<string> signerFilter = null);
    }
}
