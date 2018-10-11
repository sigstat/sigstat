using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common;
using SigStat.Common.Helpers;

namespace SigStat.WpfSample.Model
{
    public class CompositeTimeFilterClassifier : BaseClassifier
    {
        public IClassifier MainClassifier { get; set; }
        public TimeFilterClassifier TimeFilterClassifier { get; private set; }

        public override string Name
        {
            get
            {
                if (MainClassifier is DTWClassifier)
                {
                    return "CompositeTimeFilterClassifier_" + ((DTWClassifier)MainClassifier).DtwType;
                }
                else
                    return base.Name;
            }
        }

        public override Logger Logger {
            set
            {
                base.Logger = value;
                MainClassifier.Logger = value;
                TimeFilterClassifier.Logger = value;
            }
        }

        public CompositeTimeFilterClassifier(IClassifier mainClassifier)
        {
            MainClassifier = mainClassifier;
            TimeFilterClassifier = new TimeFilterClassifier();
        }

        //return még ha használom nem korrekt
        public override double Train(List<Signature> signatures)
        {
            TimeFilterClassifier.Train(signatures);
            return MainClassifier.Train(signatures);
        }


        public override bool Test(Signature signature)
        {
            if (!TimeFilterClassifier.Test(signature))
                return false;
            else
                return MainClassifier.Test(signature);
        }

   
    }
}
