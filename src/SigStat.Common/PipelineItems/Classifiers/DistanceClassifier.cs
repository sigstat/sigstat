using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    public class DistanceClassifier : IClassifier
    {
        Func<Signature, Signature, double> Distance { get; set; }

        public ISignerModel Train(List<Signature> signatures)
        {
            double[,]

        }
    }

    public class SignerModel
    {

    }
}
