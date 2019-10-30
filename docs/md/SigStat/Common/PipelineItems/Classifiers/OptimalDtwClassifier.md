# [OptimalDtwClassifier](./OptimalDtwClassifier.md)

Namespace: [SigStat]() > [Common](./../../README.md) > [PipelineItems]() > [Classifiers](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IDistanceClassifier](./../../Pipeline/IDistanceClassifier.md), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
This [IDistanceClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Pipeline/IDistanceClassifier.md) implementation will consider both test and  training samples and claculate the threshold to separate the original and forged  signatures to approximate EER. Note that this classifier is not applicable for  real world scenarios. It was developed to test the theoratical boundaries of  threshold based classification

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| OptimalDtwClassifier ([Func](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>) | Initializes a new instance of the [OptimalDtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/OptimalDtwClassifier.md) class. | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| DistanceFunction | The function used to calculate the distance between two data points during DTW calculation | 
| Features | [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md)s to consider during classification | 
| Sampler | [Sampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/OptimalDtwClassifier.md) used for selecting training and test sets during a benchmark | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Test](./Methods/OptimalDtwClassifier--Test.md) ([`ISignerModel`](./../../Pipeline/ISignerModel.md), [`Signature`](./../../Signature.md) ) |  | 
| [Train](./Methods/OptimalDtwClassifier--Train.md) ([`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> ) |  | 


