using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SigStat.Common.Helpers.NetCoreSerialization
{
    public class NetCoreFeatureDescriptorDictionaryConverter : JsonConverter<Dictionary<string, FeatureDescriptor>>
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Dictionary<string, FeatureDescriptor>));
        }
        public override Dictionary<string, FeatureDescriptor> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Dictionary<string, FeatureDescriptor> features = new Dictionary<string, FeatureDescriptor>();
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                reader.Read();
                String s = reader.GetString();
                /*foreach (string fd in ja.Children())
                {
                    string[] strings = fd.Split('|');
                    string key = strings[0].Trim();
                    string featureType = strings[1].Trim();
                    Type currType = Type.GetType(featureType);
                    var fdType = typeof(FeatureDescriptor<>).MakeGenericType(currType);
                    var get = fdType.GetMethod("Get", BindingFlags.Public | BindingFlags.Static);

                    features.Add(key, (FeatureDescriptor)get.Invoke(null, new object[] { key }));
                }*/
                // Return the result
            }
            return features;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, FeatureDescriptor> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteStartArray();
            foreach (var fd in value.Values)
            {
                writer.WriteStringValue(fd.Key + " | " + fd.FeatureType.AssemblyQualifiedName);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
