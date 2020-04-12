using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Logging
{
    /// <summary>
    /// Analizes logs and creates a model from the gained information
    /// </summary>
    public class LogAnalyzer
    {
        /// <summary>
        /// Creates a BenchmarkLogModel from previous logs
        /// </summary>
        /// <param name="logs">The collection of logs, that contains the required information for a BenchmarkLogModel</param>
        /// <returns>The Benchmark model filled with information according to the logs</returns>
        public static BenchmarkLogModel GetBenchmarkLogModel(IEnumerable<SigStatLogState> logs)
        {
            var model = new BenchmarkLogModel();



            foreach (var log in logs)
            {
                if (log is null) continue;
                //Benchmark key-values
                if (log.GetType() == typeof(BenchmarkKeyValueLogState))
                {
                    BenchmarkKeyValueLogState keyValueLog = (BenchmarkKeyValueLogState)log;

                    if (!model.KeyValueGroups.ContainsKey(keyValueLog.Group))
                        model.KeyValueGroups.Add(keyValueLog.Group, new KeyValueGroup(keyValueLog.Group));
                    model.KeyValueGroups[keyValueLog.Group].Items.Add(new KeyValuePair<string, object>(keyValueLog.Key, keyValueLog.Value));
                }
                //Classifier distances
                else if (log.GetType() == typeof(ClassifierDistanceLogState))
                {
                    ClassifierDistanceLogState classifierLog = (ClassifierDistanceLogState)log;

                    if (!model.SignerResults.ContainsKey(classifierLog.Signer1Id))
                        model.SignerResults.Add(classifierLog.Signer1Id, new SignerResults(classifierLog.Signer1Id));
                    model.SignerResults[classifierLog.Signer1Id].DistanceMatrix[classifierLog.Signature1Id, classifierLog.Signature2Id] = classifierLog.distance;
                }
                //Benchmark results
                else if (log.GetType() == typeof(BenchmarkResultsLogState))
                {
                    BenchmarkResultsLogState benchmarkLog = (BenchmarkResultsLogState)log;

                    model.BenchmarkResults.Items.Add(new KeyValuePair<string, object>("FAR", benchmarkLog.Far));
                    model.BenchmarkResults.Items.Add(new KeyValuePair<string, object>("FRR", benchmarkLog.Frr));
                    model.BenchmarkResults.Items.Add(new KeyValuePair<string, object>("AER", benchmarkLog.Aer));
                }
                //Signer results
                else if (log.GetType() == typeof(SignerResultsLogState))
                {
                    SignerResultsLogState signerLog = (SignerResultsLogState)log;

                    if (!model.SignerResults.ContainsKey(signerLog.SignerID))
                        model.SignerResults.Add(signerLog.SignerID, new SignerResults(signerLog.SignerID));
                    model.SignerResults[signerLog.SignerID].Far = signerLog.Far;
                    model.SignerResults[signerLog.SignerID].Frr = signerLog.Frr;
                    model.SignerResults[signerLog.SignerID].Aer = signerLog.Aer;
                }
            }


            return model;
        }
    }
}
