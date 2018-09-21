using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a signature as a collection of features, containing the data that flows in the pipeline.
    /// </summary>
    public class Signature
    {
        /// <summary>An identifier for the Signature. Keep it unique to be useful for logs. </summary>
        public string ID { get; set; }
        /// <summary>Represents our knowledge on the origin of the signature. <see cref="Origin.Unknown"/> may be used in practice before it is verified.</summary>
        public Origin Origin { get; set; }
        /// <summary>A reference to the <see cref="Common.Signer"/> who this signature belongs to. (The origin is not constrained to be genuine.)</summary>
        public Signer Signer { get; set; }

        private readonly ConcurrentDictionary<string, object> features = new ConcurrentDictionary<string, object>();


        /// <summary>
        /// Gets or sets the specified feature.
        /// </summary>
        /// <param name="featureKey"></param>
        /// <returns>The feature object without cast.</returns>
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


        /// <summary>
        /// Gets or sets the specified feature.
        /// </summary>
        /// <param name="featureDescriptor"></param>
        /// <returns>The feature object without cast.</returns>
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

        /// <summary>
        /// Gets the specified feature.
        /// </summary>
        /// <param name="featureKey"></param>
        /// <returns>The casted feature object</returns>
        public T GetFeature<T>(string featureKey)
        {
            return (T)features[featureKey];
        }
        /// <summary>
        /// Gets the specified feature. This is the preferred way.
        /// </summary>
        /// <param name="featureDescriptor"></param>
        /// <returns>The casted feature object</returns>
        public T GetFeature<T>(FeatureDescriptor<T> featureDescriptor)
        {
            return (T)features[featureDescriptor.Key];
        }

        /// <summary>
        /// Gets the specified feature. This is the preferred way.
        /// </summary>
        /// <param name="featureDescriptor"></param>
        /// <returns>The casted feature object</returns>
        public T GetFeature<T>(FeatureDescriptor featureDescriptor)
        {
            //TODO: try cast, catch log
            return (T)features[featureDescriptor.Key];
        }

        /*public T GetFeature<T>()
        {
            return (T)features[FeatureDescriptor.GetKey<T>()];
        }*/

        /*public T GetFeature<T>(int index)
        {
            return ((List<T>)features[FeatureDescriptor.GetKey<T>()])[index];
        }*/

        /// <summary>
        /// Gets a collection of <see cref="FeatureDescriptor"/>s that are used in this signature.
        /// </summary>
        /// <returns>A collection of <see cref="FeatureDescriptor"/>s.</returns>
        public IEnumerable<FeatureDescriptor> GetFeatureDescriptors()
        {
            return features.Keys.Select(k => FeatureDescriptor.GetDescriptor(k));
        }

        /*/// <summary>
        /// Gets a list of <see cref="FeatureDescriptor"/>s of given type <typeparamref name="T"/> that are used in this signature.
        /// </summary>
        /// <returns>A list of <see cref="FeatureDescriptor"/>s.</returns>
        public List<T> GetFeatures<T>()
        {
            return (features.TryGetValue(FeatureDescriptor.GetKey<T>(), out var result)) ? result as List<T> : null;
        }*/

        /*public void SetFeature<T>(T feature)
        {
            features[FeatureDescriptor.GetKey(feature.GetType())] = feature;
        }*/

        /*public void SetFeatures<T>(List<T> feature)
        {
            features[FeatureDescriptor.GetKey<T>()] = feature;
        }*/

        /// <summary>
        /// Sets the specified feature. This is the preferred way.
        /// </summary>
        /// <param name="featureDescriptor">The feature to put the new value in.</param>
        /// <param name="feature">The value to set.</param>
        public void SetFeature<T>(FeatureDescriptor featureDescriptor, T feature)
        {
            features[featureDescriptor.Key] = feature;
        }

        /// <summary>
        /// Aggregate multiple features into one. Example: X, Y features -> P.xy feature.
        /// Use this for example at DTW algorithm input.
        /// </summary>
        /// <param name="fs">List of features to aggregate.</param>
        /// <returns>Aggregated feature value</returns>
        public List<double[]> GetAggregateFeature(List<FeatureDescriptor> fs)
        {
            double[][] values = null;

            int len = this.GetFeature<List<double>>(fs[0].Key).Count;
            //TODO: exception if: fs[0].len != fs[1].len
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

        public bool HasFeature(FeatureDescriptor featureDescriptor)
        {
            return features.ContainsKey(featureDescriptor.Key);
        }

        public bool HasFeature(string featureKey)
        {
            return features.ContainsKey(featureKey);
        }


        public override string ToString()
        {
            return ID;
        }


    }
}
