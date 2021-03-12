using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Logging
{
    /// <summary>
    /// Represents the results of a benchmark
    /// </summary>
    public class BenchmarkLogModel
    {
        /// <summary>
        /// Name of the "BenchmarkResults" group
        /// </summary>
        [JsonIgnore]
        public static string BenchmarkResultsGroupName => "BenchmarkResults";
        /// <summary>
        /// Name of the "Parameters" group
        /// </summary>
        [JsonIgnore]
        public static string ParametersGroupName => "Parameters";
        /// <summary>
        /// Name of the "Excecution" group
        /// </summary>
        [JsonIgnore]
        public static string ExecutionGroupName => "Execution";
        /// <summary>
        /// Benchmark results stored in Key-Value groups
        /// </summary>
        public Dictionary<string, KeyValueGroup> KeyValueGroups { get; set; }

        /// <summary>
        /// Benchmark results group
        /// </summary>
        [JsonIgnore]
        public KeyValueGroup BenchmarkResults { get { return KeyValueGroups[BenchmarkResultsGroupName]; } }

        /// <summary>
        /// Parameters group
        /// </summary>
        [JsonIgnore]
        public KeyValueGroup Parameters { get { return KeyValueGroups[ParametersGroupName]; } }

        /// <summary>
        /// Excecution group
        /// </summary>
        [JsonIgnore]
        public KeyValueGroup Excecution { get { return KeyValueGroups[ExecutionGroupName]; } }

        /// <summary>
        /// Results belonging to signers
        /// </summary>
        public SortedDictionary<string, SignerResults> SignerResults { get; set; }

        /// <summary>
        /// Default constructor creating a blank model.
        /// </summary>
        public BenchmarkLogModel()
        {
            KeyValueGroups = new Dictionary<string, KeyValueGroup>();
            KeyValueGroups.Add(BenchmarkResultsGroupName, new KeyValueGroup(BenchmarkResultsGroupName));
            KeyValueGroups.Add(ParametersGroupName, new KeyValueGroup(ParametersGroupName));
            KeyValueGroups.Add(ExecutionGroupName, new KeyValueGroup(ExecutionGroupName));
            SignerResults = new SortedDictionary<string, SignerResults>();
        }

    }


}
