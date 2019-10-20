using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SigStat.Common.Helpers.Serialization;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class DistanceFunctionJsonConverterTests
    {
        public static JsonSerializerSettings GetTestSettings()
        {
            return new JsonSerializerSettings

            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new VerifierResolver(),
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState()),
                Converters = new List<JsonConverter> { new DistanceFunctionJsonConverter() }
            };
        }
        [TestMethod]
        public void TestCanConvert()
        {
            var converter = new DistanceFunctionJsonConverter();
            Func<double[], double[], double> distanceFunc = Accord.Math.Distance.Euclidean;
            var convertible = converter.CanConvert(distanceFunc.GetType());
            Assert.IsTrue(convertible); 
            var otherObj = new object();
            var notConvertible = converter.CanConvert(otherObj.GetType());
            Assert.IsFalse(notConvertible);
        }

        [TestMethod]
        public void TestWrite()
        {
            Func<double[], double[], double> distanceFunc = Accord.Math.Distance.Cosine;
            var json = JsonConvert.SerializeObject(distanceFunc, Formatting.Indented, GetTestSettings());
            var expectedJson = $"\"{distanceFunc.Method.DeclaringType.AssemblyQualifiedName}|{distanceFunc.Method}\"";
            Assert.AreEqual(expectedJson,json);
        }

        [TestMethod]
        public void TestRead()
        {
            Func<double[], double[], double> distanceFunc = Accord.Math.Distance.Euclidean;
            var funcJson = $"\"{distanceFunc.Method.DeclaringType.AssemblyQualifiedName}|{distanceFunc.Method}\"";
            var funcDeserialized =
                JsonConvert.DeserializeObject<Func<double[], double[], double>>(funcJson, GetTestSettings());
            Assert.AreEqual(distanceFunc,funcDeserialized);
        }

        [TestMethod]
        public void TestReadWrongJson()
        {
            Func<double[], double[], double> distanceFunc = Accord.Math.Distance.Euclidean;
            var funcJson = $"{distanceFunc.Method.DeclaringType.AssemblyQualifiedName}|{distanceFunc.Method}\"";
            Assert.ThrowsException<JsonReaderException>(() =>
                JsonConvert.DeserializeObject<Dictionary<string, FeatureDescriptor>>(funcJson, GetTestSettings()));
        }
    }
}
