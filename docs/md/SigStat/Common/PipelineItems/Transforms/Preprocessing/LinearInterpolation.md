# [LinearInterpolation](./LinearInterpolation.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [IInterpolation](./IInterpolation.md)

## Summary
Performs linear interpolation on the input

## Constructors

| Name | Summary | 
| --- | --- | 
| LinearInterpolation (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> | FeatureValues |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> | TimeValues |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | GetValue ( [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double) timestamp ) | Gets the interpolated value at a given timestamp | 


