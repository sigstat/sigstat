using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SigStat.Common.Helpers.Serialization
{
    class VerifierResolver : DefaultContractResolver
    {
        
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            JsonObjectContract contract = base.CreateObjectContract(objectType);
            if (objectType == typeof(FeatureDescriptor))
            {
                contract.Converter = new FeatureDescriptorJsonConverter();
            }
            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(FeatureDescriptor<>))
            {
                contract.Converter = new FeatureDescriptorTJsonConverter();
            }
            return contract;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            return props.Where(p => ((p.Writable || p.PropertyName.Equals("AllFeatures")) && !p.PropertyName.Equals("Logger"))).ToList();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyName == "AllFeatures")
            {
                property.Converter = new FeatureDescriptorDictionaryConverter();
            }
            if (property.PropertyType.IsGenericType && 
                (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) &&
                (property.PropertyType == typeof(List<FeatureDescriptor>) || (property.PropertyType.GetGenericArguments()[0].IsGenericType &&
                property.PropertyType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(FeatureDescriptor<>)))))
            {
                property.Converter = new FeatureDescriptorListJsonConverter();
            }
            return property;
        }
    }
}
