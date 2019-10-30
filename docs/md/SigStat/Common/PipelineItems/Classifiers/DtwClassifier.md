# [DtwClassifier](./DtwClassifier.md)

Namespace: [SigStat]() > [Common](./../../README.md) > [PipelineItems]() > [Classifiers](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IDistanceClassifier](./../../Pipeline/IDistanceClassifier.md), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
Classifies Signatures with the [Dtw](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Algorithms/Dtw.md) algorithm.

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| DtwClassifier () | Initializes a new instance of the [DtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md) class with the default Manhattan distance method. | 
| DtwClassifier ([Func](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>) | Initializes a new instance of the [DtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md) class with a specified distance method. | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| DistanceFunction | The function used to calculate the distance between two data points during DTW calculation | 
| Features | Gets or sets the features to consider during distance calculation | 
| MultiplicationFactor | Gets or sets the multiplication factor to be used during threshold calculation | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Test](./Methods/DtwClassifier--Test.md) ([`ISignerModel`](./../../Pipeline/ISignerModel.md), [`Signature`](./../../Signature.md) ) |  | 
| [Train](./Methods/DtwClassifier--Train.md) ([`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> ) |  | 


