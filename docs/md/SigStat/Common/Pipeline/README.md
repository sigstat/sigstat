# [SigStat.Common.Pipeline](./README.md)

## Types

- [`IClassifier`](./IClassifier.md)
	- Trains classification models based on reference signatures
- [`IDistanceClassifier`](./IDistanceClassifier.md)
	- Trains classification models based on reference signatures, by calculating the distances between signature pairs
- [`Input`](./Input.md)
	- Annotates an input `SigStat.Common.FeatureDescriptor` in a transformation pipeline
- [`IPipelineIO`](./IPipelineIO.md)
	- Supports the definition of `SigStat.Common.Pipeline.PipelineInput` and `SigStat.Common.Pipeline.PipelineOutput`
- [`ISignerModel`](./ISignerModel.md)
	- Analyzes signatures based on their similiarity to the trained model
- [`Output`](./Output.md)
	- Annotates an output `SigStat.Common.FeatureDescriptor` in a transformation pipeline
- [`ParallelTransformPipeline`](./ParallelTransformPipeline.md)
	- Runs pipeline items in parallel.  <para>Default Pipeline Output: Range of all the Item outputs.</para>
- [`PipelineInput`](./PipelineInput.md)
	- Represents an input for a `SigStat.Common.Pipeline.PipelineInput.PipelineItem`
- [`PipelineOutput`](./PipelineOutput.md)
	- Represents an output for a `SigStat.Common.Pipeline.PipelineOutput.PipelineItem`
- [`SequentialTransformPipeline`](./SequentialTransformPipeline.md)
	- Runs pipeline items in a sequence.  <para>Default Pipeline Output: Output of the last Item in the sequence.</para>

