# [Multiply](./Multiply.md)

Namespace: [SigStat]() > [Common]() > [Transforms]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Multiplies the values of a feature with a given constant.  <para>Pipeline Input type: List{double}</para><para>Default Pipeline Output: (List{double}) Input</para>

## Constructors

| Name | Summary | 
| --- | --- | 
| Multiply ( [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double) ) | Initializes a new instance of the `SigStat.Common.Transforms.Multiply` class with specified settings. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputList |  | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | Output |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../Signature.md) ) |  | 


