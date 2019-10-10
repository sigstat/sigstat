using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;

namespace SigStat.Common.Helpers.NetCoreSerialization
{
    public class NetCoreObjectConverter : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("$Type");
            writer.WriteStringValue(value.GetType().ToString());
            foreach (var pi in value.GetType().GetProperties())
            {
                if (!pi.CanWrite || !pi.CanRead)
                    return;

                var propValue = pi.GetValue(value);
                if (pi.PropertyType.IsPrimitive)
                {
                    writer.WriteString(pi.Name, propValue.ToString());
                }
                else
                {
                    writer.WritePropertyName(pi.Name);
                    var converter = options.GetConverter(propValue.GetType());
                    var writeMethod = converter.GetType().GetMethod("Write");
                    writeMethod.Invoke(converter, new object[] { writer, propValue, options });

                }
                
            }
            writer.WriteEndObject();
        }
    }
}
