# [BinaryRasterizer](./BinaryRasterizer.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Converts standard features to a binary raster.  <br>Default Pipeline Input: Standard [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: (bool[,]) Binarized

## Constructors

| Name | Summary | 
| --- | --- | 
| BinaryRasterizer ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) resolutionX, [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) resolutionY, [`Single`](https://docs.microsoft.com/en-us/dotnet/api/System.Single) penWidth ) | Initializes a new instance of the [SigStat.Common.Transforms.BinaryRasterizer](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/BinaryRasterizer.md) class with specified raster size and pen width. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)>> | InputButton | Gets or sets the [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the stroke endings of an online signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputX | Gets or sets the [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the X coordinates of an online signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputY | Gets or sets the [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the Y coordinates of an online signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)[]> | Output | Gets or sets the [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the output of the transformation | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../Signature.md) signature ) |  | 


