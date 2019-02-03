using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace SigStat.Common
{
    /// <summary>
    /// TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.
    /// IProgress, ILogger, IPipelineIO default implementacioja.
    /// </summary>
    public abstract class PipelineBase : ILoggerObject, IProgress
    {
        /// <inheritdoc/>
        public List<FeatureDescriptor> InputFeatures { get; set; } = new List<FeatureDescriptor>();
        /// <inheritdoc/>
        public List<FeatureDescriptor> OutputFeatures { get; set; } = new List<FeatureDescriptor>();

        /// <inheritdoc/>
        public ILogger Logger { get; set; }

        private int progress = 0;
        /// <inheritdoc/>
        public int Progress
        {
            get { return progress; }
            protected set
            {
                if (progress == value)
                    return;
                progress = value;
                OnProgressChanged();
            }
        }


        /// <summary>
        /// The event is raised whenever the value of <see cref="Progress"/> changes
        /// </summary>
        public event EventHandler<int> ProgressChanged;

        /// <summary>
        /// Raises the <see cref="ProgressChanged"/> event
        /// </summary>
        protected virtual void OnProgressChanged()
        {
            ProgressChanged?.Invoke(this, Progress);
        }
    }



}
