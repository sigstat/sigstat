using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SigStat.Common.Helpers.NetCoreSerialization
{
    public class NetCoreSerializationHelper
    {
        public static JsonSerializerOptions GetSettings()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                MaxDepth = 64,
                IgnoreReadOnlyProperties = true,
                WriteIndented = true,
            };
            options.Converters.Add(new NetCoreFeatureDescriptorDictionaryConverter());
            options.Converters.Add(new NetCoreFeatureDescriptorConverter());
            options.Converters.Add(new NetCoreFeatureDescriptorTConverter());
            options.Converters.Add(new NetCoreConcurrentDictionaryConverter());
            return options;
        }
        public static T Deserialize<T>(string s) where T : new()
        {
            return JsonSerializer.Deserialize<T>(s,GetSettings());
        }

        public static string Serialize<T>(T o)
        {
            return JsonSerializer.Serialize(o,GetSettings());
        }

        public static void SerializeToFile<T>(T o, string path)
        {
            File.WriteAllText(path, Serialize(o));
        }

        public static T DeserializeFromFile<T>(string path) where T : new()
        {
            return Deserialize<T>(File.ReadAllText(path));
        }
    }
}
