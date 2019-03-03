#### `ArrayExtension`

Helper methods for processing arrays
```csharp
public static class SigStat.Common.ArrayExtension

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>ValueTuple<Int32, Int32></sub> | <sub>GetCog(this Double[,])</sub> | <sub>Calculates the center of gravity, assuming that each cell contains  a weight value</sub> | 
| <sub>IEnumerable<T></sub> | <sub>GetValues(this T[,])</sub> | <sub>Enumerates all values in a two dimensional array</sub> | 
| <sub>T[,]</sub> | <sub>SetValues(this T[,], T)</sub> | <sub>Sets all values in a two dimensional array to ``</sub> | 
| <sub>Double</sub> | <sub>Sum(this Double[,], Int32, Int32, Int32, Int32)</sub> | <sub>Calculates the sum of the values in the given sub-array</sub> | 
| <sub>Double</sub> | <sub>SumCol(this Double[,], Int32)</sub> | <sub>Returns the sum of column values in a two dimensional array</sub> | 
| <sub>Double</sub> | <sub>SumRow(this Double[,], Int32)</sub> | <sub>Returns the sum of row values in a two dimensional array</sub> | 


#### `Baseline`

```csharp
public class SigStat.Common.Baseline

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>PointF</sub> | <sub>End</sub> | <sub>Endpoint of the baseline</sub> | 
| <sub>PointF</sub> | <sub>Start</sub> | <sub>Starting point of the baseline</sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>String</sub> | <sub>ToString()</sub> | <sub>Returns a string representation of the baseline</sub> | 


#### `BasicMetadataExtraction`

Extracts basic statistical signature (like `SigStat.Common.Features.Bounds` or `SigStat.Common.Features.Cog`) information from an Image
```csharp
public class SigStat.Common.BasicMetadataExtraction
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


###### Static Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>Trim</sub> | <sub>Represents theratio of significant pixels that should be trimmed  from each side while calculating `SigStat.Common.Features.TrimmedBounds`</sub> | 


#### `BenchmarkResults`

Contains the benchmark results of every `SigStat.Common.Signer` and the summarized final results.
```csharp
public struct SigStat.Common.BenchmarkResults

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Result</sub> | <sub>FinalResult</sub> | <sub>Summarized, final result of the benchmark execution.</sub> | 
| <sub>List<Result></sub> | <sub>SignerResults</sub> | <sub>List that contains the `SigStat.Common.Result`s for each `SigStat.Common.Signer`</sub> | 


#### `FeatureDescriptor`

Represents a feature with name and type.
```csharp
public class SigStat.Common.FeatureDescriptor

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Type</sub> | <sub>FeatureType</sub> | <sub>Gets or sets the type of the feature.</sub> | 
| <sub>Boolean</sub> | <sub>IsCollection</sub> | <sub>Gets whether the type of the feature is List.</sub> | 
| <sub>String</sub> | <sub>Key</sub> | <sub>Gets the unique key of the feature.</sub> | 
| <sub>String</sub> | <sub>Name</sub> | <sub>Gets or sets a human readable name of the feature.</sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>String</sub> | <sub>ToString()</sub> | <sub>Returns a string represenatation of the FeatureDescriptor</sub> | 


###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Dictionary<String, FeatureDescriptor></sub> | <sub>descriptors</sub> | <sub>The static dictionary of all descriptors.</sub> | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>FeatureDescriptor</sub> | <sub>Get(String)</sub> | <sub>Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key.</sub> | 
| <sub>FeatureDescriptor<T></sub> | <sub>Get(String)</sub> | <sub>Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key.</sub> | 
| <sub>Boolean</sub> | <sub>IsRegistered(String)</sub> | <sub>Returns true, if there is a FeatureDescriptor registered with the given key</sub> | 
| <sub>FeatureDescriptor</sub> | <sub>Register(String, Type)</sub> | <sub>Registers a new `SigStat.Common.FeatureDescriptor` with a given key.  If the FeatureDescriptor is allready registered, this function will  return a reference to the originally registered FeatureDescriptor.  to the a</sub> | 


#### `FeatureDescriptor<T>`

Represents a feature with the type `type`
```csharp
public class SigStat.Common.FeatureDescriptor<T>
    : FeatureDescriptor

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>FeatureDescriptor<T></sub> | <sub>Get(String)</sub> | <sub>Gets the `SigStat.Common.FeatureDescriptor`1` specified by ``.  If the key is not registered yet, a new `SigStat.Common.FeatureDescriptor`1` is automatically created with the given key and type.</sub> | 


#### `Features`

Standard set of features.
```csharp
public static class SigStat.Common.Features

```

###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>IReadOnlyList<FeatureDescriptor></sub> | <sub>All</sub> | <sub>Returns a readonly list of all `SigStat.Common.FeatureDescriptor`s defined in `SigStat.Common.Features`</sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>Altitude</sub> | <sub>Altitude of an online signature as a function of `SigStat.Common.Features.T`</sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>Azimuth</sub> | <sub>Azimuth of an online signature as a function of `SigStat.Common.Features.T`</sub> | 
| <sub>FeatureDescriptor<RectangleF></sub> | <sub>Bounds</sub> | <sub>Actual bounds of the signature</sub> | 
| <sub>FeatureDescriptor<List<Boolean>></sub> | <sub>Button</sub> | <sub>Pen position of an online signature as a function of `SigStat.Common.Features.T`</sub> | 
| <sub>FeatureDescriptor<Point></sub> | <sub>Cog</sub> | <sub>Center of gravity in a signature</sub> | 
| <sub>FeatureDescriptor<Int32></sub> | <sub>Dpi</sub> | <sub>Dots per inch</sub> | 
| <sub>FeatureDescriptor<Image<Rgba32>></sub> | <sub>Image</sub> | <sub>The visaul representation of a signature</sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>Pressure</sub> | <sub>Pressure of an online signature as a function of `SigStat.Common.Features.T`</sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>T</sub> | <sub>Timestamps for online signatures</sub> | 
| <sub>FeatureDescriptor<Rectangle></sub> | <sub>TrimmedBounds</sub> | <sub>Represents the main body of the signature `SigStat.Common.BasicMetadataExtraction`</sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>X</sub> | <sub>X coordinates of an online signature as a function of `SigStat.Common.Features.T`</sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>Y</sub> | <sub>Y coordinates of an online signature as a function of `SigStat.Common.Features.T`</sub> | 


#### `ILoggerObject`

Represents a type, that contains an ILogger property that can be used to perform logging.
```csharp
public interface SigStat.Common.ILoggerObject

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>ILogger</sub> | <sub>Logger</sub> | <sub>Gets or sets the ILogger implementation used to perform logging</sub> | 


#### `ILoggerObjectExtensions`

ILoggerObject extension methods for common scenarios.
```csharp
public static class SigStat.Common.ILoggerObjectExtensions

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Error(this ILoggerObject, String, Object[])</sub> | <sub>Formats and writes an error log message.</sub> | 
| <sub>void</sub> | <sub>Log(this ILoggerObject, String, Object[])</sub> | <sub>Formats and writes an informational log message.</sub> | 
| <sub>void</sub> | <sub>Trace(this ILoggerObject, String, Object[])</sub> | <sub>Formats and writes a trace log message.</sub> | 
| <sub>void</sub> | <sub>Warn(this ILoggerObject, String, Object[])</sub> | <sub>Formats and writes an warning log message.</sub> | 


#### `ITransformation`

Allows implementing a pipeline transform item capable of logging, progress tracking and IO rewiring.
```csharp
public interface SigStat.Common.ITransformation
    : IPipelineIO

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub>Executes the transform on the `` parameter.  This function gets called by the pipeline.</sub> | 


#### `Loop`

Represents a loop in a signature
```csharp
public class SigStat.Common.Loop

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>RectangleF</sub> | <sub>Bounds</sub> | <sub>The bounding rectangle of the loop</sub> | 
| <sub>PointF</sub> | <sub>Center</sub> | <sub>The geometrical center of the looop</sub> | 
| <sub>List<PointF></sub> | <sub>Points</sub> | <sub>A list of defining points of the loop</sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>String</sub> | <sub>ToString()</sub> | <sub>Returns a string representation of the loop</sub> | 


#### `MathHelper`

Common mathematical functions used by the SigStat framework
```csharp
public static class SigStat.Common.MathHelper

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>Min(Double, Double, Double)</sub> | <sub>Returns the smallest of the three double parameters</sub> | 


#### `Origin`

Represents our knowledge on the origin of a signature.
```csharp
public enum SigStat.Common.Origin
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| <sub>Value</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>0</sub> | Unknown | <sub>Use this in practice before a signature is verified.</sub> | 
| <sub>1</sub> | Genuine | <sub>The `SigStat.Common.Signature`'s origin is verified to be from `SigStat.Common.Signature.Signer`</sub> | 
| <sub>2</sub> | Forged | <sub>The `SigStat.Common.Signature` is a forgery.</sub> | 


#### `PipelineBase`

TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.  IProgress, ILogger, IPipelineIO default implementacioja.
```csharp
public abstract class SigStat.Common.PipelineBase
    : ILoggerObject, IProgress, IPipelineIO

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>ILogger</sub> | <sub>Logger</sub> | <sub></sub> | 
| <sub>List<PipelineInput></sub> | <sub>PipelineInputs</sub> | <sub></sub> | 
| <sub>List<PipelineOutput></sub> | <sub>PipelineOutputs</sub> | <sub></sub> | 
| <sub>Int32</sub> | <sub>Progress</sub> | <sub></sub> | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>EventHandler<Int32></sub> | <sub>ProgressChanged</sub> | <sub>The event is raised whenever the value of `SigStat.Common.PipelineBase.Progress` changes</sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>OnProgressChanged()</sub> | <sub>Raises the `SigStat.Common.PipelineBase.ProgressChanged` event</sub> | 


#### `Result`

Contains the benchmark results of a single `SigStat.Common.Signer`
```csharp
public class SigStat.Common.Result

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>Aer</sub> | <sub>Average Error Rate</sub> | 
| <sub>Double</sub> | <sub>Far</sub> | <sub>False Acceptance Rate</sub> | 
| <sub>Double</sub> | <sub>Frr</sub> | <sub>False Rejection Rate</sub> | 
| <sub>String</sub> | <sub>Signer</sub> | <sub>Identifier of the `SigStat.Common.Result.Signer`</sub> | 


#### `Sampler`

Takes samples from a set of `SigStat.Common.Signature`s by given sampling strategies.  Use this to fine-tune the `SigStat.Common.VerifierBenchmark`
```csharp
public class SigStat.Common.Sampler

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Int32</sub> | <sub>BatchSize</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Init(Signer)</sub> | <sub>Initialize the Sampler with a Signer's Signatures.</sub> | 
| <sub>void</sub> | <sub>Init(List<Signer>)</sub> | <sub>Initialize the Sampler with a Signer's Signatures.</sub> | 
| <sub>void</sub> | <sub>Init(List<Signature>)</sub> | <sub>Initialize the Sampler with a Signer's Signatures.</sub> | 
| <sub>List<Signature></sub> | <sub>SampleForgeryTests(Func<List<Signature>, List<Signature>>)</sub> | <sub>Samples a batch of forged signatures to test on.</sub> | 
| <sub>List<Signature></sub> | <sub>SampleGenuineTests(Func<List<Signature>, List<Signature>>)</sub> | <sub>Samples a batch of genuine test signatures to test on.</sub> | 
| <sub>List<Signature></sub> | <sub>SampleReferences(Func<List<Signature>, List<Signature>>)</sub> | <sub>Samples a batch of genuine reference signatures to train on.</sub> | 


#### `Signature`

Represents a signature as a collection of features, containing the data that flows in the pipeline.
```csharp
public class SigStat.Common.Signature

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>String</sub> | <sub>ID</sub> | <sub>An identifier for the Signature. Keep it unique to be useful for logs.</sub> | 
| <sub>Object</sub> | <sub>Item</sub> | <sub>Gets or sets the specified feature.</sub> | 
| <sub>Object</sub> | <sub>Item</sub> | <sub>Gets or sets the specified feature.</sub> | 
| <sub>Origin</sub> | <sub>Origin</sub> | <sub>Represents our knowledge on the origin of the signature. `SigStat.Common.Origin.Unknown` may be used in practice before it is verified.</sub> | 
| <sub>Signer</sub> | <sub>Signer</sub> | <sub>A reference to the `SigStat.Common.Signer` who this signature belongs to. (The origin is not constrained to be genuine.)</sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<Double[]></sub> | <sub>GetAggregateFeature(List<FeatureDescriptor>)</sub> | <sub>Aggregate multiple features into one. Example: X, Y features -&gt; P.xy feature.  Use this for example at DTW algorithm input.</sub> | 
| <sub>T</sub> | <sub>GetFeature(String)</sub> | <sub>Gets the specified feature.</sub> | 
| <sub>T</sub> | <sub>GetFeature(FeatureDescriptor<T>)</sub> | <sub>Gets the specified feature.</sub> | 
| <sub>T</sub> | <sub>GetFeature(FeatureDescriptor)</sub> | <sub>Gets the specified feature.</sub> | 
| <sub>IEnumerable<FeatureDescriptor></sub> | <sub>GetFeatureDescriptors()</sub> | <sub>Gets a collection of `SigStat.Common.FeatureDescriptor`s that are used in this signature.</sub> | 
| <sub>Boolean</sub> | <sub>HasFeature(FeatureDescriptor)</sub> | <sub>Returns true if the signature contains the specified feature</sub> | 
| <sub>Boolean</sub> | <sub>HasFeature(String)</sub> | <sub>Returns true if the signature contains the specified feature</sub> | 
| <sub>void</sub> | <sub>SetFeature(FeatureDescriptor, T)</sub> | <sub>Sets the specified feature.</sub> | 
| <sub>void</sub> | <sub>SetFeature(String, T)</sub> | <sub>Sets the specified feature.</sub> | 
| <sub>String</sub> | <sub>ToString()</sub> | <sub>Returns a string representation of the signature</sub> | 


#### `Signer`

Represents a person as a `SigStat.Common.Signer.ID` and a list of `SigStat.Common.Signer.Signatures`.
```csharp
public class SigStat.Common.Signer

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>String</sub> | <sub>ID</sub> | <sub>An identifier for the Signer. Keep it unique to be useful for logs.</sub> | 
| <sub>List<Signature></sub> | <sub>Signatures</sub> | <sub>List of signatures that belong to the signer.  (Their origin is not constrained to be genuine.)</sub> | 


#### `SigStatEvents`

Standard event identifiers used by the SigStat system
```csharp
public static class SigStat.Common.SigStatEvents

```

###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>EventId</sub> | <sub>BenchmarkEvent</sub> | <sub>Events originating from a benchmark</sub> | 
| <sub>EventId</sub> | <sub>VerifierEvent</sub> | <sub>Events originating from a verifier</sub> | 


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
| <sub>IDataSetLoader</sub> | <sub>Loader</sub> | <sub>The loader that will provide the database for benchmarking</sub> | 
| <sub>ILogger</sub> | <sub>Logger</sub> | <sub>Gets or sets the attached `Microsoft.Extensions.Logging.ILogger` object used to log messages. Hands it over to the verifier.</sub> | 
| <sub>Int32</sub> | <sub>Progress</sub> | <sub></sub> | 
| <sub>Sampler</sub> | <sub>Sampler</sub> | <sub>The `SigStat.Common.Sampler` to be used for benchmarking</sub> | 
| <sub>Verifier</sub> | <sub>Verifier</sub> | <sub>Gets or sets the `SigStat.Common.Model.Verifier` to be benchmarked.</sub> | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>EventHandler<Int32></sub> | <sub>ProgressChanged</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>BenchmarkResults</sub> | <sub>Execute(Boolean = True)</sub> | <sub>Execute the benchmarking process.</sub> | 


