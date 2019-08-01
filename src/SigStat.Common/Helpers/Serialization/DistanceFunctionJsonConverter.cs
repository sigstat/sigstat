using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers.Serialization
{

    /// <summary>
    /// Helper class for serializing distance functions
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class DistanceFunctionJsonConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(FeatureDescriptor));
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>

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
