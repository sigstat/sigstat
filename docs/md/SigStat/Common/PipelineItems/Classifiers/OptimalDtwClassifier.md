# [OptimalDtwClassifier](./OptimalDtwClassifier.md)

Namespace: [SigStat]() > [Common](./../../README.md) > [PipelineItems]() > [Classifiers](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IDistanceClassifier](./../../Pipeline/IDistanceClassifier.md), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
This [IDistanceClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Pipeline/IDistanceClassifier.md) implementation will consider both test and  training samples and claculate the threshold to separate the original and forged  signatures to approximate EER. Note that this classifier is not applicable for  real world scenarios. It was developed to test the theoratical boundaries of  threshold based classification

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>OptimalDtwClassifier ( [Func](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> )</sub>| <sub>Initializes a new instance of the [OptimalDtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/OptimalDtwClassifier.md) class.</sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>DistanceFunction</sub>| <sub>The function used to calculate the distance between two data points during DTW calculation</sub>| <br>
| <sub>Features</sub>| <sub>[FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md)s to consider during classification</sub>| <br>
| <sub>Sampler</sub>| <sub>[Sampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/OptimalDtwClassifier.md) used for selecting training and test sets during a benchmark</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Test](./Methods/OptimalDtwClassifier-100663910.md) ( [`ISignerModel`](./../../Pipeline/ISignerModel.md), [`Signature`](./../../Signature.md) )</sub>| <sub></sub>| <br>
| <sub>[Train](./Methods/OptimalDtwClassifier-100663908.md) ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> )</sub>| <sub></sub>| <br>


