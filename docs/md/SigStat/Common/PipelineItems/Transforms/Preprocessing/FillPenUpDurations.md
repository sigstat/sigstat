# [FillPenUpDurations](./FillPenUpDurations.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
This transformation will fill "holes" in the "Time" feature by interpolating the last known  feature values.

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>FillPenUpDurations (  )</sub>| <sub></sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>InputFeatures</sub>| <sub>Gets or sets the features of an online signature that need to be altered</sub>| <br>
| <sub>InterpolationType</sub>| <sub>An implementation of [IInterpolation](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Transforms/Preprocessing/IInterpolation.md)</sub>| <br>
| <sub>OutputFeatures</sub>| <sub>Gets or sets the features of an online signature that were altered</sub>| <br>
| <sub>TimeInputFeature</sub>| <sub>Gets or sets the feature representing the timestamps of an online signature</sub>| <br>
| <sub>TimeOutputFeature</sub>| <sub>Gets or sets the feature representing the modified timestamps of an online signature</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Transform](./Methods/FillPenUpDurations-100663741.md) ( [`Signature`](./../../../Signature.md) )</sub>| <sub></sub>| <br>


