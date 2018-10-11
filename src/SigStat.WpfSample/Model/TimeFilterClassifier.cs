using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common;
using SigStat.Common.Helpers;

namespace SigStat.WpfSample.Model
{
    public class TimeFilterClassifier : IClassifier
    {
        public Logger Logger { get; set; }

        private List<Signature> originals;
        private double threshold;

        public double Train(List<Signature> signatures)
        {
            if (signatures == null)
                throw new ArgumentNullException(nameof(signatures));
            if (signatures.Count == 0)
                throw new ArgumentException("'signatures' contains no elements", nameof(signatures));

            originals = signatures;

            List<object[]> debugInfo = new List<object[]>();

            double maxTime = 0;
            double minTime = Double.MaxValue;
            foreach (var sig in signatures)
            {
                var sigTime = sig.GetFeature(Features.T).Max() - sig.GetFeature(Features.T).Min();

                debugInfo.Add(new object[] { sig.ID, sigTime });

                if (sigTime > maxTime)
                    maxTime = sigTime;
                if (sigTime < minTime)
                    minTime = sigTime;
            }

            Logger.Info(this, signatures[0].Signer.ID + "-maxTime", debugInfo);


            threshold = maxTime + 2.5 * (maxTime - minTime);

            return maxTime;
        }

        public bool Test(Signature signature)
        {
            var debugInfo = (List<object[]>)Logger.ObjectEntries[signature.Signer.ID + "-maxTime"];

            var sigTime = signature.GetFeature(Features.T).Max() - signature.GetFeature(Features.T).Min();

            debugInfo.Add(new object[] { signature.ID, sigTime });

            return sigTime <= threshold;
        }


    }
}
