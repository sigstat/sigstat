using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.Common.Helpers.Serialization
{
    public class RectangleFConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RectangleF);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = serializer.Deserialize<JObject>(reader);

            return new RectangleF((float)jObject["X"], (float)jObject["Y"], (float)jObject["Width"], (float)jObject["Height"]);

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rectangle = (RectangleF)value;

            serializer.Serialize(
                writer, new JObject { { "X", rectangle.X }, { "Y", rectangle.Y }, { "Width", rectangle.Width }, { "Height", rectangle.Height } });
        }
    }
}
