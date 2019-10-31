# [SigStat.Common.Pipeline](./README.md)

## Types

- [`IClassifier`](./IClassifier.md)
	- Trains classification models based on reference signatures
- [`IDistanceClassifier`](./IDistanceClassifier.md)
	- Trains classification models based on reference signatures, by calculating the distances between signature pairs
- [`Input`](./Input.md)
	- Annotates an input [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) in a transformation pipeline
- [`IPipelineIO`](./IPipelineIO.md)
	- Supports the definition of [PipelineInput](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Pipeline/PipelineInput.md) and [PipelineOutput](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Pipeline/PipelineOutput.md)
- [`ISignerModel`](./ISignerModel.md)
	- Analyzes signatures based on their similiarity to the trained model
- [`Output`](./Output.md)
	- Annotates an output [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) in a transformation pipeline
- [`ParallelTransformPipeline`](./ParallelTransformPipeline.md)
	- Runs pipeline items in parallel.  <br>Default Pipeline Output: Range of all the Item outputs.
- [`PipelineInput`](./PipelineInput.md)
	- Represents an input for a [PipelineItem](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Pipeline/PipelineInput.md)
- [`PipelineOutput`](./PipelineOutput.md)
	- Represents an output for a [PipelineItem](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Pipeline/PipelineOutput.md)
- [`SequentialTransformPipeline`](./SequentialTransformPipeline.md)
	- Runs pipeline items in a sequence.  <br>Default Pipeline Output: Output of the last Item in the sequence.

