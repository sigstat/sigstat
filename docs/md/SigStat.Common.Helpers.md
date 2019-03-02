#### `IProgress`

Enables progress tracking by expsoing the `SigStat.Common.Helpers.IProgress.Progress` property and the `SigStat.Common.Helpers.IProgress.ProgressChanged` event.
```csharp
public interface SigStat.Common.Helpers.IProgress

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Property` | <sub>Progress</sub> | Gets the current progress in percentage. | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `EventHandler<Int32>` | <sub>ProgressChanged</sub> | Invoked whenever the `SigStat.Common.Helpers.IProgress.Progress` property is changed. | 


