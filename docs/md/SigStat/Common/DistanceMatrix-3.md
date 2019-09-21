# [DistanceMatrix](./DistanceMatrix-3.md)\<[TRowKey](./DistanceMatrix-3.md), [TColumnKey](./DistanceMatrix-3.md), [TValue](./DistanceMatrix-3.md)>

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

## Summary
A Sparse Matrix representation of a distance graph.

## Constructors

| Name | Summary | 
| --- | --- | 
| DistanceMatrix (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [TValue](./DistanceMatrix-3.md) | Item [ [`TRowKey`](./DistanceMatrix-3.md) ] | Gets or sets a distance for a given row and column | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | ContainsKey ( [`TRowKey`](./DistanceMatrix-3.md), [`TColumnKey`](./DistanceMatrix-3.md) ) |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | TryGetValue ( [`TRowKey`](./DistanceMatrix-3.md), [`TColumnKey`](./DistanceMatrix-3.md), out [`TValue`](./DistanceMatrix-3.md) ) |  | 


