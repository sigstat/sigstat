using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a feature with the type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Type of the feature.</typeparam>
    public class FeatureDescriptor<T> : FeatureDescriptor
    {
        private FeatureDescriptor(string name, string key, Type featureType) : base(name, key, featureType) { }
        private FeatureDescriptor(string name, string key) : base(name, key, typeof(T)) { }

        /// <summary>
        /// Get the <see cref="FeatureDescriptor{T}"/> of <paramref name="key"/>.
        /// If the key is not used yet, create one.
        /// This is the preferred way.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static FeatureDescriptor<T> Descriptor(string key)
        {
            if (descriptors.ContainsKey(key))
                return (FeatureDescriptor<T>)descriptors[key];
            //TODO: log info new descriptor created
            return new FeatureDescriptor<T>(key, key);//ha meg nincs ilyen, akkor csinalunk
        }

    }
}
