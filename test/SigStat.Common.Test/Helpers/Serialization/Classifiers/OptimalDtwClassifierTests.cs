using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;
using SigStat.Common.PipelineItems.Classifiers;

namespace SigStat.Common.Test.Helpers.Serialization.Classifiers
{ 
    [TestClass]
    public class OptimalDtwClassifierTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var optimalDtwClassifier = new OptimalDtwClassifier()
            {
                Sampler = new FirstNSampler(10),
                Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
            };
            var json = SerializationHelper.JsonSerialize(optimalDtwClassifier, true);
            var expectedJson = @" {
            ""Features"":[""X"",""Y"",""Pressure""],
            ""Sampler"": {
              ""$type"": ""SigStat.Common.Framework.Samplers.FirstNSampler, SigStat.Common"",
              ""N"": 10
            },
            ""DistanceFunction"": ""Accord.Math.Distance.Euclidean, Accord.Math"",
            ""WarpingWindowLength"": 0
            }";
        JsonAssert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var optimalDtwClassifier = new OptimalDtwClassifier()
            {
                Sampler = new FirstNSampler(10),
                Features = new List<FeatureDescriptor>() {Features.X, Features.Y, Features.Pressure}
            };
            var dtwJson = SerializationHelper.JsonSerialize(optimalDtwClassifier);
            var deserializedDtw = SerializationHelper.Deserialize<OptimalDtwClassifier>(dtwJson);
            JsonAssert.AreEqual(optimalDtwClassifier, deserializedDtw);
        }
    }
}
