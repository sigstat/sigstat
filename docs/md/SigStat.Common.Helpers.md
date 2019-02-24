## `ConfigurationHelper`

```csharp
public class SigStat.Common.Helpers.ConfigurationHelper

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Configuration` | Load() |  | 


## `ILogger`

Enables logging by exposing a `SigStat.Common.Helpers.Logger` property.
```csharp
public interface SigStat.Common.Helpers.ILogger

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Logger` | Logger | Gets or sets the attached `SigStat.Common.Helpers.Logger` object used to log messages. | 


## `IProgress`

Enables progress tracking by expsoing the `SigStat.Common.Helpers.IProgress.Progress` property and the `SigStat.Common.Helpers.IProgress.ProgressChanged` event.
```csharp
public interface SigStat.Common.Helpers.IProgress

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Int32` | Progress | Gets the current progress in percentage. | 


Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `EventHandler<Int32>` | ProgressChanged | Invoked whenever the `SigStat.Common.Helpers.IProgress.Progress` property is changed. | 


## `LogEntry`

Represents a single entry of the log, consisting of a timestamp, a level, a sender and the message.
```csharp
public class SigStat.Common.Helpers.LogEntry

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `LogLevel` | Level | Log level of the entry. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | ToString() | Format the contained data to string, divided by tab characters.  Use this to display the entry in the console. | 


## `Logger`

A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.
```csharp
public class SigStat.Common.Helpers.Logger

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `List<LogEntry>` | Entries |  | 
| `LogLevel` | FilteringLevel |  | 
| `IReadOnlyDictionary<String, Object>` | ObjectEntries |  | 
| `Boolean` | StoreEntries | Enable or disable the storing of log entries. This can come useful for filtering by certain type of entries. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Debug(Object, String) | Enqueue a debug level log entry. | 
| `void` | EnqueueEntry(LogLevel, Object, String) | Enqueue a new log entry with specified level. The entry is filtered through `SigStat.Common.Helpers.Logger.FilteringLevel`. | 
| `void` | Error(Object, String) | Enqueue an error level log entry. | 
| `void` | Fatal(Object, String) | Enqueue a fatal level log entry. | 
| `void` | Info(Object, String) | Enqueue an information level log entry. | 
| `void` | Info(Object, String, Object) | Enqueue an information level log entry. | 
| `void` | Stop() | Stop accepting entries, flush the queue and stop the consuming thread. | 
| `void` | Warn(Object, String) | Enqueue a warning level log entry. | 


## `LogLevel`

Represents the level of log.  Lowest level: Off.  Highest level: Debug.
```csharp
public enum SigStat.Common.Helpers.LogLevel
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | Off | Completely turn off logging. | 
| `1` | Fatal | Represents a fatal error level log. | 
| `2` | Error | Represents an error level log. | 
| `3` | Warn | Represents a warning level log. | 
| `4` | Info | Represents an information level log. | 
| `5` | Debug | Represents a debug level log. | 


