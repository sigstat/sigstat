using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SigStat.Common.Helpers
{
    public class FeatureDescriptorTJsonConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsGenericType)
            {
                return (objectType.GetTypeInfo().BaseType == typeof(FeatureDescriptor));
            }
            else
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load the JSON for the Result into a JObject
            JObject jo = JObject.Load(reader);

            // Read the properties which will be used as constructor parameters
            string key = (string)jo["Key"];

            // Construct the Result object using the non-default constructor
            

            // (If anything else needs to be populated on the result object, do that here)

            // Return the result
            return FeatureDescriptor.Get(key);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
