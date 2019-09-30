# [TranslatePreproc](./TranslatePreproc.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
This transformations can be used to translate the coordinates of an online signature

## Constructors

| Name | Summary | 
| --- | --- | 
| TranslatePreproc (  ) | Initializes a new instance of the [SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc]() class. | 
| TranslatePreproc ( [`OriginType`](./OriginType.md) goalOrigin ) | Initializes a new instance of the [SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc]() class. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [OriginType](./OriginType.md) | GoalOrigin | Goal origin of the translation | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputFeature | Input [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor.md) (e.g. [SigStat.Common.Features.X](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md)) | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | NewOrigin | New origin after the translation | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | OutputFeature | Output [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor.md) (e.g. [SigStat.Common.Features.X](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md)) | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../../../Signature.md) signature ) |  | 


