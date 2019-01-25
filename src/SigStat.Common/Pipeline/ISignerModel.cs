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
        /// Returns a double value in the range [0..1], representing the probability of the given signature belonging to the trained model.
        /// <list type="bullet">
        /// <item>0: non-match</item>
        /// <item>0.5: inconclusive</item>
        /// <item>1: match</item>
        /// </list>
        /// </summary>
        /// <param name="signature"></param>
        /// <returns></returns>
        double Test(Signature signature);
    }
}
