# [SequentialTransformPipeline](./SequentialTransformPipeline.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Pipeline](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [ITransformation](./../ITransformation.md)

## Summary
Runs pipeline items in a sequence.  <br>Default Pipeline Output: Output of the last Item in the sequence.

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| SequentialTransformPipeline () |  | 


## Fields

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| Items | List of transforms to be run in sequence. | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| PipelineInputs | Gets the pipeline inputs. | 
| PipelineOutputs | Gets the pipeline outputs. | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Add](./Methods/SequentialTransformPipeline--Add.md) ( [`ITransformation`](./../ITransformation.md) ) | Add new transform to the list. | 
| [GetEnumerator](./Methods/SequentialTransformPipeline--GetEnumerator.md) (  ) |  | 
| [Transform](./Methods/SequentialTransformPipeline--Transform.md) ( [`Signature`](./../Signature.md) ) | Executes transform [Items](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Pipeline/SequentialTransformPipeline.md) in sequence.  Passes input features for each.  Output is the output of the last Item in the sequence. | 


