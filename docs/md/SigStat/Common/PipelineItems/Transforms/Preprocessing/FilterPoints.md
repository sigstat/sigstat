# [FilterPoints](./FilterPoints.md)

Namespace: [SigStat]() > [Common](./../../../README.md) > [PipelineItems]() > [Transforms]() > [Preprocessing](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../../../ILoggerObject.md), [IProgress](./../../../Helpers/IProgress.md), [IPipelineIO](./../../../Pipeline/IPipelineIO.md), [ITransformation](./../../../ITransformation.md)

## Summary
Removes samples based on a criteria from online signature time series

## Constructors

| Name | Summary | 
| --- | --- | 
| FilterPoints (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>>> | InputFeatures | [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor.md) list of all features to resample | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | KeyFeatureInput | [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor.md) that controls the removal of samples (e.g. [SigStat.Common.Features.Pressure](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md)) | 
| [FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | KeyFeatureOutput | Resampled output for [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor.md) that controls the removal of samples (e.g. [SigStat.Common.Features.Pressure](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md)) | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[FeatureDescriptor](./../../../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>>> | OutputFeatures | Resampled output for all input features | 
| [Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) | Percentile | The lowes percentile of the [SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints.KeyFeatureInput]() will be removed during filtering | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../../../Signature.md) signature ) |  | 


