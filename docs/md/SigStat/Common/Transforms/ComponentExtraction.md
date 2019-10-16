# [ComponentExtraction](./ComponentExtraction.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Extracts unsorted components by tracing through the binary Skeleton raster.  <br>Default Pipeline Input: (bool[,]) Skeleton, (List{Point}) EndPoints, (List{Point}) CrossingPoints<br>Default Pipeline Output: (List{List{PointF}}) Components

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>ComponentExtraction ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) )</sub>| <sub>Initializes a new instance of the [ComponentExtraction](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/ComponentExtraction.md) class with specified sampling resolution.</sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>CrossingPoints</sub>| <sub>crossing points</sub>| <br>
| <sub>EndPoints</sub>| <sub>endpoints</sub>| <br>
| <sub>OutputComponents</sub>| <sub>Output components</sub>| <br>
| <sub>Skeleton</sub>| <sub>binary representation of a signature image</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Transform](./Methods/ComponentExtraction-100663567.md) ( [`Signature`](./../Signature.md) )</sub>| <sub></sub>| <br>


