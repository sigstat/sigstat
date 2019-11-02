using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SigStat.Common.Helpers.Serialization;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class RectangleFConverterTests
    {
        public static JsonSerializerSettings GetTestSettings()
        {
            return new JsonSerializerSettings

            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new VerifierResolver(),
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState(false)),
                Converters = new List<JsonConverter> { new RectangleFConverter() }
            };
        }

        [TestMethod]
        public void TestCanConvert()
        {
            var converter = new RectangleFConverter();
            var rectangleF = new RectangleF(1.0f, 1.0f, 1.0f, 1.0f);
            var convertible = converter.CanConvert(rectangleF.GetType());
            Assert.IsTrue(convertible);
            var otherObj = new object();
            var notConvertible = converter.CanConvert(otherObj.GetType());
            Assert.IsFalse(notConvertible);
        }

        [TestMethod]
        public void TestWrite()
        {
            var jsonSerializerSettings = GetTestSettings();
            var rectangleF = new RectangleF(1.0f, 1.0f, 1.0f, 1.0f);
            var json = JsonConvert.SerializeObject(rectangleF,Formatting.Indented,jsonSerializerSettings);
            JsonAssert.AreEqual(rectangleF, json,jsonSerializerSettings);
        }

        [TestMethod]
        public void TestRead()
        {
            var jsonSerializerSettings = GetTestSettings();
            var rectangleF = new RectangleF(1.0f, 1.0f, 1.0f, 1.0f);
            var json = JsonConvert.SerializeObject(rectangleF, jsonSerializerSettings);
            var rectangleFDeserialized = JsonConvert.DeserializeObject<RectangleF>(json, jsonSerializerSettings);
            JsonAssert.AreEqual(rectangleF, rectangleFDeserialized);
        }

        [TestMethod]
        public void TestReadWrongJson()
        {
            var json = @" ""X"": 1.0, ""Y"": 1.0,""Width"": 1.0,""Height""}";
                Assert.ThrowsException<InvalidCastException>(() =>
                    JsonConvert.DeserializeObject<RectangleF>(json, GetTestSettings()));
        }
    }
}
