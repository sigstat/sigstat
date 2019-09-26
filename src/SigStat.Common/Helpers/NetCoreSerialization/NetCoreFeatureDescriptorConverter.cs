using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SigStat.Common.Helpers.NetCoreSerialization
{
    public class NetCoreFeatureDescriptorConverter : JsonConverter<FeatureDescriptor>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return (typeToConvert == typeof(FeatureDescriptor));
        }
        public override FeatureDescriptor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                reader.Read();
                string value = reader.GetString();
                if (value.Contains("|"))
                {
                    string[] strings = value.Split('|');
                    string key = strings[0].Trim();
                    string featureType = strings[1].Trim();
                    Type currType = Type.GetType(featureType);
                    FeatureDescriptor fd = FeatureDescriptor.Register(key, currType);
                    return fd;
                }
                else
                {
                    FeatureDescriptor fd = FeatureDescriptor.Get(value);
                    return fd;
                }
            }
            else return null;
        }

        public override void Write(Utf8JsonWriter writer, FeatureDescriptor value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Key + " | " + value.FeatureType.AssemblyQualifiedName);

        }
    }
}
