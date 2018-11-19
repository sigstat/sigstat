using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a person as a <see cref="ID"/> and a list of <see cref="Signatures"/>.
    /// </summary>
    public class Signer//TODO: <TSignature> where TSignature: Signature
    {
        /// <summary>
        /// An identifier for the Signer. Keep it unique to be useful for logs.
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// List of signatures that belong to the signer. 
        /// (Their origin is not constrained to be genuine.)
        /// </summary>
        public virtual List<Signature> Signatures { get; set; } = new List<Signature>();

        //ezt cachelni kene, de azzal is lennenek bajok. Inkabb a Sampler osztaly megoldja
    }
}
