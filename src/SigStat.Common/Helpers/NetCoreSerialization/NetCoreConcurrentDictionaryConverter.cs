using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SigStat.Common.Helpers.NetCoreSerialization
{
    class NetCoreConcurrentDictionaryConverter : JsonConverter<ConcurrentDictionary<string, object>>
    {
        public override ConcurrentDictionary<string, object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            ConcurrentDictionary<string, object> obj = new ConcurrentDictionary<string, object>();
            if(reader.TokenType == JsonTokenType.StartObject)
            {
                reader.Read();
            }

            while (reader.TokenType == JsonTokenType.PropertyName)
            {
                //key
                string key = reader.GetString();
                reader.Read();

                //type
                Type currType = null;
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    reader.Read();
                    reader.Read();
                    currType = Type.GetType(reader.GetString());
                    reader.Read();
                }

                //list
                reader.Read();
                reader.Read();
                var instancedList = (IList)Activator.CreateInstance(currType);

                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    if (currType.AssemblyQualifiedName.Contains("Int")){
                        instancedList.Add(reader.GetInt32());
                    }
                    else if (currType.AssemblyQualifiedName.Contains("Boolean")){
                        instancedList.Add(reader.GetBoolean());
                    }
                    else if (currType.AssemblyQualifiedName.Contains("Double"))
                    {
                        instancedList.Add(reader.GetDouble());
                    }
                    else
                    {
                        instancedList.Add(reader.GetString());
                    }

                    reader.Read();
                }
                obj.TryAdd(key, instancedList);
                reader.Read();
                reader.Read();
            }
            return obj;
        }

        public override void Write(Utf8JsonWriter writer, ConcurrentDictionary<string, object> value, JsonSerializerOptions options)
        {
           writer.WriteStartObject();
           foreach(var c in value)
            {
                writer.WritePropertyName(c.Key);
                writer.WriteStartObject();
                writer.WriteString("$type", c.Value.ToString());
                Type currType = c.Value.GetType();
                var ToArray = currType.GetMethod("ToArray", BindingFlags.Public | BindingFlags.Instance);
                var array = ToArray.Invoke(c.Value, null);
                IEnumerable enumerableArray = array as IEnumerable;
                if (enumerableArray == null)
                {
                    throw new InvalidOperationException("signer feature not enumerable");
                }
                writer.WritePropertyName("values");
                writer.WriteStartArray();
                foreach (var i in enumerableArray)
                {
                    if (currType.AssemblyQualifiedName.Contains("Int"))
                    {
                        writer.WriteNumberValue((Int32)i);
                    }
                    else if (currType.AssemblyQualifiedName.Contains("Boolean"))
                    {
                        writer.WriteBooleanValue((bool)i);
                    }
                    else if (currType.AssemblyQualifiedName.Contains("Double"))
                    {
                        writer.WriteNumberValue((double)i);
                    }
                    else
                    {
                        writer.WriteStringValue(i.ToString());
                    }
                }
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
