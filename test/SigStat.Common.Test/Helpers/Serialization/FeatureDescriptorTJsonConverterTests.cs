using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SigStat.Common.Helpers;
using SigStat.Common.Helpers.Serialization;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class FeatureDescriptorTJsonConverterTests
    {
        public static JsonSerializerSettings GetTestSettings()
        {
            return new JsonSerializerSettings

            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new VerifierResolver(),
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState()),
                Converters = new List<JsonConverter> { new FeatureDescriptorTJsonConverter() }
            };
        }

        [TestMethod]
        public void TestCanConvert()
        {
            var converter = new FeatureDescriptorTJsonConverter();
            var featureDescriptor = Features.Pressure;
            var convertible = converter.CanConvert(featureDescriptor.GetType());
            Assert.IsTrue(convertible);
            var otherObj = new object();
            var notConvertible = converter.CanConvert(otherObj.GetType());
            Assert.IsFalse(notConvertible);
        }

        [TestMethod]
        public void TestWrite()
        {
            var setting = GetTestSettings();
            var feature = Features.Pressure;
            var json = JsonConvert.SerializeObject(feature, Formatting.Indented, setting);
            var expectedJson = $"\"{Features.Pressure.Key} | {Features.Pressure.FeatureType.AssemblyQualifiedName}\"";
            Assert.AreEqual(expectedJson,json);
            var shortJson = JsonConvert.SerializeObject(feature, Formatting.Indented, setting);
            var expectedShortJson = $"\"{Features.Pressure.Key}\"";
            Assert.AreEqual(expectedShortJson,shortJson);
        }

        [TestMethod]
        public void TestRead()
        {
            var feature = $"\"{Features.Pressure.Key} | {Features.Pressure.FeatureType.AssemblyQualifiedName}\"";
            var featureDeserialized = JsonConvert.DeserializeObject<FeatureDescriptor<List<double>>>(feature,GetTestSettings());
            Assert.AreSame(featureDeserialized,Features.Pressure);
            var shortFeature = $"\"{Features.Pressure.Key}\"";
            var featureDeserializedShort =
                JsonConvert.DeserializeObject<FeatureDescriptor<List<double>>>(shortFeature, GetTestSettings());
            Assert.AreSame(featureDeserializedShort,Features.Pressure);
        }
        [TestMethod]
        public void TestReadWrongJson()
        {
            var json = $"{Features.Pressure.Key} | {Features.Pressure.FeatureType.AssemblyQualifiedName}\"";
            Assert.ThrowsException<JsonReaderException>(() =>
                JsonConvert.DeserializeObject<FeatureDescriptor<List<double>>>(json, GetTestSettings()));
        }
    }
}
