using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class VerifierBenchmarkTests
    {
        [TestMethod]
        public void TestSerializer()
        {
            var verifier = new VerifierBenchmark()
            {
                Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true),
                Verifier = new Verifier()
                {
                    Pipeline = new SequentialTransformPipeline
                    {
                        new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},

                        new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                        new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
                        new FillPenUpDurations()
                        {
                            InputFeatures = new List<FeatureDescriptor<List<double>>>(){ Features.X, Features.Y, Features.Pressure },
                            OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                            InterpolationType = typeof(CubicInterpolation),
                            TimeInputFeature =Features.T,
                            TimeOutputFeature = Features.T
                        }
                    }
                    ,
                    Classifier = new OptimalDtwClassifier()
                    {
                        Sampler = new FirstNSampler(10),
                        Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                    }
                },
                Sampler = new FirstNSampler(10),
                Logger = new SimpleConsoleLogger(),
            };
            var json = SerializationHelper.JsonSerialize(verifier);
            TestHelper.AssertJson(verifier, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedVerifier = new VerifierBenchmark()
            {
                Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true),
                Verifier = new Verifier()
                {
                    Pipeline = new SequentialTransformPipeline
                    {
                        new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},

                        new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                        new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
                        new FillPenUpDurations()
                        {
                            InputFeatures = new List<FeatureDescriptor<List<double>>>(){ Features.X, Features.Y, Features.Pressure },
                            OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                            InterpolationType = typeof(CubicInterpolation),
                            TimeInputFeature =Features.T,
                            TimeOutputFeature = Features.T
                        }
                    }
                    ,
                    Classifier = new OptimalDtwClassifier()
                    {
                        Sampler = new FirstNSampler(10),
                        Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                    }
                },
                Sampler = new FirstNSampler(10),
                Logger = new SimpleConsoleLogger(),
            };
            var verifierJson = SerializationHelper.JsonSerialize(expectedVerifier);
            var deserializedVerifier = SerializationHelper.Deserialize<VerifierBenchmark>(verifierJson);
            TestHelper.AssertJson(expectedVerifier, deserializedVerifier);
        }
    }
}
