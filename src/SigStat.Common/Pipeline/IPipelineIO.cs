using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SigStat.Common.Pipeline
{
    public interface IPipelineIO
    {
        List<PipelineInput> PipelineInputs { get; }

        List<PipelineOutput> PipelineOutputs { get; }
    }

    public class PipelineInput
    {
        public FeatureDescriptor FD {
            get => (FeatureDescriptor)FI.GetValue(PipelineItem);
            set => FI.SetValue(PipelineItem, value);
        }

        public AutoSetMode AutoSetMode
        {
            get => FI.GetCustomAttribute<Input>().AutoSetMode;
        }

        private object PipelineItem;
        private FieldInfo FI;

        public PipelineInput(object PipelineItem, FieldInfo FI)
        {
            this.PipelineItem = PipelineItem;
            this.FI = FI;
        }

    }

    public class PipelineOutput
    {
        public FeatureDescriptor FD
        {
            get => (FeatureDescriptor)FI.GetValue(PipelineItem);
            set => FI.SetValue(PipelineItem, value);
        }
        public bool IsTemporary => FI.GetCustomAttribute<Output>().Default == null;
        public string Default => FI.GetCustomAttribute<Output>().Default;

        private object PipelineItem;
        private FieldInfo FI;

        public PipelineOutput(object PipelineItem, FieldInfo FI)
        {
            this.PipelineItem = PipelineItem;
            this.FI = FI;
        }

    }

}
