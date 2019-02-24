#### `ConfigurationHelper`

```csharp
public class SigStat.Common.Helpers.ConfigurationHelper

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Configuration` | <sub>Load()</sub> |  | 


#### `ILogger`

Enables logging by exposing a `SigStat.Common.Helpers.Logger` property.
```csharp
public interface SigStat.Common.Helpers.ILogger

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Logger` | Logger | Gets or sets the attached `SigStat.Common.Helpers.Logger` object used to log messages. | 


#### `IProgress`

Enables progress tracking by expsoing the `SigStat.Common.Helpers.IProgress.Progress` property and the `SigStat.Common.Helpers.IProgress.ProgressChanged` event.
```csharp
public interface SigStat.Common.Helpers.IProgress

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Int32` | Progress | Gets the current progress in percentage. | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `EventHandler<Int32>` | ProgressChanged | Invoked whenever the `SigStat.Common.Helpers.IProgress.Progress` property is changed. | 


#### `LogEntry`

Represents a single entry of the log, consisting of a timestamp, a level, a sender and the message.
```csharp
public class SigStat.Common.Helpers.LogEntry

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `LogLevel` | Level | Log level of the entry. | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>ToString()</sub> | Format the contained data to string, divided by tab characters.  Use this to display the entry in the console. | 


#### `Logger`

A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.
```csharp
public class SigStat.Common.Helpers.Logger

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<LogEntry>` | Entries |  | 
| `LogLevel` | FilteringLevel |  | 
| `IReadOnlyDictionary<String, Object>` | ObjectEntries |  | 
| `Boolean` | StoreEntries | Enable or disable the storing of log entries. This can come useful for filtering by certain type of entries. | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Debug(Object, String)</sub> | Enqueue a debug level log entry. | 
| `void` | <sub>EnqueueEntry(LogLevel, Object, String)</sub> | Enqueue a new log entry with specified level. The entry is filtered through `SigStat.Common.Helpers.Logger.FilteringLevel`. | 
| `void` | <sub>Error(Object, String)</sub> | Enqueue an error level log entry. | 
| `void` | <sub>Fatal(Object, String)</sub> | Enqueue a fatal level log entry. | 
| `void` | <sub>Info(Object, String)</sub> | Enqueue an information level log entry. | 
| `void` | <sub>Info(Object, String, Object)</sub> | Enqueue an information level log entry. | 
| `void` | <sub>Stop()</sub> | Stop accepting entries, flush the queue and stop the consuming thread. | 
| `void` | <sub>Warn(Object, String)</sub> | Enqueue a warning level log entry. | 


#### `LogLevel`

Represents the level of log.  Lowest level: Off.  Highest level: Debug.
```csharp
public enum SigStat.Common.Helpers.LogLevel
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| <sub>Value</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `0` | Off | Completely turn off logging. | 
| `1` | Fatal | Represents a fatal error level log. | 
| `2` | Error | Represents an error level log. | 
| `3` | Warn | Represents a warning level log. | 
| `4` | Info | Represents an information level log. | 
| `5` | Debug | Represents a debug level log. | 


