using SigStat.Common;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Model
{
    public interface IClassifier: ILogger
    {
        double Train(List<Signature> signatures);

        bool Test(Signature signature);
    }
}
