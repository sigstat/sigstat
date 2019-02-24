## `DTWClassifier`

Classifies Signatures with the `SigStat.Common.Algorithms.Dtw` algorithm.
```csharp
public class SigStat.Common.PipelineItems.Classifiers.DTWClassifier
    : PipelineBase, IClassification, ILogger, IProgress, IPipelineIO, IEnumerable

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Add(`FeatureDescriptor`) |  | 
| `IEnumerator` | GetEnumerator() |  | 
| `Double` | Pair(`Signature`, `Signature`) | Aggregates the input features and executes the `SigStat.Common.Algorithms.Dtw` algorithm. | 


## `WeightedClassifier`

Classifies Signatures by weighing other Classifier results.
```csharp
public class SigStat.Common.PipelineItems.Classifiers.WeightedClassifier
    : PipelineBase, IEnumerable, IClassification, ILogger, IProgress, IPipelineIO

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `List<ValueTuple<IClassification, Double>>` | Items | List of classifiers and belonging weights. | 


Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Logger` | Logger | Gets or sets the Logger. Passes it to child Items as well. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Add(`ValueTuple<IClassification, Double>`) | Add a new classifier with given weight to the list of items. | 
| `IEnumerator` | GetEnumerator() |  | 
| `Double` | Pair(`Signature`, `Signature`) | Execute each classifier in the list and weigh returned costs. | 


