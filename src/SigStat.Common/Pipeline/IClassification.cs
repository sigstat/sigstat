using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Allows implementing a pipeline classifier item capable of logging, progress tracking and IO rewiring.
    /// </summary>
    public interface IClassification : ILogger, IProgress, IPipelineIO
    {
        /// <summary>
        /// Executes the classification by pairing the parameters.
        /// This function gets called by the pipeline.
        /// </summary>
        /// <param name="signature1">The <see cref="Signature"/> with a set of features to classify by.</param>
        /// <param name="signature2">The <see cref="Signature"/> with a set of features to classify by.</param>
        /// <returns>Cost between the two signatures</returns>
        double Pair(Signature signature1, Signature signature2);
    }


    /// <summary>
    /// Extension methods for <see cref="IClassification"/> for convenient IO rewiring.
    /// </summary>
    public static class IClassificationMethods
    {//TODO: ez is lehet majd default interface implementation C# 8.0 ban
        /// <summary>
        /// Sets the InputFeatures in a convenient way.
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="inputFeatures"></param>
        /// <returns>The caller.</returns>
        public static IClassification Input(this IClassification caller, params FeatureDescriptor[] inputFeatures)
        {
            caller.InputFeatures = inputFeatures.ToList();
            return caller;
        }
        /// <summary>
        /// Sets the OutputFeatures in a convenient way.
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="outputFeatures"></param>
        /// <returns>The caller.</returns>
        public static IClassification Output(this IClassification caller, params FeatureDescriptor[] outputFeatures)
        {
            caller.OutputFeatures = outputFeatures.ToList();
            return caller;
        }
    }

}
