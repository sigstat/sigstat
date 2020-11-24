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
    public interface ITransformation : IPipelineIO
    {
        /// <summary>
        /// Executes the transform on the <paramref name="signature"/> parameter.
        /// This function gets called by the pipeline.
        /// </summary>
        /// <param name="signature">The <see cref="Signature"/> with a set of features to be transformed.</param>
        void Transform(Signature signature);
    }


}
