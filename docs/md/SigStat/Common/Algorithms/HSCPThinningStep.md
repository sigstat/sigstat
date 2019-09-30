# [HSCPThinningStep](./HSCPThinningStep.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Algorithms](./README.md)

Assembly: SigStat.Common.dll

## Summary
HSCP thinning algorithm  http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf

## Constructors

| Name | Summary | 
| --- | --- | 
| HSCPThinningStep (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Nullable](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1)\<[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)> | ResultChanged | Gets whether the last [SigStat.Common.Algorithms.HSCPThinningStep.Scan(System.Boolean[0:,0:])]() call was effective. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)[] | Scan ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)[] b ) | Does one step of the thinning. Call it iteratively while ResultChanged. | 


