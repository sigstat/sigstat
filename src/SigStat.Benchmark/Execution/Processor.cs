using SigStat.Benchmark.Helpers;
using SigStat.Benchmark.Model;
using SigStat.Common;
using SigStat.Common.Algorithms.Distances;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Logging;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Execution
{
    public class ClassificationResult
    {
        public string Algorithm { get; set; }
        public string Distance { get; set; }
        public string J { get; set; }
        public string K { get; set; }

        public double Far { get; set; }
        public double Frr { get; set; }
        public double Aer { get; set; }
    }
    class Processor
    {
        internal static async Task RunAsync()
        {

            Console.WriteLine($"{DateTime.Now}: Gathering results for experiment {Program.Experiment}...");
            var reportCount = await BenchmarkDatabase.CountQueued();
            var rulesString = await BenchmarkDatabase.GetGrammarRules();
            var rules = GrammarEngine.ParseRules(rulesString);
            var benchmarkBuilder = new BenchmarkBuilder();
            var distances = new Dictionary<string, IDistance<double[]>>
            {
                { "Manhattan", new ManhattanDistance() },
                { "Euclidean", new EuclideanDistance() }
            }; 
            Console.WriteLine("Loading databases...");
            Dictionary<string, MemoryDataSetLoader> databases = benchmarkBuilder.GetLoaders().ToDictionary(kvp => kvp.Key, kvp => new MemoryDataSetLoader(kvp.Value.EnumerateSigners()));
            using (var progress = ProgressHelper.StartNew(reportCount, 10))
            {
                var report = await BenchmarkDatabase.LockNextBenchmarkReport();
                while (report != null)
                {
                    try
                    {
                        var configDict = GrammarEngine.ParseSentence(report.Config, rules);
                        var sampler = benchmarkBuilder.GetSampler(report.Split);
                        var loader = databases[report.Database];
                        var distance = distances[report.Distance];
                        var features = benchmarkBuilder.ParseFeatures(configDict["Feature"]).Cast<FeatureDescriptor>().ToList();
                        //report.SignerResults.Single(sr => sr.SignerID == s.ID).DistanceMatrix
                        var signerModels = loader.EnumerateSigners().Select(signer => new OneClassNearestNeighborClassifier.SignerModel()
                        {
                            SignerID = signer.ID,
                            TrainingSignatures = sampler.TrainingFilter( signer.Signatures).Select(s => new KeyValuePair<string, double[][]>(s.ID, s.GetAggregateFeature(features).ToArray())).ToList(),
                            DistanceCache = report.SignerResults.Single(s => s.SignerID == signer.ID).DistanceMatrix

                        }).ToList();
                        List<ClassificationResult> results = new List<ClassificationResult>();
                        for (int j = 1; j <= 10; j++)
                        {
                            for (int k = 1; k <= 9; k++)
                            {
                                Console.WriteLine($"J: {j}, K: {k}");
                                var benchmark =  benchmarkBuilder.Build(configDict);
                                benchmark.Loader = loader;
                                benchmark.SignerModels = signerModels.Cast<ISignerModel>().ToList();
                                benchmark.Verifier.Classifier = new OneClassNearestNeighborClassifier(j, k, 1, new DtwDistance(distance)) {  Features = features };
                                benchmark.Verifier.Pipeline.Items.Clear();
                                benchmark.Logger = new SimpleConsoleLogger();
                                var benchmarkResult = benchmark.Execute(false);
                                var classificationResult = new ClassificationResult()
                                {
                                    Algorithm = "OCjkNN dtw",
                                    Distance = distance.GetType().Name,
                                    K = k.ToString(),
                                    J = j.ToString(),
                                    Frr = benchmarkResult.FinalResult.Frr,
                                    Far = benchmarkResult.FinalResult.Far,
                                    Aer = benchmarkResult.FinalResult.Aer
                                };
                                results.Add(classificationResult);
                            }
                        }
                        await BenchmarkDatabase.SendResults(report.Config, "classification", results);
                        progress.Value++;
                        report = await BenchmarkDatabase.LockNextBenchmarkReport();
                    }
                    catch (Exception exc)
                    {
                        report = null;
                        await BenchmarkDatabase.SendErrorLog(report.Config, exc.ToString());
                    }
                }
            }
            Console.WriteLine("Ready");
        }


    }
}
