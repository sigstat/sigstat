using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SigStat.Common.Helpers.Serialization
{

    /// <summary>
    /// Helper class for serializing distance functions
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class DistanceFunctionJsonConverter : JsonConverter<Func<double[], double[], double>>
    {
        /// <inheritdoc/>
        public override Func<double[], double[], double> ReadJson(JsonReader reader, Type objectType, Func<double[], double[], double> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var distanceFunc = (string)reader.Value;
            var names = distanceFunc.Split(',');
            
            var type = Type.GetType($"{names[0].Substring(0,names[0].LastIndexOf('.'))},{names[1]}");

            Func<double[], double[], double> resultFunc;

            if (type != null)
            {
                var paramTypes = new List<Type>()
                {
                    typeof(double[]),
                    typeof(double[]),
                };
                var method = type.GetMethod(names[0].Split('.').Last(), paramTypes.ToArray());
                resultFunc = (Func<double[], double[], double>)
                    Delegate.CreateDelegate(typeof(Func<double[], double[], double>), method);
            }
            else
            {
                throw new NotSupportedException("Unsopported distance function");
            }
           

            return resultFunc;

        }
        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, Func<double[], double[], double> value, JsonSerializer serializer)
        {
            if (value.Method.DeclaringType != null)
            {
                serializer.Serialize(writer, $"{value.Method.DeclaringType}.{value.Method.Name}, {value.Method.DeclaringType.Assembly.GetName().Name}");
            }
        }
    }
}
