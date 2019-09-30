# [ImageGenerator](./ImageGenerator.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Generates an image feature out of a binary raster.  Optionally, saves the image to a png file.  Useful for debugging pipeline steps.  <br>Pipeline Input type: bool[,]<br>Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage

## Constructors

| Name | Summary | 
| --- | --- | 
| ImageGenerator (  ) | Initializes a new instance of the [SigStat.Common.Transforms.ImageGenerator](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Transforms/ImageGenerator.md) class with default settings: skip file writing, Blue ink on white paper. | 
| ImageGenerator ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) writeToFile ) | Initializes a new instance of the [SigStat.Common.Transforms.ImageGenerator](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Transforms/ImageGenerator.md) class with default settings. | 
| ImageGenerator ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) writeToFile, [`Rgba32`](./ImageGenerator.md) foregroundColor, [`Rgba32`](./ImageGenerator.md) backgroundColor ) | Initializes a new instance of the [SigStat.Common.Transforms.ImageGenerator](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Transforms/ImageGenerator.md) class with specified settings. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Rgba32](./ImageGenerator.md) | BackgroundColor | Gets or sets the color of the backgroung used to render the signature | 
| [Rgba32](./ImageGenerator.md) | ForegroundColor | Gets or sets the color of the foreground used to render the signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)[]> | Input | Input [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor.md) for the binary image of a signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Image](./ImageGenerator.md)\<[Rgba32](./ImageGenerator.md)>> | OutputImage | Input [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor.md) for the binary image of a signature | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | WriteToFile | Gets or sets a value indicating whether the results should be saved to a file or not. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../Signature.md) signature ) |  | 


