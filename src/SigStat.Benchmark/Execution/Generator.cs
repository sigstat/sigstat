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
    class Generator
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
            string rulesString;
            if (File.Exists(rulesFilePath))
            {//read rules from file
                WriteLine($"Loading rules for experiment {Program.Experiment}...");
                rulesString = await File.ReadAllTextAsync(rulesFilePath);
            }
            else
            {
                WriteLine("Rules file not provided. Using default test rules.");
                rulesString = defaultRuleString;
            }

            WriteLine("Initializing experiment...");
            
            await InitializeExperiment(rulesString);

            WriteLine("Ready.");
        }

        public static async Task InitializeExperiment(string rulesString)
        {
            bool experimentExists = await BenchmarkDatabase.ExperimentExists();
            var prevRules = BenchmarkDatabase.GetGrammarRules();


            int choice = 1;//Default: Clear previous experiment
            if (experimentExists)
            {
                var queued = BenchmarkDatabase.CountQueued();
                var locked = BenchmarkDatabase.CountLocked();
                var errors = BenchmarkDatabase.CountFaulted();
                var finished = BenchmarkDatabase.CountFinished();
                Task.WaitAll(queued, locked, errors, finished, prevRules);

                if (rulesString != prevRules.Result)
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
                    while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out choice) || choice > 2) ;
                    Console.WriteLine();
                }
            }

            switch (choice)
            { 
                case 0: //exit
                    return;
                case 1: //clear all
                    WriteLine($"Clearing Experiment...");
                    await BenchmarkDatabase.ClearExperiment();
                    WriteLine($"Deleted all configurations.");
                    break;
                case 2:
                    WriteLine($"Removing locks...");
                    int stuckCnt = await BenchmarkDatabase.RemoveLocks();
                    int faultsCnt = await BenchmarkDatabase.ResetFaulted();
                    WriteLine($"Removed {stuckCnt + faultsCnt} locks.");
                    break;
            }

            if (rulesString != prevRules.Result)
            {
                WriteLine($"Setting grammar rules...");
                await BenchmarkDatabase.SetGrammarRules(rulesString);
            }

            WriteLine($"Generating combinations... ");
            var configs = EnumerateBenchmarks(rulesString);
            WriteLine($"{configs.Count()} combinations generated. Updating database...");

            int insertedCnt = await BenchmarkDatabase.UpsertConfigs(configs);
            WriteLine($"{insertedCnt} new items inserted.");

        }

        public static void WriteLine(string value)
        {
            Console.WriteLine($"{DateTime.Now}: {value}");
        }

    }
}
