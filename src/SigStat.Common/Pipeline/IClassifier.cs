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
        IClassificationModel Train(List<Signature> signatures);
    }

   
}
