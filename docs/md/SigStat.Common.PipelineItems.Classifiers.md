#### `DtwClassifier`

<sub>Classifies Signatures with the `SigStat.Common.Algorithms.Dtw` algorithm.</sub>
```csharp
public class SigStat.Common.PipelineItems.Classifiers.DtwClassifier
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, IClassifier

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<FeatureDescriptor></sub> | <sub>InputFeatures</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>Test(ISignerModel, Signature)</sub> | <sub></sub> | 
| <sub>ISignerModel</sub> | <sub>Train(List<Signature>)</sub> | <sub></sub> | 


#### `DtwSignerModel`

<sub>Represents a trained model for `SigStat.Common.PipelineItems.Classifiers.DtwClassifier`</sub>
```csharp
public class SigStat.Common.PipelineItems.Classifiers.DtwSignerModel
    : ISignerModel

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double[,]</sub> | <sub>DistanceMatrix</sub> | <sub>DTW distance matrix of the genuine signatures</sub> | 
| <sub>Double</sub> | <sub>Threshold</sub> | <sub>A threshold, that will be used for classification. Signatures with  an average DTW distance from the genuines above this threshold will  be classified as forgeries</sub> | 


###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<Double[][]></sub> | <sub>GenuineSignatures</sub> | <sub>A list a of genuine signatures used for training</sub> | 


#### `OptimalDtwClassifier`

```csharp
public class SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, IClassifier

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<FeatureDescriptor></sub> | <sub>Features</sub> | <sub></sub> | 
| <sub>Sampler</sub> | <sub>Sampler</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>Test(ISignerModel, Signature)</sub> | <sub></sub> | 
| <sub>ISignerModel</sub> | <sub>Train(List<Signature>)</sub> | <sub></sub> | 


#### `WeightedClassifier`

<sub>Classifies Signatures by weighing other Classifier results.</sub>
```csharp
public class SigStat.Common.PipelineItems.Classifiers.WeightedClassifier
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, IEnumerable, IClassifier

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<ValueTuple<IClassifier, Double>></sub> | <sub>Items</sub> | <sub>List of classifiers and belonging weights.</sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Add(ValueTuple<IClassifier, Double>)</sub> | <sub>Add a new classifier with given weight to the list of items.</sub> | 
| <sub>IEnumerator</sub> | <sub>GetEnumerator()</sub> | <sub></sub> | 
| <sub>Double</sub> | <sub>Test(ISignerModel, Signature)</sub> | <sub></sub> | 
| <sub>ISignerModel</sub> | <sub>Train(List<Signature>)</sub> | <sub></sub> | 


