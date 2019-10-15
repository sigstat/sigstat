# [Translate](./Translate.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [ITransformation](./../ITransformation.md)

## Summary
Sequential pipeline to translate X and Y [Features](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md) by specified vector (constant or feature).  The following Transforms are called: [AddConst](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/AddConst.md) twice, or [AddVector](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/AddVector.md).  <br>Default Pipeline Input: [X](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md), [Y](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: [X](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md), [Y](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>Translate ( [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double), [`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double) )</sub>| <sub></sub>| <br>
| <sub>Translate ( [`FeatureDescriptor`](./../FeatureDescriptor-1.md)\<[`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> )</sub>| <sub></sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>InputX</sub>| <sub>The feature representing the horizontal coordinates of an online signature</sub>| <br>
| <sub>InputY</sub>| <sub>The feature representing the vertical coordinates of an online signature</sub>| <br>
| <sub>OutputX</sub>| <sub>Target feature for storing the transformed horizontal coordinates</sub>| <br>
| <sub>OutputY</sub>| <sub>Target feature for storing the transformed vertical coordinates</sub>| <br>


