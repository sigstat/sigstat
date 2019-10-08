# [ResampleSamplesCountBased](./ResampleSamplesCountBased.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Resamples an online signature to a specific sample count using the specified [PipelineItems.Transforms.Preprocessing.IInterpolation](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Transforms/Preprocessing/IInterpolation.md) algorithm

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>ResampleSamplesCountBased (  )</sub><img width=200/>| <sub></sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>InputFeatures</sub><img width=200/>| <sub>Gets or sets the input features.</sub>| <br>
| <sub>InterpolationType</sub><img width=200/>| <sub>Gets or sets the type of the interpolation. <seealso cref="T:SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation" /></sub>| <br>
| <sub>NumOfSamples</sub><img width=200/>| <sub>Gets or sets the number of samples.</sub>| <br>
| <sub>OriginalTFeature</sub><img width=200/>| <sub>Gets or sets the input timestamp feature.</sub>| <br>
| <sub>OutputFeatures</sub><img width=200/>| <sub>Gets or sets the resampled  features.</sub>| <br>
| <sub>ResampledTFeature</sub><img width=200/>| <sub>Gets or sets the resampled timestamp feature.</sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[Transform](./Methods/ResampleSamplesCountBased-100663803.md) ( [`Signature`](./../../../Signature.md) )</sub><img width=200/>| <sub></sub>| <br>


