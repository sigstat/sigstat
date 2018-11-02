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
        /// Gets the <see cref="FeatureDescriptor"/> specified by <paramref name="key"/>.
        /// Throws <see cref="KeyNotFoundException"/> exception if there is no descriptor registered with the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static FeatureDescriptor Get(string key)
        {
            if (descriptors.TryGetValue(key, out var descriptor))
                return descriptor;
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
                return (FeatureDescriptor<T>)descriptors[key];
            //TODO: log info new descriptor created
            return FeatureDescriptor<T>.Get(key);
        }

    }
}
