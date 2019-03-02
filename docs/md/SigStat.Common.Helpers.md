#### `IProgress`

Enables progress tracking by expsoing the `SigStat.Common.Helpers.IProgress.Progress` property and the `SigStat.Common.Helpers.IProgress.ProgressChanged` event.
```csharp
public interface SigStat.Common.Helpers.IProgress

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `<sub>Int32</sub>` | <sub>Progress</sub> | Gets the current progress in percentage. | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `<sub>EventHandler<Int32></sub>` | <sub>ProgressChanged</sub> | Invoked whenever the `SigStat.Common.Helpers.IProgress.Progress` property is changed. | 


#### `SimpleConsoleLogger`

A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.
```csharp
public class SigStat.Common.Helpers.SimpleConsoleLogger
    : ILogger

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `<sub>LogLevel</sub>` | <sub>LogLevel</sub> | All events below this level will be filtered | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `<sub>IDisposable</sub>` | <sub>BeginScope(TState)</sub> |  | 
| `<sub>Boolean</sub>` | <sub>IsEnabled(LogLevel)</sub> |  | 
| `<sub>void</sub>` | <sub>Log(LogLevel, EventId, TState, Exception, Func<TState, Exception, String>)</sub> |  | 


