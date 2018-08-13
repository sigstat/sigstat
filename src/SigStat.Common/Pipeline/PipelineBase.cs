using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common
{
    /// <summary>
    /// TODO: C# 8.0 ban ezt atalakitani default implementacios interface be
    /// </summary>
    public abstract class PipelineBase
    {
        public List<FeatureDescriptor> InputFeatures { get; set; }
        public List<FeatureDescriptor> OutputFeatures { get; set; }

        public Logger Logger { get; set; }
        protected void Log(LogLevel level, string message)
        {
            if (Logger != null)
                Logger.AddEntry(level, this, message);
        }

        private int _progress = 0;
        public int Progress {
            get => _progress;
            protected set {
                if (_progress == value)
                    return; _progress = value;
                ProgressChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<int> ProgressChanged;

    }



}
