using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    public static class TxtHelper
    {
        public static string BasePath = @"Results";

        public static void Save(string[] lines, string fileName)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter((BasePath + '\\' + fileName + ".txt").GetPath()))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }

        public static string[] BenchmarkResToLines(BenchmarkResults results)
        {
            var res = new List<string>();
            foreach (var result in results.SignerResults)
            {
                var newLine = result.Signer + " " + result.Frr.ToString() + " " +
                                result.Far.ToString() + " " + result.Aer.ToString();
                res.Add(newLine);
            }
            var finalResult = results.FinalResult;
            var finalLine = "Avg " + finalResult.Frr + " " + finalResult.Frr + " " + finalResult.Aer;
            res.Add(finalLine);
            return res.ToArray();
        }

        public static string[] TuplesToLines(List<Tuple<string, string, double>> tuples)
        {
            var res = new List<string>();
            foreach (var tuple in tuples)
            {
                var newLine = tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3.ToString();
                res.Add(newLine);
            }
            return res.ToArray();
        }

        public static string[] ArrayToLines(double[,] dists)
        {
            var res = new List<string>();
            for (int i = 0; i < dists.GetLength(0); i++)
            {
                string newLine = "";
                for (int j = 0; j < dists.GetLength(1); j++)
                {
                    newLine =  newLine + dists[i, j].ToString() + " ";
                }
                res.Add(newLine);
            }
            return res.ToArray();
        }
    }
}
