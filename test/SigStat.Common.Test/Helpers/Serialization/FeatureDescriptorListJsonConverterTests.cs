using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SigStat.Common.Helpers.Serialization;
using SigStat.Common.Pipeline;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class FeatureDescriptorListJsonConverterTests
    {
        public static JsonSerializerSettings GetTestSettings()
        {
            return new JsonSerializerSettings

            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new VerifierResolver(),
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState()),
                Converters = new List<JsonConverter> { new FeatureDescriptorListJsonConverter() }
            };
        }
        [TestMethod]
        public void TestCanConvert()
        {
            var converter = new FeatureDescriptorListJsonConverter();
            var featureList = new List<FeatureDescriptor>
            {
                Features.X,
                Features.Y
            };
            var convertible = converter.CanConvert(featureList.GetType());
            Assert.IsTrue(convertible);
            var otherObj = new object();
            var notConvertible = converter.CanConvert(otherObj.GetType());
            Assert.IsFalse(notConvertible);
        }

        [TestMethod]
        public void TestWrite()
        {
            var jsonSerializerSettings = GetTestSettings();
            var cleanSerializerSettings = new JsonSerializerSettings

            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new VerifierResolver(),
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState()),
                Converters = new List<JsonConverter> { new FeatureDescriptorListJsonConverter() }
            };
            var featureList = new List<FeatureDescriptor>
            {
                Features.X,
                Features.Y
            };
            var json = JsonConvert.SerializeObject(featureList, Formatting.Indented, jsonSerializerSettings);
            TestHelper.AssertJson(featureList, json, cleanSerializerSettings);
            var shortJson = JsonConvert.SerializeObject(featureList, Formatting.Indented, jsonSerializerSettings);
            TestHelper.AssertJson(featureList, shortJson, jsonSerializerSettings);
        }

        [TestMethod]
        public void TestRead()
        {
            var jsonSerializerSettings = GetTestSettings();
            var featureList = new List<FeatureDescriptor>
            {
                Features.X,
                Features.Y
            };
            var json = JsonConvert.SerializeObject(featureList, Formatting.Indented, jsonSerializerSettings);
            var shortJson = JsonConvert.SerializeObject(featureList, Formatting.Indented, jsonSerializerSettings);
            var featureListDeserialized =
                JsonConvert.DeserializeObject<List<FeatureDescriptor>>(json, GetTestSettings());
            TestHelper.AssertJson(featureList, featureListDeserialized, jsonSerializerSettings);
            var shortFeatureListDeserialized =
                JsonConvert.DeserializeObject<List<FeatureDescriptor>>(shortJson, GetTestSettings());
            TestHelper.AssertJson(featureList,shortFeatureListDeserialized, jsonSerializerSettings);
        }

        [TestMethod]
        public void TestReadWrongJson()
        {
            var json = $"\"{Features.X.Key} | {Features.X.FeatureType.AssemblyQualifiedName}\",\"{Features.Y.Key} | {Features.Y.FeatureType.AssemblyQualifiedName}\"]";
            Assert.ThrowsException<JsonSerializationException>(() =>
                JsonConvert.DeserializeObject<Dictionary<string, FeatureDescriptor>>(json, GetTestSettings()));
        }
    }
}
