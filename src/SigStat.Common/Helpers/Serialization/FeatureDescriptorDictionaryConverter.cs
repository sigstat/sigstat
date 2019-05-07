using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SigStat.Common.Helpers.Serialization
{
    /// <summary>
    /// Custom serializer for a Dictionary of <see cref="FeatureDescriptor"/>
    /// </summary>
    class FeatureDescriptorDictionaryConverter : JsonConverter
    {
        /// <summary>
        /// Tells if the current object is of the correct type
        /// </summary>
        /// <param name="objectType">The type of the object</param>
        /// <returns>If the object can be converted or not</returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Dictionary<string, FeatureDescriptor>));
        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Deserializes the dictionary of <see cref="FeatureDescriptor"/> created by the this class
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, FeatureDescriptor> features = new Dictionary<string, FeatureDescriptor>();
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
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Serializes the dictionary <see cref="FeatureDescriptor"/> with type of the descriptor
        /// </summary>
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
