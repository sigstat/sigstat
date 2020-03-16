using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.Common.Pipeline;
using SigStat.Common.Model;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Helpers;
using System.Threading.Tasks;
using System.IO;
using SigStat.Benchmark.Model;
using System.Linq;
using SigStat.Benchmark.Helpers;

namespace SigStat.Benchmark
{
    class BenchmarkGenerator
    {
        /// <summary>
        /// Example rules that generate test benchmark configurations.
        /// This is used when no input file is provided.
        /// </summary>
        const string defaultRuleString = @"                
                [Benchmark] -> [Database]_[Split]_[Feature]_[Verifier]
                [Database] -> *svc2004 | mcyt100 | dutch | chinese | japanese
                [Feature] -> *X | *Y | P | XY | XYP | XP | YP
                [Split] -> *s1 | s2 | s3 | s4
                [Verifier] -> [Pipeline]_[Classifier]
                [Classifier] -> *Dtw_[Distance] | OptimalDtw_[Distance]
                [Distance] -> Manhattan | Euclidean
                [Pipeline] -> [Rotation]_[Gap]_[Resampling]_[Scaling]_[Translation]
                [Rotation] -> *none | rotation
                [Gap] -> [FilterGap]_[FillGap]
                [FilterGap] -> *none | filter 
                [FillGap] -> *none | fill_[FillInterpolation]
                [FillInterpolation] -> *linear | cubic
                [Resampling] -> *none | [SampleCount]samples_[ResamplingInterpolation]
                [SampleCount] -> *50 | 100 | 500 | 1000
                [ResamplingInterpolation] -> *linear | cubic
                [Scaling] -> *none | scale1| scaleS
                [Translation] -> *none | X0 | Y0 | XY0 | CogX | CogY | CogXY";


        public static IEnumerable<string> EnumerateBenchmarks(string ruleString)
        {
            var rules = GrammarEngine.ParseRules(ruleString);
            foreach (var derivedSentence in GrammarEngine.GenerateAllSentences(rules))
            {
                yield return derivedSentence.Sentence;
            }
        }


        /// <summary>
        /// Clear old configs, generate new configs, write to db
        /// </summary>
        /// <returns></returns>
        internal static async Task RunAsync(string rulesFilePath)
        {
            try
            {

                string rulesString;
                if (File.Exists(rulesFilePath))
                {//read rules from file
                    Console.WriteLine($"{DateTime.Now}: Loading rules for experiment {Program.Experiment}...");
                    rulesString = await File.ReadAllTextAsync(rulesFilePath);
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now}: Rules file not provided. Using default test rules.");
                    rulesString = defaultRuleString;
                }

                Console.WriteLine($"{DateTime.Now}: Initializing experiment...");
                bool success = await InitializeExperiment(rulesString);
                if (!success)
                    return;



                Console.WriteLine($"{DateTime.Now}: Ready.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task<bool> InitializeExperiment(string rulesString)
        {
            bool experimentExists = await BenchmarkDatabase.ExperimentExists();
            string prevRules = null;


            int choice = 1;//Default: Clear previous experiment
            if (experimentExists)
            {
                var queued = BenchmarkDatabase.CountQueued();
                var locked = BenchmarkDatabase.CountLocked();
                var errors = BenchmarkDatabase.CountFaulted();
                var finished = BenchmarkDatabase.CountFinished();
                var prevRulesTask = BenchmarkDatabase.GetGrammarRules();
                Task.WaitAll(queued, locked, errors, finished, prevRulesTask);
                prevRules = prevRulesTask.Result;

                if (rulesString != prevRules)
                    Console.WriteLine("Warning: The currently loaded rules don't match with the experiment's previous run.");

                if (!Console.IsInputRedirected)
                {
                    Console.WriteLine("This experiment already exists. Choose an option:");
                    Console.WriteLine();
                    Console.WriteLine($"(0) - Exit");
                    Console.WriteLine($"(1) - Clear Experiment");
                    Console.WriteLine($"        * Clear all {queued.Result + locked.Result + errors.Result + finished.Result} items");
                    Console.WriteLine($"(2) - Keep Results");
                    Console.WriteLine($"        * Keep {finished.Result} successful results");
                    Console.WriteLine($"        * Keep {queued.Result} unprocessed items in queue");
                    Console.WriteLine($"        * Remove {locked.Result + errors.Result} locks ({locked.Result} stuck, {errors.Result} faulted)");
                    while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out choice) || choice > 2);
                    Console.WriteLine();
                }
            }

            if (choice == 0)
            {//exit
                return false;
            }
            else if (choice == 1)
            {//clear all
                Console.WriteLine($"{DateTime.Now}: Clearing Experiment...");
                var deletedCount = await BenchmarkDatabase.ClearExperiment();
                Console.WriteLine($"{DateTime.Now}: Deleted {deletedCount} configurations.");
            }
            else if (choice == 2)
            {//keep results
                Console.WriteLine($"{DateTime.Now}: Removing locks...");
                int stuckCnt = await BenchmarkDatabase.RemoveLocks();
                int faultsCnt = await BenchmarkDatabase.ResetFaulted();
                Console.WriteLine($"{DateTime.Now}: Removed {stuckCnt + faultsCnt} locks.");
            }

            if (rulesString != prevRules)
            {
                Console.WriteLine($"{DateTime.Now}: Setting grammar rules...");
                await BenchmarkDatabase.SetGrammarRules(rulesString);
            }

            Console.WriteLine($"{DateTime.Now}: Generating combinations... ");
            var configs = EnumerateBenchmarks(rulesString);
            Console.WriteLine($"{DateTime.Now}: {configs.Count()} combinations generated. Updating database...");

            int insertedCnt = await BenchmarkDatabase.UpsertConfigs(configs);
            Console.WriteLine($"{DateTime.Now}: {insertedCnt} new items inserted.");

            return true;
        }

    }
}
