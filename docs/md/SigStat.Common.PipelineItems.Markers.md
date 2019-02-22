## `LogMarker`

Logs the Pipeline Input. Useful for logging TimeMarker results.  <para>Default Pipeline Output: -</para>
```csharp
public class SigStat.Common.PipelineItems.Markers.LogMarker
    : PipelineBase, ITransformation, ILogger, IProgress, IPipelineIO

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Transform(`Signature` signature) |  | 


## `TimeMarkerStart`

Starts a timer to measure completion time of following transforms.  <para>Default Pipeline Output: (`System.DateTime`) DefaultTimer</para>
```csharp
public class SigStat.Common.PipelineItems.Markers.TimeMarkerStart
    : PipelineBase, ITransformation, ILogger, IProgress, IPipelineIO

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Transform(`Signature` signature) |  | 


## `TimeMarkerStop`

Stops a timer to measure completion time of previous transforms.  <para>Default Pipeline Output: (`System.DateTime`) DefaultTimer</para>
```csharp
public class SigStat.Common.PipelineItems.Markers.TimeMarkerStop
    : PipelineBase, ITransformation, ILogger, IProgress, IPipelineIO

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Transform(`Signature` signature) |  | 


