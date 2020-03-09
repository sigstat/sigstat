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
        //Example ruleString
        /*const string ruleString = @"                
                [Benchmark] -> [Database]_[Split]_[Feature]_[Verifier]
                [Database] -> *svc2004 | mcyt100 | dutch | chinese | japanese
                [Feature] -> *X | Y | P | XY | XYP | XP | YP
                [Split] -> *s1 | s2 | s3 | s4
                [Verifier] -> [Pipeline]_[Classifier]
                [Classifier] -> *Dtw_[Distance] | OptimalDtw_[Distance]
                [Distance] -> *Manhattan | Euclidean
                [Pipeline] -> [Rotation]_[Gap]_[Resampling]_[Scaling]_[Translation]
                [Rotation] -> *none | rotation
                [Gap] -> [FilterGap]_[FillGap]
                [FilterGap] -> none | filter 
                [FillGap] -> none | fill_[FillInterpolation]
                [FillInterpolation] -> linear | cubic
                [Resampling] -> none | [SampleCount]samples_[ResamplingInterpolation]
                [SampleCount] -> 50 | 100 | 500 | 1000
                [ResamplingInterpolation] -> linear | cubic
                [Scaling] -> none| scale1| scaleS
                [Translation] -> X0|Y0|XY0|CogX|CogY|CogXY| none";*/


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
        internal static async Task RunAsync()
        {
            try
            {
                //Using the 'rules' db collection to load rules for current experiment
                Console.WriteLine($"{DateTime.Now}: Loading rules for experiment {Program.Experiment}...");
                string ruleString = await BenchmarkDatabase.GetGrammarRules();

                Console.WriteLine($"{DateTime.Now}: Initializing experiment...");
                bool success = await BenchmarkDatabase.InitializeExperiment();
                if (!success)
                    return;

                Console.WriteLine($"{DateTime.Now}: Generating combinations...");
                var configs = EnumerateBenchmarks(ruleString);

                Console.WriteLine($"{DateTime.Now}: Writing {configs.Count()} items to database...");

                await BenchmarkDatabase.InsertConfigs(configs);

                Console.WriteLine($"{DateTime.Now}: Ready.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
