using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using SigStat.WpfSample.Common;
using SigStat.WpfSample.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Model
{
    public class MyVerifier: Verifier
    {
        public IClassifier Classifier { get; set; }

        public MyVerifier(IClassifier classifier)
        {
            Classifier = classifier;
        }

        public override void Train(List<Signature> signatures)
        {
            foreach (var sig in signatures)
            {
                Pipeline.Transform(sig);
            }
            Classifier.Train(signatures);
        }

        public override bool Test(Signature sig)
        {
            Pipeline.Transform(sig);

            return Classifier.Test(sig)>0.5;
        }

        protected override void LoggerChanged(Logger oldLogger, Logger newLogger)
        {
            if (Classifier!=null)
            {
                Classifier.Logger = newLogger;
            }


        }

    }
}
