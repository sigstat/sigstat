using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a type, that contains an ILogger property that can be used to perform logging.
    /// </summary>
    public interface ILoggerObject
    {
        /// <summary>
        /// Gets or sets the ILogger implementation used to perform logging 
        /// </summary>
        ILogger Logger { get; set; }
    }
}
