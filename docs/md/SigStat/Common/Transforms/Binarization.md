# [Binarization](./Binarization.md)

Namespace: [SigStat]() > [Common]() > [Transforms]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Generates a binary raster version of the input image with the iterative threshold method.  <para>Pipeline Input type: Image{Rgba32}</para><para>Default Pipeline Output: (bool[,]) Binarized</para>

## Constructors

| Name | Summary | 
| --- | --- | 
| Binarization (  ) | Initializes a new instance of the `SigStat.Common.Transforms.Binarization` class with default settings: Iterative threshold and `SigStat.Common.Transforms.Binarization.ForegroundType.Dark`. | 
| Binarization ( [`ForegroundType`](./Binarization.md), [`Nullable`](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1)\<[`Double`](https://docs.microsoft.com/en-us/dotnet/api/System.Double)> ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Image](./Binarization.md)\<[Rgba32](./Binarization.md)>> | InputImage |  | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)[]> | OutputMask |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Transform ( [`Signature`](./../Signature.md) ) |  | 


