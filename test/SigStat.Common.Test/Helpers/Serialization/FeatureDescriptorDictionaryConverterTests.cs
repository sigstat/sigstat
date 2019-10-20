using System.Collections.Generic;
using System.Runtime.Serialization;
using SigStat.Common.Helpers.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class FeatureDescriptorDictionaryConverterTests
    {
        public static JsonSerializerSettings GetTestSettings()
        {
            return new JsonSerializerSettings

            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new VerifierResolver(),
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState()),
                Converters = new List<JsonConverter> { new FeatureDescriptorDictionaryConverter() }
            };
        }

        [TestMethod]
        public void TestCanConvert()
        {
            var converter = new FeatureDescriptorDictionaryConverter();
            var featureDescriptor = new Dictionary<string, FeatureDescriptor>();
            var convertible = converter.CanConvert(featureDescriptor.GetType());
            Assert.IsTrue(convertible);
            var otherObj = new object();
            var notConvertible = converter.CanConvert(otherObj.GetType());
            Assert.IsFalse(notConvertible);
        }

        [TestMethod]
        public void TestWrite()
        {
            var features = new Dictionary<string, FeatureDescriptor>();
            features.Add("Pressure", Features.Pressure);
            features.Add("Altitude", Features.Altitude);
            var json = JsonConvert.SerializeObject(features, Formatting.Indented, GetTestSettings());
            var expectedJson = $"[\r\n  \"{Features.Pressure.Key} | {Features.Pressure.FeatureType.AssemblyQualifiedName}\",\r\n  \"{Features.Altitude.Key} | {Features.Altitude.FeatureType.AssemblyQualifiedName}\"\r\n]";
            Assert.AreEqual(expectedJson, json);
        }


        [TestMethod]
        public void TestRead()
        {
            var featuresJson = $"[\r\n  \"{Features.Pressure.Key} | {Features.Pressure.FeatureType.AssemblyQualifiedName}\",\r\n  \"{Features.Altitude.Key} | {Features.Altitude.FeatureType.AssemblyQualifiedName}\"\r\n]";
            var featuresDeserialized = JsonConvert.DeserializeObject<Dictionary<string,FeatureDescriptor>>(featuresJson, GetTestSettings());
            var features = new Dictionary<string, FeatureDescriptor>();
            features.Add("Pressure", Features.Pressure);
            features.Add("Altitude", Features.Altitude);
            Assert.AreEqual(featuresDeserialized.Count, features.Count);
            Assert.AreEqual(featuresDeserialized["Pressure"], features["Pressure"]);
            Assert.AreEqual(featuresDeserialized["Altitude"], features["Altitude"]);
        }

        [TestMethod]
        public void TestReadWrongJson()
        {
            var json = $"\r\n  \"{Features.Pressure.Key} | {Features.Pressure.FeatureType.AssemblyQualifiedName}\",\r\n  \"{Features.Altitude.Key} | {Features.Altitude.FeatureType.AssemblyQualifiedName}\"\r\n]";
            Assert.ThrowsException<JsonReaderException>(() =>
                JsonConvert.DeserializeObject<Dictionary<string,FeatureDescriptor>>(json, GetTestSettings()));
        }
    }
}
