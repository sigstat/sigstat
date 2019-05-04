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
            JArray ja = JArray.Load(reader);
            foreach (string fd in ja.Children())
            {
                string[] strings = fd.Split('|');
                string key = strings[0].Trim();
                string featureType = strings[1].Trim();
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

            var state = serializer.Context.Context as FeatureStreamingContextState;
            var array = (Dictionary<string, FeatureDescriptor>)value;
            writer.WriteStartArray();
            foreach (var fd in array.Values)
            {
                serializer.Serialize(writer,  fd.Key + " | " + fd.FeatureType.AssemblyQualifiedName);
                state.KnownFeatureKeys.Add(fd.Key);
            }
            writer.WriteEndArray();
        }
    }
}
