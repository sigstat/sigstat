using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SigStat.Common.Helpers.NetCoreSerialization
{
    public class NetCoreFeatureDescriptorTConverter : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType)
            {
                return (typeToConvert.GetGenericTypeDefinition() == typeof(FeatureDescriptor<>));
            }
            else
            {
                return false;
            }
        }

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            FeatureDescriptor fd = FeatureDescriptor.Register(reader.GetString(), typeToConvert);
            return fd;
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            var fd = (FeatureDescriptor)value;
            writer.WriteStringValue(fd.Name);

        }
    }
}
