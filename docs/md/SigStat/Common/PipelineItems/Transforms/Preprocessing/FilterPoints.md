# [FilterPoints](./FilterPoints.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Removes samples based on a criteria from online signature time series

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>FilterPoints (  )</sub>| <sub></sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>InputFeatures</sub>| <sub>[FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) list of all features to resample</sub>| <br>
| <sub>KeyFeatureInput</sub>| <sub>[FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) that controls the removal of samples (e.g. [Pressure](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md))</sub>| <br>
| <sub>KeyFeatureOutput</sub>| <sub>Resampled output for [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) that controls the removal of samples (e.g. [Pressure](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md))</sub>| <br>
| <sub>OutputFeatures</sub>| <sub>Resampled output for all input features</sub>| <br>
| <sub>Percentile</sub>| <sub>The lowes percentile of the [KeyFeatureInput](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Transforms/Preprocessing/FilterPoints.md) will be removed during filtering</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Transform](./Methods/FilterPoints-100663755.md) ( [`Signature`](./../../../Signature.md) )</sub>| <sub></sub>| <br>


