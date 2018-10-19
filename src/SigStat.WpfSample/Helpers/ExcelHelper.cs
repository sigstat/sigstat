using OfficeOpenXml;
using OfficeOpenXml.Style;
using SigStat.Common.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Helpers
{
    public class ExcelHelper
    {
        public static ExcelWorksheet GetWorkSheetFromPackage(ExcelPackage package, string wsName)
        {
            ExcelWorksheet ws = package.Workbook.Worksheets[wsName];
            if (ws == null)
            {
                ws = package.Workbook.Worksheets.Add(wsName);
            }
            else
            {
                package.Workbook.Worksheets.Delete(ws);
                //package.Save();
                ws = package.Workbook.Worksheets.Add(wsName);
            }
            return ws;
        }



        #region Automatic classification
        public static void SetAutomaticClassificationHeader(ExcelWorksheet ws)
        {
            ws.Cells["A1"].Value = "SignerIndex";
            ws.Cells["B1"].Value = "FARByX";
            ws.Cells["C1"].Value = "FRRByX";
            ws.Cells["D1"].Value = "AERByX";
            ws.Cells["E1"].Value = "FARByY";
            ws.Cells["F1"].Value = "FRRByY";
            ws.Cells["G1"].Value = "AERByY";
            ws.Cells["H1"].Value = "FARByXY";
            ws.Cells["I1"].Value = "FRRByXY";
            ws.Cells["J1"].Value = "AERByXY";
            ws.Cells["A1:J1"].Style.Font.Bold = true;
            ws.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:J1"].AutoFitColumns();
        }

        public static void SetAutomaticClassificationRow(ExcelWorksheet ws, int signerIndex,
            double farX, double farY, double farXY,
            double frrX, double frrY, double frrXY,
            double aerX, double aerY, double aerXY)
        {
            int lineIndex = signerIndex + 1;
            ws.Cells["A" + lineIndex].Value = signerIndex;
            ws.Cells["B" + lineIndex].Value = farX;
            ws.Cells["C" + lineIndex].Value = frrX;
            ws.Cells["D" + lineIndex].Value = aerX;
            ws.Cells["E" + lineIndex].Value = farY;
            ws.Cells["F" + lineIndex].Value = frrY;
            ws.Cells["G" + lineIndex].Value = aerY;
            ws.Cells["H" + lineIndex].Value = farXY;
            ws.Cells["I" + lineIndex].Value = frrXY;
            ws.Cells["J" + lineIndex].Value = aerXY;

            ws.Cells["A" + lineIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        public static void SetAutomaticClassificationSummary(ExcelWorksheet ws, int lineIndex)
        {
            ws.Cells["A" + lineIndex].Value = "Average";
            ws.Cells["B" + lineIndex].Formula = "AVERAGE(B2:B" + (lineIndex - 1) + ")";
            ws.Cells["C" + lineIndex].Formula = "AVERAGE(C2:C" + (lineIndex - 1) + ")";
            ws.Cells["D" + lineIndex].Formula = "AVERAGE(D2:D" + (lineIndex - 1) + ")";
            ws.Cells["E" + lineIndex].Formula = "AVERAGE(E2:E" + (lineIndex - 1) + ")";
            ws.Cells["F" + lineIndex].Formula = "AVERAGE(F2:F" + (lineIndex - 1) + ")";
            ws.Cells["G" + lineIndex].Formula = "AVERAGE(G2:G" + (lineIndex - 1) + ")";
            ws.Cells["H" + lineIndex].Formula = "AVERAGE(H2:H" + (lineIndex - 1) + ")";
            ws.Cells["I" + lineIndex].Formula = "AVERAGE(I2:I" + (lineIndex - 1) + ")";
            ws.Cells["J" + lineIndex].Formula = "AVERAGE(J2:J" + (lineIndex - 1) + ")";

            ws.Cells["A1:J1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            ws.Cells["A" + lineIndex + ":J" + lineIndex].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            ws.Cells["A1:A" + lineIndex].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            ws.Cells["D1:D" + lineIndex].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            ws.Cells["G1:G" + lineIndex].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            ws.Cells["A" + lineIndex].Style.Font.Bold = true;
            ws.Cells["A" + lineIndex].Style.Font.Italic = true;
            ws.Cells["A" + lineIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }
        #endregion

        #region Fused score automatic classification
        public static void SetFusedScoreAutomaticClassificationHeader(ExcelWorksheet ws)
        {
            ws.Cells["A1"].Value = "SignerIndex";
            ws.Cells["B1"].Value = "FARWithDTW";
            ws.Cells["C1"].Value = "FRRWithDTW";
            ws.Cells["D1"].Value = "AERWithDTW";
            ws.Cells["E1"].Value = "FARWithFusedScore";
            ws.Cells["F1"].Value = "FRRWithFusedScore";
            ws.Cells["G1"].Value = "AERWithFusedScore";
            ws.Cells["A1:G1"].Style.Font.Bold = true;
            ws.Cells["A1:G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:G1"].AutoFitColumns();
        }

        public static void SetFusedScoreAutomaticClassificationRow(ExcelWorksheet ws, int signerIndex,
            double farDTW, double farFusedScore,
            double frrDTW, double frrFusedScore,
            double aerDTW, double aerFusedScore)
        {
            int lineIndex = signerIndex + 1;
            ws.Cells["A" + lineIndex].Value = signerIndex;
            ws.Cells["B" + lineIndex].Value = farDTW;
            ws.Cells["C" + lineIndex].Value = frrDTW;
            ws.Cells["D" + lineIndex].Value = aerDTW;
            ws.Cells["E" + lineIndex].Value = farFusedScore;
            ws.Cells["F" + lineIndex].Value = frrFusedScore;
            ws.Cells["G" + lineIndex].Value = aerFusedScore;

            ws.Cells["A" + lineIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        public static void SetFusedScoreAutomaticClassificationSummary(ExcelWorksheet ws, int lineIndex)
        {
            ws.Cells["A" + lineIndex].Value = "Average";
            ws.Cells["B" + lineIndex].Formula = "AVERAGE(B2:B" + (lineIndex - 1) + ")";
            ws.Cells["C" + lineIndex].Formula = "AVERAGE(C2:C" + (lineIndex - 1) + ")";
            ws.Cells["D" + lineIndex].Formula = "AVERAGE(D2:D" + (lineIndex - 1) + ")";
            ws.Cells["E" + lineIndex].Formula = "AVERAGE(E2:E" + (lineIndex - 1) + ")";
            ws.Cells["F" + lineIndex].Formula = "AVERAGE(F2:F" + (lineIndex - 1) + ")";
            ws.Cells["G" + lineIndex].Formula = "AVERAGE(G2:G" + (lineIndex - 1) + ")";


            ws.Cells["A1:G1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            ws.Cells["A" + lineIndex + ":G" + lineIndex].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            ws.Cells["A" + lineIndex + ":G" + lineIndex].Style.Numberformat.Format = "0.0000%";
            ws.Cells["A1:A" + lineIndex].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            ws.Cells["D1:D" + lineIndex].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            ws.Cells["A" + lineIndex].Style.Font.Bold = true;
            ws.Cells["A" + lineIndex].Style.Font.Italic = true;
            ws.Cells["A" + lineIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //ws.View.FreezePanes(1, 0);
        }

        internal static void WriteTable(ExcelWorksheet ws, int startRow, int startCol, object[,] table)
        {
            int height = table.GetLength(0);
            int width = table.GetLength(1);

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    ws.Cells[startRow + row, startCol + col].Value = table[row, col];
                }
            }
        }

        internal static void WriteTable(ExcelWorksheet ws, int startRow, int startCol, IEnumerable items)
        {
            var properties = items.Cast<object>().First().GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                ws.Cells[startRow, startCol + i].Value = properties[i].Name;
            }

            int row = 1;
            foreach (var item in items)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    ws.Cells[startRow+row, startCol + i].Value = properties[i].GetValue(item);
                }
                row++;
            }
        }
        #endregion

        #region Fused score automatic calssification threshold test
        public static void SetFusedScoreAutomaticClassificationThresholdTestHeader(ExcelWorksheet ws)
        {
            ws.Cells["A1"].Value = "SignerIndex";
            ws.Cells["B1"].Value = "FARWithFusedScore";
            ws.Cells["C1"].Value = "FRRWithFusedScore";
            ws.Cells["D1"].Value = "AERWithFusedScore";
            ws.Cells["A1:D1"].Style.Font.Bold = true;
            ws.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:D1"].AutoFitColumns();
        }

        public static void SetFusedScoreAutomaticClassificationThresholdTestRow(ExcelWorksheet ws, ThresholdResult result)
        {
            int signerIndex = Convert.ToInt32(result.Signer);
            int lineIndex = signerIndex + 1;
            ws.Cells["A" + lineIndex].Value = signerIndex;
            ws.Cells["B" + lineIndex].Value = result.Far;
            ws.Cells["C" + lineIndex].Value = result.Frr;
            ws.Cells["D" + lineIndex].Value = result.Aer;

            ws.Cells["A" + lineIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        public static void SetFusedScoreAutomaticClassificationThresholdTestSummary(ExcelWorksheet ws, int lineIndex)
        {
            ws.Cells["A" + lineIndex].Value = "Average";
            ws.Cells["B" + lineIndex].Formula = "AVERAGE(B2:B" + (lineIndex - 1) + ")";
            ws.Cells["C" + lineIndex].Formula = "AVERAGE(C2:C" + (lineIndex - 1) + ")";
            ws.Cells["D" + lineIndex].Formula = "AVERAGE(D2:D" + (lineIndex - 1) + ")";


            ws.Cells["A1:D1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            ws.Cells["A" + lineIndex + ":D" + lineIndex].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            ws.Cells["A" + lineIndex + ":D" + lineIndex].Style.Numberformat.Format = "0.0000%";
            ws.Cells["A1:A" + lineIndex].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            ws.Cells["A" + lineIndex].Style.Font.Bold = true;
            ws.Cells["A" + lineIndex].Style.Font.Italic = true;
            ws.Cells["A" + lineIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //ws.View.FreezePanes(1, 0);
        }

        public static void SetFusedScoreAutoClassThresholdTestSummarySheetHeader(ExcelWorksheet ws)
        {
            ws.Cells["A1"].Value = "FeatureFilter";
            ws.Cells["B1"].Value = "FARWithFusedScore";
            ws.Cells["C1"].Value = "FRRWithFusedScore";
            ws.Cells["D1"].Value = "AERWithFusedScore";
            ws.Cells["A1:D1"].Style.Font.Bold = true;
            ws.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:D1"].AutoFitColumns();
        }

        public static void SetFusedScoreAutoClassThresholdTestSummarySheetRow(ExcelWorksheet ws, string refWsName, int lineIndex, int refLineIndex)
        {
            ws.Cells["A" + lineIndex].Value = refWsName;
            ws.Cells["B" + lineIndex].Value = "=" + refWsName + "!B" + refLineIndex;
            ws.Cells["C" + lineIndex].Value = "=" + refWsName + "!C" + refLineIndex;
            ws.Cells["D" + lineIndex].Value = "=" + refWsName + "!D" + refLineIndex;
        }
        #endregion

        #region Debug table
        public static void SetDebugTableHeader(ExcelWorksheet ws)
        {
            ws.Cells["A1"].Value = "Signature";
            ws.Cells["L1"].Value = "Average";
            ws.Cells["M1"].Value = "Status";
            ws.Cells["N1"].Value = "Minimum";
            ws.Cells["O1"].Value = "Status";
            ws.Cells["A1:O1"].Style.Font.Bold = true;
            ws.Cells["A1:O1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:O1"].AutoFitColumns();

            ws.Cells["Q1"].Value = "Threshold";
            ws.Cells["R1"].Value = "FAR";
            ws.Cells["S1"].Value = "FRR";
            ws.Cells["T1"].Value = "AER";
            ws.Cells["Q1:T1"].Style.Font.Bold = true;
            ws.Cells["Q1:T1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["Q1:T1"].AutoFitColumns();
            ws.Cells["R2:T2"].Style.Numberformat.Format = "0.0000%";

            ws.Cells["Q5"].Value = "Avg dist";
            ws.Cells["Q6"].Value = "Min dist";
            ws.Cells["R5"].Formula = "COUNTIF(M22:M31,\"Accepted\")/10";
            ws.Cells["S5"].Formula = "COUNTIF(M12:M21, \"Rejected\")/ 10";
            ws.Cells["T5"].Formula = "(R5+S5)/2";
            ws.Cells["R6"].Formula = "COUNTIF(O22:O31,\"Accepted\")/10";
            ws.Cells["S6"].Formula = "COUNTIF(O12:O21,\"Rejected\")/10";
            ws.Cells["T6"].Formula = "(R6+S6)/2";
            ws.Cells["R5:T5"].Style.Numberformat.Format = "0.0000%";
            ws.Cells["R6:T6"].Style.Numberformat.Format = "0.0000%";

            for (int i = 0; i < 10; i++)
            {
                char column = Convert.ToChar('A' + i + 1);
                ws.Cells[column + "1"].Value = "Test" + (i + 1);
            }

            for (int i = 0; i < 30; i++)
            {
                if (i < 10)
                    ws.Cells["A" + (i + 2)].Value = "Test" + (i % 10 + 1);
                else if (i < 20)
                {
                    ws.Cells["A" + (i + 2)].Value = "Original" + (i % 10 + 1);
                    ws.Cells["L" + (i + 2)].Formula = "AVERAGE(B" + (i + 2) + ":K" + (i + 2) + ")";
                    ws.Cells["N" + (i + 2)].Formula = "MIN(B" + (i + 2) + ":K" + (i + 2) + ")";
                    ws.Cells["M" + (i + 2)].Formula = @"IF(L" + (i + 2) + "<=$Q$2,\"Accepted\",\"Rejected\")";
                    ws.Cells["O" + (i + 2)].Formula = @"IF(N" + (i + 2) + "<=$Q$2,\"Accepted\",\"Rejected\")";
                }
                else
                {
                    ws.Cells["A" + (i + 2)].Value = "Forgery" + (i % 10 + 1);
                    ws.Cells["L" + (i + 2)].Formula = "AVERAGE(B" + (i + 2) + ":K" + (i + 2) + ")";
                    ws.Cells["N" + (i + 2)].Formula = "MIN(B" + (i + 2) + ":K" + (i + 2) + ")";
                    ws.Cells["M" + (i + 2)].Formula = @"IF(L" + (i + 2) + "<=$Q$2,\"Accepted\",\"Rejected\")";
                    ws.Cells["O" + (i + 2)].Formula = @"IF(N" + (i + 2) + "<=$Q$2,\"Accepted\",\"Rejected\")";
                }
            }

            ws.Cells["A2:A31"].Style.Font.Bold = true;
            ws.Cells["A2:A31"].AutoFitColumns();

            ws.Cells["B2:K31"].Style.Numberformat.Format = "#,##0.000000";
        }

        public static void SetDebugTableDistanceValueCell(ExcelWorksheet ws, int colSignatureNumber, int rowSignatureNumber, double distValue)
        {
            char column = Convert.ToChar('A' + colSignatureNumber);
            int row = rowSignatureNumber + 1;
            ws.Cells[column + row.ToString()].Value = distValue;
        }

        public static void SetDebugTableStatusValueCell(ExcelWorksheet ws, int rowSignatureNumber, bool statusValue)
        {
            int row = rowSignatureNumber + 1;
            ws.Cells["M" + row].Value = statusValue ? "Accepted" : "Rejected";
        }

        public static void SetDebugTableResults(ExcelWorksheet ws, double threshold, double far, double frr, double aer)
        {
            ws.Cells["Q2"].Value = threshold;
            ws.Cells["R2"].Value = far;
            ws.Cells["S2"].Value = frr;
            ws.Cells["T2"].Value = aer;
        }

        public static void SetDebugTableSummarySheetHeader(ExcelWorksheet ws)
        {
            ws.Cells["A1"].Value = "Signer";
            ws.Cells["B1"].Value = "FAR";
            ws.Cells["C1"].Value = "FRR";
            ws.Cells["D1"].Value = "AER";
            ws.Cells["E1"].Value = "FAR (avg dist)";
            ws.Cells["F1"].Value = "FRR (avg dist)";
            ws.Cells["G1"].Value = "AER (avg dist)";
            ws.Cells["H1"].Value = "FAR (min dist)";
            ws.Cells["I1"].Value = "FRR (min dist)";
            ws.Cells["J1"].Value = "AER (min dist)";

            ws.Cells["A1:J1"].Style.Font.Bold = true;
            ws.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:J1"].AutoFitColumns();
            ws.Cells["A1:J1"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

            ws.Cells["A42"].Value = "Average";
            ws.Cells["A42:J42"].Style.Font.Italic = true;
            ws.Cells["B42:J42"].Style.Numberformat.Format = "0.0000%";
            ws.Cells["A42:J42"].Style.Border.Top.Style = ExcelBorderStyle.Medium;

            for (char c = 'B'; c < 'K'; c++)
            {
                ws.Cells[c + "42"].Formula = "AVERAGE(" + c + "2:" + c + "41)";
            }

            ws.Cells["A1:A42"].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            ws.Cells["D1:D42"].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            ws.Cells["G1:G42"].Style.Border.Right.Style = ExcelBorderStyle.Medium;
        }

        public static void SetDebugTableSummarySheetRow(ExcelWorksheet ws, string refWsName, int lineIndex)
        {
            ws.Cells["A" + lineIndex].Value = refWsName;
            ws.Cells["B" + lineIndex].Formula = "'" + refWsName + "'!R2";
            ws.Cells["C" + lineIndex].Formula = "'" + refWsName + "'!S2";
            ws.Cells["D" + lineIndex].Formula = "'" + refWsName + "'!T2";
            ws.Cells["E" + lineIndex].Formula = "'" + refWsName + "'!R5";
            ws.Cells["F" + lineIndex].Formula = "'" + refWsName + "'!S5";
            ws.Cells["G" + lineIndex].Formula = "'" + refWsName + "'!T5";
            ws.Cells["H" + lineIndex].Formula = "'" + refWsName + "'!R6";
            ws.Cells["I" + lineIndex].Formula = "'" + refWsName + "'!S6";
            ws.Cells["J" + lineIndex].Formula = "'" + refWsName + "'!T6";

            ws.Cells["A" + lineIndex + ":J" + lineIndex].Style.Numberformat.Format = "0.0000%";

        }


        public static void SetOptiClassDebugTableHeader(ExcelWorksheet ws)
        {
            ws.Cells["A1"].Value = "Signature";
            ws.Cells["V1"].Value = "Average";
            ws.Cells["W1"].Value = "Status";
            ws.Cells["X1"].Value = "Minimum";
            ws.Cells["Y1"].Value = "Status";
            ws.Cells["A1:Y1"].Style.Font.Bold = true;
            ws.Cells["A1:Y1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:Y1"].AutoFitColumns();

            ws.Cells["AA1"].Value = "Threshold";
            ws.Cells["AB1"].Value = "FAR";
            ws.Cells["AC1"].Value = "FRR";
            ws.Cells["AD1"].Value = "AER";
            ws.Cells["AA1:AD1"].Style.Font.Bold = true;
            ws.Cells["AA1:AD1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["AA1:AD1"].AutoFitColumns();
            ws.Cells["AB2:AD2"].Style.Numberformat.Format = "0.0000%";

            ws.Cells["AA5"].Value = "Avg dist";
            ws.Cells["AA6"].Value = "Min dist";
            ws.Cells["AB5"].Formula = "COUNTIF(W22:W41,\"Accepted\")/20";
            ws.Cells["AC5"].Formula = "COUNTIF(W2:W21, \"Rejected\")/ 20";
            ws.Cells["AD5"].Formula = "(AB5+AC5)/2";
            ws.Cells["AB6"].Formula = "COUNTIF(Y22:Y41,\"Accepted\")/20";
            ws.Cells["AC6"].Formula = "COUNTIF(Y2:Y21,\"Rejected\")/20";
            ws.Cells["AD6"].Formula = "(AB6+AC6)/2";
            ws.Cells["AB5:AD5"].Style.Numberformat.Format = "0.0000%";
            ws.Cells["AB6:AD6"].Style.Numberformat.Format = "0.0000%";

            for (int i = 0; i < 20; i++)
            {
                char column = Convert.ToChar('A' + i + 1);
                ws.Cells[column + "1"].Value = "Original" + (i + 1);
            }

            for (int i = 0; i < 40; i++)
            {
                if (i < 20)
                {
                    char column = Convert.ToChar('A' + i + 1);
                    if (column == 'B')
                    {
                        ws.Cells["V" + (i + 2)].Formula = "AVERAGE(C" + (i + 2) + ":U" + (i + 2) + ")";
                        ws.Cells["X" + (i + 2)].Formula = "MIN(C" + (i + 2) + ":U" + (i + 2) + ")";
                    }
                    else if (column == 'U')
                    {
                        ws.Cells["V" + (i + 2)].Formula = "AVERAGE(B" + (i + 2) + ":T" + (i + 2) + ")";
                        ws.Cells["X" + (i + 2)].Formula = "MIN(B" + (i + 2) + ":T" + (i + 2) + ")";
                    }
                    else
                    {
                        ws.Cells["V" + (i + 2)].Formula = "AVERAGE(B" + (i + 2) + ":" + (char)(column - 1) + (i + 2) + "," + (char)(column + 1) + (i + 2) + ":U" + (i + 2) + ")";
                        ws.Cells["X" + (i + 2)].Formula = "MIN(B" + (i + 2) + ":" + (char)(column - 1) + (i + 2) + "," + (char)(column + 1) + (i + 2) + ":U" + (i + 2) + ")";
                    }

                    ws.Cells["A" + (i + 2)].Value = "Original" + i;
                    ws.Cells["W" + (i + 2)].Formula = @"IF(V" + (i + 2) + "<=$AA$2,\"Accepted\",\"Rejected\")";
                    ws.Cells["Y" + (i + 2)].Formula = @"IF(X" + (i + 2) + "<=$AA$2,\"Accepted\",\"Rejected\")";
                }
                else
                {
                    ws.Cells["A" + (i + 2)].Value = "Forgery" + (i % 20 + 1);
                    ws.Cells["V" + (i + 2)].Formula = "AVERAGE(B" + (i + 2) + ":U" + (i + 2) + ")";
                    ws.Cells["X" + (i + 2)].Formula = "MIN(B" + (i + 2) + ":U" + (i + 2) + ")";
                    ws.Cells["W" + (i + 2)].Formula = @"IF(V" + (i + 2) + "<=$AA$2,\"Accepted\",\"Rejected\")";
                    ws.Cells["Y" + (i + 2)].Formula = @"IF(X" + (i + 2) + "<=$AA$2,\"Accepted\",\"Rejected\")";
                }
            }

            ws.Cells["A2:A41"].Style.Font.Bold = true;
            ws.Cells["A2:A41"].AutoFitColumns();

            ws.Cells["B2:U41"].Style.Numberformat.Format = "#,##0.000000";
            ws.Cells["A1:A41"].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            ws.Cells["U1:U41"].Style.Border.Right.Style = ExcelBorderStyle.Medium;

        }

        public static void SetOptiClassDebugTableResults(ExcelWorksheet ws, double threshold, double far, double frr, double aer)
        {
            ws.Cells["AA2"].Value = threshold;
            ws.Cells["AB2"].Value = far;
            ws.Cells["AC2"].Value = frr;
            ws.Cells["AD2"].Value = aer;
        }

        public static void SetOptiClassDebugTableSummarySheetRow(ExcelWorksheet ws, string refWsName, int lineIndex)
        {
            ws.Cells["A" + lineIndex].Value = refWsName;
            ws.Cells["B" + lineIndex].Formula = "'" + refWsName + "'!AB2";
            ws.Cells["C" + lineIndex].Formula = "'" + refWsName + "'!AC2";
            ws.Cells["D" + lineIndex].Formula = "'" + refWsName + "'!AD2";
            ws.Cells["E" + lineIndex].Formula = "'" + refWsName + "'!AB5";
            ws.Cells["F" + lineIndex].Formula = "'" + refWsName + "'!AC5";
            ws.Cells["G" + lineIndex].Formula = "'" + refWsName + "'!AD5";
            ws.Cells["H" + lineIndex].Formula = "'" + refWsName + "'!AB6";
            ws.Cells["I" + lineIndex].Formula = "'" + refWsName + "'!AC6";
            ws.Cells["J" + lineIndex].Formula = "'" + refWsName + "'!AD6";

            ws.Cells["A" + lineIndex + ":J" + lineIndex].Style.Numberformat.Format = "0.0000%";

        }
        #endregion

        #region General functions to set table
        public static void SetBenchmarkresultOfClassificationHeader(ExcelWorksheet ws)
        {
            ws.Cells["A1"].Value = "Signer";
            ws.Cells["B1"].Value = "AvgFAR";
            ws.Cells["C1"].Value = "AvgFRR";
            ws.Cells["D1"].Value = "AvgAER";

            ws.Cells["A1:D1"].Style.Font.Bold = true;
            ws.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:D1"].AutoFitColumns();
            ws.Cells["A1:D1"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
        }

        public static void SetBenchmarkresultOfClassificationRow(ExcelWorksheet ws, Result result)
        {
            int lineindex = int.Parse(result.Signer) + 1;
            ws.Cells["A" + lineindex].Value = result.Signer;
            ws.Cells["B" + lineindex].Value = result.Far;
            ws.Cells["C" + lineindex].Value = result.Frr;
            ws.Cells["D" + lineindex].Value = result.Aer;

            ws.Cells["A" + lineindex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["B" + lineindex + ":D" + lineindex].Style.Numberformat.Format = "0.0000%";

        }

        public static void SetBenchmarkresultOfClassificationSummaryRow(ExcelWorksheet ws, Result finalResult, int lineindex)
        {
            ws.Cells["A" + lineindex].Value = "Average";
            ws.Cells["B" + lineindex].Value = finalResult.Far;
            ws.Cells["C" + lineindex].Value = finalResult.Frr;
            ws.Cells["D" + lineindex].Value = finalResult.Aer;

            ws.Cells["A" + lineindex].Style.Font.Italic = true;
            ws.Cells["B" + lineindex + ":D" + lineindex].Style.Font.Bold = true;
            ws.Cells["B" + lineindex + ":D" + lineindex].Style.Numberformat.Format = "0.0000%";
            ws.Cells["A" + lineindex + ":D" + lineindex].AutoFitColumns();
            ws.Cells["A" + lineindex + ":D" + lineindex].Style.Border.Top.Style = ExcelBorderStyle.Medium;
        }

        #endregion

    }
}
