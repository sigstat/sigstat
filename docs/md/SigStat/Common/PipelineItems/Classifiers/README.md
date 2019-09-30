# [SigStat.Common.PipelineItems.Classifiers](./README.md)

## Types

- [`DtwClassifier`](./DtwClassifier.md)
	- Classifies Signatures with the [SigStat.Common.Algorithms.Dtw](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Algorithms/Dtw.md) algorithm.
- [`DtwSignerModel`](./DtwSignerModel.md)
	- Represents a trained model for [SigStat.Common.PipelineItems.Classifiers.DtwClassifier]()
- [`OptimalDtwClassifier`](./OptimalDtwClassifier.md)
	- This [SigStat.Common.Pipeline.IDistanceClassifier]() implementation will consider both test and  training samples and claculate the threshold to separate the original and forged  signatures to approximate EER. Note that this classifier is not applicable for  real world scenarios. It was developed to test the theoratical boundaries of  threshold based classification
- [`WeightedClassifier`](./WeightedClassifier.md)
	- Classifies Signatures by weighing other Classifier results.

