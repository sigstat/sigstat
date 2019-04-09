# [TranslatePreproc](./TranslatePreproc.md)

Namespace: [SigStat]() > [Common]() > [PipelineItems]() > [Transforms]() > [Preprocessing]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)


## Constructors

| Name | Summary | 
| --- | --- | 
| TranslatePreproc (  ) |  | 
| TranslatePreproc ( [`OriginType`](./OriginType.md) ) |  | 
| TranslatePreproc ( [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double) ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [OriginType](./OriginType.md) | GoalOrigin |  | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputFeature |  | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | NewOrigin |  | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | OutputFeature |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../../../Signature.md) ) |  | 


