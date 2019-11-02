using Newtonsoft.Json;
using SigStat.Common.Helpers.Serialization;
using System;
using System.Reflection;

namespace SigStat.Common.Helpers
{
    /// <summary>
    /// Custom serializer for <see cref="FeatureDescriptor{T}"/> objects
    /// </summary>
    public class FeatureDescriptorTJsonConverter : JsonConverter
    {
        /// <summary>
        /// Tells if the current object is of the correct type
        /// </summary>
        /// <param name="objectType">The type of the object</param>
        /// <returns>If the object can be converted or not</returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsGenericType)
            {
                return (objectType.GetGenericTypeDefinition() == typeof(FeatureDescriptor<>));
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Deserializes the <see cref="FeatureDescriptor{T}"/> json created by the this class
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            string value = (string)reader.Value;
            if (value.Contains("|"))
            {
                string[] strings = value.Split('|');
                string key = strings[0].Trim();
                string featureType = strings[1].Trim();
                Type currType = Type.GetType(featureType);
                var fdType = typeof(FeatureDescriptor<>).MakeGenericType(currType);
                var get = fdType.GetMethod("Get", BindingFlags.Public | BindingFlags.Static);
                return get.Invoke(null, new object[] { key });
            }
            else
            {
                FeatureDescriptor fd = FeatureDescriptor.Get(value);
                return fd;
            }
        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Serializes the <see cref="FeatureDescriptor{T}"/> to json with type depending on if it was serialized earlier or not
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.Formatting = Formatting.None;
            var state = serializer.Context.Context as FeatureStreamingContextState;
            var fd = (FeatureDescriptor)value;
            if (!state.KnownFeatureKeys.Contains(fd.Key) && state.CompactFeatures == false)
            {
                serializer.Serialize(writer, fd.Key + " | " + fd.FeatureType.AssemblyQualifiedName);
                state.KnownFeatureKeys.Add(fd.Key);
            }
            else
            {
                serializer.Serialize(writer,  fd.Key  );
            }
            writer.Formatting = Formatting.Indented;
        }
    }
}
