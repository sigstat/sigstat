#### `ArrayExtension`

Helper methods for processing arrays
```csharp
public static class SigStat.Common.ArrayExtension

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>ValueTuple<Int32, Int32></sub>` | <sub>GetCog(this Double[,])</sub> | Calculates the center of gravity, assuming that each cell contains  a weight value | 
| `<sub>IEnumerable<T></sub>` | <sub>GetValues(this T[,])</sub> | Enumerates all values in a two dimensional array | 
| `<sub>T[,]</sub>` | <sub>SetValues(this T[,], T)</sub> | Sets all values in a two dimensional array to `` | 
| `<sub>Double</sub>` | <sub>Sum(this Double[,], Int32, Int32, Int32, Int32)</sub> | Calculates the sum of the values in the given sub-array | 
| `<sub>Double</sub>` | <sub>SumCol(this Double[,], Int32)</sub> | Returns the sum of column values in a two dimensional array | 
| `<sub>Double</sub>` | <sub>SumRow(this Double[,], Int32)</sub> | Returns the sum of row values in a two dimensional array | 


#### `Baseline`

```csharp
public class SigStat.Common.Baseline

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>PointF</sub>` | <sub>End</sub> | Endpoint of the baseline | 
| `<sub>PointF</sub>` | <sub>Start</sub> | Starting point of the baseline | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>String</sub>` | <sub>ToString()</sub> | Returns a string representation of the baseline | 


#### `BasicMetadataExtraction`

Extracts basic statistical signature (like `SigStat.Common.Features.Bounds` or `SigStat.Common.Features.Cog`) information from an Image
```csharp
public class SigStat.Common.BasicMetadataExtraction
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>Transform(Signature)</sub> |  | 


###### Static Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Double</sub>` | <sub>Trim</sub> | Represents theratio of significant pixels that should be trimmed  from each side while calculating `SigStat.Common.Features.TrimmedBounds` | 


#### `BenchmarkResults`

Contains the benchmark results of every `SigStat.Common.Signer` and the summarized final results.
```csharp
public struct SigStat.Common.BenchmarkResults

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Result</sub>` | <sub>FinalResult</sub> | Summarized, final result of the benchmark execution. | 
| `<sub>List<Result></sub>` | <sub>SignerResults</sub> | List that contains the `SigStat.Common.Result`s for each `SigStat.Common.Signer` | 


#### `FeatureDescriptor`

Represents a feature with name and type.
```csharp
public class SigStat.Common.FeatureDescriptor

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Type</sub>` | <sub>FeatureType</sub> | Gets or sets the type of the feature. | 
| `<sub>Boolean</sub>` | <sub>IsCollection</sub> | Gets whether the type of the feature is List. | 
| `<sub>String</sub>` | <sub>Key</sub> | Gets the unique key of the feature. | 
| `<sub>String</sub>` | <sub>Name</sub> | Gets or sets a human readable name of the feature. | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>String</sub>` | <sub>ToString()</sub> | Returns a string represenatation of the FeatureDescriptor | 


###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Dictionary<String, FeatureDescriptor></sub>` | <sub>descriptors</sub> | The static dictionary of all descriptors. | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>FeatureDescriptor</sub>` | <sub>Get(String)</sub> | Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key. | 
| `<sub>FeatureDescriptor<T></sub>` | <sub>Get(String)</sub> | Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key. | 
| `<sub>Boolean</sub>` | <sub>IsRegistered(String)</sub> | Returns true, if there is a FeatureDescriptor registered with the given key | 
| `<sub>FeatureDescriptor</sub>` | <sub>Register(String, Type)</sub> | Registers a new `SigStat.Common.FeatureDescriptor` with a given key.  If the FeatureDescriptor is allready registered, this function will  return a reference to the originally registered FeatureDescriptor.  to the a | 


#### `FeatureDescriptor<T>`

Represents a feature with the type `type`
```csharp
public class SigStat.Common.FeatureDescriptor<T>
    : FeatureDescriptor

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>FeatureDescriptor<T></sub>` | <sub>Get(String)</sub> | Gets the `SigStat.Common.FeatureDescriptor`1` specified by ``.  If the key is not registered yet, a new `SigStat.Common.FeatureDescriptor`1` is automatically created with the given key and type. | 


#### `Features`

Standard set of features.
```csharp
public static class SigStat.Common.Features

```

###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>IReadOnlyList<FeatureDescriptor></sub>` | <sub>All</sub> | Returns a readonly list of all `SigStat.Common.FeatureDescriptor`s defined in `SigStat.Common.Features` | 
| `<sub>FeatureDescriptor<List<Double>></sub>` | <sub>Altitude</sub> | Altitude of an online signature as a function of `SigStat.Common.Features.T` | 
| `<sub>FeatureDescriptor<List<Double>></sub>` | <sub>Azimuth</sub> | Azimuth of an online signature as a function of `SigStat.Common.Features.T` | 
| `<sub>FeatureDescriptor<RectangleF></sub>` | <sub>Bounds</sub> | Actual bounds of the signature | 
| `<sub>FeatureDescriptor<List<Boolean>></sub>` | <sub>Button</sub> | Pen position of an online signature as a function of `SigStat.Common.Features.T` | 
| `<sub>FeatureDescriptor<Point></sub>` | <sub>Cog</sub> | Center of gravity in a signature | 
| `<sub>FeatureDescriptor<Int32></sub>` | <sub>Dpi</sub> | Dots per inch | 
| `<sub>FeatureDescriptor<Image<Rgba32>></sub>` | <sub>Image</sub> | The visaul representation of a signature | 
| `<sub>FeatureDescriptor<List<Double>></sub>` | <sub>Pressure</sub> | Pressure of an online signature as a function of `SigStat.Common.Features.T` | 
| `<sub>FeatureDescriptor<List<Double>></sub>` | <sub>T</sub> | Timestamps for online signatures | 
| `<sub>FeatureDescriptor<Rectangle></sub>` | <sub>TrimmedBounds</sub> | Represents the main body of the signature `SigStat.Common.BasicMetadataExtraction` | 
| `<sub>FeatureDescriptor<List<Double>></sub>` | <sub>X</sub> | X coordinates of an online signature as a function of `SigStat.Common.Features.T` | 
| `<sub>FeatureDescriptor<List<Double>></sub>` | <sub>Y</sub> | Y coordinates of an online signature as a function of `SigStat.Common.Features.T` | 


#### `ILoggerObject`

Represents a type, that contains an ILogger property that can be used to perform logging.
```csharp
public interface SigStat.Common.ILoggerObject

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>ILogger</sub>` | <sub>Logger</sub> | Gets or sets the ILogger implementation used to perform logging | 


#### `ILoggerObjectExtensions`

ILoggerObject extension methods for common scenarios.
```csharp
public static class SigStat.Common.ILoggerObjectExtensions

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>Error(this ILoggerObject, String, Object[])</sub> | Formats and writes an error log message. | 
| `<sub>void</sub>` | <sub>Log(this ILoggerObject, String, Object[])</sub> | Formats and writes an informational log message. | 
| `<sub>void</sub>` | <sub>Trace(this ILoggerObject, String, Object[])</sub> | Formats and writes a trace log message. | 
| `<sub>void</sub>` | <sub>Warn(this ILoggerObject, String, Object[])</sub> | Formats and writes an warning log message. | 


#### `ITransformation`

Allows implementing a pipeline transform item capable of logging, progress tracking and IO rewiring.
```csharp
public interface SigStat.Common.ITransformation
    : IPipelineIO

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>Transform(Signature)</sub> | Executes the transform on the `` parameter.  This function gets called by the pipeline. | 


#### `Loop`

Represents a loop in a signature
```csharp
public class SigStat.Common.Loop

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>RectangleF</sub>` | <sub>Bounds</sub> | The bounding rectangle of the loop | 
| `<sub>PointF</sub>` | <sub>Center</sub> | The geometrical center of the looop | 
| `<sub>List<PointF></sub>` | <sub>Points</sub> | A list of defining points of the loop | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>String</sub>` | <sub>ToString()</sub> | Returns a string representation of the loop | 


#### `MathHelper`

Common mathematical functions used by the SigStat framework
```csharp
public static class SigStat.Common.MathHelper

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Double</sub>` | <sub>Min(Double, Double, Double)</sub> | Returns the smallest of the three double parameters | 


#### `Origin`

Represents our knowledge on the origin of a signature.
```csharp
public enum SigStat.Common.Origin
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| <sub>Value</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>0</sub>` | Unknown | Use this in practice before a signature is verified. | 
| `<sub>1</sub>` | Genuine | The `SigStat.Common.Signature`'s origin is verified to be from `SigStat.Common.Signature.Signer` | 
| `<sub>2</sub>` | Forged | The `SigStat.Common.Signature` is a forgery. | 


#### `PipelineBase`

TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.  IProgress, ILogger, IPipelineIO default implementacioja.
```csharp
public abstract class SigStat.Common.PipelineBase
    : ILoggerObject, IProgress, IPipelineIO

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>ILogger</sub>` | <sub>Logger</sub> |  | 
| `<sub>List<PipelineInput></sub>` | <sub>PipelineInputs</sub> |  | 
| `<sub>List<PipelineOutput></sub>` | <sub>PipelineOutputs</sub> |  | 
| `<sub>Int32</sub>` | <sub>Progress</sub> |  | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>EventHandler<Int32></sub>` | <sub>ProgressChanged</sub> | The event is raised whenever the value of `SigStat.Common.PipelineBase.Progress` changes | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>OnProgressChanged()</sub> | Raises the `SigStat.Common.PipelineBase.ProgressChanged` event | 


#### `Result`

Contains the benchmark results of a single `SigStat.Common.Signer`
```csharp
public class SigStat.Common.Result

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Double</sub>` | <sub>Aer</sub> | Average Error Rate | 
| `<sub>Double</sub>` | <sub>Far</sub> | False Acceptance Rate | 
| `<sub>Double</sub>` | <sub>Frr</sub> | False Rejection Rate | 
| `<sub>String</sub>` | <sub>Signer</sub> | Identifier of the `SigStat.Common.Result.Signer` | 


#### `Sampler`

Takes samples from a set of `SigStat.Common.Signature`s by given sampling strategies.  Use this to fine-tune the `SigStat.Common.VerifierBenchmark`
```csharp
public class SigStat.Common.Sampler

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Int32</sub>` | <sub>BatchSize</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>Init(Signer)</sub> | Initialize the Sampler with a Signer's Signatures. | 
| `<sub>void</sub>` | <sub>Init(List<Signer>)</sub> | Initialize the Sampler with a Signer's Signatures. | 
| `<sub>void</sub>` | <sub>Init(List<Signature>)</sub> | Initialize the Sampler with a Signer's Signatures. | 
| `<sub>List<Signature></sub>` | <sub>SampleForgeryTests(Func<List<Signature>, List<Signature>>)</sub> | Samples a batch of forged signatures to test on. | 
| `<sub>List<Signature></sub>` | <sub>SampleGenuineTests(Func<List<Signature>, List<Signature>>)</sub> | Samples a batch of genuine test signatures to test on. | 
| `<sub>List<Signature></sub>` | <sub>SampleReferences(Func<List<Signature>, List<Signature>>)</sub> | Samples a batch of genuine reference signatures to train on. | 


#### `Signature`

Represents a signature as a collection of features, containing the data that flows in the pipeline.
```csharp
public class SigStat.Common.Signature

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>String</sub>` | <sub>ID</sub> | An identifier for the Signature. Keep it unique to be useful for logs. | 
| `<sub>Object</sub>` | <sub>Item</sub> | Gets or sets the specified feature. | 
| `<sub>Object</sub>` | <sub>Item</sub> | Gets or sets the specified feature. | 
| `<sub>Origin</sub>` | <sub>Origin</sub> | Represents our knowledge on the origin of the signature. `SigStat.Common.Origin.Unknown` may be used in practice before it is verified. | 
| `<sub>Signer</sub>` | <sub>Signer</sub> | A reference to the `SigStat.Common.Signer` who this signature belongs to. (The origin is not constrained to be genuine.) | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>List<Double[]></sub>` | <sub>GetAggregateFeature(List<FeatureDescriptor>)</sub> | Aggregate multiple features into one. Example: X, Y features -&gt; P.xy feature.  Use this for example at DTW algorithm input. | 
| `<sub>T</sub>` | <sub>GetFeature(String)</sub> | Gets the specified feature. | 
| `<sub>T</sub>` | <sub>GetFeature(FeatureDescriptor<T>)</sub> | Gets the specified feature. | 
| `<sub>T</sub>` | <sub>GetFeature(FeatureDescriptor)</sub> | Gets the specified feature. | 
| `<sub>IEnumerable<FeatureDescriptor></sub>` | <sub>GetFeatureDescriptors()</sub> | Gets a collection of `SigStat.Common.FeatureDescriptor`s that are used in this signature. | 
| `<sub>Boolean</sub>` | <sub>HasFeature(FeatureDescriptor)</sub> | Returns true if the signature contains the specified feature | 
| `<sub>Boolean</sub>` | <sub>HasFeature(String)</sub> | Returns true if the signature contains the specified feature | 
| `<sub>void</sub>` | <sub>SetFeature(FeatureDescriptor, T)</sub> | Sets the specified feature. | 
| `<sub>void</sub>` | <sub>SetFeature(String, T)</sub> | Sets the specified feature. | 
| `<sub>String</sub>` | <sub>ToString()</sub> | Returns a string representation of the signature | 


#### `Signer`

Represents a person as a `SigStat.Common.Signer.ID` and a list of `SigStat.Common.Signer.Signatures`.
```csharp
public class SigStat.Common.Signer

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>String</sub>` | <sub>ID</sub> | An identifier for the Signer. Keep it unique to be useful for logs. | 
| `<sub>List<Signature></sub>` | <sub>Signatures</sub> | List of signatures that belong to the signer.  (Their origin is not constrained to be genuine.) | 


#### `SigStatEvents`

Standard event identifiers used by the SigStat system
```csharp
public static class SigStat.Common.SigStatEvents

```

###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>EventId</sub>` | <sub>BenchmarkEvent</sub> | Events originating from a benchmark | 
| `<sub>EventId</sub>` | <sub>VerifierEvent</sub> | Events originating from a verifier | 


#### `SVC2004Sampler`

```csharp
public class SigStat.Common.SVC2004Sampler
    : Sampler

```

#### `VerifierBenchmark`

Benchmarking class to test error rates of a `SigStat.Common.Model.Verifier`
```csharp
public class SigStat.Common.VerifierBenchmark
    : ILoggerObject

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>IDataSetLoader</sub>` | <sub>Loader</sub> | The loader that will provide the database for benchmarking | 
| `<sub>ILogger</sub>` | <sub>Logger</sub> | Gets or sets the attached `Microsoft.Extensions.Logging.ILogger` object used to log messages. Hands it over to the verifier. | 
| `<sub>Int32</sub>` | <sub>Progress</sub> |  | 
| `<sub>Sampler</sub>` | <sub>Sampler</sub> | The `SigStat.Common.Sampler` to be used for benchmarking | 
| `<sub>Verifier</sub>` | <sub>Verifier</sub> | Gets or sets the `SigStat.Common.Model.Verifier` to be benchmarked. | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>EventHandler<Int32></sub>` | <sub>ProgressChanged</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>BenchmarkResults</sub>` | <sub>Execute(Boolean = True)</sub> | Execute the benchmarking process. | 


