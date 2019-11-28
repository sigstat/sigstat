using SigStat.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    public static class TxtHelper
    {
        public static string BasePath = @"Results";

        public static void Save(string[] lines, string fileName)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter((Path.Combine(BasePath, fileName + ".txt")).GetPath()))
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

        public static string[] ReSamplingResultsToLines(Tuple<List<double>, List<List<double>>> results)
        {
            return ReSamplingResultsToLines(results.Item1, results.Item2);
        }

        public static string[] ReSamplingResultsToLines(List<double> resList, List<List<double>> dataLists)
        {
            int n = resList.Count;
            for (int i = 0; i < dataLists.Count; i++)
            {
                if (dataLists[i].Count != n)
                {
                    throw new Exception();
                }
            }
            string[] res = new string[n];
            for (int i = 0; i < n; i++)
            {
                res[i] += resList[i].ToString();
                res[i] += " ";
                for (int j = 0; j < dataLists.Count; j++)
                { 
                    res[i] += dataLists[j][i].ToString();
                    res[i] += " ";
                }
            }
            return res;
        }
    }
}
