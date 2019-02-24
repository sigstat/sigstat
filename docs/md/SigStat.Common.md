#### `ArrayExtension`

```csharp
public static class SigStat.Common.ArrayExtension

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `T[]` | <sub>Clone(this T[])</sub> |  | 
| `T[][]` | <sub>CreateNested(Int32, Int32)</sub> |  | 
| `T[]` | <sub>ForEach(this T[], Action<T>)</sub> | Performs a given action on all items of the array and returns the original array. | 
| `IEnumerable<T>` | <sub>GetColumn(this T[,], Int32)</sub> |  | 
| `T[,]` | <sub>GetPart(this T[,], Int32, Int32, Int32, Int32)</sub> |  | 
| `IEnumerable<T>` | <sub>GetRow(this T[,], Int32)</sub> |  | 
| `Tuple<Int32, Int32>` | <sub>IndexOf(this Int32[,], Int32)</sub> |  | 
| `Tuple<Int32, Int32>` | <sub>IndexOf(this Double[,], Double)</sub> |  | 
| `Int32` | <sub>IndexOf(this T[], T)</sub> |  | 
| `Int32` | <sub>Max(this Int32[,])</sub> |  | 
| `Byte` | <sub>Max(this Byte[,])</sub> |  | 
| `Double` | <sub>Max(this Double[,])</sub> |  | 
| `void` | <sub>SetColumn(this T[,], Int32, T)</sub> |  | 
| `void` | <sub>SetRow(this T[,], Int32, T)</sub> |  | 
| `T[,]` | <sub>SetValues(this T[,], T)</sub> |  | 
| `T[]` | <sub>Shuffle(this T[])</sub> |  | 


#### `Baseline`

```csharp
public class SigStat.Common.Baseline

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `PointF` | <sub>End</sub> |  | 
| `PointF` | <sub>Start</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>ToString()</sub> |  | 


#### `Configuration`

```csharp
public class SigStat.Common.Configuration

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>DatabaseFolder</sub> |  | 
| `Lazy<Configuration>` | <sub>Default</sub> |  | 


#### `DataSet`

```csharp
public class SigStat.Common.DataSet

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<Signer>` | <sub>Signers</sub> |  | 


#### `FeatureAttribute`

```csharp
public class SigStat.Common.FeatureAttribute
    : Attribute, _Attribute

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>FeatureKey</sub> |  | 


#### `FeatureDescriptor`

Represents a feature with name and type.
```csharp
public class SigStat.Common.FeatureDescriptor

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Type` | <sub>FeatureType</sub> | Gets or sets the type of the feature. | 
| `Boolean` | <sub>IsCollection</sub> | Gets whether the type of the feature is List. | 
| `String` | <sub>Key</sub> | Gets the unique key of the feature. | 
| `String` | <sub>Name</sub> | Gets or sets a human readable name of the feature. | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>ToString()</sub> | Returns a string represenatation of the FeatureDescriptor | 


###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Dictionary<String, FeatureDescriptor>` | <sub>descriptors</sub> | The static dictionary of all descriptors. | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor` | <sub>Get(String)</sub> | Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key. | 
| `FeatureDescriptor<T>` | <sub>Get(String)</sub> | Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key. | 
| `Boolean` | <sub>IsRegistered(String)</sub> |  | 
| `FeatureDescriptor` | <sub>Register(String, Type)</sub> |  | 


#### `FeatureDescriptor<T>`

Represents a feature with the type `type`
```csharp
public class SigStat.Common.FeatureDescriptor<T>
    : FeatureDescriptor

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<T>` | <sub>Get(String)</sub> | Gets the `SigStat.Common.FeatureDescriptor`1` specified by ``.  If the key is not registered yet, a new `SigStat.Common.FeatureDescriptor`1` is automatically created with the given key and type. | 


#### `Features`

Standard set of features.
```csharp
public static class SigStat.Common.Features

```

###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IReadOnlyList<FeatureDescriptor>` | <sub>All</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Altitude</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Azimuth</sub> |  | 
| `FeatureDescriptor<RectangleF>` | <sub>Bounds</sub> |  | 
| `FeatureDescriptor<List<Boolean>>` | <sub>Button</sub> |  | 
| `FeatureDescriptor<Point>` | <sub>Cog</sub> |  | 
| `FeatureDescriptor<Int32>` | <sub>Dpi</sub> |  | 
| `FeatureDescriptor<Image<Rgba32>>` | <sub>Image</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Pressure</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>T</sub> |  | 
| `FeatureDescriptor<Rectangle>` | <sub>TrimmedBounds</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>X</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Y</sub> |  | 


#### `IClassification`

Allows implementing a pipeline classifier item capable of logging, progress tracking and IO rewiring.
```csharp
public interface SigStat.Common.IClassification
    : ILogger, IProgress, IPipelineIO

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Pair(Signature, Signature)</sub> | Executes the classification by pairing the parameters.  This function gets called by the pipeline. | 


#### `IClassificationMethods`

Extension methods for `SigStat.Common.IClassification` for convenient IO rewiring.
```csharp
public static class SigStat.Common.IClassificationMethods

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IClassification` | <sub>Input(this IClassification, FeatureDescriptor[])</sub> | Sets the InputFeatures in a convenient way. | 
| `IClassification` | <sub>Output(this IClassification, FeatureDescriptor[])</sub> | Sets the OutputFeatures in a convenient way. | 


#### `ITransformation`

Allows implementing a pipeline transform item capable of logging, progress tracking and IO rewiring.
```csharp
public interface SigStat.Common.ITransformation
    : ILogger, IProgress, IPipelineIO

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> | Executes the transform on the `` parameter.  This function gets called by the pipeline. | 


#### `ITransformationMethods`

Extension methods for `SigStat.Common.ITransformation` for convenient IO rewiring.
```csharp
public static class SigStat.Common.ITransformationMethods

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `ITransformation` | <sub>Input(this ITransformation, FeatureDescriptor[])</sub> | Sets the InputFeatures in a convenient way. | 
| `ITransformation` | <sub>Output(this ITransformation, FeatureDescriptor[])</sub> | Sets the OutputFeatures in a convenient way. | 


#### `Loop`

```csharp
public class SigStat.Common.Loop

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `RectangleF` | <sub>Bounds</sub> |  | 
| `PointF` | <sub>Center</sub> |  | 
| `List<PointF>` | <sub>Points</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>ToString()</sub> |  | 


#### `MathHelper`

```csharp
public static class SigStat.Common.MathHelper

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Min(Double, Double, Double)</sub> | Returns the smallest of the three double parameters | 


#### `Matrix`

```csharp
public static class SigStat.Common.Matrix

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `E[,]` | <sub>Evaluate(T[,], ItemEvaluator<E, T>)</sub> |  | 
| `T[,]` | <sub>FromTableRows(IEnumerable<DataRow>, Int32, Int32)</sub> | Egy DataRow gyüjteményt átalakít egy kétdimenziós tömbbé.  Az átalakítás során ignoreColumns oszlopot és ignoreRows sort  figyelmen kívül hagy. | 
| `Point` | <sub>GetCog(Double[,])</sub> |  | 
| `IEnumerable<Point>` | <sub>GetNeighbourPixels(this Point)</sub> |  | 
| `IEnumerable<Point>` | <sub>GetNeighbours(this Point, Point, Int32)</sub> |  | 
| `Double` | <sub>GetSum(Double[,], Int32, Int32, Int32, Int32)</sub> |  | 
| `Double` | <sub>GetSumCol(Double[,], Int32)</sub> |  | 
| `Double` | <sub>GetSumRow(Double[,], Int32)</sub> |  | 
| `Boolean[,]` | <sub>Invert(this Boolean[,])</sub> | returns a copy of the array with inverted values | 
| `Byte[,]` | <sub>Neighbours(T[,], T)</sub> | returns a same sized matrix with each item showing the neighbour count for the given position. | 
| `T[]` | <sub>SetValues(this T[], T)</sub> |  | 
| `T[]` | <sub>SetValues(this T[], Func<T, T>)</sub> |  | 


#### `Origin`

Represents our knowledge on the origin of a signature.
```csharp
public enum SigStat.Common.Origin
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| <sub>Value</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `0` | Unknown | Use this in practice before a signature is verified. | 
| `1` | Genuine | The `SigStat.Common.Signature`'s origin is verified to be from `SigStat.Common.Signature.Signer` | 
| `2` | Forged | The `SigStat.Common.Signature` is a forgery. | 


#### `PipelineBase`

TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.  IProgress, ILogger, IPipelineIO default implementacioja.
```csharp
public abstract class SigStat.Common.PipelineBase

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<FeatureDescriptor>` | <sub>InputFeatures</sub> |  | 
| `Logger` | <sub>Logger</sub> |  | 
| `List<FeatureDescriptor>` | <sub>OutputFeatures</sub> |  | 
| `Int32` | <sub>Progress</sub> |  | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `EventHandler<Int32>` | <sub>ProgressChanged</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Log(LogLevel, String)</sub> | Enqueues a new log entry to be consumed by the attached `SigStat.Common.Helpers.Logger`. Use this when developing new pipeline items. | 
| `void` | <sub>OnProgressChanged(Int32)</sub> | Used to raise base class event in derived classes.  See explanation: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-raise-base-class-events-in-derived-classes">Event docs link</see>. | 


#### `Signature`

Represents a signature as a collection of features, containing the data that flows in the pipeline.
```csharp
public class SigStat.Common.Signature

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>ID</sub> | An identifier for the Signature. Keep it unique to be useful for logs. | 
| `Object` | <sub>Item</sub> | Gets or sets the specified feature. | 
| `Object` | <sub>Item</sub> | Gets or sets the specified feature. | 
| `Origin` | <sub>Origin</sub> | Represents our knowledge on the origin of the signature. `SigStat.Common.Origin.Unknown` may be used in practice before it is verified. | 
| `Signer` | <sub>Signer</sub> | A reference to the `SigStat.Common.Signer` who this signature belongs to. (The origin is not constrained to be genuine.) | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<Double[]>` | <sub>GetAggregateFeature(List<FeatureDescriptor>)</sub> | Aggregate multiple features into one. Example: X, Y features -&gt; P.xy feature.  Use this for example at DTW algorithm input. | 
| `T` | <sub>GetFeature(String)</sub> | Gets the specified feature. | 
| `T` | <sub>GetFeature(FeatureDescriptor<T>)</sub> | Gets the specified feature. | 
| `T` | <sub>GetFeature(FeatureDescriptor)</sub> | Gets the specified feature. | 
| `IEnumerable<FeatureDescriptor>` | <sub>GetFeatureDescriptors()</sub> | Gets a collection of `SigStat.Common.FeatureDescriptor`s that are used in this signature. | 
| `Boolean` | <sub>HasFeature(FeatureDescriptor)</sub> | Returns true if the signature contains the specified feature | 
| `Boolean` | <sub>HasFeature(String)</sub> | Returns true if the signature contains the specified feature | 
| `void` | <sub>SetFeature(FeatureDescriptor, T)</sub> | Sets the specified feature. | 
| `void` | <sub>SetFeature(String, T)</sub> | Sets the specified feature. | 
| `String` | <sub>ToString()</sub> | Returns a string representation of the signature | 


#### `Signer`

Represents a person as a `SigStat.Common.Signer.ID` and a list of `SigStat.Common.Signer.Signatures`.
```csharp
public class SigStat.Common.Signer

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>ID</sub> | An identifier for the Signer. Keep it unique to be useful for logs. | 
| `List<Signature>` | <sub>Signatures</sub> | List of signatures that belong to the signer.  (Their origin is not constrained to be genuine.) | 


#### `Vector`

```csharp
public class SigStat.Common.Vector

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Angle</sub> |  | 
| `Double` | <sub>B</sub> |  | 
| `Rectangle` | <sub>BoundingRectangle</sub> |  | 
| `Rectangle` | <sub>Bounds</sub> |  | 
| `Point` | <sub>COG</sub> |  | 
| `Point` | <sub>End</sub> |  | 
| `Double` | <sub>Length</sub> |  | 
| `Point` | <sub>Location</sub> |  | 
| `Double` | <sub>M</sub> |  | 
| `Point` | <sub>Start</sub> |  | 
| `Int32` | <sub>Vx</sub> |  | 
| `Int32` | <sub>Vy</sub> |  | 
| `Int32` | <sub>X</sub> |  | 
| `Int32` | <sub>X2</sub> |  | 
| `Int32` | <sub>Y</sub> |  | 
| `Int32` | <sub>Y2</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Add(Point)</sub> |  | 
| `Vector` | <sub>Clone()</sub> |  | 
| `Boolean` | <sub>Equals(Object)</sub> | Két vektor akkor egyenlő, ha ugyanabból a pontból indulnak ki és ugyanabban az irányba  mutatnak és hosszuk is megegyezik. | 
| `IEnumerator<VectorPoint>` | <sub>GetEnumerator()</sub> |  | 
| `Int32` | <sub>GetHashCode()</sub> |  | 
| `Double` | <sub>GetLength()</sub> |  | 
| `Vector` | <sub>GetNormal()</sub> | Elofrgatja a vektort 90 fokkal a kezdőpontja körül az óramutató járásával megegyező irányba | 
| `Vector` | <sub>GetNormal(Double)</sub> | Elofrgatja a vektort 90 fokkal a kezdőpontja körül az óramutató járásával megegyező irányba | 
| `String` | <sub>ToMatlabString()</sub> |  | 
| `String` | <sub>ToString()</sub> |  | 


