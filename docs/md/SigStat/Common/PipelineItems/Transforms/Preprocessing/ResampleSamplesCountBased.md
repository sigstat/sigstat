# [ResampleSamplesCountBased](./ResampleSamplesCountBased.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Resamples an online signature to a specific sample count using the specified [IInterpolation](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Transforms/Preprocessing/IInterpolation.md) algorithm

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>ResampleSamplesCountBased (  )</sub>| <sub></sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>InputFeatures</sub>| <sub>Gets or sets the input features.</sub>| <br>
| <sub>InterpolationType</sub>| <sub>Gets or sets the type of the interpolation. <seealso cref="T:SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation" /></sub>| <br>
| <sub>NumOfSamples</sub>| <sub>Gets or sets the number of samples.</sub>| <br>
| <sub>OriginalTFeature</sub>| <sub>Gets or sets the input timestamp feature.</sub>| <br>
| <sub>OutputFeatures</sub>| <sub>Gets or sets the resampled  features.</sub>| <br>
| <sub>ResampledTFeature</sub>| <sub>Gets or sets the resampled timestamp feature.</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Transform](./Methods/ResampleSamplesCountBased-100663829.md) ( [`Signature`](./../../../Signature.md) )</sub>| <sub></sub>| <br>


