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

        //public virtual Logger Logger { get; set; }

        public abstract double Test(Signature signature);

        public abstract double Train(List<Signature> signatures);

        public double CalculateTestResult(double value, double threshold)
        {
            //bool
            //if (value < threshold) return 0; else return 1;
            // linear
            return  Math.Max(1 - (value / threshold) / 2, 0);





        }
    }
}
