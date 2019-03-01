using SigStat.Common;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Model
{
    public interface IClassifier//: ILogger
    {
        string Name { get; }
        double Train(List<Signature> signatures);

        double Test(Signature signature);
    }
}
