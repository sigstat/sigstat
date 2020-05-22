using SigStat.Benchmark.Helpers;
using SigStat.Benchmark.Model;
using SigStat.Common;
using SigStat.Common.Algorithms.Distances;
using SigStat.Common.Helpers;
using SigStat.Common.Model;
using SigStat.Common.PipelineItems.Classifiers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Execution
{
    class ClassificationResult
    {
        public string Algorithm { get; set; }
        public string Distance { get; set; }
        public string Parameter { get; set; }
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
            var builder = new BenchmarkBuilder();
            var distances = new Dictionary<string, IDistance<double[]>>
            {
                { "Manhattan", new ManhattanDistance() },
                { "Euclidean", new EuclideanDistance() }
            };
            Console.WriteLine("Loading databases...");
            Dictionary<string, List<Signer>> databases = builder.GetLoaders().ToDictionary(kvp => kvp.Key, kvp => kvp.Value.EnumerateSigners().ToList());
            using (var progress = ProgressHelper.StartNew(reportCount, 10))
            {
                var report = await BenchmarkDatabase.LockNextBenchmarkReport();
                while (report != null)
                {
                    try
                    {
                        var sampler = builder.GetSampler(report.Split);
                        var db = databases[report.Database];
                        var distance = distances[report.Distance];

                        List<ClassificationResult> results = new List<ClassificationResult>();
                        for (int k = 1; k <= 10; k++)
                        {
                            results.Add(GetClassificationResults(report, distance, k, sampler, db)); ;
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

        private static ClassificationResult GetClassificationResults(BenchmarkReport report, IDistance<double[]> distance, int k, Sampler sampler, List<Signer> signers)
        {
            NearestNeighborEerClassifier cl = new NearestNeighborEerClassifier(k, new DtwDistance(new EuclideanDistance()));
            cl.Sampler = sampler;
            cl.NearestNeighborCount = k;

            var models = signers.Select(s =>
                (NearestNeighborEerClassifier.SignerModel)cl.Train(
                    s.Signatures,
                    report.SignerResults.Single(sr => sr.SignerID == s.ID).DistanceMatrix)
                ).ToList();
            var errors = models.Select(m => m.ErrorRates.First(e => e.Key == m.Threshold).Value).ToList();
            var result = new ClassificationResult()
            {
                Algorithm = "kNN dtw",
                Distance = distance.GetType().Name,
                Parameter = k.ToString(),
                Frr = errors.Sum(e => e.Frr) / errors.Count,
                Far = errors.Sum(e => e.Far) / errors.Count,
            };
            result.Aer = (result.Frr + result.Far) / 2.0;
            return result;
        }
    }
}
