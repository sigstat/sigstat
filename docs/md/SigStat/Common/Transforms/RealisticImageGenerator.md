# [RealisticImageGenerator](./RealisticImageGenerator.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Generates a realistic looking image of the Signature based on standard features. Uses blue ink and white paper. It does NOT save the image to file.  <br>Default Pipeline Input: X, Y, Button, Pressure, Azimuth, Altitude [Features](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: [Image](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| RealisticImageGenerator ([`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32), [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)) | Initializes a new instance of the [RealisticImageGenerator](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/RealisticImageGenerator.md) class with specified settings. | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Transform](./Methods/RealisticImageGenerator--Transform.md) ([`Signature`](./../Signature.md) ) |  | 


