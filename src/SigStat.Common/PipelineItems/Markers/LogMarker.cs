using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Markers
{
    /// <summary>
    /// Logs the Pipeline Input. Useful for logging TimeMarker results.
    /// <para>Default Pipeline Output: -</para>
    /// </summary>
    public class LogMarker : PipelineBase, ITransformation
    {
        private readonly LogLevel level;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogMarker"/> class with specified log level.
        /// </summary>
        /// <param name="level">The level to log the message at.</param>
        public LogMarker(LogLevel level)
        {
            this.level = level;
            OutputFeatures = new List<FeatureDescriptor>();//no output
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            Log(level, $"Signature{signature.ID}.{InputFeatures[0].Name} = {signature[InputFeatures[0]].ToString()}");
        }
    }
}
