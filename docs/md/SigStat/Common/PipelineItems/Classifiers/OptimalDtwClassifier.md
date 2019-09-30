# [OptimalDtwClassifier](./OptimalDtwClassifier.md)

Namespace: [SigStat]() > [Common](./../../README.md) > [PipelineItems]() > [Classifiers](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IDistanceClassifier](./../../Pipeline/IDistanceClassifier.md), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
This [SigStat.Common.Pipeline.IDistanceClassifier]() implementation will consider both test and  training samples and claculate the threshold to separate the original and forged  signatures to approximate EER. Note that this classifier is not applicable for  real world scenarios. It was developed to test the theoratical boundaries of  threshold based classification

## Constructors

| Name | Summary | 
| --- | --- | 
| OptimalDtwClassifier ( [`Func`](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> distanceFunction ) | Initializes a new instance of the [SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier]() class. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Func](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> | DistanceFunction | The function used to calculate the distance between two data points during DTW calculation | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[FeatureDescriptor](./../../FeatureDescriptor.md)> | Features | [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor.md)s to consider during classification | 
| [Sampler](./../../Sampler.md) | Sampler | [SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.Sampler](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Sampler.md) used for selecting training and test sets during a benchmark | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | Test ( [`ISignerModel`](./../../Pipeline/ISignerModel.md) signerModel, [`Signature`](./../../Signature.md) signature ) |  | 
| [ISignerModel](./../../Pipeline/ISignerModel.md) | Train ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> signatures ) |  | 


