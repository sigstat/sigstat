using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SigStat.Common.Helpers.Serialization
{
    /// <summary>
    /// Custom serializer for lists containing <see cref="FeatureDescriptor"/> or  <see cref="FeatureDescriptor{T}"/> objects
    /// </summary>
    class FeatureDescriptorListJsonConverter : JsonConverter
    {
        private FeatureDescriptorTJsonConverter helperConverter = new FeatureDescriptorTJsonConverter();
        /// <summary>
        /// Tells if the current object is of the correct type
        /// </summary>
        /// <param name="objectType">The type of the object</param>
        /// <returns>If the object can be converted or not</returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<PipelineOutput>));
        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Deserializes the list of <see cref="FeatureDescriptor"/> objects
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray ja = JArray.Load(reader);
            var listType = typeof(List<>).MakeGenericType(objectType.GenericTypeArguments);
            var instance = (IList)Activator.CreateInstance(listType);
            foreach (string fd in ja.Children())
            {
                object featureDescriptor = GetFeatureDesricptor(fd);
                instance.Add(featureDescriptor);
            }
            return instance;
        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Serializes the list of <see cref="FeatureDescriptor"/> objects to json
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.Formatting = Formatting.None;
            writer.WriteStartArray();
            foreach (var fd in (IList)value)
            {
                helperConverter.WriteJson(writer,fd,serializer);
            }
            writer.Formatting = Formatting.None;
            writer.WriteEndArray();
            writer.Formatting = Formatting.Indented;
        }

        private object GetFeatureDesricptor(string json)
        {

            string value = json;
            if (value.Contains("|"))
            {
                string[] strings = value.Split('|');
                string key = strings[0].Trim();
                string featureType = strings[1].Trim();
                Type currType = Type.GetType(featureType);
                var fdType = typeof(FeatureDescriptor<>).MakeGenericType(currType.GenericTypeArguments);
                var get = fdType.GetMethod("Get", BindingFlags.Public | BindingFlags.Static);
                return get.Invoke(null, new object[] { key });
            }
            else
            {
                FeatureDescriptor fd = FeatureDescriptor.Get(value);
                return fd;
            }
        }
    }
}
