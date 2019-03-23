using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;
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
    [JsonObject(MemberSerialization.OptIn)]
    public class Verifier : ILoggerObject
    {
        //private readonly EventId VerifierEvent = new EventId(8900, "Verifier");

        private SequentialTransformPipeline pipeline;
        /// <summary> Gets or sets the transform pipeline. Hands over the Logger object. </summary>
        [JsonProperty]
        public SequentialTransformPipeline Pipeline { get => pipeline;
            set
            {
                pipeline = value;
                if (pipeline.Logger == null)
                {
                    pipeline.Logger = this.Logger;
                }
            }
        }

        /// <summary>  Gets or sets the classifier pipeline. Hands over the Logger object. </summary>
        [JsonProperty]
        public IClassifier Classifier { get; set; }
        [JsonProperty]
        public ISignerModel SignerModel { get; set; }

        /// <summary> Gets or sets the class responsible for logging</summary>
        [JsonProperty]
        public ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verifier"/> class
        /// </summary>
        /// <param name="logger">Initializes the Logger property of the <see cref="Verifier"/></param>
        [JsonConstructor]
        public Verifier(ILogger logger = null)
        {
            this.Logger = logger;
            this.LogTrace("Verifier created");
        }

        public Verifier(Verifier v)
        {
            this.Logger = v.Logger;
            this.Pipeline = v.Pipeline;
            this.Classifier = v.Classifier;
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
            // TODO: centralize logger injection
            Pipeline.Logger = this.Logger;
            if (Classifier is ILoggerObject)
                ((ILoggerObject)Classifier).Logger = this.Logger;

            if (signatures.Any(s => s.Origin != Origin.Genuine))
            {
                //this.Warn( $"Training with a non-genuine signature.");
            }

            foreach (var sig in signatures)
            {
                Pipeline.Transform(sig);
            }
            this.LogTrace("Signatures transformed.");

            if (Classifier == null)
                this.LogError("No Classifier attached to the Verifier");
            else
                SignerModel = Classifier.Train(signatures);

            this.LogInformation("Training finished.");

        }

        /// <summary>
        /// Verifies the genuinity of <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>True if <paramref name="signature"/> passes the verification test.</returns>
        public virtual double Test(Signature signature)
        {
            this.LogInformation("Verifying signature {signature}.", signature.ID);

            Pipeline.Transform(signature);

            this.LogInformation("Signature {signature} transformed.", signature.ID);

            if (SignerModel == null)
                this.LogError("Signer model not available. Train or provide a model for verification.");

            var result = Classifier.Test(SignerModel, signature);

            this.LogInformation("Verification result for signature '{signature}': {result}", signature.ID, result);
            return result;
        }
    }
}
