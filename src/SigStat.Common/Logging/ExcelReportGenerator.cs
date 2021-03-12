using OfficeOpenXml;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Logging
{
    /// <summary>
    /// This class is used to generate a report in Excel file format, form a Benchmark model.
    /// </summary>
    static public class ExcelReportGenerator
    {
        /// <summary>
        /// Generates an Excel file that contains the report.
        /// </summary>
        /// <param name="model">The model of the report</param>
        /// <param name="fileName">The name of the generated excel file</param>
        public static void GenerateReport(BenchmarkLogModel model, string fileName = null)
        {
            if (fileName == null)
            {
                var date = DateTime.Now;
                fileName = date.Year.ToString() + '_' + date.Month + '_' + date.Day + '_' + date.Hour + '-' + date.Minute + '-' + date.Second + @"Report.xlsx";
            }


            using (ExcelPackage pkg = new ExcelPackage(new System.IO.FileInfo(fileName)))
            {
                const int graphWidth = 1400;
                const int graphHeight = 700;
                //Write the summary sheet
                ExcelWorksheet summarySheet = pkg.Workbook.Worksheets.Add("Summary");

                summarySheet.Cells["A5:I6"].InsertLegend("Go to http://sigstat.org/PrepocessingBenchmark for details", "Description");

                summarySheet.InsertDictionary(8, 2, model.Excecution.Items, model.Excecution.Name, Helpers.Excel.ExcelColor.Secondary);

                
                summarySheet.InsertDictionary(8, 5, model.Parameters.Items, model.Parameters.Name, Helpers.Excel.ExcelColor.Secondary);

                summarySheet.InsertDictionary(8, 8, model.BenchmarkResults.Items, model.BenchmarkResults.Name, Helpers.Excel.ExcelColor.Warning);

                //Write the results sheet
                ExcelWorksheet resultsSheet = pkg.Workbook.Worksheets.Add("Results");

                List<object> signerResults = new List<object>();

                foreach (var signerResult in model.SignerResults)
                {
                    signerResults.Add(new { Signer = signerResult.Value.SignerID, FAR = signerResult.Value.Far, FRR = signerResult.Value.Frr, AER = signerResult.Value.Aer });
                }

                resultsSheet.InsertTable(2, 2, signerResults, "Signer results", Helpers.Excel.ExcelColor.Primary, true, "SignerResults");
                var signerTable = resultsSheet.Cells[resultsSheet.Names["SignerResults"].Start.Row, resultsSheet.Names["SignerResults"].Start.Column, resultsSheet.Names["SignerResults"].End.Row, resultsSheet.Names["SignerResults"].End.Column];
                resultsSheet.InsertColumnChart(signerTable, 3, 8, "Error rates", "SignerId", "Error rate", resultsSheet.Cells["C3:E3"], graphWidth, graphHeight);

                foreach (var signerResult in model.SignerResults)
                {

                    //Create new sheet for each signer
                    var sheet = pkg.Workbook.Worksheets.Add(signerResult.Key);

                    //Write signers distance matrix
                    sheet.InsertTable(2, 2, signerResult.Value.DistanceMatrix.ToArray(), "Distance matrix", Helpers.Excel.ExcelColor.Info, true, true);

                }


                pkg.Save();
            }
        }
    }
}
