using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SigStat.Common.Helpers.NetCoreSerialization
{
    public class NetCoreSamplerConverter : JsonConverter<Sampler>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(Sampler));
        }

        public override Sampler Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Sampler value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("$Type");
            writer.WriteStringValue(value.GetType().ToString());
            writer.WriteEndObject();
        }
    }
}
