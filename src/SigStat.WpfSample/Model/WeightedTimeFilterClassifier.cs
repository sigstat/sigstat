using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common;
using SigStat.Common.Helpers;

namespace SigStat.WpfSample.Model
{
    public class WeightedTimeFilterClassifier : CompositeTimeFilterClassifier
    {
        public double MainClassifierWeight { get; private set; }

        public override string Name
        {
            get
            {
                if (MainClassifier is DTWClassifier)
                {
                    return "WeightedTimeFilterClassifier_" + ((DTWClassifier)MainClassifier).DtwType;
                }
                else
                    return base.Name;
            }
        }

        public WeightedTimeFilterClassifier(IClassifier mainClassifier, double mainClassifierWeight = 0.5) : base(mainClassifier)
        {
            MainClassifierWeight = mainClassifierWeight;
        }

        //TODO: return érték használatra kívül még nem alkalmas, értelmetlen
        public override double Train(List<Signature> signatures)
        {
            var timeClassifierWeight = 1 - MainClassifierWeight;
            var timeTreshold = TimeFilterClassifier.Train(signatures);
            var mainThreshold = MainClassifier.Train(signatures);
            return timeClassifierWeight * timeTreshold + MainClassifierWeight * mainThreshold;
        }

        public override double Test(Signature signature)
        {
            var timeDecision = TimeFilterClassifier.Test(signature);// ? 1 : 0;
            var mainDecision = MainClassifier.Test(signature);// ? 1: 0;

            var timeClassifierWeight = 1 - MainClassifierWeight;

            return timeClassifierWeight * timeDecision + MainClassifierWeight * mainDecision;

            //return ((MainClassifierWeight * mainDecision) + (timeClassifierWeight * timeDecision)) >= 0.5;


            //if (timeDecision && mainDecision) return true;
            //else if (!timeDecision && !mainDecision) return false;
            //else if (MainClassifierWeight > timeClassifierWeight) return mainDecision;
            //else if (MainClassifierWeight < timeClassifierWeight) return timeDecision;
            //else return !timeDecision ? timeDecision : mainDecision;
        }
    }
}
