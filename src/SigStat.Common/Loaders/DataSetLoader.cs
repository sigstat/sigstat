using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Loaders
{
    public abstract class DataSetLoader : IDataSetLoader, ILogger
    {
        public Logger Logger { get; set; }
        protected void Log(LogLevel level, string message)
        {
            if (Logger != null)
                Logger.EnqueueEntry(level, this, message);
        }

        public abstract IEnumerable<Signer> EnumerateSigners(Predicate<string> signerFilter = null);

    }
}
