using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SigStat.Common.Helpers.Serialization;
using System;

namespace SigStat.Common.Helpers
{
    /// <summary>
    /// Custom serializer for <see cref="FeatureDescriptor"/> objects
    /// </summary>
    public class FeatureDescriptorJsonConverter : JsonConverter
    {
        /// <summary>
        /// Tells if the current object is of the correct type
        /// </summary>
        /// <param name="objectType">The type of the object</param>
        /// <returns>If the object can be converted or not</returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(FeatureDescriptor));
        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Deserializes the <see cref="FeatureDescriptor"/> json created by the this class
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
                FeatureDescriptor fd = FeatureDescriptor.Register(key, currType);
                return fd;
            }
            else{
                FeatureDescriptor fd = FeatureDescriptor.Get(value);
                return fd;
            }
        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Serializes the <see cref="FeatureDescriptor"/> to json with type depending on if it was serialized earlier or not
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.Formatting = Formatting.None;
            var state = serializer.Context.Context as FeatureStreamingContextState;
            var fd = (FeatureDescriptor)value;
            if (!state.KnownFeatureKeys.Contains(fd.Key))
            {
                serializer.Serialize(writer, fd.Key + " | " + fd.FeatureType.AssemblyQualifiedName);
                state.KnownFeatureKeys.Add(fd.Key);
                
            }
            else
            {
                serializer.Serialize(writer, new JObject {  fd.Key  });
            }
            writer.Formatting = Formatting.Indented;
        }
    }
}
