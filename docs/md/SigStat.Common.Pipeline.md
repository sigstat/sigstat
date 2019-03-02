#### `AutoSetMode`

```csharp
public enum SigStat.Common.Pipeline.AutoSetMode
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| <sub>Value</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `0` | IfNull |  | 
| `1` | Always |  | 
| `2` | Never |  | 


#### `IClassifier`

Trains classification models based on reference signatures
```csharp
public interface SigStat.Common.Pipeline.IClassifier

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Test(ISignerModel, Signature)</sub> | Returns a double value in the range [0..1], representing the probability of the given signature belonging to the trained model.  <list type="bullet"><item>0: non-match</item><item>0.5: inconclusive</item><item>1: match</item></list> | 
| `ISignerModel` | <sub>Train(List<Signature>)</sub> | Trains a model based on the signatures and returns the trained model | 


#### `Input`

```csharp
public class SigStat.Common.Pipeline.Input
    : Attribute, _Attribute

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `AutoSetMode` | <sub>AutoSetMode</sub> |  | 


#### `IPipelineIO`

```csharp
public interface SigStat.Common.Pipeline.IPipelineIO

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `` | <sub>PipelineInputs</sub> |  | 
| `` | <sub>PipelineOutputs</sub> |  | 


#### `ISignerModel`

Analyzes signatures based on their similiarity to the trained model
```csharp
public interface SigStat.Common.Pipeline.ISignerModel

```

#### `Output`

```csharp
public class SigStat.Common.Pipeline.Output
    : Attribute, _Attribute

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>Default</sub> |  | 


#### `ParallelTransformPipeline`

Runs pipeline items in parallel.  <para>Default Pipeline Output: Range of all the Item outputs.</para>
```csharp
public class SigStat.Common.Pipeline.ParallelTransformPipeline
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, IEnumerable, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<ITransformation>` | <sub>Items</sub> | List of transforms to be run parallel. | 


###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `` | <sub>PipelineInputs</sub> |  | 
| `` | <sub>PipelineOutputs</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Add(ITransformation)</sub> | Add new transform to the list. | 
| `IEnumerator` | <sub>GetEnumerator()</sub> |  | 
| `void` | <sub>Transform(Signature)</sub> | Executes transform `SigStat.Common.Pipeline.ParallelTransformPipeline.Items` parallel.  Passes input features for each.  Output is a range of all the Item outputs. | 


#### `PipelineInput`

```csharp
public class SigStat.Common.Pipeline.PipelineInput

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `` | <sub>AutoSetMode</sub> |  | 
| `` | <sub>FD</sub> |  | 
| `` | <sub>FieldName</sub> |  | 
| `` | <sub>IsCollectionOfFeatureDescriptors</sub> |  | 
| `` | <sub>Type</sub> |  | 


#### `PipelineOutput`

```csharp
public class SigStat.Common.Pipeline.PipelineOutput

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `` | <sub>Default</sub> |  | 
| `` | <sub>FD</sub> |  | 
| `` | <sub>FieldName</sub> |  | 
| `` | <sub>IsCollectionOfFeatureDescriptors</sub> |  | 
| `` | <sub>IsTemporary</sub> |  | 
| `` | <sub>Type</sub> |  | 


#### `SequentialTransformPipeline`

Runs pipeline items in a sequence.  <para>Default Pipeline Output: Output of the last Item in the sequence.</para>
```csharp
public class SigStat.Common.Pipeline.SequentialTransformPipeline
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, IEnumerable, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<ITransformation>` | <sub>Items</sub> | List of transforms to be run in sequence. | 


###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `` | <sub>PipelineInputs</sub> |  | 
| `` | <sub>PipelineOutputs</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Add(ITransformation)</sub> | Add new transform to the list. | 
| `IEnumerator` | <sub>GetEnumerator()</sub> |  | 
| `void` | <sub>Transform(Signature)</sub> | Executes transform `SigStat.Common.Pipeline.SequentialTransformPipeline.Items` in sequence.  Passes input features for each.  Output is the output of the last Item in the sequence. | 


