using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common
{
    /// <summary>
    /// TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.
    /// IProgress, ILogger, IPipelineIO default implementacioja.
    /// </summary>
    public abstract class PipelineBase
    {
        /// <inheritdoc/>
        public List<FeatureDescriptor> InputFeatures { get; set; } = new List<FeatureDescriptor>();
        /// <inheritdoc/>
        public List<FeatureDescriptor> OutputFeatures { get; set; } = new List<FeatureDescriptor>();

        /// <inheritdoc/>
        public Logger Logger { get; set; }

        /// <summary>
        /// Enqueues a new log entry to be consumed by the attached <see cref="Helpers.Logger"/>. Use this when developing new pipeline items.
        /// </summary>
        /// <param name="level">Specifies the level of the entry. Higher levels than the <see cref="Helpers.Logger"/>'s filter level will be ignored.</param>
        /// <param name="message">The main content of the log entry.</param>
        protected void Log(LogLevel level, string message)
        {
            if (Logger != null)
                Logger.EnqueueEntry(level, this, message);
        }

        private int _progress = 0;
        /// <inheritdoc/>
        public int Progress {
            get => _progress;
            protected set {
                if (_progress == value)
                    return; _progress = value;
                ProgressChanged?.Invoke(this, value);
            }
        }
        /// <inheritdoc/>
        public event EventHandler<int> ProgressChanged;

        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-raise-base-class-events-in-derived-classes
        /// <summary>
        /// Used to raise base class event in derived classes.
        /// See explanation: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-raise-base-class-events-in-derived-classes">Event docs link</see>.
        /// </summary>
        /// <param name="v"></param>
        public void OnProgressChanged(int v)
        {
            //ez sem fog kelleni default interface implementacioval.
            ProgressChanged?.Invoke(this, v);
        }

    }



}
