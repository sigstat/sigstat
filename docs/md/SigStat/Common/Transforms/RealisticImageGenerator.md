# [RealisticImageGenerator](./RealisticImageGenerator.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Generates a realistic looking image of the Signature based on standard features. Uses blue ink and white paper. It does NOT save the image to file.  <br>Default Pipeline Input: X, Y, Button, Pressure, Azimuth, Altitude [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: [SigStat.Common.Features.Image]()

## Constructors

| Name | Summary | 
| --- | --- | 
| RealisticImageGenerator ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) resolutionX, [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) resolutionY ) | Initializes a new instance of the [SigStat.Common.Transforms.RealisticImageGenerator](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/RealisticImageGenerator.md) class with specified settings. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../Signature.md) signature ) |  | 


