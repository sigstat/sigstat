using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common;
using SigStat.Common.Helpers;

namespace SigStat.WpfSample.Model
{
    public class CompositeTimeFilterClassifier : IClassifier
    {
        public IClassifier MainClassifier { get; set; }
        public TimeFilterClassifier TimeFilterClassifier { get; private set; }

        private Logger _logger;
        public Logger Logger {
            get => _logger;
            set
            {
                _logger = value;
                MainClassifier.Logger = _logger;
                TimeFilterClassifier.Logger = _logger;
            }
        }

        public CompositeTimeFilterClassifier(IClassifier mainClassifier)
        {
            MainClassifier = mainClassifier;
            TimeFilterClassifier = new TimeFilterClassifier();
        }

        //return még ha használom nem korrekt
        public double Train(List<Signature> signatures)
        {
            TimeFilterClassifier.Train(signatures);
            return MainClassifier.Train(signatures);
        }


        public bool Test(Signature signature)
        {
            if (!TimeFilterClassifier.Test(signature))
                return false;
            else
                return MainClassifier.Test(signature);
        }

   
    }
}
