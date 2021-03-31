using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SigStat.Common.Pipeline
{
    /// <summary>
    /// Supports the definition of <see cref="PipelineInput"/> and <see cref="PipelineOutput"/>
    /// </summary>
    public interface IPipelineIO
    {
        /// <summary>
        /// A collection of inputs for the pipeline elements
        /// </summary>
        List<PipelineInput> PipelineInputs { get; }

        /// <summary>
        /// A collection of outputs for the pipeline elements
        /// </summary>
        List<PipelineOutput> PipelineOutputs { get; }
    }

    /// <summary>
    /// Represents an input for a <see cref="PipelineItem"/>
    /// </summary>
    public class PipelineInput
    {
        /// <summary>
        /// Gets or sets the fd.
        /// </summary>
        public object FD
        {
            get => PI.GetValue(PipelineItem);
            set => PI.SetValue(PipelineItem, value);
        }
        /// <summary>
        /// Gets the AutoSetMode
        /// </summary>
        public AutoSetMode AutoSetMode => PI.GetCustomAttribute<Input>().AutoSetMode;
        /// <summary>
        /// Gets the type of the property
        /// </summary>
        public Type Type => PI.PropertyType/*.GetGenericArguments()[0]*/;
        /// <summary>
        /// Gets a value indicating whether this instance is collection of feature descriptors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is collection of feature descriptors; otherwise, <c>false</c>.
        /// </value>
        public bool IsCollectionOfFeatureDescriptors => Type.GetGenericTypeDefinition() == typeof(List<>);
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropName => PI.Name;

        private readonly object PipelineItem;
        private readonly PropertyInfo PI;

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineInput"/> class.
        /// </summary>
        /// <param name="PipelineItem">The pipeline item.</param>
        /// <param name="PI">The pi.</param>
        /// <exception cref="Exception">Pipeline Input '{PropName}' of '{PipelineItem.ToString()}' not public</exception>
        public PipelineInput(object PipelineItem, PropertyInfo PI)
        {
            this.PipelineItem = PipelineItem;
            this.PI = PI;
            if (!(PI.GetMethod.IsPublic && PI.SetMethod.IsPublic))//ide is kene Logger
                throw new InvalidOperationException($"Pipeline Input '{PropName}' of '{PipelineItem.ToString()}' not public");
        }

    }

    /// <summary>
    /// Represents an output for a <see cref="PipelineItem"/>
    /// </summary>
    public class PipelineOutput
    {
        /// <summary>
        /// Gets or sets the fd.
        /// </summary>
        public object FD
        {
            get => PI.GetValue(PipelineItem);
            set => PI.SetValue(PipelineItem, value);
        }
        /// <summary>
        /// Gets a value indicating whether this instance is temporary.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is temporary; otherwise, <c>false</c>.
        /// </value>
        public bool IsTemporary => PI.GetCustomAttribute<Output>().Default == null;
        /// <summary>
        /// Gets the default value
        /// </summary>
        public string Default => PI.GetCustomAttribute<Output>().Default;
        /// <summary>
        /// Gets the type of the property
        /// </summary>
        public Type Type => PI.PropertyType/*.GetGenericArguments()[0]*/;
        /// <summary>
        /// Gets a value indicating whether this instance is collection of feature descriptors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is collection of feature descriptors; otherwise, <c>false</c>.
        /// </value>
        public bool IsCollectionOfFeatureDescriptors => Type.GetGenericTypeDefinition() == typeof(List<>);
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropName => PI.Name;

        private readonly object PipelineItem;
        private readonly PropertyInfo PI;

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineOutput"/> class.
        /// </summary>
        /// <param name="PipelineItem">The pipeline item.</param>
        /// <param name="PI">The pi.</param>
        /// <exception cref="Exception">Pipeline Output '{PropName}' of '{PipelineItem.ToString()}' not public</exception>
        public PipelineOutput(object PipelineItem, PropertyInfo PI)
        {
            this.PipelineItem = PipelineItem;
            this.PI = PI;
            if (!(PI.GetMethod.IsPublic && PI.SetMethod.IsPublic))//ide is kene Logger
                throw new InvalidOperationException($"Pipeline Output '{PropName}' of '{PipelineItem.ToString()}' not public");
        }

    }

}
