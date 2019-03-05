using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SigStat.Common.Pipeline
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class Input : System.Attribute
    {
        public AutoSetMode AutoSetMode = AutoSetMode.IfNull;
        public Input(AutoSetMode AutoSetMode = AutoSetMode.IfNull)
        {
            this.AutoSetMode = AutoSetMode;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class Output : System.Attribute
    {
        public string Default;
        //public Type Type;//ezzel is lehetne force-olni a tipus megadasat

        public Output(string Default)
        {
            this.Default = Default;
        }

        public Output()
        {
            this.Default = null;//-> temporary
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
