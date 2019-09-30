# [WeightedClassifier](./WeightedClassifier.md)

Namespace: [SigStat]() > [Common](./../../README.md) > [PipelineItems]() > [Classifiers](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../ILoggerObject.md), [IProgress](./../../Helpers/IProgress.md), [IPipelineIO](./../../Pipeline/IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [IClassifier](./../../Pipeline/IClassifier.md)

## Summary
Classifies Signatures by weighing other Classifier results.

## Constructors

| Name | Summary | 
| --- | --- | 
| WeightedClassifier (  ) |  | 


## Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[ValueTuple](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple-2)\<[IClassifier](./../../Pipeline/IClassifier.md), [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | Items | List of classifiers and belonging weights. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Add ( [`ValueTuple`](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple-2)\<[`IClassifier`](./../../Pipeline/IClassifier.md), [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> newItem ) | Add a new classifier with given weight to the list of items. | 
| [IEnumerator](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerator) | GetEnumerator (  ) |  | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | Test ( [`ISignerModel`](./../../Pipeline/ISignerModel.md) model, [`Signature`](./../../Signature.md) signature ) |  | 
| [ISignerModel](./../../Pipeline/ISignerModel.md) | Train ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> signatures ) |  | 


