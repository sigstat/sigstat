using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers
{
    /// <summary>
    /// Enables logging by exposing a <see cref="Helpers.Logger"/> property.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets or sets the attached <see cref="Helpers.Logger"/> object used to log messages.
        /// </summary>
        Logger Logger { get; set; }

        //TODO: C# 8.0 default interface implementation, PipelineBase helyett.

    }
}
