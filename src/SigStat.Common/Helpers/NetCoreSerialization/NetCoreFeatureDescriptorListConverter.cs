using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SigStat.Common.Helpers.NetCoreSerialization
{
    public class NetCoreFeatureDescriptorListConverter : JsonConverter<List<FeatureDescriptor>>
    {
        public override List<FeatureDescriptor> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, List<FeatureDescriptor> value, JsonSerializerOptions options)
        {
            writer.WritePropertyName("Features");
            writer.WriteStartArray();
            foreach(var f in value)
            {
                writer.WriteStringValue(f.Name);
            }
            writer.WriteEndArray();
        }
    }
}
