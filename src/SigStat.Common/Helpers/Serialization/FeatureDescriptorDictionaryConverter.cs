using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SigStat.Common.Helpers.Serialization
{
    class FeatureDescriptorDictionaryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Dictionary<string, FeatureDescriptor>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, FeatureDescriptor> features = new Dictionary<string, FeatureDescriptor>();
            // Load the JSON for the Result into a JObject
            JObject jo = JObject.Load(reader);
            var items = jo.First.First;
            foreach (var fd in items)
            {
                string key = (string)fd["Key"];
                string featureType = (string)fd["FeatureType"];
                Type currType = Type.GetType(featureType);
                var fdType = typeof(FeatureDescriptor<>).MakeGenericType(currType);
                var get = fdType.GetMethod("Get", BindingFlags.Public | BindingFlags.Static);

                features.Add(key,(FeatureDescriptor)get.Invoke(null, new object[] { key }));
            }
            // Return the result
            return features;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var array = (Dictionary<string, FeatureDescriptor>)value;
            writer.WriteStartObject();
            writer.WritePropertyName("Items");
            writer.WriteStartArray();
            foreach (var fd in array.Values)
            {
                serializer.Serialize(writer, new JObject { { "Key", fd.Key }, { "FeatureType", fd.FeatureType.AssemblyQualifiedName } });
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
