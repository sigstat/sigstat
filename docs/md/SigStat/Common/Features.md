# [Features](./Features.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

## Summary
Standard set of features.

## Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| [IReadOnlyList](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyList-1)\<[FeatureDescriptor](./FeatureDescriptor.md)> | All | Returns a readonly list of all `SigStat.Common.FeatureDescriptor`s defined in `SigStat.Common.Features` | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | Altitude | Altitude of an online signature as a function of `SigStat.Common.Features.T` | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | Azimuth | Azimuth of an online signature as a function of `SigStat.Common.Features.T` | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[RectangleF](https://docs.microsoft.com/en-us/dotnet/api/System.Drawing.RectangleF)> | Bounds | Actual bounds of the signature | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)>> | Button | Pen position of an online signature as a function of `SigStat.Common.Features.T` | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Drawing.Point)> | Cog | Center of gravity in a signature | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)> | Dpi | Dots per inch | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[Image](./Features.md)\<[Rgba32](./Features.md)>> | Image | The visaul representation of a signature | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | Pressure | Pressure of an online signature as a function of `SigStat.Common.Features.T` | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | T | Timestamps for online signatures | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[Rectangle](https://docs.microsoft.com/en-us/dotnet/api/System.Drawing.Rectangle)> | TrimmedBounds | Represents the main body of the signature `SigStat.Common.BasicMetadataExtraction` | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | X | X coordinates of an online signature as a function of `SigStat.Common.Features.T` | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | Y | Y coordinates of an online signature as a function of `SigStat.Common.Features.T` | 


