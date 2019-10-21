using Newtonsoft.Json;
using SigStat.Common.Helpers.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace SigStat.Common.Helpers
{
    /// <summary>
    /// Json serialization and deserialization using the custom resolver  <see cref="VerifierResolver"/>
    /// </summary>
    public class SerializationHelper
    {
        /// <summary>
        /// Settings used for the serialization methods
        /// </summary>
        /// <returns>A new settings object</returns>
        public static JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings

            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new VerifierResolver(),
                Context = new StreamingContext(StreamingContextStates.All, new FeatureStreamingContextState())
            };
        }
        /// <summary>
        /// Constructs object from strings that were serialized previously
        /// </summary>
        /// <typeparam name="T">A type which has a public parameterless constructor</typeparam>
        /// <param name="s">The serialized string</param>
        /// <returns>The object that was serialized</returns>
        public static T Deserialize<T>(string s) where T : class
        {
            return JsonConvert.DeserializeObject<T>(s, GetSettings());
        }
        /// <summary>
        /// Constructs object from file given by a path
        /// </summary>
        /// <typeparam name="T">A type which has a public parameterless constructor</typeparam>
        /// <param name="path">Relative path to the file</param>
        /// <returns>The object that was serialized to the file</returns>
        public static T DeserializeFromFile<T>(string path) where T : class
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path), GetSettings());
        }

        /// <summary>
        /// Writes object to file to the given by path in json format
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="o">The object</param>
        /// <param name="path">Relative path</param>
        public static void JsonSerializeToFile<T>(T o, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(o, Formatting.Indented, GetSettings()));
        }
        /// <summary>
        /// Creates json string from object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="o">The object</param>
        /// <returns>The json string constructed from the object</returns>
        public static string JsonSerialize<T>(T o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, GetSettings());
        }
    }
}
