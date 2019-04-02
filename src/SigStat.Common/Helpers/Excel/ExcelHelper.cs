using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using SigStat.Common.Helpers.Excel.Palette;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using SigStat.Common.Helpers.Excel.Level;

namespace SigStat.Common.Helpers.Excel
{
    static class CellHandler
    {


        /// <summary>
        /// Merge all cells into one in the range.
        /// </summary>
        /// <param name="range">Cells to merge</param>
        public static void Merge(this ExcelRangeBase range)
        {
            ExcelCellAddress start = range.Start;
            ExcelCellAddress end = range.End;
            range.Worksheet.Cells[start.Row, start.Column, end.Row, end.Column].Merge = true;
        }

        /// <summary>
        /// Format cells in the range into a table
        /// </summary>
        /// <param name="range">The table's cells</param>
        /// <param name="color">Color palette of the table</param>
        /// <param name="showColumnHeader">Defines if the table has column header</param>
        /// <param name="showRowHeader">Defines if the table has row header</param>
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

        /// <summary>
        /// Format cells in the range into a table with possible title
        /// </summary>
        /// <param name="range">The table's cells</param>
        /// <param name="title">The table's title, if null, the table won't have title</param>
        /// <param name="color">Color palette of the table</param>
        /// <param name="showColumnHeader">Defines if the table has column header</param>
        /// <param name="showRowHeader">Defines if the table has row header</param>
        /// <returns></returns>
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

        /// <summary>
        /// Insert table filled with data from a 2D array
        /// </summary>
        /// <param name="ws">Worksheet in wich the table is created</param>
        /// <param name="col">Starting column of the table</param>
        /// <param name="row">Starting row of the table</param>
        /// <param name="data">2D array in wich the data to insert is stored</param>
        /// <param name="title">The table's title</param>
        /// <param name="color">The table's color</param>
        /// <param name="hasRowHeader">Defines if the table has row header</param>
        /// <param name="hasColumnHeader">Defines if the table has column header</param>
        /// <param name="name">If given, creates a named range, with this name</param>
        /// /// <returns>Range of the inserted data</returns>
        public static ExcelRange InsertTable(this ExcelWorksheet ws, int row, int col, object[,] data, string title = null, ExcelColor color = ExcelColor.Primary, bool hasRowHeader = true, bool hasColumnHeader = true, string name = null)
        {
            //Get table's range
            int tableLength = data.GetLength(0);
            int tableHeight = data.GetLength(1);
            var tableRange = ws.Cells[row, col, row + tableHeight - 1, col + tableLength - 1];

            //Format table
            //Get the starting row for inserting data
            int startRow = tableRange.FormatAsTableWithTitle(title, color, hasColumnHeader, hasRowHeader);

            //Fill table with data
            for (int i = 0; i < tableHeight; i++)
            {
                for (int j = 0; j < tableLength; j++)
                {
                    ws.Cells[startRow + i, col + j].Value = data[j, i];
                }
            }

            //Set columns to autofit for better output
            tableRange = ws.Cells[startRow, col, startRow + tableHeight - 1, col + tableLength - 1];
            tableRange.AutoFitColumns();

            //Create NamedRange
            if (name != null)
                ws.Names.Add(name, tableRange);

            return tableRange;
        }

        /// <summary>
        /// Insert table filled with data from a 2D array
        /// </summary>
        /// <param name="ws">Worksheet in wich the table is created</param>
        /// <param name="col">Starting column of the table</param>
        /// <param name="row">Starting row of the table</param>
        /// <param name="data">2D array in wich the data to insert is stored (double values)</param>
        /// <param name="title">The table's title</param>
        /// <param name="color">The table's color</param>
        /// <param name="hasRowHeader">Defines if the table has row header</param>
        /// <param name="hasColumnHeader">Defines if the table has column header</param>
        /// /// <param name="name">If given, creates a named range, with this name</param>
        /// <returns>Range of the inserted data</returns>
        public static ExcelRange InsertTable(this ExcelWorksheet ws, int row, int col, double[,] data, string title = null, ExcelColor color = ExcelColor.Primary, bool hasRowHeader = true, bool hasColumnHeader = true, string name = null)
        {
            //Get table's range
            int tableLength = data.GetLength(0);
            int tableHeight = data.GetLength(1);
            var tableRange = ws.Cells[row, col, row + tableHeight - 1, col + tableLength - 1];

            //Format table
            //Get the starting row for inserting data
            int startRow = tableRange.FormatAsTableWithTitle(title, color, hasColumnHeader, hasRowHeader);

            //Fill table with data
            for (int i = 0; i < tableHeight; i++)
            {
                for (int j = 0; j < tableLength; j++)
                {
                    ws.Cells[startRow + i, col + j].Value = data[j, i];
                }
            }

            //Set columns to autofit for better output
            tableRange = ws.Cells[startRow, col, startRow + tableHeight - 1, col + tableLength - 1];
            tableRange.AutoFitColumns();

            //create NamedRange
            if (name != null)
                ws.Names.Add(name, tableRange);
            return tableRange;
        }

        /// <summary>
        /// Insert a table filled with data from an IEnumerable
        /// </summary>
        /// <typeparam name="T">Type of inserted objects</typeparam>
        /// <param name="ws">Worksheet in wich the table is created</param>
        /// <param name="col">Starting column of the table</param>
        /// <param name="row">Starting row of the table</param>
        /// <param name="data">IEnumerable in wich the data to insert is stored</param>
        /// <param name="title">The table's title</param>
        /// <param name="color">The table's color</param>
        /// <param name="showHeader">Defines if the table has header</param>
        /// <param name="Name">If given, creates a named range, with this name</param>
        ///  <returns>Range of the inserted data</returns>
        public static ExcelRange InsertTable<T>(this ExcelWorksheet ws, int row, int col, IEnumerable<T> data, string title = null, ExcelColor color = ExcelColor.Primary, bool showHeader = true, string Name = null)
        {
            //Get required Type data
            var dataType = data.First().GetType();
            var props = dataType.GetProperties();

            //We use the data often, don't want to always iterate on it
            var dataArray = data.ToArray();
            //Get table's range
            //If we show a header the height of the table is 1 bigger
            var tableHeight = dataArray.Length + (showHeader ? 1 : 0);
            var tableLength = props.Length;
            var tableRange = ws.Cells[row, col, row + tableHeight - 1, col + tableLength - 1];

            //Format table
            //Get the starting row for inserting data
            int startRow = tableRange.FormatAsTableWithTitle(title, color, showHeader, false);

            //Refresh table range
            tableRange = ws.Cells[startRow, col, startRow + tableHeight - 1, col + tableLength - 1];

            //Write header
            if (showHeader)
            {
                //If we show header then it contains the name of the properties
                for (int i = 0; i < props.Length; i++)
                {
                    var cell = ws.Cells[startRow, col + i];
                    //If the property has Display attribute "Name" then the header will contain that instead of the property's name from code
                    string name = ((DisplayAttribute)(props[i].GetCustomAttributes(typeof(DisplayAttribute), true)?.FirstOrDefault()))?.Name;
                    if (name != null)
                        cell.Value = name;
                    else
                        cell.Value = props[i].Name;

                    //Ha van description attribute (és még nincs más komment) akkor kommentben a cellára teszi
                    string description = ((DisplayAttribute)(props[i].GetCustomAttributes(typeof(DisplayAttribute), true)?.FirstOrDefault()))?.Description;
                    if (description != null)
                    {
                        if (cell.Comment == null)
                            cell.AddComment(description, "ExcelReportGenerator");
                    }
                }
                startRow += 1;
            }

            //Set numberformat for the table
            for (int i = 0; i < props.Length; i++)
            {
                //If column's property has DisplayFormatAttribute set the table's columns format to that
                string format = ((DisplayFormatAttribute)(props[i].GetCustomAttributes(typeof(DisplayFormatAttribute), true)?.FirstOrDefault()))?.DataFormatString;
                if (format != null)
                {
                    ws.Cells[tableRange.Start.Row, tableRange.Start.Column + i, tableRange.End.Row, tableRange.Start.Column + i].Style.Numberformat.Format = format;
                }

                //Ha a property dátum, akkor dátum formátumot kap
                if (props[i].PropertyType == typeof(DateTime))
                {
                    ws.Cells[tableRange.Start.Row, tableRange.Start.Column + i, tableRange.End.Row, tableRange.Start.Column + i].Style.Numberformat.Format = "yyyy.mm.dd.";
                }
            }



            //Insert data
            for (int i = 0; i < dataArray.Length; i++)
            {
                for (int j = 0; j < props.Length; j++)
                {
                    Object obj = props[j].GetValue(dataArray[i]);
                    ws.Cells[startRow + i, col + j].Value = obj;
                }
            }

            //Set columns to autofit for better output
            tableRange.AutoFitColumns();

            //create NamedRange
            if (Name != null)
                ws.Names.Add(Name, tableRange);
            return tableRange;
        }

        /// <summary>
        /// Insert table from key-value pairs
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="ws">Worksheet in wich the table is created</param>
        /// <param name="col">Starting column of the table</param>
        /// <param name="row">Starting row of the table</param>
        /// <param name="data">IEnumerable of key-value pairs in wich the data to insert is stored</param>
        /// <param name="title">The table's title</param>
        /// <param name="color">The table's color</param>
        /// <param name="Name">If given, creates a named range, with this name</param>
        /// <returns>Range of the inserted data</returns>
        public static ExcelRange InsertDictionary<TKey, TValue>(this ExcelWorksheet ws, int row, int col, IEnumerable<KeyValuePair<TKey, TValue>> data, string title = null, ExcelColor color = ExcelColor.Primary, string Name = null)
        {

            //Get table's range
            //If we show a header the height of the table is 1 bigger
            int tableLength = 2;
            int tableHeight = data.Count();
            var tableRange = ws.Cells[row, col, row + tableHeight - 1, col + tableLength - 1];

            //Format table
            //Get the starting row for inserting data
            int startRow = tableRange.FormatAsTableWithTitle(title, color, false, false);

            //Write data
            int i = 0;
            foreach (var value in data)
            {
                ws.Cells[startRow + i, col].Value = value.Key;
                ws.Cells[startRow + i, col + 1].Value = value.Value;
                i++;
            }

            //Set columns to autofit for better output
            tableRange = ws.Cells[startRow, col, startRow + tableHeight - 1, col + tableLength - 1];
            tableRange.AutoFitColumns();

            //create NamedRange
            if (Name != null)
                ws.Names.Add(Name, tableRange);
            return tableRange;

        }

        /// <summary>
        /// Insert a hierarchical list in tree style into the worksheet
        /// </summary>
        /// <param name="ws">Worksheet in wich the list is inserted</param>
        /// <param name="col">Starting column of the list</param>
        /// <param name="row">Starting row of the list</param>
        /// <param name="root">Root element of the list</param>
        /// <param name="title">Title of the list</param>
        /// <param name="color">color of the list</param>
        public static void InsertHierarchicalList(this ExcelWorksheet ws, int row, int col, HierarchyElement root, string title = null, ExcelColor color = ExcelColor.Primary)
        {
            //Get hierarchy debth and the number of items in it
            int depth = root.GetDepth();
            int length = root.GetCount();

            //Get table range
            ExcelRange range = ws.Cells[row, col, row + length - 1, col + depth - 1];


            //Format

            var palette = PaletteStorage.GetPalette(color);

            var startRow = row;


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

            //Insert data into formatted cells
            ws.PrintHierarchicalList(col, startRow, root);

            //Set columns to a low width
            for (int i = col; i < (col + depth - 1); i++)
            {
                ws.Column(i).Width = 2;
            }
            //set last column to autosize
            ws.Column(col + depth - 1).AutoFit();
        }

        //Prints hierarchical list's data from x,y position
        private static void PrintHierarchicalList(this ExcelWorksheet ws, int x, int y, HierarchyElement root)
        {
            ws.Cells[y, x].Value = root.Content;
            if (root.Children.Count > 0)
            {
                int startRow = y + 1;
                int startColumn = x + 1;
                foreach (var child in root.Children)
                {
                    ws.PrintHierarchicalList(startColumn, startRow, child);
                    startRow += child.GetCount();
                }
            }
        }

        /// <summary>
        /// Insert legend
        /// </summary>
        /// <param name="range">Range of the legend</param>
        /// <param name="text">Text of the legend</param>
        /// <param name="title">Title of the legend (can be null)</param>
        /// <param name="color">Color of the legend</param>
        public static void InsertLegend(this ExcelRange range, string text, string title = null, ExcelColor color = ExcelColor.Info)
        {
            var ws = range.Worksheet;
            var palette = PaletteStorage.GetPalette(color);
            //Set legends text range
            var legendRange = range;

            //If legend has title, format it
            if (title != null)
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

        /// <summary>
        /// Creates a link to given sheet
        /// </summary>
        /// <param name="range">Cells to place the link in</param>
        /// <param name="sheet">Destination sheet's name</param>
        public static void InsertLink(this ExcelRange range, string sheet)
        {
            range.Hyperlink = new Uri($"#'{sheet}'!A1", UriKind.Relative);
            range.Style.HyperlinkStyle();
        }



        /// <summary>
        /// Creates a link to selected cells in given sheet
        /// </summary>
        /// <param name="range">Cells to place the link in</param>
        /// <param name="sheet">Destination sheet's name</param>
        /// <param name="cells">Destination cells' address</param>
        public static void InsertLink(this ExcelRange range, string sheet, string cells)
        {
            range.Hyperlink = new Uri($"#'{sheet}'!{cells}", UriKind.Relative);
            range.Style.HyperlinkStyle();
        }

        //Set style to resemble a hyperlink
        private static void HyperlinkStyle(this OfficeOpenXml.Style.ExcelStyle style)
        {
            style.Font.UnderLine = true;
            style.Font.Color.SetColor(Color.Blue);
        }

        /// <summary>
        /// Draws a line chart for the given data
        /// </summary>
        /// <param name="ws">Worksheet in wich the graph is inserted</param>
        /// <param name="range">Range containing the data (first row for x axis other rows for series)</param>
        /// <param name="col">The graph inserted starts at this column</param>
        /// <param name="row">The graph inserted starts at this row</param>
        /// <param name="name">Id and default title of the graph</param>
        /// <param name="xLabel">Label for x axis of the graph</param>
        /// <param name="yLabel">Label for y axis of the graph</param>
        /// <param name="serieLabels">If the graph hase more than one series, each can be named separately</param>
        /// <param name="width">Graph's width in px</param>
        /// <param name="height">Graph's height in px</param>
        /// <param name="title">Title of the graph if the defauolt name has to be overwritten</param>
        public static void InsertLineChart(this ExcelWorksheet ws, ExcelRange range, int row, int col, string name, string xLabel = null, string yLabel = null, ExcelRange SerieLabels = null, int width = -1, int height = -1, string title = null)
        {
            var chart = (ExcelLineChart)ws.Drawings.AddChart(name, OfficeOpenXml.Drawing.Chart.eChartType.Line);
            //set the chart's position
            chart.SetPosition(row, 0, col, 0);

            //x axis range
            var xRange = ws.Cells[range.Start.Row, range.Start.Column, range.End.Row, range.Start.Column];

            //Load series to the graph
            for (int i = range.Start.Column + 1, j = 0; i <= range.End.Column; i++, j++)
            {
                chart.Series.Add(ws.Cells[range.Start.Row, i, range.End.Row, i], xRange).HeaderAddress = ws.Cells[SerieLabels.Start.Row, SerieLabels.Start.Column + j];
            }

            //If title is explicitly defined, set it, else it is the same as the graph's id
            chart.Title.Text = (title == null) ? name : title;

            //If chart's size is defined (and is valid) set the size
            if (width > 0 && height > 0)
            {
                chart.SetSize(width, height);
            }

            //set axis labels
            chart.YAxis.Title.Text = yLabel;
            chart.XAxis.Title.Text = xLabel;

            //Format graph to look aesthetic
            chart.Legend.Position = eLegendPosition.Bottom;
            chart.DataLabel.ShowLeaderLines = true;
            chart.YAxis.MajorGridlines.Fill.Color = System.Drawing.Color.LightGray;
            chart.YAxis.Border.Fill.Color = System.Drawing.Color.Transparent;
            chart.XAxis.Border.Fill.Color = System.Drawing.Color.Transparent;
            chart.RoundedCorners = false;
        }

        /// <summary>
        /// Draws a column chart for the given data
        /// </summary>
        /// <param name="ws">Worksheet in wich the graph is inserted</param>
        /// <param name="range">Range containing the data (first row for x axis other rows for series)</param>
        /// <param name="col">The graph inserted starts at this column</param>
        /// <param name="row">The graph inserted starts at this row</param>
        /// <param name="name">Id and default title of the graph</param>
        /// <param name="xLabel">Label for x axis of the graph</param>
        /// <param name="yLabel">Label for y axis of the graph</param>
        /// <param name="serieLabels">If the graph hase more than one series, each can be named separately</param>
        /// <param name="width">Graph's width in px</param>
        /// <param name="height">Graph's height in px</param>
        /// <param name="title">Title of the graph if the defauolt name has to be overwritten</param>
        public static void InsertColumnChart(this ExcelWorksheet ws, ExcelRange range, int row, int col, string name, string xLabel = null, string yLabel = null, ExcelRange serieLabels = null, int width = -1, int height = -1, string title = null)
        {
            var chart = (ExcelBarChart)ws.Drawings.AddChart(name, OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered);

            //set the chart's position
            chart.SetPosition(row, 0, col, 0);

            //x axis range
            var xRange = ws.Cells[range.Start.Row, range.Start.Column, range.End.Row, range.Start.Column];

            //Load series to the graph
            for (int i = range.Start.Column + 1, j = 0; i <= range.End.Column; i++, j++)
            {
                chart.Series.Add(ws.Cells[range.Start.Row, i, range.End.Row, i], xRange).HeaderAddress = ws.Cells[serieLabels.Start.Row, serieLabels.Start.Column + j];
            }

            //If title is explicitly defined, set it, else it is the same as the graph's id
            chart.Title.Text = (title == null) ? name : title;

            //If chart's size is defined (and is valid) set the size
            if (width > 0 && height > 0)
            {
                chart.SetSize(width, height);
            }

            //set axis labels
            chart.YAxis.Title.Text = yLabel;
            chart.XAxis.Title.Text = xLabel;

            //Format graph to look aesthetic
            chart.Legend.Position = eLegendPosition.Bottom;
            chart.DataLabel.ShowLeaderLines = true;
            chart.YAxis.MajorGridlines.Fill.Color = System.Drawing.Color.LightGray;
            chart.YAxis.Border.Fill.Color = System.Drawing.Color.Transparent;
            chart.XAxis.Border.Fill.Color = System.Drawing.Color.Transparent;
            chart.RoundedCorners = false;
        }

        /// <summary>
        /// Inserts text into the defined cell, and format to match text level
        /// </summary>
        /// <param name="ws">Worksheet in wich the text is inserted</param>
        /// <param name="row">Row of the cell</param>
        /// <param name="col">Column of the cell</param>
        /// <param name="text">Text to insert</param>
        /// <param name="level">Level of text</param>
        public static void InsertText(this ExcelWorksheet ws, int row, int col, string text, TextLevel level = TextLevel.Normal)
        {
            ws.Cells[row, col].Value = text;
            ws.Cells[row, col].Style.StyleAs(level);
        }




        //Draw all borders in range
        private static void BorderEverywhere(this ExcelRange range, OfficeOpenXml.Style.ExcelBorderStyle style = OfficeOpenXml.Style.ExcelBorderStyle.Thin)
        {
            range.Style.Border.Top.Style = range.Style.Border.Left.Style = range.Style.Border.Bottom.Style = range.Style.Border.Right.Style = style;
        }
        //Draw border around range
        private static void BorderAround(this ExcelRange range, OfficeOpenXml.Style.ExcelBorderStyle style = OfficeOpenXml.Style.ExcelBorderStyle.Thin)
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
