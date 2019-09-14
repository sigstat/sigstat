# [DtwClassifier](./DtwClassifier.md)

Namespace: [SigStat]() > [Common]() > [PipelineItems]() > [Classifiers]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IDistanceClassifier](./../../Pipeline/IDistanceClassifier.md), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
Classifies Signatures with the `SigStat.Common.Algorithms.Dtw` algorithm.

## Constructors

| Name | Summary | 
| --- | --- | 
| DtwClassifier (  ) | Initializes a new instance of the `SigStat.Common.PipelineItems.Classifiers.DtwClassifier` class with the default Manhattan distance method. | 
| DtwClassifier ( [`Func`](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> ) | Initializes a new instance of the `SigStat.Common.PipelineItems.Classifiers.DtwClassifier` class with a specified distance method. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Func](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> | DistanceFunction | The function used to calculate the distance between two data points during DTW calculation | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[FeatureDescriptor](./../../FeatureDescriptor.md)> | Features | Gets or sets the features to consider during distance calculation | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | MultiplicationFactor | Gets or sets the multiplication factor to be used during threshold calculation | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | Test ( [`ISignerModel`](./../../Pipeline/ISignerModel.md), [`Signature`](./../../Signature.md) ) |  | 
| [ISignerModel](./../../Pipeline/ISignerModel.md) | Train ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> ) |  | 


