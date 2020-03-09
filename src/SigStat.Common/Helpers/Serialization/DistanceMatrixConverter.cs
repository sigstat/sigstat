using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers.Serialization
{
    /// <summary>
    /// Serializes/Deserializes a <see cref="DistanceMatrix{string,string,double}"/> object using its ToArray() and FromArray() methods.
    /// </summary>
    public class DistanceMatrixConverter : JsonConverter<DistanceMatrix<string, string, double>>
    {
        /// <inheritdoc/>
        public override DistanceMatrix<string, string, double> ReadJson(JsonReader reader, Type objectType, DistanceMatrix<string, string, double> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var a = serializer.Deserialize<object[,]>(reader);
            return DistanceMatrix<string, string, double>.FromArray(a);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, DistanceMatrix<string, string, double> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToArray());
        }
    }
}
