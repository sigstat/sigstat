# [TranslatePreproc](./TranslatePreproc.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
This transformations can be used to translate the coordinates of an online signature

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>TranslatePreproc (  )</sub>| <sub>Initializes a new instance of the [TranslatePreproc](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Transforms/Preprocessing/TranslatePreproc.md) class.</sub>| <br>
| <sub>TranslatePreproc ( [`OriginType`](./OriginType.md) )</sub>| <sub>Initializes a new instance of the [TranslatePreproc](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Transforms/Preprocessing/TranslatePreproc.md) class.</sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>GoalOrigin</sub>| <sub>Goal origin of the translation</sub>| <br>
| <sub>InputFeature</sub>| <sub>Input [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) (e.g. [X](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md))</sub>| <br>
| <sub>NewOrigin</sub>| <sub>New origin after the translation</sub>| <br>
| <sub>OutputFeature</sub>| <sub>Output [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) (e.g. [X](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md))</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Transform](./Methods/TranslatePreproc-100663868.md) ( [`Signature`](./../../../Signature.md) )</sub>| <sub></sub>| <br>


