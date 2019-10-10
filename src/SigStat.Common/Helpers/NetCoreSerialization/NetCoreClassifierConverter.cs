using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;

namespace SigStat.Common.Helpers.NetCoreSerialization
{
    public class NetCoreClassifierConverter : JsonConverter<OptimalDtwClassifier>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(OptimalDtwClassifier));
        }

        public override OptimalDtwClassifier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, OptimalDtwClassifier value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("$Type");
            writer.WriteStringValue(value.GetType().ToString());
            writer.WriteStringValue(JsonSerializer.Serialize(value.Sampler, options));
            writer.WriteStringValue(JsonSerializer.Serialize(value.Features, options));
            writer.WriteEndObject();
        }
    }
}
