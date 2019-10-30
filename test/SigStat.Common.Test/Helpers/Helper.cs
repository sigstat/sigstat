using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test
{
    static class TestHelper
    {
        public static JsonSerializer SigStatSerializer { get; set; }
        public static Signature BuildSignature(string signatureId = "Demo", Origin origin = Origin.Genuine, string signerID = "S01")
        {
            Signature signature = new Signature();
            signature.ID = signatureId;
            signature.Origin = origin;
            signature.Signer = new Signer()
            {
                ID = signerID
            };
            return signature;
            //Signature.SetFeature(Features.X, new List<double> { 1, 2, 3 });
        }

        static TestHelper()
        {
            SigStatSerializer = JsonSerializer.Create(SerializationHelper.GetSettings());
        }
        public static void AssertJson(object original, string json)
        {
            var expectedJsonToken = JToken.FromObject(original, JsonSerializer.Create(SerializationHelper.GetSettings()));
            var realJsonToken = JToken.Parse(json);
            Assert.IsTrue(JToken.EqualityComparer.Equals(expectedJsonToken, realJsonToken));
        }

        public static void AssertJson(object original, object deserialized)
        {
            var originalJsonToken = JToken.FromObject(original, JsonSerializer.Create(SerializationHelper.GetSettings()));
            var deserializedJsonToken = JToken.FromObject(deserialized, JsonSerializer.Create(SerializationHelper.GetSettings()));
            Assert.IsTrue(JToken.EqualityComparer.Equals(originalJsonToken,deserializedJsonToken));
        }

        public static void AssertJson(object original, string json, JsonSerializerSettings jsonSerializerSettings)
        {
            var expectedJsonToken = JToken.FromObject(original, JsonSerializer.Create(jsonSerializerSettings));
            var realJsonToken = JToken.Parse(json);
            Assert.IsTrue(JToken.EqualityComparer.Equals(expectedJsonToken, realJsonToken));
        }
        public static void AssertJson(object original, object deserialized, JsonSerializerSettings jsonSerializerSettings)
        {
            var originalJsonToken = JToken.FromObject(original, JsonSerializer.Create(jsonSerializerSettings));
            var deserializedJsonToken = JToken.FromObject(deserialized, JsonSerializer.Create(jsonSerializerSettings));
            Assert.IsTrue(JToken.EqualityComparer.Equals(originalJsonToken, deserializedJsonToken));
        }
    }
}
