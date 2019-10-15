# [WeightedClassifier](./WeightedClassifier.md)

Namespace: [SigStat]() > [Common](./../../README.md) > [PipelineItems]() > [Classifiers](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
Classifies Signatures by weighing other Classifier results.

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>WeightedClassifier (  )</sub>| <sub></sub>| <br>


## Fields

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>Items</sub>| <sub>List of classifiers and belonging weights.</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Add](./Methods/WeightedClassifier-100663912.md) ( [`ValueTuple`](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple-2)\<[`IClassifier`](./../../Pipeline/IClassifier.md), [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> )</sub>| <sub>Add a new classifier with given weight to the list of items.</sub>| <br>
| <sub>[GetEnumerator](./Methods/WeightedClassifier-100663911.md) (  )</sub>| <sub></sub>| <br>
| <sub>[Test](./Methods/WeightedClassifier-100663914.md) ( [`ISignerModel`](./../../Pipeline/ISignerModel.md), [`Signature`](./../../Signature.md) )</sub>| <sub></sub>| <br>
| <sub>[Train](./Methods/WeightedClassifier-100663913.md) ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> )</sub>| <sub></sub>| <br>


