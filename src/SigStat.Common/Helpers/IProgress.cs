using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers
{
    public interface IProgress
    {
        event EventHandler<int> ProgressChanged;
        int Progress { get; /*private set;*/ }
    }
}
