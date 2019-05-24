using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Extension methods for common IO operations
    /// </summary>
    public static class IOExtensions
    {
        /// <summary>
        /// Gets the given relative or absolute path in a platform neutral form
        /// </summary>
        /// <param name="path"></param>
        public static string GetPath(this string path)
        {
            return path.Replace('\\', Path.DirectorySeparatorChar);
        }
    }
}
