using OfficeOpenXml;
using SigStat.Common.Helpers.Excel.Palette;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace SigStat.Common.Helpers.Excel
{
    public static class ExcelHelper
    {

        //Merge cells in given range
        public static void Merge(this ExcelRangeBase range)
        {
            ExcelCellAddress start = range.Start;
            ExcelCellAddress end = range.End;
            range.Worksheet.Cells[start.Row, start.Column, end.Row, end.Column].Merge = true;
        }

        public static void FormatAsTable(this ExcelRange range, ExcelColor color = ExcelColor.Primary, bool showColumnHeader = true, bool showRowHeader = true)
        {
            var ws = range.Worksheet;
            var palette = PaletteStorage.GetPalette(color);
            range.BorderEverywhere();
            //First fill every cell as Light color from palette
            range.Fill(palette.LightColor);

            //Row header's color is main
            if (showRowHeader)
            {
                //row header starts from first row first column, and ends at first row last column
                var headerRange = ws.Cells[range.Start.Row, range.Start.Column, range.End.Row, range.Start.Column];
                headerRange.Fill(palette.MainColor);
            }

            //Column header's color is  main
            if (showColumnHeader)
            {
                //Column header starts from first row first column, and ends at first row last column
                var headerRange = ws.Cells[range.Start.Row, range.Start.Column, range.Start.Row, range.End.Column];
                headerRange.Fill(palette.MainColor);
            }

        }

        //If the title has no value then dont draw title, returns the first row of the actual table
        public static int FormatAsTableWithTitle(this ExcelRange range, string title, ExcelColor color = ExcelColor.Primary, bool showColumnHeader = true, bool showRowHeader = true)
        {

            var ws = range.Worksheet;
            var palette = PaletteStorage.GetPalette(color);

            ExcelRange tableRange = range;

            var nextRow = range.Start.Row;


            if (title != null)
            {
                //Get the title's cells and format title
                var titleCells = ws.Cells[range.Start.Row, range.Start.Column, range.Start.Row, range.End.Column];
                titleCells.Fill(palette.DarkColor);
                titleCells.Merge();
                titleCells.BorderAround();
                titleCells.Value = title;
                //if there's title then the range should be changed after (pushed down by one row)
                tableRange = ws.Cells[range.Start.Row + 1, range.Start.Column, range.End.Row + 1, range.End.Column];
                nextRow++;
            }

            //Format table under title
            tableRange.FormatAsTable(color, showColumnHeader, showRowHeader);
            return nextRow;
        }

        //Insert table filled with data from a 2D array
        public static void InsertTable(this ExcelWorksheet ws, int x, int y, object[,] data, string title = null, ExcelColor color = ExcelColor.Primary, bool hasRowHeader = true, bool hasColumnHeader = true)
        {
            //Get table's range
            int tableLength = data.GetLength(0);
            int tableHeight = data.GetLength(1);
            var tableRange = ws.Cells[y, x, y + tableHeight - 1, x + tableLength - 1];

            //Format table
            //Get the starting row for inserting data
            int startRow = tableRange.FormatAsTableWithTitle(title, color, hasColumnHeader, hasRowHeader);

            //Fill table with data
            for (int i = 0; i < tableHeight; i++)
            {
                for (int j = 0; j < tableLength; j++)
                {
                    ws.Cells[startRow + i, x + j].Value = data[j, i];
                }
            }

            //Set columns to autofit for better output
            tableRange = ws.Cells[startRow, x, startRow + tableHeight - 1, x + tableLength - 1];
            tableRange.AutoFitColumns();
        }

        //Insert a table filled with data from an IEnumerable
        public static void InsertTable<T>(this ExcelWorksheet ws, int x, int y, IEnumerable<T> data, string title = null, ExcelColor color = ExcelColor.Primary, bool showHeader = true)
        {
            //Get required Type data
            var dataType = data.First().GetType();
            var props = new List<PropertyInfo>(dataType.GetProperties());

            //Get table's range
            //If we show a header the height of the table is 1 bigger
            var tableHeight = data.Count() + (showHeader ? 1 : 0);
            var tableLength = props.Count();
            var tableRange = ws.Cells[y, x, y + tableHeight - 1, x + tableLength - 1];

            //Format table
            //Get the starting row for inserting data
            int startRow = tableRange.FormatAsTableWithTitle(title, color, showHeader, false);

            //Write header
            if (showHeader)
            {
                //If we show header then it contains the name of the properties
                for (int i = 0; i < props.Count(); i++)
                {
                    ws.Cells[startRow, x + i].Value = props[i].Name;
                }
                startRow += 1;
            }

            //temporary object
            Object obj;

            //Insert data
            for (int i = 0; i < data.Count(); i++)
            {
                for (int j = 0; j < props.Count; j++)
                {
                    obj = props[j].GetValue(data.ElementAt(i));
                    ws.Cells[startRow + i, x + j].Value = obj;
                }
            }

            //Set columns to autofit for better output
            tableRange = ws.Cells[startRow, x, startRow + tableHeight - 1, x + tableLength - 1];
            tableRange.AutoFitColumns();
        }

        public static void InsertTable<TKey, TValue>(this ExcelWorksheet ws, int x, int y, IEnumerable<KeyValuePair<TKey, TValue>> data, string title = null, ExcelColor color = ExcelColor.Primary, bool showHeader = false)
        {

            //Get table's range
            //If we show a header the height of the table is 1 bigger
            int tableLength = 2;
            int tableHeight = data.Count() + (showHeader ? 1 : 0);
            var tableRange = ws.Cells[y, x, y + tableHeight - 1, x + tableLength - 1];

            //Format table
            //Get the starting row for inserting data
            int startRow = tableRange.FormatAsTableWithTitle(title, color, showHeader, false);

            //Write header
            if (showHeader)
            {
                ws.Cells[startRow, x].Value = "Key";
                ws.Cells[startRow, x + 1].Value = "Value";
                startRow += 1;
            }

            //Write data
            int i = 0;
            foreach (var value in data)
            {
                ws.Cells[startRow + i, x].Value = value.Key;
                ws.Cells[startRow + i, x + 1].Value = value.Value;
                i++;
            }

            //Set columns to autofit for better output
            tableRange = ws.Cells[startRow, x, startRow + tableHeight - 1, x + tableLength - 1];
            tableRange.AutoFitColumns();

        }

        public static void InsertHierarchicalList(this ExcelWorksheet ws, int x, int y, HierarchyElement root, string title = null, ExcelColor color = ExcelColor.Primary)
        {
            //Get hierarchy debth and the number of items in it
            int depth = root.getDepth();
            int length = root.getCount();

            //Get table range
            ExcelRange range = ws.Cells[y, x, y + length - 1, x + depth - 1];


            //Format
            //int startRow = range.FormatAsTableWithTitle(title, color, false, false);      -- not cool everywhere border

            var palette = PaletteStorage.GetPalette(color);

            var startRow = y;


            if (title != null)
            {
                //Get the title's cells and format title
                var titleCells = ws.Cells[range.Start.Row, range.Start.Column, range.Start.Row, range.End.Column];
                titleCells.Fill(palette.MainColor);
                titleCells.Merge();
                titleCells.BorderAround();
                titleCells.Value = title;
                //if there's title then the range should be changed after (pushed down by one row)
                range = ws.Cells[range.Start.Row + 1, range.Start.Column, range.End.Row + 1, range.End.Column];
                startRow++;
            }

            //Format table under title
            range.BorderAround();
            range.Fill(palette.LightColor);


            ws.PrintHierarchicalList(x, startRow, root);

            //Set columns to a low width
            for (int i = x; i < (x + depth - 1); i++)
            {
                ws.Column(i).Width = 2;
            }
            //set last column to autosize
            ws.Column(x + depth - 1).AutoFit();
        }

        //Prints hierarchical list from x,y position
        public static void PrintHierarchicalList(this ExcelWorksheet ws, int x, int y, HierarchyElement root)
        {
            ws.Cells[y, x].Value = root;
            if (root.Children.Count > 0)
            {
                int startRow = y + 1;
                int startColumn = x + 1;
                foreach (var child in root.Children)
                {
                    ws.PrintHierarchicalList(startColumn, startRow, child);
                    startRow += child.getCount();
                }
            }
        }

        public static void InsertLegend(this ExcelRange range, string text, string title = null, bool hasHeader = false, ExcelColor color = ExcelColor.Info)
        {
            var ws = range.Worksheet;
            var palette = PaletteStorage.GetPalette(color);
            //Set legends text range
            var legendRange = range;

            //If legend has header, format it
            if (hasHeader)
            {
                var titleCells = ws.Cells[range.Start.Row, range.Start.Column, range.Start.Row, range.End.Column];
                titleCells.Merge();
                titleCells.Value = title;
                titleCells.BorderAround();
                titleCells.Fill(palette.MainColor);

                //Alter text's range
                legendRange = ws.Cells[range.Start.Row + 1, range.Start.Column, range.End.Row, range.End.Column];
            }

            //Format legends text
            legendRange.Merge();
            legendRange.Value = text;
            legendRange.BorderAround();
            legendRange.Fill(palette.LightColor);
            legendRange.Style.WrapText = true;
            legendRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            legendRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


        }

        //Creates a link to given sheet
        public static void InsertLink(this ExcelRange range, string sheet)
        {
            range.Hyperlink = new Uri($"#'{sheet}'!A1", UriKind.Relative);
            range.Style.HyperlinkStyle();
        }



        //Creates link to given cells in given sheet
        public static void InsertLink(this ExcelRange range, string sheet, string cells)
        {
            range.Hyperlink = new Uri($"#'{sheet}'!{cells}", UriKind.Relative);
            range.Style.HyperlinkStyle();
        }

        //Set style to resemble a hyperlink
        public static void HyperlinkStyle(this OfficeOpenXml.Style.ExcelStyle style)
        {
            style.Font.UnderLine = true;
            style.Font.Color.SetColor(Color.Blue);
        }


        //Draw all borders in range
        public static void BorderEverywhere(this ExcelRange range, OfficeOpenXml.Style.ExcelBorderStyle style = OfficeOpenXml.Style.ExcelBorderStyle.Thin)
        {
            range.Style.Border.Top.Style = range.Style.Border.Left.Style = range.Style.Border.Bottom.Style = range.Style.Border.Right.Style = style;
        }
        //Draw border around range
        public static void BorderAround(this ExcelRange range, OfficeOpenXml.Style.ExcelBorderStyle style = OfficeOpenXml.Style.ExcelBorderStyle.Thin)
        {
            range.Style.Border.BorderAround(style);
        }

        //Set background color in range
        private static void Fill(this ExcelRange range, Color color)
        {
            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(color);
        }
    }

}
