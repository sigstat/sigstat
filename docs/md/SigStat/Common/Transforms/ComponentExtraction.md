# [ComponentExtraction](./ComponentExtraction.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Extracts unsorted components by tracing through the binary Skeleton raster.  <br>Default Pipeline Input: (bool[,]) Skeleton, (List{Point}) EndPoints, (List{Point}) CrossingPoints<br>Default Pipeline Output: (List{List{PointF}}) Components

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>ComponentExtraction ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) )</sub><img width=200/>| <sub>Initializes a new instance of the [Transforms.ComponentExtraction](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/ComponentExtraction.md) class with specified sampling resolution.</sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>CrossingPoints</sub><img width=200/>| <sub>crossing points</sub>| <br>
| <sub>EndPoints</sub><img width=200/>| <sub>endpoints</sub>| <br>
| <sub>OutputComponents</sub><img width=200/>| <sub>Output components</sub>| <br>
| <sub>Skeleton</sub><img width=200/>| <sub>binary representation of a signature image</sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[Transform](./Methods/ComponentExtraction-100663567.md) ( [`Signature`](./../Signature.md) )</sub><img width=200/>| <sub></sub>| <br>


