# [UniformScale](./UniformScale.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Maps values of a feature to a specific range and another proportional.  <br>BaseDimension: feature modelled the base dimension of the scaling. <br>ProportionalDimension: feature modelled the dimension scaled proportionally to the base dimension. <br>BaseDimensionOutput: output feature for scaled BaseDimension&gt;<br>ProportionalDimensionOutput: output feature for scaled ProportionalDimension&gt;

## Constructors

| Name | Summary | 
| --- | --- | 
| UniformScale (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | BaseDimension | Gets or sets the base dimension. | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | BaseDimensionOutput | Gets or sets the output base dimension output. | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | NewMaxBaseValue | Upper bound of the interval, in which the base dimension will be scaled | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | NewMinBaseValue | Lower bound of the interval, in which the base dimension will be scaled | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | NewMinProportionalValue | Lower bound of the interval, in which the proportional dimension will be scaled | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | ProportionalDimension | Gets or sets the ProportionalDimension. | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | ProportionalDimensionOutput | Gets or sets the output proportional dimension output. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../../../Signature.md) signature ) |  | 


