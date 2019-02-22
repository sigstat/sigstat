using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SigStat.Common.Pipeline
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class Input : System.Attribute
    {
        public AutoSetMode AutoSetMode = AutoSetMode.IfNull;
        public Input(AutoSetMode AutoSetMode = AutoSetMode.IfNull)
        {
            this.AutoSetMode = AutoSetMode;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class Output : System.Attribute
    {
        //public FeatureDescriptor Default;
        string Default;
        public Output(string Default)
        {
            this.Default = Default;
        }
    }

    //Ötlet: 3. Attributenak felvehetjuk: InputOutput

    public enum AutoSetMode
    {
        IfNull,
        Always,
        Never
    }
}
