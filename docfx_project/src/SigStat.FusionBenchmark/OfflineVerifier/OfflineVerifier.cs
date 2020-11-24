using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Helpers.Serialization;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.FusionBenchmark.OfflineVerifier
{
    public class OfflineVerifier : ILoggerObject
    {

        public Dictionary<string, FeatureDescriptor> AllFeatures
        {
            get
            {
                return FeatureDescriptor.GetAll();
            }
        }

        private SequentialTransformPipeline offlinePipeline;

        public SequentialTransformPipeline OfflinePipeline
        {
            get => offlinePipeline;
            set
            {
                offlinePipeline = value;
                if (offlinePipeline.Logger == null)
                {
                    offlinePipeline.Logger = this.Logger;
                }
            }
        }

        private SequentialTransformPipeline fusionPipeline;

        public SequentialTransformPipeline FusionPipeline
        {
            get => fusionPipeline;
            set
            {
                fusionPipeline = value;
                if (fusionPipeline.Logger == null)
                {
                    fusionPipeline.Logger = this.Logger;
                }
            }
        }

        public IClassifier Classifier { get; set; }

        public ISignerModel SignerModel { get; set; }

        public ILogger Logger { get; set; }

        [JsonConstructor]
        public OfflineVerifier(ILogger logger = null)
        {
            this.Logger = logger;
            this.LogTrace("Verifier created");
        }

        public OfflineVerifier()
        {
            this.Logger = null;
            this.LogTrace("Verifier created");
        }

        public OfflineVerifier(OfflineVerifier v)
        {
            this.Logger = v.Logger;
            this.offlinePipeline = v.OfflinePipeline;
            this.fusionPipeline = v.FusionPipeline;
            this.Classifier = v.Classifier;
        }

        public virtual void Train(List<Signature> signatures)
        {
            // TODO: centralize logger injection
            OfflinePipeline.Logger = this.Logger;
            if (Classifier is ILoggerObject)
                ((ILoggerObject)Classifier).Logger = this.Logger;

            if (signatures.Any(s => s.Origin != Origin.Genuine))
            {
                //this.Warn( $"Training with a non-genuine signature.");
            }

            foreach (var sig in signatures)
            {
                OfflinePipeline.Transform(sig);
            }
            this.LogTrace("Signatures transformed.");

            if (Classifier == null)
                this.LogError("No Classifier attached to the Verifier");
            else
                SignerModel = Classifier.Train(signatures);

            this.LogInformation("Training finished.");

        }

        public virtual double Test(Signature signature)
        {
            this.LogInformation("Verifying signature {signature}.", signature.ID);

            FusionPipeline.Transform(signature);

            this.LogInformation("Signature {signature} transformed.", signature.ID);

            if (SignerModel == null)
                this.LogError("Signer model not available. Train or provide a model for verification.");

            var result = Classifier.Test(SignerModel, signature);

            this.LogInformation("Verification result for signature '{signature}': {result}", signature.ID, result);
            return result;
        }
    }
}
