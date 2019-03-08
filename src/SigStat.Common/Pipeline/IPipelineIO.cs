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
            get => PI.GetValue(PipelineItem);
            set => PI.SetValue(PipelineItem, value);
        }
        public AutoSetMode AutoSetMode => PI.GetCustomAttribute<Input>().AutoSetMode;
        public Type Type => PI.PropertyType/*.GetGenericArguments()[0]*/;
        public bool IsCollectionOfFeatureDescriptors => Type.GetGenericTypeDefinition() == typeof(List<>);
        public string PropName => PI.Name;

        private object PipelineItem;
        private PropertyInfo PI;

        public PipelineInput(object PipelineItem, PropertyInfo PI)
        {
            this.PipelineItem = PipelineItem;
            this.PI = PI;
            if (!(PI.GetMethod.IsPublic && PI.SetMethod.IsPublic))//ide is kene Logger
                throw new Exception($"Pipeline Input '{PropName}' of '{PipelineItem.ToString()}' not public");
        }

    }

    public class PipelineOutput
    {
        public object FD
        {
            get => PI.GetValue(PipelineItem);
            set => PI.SetValue(PipelineItem, value);
        }
        public bool IsTemporary => PI.GetCustomAttribute<Output>().Default == null;
        public string Default => PI.GetCustomAttribute<Output>().Default;
        public Type Type => PI.PropertyType/*.GetGenericArguments()[0]*/;
        public bool IsCollectionOfFeatureDescriptors => Type.GetGenericTypeDefinition() == typeof(List<>);
        public string PropName => PI.Name;

        private object PipelineItem;
        private PropertyInfo PI;

        public PipelineOutput(object PipelineItem, PropertyInfo PI)
        {
            this.PipelineItem = PipelineItem;
            this.PI = PI;
            if (!(PI.GetMethod.IsPublic && PI.SetMethod.IsPublic))//ide is kene Logger
                throw new Exception($"Pipeline Output '{PropName}' of '{PipelineItem.ToString()}' not public");
        }

    }

}
