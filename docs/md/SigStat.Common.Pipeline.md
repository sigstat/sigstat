#### `IClassificationModel`

<sub>Analyzes signatures based on their similiarity to the trained model</sub>
```csharp
public interface SigStat.Common.Pipeline.IClassificationModel

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Test(Signature)</sub> | Returns a double value in the range [0..1], representing the probability of the given signature belonging to the trained model.  <list type="bullet"><item>0: non-match</item><item>0.5: inconclusive</item><item>1: match</item></list> | 


#### `IClassifier`

<sub>Trains classification models based on reference signatures</sub>
```csharp
public interface SigStat.Common.Pipeline.IClassifier

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IClassificationModel` | <sub>Train(List<Signature>)</sub> | Trains a model based on the signatures and returns the trained model | 


#### `IPipelineIO`

<sub>Gives ability to get or set (rewire) a pipeline item's default input and output features.</sub>
```csharp
public interface SigStat.Common.Pipeline.IPipelineIO

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<FeatureDescriptor>` | InputFeatures | List of features to be used as input. | 
| `List<FeatureDescriptor>` | OutputFeatures | List of features to be used as output. | 


#### `ParallelTransformPipeline`

<sub>Runs pipeline items in parallel.  <para>Default Pipeline Output: Range of all the Item outputs.</para></sub>
```csharp
public class SigStat.Common.Pipeline.ParallelTransformPipeline
    : PipelineBase, IEnumerable, ITransformation, ILogger, IProgress, IPipelineIO

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<ITransformation>` | Items |  | 
| `Logger` | Logger | Passes Logger to child items as well. | 
| `Int32` | Progress | Gets the minimum progess of all the child items. | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Add(ITransformation)</sub> | Add new transform to the list. Pass `SigStat.Common.Pipeline.ParallelTransformPipeline.Logger` and set up Progress event. | 
| `IEnumerator` | <sub>GetEnumerator()</sub> |  | 
| `void` | <sub>Transform(Signature)</sub> | Executes transform `SigStat.Common.Pipeline.ParallelTransformPipeline.Items` parallel.  Passes input features for each.  Output is a range of all the Item outputs. | 


#### `SequentialTransformPipeline`

<sub>Runs pipeline items in a sequence.  <para>Default Pipeline Output: Output of the last Item in the sequence.</para></sub>
```csharp
public class SigStat.Common.Pipeline.SequentialTransformPipeline
    : PipelineBase, IEnumerable, ITransformation, ILogger, IProgress, IPipelineIO

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<ITransformation>` | Items |  | 
| `Logger` | Logger | Passes Logger to child items as well. | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Add(ITransformation)</sub> | Add new transform to the list. Pass `SigStat.Common.Pipeline.SequentialTransformPipeline.Logger` and set up Progress event. | 
| `IEnumerator` | <sub>GetEnumerator()</sub> |  | 
| `void` | <sub>Transform(Signature)</sub> | Executes transform `SigStat.Common.Pipeline.SequentialTransformPipeline.Items` in sequence.  Passes input features for each.  Output is the output of the last Item in the sequence. | 


