using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;

namespace SigStat.Common.Helpers.Serialization
{
    /// <summary>
    /// Custom serializer for <see cref="RectangleF"/> objects
    /// </summary>
    public class RectangleFConverter : JsonConverter
    {
        /// <summary>
        /// Tells if the current object is of the correct type
        /// </summary>
        /// <param name="objectType">The type of the object</param>
        /// <returns>If the object can be converted or not</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RectangleF);
        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Deserializes the <see cref="RectangleF"/> json created by the same class
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = serializer.Deserialize<JObject>(reader);

            return new RectangleF((float)jObject["X"], (float)jObject["Y"], (float)jObject["Width"], (float)jObject["Height"]);

        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Serializes the <see cref="RectangleF"/> to json
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rectangle = (RectangleF)value;

            serializer.Serialize(
                writer, new JObject { { "X", rectangle.X }, { "Y", rectangle.Y }, { "Width", rectangle.Width }, { "Height", rectangle.Height } });
        }
    }
}
