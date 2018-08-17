using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Allows implementing a pipeline transform item capable of logging, progress tracking and IO rewiring.
    /// </summary>
    public interface ITransformation : ILogger, IProgress, IPipelineIO
    {
        /// <summary>
        /// Executes the transform on the <paramref name="signature"/> parameter.
        /// This function gets called by the pipeline.
        /// </summary>
        /// <param name="signature">The <see cref="Signature"/> with a set of features to be transformed.</param>
        void Transform(Signature signature);
    }

    /// <summary>
    /// Extension methods for <see cref="ITransformation"/> for convenient IO rewiring.
    /// </summary>
    public static class ITransformationMethods
    {//TODO: ez is lehet majd default interface implementation C# 8.0 ban
        /// <summary>
        /// Sets the InputFeatures in a convenient way.
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="inputFeatures"></param>
        /// <returns>The caller.</returns>
        public static ITransformation Input(this ITransformation caller, params FeatureDescriptor[] inputFeatures)
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
        public static ITransformation Output(this ITransformation caller, params FeatureDescriptor[] outputFeatures)
        {
            caller.OutputFeatures = outputFeatures.ToList();
            return caller;
        }
    }

}
