using System.Collections.Generic;
using System.Linq;
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

        public static void AreEqual(object expected, string json)
        {
            var expectedJsonToken = JToken.FromObject(expected, JsonSerializer.Create(SerializationHelper.GetSettings()));
            var realJsonToken = JToken.Parse(json);
            Assert.IsTrue(JToken.EqualityComparer.Equals(expectedJsonToken, realJsonToken));
        }

        public static void AreEqual(object expected, object actual)
        {
            //TODO: propertyk alapján végigmenni és egyesével összenézni őket
            //... kivételek
            // csak írható/olvasható nem kell
            // JSonIgnore nem kell
            // referencia típus
            //  - stringeknél mehet az összehasonlítás
            //  - többinél rekurzívan kell menni tovább

            var expectedType = expected.GetType();
            var expectedProperties = expectedType.GetProperties();
            var actualType = actual.GetType();
            var actualProperties = actualType.GetProperties();

            Assert.AreEqual(expectedType,actualType);
            Assert.AreEqual(expectedProperties.Length, actualProperties.Length);

            if ((expectedType == typeof(List<FeatureDescriptor>) && actualType == typeof(List<FeatureDescriptor>)))
            {
                Assert.IsTrue(AreFeatureListEqual(expected as List<FeatureDescriptor>, actual as List<FeatureDescriptor>));
            }
            else
            {
                for (var index = 0; index < expectedProperties.Length; index++)
                {
                    var expectedProperty = expectedProperties[index];
                    var expectedValue = expectedProperty.GetValue(expected);

                    var actualProperty = actualProperties[index];
                    var actualValue = actualProperty.GetValue(actual);

                    if (!actualProperty.CanWrite && !expectedProperty.CanWrite) continue;
                    var expectedAttributes = expectedProperty.GetCustomAttributes(true)
                        .Select(a => a is JsonIgnoreAttribute).ToArray();
                    var actualAttributes = actualProperty.GetCustomAttributes(true)
                        .Select(a => a is JsonIgnoreAttribute).ToArray();
                    if (expectedAttributes.Any(a => a) && actualAttributes.Any(a => a)) continue;
                    if (actualProperty.PropertyType == typeof(string) &&
                        expectedProperty.PropertyType == typeof(string))
                    {
                        Assert.AreEqual(expectedValue, actualValue);
                    }
                    else if (actualProperty.PropertyType.IsClass && expectedProperty.PropertyType.IsClass)
                    {
                        AreEqual(expectedValue, actualValue);
                    }
                    else
                    {
                        Assert.AreEqual(expectedValue, actualValue);
                    }
                }
            }
        }

        public static bool AreFeatureListEqual(List<FeatureDescriptor> expected, List<FeatureDescriptor> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            return !expected.Where((f, i) => f.Key != actual[i].Key).Any();
        }

        public static void AreEqual(object expected, string json, JsonSerializerSettings jsonSerializerSettings)
        {
            var expectedJsonToken = JToken.FromObject(expected, JsonSerializer.Create(jsonSerializerSettings));
            var realJsonToken = JToken.Parse(json);
            Assert.IsTrue(JToken.EqualityComparer.Equals(expectedJsonToken, realJsonToken));
        }
        public static void AreEqual(object expected, object actual, JsonSerializerSettings jsonSerializerSettings)
        {
            var originalJsonToken = JToken.FromObject(expected, JsonSerializer.Create(jsonSerializerSettings));
            var deserializedJsonToken = JToken.FromObject(actual, JsonSerializer.Create(jsonSerializerSettings));
            Assert.IsTrue(JToken.EqualityComparer.Equals(originalJsonToken, deserializedJsonToken));
        }
    }
}
