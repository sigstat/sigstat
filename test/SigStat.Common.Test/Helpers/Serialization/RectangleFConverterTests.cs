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
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState()),
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
            var rectangleF = new RectangleF(1.0f, 1.0f, 1.0f, 1.0f);
            var json = JsonConvert.SerializeObject(rectangleF,Formatting.Indented,GetTestSettings());
            var expectedJson =
                "{\r\n  \"X\": 1.0,\r\n  \"Y\": 1.0,\r\n  \"Width\": 1.0,\r\n  \"Height\": 1.0\r\n}";
            Assert.AreEqual(expectedJson,json);
        }

        [TestMethod]
        public void TestRead()
        {
            var json = "{\r\n  \"X\": 1.0,\r\n  \"Y\": 1.0,\r\n  \"Width\": 1.0,\r\n  \"Height\": 1.0\r\n}";
            var rectangleFDeserialized = JsonConvert.DeserializeObject<RectangleF>(json, GetTestSettings());
            var rectangleF = new RectangleF(1.0f, 1.0f, 1.0f, 1.0f);
            Assert.AreEqual(rectangleF, rectangleFDeserialized);
        }

        [TestMethod]
        public void TestReadWrongJson()
        {
            var json = "\r\n  \"X\": 1.0,\r\n  \"Y\": 1.0,\r\n  \"Width\": 1.0,\r\n  \"Height\": 1.0\r\n}";
                Assert.ThrowsException<InvalidCastException>(() =>
                    JsonConvert.DeserializeObject<RectangleF>(json, GetTestSettings()));
        }
    }
}
