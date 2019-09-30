# [PipelineBase](./PipelineBase.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./ILoggerObject.md), [IProgress](./Helpers/IProgress.md), [IPipelineIO](./Pipeline/IPipelineIO.md)

## Summary
TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.  ILoggerObject, IProgress, IPipelineIO default implementacioja.

## Constructors

| Name | Summary | 
| --- | --- | 
| PipelineBase (  ) | Initializes a new instance of the [SigStat.Common.PipelineBase](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/PipelineBase.md) class. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [ILogger](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger) | Logger |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[PipelineInput](./Pipeline/PipelineInput.md)> | PipelineInputs | A collection of inputs for the pipeline elements | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[PipelineOutput](./Pipeline/PipelineOutput.md)> | PipelineOutputs | A collection of outputs for the pipeline elements | 
| [Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) | Progress |  | 


## Events

| Type | Name | Summary | 
| --- | --- | --- | 
| [EventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.EventHandler-1)\<[Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)> | ProgressChanged | The event is raised whenever the value of [SigStat.Common.PipelineBase.Progress]() changes | 


