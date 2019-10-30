# [ImageGenerator](./ImageGenerator.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Generates an image feature out of a binary raster.  Optionally, saves the image to a png file.  Useful for debugging pipeline steps.  <br>Pipeline Input type: bool[,]<br>Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| ImageGenerator () | Initializes a new instance of the [ImageGenerator](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/ImageGenerator.md) class with default settings: skip file writing, Blue ink on white paper. | 
| ImageGenerator ([`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)) | Initializes a new instance of the [ImageGenerator](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/ImageGenerator.md) class with default settings. | 
| ImageGenerator ([`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean), [`Rgba32`](./ImageGenerator.md), [`Rgba32`](./ImageGenerator.md)) | Initializes a new instance of the [ImageGenerator](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/ImageGenerator.md) class with specified settings. | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| BackgroundColor | Gets or sets the color of the backgroung used to render the signature | 
| ForegroundColor | Gets or sets the color of the foreground used to render the signature | 
| Input | Input [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) for the binary image of a signature | 
| OutputImage | Input [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) for the binary image of a signature | 
| WriteToFile | Gets or sets a value indicating whether the results should be saved to a file or not. | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Transform](./Methods/ImageGenerator--Transform.md) ( [`Signature`](./../Signature.md) ) |  | 


