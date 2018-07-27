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

        //ezt nem lehet..
        /*public T this<T>[FeatureDescriptor<T> featureDescriptor]
        {
            get
            {
                return (T)features[featureDescriptor.Key];
            }
            set
            {
                features[featureDescriptor.Key] = value;
            }
        }*/

        public T GetFeature<T>(string featureKey)
        {
            return (T)features[featureKey];
        }

        public T GetFeature<T>(FeatureDescriptor<T> featureDescriptor)
        {
            return (T)features[featureDescriptor.Key];
        }

        //proba
        public T GetFeature<T>(FeatureDescriptor f)
        {
            return (T)features[f.Key];
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
            return (features.TryGetValue(FeatureDescriptor.GetKey<T>(), out var result)) ? result as List<T> : null;
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

        /// <summary>
        /// feature aggregalas, pl. X és Y feature a bemenet -> P.xy lesz.
        /// Hasznos pl. multidimenziós DTW bemeneténél.
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public List<double[]> GetAggregateFeature(List<FeatureDescriptor> fs)
        {
            //TODO: mi legyen, ha nem array a feature

            double[][] values = null;

            int len = this.GetFeature<List<double>>(fs[0].Key).Count;
            values = new double[len][];
            for (int i = 0; i < len; i++)
                values[i] = new double[fs.Count];//dim

            for (int iF = 0; iF < fs.Count; iF++)
            {
                var v = this.GetFeature<List<double>>(fs[iF].Key);
                for (int i = 0; i < len; i++)
                {
                    values[i][iF] = v[i];
                }
            }
            return values.ToList();
        }
  

 



    }
}
