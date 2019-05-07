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
    /// <summary>
    /// Custom resolver for customizing the json serialization 
    /// </summary>
    class VerifierResolver : DefaultContractResolver
    {
        /// <summary>
        /// Decides if the current property should be serialized or not
        /// </summary>
        /// <param name="type">The type of the current property</param>
        /// <param name="memberSerialization">The type of member serialization in Json.NET</param>
        /// <returns>A bool</returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            return props.Where(p => ((p.Writable || p.PropertyName.Equals("AllFeatures")) && !p.PropertyName.Equals("Logger"))).ToList();
        }
        /// <summary>
        /// Selects which JsonConverter should be used for the property
        /// </summary>
        /// <param name="member">A  <see cref="MemberInfo"/></param>
        /// <param name="memberSerialization">The type of member serialization in Json.NET</param>
        /// <returns></returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyName == "AllFeatures")
            {
                property.Converter = new FeatureDescriptorDictionaryConverter();
            }
            if (
                property.PropertyType.IsGenericType && 
               (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) && 
               (property.PropertyType == typeof(List<FeatureDescriptor>) || 
               (property.PropertyType.GetGenericArguments()[0].IsGenericType && 
               property.PropertyType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(FeatureDescriptor<>))))
               )
            {
                property.Converter = new FeatureDescriptorListJsonConverter();
            }
            if (property.PropertyType == typeof(FeatureDescriptor))
            {
                property.Converter = new FeatureDescriptorJsonConverter();
            }
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(FeatureDescriptor<>))
            {
                property.Converter = new FeatureDescriptorTJsonConverter();
            }
            return property;
        }
    }
}
