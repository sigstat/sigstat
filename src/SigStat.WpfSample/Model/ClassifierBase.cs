using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common;
using SigStat.Common.Helpers;

namespace SigStat.WpfSample.Model
{
    public abstract class BaseClassifier : IClassifier
    {
        public virtual string Name => GetType().Name;

        public virtual Logger Logger { get; set; }

        public abstract bool Test(Signature signature);

        public abstract double Train(List<Signature> signatures);
    }
}
