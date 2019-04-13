# [DtwClassifier](./DtwClassifier.md)

Namespace: [SigStat]() > [Common]() > [PipelineItems]() > [Classifiers]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
Classifies Signatures with the `SigStat.Common.Algorithms.Dtw` algorithm.

## Constructors

| Name | Summary | 
| --- | --- | 
| DtwClassifier (  ) | Initializes a new instance of the `SigStat.Common.PipelineItems.Classifiers.DtwClassifier` class with the default Manhattan distance method. | 
| DtwClassifier ( [`Func`](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[FeatureDescriptor](./../../FeatureDescriptor.md)> | Features |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | Test ( [`ISignerModel`](./../../Pipeline/ISignerModel.md), [`Signature`](./../../Signature.md) ) |  | 
| [ISignerModel](./../../Pipeline/ISignerModel.md) | Train ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> ) |  | 


