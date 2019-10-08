# [SigStat.Common.PipelineItems.Classifiers](./README.md)

## Types

- [`DtwClassifier`](./DtwClassifier.md)
	- Classifies Signatures with the [Algorithms.Dtw](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Algorithms/Dtw.md) algorithm.
- [`DtwSignerModel`](./DtwSignerModel.md)
	- Represents a trained model for [PipelineItems.Classifiers.DtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md)
- [`OptimalDtwClassifier`](./OptimalDtwClassifier.md)
	- This [Pipeline.IDistanceClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Pipeline/IDistanceClassifier.md) implementation will consider both test and  training samples and claculate the threshold to separate the original and forged  signatures to approximate EER. Note that this classifier is not applicable for  real world scenarios. It was developed to test the theoratical boundaries of  threshold based classification
- [`WeightedClassifier`](./WeightedClassifier.md)
	- Classifies Signatures by weighing other Classifier results.

