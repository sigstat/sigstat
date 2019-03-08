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
    /// ILoggerObject, IProgress, IPipelineIO default implementacioja.
    /// </summary>
    public abstract class PipelineBase : ILoggerObject, IProgress, IPipelineIO
    {
        public virtual List<PipelineInput> PipelineInputs { get; private set; }
        public virtual List<PipelineOutput> PipelineOutputs { get; private set; }

        public PipelineBase()
        {
            //init io
            PipelineInputs = this.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(Input)))
                .Select(prop => new PipelineInput(this, prop))
                .ToList();

            PipelineOutputs = this.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(Output)))
                .Select(prop => {
                    var val = prop.GetValue(this);
                    if (val == null)
                    {
                        var attr = (Output)Attribute.GetCustomAttribute(prop, typeof(Output));
                        var propType = prop.PropertyType;
                        if (!(propType.GetGenericTypeDefinition() == typeof(List<>)))
                        {
                            prop.SetValue(this, FeatureDescriptor.Register(attr.Default ??
                                $"TMP_{this.GetType().ToString()}_{this.GetHashCode().ToString()}_{prop.Name}", propType.GetGenericArguments()[0]));
                        }
                        else
                        {//List of Features
                            //prop.SetValue(this, new List<FeatureDescriptor>());
                            //de mennyit? nem eleg egy ures listat letrehozni, mert akkor a transform nak kell megadnia az FD-kat
                            //most hack: transform mondja meg, itt marad null
                        }
                    }
                    return new PipelineOutput(this, prop);
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
