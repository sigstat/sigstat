# [OptimalDtwClassifier](./OptimalDtwClassifier.md)

Namespace: [SigStat]() > [Common]() > [PipelineItems]() > [Classifiers]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IClassifier](./../../Pipeline/IClassifier.md)


## Constructors

| Name | Summary | 
| --- | --- | 
| OptimalDtwClassifier ( [`Func`](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Func](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> | DistanceFunction |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[FeatureDescriptor](./../../FeatureDescriptor.md)> | Features |  | 
| [Sampler](./../../Sampler.md) | Sampler |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | Test ( [`ISignerModel`](./../../Pipeline/ISignerModel.md), [`Signature`](./../../Signature.md) ) |  | 
| [ISignerModel](./../../Pipeline/ISignerModel.md) | Train ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> ) |  | 


