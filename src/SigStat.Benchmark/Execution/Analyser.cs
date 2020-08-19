using OfficeOpenXml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common.Helpers;
using System.Diagnostics;
using SigStat.Benchmark.Helpers;
using SigStat.Benchmark.Execution;
using System.Reflection;

namespace SigStat.Benchmark
{
    public class Analyser
    {
        public class ReportLine
        {
            public string Key { get; set; }
            public double FRR { get; set; }
            public double FAR { get; set; }
            public double AER { get; set; }



            public string Database { get; set; }
            public string Feature { get; set; }
            public string Split { get; set; }
            public string Classifier { get; set; }
            public string Distance { get; set; }
            public string Rotation { get; set; }
            public string FillGap { get; set; }
            public string FilterGap { get; set; }
            public string FillInterpolation { get; set; }
            public string Resampling { get; set; }
            public string SampleCount { get; set; }
            public string ResamplingInterpolation { get; set; }
            public string Scaling { get; set; }
            public string Translation { get; set; }


            public string Date { get; set; }
            public string Agent { get; set; }
            public string Duration { get; set; }

            public string Gap { get; set; }
            public string Pipeline { get; set; }
            public string Benchmark { get; set; }
            public string Verifier { get; set; }

            public List<ClassificationResult> ClassificationResults { get; set; }

        }

        static PropertyInfo[] ReportLineProperties = typeof(ReportLine).GetProperties();
        internal static async Task RunAsync(string reportFilePath)
        {
            Console.WriteLine($"{DateTime.Now}: Gathering results for experiment {Program.Experiment}...");
            var totalCount = await BenchmarkDatabase.CountFinished();

            var reportLine = BenchmarkDatabase.GetResults().First();
            var classificationHeaders = reportLine.ClassificationResults.OrderBy(c => c.J).ThenBy(c => c.K)
                   .SelectMany(c => new[] { $"{c.J}-{c.K} FAR", $"{c.J}-{c.K} FRR", $"{c.J}-{c.K} AER" });
            var headers = ReportLineProperties.Select(p => p.Name).Concat(classificationHeaders).ToArray();

            Console.WriteLine("Generating Excel document");
            using (var fs = File.Create(reportFilePath))
            using (var sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.WriteLine(string.Join(';', headers));
                foreach (var line in EnumerateReportLinesWithProgress(totalCount))
                {
                    sw.WriteLine(string.Join(';', line));
                }
            }
            //using (var p = new ExcelPackage())
            //{
            //    var sheet = p.Workbook.Worksheets.Add("Summary");
            //    sheet.InsertTable(1, 1, EnumerateReportLinesWithProgress(totalCount), headers);
            //    p.SaveAs(new FileInfo(reportFilePath));
            //}
        }

        internal static IEnumerable<IEnumerable<object>> EnumerateReportLinesWithProgress(int totalCount)
        {
            using (var progress = ProgressHelper.StartNew(totalCount, 1))
            {
                foreach (var reportLine in BenchmarkDatabase.GetResults())
                {
                    progress.Value++;
                    yield return GetReportLineValues(reportLine);
                }
            }
        }

        private static IEnumerable<object> GetReportLineValues(ReportLine reportLine)
        {
            foreach (var prop in ReportLineProperties)
            {
                yield return prop.GetValue(reportLine);
            }
            var classificationValues = reportLine.ClassificationResults.OrderBy(c => c.J).ThenBy(c => c.K)
                   .SelectMany(c => new[] { c.Far, c.Frr, c.Aer });
            foreach (var val in classificationValues)
            {
                yield return val;
            }

        }
    }
}
