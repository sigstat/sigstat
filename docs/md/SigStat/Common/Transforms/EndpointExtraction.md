# [EndpointExtraction](./EndpointExtraction.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Extracts EndPoints and CrossingPoints from Skeleton.  <para>Default Pipeline Input: (bool[,]) Skeleton</para><para>Default Pipeline Output: (List{Point}) EndPoints, (List{Point}) CrossingPoints </para>

## Constructors

| Name | Summary | 
| --- | --- | 
| EndpointExtraction (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Drawing.Point)>> | OutputCrossingPoints | OutputCrossingPoints | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Drawing.Point)>> | OutputEndpoints | OutputEndpoints | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)[]> | Skeleton | Binary representation of an image | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../Signature.md) ) |  | 


