using System;
using System.Collections.Generic;
using System.Linq;
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
        public object FD {
            get => FI.GetValue(PipelineItem);
            set => FI.SetValue(PipelineItem, value);
        }
        public AutoSetMode AutoSetMode => FI.GetCustomAttribute<Input>().AutoSetMode;
        public Type Type => FI.FieldType/*.GetGenericArguments()[0]*/;
        public bool IsCollectionOfFeatureDescriptors => Type.GetGenericTypeDefinition() == typeof(List<>);
        public string FieldName => FI.Name;

        private object PipelineItem;
        private FieldInfo FI;

        public PipelineInput(object PipelineItem, FieldInfo FI)
        {
            this.PipelineItem = PipelineItem;
            this.FI = FI;
            if (!FI.IsPublic)//ide is kene Logger
                throw new Exception($"Pipeline Input '{FieldName}' of '{PipelineItem.ToString()}' not public");
        }

    }

    public class PipelineOutput
    {
        public object FD
        {
            get => FI.GetValue(PipelineItem);
            set => FI.SetValue(PipelineItem, value);
        }
        public bool IsTemporary => FI.GetCustomAttribute<Output>().Default == null;
        public string Default => FI.GetCustomAttribute<Output>().Default;
        public Type Type => FI.FieldType/*.GetGenericArguments()[0]*/;
        public bool IsCollectionOfFeatureDescriptors => Type.GetGenericTypeDefinition() == typeof(List<>);
        public string FieldName => FI.Name;

        private object PipelineItem;
        private FieldInfo FI;

        public PipelineOutput(object PipelineItem, FieldInfo FI)
        {
            this.PipelineItem = PipelineItem;
            this.FI = FI;
            if (!FI.IsPublic)//ide is kene Logger
                throw new Exception($"Pipeline Output '{FieldName}' of '{PipelineItem.ToString()}' not public");
        }

    }

}
