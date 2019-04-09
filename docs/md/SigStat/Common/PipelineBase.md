# [PipelineBase](./PipelineBase.md)

Namespace: [SigStat]() > [Common]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./ILoggerObject.md), [IProgress](./Helpers/IProgress.md), [IPipelineIO](./Pipeline/IPipelineIO.md)

## Summary
TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.  ILoggerObject, IProgress, IPipelineIO default implementacioja.

## Constructors

| Name | Summary | 
| --- | --- | 
| PipelineBase (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [ILogger](./PipelineBase.md) | Logger |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[PipelineInput](./Pipeline/PipelineInput.md)> | PipelineInputs |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[PipelineOutput](./Pipeline/PipelineOutput.md)> | PipelineOutputs |  | 
| [Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) | Progress |  | 


## Events

| Type | Name | Summary | 
| --- | --- | --- | 
| [EventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.EventHandler-1)\<[Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)> | ProgressChanged | The event is raised whenever the value of `SigStat.Common.PipelineBase.Progress` changes | 


