using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a signature as a collection of features, containing the data that flows in the pipeline.
    /// </summary>
    public class Signature: IEnumerable<KeyValuePair<FeatureDescriptor, object>>
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
                // The approach below ensures, that the feature descriptor is registered on first use
                FeatureDescriptor.Register(featureKey, value.GetType());
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

        /// <summary>
        /// Initializes a signature instance
        /// </summary>
        public Signature()
        {

        }

        /// <summary>
        /// Initializes a signature instance with the given properties
        /// </summary>
        /// <param name="signatureID"></param>
        /// <param name="origin"></param>
        /// <param name="signer"></param>
        public Signature(string signatureID, Origin origin = Origin.Unknown, Signer signer = null)
        {
            ID = signatureID;
            Origin = origin;
            Signer = signer;
        }

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

        /// <summary>
        /// Gets a collection of <see cref="FeatureDescriptor"/>s that are used in this signature.
        /// </summary>
        /// <returns>A collection of <see cref="FeatureDescriptor"/>s.</returns>
        public IEnumerable<FeatureDescriptor> GetFeatureDescriptors()
        {
            return features.Keys.Select(k => FeatureDescriptor.Get(k));
        }

        /// <summary>
        /// Sets the specified feature. 
        /// </summary>
        /// <param name="featureDescriptor">The feature to put the new value in.</param>
        /// <param name="feature">The value to set.</param>
        public Signature SetFeature<T>(FeatureDescriptor featureDescriptor, T feature)
        {
            features[featureDescriptor.Key] = feature;
            return this;
        }
        /// <summary>
        /// Sets the specified feature. 
        /// </summary>
        /// <param name="featureKey">The unique key of the feature.</param>
        /// <param name="feature">The value to set.</param>
        public Signature SetFeature<T>(string featureKey, T feature)
        {
            //Ensure, that the FeatureDescriptor is registered
            FeatureDescriptor.Get<T>(featureKey);
            features[featureKey] = feature;
            return this;
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
            {
                values[i] = new double[fs.Count];//dim
            }

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

        /// <summary>
        /// Returns true if the signature contains the specified feature
        /// </summary>
        /// <param name="featureDescriptor"></param>
        /// <returns></returns>
        public bool HasFeature(FeatureDescriptor featureDescriptor)
        {
            return features.ContainsKey(featureDescriptor.Key);
        }

        /// <summary>
        /// Returns true if the signature contains the specified feature
        /// </summary>
        /// <param name="featureKey"></param>
        /// <returns></returns>
        public bool HasFeature(string featureKey)
        {
            return features.ContainsKey(featureKey);
        }

        /// <summary>
        /// Returns a string representation of the signature
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ID??"";
        }

        public IEnumerator<KeyValuePair<FeatureDescriptor, object>> GetEnumerator()
        {
            return features.Select(kvp => new KeyValuePair<FeatureDescriptor, object>(FeatureDescriptor.Get(kvp.Key), kvp.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //public void Add(FeatureDescriptor featureDescriptor, object featureValue)
        //{
        //    SetFeature(featureDescriptor, featureValue);
        //}
    }
}
