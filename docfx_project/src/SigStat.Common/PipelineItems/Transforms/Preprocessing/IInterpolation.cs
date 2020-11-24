using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Represents an interploation algorithm
    /// </summary>
    public interface IInterpolation
    {
        /// <summary>
        /// Gets or sets the feature values.
        /// </summary>
        List<double> FeatureValues { get; set; }
        /// <summary>
        /// Timestamps
        /// </summary>
        List<double> TimeValues { get; set; }

        /// <summary>
        /// Gets the interpolated value at a given timestamp
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns></returns>
        double GetValue(double timestamp);
    }
}
