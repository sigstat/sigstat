using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// Exposes a function to enable loading collections of <see cref="Signer"/>s.
    /// Base abstract class: <see cref="DataSetLoader"/>.
    /// </summary>
    internal interface IDataSetLoader
    {
        /// <summary>
        /// Loads the database and returns the collection of <see cref="Signer"/>s that match the <paramref name="signerFilter"/>.
        /// </summary>
        /// <param name="signerFilter">Filter to specify which Signers to load. Example: (p=>p=="01")</param>
        /// <returns>Collection of <see cref="Signer"/>s that match the <paramref name="signerFilter"/></returns>
        IEnumerable<Signer> EnumerateSigners(Predicate<string> signerFilter = null);
    }
}
