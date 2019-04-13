# [ImageGenerator](./ImageGenerator.md)

Namespace: [SigStat]() > [Common]() > [Transforms]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Generates an image feature out of a binary raster.  Optionally, saves the image to a png file.  Useful for debugging pipeline steps.  <para>Pipeline Input type: bool[,]</para><para>Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage</para>

## Constructors

| Name | Summary | 
| --- | --- | 
| ImageGenerator (  ) | Initializes a new instance of the `SigStat.Common.Transforms.ImageGenerator` class with default settings: skip file writing, Blue ink on white paper. | 
| ImageGenerator ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) ) | Initializes a new instance of the `SigStat.Common.Transforms.ImageGenerator` class with default settings. | 
| ImageGenerator ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean), [`Rgba32`](./ImageGenerator.md), [`Rgba32`](./ImageGenerator.md) ) | Initializes a new instance of the `SigStat.Common.Transforms.ImageGenerator` class with specified settings. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Rgba32](./ImageGenerator.md) | BackgroundColor |  | 
| [Rgba32](./ImageGenerator.md) | ForegroundColor |  | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)[]> | Input |  | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)[]> | Output |  | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Image](./ImageGenerator.md)\<[Rgba32](./ImageGenerator.md)>> | OutputImage |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | WriteToFile |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../Signature.md) ) |  | 


