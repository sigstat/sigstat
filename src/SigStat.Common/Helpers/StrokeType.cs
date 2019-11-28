using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Describes the type of a stroke
    /// </summary>
    public enum StrokeType
    {
        /// <summary>
        /// The type of the stroke is not known
        /// </summary>
        Unknown,
        /// <summary>
        /// The stroke was made in the air (the pen did not tuch the tablet/paper)
        /// </summary>
        Up,
        /// <summary>
        /// The stroke was made on the writing surface (tablet, paper etc.)
        /// </summary>
        Down
    }
}
