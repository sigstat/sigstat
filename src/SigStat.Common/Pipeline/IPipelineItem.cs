using SigStat.Common;
using System;
using System.Collections.Generic;
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

    public interface ITransformation// : IPipelineItem
    {
        void Transform(Signature signature);
    }

    public interface IClassification// : IPipelineItem
    {
        double Pair(Signature signature1, Signature signature2);

        //void Train(IEnumerable<Signature> signatures);
        //double Test(Signature signature);
    }
}
