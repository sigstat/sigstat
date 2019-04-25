using Newtonsoft.Json;
using SigStat.Common.Helpers.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace SigStat.Common.Helpers
{
    public class SerializationHelper
    {

        public static JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings

            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new VerifierResolver(),
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState())
            };
        }

        public static T Deserialize<T>(string s) where T : new()
        {
            T desirializedObject = JsonConvert.DeserializeObject<T>(s, GetSettings());

            return desirializedObject;
        }
        public static T DeserializeFromFile<T>(string path) where T : new()
        {
            T desirializedObject = JsonConvert.DeserializeObject<T>(File.ReadAllText(path), GetSettings());

            return desirializedObject;
        }

        public static void JsonSerializeToFile<T>(T o, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(o, Formatting.Indented, GetSettings()));
        }

        public static string JsonSerialize<T>(T o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, GetSettings());
        }
    }
}
