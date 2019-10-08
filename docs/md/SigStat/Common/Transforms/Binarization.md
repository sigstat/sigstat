# [Binarization](./Binarization.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Generates a binary raster version of the input image with the iterative threshold method.  <br>Pipeline Input type: Image{Rgba32}<br>Default Pipeline Output: (bool[,]) Binarized

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>Binarization (  )</sub><img width=200/>| <sub>Initializes a new instance of the [Transforms.Binarization](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/Binarization.md) class with default settings: Iterative threshold and [Transforms.Binarization.ForegroundType.Dark](https://github.com/sigstat/sigstat/blob/develop/docs/md/.md).</sub>| <br>
| <sub>Binarization ( [`ForegroundType`](./Binarization.md), [`Nullable`](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> )</sub><img width=200/>| <sub></sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>InputImage</sub><img width=200/>| <sub>Gets or sets the featuredescriptor of the input image.</sub>| <br>
| <sub>OutputMask</sub><img width=200/>| <sub>Gets or sets the featuredescriptor of a the binarized image.</sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[Transform](./Methods/Binarization-100663644.md) ( [`Signature`](./../Signature.md) )</sub><img width=200/>| <sub></sub>| <br>


