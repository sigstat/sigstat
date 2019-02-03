using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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
        private readonly EventId VerifierEvent = new EventId(8900, "Verifier");

        private SequentialTransformPipeline pipeline;
        /// <summary> Gets or sets the transform pipeline. Hands over the Logger object. </summary>
        public SequentialTransformPipeline Pipeline { get => pipeline; set { pipeline = value;  } }

        private IClassifier classifier;
        /// <summary>  Gets or sets the classifier pipeline. Hands over the Logger object. </summary>
        public IClassifier Classifier { get => classifier; set { classifier = value; } }

        /// <summary> Gets or sets the class responsible for logging</summary>
        public ILogger Logger { get; set; }

        private ISignerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="Verifier"/> class
        /// </summary>
        /// <param name="logger">Initializes the Logger property of the <see cref="Verifier"/></param>
        public Verifier(ILogger logger = null)
        {
            this.Logger = logger;
            Logger.LogTrace(VerifierEvent, "Verifier created");
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
                //Logger.LogWarning( $"Training with a non-genuine signature. ID: {sig.ID}");
            }

            foreach (var sig in signatures)
            {
                Pipeline.Transform(sig);
            }
            //Logger.LogTrace( "Signatures transformed.");

            model = Classifier.Train(signatures);

            //Logger.LogTrace( $"Limit approximation: {limit}");
            //Logger.LogInformation( "Training finished.");

        }

        /// <summary>
        /// Verifies the genuinity of <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>True if <paramref name="signature"/> passes the verification test.</returns>
        public virtual double Test(Signature signature)
        {
            Logger.LogInformation(VerifierEvent, "Verifying 'signature {signature}'.", signature.ID);

            Pipeline.Transform(signature);
            Logger.LogInformation(VerifierEvent, "'Signature {signature}' transformed.", signature.ID);

            var result = classifier.Test(model,signature);
            //double[] vals = new double[genuines.Count];
            //for (int i = 0; i < genuines.Count; i++)
            //{
            //    vals[i] = Classifier.Pair(sig, genuines[i]);
            //    Progress = (int)(i / (double)(genuines.Count - 1) * 100.0);
            //}
            //double avg = vals.Average();
            Logger.LogInformation(VerifierEvent, "Verification result for signature '{signature}': {result}", signature.ID, result);
            //Logger.LogInformation( $"Verification SignatureID {sig.ID}  finished.");
            //return avg < limit;
            return result;
        }
    }
}
