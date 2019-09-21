# [Scale](./Scale.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Maps values of a feature to a specific range.  <para>InputFeature: feature to be scaled.</para><para>OutputFeature: output feature for scaled InputFeature&gt;</para>

## Constructors

| Name | Summary | 
| --- | --- | 
| Scale (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputFeature | Gets or sets the input feature. | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | NewMaxValue | <para>NewMaxValue: upper bound of the interval, in which the input feature will be scaled</para> | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | NewMinValue | <para>NewMinValue: lower bound of the interval, in which the input feature will be scaled</para> | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | OutputFeature | Gets or sets the output feature. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../../../Signature.md) ) |  | 


