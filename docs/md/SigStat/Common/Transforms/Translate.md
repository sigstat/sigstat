# [Translate](./Translate.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [ITransformation](./../ITransformation.md)

## Summary
Sequential pipeline to translate X and Y [Features](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md) by specified vector (constant or feature).  The following Transforms are called: [Transforms.AddConst](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/AddConst.md) twice, or [Transforms.AddVector](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/AddVector.md).  <br>Default Pipeline Input: [Features.X](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md), [Features.Y](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: [Features.X](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md), [Features.Y](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>Translate ( [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double), [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double) )</sub><img width=200/>| <sub></sub>| <br>
| <sub>Translate ( [`FeatureDescriptor`](./../FeatureDescriptor-1.md)\<[`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> )</sub><img width=200/>| <sub></sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>InputX</sub><img width=200/>| <sub>The feature representing the horizontal coordinates of an online signature</sub>| <br>
| <sub>InputY</sub><img width=200/>| <sub>The feature representing the vertical coordinates of an online signature</sub>| <br>
| <sub>OutputX</sub><img width=200/>| <sub>Target feature for storing the transformed horizontal coordinates</sub>| <br>
| <sub>OutputY</sub><img width=200/>| <sub>Target feature for storing the transformed vertical coordinates</sub>| <br>


