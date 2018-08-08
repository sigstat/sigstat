using SigStat.Common;
using SigStat.Common.Helpers;
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

    /// <summary>
    /// TODO: C# 8.0 ban ezt atalakitani default implementacios interface be
    /// </summary>
    public abstract class PipelineBase : ILogger, IProgress
    {

        public Logger Logger { get; set; }

        private int _progress = 0;
        public int Progress { get => _progress; set { _progress = value; ProgressChanged(this, value); } }

        public event EventHandler<int> ProgressChanged = delegate { };

        protected void Log(LogLevel level, string message)
        {
            if (Logger != null)
                Logger.AddEntry(level, this, message);
        }
    }

    public interface ITransformation : ILogger, IProgress
    {
        void Transform(Signature signature);
    }

    public interface IClassification : ILogger
    {
        double Pair(Signature signature1, Signature signature2);

        //void Train(IEnumerable<Signature> signatures);
        //double Test(Signature signature);
    }
}
