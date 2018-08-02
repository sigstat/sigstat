using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SigStat.Common.Helpers
{
    //TODO: egy queue-t bevezeti, ha sok thread ir az baj
    public class Logger
    {
        public List<LogEntry> Entries = new List<LogEntry>();
        public LogLevel Level = LogLevel.Error;
        public Stream OutputStream = new MemoryStream();//ez egy Stream, at lehet allitani akarmire is
        private StreamWriter sw;

        public Action<string> Output { get; }

        public Logger(LogLevel level, Action<string> output)
        {
            this.Level = level;
            sw = new StreamWriter(OutputStream);
            Output = output;
        }

        public Logger(LogLevel level)
        {
            this.Level = level;
            sw = new StreamWriter(OutputStream);
            Output = (s) => { };
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

        private void AddEntry(LogLevel messageLevel, object sender, string message)
        {
            if (Level >= messageLevel)
            {
                LogEntry newMessage = new LogEntry(DateTime.Now, messageLevel, sender, message);
                Entries.Add(newMessage);
                sw.WriteLine(newMessage.ToString());
                Output(newMessage.ToString());
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
            return $"{Timestamp.ToString()} [{Level}] - {Sender.ToString()}: {Message}";
        }
    }

}
