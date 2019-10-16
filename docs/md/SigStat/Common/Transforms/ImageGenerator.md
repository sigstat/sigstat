# [ImageGenerator](./ImageGenerator.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Generates an image feature out of a binary raster.  Optionally, saves the image to a png file.  Useful for debugging pipeline steps.  <br>Pipeline Input type: bool[,]<br>Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>ImageGenerator (  )</sub>| <sub>Initializes a new instance of the [ImageGenerator](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/ImageGenerator.md) class with default settings: skip file writing, Blue ink on white paper.</sub>| <br>
| <sub>ImageGenerator ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) )</sub>| <sub>Initializes a new instance of the [ImageGenerator](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/ImageGenerator.md) class with default settings.</sub>| <br>
| <sub>ImageGenerator ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean), [`Rgba32`](./ImageGenerator.md), [`Rgba32`](./ImageGenerator.md) )</sub>| <sub>Initializes a new instance of the [ImageGenerator](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/ImageGenerator.md) class with specified settings.</sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>BackgroundColor</sub>| <sub>Gets or sets the color of the backgroung used to render the signature</sub>| <br>
| <sub>ForegroundColor</sub>| <sub>Gets or sets the color of the foreground used to render the signature</sub>| <br>
| <sub>Input</sub>| <sub>Input [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) for the binary image of a signature</sub>| <br>
| <sub>OutputImage</sub>| <sub>Input [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) for the binary image of a signature</sub>| <br>
| <sub>WriteToFile</sub>| <sub>Gets or sets a value indicating whether the results should be saved to a file or not.</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Transform](./Methods/ImageGenerator-100663679.md) ( [`Signature`](./../Signature.md) )</sub>| <sub></sub>| <br>


