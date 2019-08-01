using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SigStat.Common.Pipeline
{
    /// <summary>
    /// Annotates an input <see cref="FeatureDescriptor"/> in a transformation pipeline
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Input : System.Attribute
    {
        /// <summary>
        /// The automatic set mode
        /// </summary>
        public AutoSetMode AutoSetMode = AutoSetMode.IfNull;
        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> class.
        /// </summary>
        /// <param name="AutoSetMode">The automatic set mode.</param>
        public Input(AutoSetMode AutoSetMode = AutoSetMode.IfNull)
        {
            this.AutoSetMode = AutoSetMode;
        }
    }

    /// <summary>
    /// Annotates an output <see cref="FeatureDescriptor"/> in a transformation pipeline
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Output : System.Attribute
    {
        /// <summary>
        /// The default value for the property
        /// </summary>
        public string Default;

        /// <summary>
        /// Initializes a new instance of the <see cref="Output"/> class.
        /// </summary>
        /// <param name="Default">The default.</param>
        public Output(string Default)
        {
            this.Default = Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Output"/> class.
        /// </summary>
        public Output()
        {
            this.Default = null;//-> temporary
        }

    }

    /// <summary>
    /// Default strategy to set the value of a property
    /// </summary>
    public enum AutoSetMode
    {
        /// <summary>
        /// Set the value if it is null
        /// </summary>
        IfNull,
        /// <summary>
        /// Always set the value
        /// </summary>
        Always,
        /// <summary>
        /// Never set the value
        /// </summary>
        Never
    }
}
