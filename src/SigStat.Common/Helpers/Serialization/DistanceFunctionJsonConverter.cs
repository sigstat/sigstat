using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers.Serialization
{
    /// <summary>
    /// </summary>
    public class DistanceFunctionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(FeatureDescriptor));
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch ((string)reader.Value)
            {
                case "Manhattan": return (Func<double[], double[], double>)Accord.Math.Distance.Manhattan;
                case "Euclidean": return (Func<double[], double[], double>)Accord.Math.Distance.Euclidean;
                default:
                    throw new Exception("Unsopported distance function");
            }
        }
        /// <summary>
        /// Overwrite of the <see cref="JsonConverter"/> method
        /// Serializes the <see cref="FeatureDescriptor"/> to json with type depending on if it was serialized earlier or not
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // HACK: !!!!!!!!!
            var func = (Func<double[], double[], double>)value;
            if (func(new[] { 0d, 0 }, new[] { 1d, 1 }) == 2)
                serializer.Serialize(writer, "Manhattan");
            else
                serializer.Serialize(writer, "Euclidean");
        }
    }
}
