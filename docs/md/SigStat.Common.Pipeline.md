#### `IClassificationModel`

Analyzes signatures based on their similiarity to the trained model
```csharp
public interface SigStat.Common.Pipeline.IClassificationModel

```

###### Methods

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `Double` | Test(Signature) | Returns a double value in the range [0..1], representing the probability of the given signature belonging to the trained model.  <list type="bullet"><item>0: non-match</item><item>0.5: inconclusive</item><item>1: match</item></list> | 


#### `IClassifier`

Trains classification models based on reference signatures
```csharp
public interface SigStat.Common.Pipeline.IClassifier

```

###### Methods

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `IClassificationModel` | Train(List<Signature>) | Trains a model based on the signatures and returns the trained model | 


#### `IPipelineIO`

Gives ability to get or set (rewire) a pipeline item's default input and output features.
```csharp
public interface SigStat.Common.Pipeline.IPipelineIO

```

###### Properties

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `List<FeatureDescriptor>` | InputFeatures | List of features to be used as input. | 
| `List<FeatureDescriptor>` | OutputFeatures | List of features to be used as output. | 


#### `ParallelTransformPipeline`

Runs pipeline items in parallel.  <para>Default Pipeline Output: Range of all the Item outputs.</para>
```csharp
public class SigStat.Common.Pipeline.ParallelTransformPipeline
    : PipelineBase, IEnumerable, ITransformation, ILogger, IProgress, IPipelineIO

```

###### Properties

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `List<ITransformation>` | Items |  | 
| `Logger` | Logger | Passes Logger to child items as well. | 
| `Int32` | Progress | Gets the minimum progess of all the child items. | 


###### Methods

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `void` | Add(ITransformation) | Add new transform to the list. Pass `SigStat.Common.Pipeline.ParallelTransformPipeline.Logger` and set up Progress event. | 
| `IEnumerator` | GetEnumerator() |  | 
| `void` | Transform(Signature) | Executes transform `SigStat.Common.Pipeline.ParallelTransformPipeline.Items` parallel.  Passes input features for each.  Output is a range of all the Item outputs. | 


#### `SequentialTransformPipeline`

Runs pipeline items in a sequence.  <para>Default Pipeline Output: Output of the last Item in the sequence.</para>
```csharp
public class SigStat.Common.Pipeline.SequentialTransformPipeline
    : PipelineBase, IEnumerable, ITransformation, ILogger, IProgress, IPipelineIO

```

###### Properties

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `List<ITransformation>` | Items |  | 
| `Logger` | Logger | Passes Logger to child items as well. | 


###### Methods

| Type | Name | Summary | 
| ---- | ---- | ---- | 
| `void` | Add(ITransformation) | Add new transform to the list. Pass `SigStat.Common.Pipeline.SequentialTransformPipeline.Logger` and set up Progress event. | 
| `IEnumerator` | GetEnumerator() |  | 
| `void` | Transform(Signature) | Executes transform `SigStat.Common.Pipeline.SequentialTransformPipeline.Items` in sequence.  Passes input features for each.  Output is the output of the last Item in the sequence. | 


