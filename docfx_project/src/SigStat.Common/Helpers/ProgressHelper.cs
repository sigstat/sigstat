using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SigStat.Common.Helpers
{
    /// <summary>
    /// A helper class for tracking progress of an operation. 
    /// </summary>
    public class ProgressHelper : IDisposable
    {
        /// <summary>
        /// The total number of individual items to be processed.
        /// </summary>
        public int Maximum { get; set; }


        private int value;
        /// <summary>
        /// The actual number of processed items.
        /// </summary>
        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                if (value == 0 || value == Maximum || (ReportIntervallSeconds > 0 && (DateTime.Now - lastProgress).TotalSeconds > ReportIntervallSeconds))
                {
                    ReportProgress?.Invoke(this);
                    lastProgress = DateTime.Now;
                }
            }
        }
        /// <summary>
        /// If larger than 0, ReportProgress event will be executed periodically after ReportIntervallSeconds when the <see cref="Value"/> property changes. 
        /// </summary>
        public int ReportIntervallSeconds { get; set; }


        private DateTime lastProgress = DateTime.Now;
        private Stopwatch stopwatch = Stopwatch.StartNew();

        /// <summary>
        /// Event will be executed periodically after <see cref="ReportIntervallSeconds"/> when the <see cref="Value"/> property changes. 
        /// </summary>
        /// <remarks>If <see cref="ReportIntervallSeconds"/> is set to 0, this event will be suppressed</remarks>
        public event Action<ProgressHelper> ReportProgress;

        /// <summary>
        /// Gets the total elapsed time measured by the current instance.
        /// </summary>
        public TimeSpan Elapsed => stopwatch.Elapsed;
        /// <summary>
        /// Gets the estimated time of completion assuming linear progress.
        /// </summary>
        public DateTime Eta => DateTime.Now + Remaining;
        /// <summary>
        /// Gets the estimated remaining time till completion assuming linear progress.
        /// </summary>
        public TimeSpan Remaining => TimeSpan.FromMilliseconds(Value > 0 ? stopwatch.ElapsedMilliseconds * (Maximum - Value) / Value : 0);

        /// <summary>
        /// Initializes an instance of <see cref="ProgressHelper"/>
        /// </summary>
        protected ProgressHelper()
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="ProgressHelper"/> with the given parameters. Make sure to manually set the <see cref="Value"/> property during operation.
        /// </summary>
        /// <param name="maximum">The total number of individual items to be processed.</param>
        /// <param name="reportIntervallSeconds">If larger than 0, ReportProgress event will be executed periodically after ReportIntervallSeconds when the <see cref="Value"/> property changes. </param>
        /// <param name="reportProgress"></param>
        /// <returns></returns>
        public static ProgressHelper StartNew(int maximum, int reportIntervallSeconds = 0, Action<ProgressHelper> reportProgress = null)
        {
            var instance = new ProgressHelper()
            {
                Maximum = maximum,
                ReportIntervallSeconds = reportIntervallSeconds
            };
            if (reportIntervallSeconds > 0)
            {
                if (reportProgress != null)
                    instance.ReportProgress += reportProgress;
                else
                    instance.ReportProgress += pr => Console.WriteLine($"{pr.Value}/{pr.Maximum} processed in {pr.Elapsed}. (ETA: {pr.Eta})"); ;
            }
            return instance;
        }


        /// <inheritdoc/>
        [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "No relevant resources to dispose of")]
        public void Dispose()
        {
            ReportProgress?.Invoke(this);
        }

    }
}
