using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SigStat.Common.Helpers
{
    //Holding a single writer open will be more efficient than repeatedly opening and closing it. If this is critical data,
    //however, you should call Flush() after each write to make sure it gets to disk.
    //Is your program multi-threaded? If so, you may wish to have a producer/consumer queue
    //TODO ezt

    public class Logger
    {
        public List<LogEntry> Entries = new List<LogEntry>();
        public LogLevel Level = LogLevel.Error;
        private StreamWriter sw;

        private Action<string> OutputAction { get; }

        public Logger(LogLevel level, Action<string> output)
        {
            this.Level = level;
            OutputAction = output;
        }

        public Logger(LogLevel level, Stream outputStream)
        {
            this.Level = level;
            sw = new StreamWriter(outputStream);
            sw.AutoFlush = true;
            OutputAction = (s) => {
                if (sw != null)
                    sw.WriteLine(s);
            };
        }

        public Logger(LogLevel level)
        {
            this.Level = level;
            sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);
            OutputAction = (s) => {
                if (sw != null)
                    sw.WriteLine(s);
            };
        }

        //proba volt:
        //public void Fatal(string message, [CallerMemberName] string callerName = "")

        //Ez picit sorminta, de konnyeb hasznalni: Log.Error(..) ahelyett hogy Log.Message<LogLevel.Error>(..)
        public void Fatal(object sender, string message)
        {
            AddEntry(LogLevel.Fatal, sender, message);
        }
        public void Error(object sender, string message)
        {
            AddEntry(LogLevel.Error, sender, message);
        }
        public void Warn(object sender, string message)
        {
            AddEntry(LogLevel.Warn, sender, message);
        }
        public void Info(object sender, string message)
        {
            AddEntry(LogLevel.Info, sender, message);
        }
        public void Debug(object sender, string message)
        {
            AddEntry(LogLevel.Debug, sender, message);
        }

        //ez pl a LogMarker miatt public
        public void AddEntry(LogLevel messageLevel, object sender, string message)
        {
            if (Level >= messageLevel)
            {
                LogEntry newEntry = new LogEntry(DateTime.Now, messageLevel, sender, message);
                Entries.Add(newEntry);
                string entryString = newEntry.ToString();
                OutputAction(entryString);
            }
        }

    }

    public enum LogLevel
    {
        Off,
        Fatal,
        Error,
        Warn,
        Info,
        Debug
    }

    public class LogEntry
    {
        public DateTime Timestamp;
        public LogLevel Level;
        public object Sender;
        public string Message;

        public LogEntry(DateTime timestamp, LogLevel level, object sender, string message)
        {
            Timestamp = timestamp;
            Level = level;
            Sender = sender;
            Message = message;
        }

        public override string ToString()
        {
            string senderID = Sender.ToString().Split('.').Last() + Sender.GetHashCode();//TODO: legyen a loggolo objecteknek IDja
            return $"{Timestamp.ToLongTimeString()} [{Level}] - {senderID}: {Message}";
        }
    }

}
