using System;
using System.Collections.Generic;
using System.Linq;
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
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState(false)),
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
            Func<int[], double[], double> otherObj = (i, d) => 0;
            var notConvertible = converter.CanConvert(otherObj.GetType());
            Assert.IsFalse(notConvertible);
        }

        [TestMethod]
        public void TestWrite()
        {
            var jsonSerializerSettings = GetTestSettings();
            Func<double[], double[], double> distanceFunc = Accord.Math.Distance.Cosine;
            var json = JsonConvert.SerializeObject(distanceFunc, Formatting.Indented, jsonSerializerSettings);
            var expectedJson = "\"Accord.Math.Distance.Cosine, Accord.Math\"";
            Assert.AreEqual(expectedJson,json);
        }

        [TestMethod]
        public void TestRead()
        {
            var jsonSerializerSettings = GetTestSettings();
            Func<double[], double[], double> distanceFunc = Accord.Math.Distance.Cosine;
            var funcJson = JsonConvert.SerializeObject(distanceFunc, Formatting.Indented, jsonSerializerSettings);
            var funcDeserialized = JsonConvert.DeserializeObject<Func<double[], double[], double>>(funcJson, jsonSerializerSettings);
            JsonAssert.AreEqual(distanceFunc, funcDeserialized);
        }

        [TestMethod]
        public void TestReadWrongJson()
        {
            Func<double[], double[], double> distanceFunc = Accord.Math.Distance.Euclidean;
            var enumerable = distanceFunc.Method.GetParameters().Select(x => x.ParameterType.FullName + ";");
            var concated = string.Concat(enumerable).TrimEnd(';');
            var funcJson = $"{distanceFunc.Method.DeclaringType?.AssemblyQualifiedName}|{distanceFunc.Method.Name}|{concated}\"";
            Assert.ThrowsException<JsonReaderException>(() =>
                JsonConvert.DeserializeObject<Dictionary<string, FeatureDescriptor>>(funcJson, GetTestSettings()));
        }
    }
}
