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
    public abstract class PipelineBase : ILoggerObject, IProgress
    {
        public List<(FeatureDescriptor, FieldInfo, AutoSetMode)> GetInputFeatures2()
        {
            return this.GetType().GetFields()
                .Where(field => Attribute.IsDefined(field, typeof(Input)))
                .Select(field => ((FeatureDescriptor)field.GetValue(this), field, field.GetCustomAttribute<Input>().AutoSetMode))
                .ToList();
        }

        /// <inheritdoc/>
        public List<FeatureDescriptor> InputFeatures { get; set; } = new List<FeatureDescriptor>();
        public virtual List<FeatureDescriptor> GetInputFeatures()
        {
            //Reflection: get fields with the Input attribute
            var fields = this.GetType().GetFields()
                .Where(field => Attribute.IsDefined(field, typeof(Input)));
            return fields.Select(field => (FeatureDescriptor)field.GetValue(this)).ToList();
        }
        /// <inheritdoc/>
        public List<FeatureDescriptor> OutputFeatures { get; set; } = new List<FeatureDescriptor>();
        public virtual List<FeatureDescriptor> GetOutputFeatures()
        {
            //Reflection: get fields with the Output attribute

            var fields = this.GetType().GetFields().Where(
                field => Attribute.IsDefined(field, typeof(Output)));

            return fields.Select(field => 
                (FeatureDescriptor)field.GetValue(this)?? //output was defined
                FeatureDescriptor.Register(field.Name, field.GetType()/*BAJ*/)         //or use default feature
            ).ToList();
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
