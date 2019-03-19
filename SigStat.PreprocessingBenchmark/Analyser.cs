using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SigStat.Common.Helpers.Excel;
using System.Collections.Concurrent;

namespace SigStat.PreprocessingBenchmark
{
    class Analyser
    {
        class ReportLine
        {
            public string Key { get; set; }

            public string Classifier { get; set; }
            public string Sampling { get; set; }
            public string Database { get; set; }
            public string Rotation { get; set; }
            public string Translation_Scaling { get; set; }
            public string ResamplingType_Filter { get; set; }
            public string ResamplingParam { get; set; }
            public string Interpolation { get; set; }
            public string Features { get; set; }

            public double FRR { get; set; }
            public double FAR { get; set; }
            public double AER { get; set; }
        }
        internal static async Task RunAsync()
        {
            var reportFiles = Directory.EnumerateFiles(Program.OutputDirectory, "*.xlsx").ToList();

            var reports = new ConcurrentBag<ReportLine>();
            Parallel.ForEach(reportFiles, rf =>
            {
                reports.Add(LoadReportLine(rf));
                Console.WriteLine(reports.Count + "/" + reportFiles.Count);

            });


            
            using (var p = new ExcelPackage())
            {
                var sheet = p.Workbook.Worksheets.Add("Summary");
                sheet.InsertTable(2, 2, reports);
                p.SaveAs(new FileInfo("Report.xlsx"));
            }
        }

        private static ReportLine LoadReportLine(string filename)
        {
            using (var p = new ExcelPackage(new FileInfo(filename)))
            {
                var summary = p.Workbook.Worksheets["Summary"];
                var result =  new ReportLine();
                result.FAR = summary.Cells["I10"].GetValue<double>();
                result.FRR = summary.Cells["I11"].GetValue<double>();
                result.AER = summary.Cells["I12"].GetValue<double>();
                result.Key = Path.GetFileNameWithoutExtension(filename);

                for (int row = 10; row <= 18; row++)
                {
                    string key = summary.Cells[row, 5].GetValue<string>();
                    string value = summary.Cells[row, 6].GetValue<string>();
                    typeof(ReportLine).GetProperty(key).SetValue(result, value);

                }
                return result;
            }
        }
    }
}
