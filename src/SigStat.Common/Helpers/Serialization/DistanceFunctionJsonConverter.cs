using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SigStat.Common.Helpers.Serialization
{

    /// <summary>
    /// Helper class for serializing distance functions
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class DistanceFunctionJsonConverter : JsonConverter<Func<double[], double[], double>>
    {
        private readonly Dictionary<string, Type> primitiveTypes = new Dictionary<string, Type>
        {
            {"int", typeof(int)},
            {"int[]", typeof(int[])},
            {"long", typeof(long)},
            {"long[]", typeof(long[])},
            {"double", typeof(double)},
            {"double[]", typeof(double[])}
        };
        public override Func<double[], double[], double> ReadJson(JsonReader reader, Type objectType, Func<double[], double[], double> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var distanceFunc = (string)reader.Value;
            var splitDistanceFunc = distanceFunc.Split('|');
            var typeValue = splitDistanceFunc[0];
            var methodNameParam = splitDistanceFunc[1];

            var methodParams = methodNameParam.Substring(methodNameParam.IndexOf('(')+1, methodNameParam.IndexOf(')') - methodNameParam.IndexOf('(') -1).ToLower().Replace(",", string.Empty).Split(' ');
            var methodName = methodNameParam.Substring(methodNameParam.IndexOf(' ')+1, methodNameParam.IndexOf('(') - methodNameParam.IndexOf(' ')-1);

            var type = Type.GetType(typeValue);

            Func<double[], double[], double> resultFunc;

            if (type != null)
            {
                var paramTypes = new List<Type>();
                foreach(var t in methodParams)
                {
                    paramTypes.Add(primitiveTypes[t]);
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
                serializer.Serialize(writer, $"{value.Method.DeclaringType.AssemblyQualifiedName}|{value.Method}");
        }
    }
}
