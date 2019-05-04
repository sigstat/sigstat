using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SigStat.Common.Helpers.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers
{
    public class FeatureDescriptorJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(FeatureDescriptor));
        }

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
