# [Translate](./Translate.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [ITransformation](./../ITransformation.md)

## Summary
Sequential pipeline to translate X and Y [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Features.md) by specified vector (constant or feature).  The following Transforms are called: [SigStat.Common.Transforms.AddConst](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/AddConst.md) twice, or [SigStat.Common.Transforms.AddVector](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/AddVector.md).  <br>Default Pipeline Input: [SigStat.Common.Features.X](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md), [SigStat.Common.Features.Y](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md)<br>Default Pipeline Output: [SigStat.Common.Features.X](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md), [SigStat.Common.Features.Y](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md)

## Constructors

| Name | Summary | 
| --- | --- | 
| Translate ( [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double) xAdd, [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double) yAdd ) |  | 
| Translate ( [`FeatureDescriptor`](./../FeatureDescriptor-1.md)\<[`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> vectorFeature ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputX | The feature representing the horizontal coordinates of an online signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputY | The feature representing the vertical coordinates of an online signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | OutputX | Target feature for storing the transformed horizontal coordinates | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | OutputY | Target feature for storing the transformed vertical coordinates | 


