using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using SigStat.Common.Helpers;
using System.IO.Compression;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// Stores and enumerates Signer data that has already been loaded
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class MemoryDataSetLoader : DataSetLoader
    {
        ///  <inheritdoc/>
        public override int SamplingFrequency => throw new NotImplementedException();

        /// <summary>
        /// Gets or sets the database path.
        /// </summary>
        public string DatabasePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether features are also loaded as <see cref="Features"/>
        /// </summary>
        public bool StandardFeatures { get; set; }
        /// <summary>
        /// Ignores any signers during the loading, that do not match the predicate
        /// </summary>
        public Predicate<Signer> SignerFilter { get; set; }

        private readonly List<Signer> signers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Svc2004Loader"/> class with specified database.
        /// </summary>
        [JsonConstructor]
        public MemoryDataSetLoader(IEnumerable<Signer> signers)
        {
            this.signers = signers.ToList();
        }

        /// <inheritdoc/>
        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {

            //TODO: EnumerateSigners should ba able to operate with a directory path, not just a zip file
            signerFilter = signerFilter ?? SignerFilter;
            if (signerFilter != null)
                return signers.Where(s => signerFilter(s));
            else
                return signers;

        }

    }
}
