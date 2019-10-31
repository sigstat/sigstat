using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class VerifierTests
    {
        [TestMethod]
        public void TestSerializer()
        {
            var verifier = new Verifier()
            {
                Pipeline = new SequentialTransformPipeline
                {
                    new NormalizeRotation()
                    {
                        InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X,
                        OutputY = Features.Y
                    },
                    new Scale() {InputFeature = Features.X, OutputFeature = Features.X},

                },
                Classifier = new OptimalDtwClassifier()
                {
                    Sampler = new FirstNSampler(10),
                    Features = new List<FeatureDescriptor>() {Features.X, Features.Y, Features.Pressure}
                }
            };
            var json = SerializationHelper.JsonSerialize(verifier);
            JsonAssert.AreEqual(verifier, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedVerifier = new Verifier()
            {
                Pipeline = new SequentialTransformPipeline
                {
                    new NormalizeRotation()
                    {
                        InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X,
                        OutputY = Features.Y
                    },
                    new Scale() {InputFeature = Features.X, OutputFeature = Features.X},

                },
                Classifier = new OptimalDtwClassifier()
                {
                    Sampler = new FirstNSampler(10),
                    Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                }
            };
            var verifierJson = SerializationHelper.JsonSerialize(expectedVerifier);
            var deserializedVerifier = SerializationHelper.Deserialize<Verifier>(verifierJson);
            JsonAssert.AreEqual(expectedVerifier, deserializedVerifier);
        }
    }
}

