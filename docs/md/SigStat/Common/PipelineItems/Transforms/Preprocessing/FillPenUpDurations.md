# [FillPenUpDurations](./FillPenUpDurations.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
This transformation will fill "holes" in the "Time" feature by interpolating the last known  feature values.

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| FillPenUpDurations () |  | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| InputFeatures | Gets or sets the features of an online signature that need to be altered | 
| InterpolationType | An implementation of [IInterpolation](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Transforms/Preprocessing/IInterpolation.md) | 
| OutputFeatures | Gets or sets the features of an online signature that were altered | 
| TimeInputFeature | Gets or sets the feature representing the timestamps of an online signature | 
| TimeOutputFeature | Gets or sets the feature representing the modified timestamps of an online signature | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Transform](./Methods/FillPenUpDurations--Transform.md) ([`Signature`](./../../../Signature.md) ) |  | 


