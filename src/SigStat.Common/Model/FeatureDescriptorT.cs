using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    public class FeatureDescriptor<T> : FeatureDescriptor
    {
        private FeatureDescriptor(string name, string key, Type featureType) : base(name, key, featureType)
        {
        }
        private FeatureDescriptor(string name, string key) : base(name, key, typeof(T))
        {

        }

        public static FeatureDescriptor<T> Descriptor(string key)
        {
            if (descriptors.ContainsKey(key))
                return (FeatureDescriptor<T>)descriptors[key];
            //TODO: log info new descriptor created
            return new FeatureDescriptor<T>(key, key);//ha meg nincs ilyen, akkor csinalunk
        }

    }
}
