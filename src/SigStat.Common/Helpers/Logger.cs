using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Common.Helpers
{
    /// <summary>
    /// A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>A producer-consumer pattern is implemented with a concurrent queue to support multi-threaded pipelines.</item>
    /// <item>Holding the StreamWriter open is more efficient than repeatedly opening and closing it.</item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// Logger l1 = new Logger(LogLevel.Info);
    /// Logger.Warn(this, "Training on non-genuine signature.");
    /// </code>
    /// </example>
    public class Logger
    {
        /// <summary>
        /// Enable or disable the storing of log entries. This can come useful for filtering by certain type of entries.
        /// </summary>
        public bool StoreEntries { get; set; } = false;
        /// <summary>
        /// A list where Entries are stored if <see cref="StoreEntries"/> is enabled.
        /// </summary>
        public List<LogEntry> Entries = new List<LogEntry>();//ez maradhat sima list, mert csak 1 consumer van

        ConcurrentDictionary<string, object> objectEntries = new ConcurrentDictionary<string, object>();
        public IReadOnlyDictionary<string, object> ObjectEntries { get { return objectEntries; } }

        /// <summary>
        /// Gets or sets the filtering level. Entries above this level will be ignored.
        /// </summary>
        public LogLevel FilteringLevel = LogLevel.Error;
        private StreamWriter sw;
        private Action<LogLevel, string> OutputAction { get; }
        //The default collection type for BlockingCollection<T> is ConcurrentQueue<T>
        private BlockingCollection<LogEntry> queue = new BlockingCollection<LogEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="filteringLevel">Entries above this level will be ignored.</param>
        /// <param name="outputStream">A stream to write consumed entries to. For example a FileStream pointing to the log archives.</param>
        /// <param name="outputAction">An action to perform when an entry is consumed. Use this for instant gui display of messages.</param>
        public Logger(LogLevel filteringLevel, Stream outputStream = null, Action<LogLevel, string> outputAction = null)
        {
            this.FilteringLevel = filteringLevel;
            if (outputStream != null)
            {
                sw = new StreamWriter(outputStream);
                sw.AutoFlush = true;
            }
            this.OutputAction = outputAction;
            StartConsumingThread();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class, and rewire the default Console IO.
        /// </summary>
        /// <param name="filteringLevel">Entries above this level will be ignored.</param>
        public Logger(LogLevel filteringLevel) :this(filteringLevel, Console.OpenStandardOutput(), null)
        {
            Console.SetOut(sw);
        }

        //proba volt:
        //public void Fatal(string message, [CallerMemberName] string callerName = "")

        //Ez picit sorminta, de konnyeb hasznalni: Log.Error(..) ahelyett hogy Log.Message<LogLevel.Error>(..)

        /// <summary>
        /// Enqueue a fatal level log entry.
        /// </summary>
        /// <param name="sender">The object who sent the log. Usually provide [this].</param>
        /// <param name="message">Main content of the log entry.</param>
        public void Fatal(object sender, string message)
        {
            EnqueueEntry(LogLevel.Fatal, sender, message);
        }
        /// <summary>
        /// Enqueue an error level log entry.
        /// </summary>
        /// <param name="sender">The object who sent the log. Usually provide [this].</param>
        /// <param name="message">Main content of the log entry.</param>
        public void Error(object sender, string message)
        {
            EnqueueEntry(LogLevel.Error, sender, message);
        }
        /// <summary>
        /// Enqueue a warning level log entry.
        /// </summary>
        /// <param name="sender">The object who sent the log. Usually provide [this].</param>
        /// <param name="message">Main content of the log entry.</param>
        public void Warn(object sender, string message)
        {
            EnqueueEntry(LogLevel.Warn, sender, message);
        }
        /// <summary>
        /// Enqueue an information level log entry.
        /// </summary>
        /// <param name="sender">The object who sent the log. Usually provide [this].</param>
        /// <param name="message">Main content of the log entry.</param>
        public void Info(object sender, string message)
        {
            EnqueueEntry(LogLevel.Info, sender, message);
        }


        public void Info(object sender, string key, object infoObject)
        {
            objectEntries[key] = infoObject;
            EnqueueEntry(LogLevel.Info, sender, "Added object with key: " + key);
        }


        /// <summary>
        /// Enqueue a debug level log entry.
        /// </summary>
        /// <param name="sender">The object who sent the log. Usually provide [this].</param>
        /// <param name="message">Main content of the log entry.</param>
        public void Debug(object sender, string message)
        {
            EnqueueEntry(LogLevel.Debug, sender, message);
        }

        //ez pl a LogMarker miatt public
        /// <summary>
        /// Enqueue a new log entry with specified level. The entry is filtered through <see cref="FilteringLevel"/>.
        /// </summary>
        /// <param name="messageLevel">Log level of the entry.</param>
        /// <param name="sender">The object who sent the log. Usually provide [this].</param>
        /// <param name="message">Main content of the log entry.</param>
        public void EnqueueEntry(LogLevel messageLevel, object sender, string message)
        {
            if (FilteringLevel >= messageLevel)
            {
                LogEntry newEntry = new LogEntry(DateTime.Now, messageLevel, sender, message);
                queue.Add(newEntry);
            }
        }

        private void StartConsumingThread()
        {
            Info(this, "Logger started consuming.");
            Task.Factory.StartNew(ConsumerLoop);
        }

        private void ConsumerLoop()
        {
            foreach (LogEntry newEntry in queue.GetConsumingEnumerable())
            {
                if (StoreEntries)
                {
                    Entries.Add(newEntry);
                }

                string s = newEntry.ToString();
                sw?.WriteLine(s);
                OutputAction?.Invoke(newEntry.Level, s);
                if (queue.IsCompleted)//IsCompleted meaning: marked as complete for adding && empty
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Stop accepting entries, flush the queue and stop the consuming thread.
        /// </summary>
        public void Stop()
        {
            //StopConsumingThread();//nem muszaj de szebb lenne (igy most rovid idore 2 consumer van)
            queue.CompleteAdding();
            ConsumerLoop();//consume rest of the entries synchronously
        }

    }

    /// <summary>
    /// Represents the level of log.
    /// Lowest level: Off.
    /// Highest level: Debug.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>Completely turn off logging.</summary>
        Off,
        /// <summary>Represents a fatal error level log.</summary>
        Fatal,
        /// <summary>Represents an error level log.</summary>
        Error,
        /// <summary>Represents a warning level log.</summary>
        Warn,
        /// <summary>Represents an information level log.</summary>
        Info,
        /// <summary>Represents a debug level log.</summary>
        Debug
    }

    /// <summary>
    /// Represents a single entry of the log, consisting of a timestamp, a level, a sender and the message.
    /// </summary>
    public class LogEntry
    {
        /// <summary>Exact date and time of the entry's creation.</summary>
        public readonly DateTime Timestamp;
        /// <summary>Log level of the entry.</summary>
        public readonly LogLevel Level;
        /// <summary>Reference of the object that created the entry.</summary>
        public readonly object Sender;
        /// <summary>Main content of the entry.</summary>
        public readonly string Message;

        /// <remark>
        /// The constructor is internal, as only the <see cref="Logger"/> may create new entries.
        /// </remark>
        internal LogEntry(DateTime timestamp, LogLevel level, object sender, string message)
        {
            Timestamp = timestamp;
            Level = level;
            Sender = sender;
            Message = message;
        }

        /// <summary>
        /// Format the contained data to string, divided by tab characters.
        /// Use this to display the entry in the console.
        /// </summary>
        /// <returns>String result.</returns>
        public override string ToString()
        {
            string senderID = Sender.ToString().Split('.').Last() + Sender.GetHashCode();//TODO: legyen a loggolo objecteknek IDja
            return $"{Timestamp.ToLongTimeString()} [{Level}] \t - {senderID}: {Message}";
        }
    }

}
