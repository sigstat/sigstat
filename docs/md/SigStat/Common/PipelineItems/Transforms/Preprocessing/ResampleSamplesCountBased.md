# [ResampleSamplesCountBased](./ResampleSamplesCountBased.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Resamples an online signature to a specific sample count using the specified [IInterpolation](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Transforms/Preprocessing/IInterpolation.md) algorithm

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| ResampleSamplesCountBased () |  | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| InputFeatures | Gets or sets the input features. | 
| InterpolationType | Gets or sets the type of the interpolation. <seealso cref="T:SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation" /> | 
| NumOfSamples | Gets or sets the number of samples. | 
| OriginalTFeature | Gets or sets the input timestamp feature. | 
| OutputFeatures | Gets or sets the resampled  features. | 
| ResampledTFeature | Gets or sets the resampled timestamp feature. | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Transform](./Methods/ResampleSamplesCountBased--Transform.md) ([`Signature`](./../../../Signature.md) ) |  | 


