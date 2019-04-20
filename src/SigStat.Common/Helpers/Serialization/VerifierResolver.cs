using Newtonsoft.Json.Serialization;
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
            if (objectType.IsGenericType && objectType.GetTypeInfo().BaseType == typeof(FeatureDescriptor))
            {
                contract.Converter = new FeatureDescriptorTJsonConverter();
            }

            return contract;
        }
    }
}
