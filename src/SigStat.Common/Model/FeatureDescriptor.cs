using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a feature with name and type.
    /// </summary>
    public class FeatureDescriptor
    {
        /// <summary> Gets or sets the name of the feature. </summary>
        public string Name { get; set; }
        /// <summary> Gets or sets the key of the feature. </summary>
        public string Key { get; set; }
        /// <summary> Gets or sets the type of the feature. </summary>
        public Type FeatureType { get; set; }

        //HACK: itt általános gyűjteménykezelés kéne a listakezelés helyett
        /// <summary> Gets whether the type of the feature is List. </summary>
        public bool IsCollection
        {
            get
            {
                return FeatureType.IsGenericType && FeatureType.GetGenericTypeDefinition() == typeof(List<>);
            }
            //private set;
        }
        /// <summary> The static dictionary of all descriptors. </summary>
        protected static readonly Dictionary<string, FeatureDescriptor> descriptors = new Dictionary<string, FeatureDescriptor>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDescriptor"/> class, and adds it to the static <see cref="descriptors"/>.
        /// Therefore, the <paramref name="key"/> parameter must be unique.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="featureType"></param>
        public FeatureDescriptor(string name, string key, Type featureType)
        {
            Name = name;
            Key = key;
            FeatureType = featureType;
            //IsCollection = FeatureType.IsGenericType && FeatureType.GetGenericTypeDefinition() == typeof(List<>);//nem csak List lehet

            if (descriptors.ContainsKey(key))
            {
                //TODO: log warning $"A descriptor with key '{key}' has already been registered. Choose an other key for your descriptor or use FeatureDescriptor.GetDescriptor() to get a reference to the existing one."
                return;//nem baj, ha mar van ilyen. Akkor ne csinaljunk semmit
            }
            else
                descriptors.Add(key, this);
        }

        /// <summary>
        /// Gets the <see cref="FeatureDescriptor"/> specified by <paramref name="key"/>.
        /// Throws error if key not found.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static FeatureDescriptor GetDescriptor(string key)
        {
             return descriptors[key];
        }

        /*public static string GetKey(Type featureType)
        {
            var fa = (FeatureAttribute)Attribute.GetCustomAttribute(featureType, typeof(FeatureAttribute));
            return fa?.FeatureKey ?? featureType.FullName;
        }
        public static string GetKey<T>()
        {
            return GetKey(typeof(T));
        }*/

    }
}
