using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    public class FeatureDescriptor
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public Type FeatureType { get; set; }

        //HACK: itt általános gyűjteménykezelés kéne a listakezelés helyett
        public bool IsCollection
        {
            get
            {
                return FeatureType.IsGenericType && FeatureType.GetGenericTypeDefinition() == typeof(List<>);
            }
        }
        static readonly Dictionary<string, FeatureDescriptor> descriptors = new Dictionary<string, FeatureDescriptor>();

        public FeatureDescriptor(string name, string key, Type featureType)
        {
            if (descriptors.ContainsKey(key))
            {
                throw new InvalidOperationException($"A descriptor with key '{key}' has already been registered. Choose an other key for your descriptor or use FeatureDescriptor.GetDescriptor() to get a reference to the existing one.");
            }
            Name = name;
            Key = key;
            FeatureType = featureType;
            descriptors.Add(key, this);
        }

        public static FeatureDescriptor GetDescriptor(string key)
        {
            return descriptors[key];
        }


        public static string GetKey(Type featureType)
        {
            var fa = (FeatureAttribute)Attribute.GetCustomAttribute(featureType, typeof(FeatureAttribute));
            return fa?.FeatureKey ?? featureType.FullName;
        }
        public static string GetKey<T>()
        {
            return GetKey(typeof(T));

        }

    }
}
