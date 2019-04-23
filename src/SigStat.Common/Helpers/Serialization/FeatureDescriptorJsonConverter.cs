using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers
{
    public class FeatureDescriptorJsonConverter : JsonConverter
    {
        private bool Detailed { get; set; }
        public FeatureDescriptorJsonConverter(bool detail)
        {
            Detailed = detail;
        }
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(FeatureDescriptor));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (Detailed)
            {
                JObject jo = JObject.Load(reader);
                string key = (string)jo["Key"];
                string featureType = (string)jo["FeatureType"];
                Type currType = Type.GetType(featureType);
                FeatureDescriptor fd = FeatureDescriptor.Register(key, currType);
                return fd;
            }
            else{
                JObject jo = JObject.Load(reader);
                string key = (string)jo["Key"];
                FeatureDescriptor fd = FeatureDescriptor.Get(key);
                return fd;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (Detailed)
            {
                var fd = (FeatureDescriptor)value;
                serializer.Serialize(writer, new JObject { { "Key", fd.Key }, { "FeatureType", fd.FeatureType.AssemblyQualifiedName } });
            }
            else
            {
                var fd = (FeatureDescriptor)value;
                serializer.Serialize(writer, new JObject { { "Key", fd.Key } });
            }
        }
    }
}
