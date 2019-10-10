# [BinaryRasterizer](./BinaryRasterizer.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Converts standard features to a binary raster.  <br>Default Pipeline Input: Standard [Features](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: (bool[,]) Binarized

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>BinaryRasterizer ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32), [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32), [`Single`](https://docs.microsoft.com/en-us/dotnet/api/System.Single) )</sub><img width=200/>| <sub>Initializes a new instance of the [Transforms.BinaryRasterizer](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/BinaryRasterizer.md) class with specified raster size and pen width.</sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>InputButton</sub><img width=200/>| <sub>Gets or sets the [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the stroke endings of an online signature</sub>| <br>
| <sub>InputX</sub><img width=200/>| <sub>Gets or sets the [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the X coordinates of an online signature</sub>| <br>
| <sub>InputY</sub><img width=200/>| <sub>Gets or sets the [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the Y coordinates of an online signature</sub>| <br>
| <sub>Output</sub><img width=200/>| <sub>Gets or sets the [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) representing the output of the transformation</sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[Transform](./Methods/BinaryRasterizer-100663656.md) ( [`Signature`](./../Signature.md) )</sub><img width=200/>| <sub></sub>| <br>


