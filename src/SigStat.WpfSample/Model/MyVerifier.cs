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
            // TODO: előbb minden végén a feature extraction
            //var SVC2004FeatureExtraction;
            //bool isFeatureExtractionIncluded = false;
            //if (TransformPipeline.GetType() == typeof(SequentialTransformPipeline))
            //{
            //    var seqTranformPipeline = TransformPipeline as SequentialTransformPipeline;
            //    var i = seqTranformPipeline.Items.FindIndex(t => t.GetType() == typeof(DerivedSvc2004FeatureExtractor));
            //    var transform = seqTranformPipeline.Items.ElementAt(i);
            //    seqTranformPipeline.Items.RemoveAt(i);
            //}
            foreach (var sig in signatures)
            {
                Pipeline.Transform(sig);
            }
            Classifier.Train(signatures);
        }

        public override double Test(Signature sig)
        {
            Pipeline.Transform(sig);

            return Classifier.Test(sig);
        }



    }
}
