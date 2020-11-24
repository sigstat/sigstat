using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Standard event identifiers used by the SigStat system
    /// </summary>
    public static class SigStatEvents
    {
        /// <summary>
        /// Events originating from a benchmark
        /// </summary>
        public static readonly EventId BenchmarkEvent = new EventId(10000, nameof(BenchmarkEvent));
        /// <summary>
        /// Events originating from a verifier
        /// </summary>
        public static readonly EventId VerifierEvent = new EventId(10000, nameof(VerifierEvent));
    }
}
