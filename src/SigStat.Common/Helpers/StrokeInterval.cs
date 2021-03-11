using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a stroke in an online signature
    /// </summary>
    public class StrokeInterval
    {
        /// <summary>
        /// The index of the firs element
        /// </summary>
        public int StartIndex { get; set; }
        /// <summary>
        /// The index of the last element
        /// </summary>
        public int EndIndex { get; set; }
        /// <summary>
        /// The <see cref="StrokeType"/> of the stroke.
        /// </summary>
        public StrokeType StrokeType { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeInterval"/> struct.
        /// </summary>
        /// <param name="startIndex">The index of the firs element</param>
        /// <param name="endIndex">The index of the last element</param>
        /// <param name="strokeType">Type of the stroke.</param>
        public StrokeInterval(int startIndex, int endIndex, StrokeType strokeType)
        {
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            this.StrokeType = strokeType;
        }
    }

}
