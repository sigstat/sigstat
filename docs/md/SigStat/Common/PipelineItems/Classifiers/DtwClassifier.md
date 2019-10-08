# [DtwClassifier](./DtwClassifier.md)

Namespace: [SigStat]() > [Common](./../../README.md) > [PipelineItems]() > [Classifiers](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IDistanceClassifier](./../../Pipeline/IDistanceClassifier.md), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
Classifies Signatures with the [Algorithms.Dtw](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Algorithms/Dtw.md) algorithm.

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>DtwClassifier (  )</sub><img width=200/>| <sub>Initializes a new instance of the [PipelineItems.Classifiers.DtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md) class with the default Manhattan distance method.</sub>| <br>
| <sub>DtwClassifier ( [Func](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> )</sub><img width=200/>| <sub>Initializes a new instance of the [PipelineItems.Classifiers.DtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md) class with a specified distance method.</sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>DistanceFunction</sub><img width=200/>| <sub>The function used to calculate the distance between two data points during DTW calculation</sub>| <br>
| <sub>Features</sub><img width=200/>| <sub>Gets or sets the features to consider during distance calculation</sub>| <br>
| <sub>MultiplicationFactor</sub><img width=200/>| <sub>Gets or sets the multiplication factor to be used during threshold calculation</sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[Test](./Methods/DtwClassifier-100663859.md) ( [`ISignerModel`](./../../Pipeline/ISignerModel.md), [`Signature`](./../../Signature.md) )</sub><img width=200/>| <sub></sub>| <br>
| <sub>[Train](./Methods/DtwClassifier-100663858.md) ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> )</sub><img width=200/>| <sub></sub>| <br>


