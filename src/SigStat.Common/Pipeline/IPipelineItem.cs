using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common
{
    /*public interface IPipelineItem
    {
        //void Run(object/Signature input);
    }*/

    /*public interface IPipelineItem<T> where T : IPipelineItem
    {
        //void Run(object/Signature input);
        
    }*/

    /*public interface IPipelineItem<ITransformation>//where T : IPipelineItem
    {
        //void Run(object/Signature input);
        void Transform(Signature signature);
    }

    public interface IPipelineItem<IClassification>//where T : IPipelineItem
    {
        //void Run(object/Signature input);
        void Test(Signature signature);
    }*/

    public interface IPipelineIO
    {
        List<FeatureDescriptor> InputFeatures { get; set; }
        List<FeatureDescriptor> OutputFeatures { get; set; }
    }

    public interface ITransformation : ILogger, IProgress, IPipelineIO
    {
        void Transform(Signature signature);
    }

    public interface IClassification : ILogger, IPipelineIO
    {
        double Pair(Signature signature1, Signature signature2);
    }

    public static class ITransformationMethods
    {
        public static ITransformation Input(this ITransformation caller, params FeatureDescriptor[] inputFeatures)
        {
            caller.InputFeatures = inputFeatures.ToList();
            return caller;
        }
        public static ITransformation Output(this ITransformation caller, params FeatureDescriptor[] outputFeatures)
        {
            caller.OutputFeatures = outputFeatures.ToList();
            return caller;
        }
    }

    public static class IClassificationMethods
    {
        public static IClassification Input(this IClassification caller, params FeatureDescriptor[] inputFeatures)
        {
            caller.InputFeatures = inputFeatures.ToList();
            return caller;
        }
        public static IClassification Output(this IClassification caller, params FeatureDescriptor[] outputFeatures)
        {
            caller.OutputFeatures = outputFeatures.ToList();
            return caller;
        }
    }

}
