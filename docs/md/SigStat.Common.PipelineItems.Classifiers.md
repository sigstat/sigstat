#### `DtwClassifier`

Classifies Signatures with the `SigStat.Common.Algorithms.Dtw` algorithm.
```csharp
public class SigStat.Common.PipelineItems.Classifiers.DtwClassifier
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, IClassifier

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<FeatureDescriptor>` | <sub>InputFeatures</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Test(ISignerModel, Signature)</sub> |  | 
| `ISignerModel` | <sub>Train(List<Signature>)</sub> |  | 


#### `DtwSignerModel`

Represents a trained model for `SigStat.Common.PipelineItems.Classifiers.DtwClassifier`
```csharp
public class SigStat.Common.PipelineItems.Classifiers.DtwSignerModel
    : ISignerModel

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double[,]` | <sub>DistanceMatrix</sub> | DTW distance matrix of the genuine signatures | 
| `Double` | <sub>Threshold</sub> | A threshold, that will be used for classification. Signatures with  an average DTW distance from the genuines above this threshold will  be classified as forgeries | 


#### `WeightedClassifier`

Classifies Signatures by weighing other Classifier results.
```csharp
public class SigStat.Common.PipelineItems.Classifiers.WeightedClassifier
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, IEnumerable, IClassifier

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<ValueTuple<IClassifier, Double>>` | <sub>Items</sub> | List of classifiers and belonging weights. | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Add(ValueTuple<IClassifier, Double>)</sub> | Add a new classifier with given weight to the list of items. | 
| `IEnumerator` | <sub>GetEnumerator()</sub> |  | 
| `Double` | <sub>Test(ISignerModel, Signature)</sub> |  | 
| `ISignerModel` | <sub>Train(List<Signature>)</sub> |  | 


