#### `AutoSetMode`

```csharp
public enum SigStat.Common.Pipeline.AutoSetMode
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| <sub>Value</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>0</sub>` | IfNull | <sub></sub> | 
| `<sub>1</sub>` | Always | <sub></sub> | 
| `<sub>2</sub>` | Never | <sub></sub> | 


#### `IClassifier`

Trains classification models based on reference signatures
```csharp
public interface SigStat.Common.Pipeline.IClassifier

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Double</sub>` | <sub>Test(ISignerModel, Signature)</sub> | <sub>Returns a double value in the range [0..1], representing the probability of the given signature belonging to the trained model.  <list type="bullet"><item>0: non-match</item><item>0.5: inconclusive</item><item>1: match</item></list></sub> | 
| `<sub>ISignerModel</sub>` | <sub>Train(List<Signature>)</sub> | <sub>Trains a model based on the signatures and returns the trained model</sub> | 


#### `Input`

```csharp
public class SigStat.Common.Pipeline.Input
    : Attribute, _Attribute

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>AutoSetMode</sub>` | <sub>AutoSetMode</sub> | <sub></sub> | 


#### `IPipelineIO`

```csharp
public interface SigStat.Common.Pipeline.IPipelineIO

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>List<PipelineInput></sub>` | <sub>PipelineInputs</sub> | <sub></sub> | 
| `<sub>List<PipelineOutput></sub>` | <sub>PipelineOutputs</sub> | <sub></sub> | 


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
| --- | --- | --- | 
| `<sub>String</sub>` | <sub>Default</sub> | <sub></sub> | 


#### `ParallelTransformPipeline`

Runs pipeline items in parallel.  <para>Default Pipeline Output: Range of all the Item outputs.</para>
```csharp
public class SigStat.Common.Pipeline.ParallelTransformPipeline
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, IEnumerable, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>List<ITransformation></sub>` | <sub>Items</sub> | <sub>List of transforms to be run parallel.</sub> | 


###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>List<PipelineInput></sub>` | <sub>PipelineInputs</sub> | <sub></sub> | 
| `<sub>List<PipelineOutput></sub>` | <sub>PipelineOutputs</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>Add(ITransformation)</sub> | <sub>Add new transform to the list.</sub> | 
| `<sub>IEnumerator</sub>` | <sub>GetEnumerator()</sub> | <sub></sub> | 
| `<sub>void</sub>` | <sub>Transform(Signature)</sub> | <sub>Executes transform `SigStat.Common.Pipeline.ParallelTransformPipeline.Items` parallel.  Passes input features for each.  Output is a range of all the Item outputs.</sub> | 


#### `PipelineInput`

```csharp
public class SigStat.Common.Pipeline.PipelineInput

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>AutoSetMode</sub>` | <sub>AutoSetMode</sub> | <sub></sub> | 
| `<sub>Object</sub>` | <sub>FD</sub> | <sub></sub> | 
| `<sub>String</sub>` | <sub>FieldName</sub> | <sub></sub> | 
| `<sub>Boolean</sub>` | <sub>IsCollectionOfFeatureDescriptors</sub> | <sub></sub> | 
| `<sub>Type</sub>` | <sub>Type</sub> | <sub></sub> | 


#### `PipelineOutput`

```csharp
public class SigStat.Common.Pipeline.PipelineOutput

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>String</sub>` | <sub>Default</sub> | <sub></sub> | 
| `<sub>Object</sub>` | <sub>FD</sub> | <sub></sub> | 
| `<sub>String</sub>` | <sub>FieldName</sub> | <sub></sub> | 
| `<sub>Boolean</sub>` | <sub>IsCollectionOfFeatureDescriptors</sub> | <sub></sub> | 
| `<sub>Boolean</sub>` | <sub>IsTemporary</sub> | <sub></sub> | 
| `<sub>Type</sub>` | <sub>Type</sub> | <sub></sub> | 


#### `SequentialTransformPipeline`

Runs pipeline items in a sequence.  <para>Default Pipeline Output: Output of the last Item in the sequence.</para>
```csharp
public class SigStat.Common.Pipeline.SequentialTransformPipeline
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, IEnumerable, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>List<ITransformation></sub>` | <sub>Items</sub> | <sub>List of transforms to be run in sequence.</sub> | 


###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>List<PipelineInput></sub>` | <sub>PipelineInputs</sub> | <sub></sub> | 
| `<sub>List<PipelineOutput></sub>` | <sub>PipelineOutputs</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>Add(ITransformation)</sub> | <sub>Add new transform to the list.</sub> | 
| `<sub>IEnumerator</sub>` | <sub>GetEnumerator()</sub> | <sub></sub> | 
| `<sub>void</sub>` | <sub>Transform(Signature)</sub> | <sub>Executes transform `SigStat.Common.Pipeline.SequentialTransformPipeline.Items` in sequence.  Passes input features for each.  Output is the output of the last Item in the sequence.</sub> | 


