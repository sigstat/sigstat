using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace SigStat.Common
{
    public class Signature
    {
        public string ID { get; set; }

        public Origin Origin { get; set; }
        public Signer Signer { get; set; }

        private readonly ConcurrentDictionary<string, object> features = new ConcurrentDictionary<string, object>();



        public object this[string featureKey]
        {
            get
            {
                return features[featureKey];
            }
            set
            {
                features[featureKey] = value;
            }
        }

        public object this[FeatureDescriptor featureDescriptor]
        {
            get
            {
                return features[featureDescriptor.Key];
            }
            set
            {
                features[featureDescriptor.Key] = value;
            }
        }

        public T GetFeature<T>(string featureKey)
        {
            return (T)features[featureKey];
        }

        public T GetFeature<T>(FeatureDescriptor<T> featureDescriptor)
        {
            return (T)features[featureDescriptor.Key];
        }

        

        public T GetFeature<T>()
        {
            return (T)features[FeatureDescriptor.GetKey<T>()];
        }

        public T GetFeature<T>(int index)
        {
            return ((List<T>)features[FeatureDescriptor.GetKey<T>()])[index];
        }

        public IEnumerable<FeatureDescriptor> GetFeatureDescriptors()
        {
            return features.Keys.Select(k => FeatureDescriptor.GetDescriptor(k));
        }

        public List<T> GetFeatures<T>()
        {
            return features[FeatureDescriptor.GetKey<T>()] as List<T>;
        }


        public void SetFeature<T>(T feature)
        {
            features[FeatureDescriptor.GetKey(feature.GetType())] = feature;
        }

        public void SetFeatures<T>(List<T> feature)
        {
            features[FeatureDescriptor.GetKey<T>()] = feature;
        }

        public void SetFeature<T>(FeatureDescriptor featureDescriptor, T feature) 
        {
            features[featureDescriptor.Key] = feature;
        }


  

 



    }
}
