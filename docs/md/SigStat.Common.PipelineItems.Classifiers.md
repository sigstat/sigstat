#### `DTWClassifier`

Classifies Signatures with the `SigStat.Common.Algorithms.Dtw` algorithm.
```csharp
public class SigStat.Common.PipelineItems.Classifiers.DTWClassifier
    : PipelineBase, IClassification, ILogger, IProgress, IPipelineIO, IEnumerable

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Add(FeatureDescriptor)</sub> |  | 
| `IEnumerator` | <sub>GetEnumerator()</sub> |  | 
| `Double` | <sub>Pair(Signature, Signature)</sub> | Aggregates the input features and executes the `SigStat.Common.Algorithms.Dtw` algorithm. | 


#### `WeightedClassifier`

Classifies Signatures by weighing other Classifier results.
```csharp
public class SigStat.Common.PipelineItems.Classifiers.WeightedClassifier
    : PipelineBase, IEnumerable, IClassification, ILogger, IProgress, IPipelineIO

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<ValueTuple<IClassification, Double>>` | Items | List of classifiers and belonging weights. | 


###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Logger` | Logger | Gets or sets the Logger. Passes it to child Items as well. | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Add(ValueTuple<IClassification, Double>)</sub> | Add a new classifier with given weight to the list of items. | 
| `IEnumerator` | <sub>GetEnumerator()</sub> |  | 
| `Double` | <sub>Pair(Signature, Signature)</sub> | Execute each classifier in the list and weigh returned costs. | 


