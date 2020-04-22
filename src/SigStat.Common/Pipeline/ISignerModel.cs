using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Pipeline
{
    /// <summary>
    /// Analyzes signatures based on their similiarity to the trained model
    /// </summary>
    public interface ISignerModel
    {
        /// <summary>
        /// Identifies the signer, to whom this model belongs to
        /// </summary>
        /// <value>
        /// The signer identifier.
        /// </value>
        string SignerID { get; }
    }
}
