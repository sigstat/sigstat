using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SigStat.Common.Helpers;
using SigStat.Common.Helpers.Serialization;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SigStat.Common.Model
{
    /// <summary>
    /// Uses pipelines to transform, train on, and classify <see cref="Signature"/> objects.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Verifier : ILoggerObject
    {

        /// <summary>
        /// This property is used by the Serializer to access a list of all FeatureDescriptors
        /// </summary>
        public Dictionary<string, FeatureDescriptor> AllFeatures
        {
            get
            {
                //TODO: We should only return the Descriptors that are actually used in the Verifier
                return FeatureDescriptor.GetAll();
            }
            set
            {

            }
        }
        //private readonly EventId VerifierEvent = new EventId(8900, "Verifier");

        private SequentialTransformPipeline pipeline = new SequentialTransformPipeline();
        /// <summary> Gets or sets the transform pipeline. Hands over the Logger object. </summary>
        
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
        
        public IClassifier Classifier { get; set; }

        /// <summary>Gets or sets the signer model.</summary>
        /// <value>The signer model.</value>
        public ISignerModel SignerModel { get; set; }
       
        /// <summary> Gets or sets the class responsible for logging</summary>
        
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Verifier"/> class.
        /// </summary>
        public Verifier(){
            this.Logger = null;
            this.LogTrace("Verifier created");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verifier"/> class based on another Verifier instance
        /// </summary>
        /// <param name="baseVerifier">The reference verifier</param>
        public Verifier(Verifier baseVerifier)
        {
            this.Logger = baseVerifier.Logger;
            this.Pipeline = baseVerifier.Pipeline;
            this.Classifier = baseVerifier.Classifier;
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

            this.LogTrace("Training finished.");

        }

        /// <summary>
        /// Verifies the genuinity of <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>True if <paramref name="signature"/> passes the verification test.</returns>
        public virtual double Test(Signature signature)
        {

            this.LogTrace("Verifying signature {signature}.", signature.ID);

            // HACK: OptimalDtwClassifier performs the transformations in the training phase
            if (!(Classifier is OptimalDtwClassifier))
                Pipeline.Transform(signature);

            this.LogTrace("Signature {signature} transformed.", signature.ID);

            if (SignerModel == null)
                this.LogError("Signer model not available. Train or provide a model for verification.");

            var result = Classifier.Test(SignerModel, signature);

            this.LogTrace("Verification result for signature '{signature}': {result}", signature.ID, result);
            return result;
        }
    }
}
