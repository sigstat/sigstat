# [Map](./Map.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Maps values of a feature to a specified range.  <br>Pipeline Input type: List{double}<br>Default Pipeline Output: (List{double}) MapResult

## Constructors

| Name | Summary | 
| --- | --- | 
| Map ( [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double) minVal, [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double) maxVal ) | Initializes a new instance of the [SigStat.Common.Transforms.Map](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/Map.md) class with specified settings. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | Input | Input | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | Output | Output | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../Signature.md) signature ) |  | 


