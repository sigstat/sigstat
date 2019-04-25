using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SigStat.Common.Helpers
{
    public class FeatureDescriptorTJsonConverter : JsonConverter
    {
        private bool Detailed { get; set; }
        public FeatureDescriptorTJsonConverter(bool detail)
        {
            Detailed = detail;
        }
        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsGenericType)
            {
                return (objectType.GetTypeInfo().BaseType == typeof(FeatureDescriptor));
            }
            else
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (Detailed)
            {
                JObject jo = JObject.Load(reader);
                string key = (string)jo["Key"];
                string featureType = (string)jo["FeatureType"];
                Type currType = Type.GetType(featureType);
                var fdType = typeof(FeatureDescriptor<>).MakeGenericType(currType.GenericTypeArguments);
                var get = fdType.GetMethod("Get", BindingFlags.Public | BindingFlags.Static);
                return get.Invoke(null, new object[] { key });
            }
            else
            {
                JObject jo = JObject.Load(reader);
                string key = (string)jo["Key"];
                FeatureDescriptor fd = FeatureDescriptor.Get(key);
                return fd;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //serializer.Context.Context // serializer.ContextState
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
