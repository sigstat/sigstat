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
        public FeatureDescriptor(string name, string key) : base(name, key, typeof(T))
        {

        }

        public new static FeatureDescriptor<T> GetDescriptor(string key)
        {
            return (FeatureDescriptor<T>)descriptors[key];
        }

    }
}
