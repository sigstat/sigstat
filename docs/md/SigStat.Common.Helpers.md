#### `IProgress`

Enables progress tracking by expsoing the `SigStat.Common.Helpers.IProgress.Progress` property and the `SigStat.Common.Helpers.IProgress.ProgressChanged` event.
```csharp
public interface SigStat.Common.Helpers.IProgress

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Int32</sub> | <sub>Progress</sub> | <sub>Gets the current progress in percentage.</sub> | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>EventHandler<Int32></sub> | <sub>ProgressChanged</sub> | <sub>Invoked whenever the `SigStat.Common.Helpers.IProgress.Progress` property is changed.</sub> | 


#### `SerializationHelper`

```csharp
public class SigStat.Common.Helpers.SerializationHelper

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>T</sub> | <sub>Deserialize(String)</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>Serialize(T)</sub> | <sub></sub> | 


#### `SimpleConsoleLogger`

A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.
```csharp
public class SigStat.Common.Helpers.SimpleConsoleLogger
    : ILogger

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>LogLevel</sub> | <sub>LogLevel</sub> | <sub>All events below this level will be filtered</sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>IDisposable</sub> | <sub>BeginScope(TState)</sub> | <sub></sub> | 
| <sub>Boolean</sub> | <sub>IsEnabled(LogLevel)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>Log(LogLevel, EventId, TState, Exception, Func<TState, Exception, String>)</sub> | <sub></sub> | 


