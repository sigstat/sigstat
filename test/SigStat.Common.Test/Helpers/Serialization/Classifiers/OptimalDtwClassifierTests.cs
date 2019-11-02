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
            var optimalDtwClassifier = new OptimalDtwClassifier();
            var json = SerializationHelper.JsonSerialize(optimalDtwClassifier);
            JsonAssert.AreEqual(optimalDtwClassifier, json);
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
