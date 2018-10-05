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
            if (signatures.Count == 0)
                throw new ArgumentException("'signatures' contains no elements", nameof(signatures));

            originals = signatures;

            object[,] debugInfo = new object[originals.Count + 1, 2];
            for (int i = 0; i < originals.Count; i++)
            {
                debugInfo[i + 1, 0] = originals[i].ID;
            }

            double maxTime = 0;
            foreach (var sig in signatures)
            {
                var sigTime = sig.GetFeature(Features.T).Max() - sig.GetFeature(Features.T).Min();

                debugInfo[signatures.IndexOf(sig)+1, 1] = sigTime;

                if (sigTime > maxTime)
                    maxTime = sigTime;
            }

            Logger.Info(this, signatures[0].Signer.ID + "-maxTime", debugInfo);

            threshold = maxTime;
            return maxTime;
        }

        public bool Test(Signature signature)
        {
            return (signature.GetFeature(Features.T).Max() - signature.GetFeature(Features.T).Min()) <= threshold;
        }

       
    }
}
