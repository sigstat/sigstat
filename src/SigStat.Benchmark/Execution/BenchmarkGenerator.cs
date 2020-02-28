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
        const string ruleString = @"                
                [Benchmark] -> [Database]_[Split]_[Feature]_[Verifier]
                [Database] -> scv2004 | mcyt100 | dutch | svc2004 | chinese | japanese
                [Feature] -> X | Y | P | XY | XYP | XP | YP
                [Split] -> s1 | s2 | s3 | s4
                [Verifier] -> [Pipeline]_[Classifier]
                [Classifier] -> Dtw_[Distance] | OptimalDtw_[Distance]
                [Distance] -> Manhattan | Euclidean
                [Pipeline]-> [Rotation]_[Gap]_[Resampling]_[Translation]_[Scaling]
                [Rotation] -> none, rotation
                [Gap]-> none | filter | fill_[FillInterpolation] | filterandfill_[FillInterpolation]
                [FillInterpolation] -> linear | cubic
                [Resampling] -> none | [SampleCount]samples_[ResamplingInterpolation]
                [SampleCount] -> 50 | 100 | 500 | 1000
                [ResamplingInterpolation] -> linear | cubic
                [Translation] -> X0|Y0|XY0|CogX|CogY|CogXY| none 
                [Scaling] -> none| 01| s";


        public static IEnumerable<string> EnumerateBenchmarks()
        {
            var rules = GrammarEngine.ParseRules(ruleString);
            foreach (var derivedSentence in GrammarEngine.GenerateAllSentences(rules))
            {
                yield return derivedSentence.Sentence;
            }
        }



        internal static async Task RunAsync(string connectionString)
        {
            Console.WriteLine("Generating benchmark configurations...");
            await GenerateBenchmarks();
        }

        internal static async Task GenerateBenchmarks()
        {
            try
            {

                Console.WriteLine("Initializing experiment: " + Program.Experiment);
                await DatabaseHelper.InitializeExperiment(Program.Experiment);

                //if ((Queue.ApproximateMessageCount ?? 0) > 0)
                //{
                //    if (!Console.IsInputRedirected)
                //    {
                //        Console.WriteLine($"There are {Queue.ApproximateMessageCount} jobs pending in the queue. Should I clear them? [Y|N]");
                //        if (Console.ReadKey(true).Key != ConsoleKey.Y)
                //        {
                //            await Queue.ClearAsync();
                //        }
                //    }
                //    else
                //        await Queue.ClearAsync();//clear queue by default
                //}

                var count = EnumerateBenchmarks().Count();
                Console.WriteLine($"Enqueueing {count} combinations...");
                int i = 0;

                //foreach (var configuration in EnumerateBenchmarks())
                //{
                //    if (i % 100 == 0)
                //        Console.WriteLine($"{i}/{count}");
                //    i++;
                //}
                Console.WriteLine("Writing configurations to db...");
                //DatabaseHelper.InsertConfigs(Program.Experiment, );

                Console.WriteLine("Ready.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
