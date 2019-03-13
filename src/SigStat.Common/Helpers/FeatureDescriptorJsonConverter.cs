using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers
{
    public class FeatureDescriptorJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(FeatureDescriptor));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load the JSON for the Result into a JObject
            JObject jo = JObject.Load(reader);

            // Read the properties which will be used as constructor parameters
            string key = (string)jo["Key"];

            // Construct the Result object using the non-default constructor
            FeatureDescriptor fd = FeatureDescriptor.Get(key);

            // (If anything else needs to be populated on the result object, do that here)

            // Return the result
            return fd;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
