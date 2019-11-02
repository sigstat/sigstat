using System.Collections;
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
            Assert.IsTrue(JToken.EqualityComparer.Equals(expectedJsonToken, actualJsonToken), $"The following json strings are not equal:\r\nExpected:{expectedJsonToken}\r\nActual: {actualJsonToken}");
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

            if (expectedType.IsGenericType && expectedType.GetGenericTypeDefinition() == typeof(List<>) && actualType.IsGenericType && actualType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var expectedList = (IList)expected;
                var actualList = (IList)actual;
                if (expectedList.Count == 0 && actualList.Count == 0) return;
            }

            if (expectedType == typeof(List<FeatureDescriptor>) && actualType == typeof(List<FeatureDescriptor>))
            {
                Assert.IsTrue(AreFeatureListEqual(expected as List<FeatureDescriptor>, actual as List<FeatureDescriptor>));
            }
            else if (expectedType == typeof(Dictionary<string, FeatureDescriptor>) &&
                     actualType == typeof(Dictionary<string, FeatureDescriptor>))
            {
                Assert.IsTrue(AreFeatureDictionaryEqual(expected as Dictionary<string, FeatureDescriptor>, actual as Dictionary<string, FeatureDescriptor>));
            }
            else if (expectedType.IsGenericType && expectedType.GetGenericTypeDefinition() == typeof(FeatureDescriptor<>)  && actualType.IsGenericType && actualType.GetGenericTypeDefinition() == typeof(FeatureDescriptor<>))
            {
                Assert.IsTrue(AreFeaturesEqual(expected as FeatureDescriptor, actual as FeatureDescriptor));
            }
            else if (expectedType == typeof(Signature) && actualType == typeof(Signature) )
            {
                AreSignaturesEqual(expected as Signature, actual as Signature);
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
                    if(actualProperty.Name.Equals("Logger") && expectedProperty.Name.Equals("Logger")) continue;
                    if(expectedValue == null && actualValue == null) continue;
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
                    else if ((actualProperty.PropertyType.IsClass && expectedProperty.PropertyType.IsClass) || (expectedProperty.PropertyType.IsInterface && actualProperty.PropertyType.IsInterface))
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

        private static bool AreFeatureListEqual(List<FeatureDescriptor> expected, List<FeatureDescriptor> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            return !expected.Where((f, i) => f.Key != actual[i].Key).Any();
        }

        private static bool AreFeatureDictionaryEqual(Dictionary<string, FeatureDescriptor> expected,
            Dictionary<string, FeatureDescriptor> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var (key, value) in expected)
            {
                if (actual.ContainsKey(key))
                {
                    if (actual[key] != value)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            return true;
        }

        private static bool AreFeaturesEqual(FeatureDescriptor expected, FeatureDescriptor actual)
        {
            return expected.Key == actual.Key;
        }

        private static void AreSignaturesEqual(Signature expected, Signature actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Origin, actual.Origin);
            Assert.AreEqual(expected.Signer.ID, actual.Signer.ID);
            var expectedSignature = expected.Signer.Signatures;
            var actualSignatures = actual.Signer.Signatures;
            Assert.AreEqual(
            expectedSignature.Where((s, i) =>
                s.ID == actualSignatures[i].ID && s.Origin.Equals(actualSignatures[i].Origin) &&
                s.Signer.Equals(expected.Signer) && actualSignatures[i].Signer.Equals(actual.Signer)).Count(),
            expectedSignature.Count);
        }
    }
}
