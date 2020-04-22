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


        public static int BatchSize { get; set; }

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
        internal static async Task RunAsync(string rulesFilePath, int batchSize)
        {
            BatchSize = batchSize;
            string rulesString;
            if (File.Exists(rulesFilePath))
            {//read rules from file
                WriteLine($"Loading rules for experiment ",Program.Experiment);
                rulesString = await File.ReadAllTextAsync(rulesFilePath);
            }
            else
            {
                WriteLine("Rules file not provided. Using default test rules.");
                rulesString = defaultRuleString;
            }

            WriteLine("Initializing experiment...");

            while (await InitializeExperiment(rulesString))
                ;

            WriteLine("Exiting application");
        }



        public static async Task<bool> InitializeExperiment(string rulesString)
        {
            var prevRules = await BenchmarkDatabase.GetGrammarRules();
            if (rulesString != prevRules)
            {
                WriteLine($"Rules have changed. Do you want to update them? [Y/N]");
                if (Console.ReadKey().KeyChar != 'y')
                    return false;
                Console.WriteLine("Updating rules...");
                await BenchmarkDatabase.SetGrammarRules(rulesString);
            }


            bool experimentExists = await BenchmarkDatabase.ExperimentExists();
            var configs = EnumerateBenchmarks(rulesString);
            var configCount = configs.Count();
            var totalCount = await BenchmarkDatabase.CountTotal();
            var queuedCount = await BenchmarkDatabase.CountQueued();
            var lockedCount = await BenchmarkDatabase.CountLocked();
            var faultedCount = await BenchmarkDatabase.CountFaulted();
            var finishedCount = await BenchmarkDatabase.CountFinished();

            Console.WriteLine();
            WriteLine($"Experiment: ",Program.Experiment);
            Console.WriteLine($"Exists: {experimentExists} Total: {totalCount} Queued: {queuedCount} Locked: {lockedCount} Faulted: {faultedCount} Finished: {finishedCount}");
            Console.WriteLine($"Current rules would generate {configCount} configurations");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("[G]enerate configs (Overwrite existing data)");
            Console.WriteLine("[C]ontinue config generation (skip existing configs)");
            Console.WriteLine("[D]elete experiment data");
            Console.WriteLine("[F]aulted removal");
            Console.WriteLine("[L]ock removal");
            Console.WriteLine("[R]esult removal");
            Console.WriteLine("E[x]it");

            char ch = ' ';
            while (!new []{ 'g', 'c','d', 'f', 'l','r', 'x' }.Contains(ch)) 
                ch = Console.ReadKey(true).KeyChar;


            switch (ch)
            {
                case 'g':
                    WriteLine($"Updating database...");
                    int insertedCnt = await BenchmarkDatabase.UpsertConfigs(configs, BatchSize);
                    WriteLine($"{insertedCnt} new items inserted.");
                    return true;
                case 'c':
                    WriteLine($"Updating database...");
                    insertedCnt = await BenchmarkDatabase.UpsertConfigs(configs, BatchSize, totalCount);
                    WriteLine($"{insertedCnt} new items inserted.");
                    return true;
                case 'd': //clear all
                    WriteLine($"Deleting experiment...");
                    await BenchmarkDatabase.ClearExperiment();
                    WriteLine($"Deleted all configurations.");
                    return true;
                case 'f':
                    WriteLine($"Removing faulted...");
                    int faultedResult = await BenchmarkDatabase.ResetFaulted();
                    WriteLine($"Removed {faultedResult} faulted configs.");
                    return true;
                case 'l':
                    WriteLine($"Removing locks...");
                    int lockedResult = await BenchmarkDatabase.RemoveLocks();
                    WriteLine($"Removed {lockedResult} locks.");
                    return true;
                case 'r':
                    WriteLine($"Removing results...");
                    int count = await BenchmarkDatabase.ResetResults();
                    WriteLine($"Removed {count} results.");
                    return true;
                case 'x': //exit
                    return false;
            }
            return false;
        }

        public static void WriteLine(string value, string value2 = null)
        {
            if (string.IsNullOrWhiteSpace(value2))
            {
                Console.WriteLine($"{DateTime.Now}: {value}");
            }
            else
            {
                var oldColor = Console.ForegroundColor;
                Console.Write($"{DateTime.Now}: {value}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(value2);
                Console.ForegroundColor = oldColor;
            }
        }

      

    }
}
