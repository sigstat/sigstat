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
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState(false)),
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
            var jsonSerializerSettings = GetTestSettings();
            var feature = Features.Pressure;
            var json = JsonConvert.SerializeObject(feature, Formatting.Indented, jsonSerializerSettings);
            var expectedJson = @"""Pressure | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e""";
            JsonAssert.AreEqual(expectedJson, json);
            var shortJson = JsonConvert.SerializeObject(feature, Formatting.Indented, jsonSerializerSettings);
            var expectedShortJson = @"""Pressure""";
            JsonAssert.AreEqual(expectedShortJson, shortJson);
        }

        [TestMethod]
        public void TestRead()
        {
            var jsonSerializerSettings = GetTestSettings();
            var feature = Features.Pressure;
            var json = JsonConvert.SerializeObject(feature, Formatting.Indented, jsonSerializerSettings);
            var shortJson = JsonConvert.SerializeObject(feature, Formatting.Indented, jsonSerializerSettings);

            var featureDeserialized = JsonConvert.DeserializeObject<FeatureDescriptor<List<double>>>(json,GetTestSettings());
            JsonAssert.AreEqual(feature, featureDeserialized);
            var featureDeserializedShort =
                JsonConvert.DeserializeObject<FeatureDescriptor<List<double>>>(shortJson, GetTestSettings());
            JsonAssert.AreEqual(feature, featureDeserializedShort);
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
