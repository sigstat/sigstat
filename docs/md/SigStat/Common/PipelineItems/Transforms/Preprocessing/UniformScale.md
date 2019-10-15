# [UniformScale](./UniformScale.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Maps values of a feature to a specific range and another proportional.  <br>BaseDimension: feature modelled the base dimension of the scaling. <br>ProportionalDimension: feature modelled the dimension scaled proportionally to the base dimension. <br>BaseDimensionOutput: output feature for scaled BaseDimension<br>ProportionalDimensionOutput: output feature for scaled ProportionalDimension

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>UniformScale (  )</sub>| <sub></sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>BaseDimension</sub>| <sub>Gets or sets the base dimension.</sub>| <br>
| <sub>BaseDimensionOutput</sub>| <sub>Gets or sets the output base dimension output.</sub>| <br>
| <sub>NewMaxBaseValue</sub>| <sub>Upper bound of the interval, in which the base dimension will be scaled</sub>| <br>
| <sub>NewMinBaseValue</sub>| <sub>Lower bound of the interval, in which the base dimension will be scaled</sub>| <br>
| <sub>NewMinProportionalValue</sub>| <sub>Lower bound of the interval, in which the proportional dimension will be scaled</sub>| <br>
| <sub>ProportionalDimension</sub>| <sub>Gets or sets the ProportionalDimension.</sub>| <br>
| <sub>ProportionalDimensionOutput</sub>| <sub>Gets or sets the output proportional dimension output.</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Transform](./Methods/UniformScale-100663886.md) ( [`Signature`](./../../../Signature.md) )</sub>| <sub></sub>| <br>


