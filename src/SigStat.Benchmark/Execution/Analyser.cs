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

        }
        internal static async Task RunAsync(string reportFilePath)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Console.WriteLine($"{DateTime.Now}: Gathering results for experiment {Program.Experiment}...");
            var reportLineCount = await BenchmarkDatabase.CountFinished();

            List<ReportLine> reports = new List<ReportLine>();

            foreach (var reportLine in BenchmarkDatabase.GetResults())
            {
                long eta = reports.Count > 0 ? sw.ElapsedMilliseconds * (reportLineCount - reports.Count) / reports.Count : 0;
                if (reports.Count % 100 == 0)
                    Console.WriteLine(DateTime.Now + ": " + reports.Count + "/" + reportLineCount + " ETA: " + TimeSpan.FromMilliseconds(eta));
                reports.Add(reportLine);

            }
            Console.WriteLine("Generating Excel document");
            using (var p = new ExcelPackage())
            {
                var sheet = p.Workbook.Worksheets.Add("Summary");
                sheet.InsertTable(2, 2, reports);
                p.SaveAs(new FileInfo(reportFilePath));
            }
        }

  
    }
}
