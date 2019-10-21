using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SigStat.Common.Helpers.Serialization
{

    /// <summary>
    /// Helper class for serializing distance functions
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class DistanceFunctionJsonConverter : JsonConverter<Func<double[], double[], double>>
    {
        public override Func<double[], double[], double> ReadJson(JsonReader reader, Type objectType, Func<double[], double[], double> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var distanceFunc = (string)reader.Value;
            var splitDistanceFunc = distanceFunc.Split('|');
            var typeValue = splitDistanceFunc[0];
            var methodName = splitDistanceFunc[1];
            var methodParams = splitDistanceFunc[2].Split(';');


            var type = Type.GetType(typeValue);

            Func<double[], double[], double> resultFunc;

            if (type != null)
            {
                var paramTypes = new List<Type>();
                foreach(var t in methodParams)
                {
                    paramTypes.Add(Type.GetType(t));
                }

                var method = type.GetMethod(methodName, paramTypes.ToArray());
                resultFunc = (Func<double[], double[], double>)
                    Delegate.CreateDelegate(typeof(Func<double[], double[], double>), method);
            }
            else
            {
                throw new Exception("Unsopported distance function");
            }

            return resultFunc;

        }

        public override void WriteJson(JsonWriter writer, Func<double[], double[], double> value, JsonSerializer serializer)
        {
            if (value.Method.DeclaringType != null)
            {
                var enumerable = value.Method.GetParameters().Select(x => x.ParameterType.FullName + ";");
                var concated = string.Concat(enumerable).TrimEnd(';');
                serializer.Serialize(writer, $"{value.Method.DeclaringType.AssemblyQualifiedName}|{value.Method.Name}|{concated}");
            }
        }
    }
}
