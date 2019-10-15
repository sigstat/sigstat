# [DtwClassifier](./DtwClassifier.md)

Namespace: [SigStat]() > [Common](./../../README.md) > [PipelineItems]() > [Classifiers](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IDistanceClassifier](./../../Pipeline/IDistanceClassifier.md), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
Classifies Signatures with the [Dtw](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Algorithms/Dtw.md) algorithm.

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>DtwClassifier (  )</sub>| <sub>Initializes a new instance of the [DtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md) class with the default Manhattan distance method.</sub>| <br>
| <sub>DtwClassifier ( [Func](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[], [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> )</sub>| <sub>Initializes a new instance of the [DtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md) class with a specified distance method.</sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>DistanceFunction</sub>| <sub>The function used to calculate the distance between two data points during DTW calculation</sub>| <br>
| <sub>Features</sub>| <sub>Gets or sets the features to consider during distance calculation</sub>| <br>
| <sub>MultiplicationFactor</sub>| <sub>Gets or sets the multiplication factor to be used during threshold calculation</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Test](./Methods/DtwClassifier-100663900.md) ( [`ISignerModel`](./../../Pipeline/ISignerModel.md), [`Signature`](./../../Signature.md) )</sub>| <sub></sub>| <br>
| <sub>[Train](./Methods/DtwClassifier-100663899.md) ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> )</sub>| <sub></sub>| <br>


