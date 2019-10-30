# [UniformScale](./UniformScale.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Maps values of a feature to a specific range and another proportional.  <br>BaseDimension: feature modelled the base dimension of the scaling. <br>ProportionalDimension: feature modelled the dimension scaled proportionally to the base dimension. <br>BaseDimensionOutput: output feature for scaled BaseDimension<br>ProportionalDimensionOutput: output feature for scaled ProportionalDimension

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| UniformScale () |  | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| BaseDimension | Gets or sets the base dimension. | 
| BaseDimensionOutput | Gets or sets the output base dimension output. | 
| NewMaxBaseValue | Upper bound of the interval, in which the base dimension will be scaled | 
| NewMinBaseValue | Lower bound of the interval, in which the base dimension will be scaled | 
| NewMinProportionalValue | Lower bound of the interval, in which the proportional dimension will be scaled | 
| ProportionalDimension | Gets or sets the ProportionalDimension. | 
| ProportionalDimensionOutput | Gets or sets the output proportional dimension output. | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Transform](./Methods/UniformScale--Transform.md) ([`Signature`](./../../../Signature.md) ) |  | 


