# [BinaryRasterizer](./BinaryRasterizer.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Converts standard features to a binary raster.  <br>Default Pipeline Input: Standard [Features](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: (bool[,]) Binarized

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| BinaryRasterizer ([`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32), [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32), [`Single`](https://docs.microsoft.com/en-us/dotnet/api/System.Single)) | Initializes a new instance of the [BinaryRasterizer](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/BinaryRasterizer.md) class with specified raster size and pen width. | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| InputButton | Gets or sets the [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the stroke endings of an online signature | 
| InputX | Gets or sets the [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the X coordinates of an online signature | 
| InputY | Gets or sets the [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the Y coordinates of an online signature | 
| Output | Gets or sets the [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the output of the transformation | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Transform](./Methods/BinaryRasterizer--Transform.md) ([`Signature`](./../Signature.md) ) |  | 


