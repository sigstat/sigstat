using Microsoft.Extensions.Logging;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// Abstract loader class to inherit from. Implements ILogger.
    /// </summary>
    public abstract class DataSetLoader : IDataSetLoader, ILoggerObject
    {
        
    /// <inheritdoc/>
    public ILogger Logger { get; set; }

       

        /// <inheritdoc/>
        public IEnumerable<Signer> EnumerateSigners()
        {
            return EnumerateSigners(null);
        }
        /// <inheritdoc/>
        public abstract IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter);

      
        /// <summary>
        /// Sampling frequency for each database
        /// </summary>
        public abstract int SamplingFrequency {get;}
        /// <summary>
        /// Ignores any signers during the loading, that do not match the predicate
        /// </summary>
        
        Predicate<Signer> IDataSetLoader.SignerFilter { get ; set ; }
    }
}
