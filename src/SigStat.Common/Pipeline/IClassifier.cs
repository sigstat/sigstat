using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Pipeline
{
    /// <summary>
    /// Trains classification models based on reference signatures
    /// </summary>
    public interface IClassifier
    {
        /// <summary>
        /// Trains a model based on the signatures and returns the trained model
        /// </summary>
        /// <param name="signatures"></param>
        /// <returns></returns>
        ISignerModel Train(List<Signature> signatures);

        /// <summary>
        /// Returns a double value in the range [0..1], representing the probability of the given signature belonging to the trained model.
        /// <list type="bullet">
        /// <item>0: non-match</item>
        /// <item>0.5: inconclusive</item>
        /// <item>1: match</item>
        /// </list>
        /// </summary>
        /// <param name="signature">The signature to test</param>
        /// <param name="model">The model aquired from the <see cref="Train(List{Signature})"/> method</param>
        /// <returns></returns>
        double Test(ISignerModel model, Signature signature);
    }

   
}
