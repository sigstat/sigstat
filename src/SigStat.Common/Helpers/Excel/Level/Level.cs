using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers.Excel.Level
{
    public enum TextLevel
    {
        Title,
        Heading1,
        Heading2,
        Heading3,
        Normal
    }
    static class Level
    {
        /// <summary>
        /// Set the style according to text level
        /// </summary>
        /// <param name="style"></param>
        /// <param name="level"></param>
        public static void StyleAs(this ExcelStyle style, TextLevel level)
        {
            switch (level)
            {
                case TextLevel.Title:
                    style.StyleAsTitle();
                    break;
                case TextLevel.Heading1:
                    style.StyleAsHeading1();
                    break;
                case TextLevel.Heading2:
                    style.StyleAsHeading2();
                    break;
                case TextLevel.Heading3:
                    style.StyleAsHeading3();
                    break;
                case TextLevel.Normal:
                    style.StyleAsNormal();
                    break;
            }
        }

        private static void StyleAsTitle(this ExcelStyle style)
        {
            style.Font.Bold = true;
            style.Font.Size = 18;
        }

        private static void StyleAsHeading1(this ExcelStyle style)
        {
            style.Font.Bold = true;
            style.Font.Size = 15;
        }
        private static void StyleAsHeading2(this ExcelStyle style)
        {
            style.Font.Bold = true;
            style.Font.Size = 13;
        }
        private static void StyleAsHeading3(this ExcelStyle style)
        {
            style.Font.Bold = true;
            style.Font.Size = 11;
        }
        private static void StyleAsNormal(this ExcelStyle style)
        {
            style.Font.Bold = false;
            style.Font.Size = 11;
        }

    }

}
