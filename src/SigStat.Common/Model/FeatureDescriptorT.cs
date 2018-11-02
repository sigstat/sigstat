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

        private static object syncRoot = new object();

        /// <summary>
        /// Gets the <see cref="FeatureDescriptor{T}"/> specified by <paramref name="key"/>.
        /// If the key is not registered yet, a new <see cref="FeatureDescriptor{T}"/> is automatically created with the given key and type.
        /// </summary>
        public new static FeatureDescriptor<T> Get(string key)
        {
            //TODO: Currently the management of FeatureDescriptors is blurred beween FeatureDescriptor and FeatureDescriptor<T> classes
            //      although we need both of them, the responsibility should be celarly separated
            if (descriptors.TryGetValue(key, out var featureDescriptor))
                return (FeatureDescriptor<T>)featureDescriptor;

            lock(syncRoot)
            {
                if (descriptors.TryGetValue(key, out featureDescriptor))
                    return (FeatureDescriptor<T>)featureDescriptor;
                //TODO: log info new descriptor created
                //QUESTION: should we always autocreate feature descriptors? How do we pass a Name parameter to a new descriptor?

                return new FeatureDescriptor<T>(key, key); ;
            }
        }


    }
}
