using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// Abstract loader class to inherit from. Implements ILogger.
    /// </summary>
    public abstract class DataSetLoader : IDataSetLoader, ILogger
    {
        /// <inheritdoc/>
        public Logger Logger { get; set; }
        /// <inheritdoc/>
        protected void Log(LogLevel level, string message)
        {
            if (Logger != null)
                Logger.EnqueueEntry(level, this, message);
        }

        /// <inheritdoc/>
        public abstract IEnumerable<Signer> EnumerateSigners(Predicate<string> signerFilter = null);

    }
}
