using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace SigStat.Common
{
    /// <summary>
    /// TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.
    /// IProgress, ILogger, IPipelineIO default implementacioja.
    /// </summary>
    public abstract class PipelineBase : ILoggerObject, IProgress, IPipelineIO
    {
        public virtual List<PipelineInput> PipelineInputs { get; private set; }
        public virtual List<PipelineOutput> PipelineOutputs { get; private set; }

        public PipelineBase()
        {
            //init io
            PipelineInputs = this.GetType().GetFields()
                .Where(field => Attribute.IsDefined(field, typeof(Input)))
                .Select(field => new PipelineInput(this, field))
                .ToList();

            PipelineOutputs = this.GetType().GetFields()
                .Where(field => Attribute.IsDefined(field, typeof(Output)))
                .Select(field => {
                    var val = (FeatureDescriptor)field.GetValue(this);
                    if (val == null)
                    {
                        var attr = (Output)Attribute.GetCustomAttribute(field, typeof(Output));
                        var fdType = field.FieldType.GetGenericArguments()[0];
                        field.SetValue(this, FeatureDescriptor.Register(attr.Default ?? 
                            $"TMP_{this.GetType().ToString()}_{this.GetHashCode().ToString()}_{field.Name}" , fdType));
                    }
                    return new PipelineOutput(this, field);
                }).ToList();
        }

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
