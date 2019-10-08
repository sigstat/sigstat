# [FilterPoints](./FilterPoints.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Removes samples based on a criteria from online signature time series

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>FilterPoints (  )</sub><img width=200/>| <sub></sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>InputFeatures</sub><img width=200/>| <sub>[FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) list of all features to resample</sub>| <br>
| <sub>KeyFeatureInput</sub><img width=200/>| <sub>[FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) that controls the removal of samples (e.g. [Features.Pressure](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md))</sub>| <br>
| <sub>KeyFeatureOutput</sub><img width=200/>| <sub>Resampled output for [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) that controls the removal of samples (e.g. [Features.Pressure](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md))</sub>| <br>
| <sub>OutputFeatures</sub><img width=200/>| <sub>Resampled output for all input features</sub>| <br>
| <sub>Percentile</sub><img width=200/>| <sub>The lowes percentile of the [PipelineItems.Transforms.Preprocessing.FilterPoints.KeyFeatureInput](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Transforms/Preprocessing/FilterPoints.md) will be removed during filtering</sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[Transform](./Methods/FilterPoints-100663755.md) ( [`Signature`](./../../../Signature.md) )</sub><img width=200/>| <sub></sub>| <br>


