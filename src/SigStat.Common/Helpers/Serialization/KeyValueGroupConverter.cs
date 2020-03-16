using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SigStat.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Helpers.Serialization
{
    /// <summary>
    /// Serializes / Deserializes a logging dictionary <see cref="KeyValueGroup"/>
    /// </summary>
    class KeyValueGroupConverter : JsonConverter<KeyValueGroup>
    {
        /// <inheritdoc/>
        public override KeyValueGroup ReadJson(JsonReader reader, Type objectType, KeyValueGroup existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jo = serializer.Deserialize<JObject>(reader);
            KeyValueGroup kvg = new KeyValueGroup((string)jo["name"]);
            kvg.Items = jo["dict"].ToObject<Dictionary<string, object>>().ToList();//TODO: test
            return kvg;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, KeyValueGroup value, JsonSerializer serializer)
        {
            var dict = value.Items.ToDictionary(p => p.Key, p => p.Value);
            JObject jo = new JObject
            {
                { "name", value.Name },
                { "dict", JObject.FromObject(dict) }
            };
            serializer.Serialize(writer, jo);
        }
    }
}
