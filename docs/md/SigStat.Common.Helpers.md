#### `BenchmarkConfig`

```csharp
public class SigStat.Common.Helpers.BenchmarkConfig

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>String</sub> | <sub>Database</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>Features</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>Filter</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>Interpolation</sub> | <sub></sub> | 
| <sub>Double</sub> | <sub>ResamplingParam</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>ResamplingType</sub> | <sub></sub> | 
| <sub>Boolean</sub> | <sub>Rotation</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>Sampling</sub> | <sub></sub> | 
| <sub>ValueTuple<String, String></sub> | <sub>TranslationScaling</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>BenchmarkConfig</sub> | <sub>FromJsonFile(String)</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>ToJsonString()</sub> | <sub></sub> | 
| <sub>String</sub> | <sub>ToShortString()</sub> | <sub></sub> | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>BenchmarkConfig</sub> | <sub>FromJsonString(String)</sub> | <sub></sub> | 
| <sub>List<BenchmarkConfig></sub> | <sub>GenerateConfigurations()</sub> | <sub></sub> | 


#### `IProgress`

<sub>Enables progress tracking by expsoing the `SigStat.Common.Helpers.IProgress.Progress` property and the `SigStat.Common.Helpers.IProgress.ProgressChanged` event.</sub>
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

<sub>A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.</sub>
```csharp
public class SigStat.Common.Helpers.SimpleConsoleLogger
    : ILogger

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>LogLevel</sub> | <sub>LogLevel</sub> | <sub>All events below this level will be filtered</sub> | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>EventHandler<String></sub> | <sub>Logged</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>IDisposable</sub> | <sub>BeginScope(TState)</sub> | <sub></sub> | 
| <sub>Boolean</sub> | <sub>IsEnabled(LogLevel)</sub> | <sub></sub> | 
| <sub>void</sub> | <sub>Log(LogLevel, EventId, TState, Exception, Func<TState, Exception, String>)</sub> | <sub></sub> | 


