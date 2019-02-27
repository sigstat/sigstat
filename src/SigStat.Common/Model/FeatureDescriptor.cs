using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a feature with name and type.
    /// </summary>
    public class FeatureDescriptor
    {
        /// <summary> Gets or sets a human readable name of the feature. </summary>
        public string Name { get; set; }
        /// <summary> Gets the unique key of the feature. </summary>
        public string Key { get; protected set; }
        /// <summary> Gets or sets the type of the feature. </summary>
        public Type FeatureType { get; set; }

        /// <summary> Gets whether the type of the feature is List. </summary>
        public bool IsCollection
        {
            get
            {
                //HACK: we should be able to handle IEnumerable, ICollection, IList etc.
                return FeatureType.IsGenericType && FeatureType.GetGenericTypeDefinition() == typeof(List<>);
            }
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
        protected FeatureDescriptor(string name, string key, Type featureType)
        {
            if (descriptors.ContainsKey(key))
            {
                throw new InvalidOperationException($"A descriptor with key '{key}' has already been registered. Choose an other key for your descriptor or use {nameof(FeatureDescriptor)}.{nameof(Get)} to get a reference to the existing one.");
            }

            Name = name;
            Key = key;
            FeatureType = featureType;
            descriptors.Add(key, this);
        }

        /// <summary>
        /// Returns true, if there is a FeatureDescriptor registered with the given key
        /// </summary>
        /// <param name="featureKey">The key to search for</param>
        /// <returns></returns>
        public static bool IsRegistered(string featureKey)
        {
            return descriptors.ContainsKey(featureKey);
        }

        /// <summary>
        /// Registers a new <see cref="FeatureDescriptor"/> with a given key.
        /// If the FeatureDescriptor is allready registered, this function will
        /// return a reference to the originally registered FeatureDescriptor.
        /// to the a
        /// </summary>
        /// <param name="featureKey">The key for identifying the FeatureDescriptor</param>
        /// <param name="type">The type of the actual feature values represented by <see cref=" FeatureDescriptor"/></param>
        /// <returns>A reference to the registered <see cref="FeatureDescriptor"/> instance</returns>
        public static FeatureDescriptor Register(string featureKey, Type type)
        {
            var fdType = typeof(FeatureDescriptor<>).MakeGenericType(type);
            var get = fdType.GetMethod("Get", BindingFlags.Public | BindingFlags.Static);
            return (FeatureDescriptor)get.Invoke(null, new object[] { featureKey });
        }

        /// <summary>
        /// Gets the <see cref="FeatureDescriptor"/> specified by <paramref name="key"/>.
        /// Throws <see cref="KeyNotFoundException"/> exception if there is no descriptor registered with the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static FeatureDescriptor Get(string key)
        {
            if (descriptors.TryGetValue(key, out var descriptor))
            {
                return descriptor;
            }

            throw new KeyNotFoundException($"There is no FeatureDescriptor registered with key: {descriptor}");
        }


        /// <summary>
        /// Gets the <see cref="FeatureDescriptor{T}"/> specified by <paramref name="key"/>.
        /// If the key is not registered yet, a new <see cref="FeatureDescriptor{T}"/> is automatically created with the given key and type.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static FeatureDescriptor<T> Get<T>(string key)
        {
            if (descriptors.ContainsKey(key))
            {
                return (FeatureDescriptor<T>)descriptors[key];
            }
            //TODO: log info new descriptor created
            return FeatureDescriptor<T>.Get(key);
        }

        /// <summary>
        /// Returns a string represenatation of the FeatureDescriptor
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} ({Key})";
        }

    }
}
