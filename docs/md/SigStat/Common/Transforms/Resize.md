# [Resize](./Resize.md)

Namespace: [SigStat]() > [Common]() > [Transforms]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Resizes the image to a specified width and height

## Constructors

| Name | Summary | 
| --- | --- | 
| Resize (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Nullable](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1)\<[Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)> | Height | The new height. Leave it as null, if you do not want to explicitly specify a given height | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Image](./Resize.md)\<[Rgba32](./Resize.md)>> | InputImage | Input `SigStat.Common.FeatureDescriptor` describing the image of the signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Image](./Resize.md)\<[Rgba32](./Resize.md)>> | OutputImage | Output `SigStat.Common.FeatureDescriptor` describing the resized image of the signature | 
| [Func](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2)\<[Image](./Resize.md)\<[Rgba32](./Resize.md)>, [Size](./Resize.md)> | ResizeFunction | Set a resize function if you want to dynamically calculate the new width and height of the image | 
| [Nullable](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1)\<[Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)> | Width | The new width. Leave it as null, if you do not want to explicitly specify a given width | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../Signature.md) ) |  | 


