using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    // TODO: Find a better name for this enum
    /// <summary>Represents our knowledge on the origin of a signature. </summary>
    public enum Origin
    {
        /// <summary>Use this in practice before a signature is verified. </summary>
        Unknown = 0,
        /// <summary>The <see cref="Signature"/>'s origin is verified to be from <see cref="Signature.Signer"/></summary>
        Genuine,
        /// <summary>The <see cref="Signature"/> is a forgery.</summary>
        Forged
    }
}
