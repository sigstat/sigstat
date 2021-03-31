using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers.Excel
{
    /// <summary>
    /// Ignores the marked property, when generating an Excel table from the class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false, Inherited =true)]
    public class ExcelIgnoreAttribute: Attribute
    {

    }
}
