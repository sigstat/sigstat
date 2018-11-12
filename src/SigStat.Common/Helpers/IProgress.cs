using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers
{
    /// <summary>
    /// Enables progress tracking by expsoing the <see cref="Progress"/> property and the <see cref="ProgressChanged"/> event.
    /// </summary>
    public interface IProgress
    {
        /// <summary>
        /// Invoked whenever the <see cref="Progress"/> property is changed.
        /// </summary>
        event EventHandler<int> ProgressChanged;

        /// <summary>
        /// Gets the current progress in percentage.
        /// </summary>
        int Progress { get; }

        //TODO: C# 8.0 default interface implementation, PipelineBase helyett.

    }
}
