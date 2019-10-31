using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test
{
    static class JsonAssert
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

        static JsonAssert()
        {
            SigStatSerializer = JsonSerializer.Create(SerializationHelper.GetSettings());
        }

        public static void AreEqual(string expected, string actual)
        {
            
            var expectedJsonToken = JToken.Parse(expected);
            var actualJsonToken = JToken.Parse(actual);
            Assert.IsTrue(JToken.EqualityComparer.Equals(expectedJsonToken, actualJsonToken), $"The following json strings are not equal:\r\nExpected:{expected}\r\nActual: {actual}");
        }

        public static void AreEqual(object original, string json)
        {
            var expectedJsonToken = JToken.FromObject(original, JsonSerializer.Create(SerializationHelper.GetSettings()));
            var realJsonToken = JToken.Parse(json);
            Assert.IsTrue(JToken.EqualityComparer.Equals(expectedJsonToken, realJsonToken));
        }

        public static void AreEqual(object original, object deserialized)
        {
            //TODO: propertyk alapján végigmenni és egyesével összenézni őket
            //... kivételek
            // csak írható/olvasható nem kell
            // JSonIgnore nem kell
            // referencia típus
            //  - stringeknél mehet az összehasonlítás
            //  - többinél rekurzívan kell menni tovább

            var originalJsonToken = JToken.FromObject(original, JsonSerializer.Create(SerializationHelper.GetSettings()));
            var deserializedJsonToken = JToken.FromObject(deserialized, JsonSerializer.Create(SerializationHelper.GetSettings()));
            Assert.IsTrue(JToken.EqualityComparer.Equals(originalJsonToken,deserializedJsonToken));
        }

        public static void AreEqual(object original, string json, JsonSerializerSettings jsonSerializerSettings)
        {
            var expectedJsonToken = JToken.FromObject(original, JsonSerializer.Create(jsonSerializerSettings));
            var realJsonToken = JToken.Parse(json);
            Assert.IsTrue(JToken.EqualityComparer.Equals(expectedJsonToken, realJsonToken));
        }
        public static void AreEqual(object original, object deserialized, JsonSerializerSettings jsonSerializerSettings)
        {
            var originalJsonToken = JToken.FromObject(original, JsonSerializer.Create(jsonSerializerSettings));
            var deserializedJsonToken = JToken.FromObject(deserialized, JsonSerializer.Create(jsonSerializerSettings));
            Assert.IsTrue(JToken.EqualityComparer.Equals(originalJsonToken, deserializedJsonToken));
        }
    }
}
