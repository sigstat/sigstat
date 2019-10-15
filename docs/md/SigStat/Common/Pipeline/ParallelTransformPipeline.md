# [ParallelTransformPipeline](./ParallelTransformPipeline.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Pipeline](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [ITransformation](./../ITransformation.md)

## Summary
Runs pipeline items in parallel.  <br>Default Pipeline Output: Range of all the Item outputs.

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>ParallelTransformPipeline (  )</sub>| <sub></sub>| <br>


## Fields

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>Items</sub>| <sub>List of transforms to be run parallel.</sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>PipelineInputs</sub>| <sub>Gets the pipeline inputs.</sub>| <br>
| <sub>PipelineOutputs</sub>| <sub>Gets the pipeline outputs.</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Add](./Methods/ParallelTransformPipeline-100663504.md) ( [`ITransformation`](./../ITransformation.md) )</sub>| <sub>Add new transform to the list.</sub>| <br>
| <sub>[GetEnumerator](./Methods/ParallelTransformPipeline-100663503.md) (  )</sub>| <sub></sub>| <br>
| <sub>[Transform](./Methods/ParallelTransformPipeline-100663505.md) ( [`Signature`](./../Signature.md) )</sub>| <sub>Executes transform [Items](https://github.com/sigstat/sigstat/blob/develop/docs/md/.md) parallel.  Passes input features for each.  Output is a range of all the Item outputs.</sub>| <br>


