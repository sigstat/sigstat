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

namespace SigStat.Benchmark
{
    class Analyser
    {
        static string InputDirectory;
        static string OutputFile;

        class ReportLine
        {
            public string Key { get; set; }

            public string Date { get; set; }
            public string Agent { get; set; }
            public string Duration { get; set; }


            public string Translation { get; set; }
            public string Scaling { get; set; }
            public string Classifier { get; set; }
            public string Sampling { get; set; }
            public string Database { get; set; }
            public string Rotation { get; set; }
            public string Translation_Scaling { get; set; }
            public string ResamplingType_Filter { get; set; }
            public string ResamplingParam { get; set; }
            public string Interpolation { get; set; }
            public string Features { get; set; }
            public string Distance { get; set; }

            public string Error { get; set; }

            public double FRR { get; set; }
            public double FAR { get; set; }
            public double AER { get; set; }
        }
        internal static async Task RunAsync(string inputDir, string output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            InputDirectory = inputDir;
            OutputFile = output;

            var reportFiles = Directory.EnumerateFiles(InputDirectory, "*.xlsx").ToList();
            reportFiles.Sort();

            var reports = new ConcurrentBag<ReportLine>();
            Parallel.ForEach(reportFiles, new ParallelOptions { MaxDegreeOfParallelism = 16 }, rf =>
            {
                long eta = reports.Count>0? sw.ElapsedMilliseconds * (reportFiles.Count - reports.Count) / reports.Count:0; 
                if (reports.Count % 100 == 0)
                    Console.WriteLine(DateTime.Now + ": " + reports.Count + "/" + reportFiles.Count+" ETA: "+TimeSpan.FromMilliseconds(eta) );
                reports.Add(LoadReportLine(rf));

            });



            using (var p = new ExcelPackage())
            {
                var sheet = p.Workbook.Worksheets.Add("Summary");
                sheet.InsertTable(2, 2, reports);
                p.SaveAs(new FileInfo(OutputFile));
            }
        }

        private static ReportLine LoadReportLine(string filename)
        {
            try
            {
                using (var p = new ExcelPackage(new FileInfo(filename)))
                {
                    var summary = p.Workbook.Worksheets["Summary"];
                    var result = new ReportLine();
                    result.FAR = summary.Cells["I10"].GetValue<double>();
                    result.FRR = summary.Cells["I11"].GetValue<double>();
                    result.AER = summary.Cells["I12"].GetValue<double>();
                    result.Key = Path.GetFileNameWithoutExtension(filename);

                    for (int row = 10; row <= 21; row++)
                    {
                        string key = summary.Cells[row, 5].GetValue<string>();
                        string value = summary.Cells[row, 6].GetValue<string>();
                        typeof(ReportLine).GetProperty(key).SetValue(result, value);

                    }

                    for (int row = 11; row <= 13; row++)
                    {
                        string key = summary.Cells[row, 2].GetValue<string>().Replace(":", "");
                        string value = summary.Cells[row, 3].GetValue<string>();
                        typeof(ReportLine).GetProperty(key).SetValue(result, value);

                    }

                    return result;

                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(filename));
                Console.WriteLine(exc.Message);
                return new ReportLine
                {
                    Key = Path.GetFileNameWithoutExtension(filename),
                    Error = exc.Message
                };
            }
        }
    }
}
