using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Model
{
    /// <summary>
    /// Uses pipelines to transform, train on, and classify <see cref="Signature"/> objects.
    /// </summary>
    public class Verifier
    {
        private SequentialTransformPipeline pipeline;
        /// <summary> Gets or sets the transform pipeline. Hands over the Logger object. </summary>
        public SequentialTransformPipeline Pipeline { get => pipeline; set { pipeline = value; pipeline.Logger = Logger; } }

        private IClassifier classifier;
        /// <summary>  Gets or sets the classifier pipeline. Hands over the Logger object. </summary>
        public IClassifier Classifier { get => classifier; set { classifier = value; classifier.Logger = Logger; } }

        private ISignerModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="Verifier"/> class, with empty pipelines and no <see cref="SigStat.Common.Helpers.Logger"/>.
        /// </summary>
        public Verifier()
        {
            Pipeline = new SequentialTransformPipeline();
            Classifier = new DTWClassifier();
        }

        /// <summary>
        /// Trains the verifier with a list of signatures. Uses the <see cref="Pipeline"/> to extract features,
        /// and <see cref="Classifier"/> to find an optimized limit.
        /// </summary>
        /// <param name="signatures">The list of signatures to train on.</param>
        /// <remarks>Note that <paramref name="signatures"/> may contain both genuine and forged signatures.
        /// It's up to the classifier, whether it takes advantage of both classes</remarks>
        public virtual void Train(List<Signature> signatures)
        {
            if (signatures.Any(s => s.Origin != Origin.Genuine))
            {
                //Log(LogLevel.Warn, $"Training with a non-genuine signature. ID: {sig.ID}");
            }

            foreach (var sig in signatures)
            {
                Pipeline.Transform(sig);
            }
            //Log(LogLevel.Debug, "Signatures transformed.");

            model = Classifier.Train(signatures);

            //Log(LogLevel.Debug, $"Limit approximation: {limit}");
            //Log(LogLevel.Info, "Training finished.");

        }

        /// <summary>
        /// Verifies the genuinity of <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>True if <paramref name="signature"/> passes the verification test.</returns>
        public virtual double Test(Signature signature)
        {
            //Log(LogLevel.Info, $"Verification SignatureID {sig.ID} in progress.");

            Pipeline.Transform(signature);
            //Log(LogLevel.Debug, "Signature transformed. Classifying..");

            return model.Test(signature);
            //double[] vals = new double[genuines.Count];
            //for (int i = 0; i < genuines.Count; i++)
            //{
            //    vals[i] = Classifier.Pair(sig, genuines[i]);
            //    Progress = (int)(i / (double)(genuines.Count - 1) * 100.0);
            //}
            //double avg = vals.Average();
            //Log(LogLevel.Debug, $"Verification SignatureID {sig.ID} result: { (avg < limit ? Origin.Genuine : Origin.Forged) }");
            //Log(LogLevel.Info, $"Verification SignatureID {sig.ID}  finished.");
            //return avg < limit;
        }
    }
}
