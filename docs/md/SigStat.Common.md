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
| `PointF` | End |  | 
| `PointF` | Start |  | 


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
| `String` | DatabaseFolder |  | 
| `Lazy<Configuration>` | Default |  | 


#### `DataSet`

```csharp
public class SigStat.Common.DataSet

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<Signer>` | Signers |  | 


#### `FeatureAttribute`

```csharp
public class SigStat.Common.FeatureAttribute
    : Attribute, _Attribute

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | FeatureKey |  | 


#### `FeatureDescriptor`

<sub>Represents a feature with name and type.</sub>
```csharp
public class SigStat.Common.FeatureDescriptor

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Type` | FeatureType | Gets or sets the type of the feature. | 
| `Boolean` | IsCollection | Gets whether the type of the feature is List. | 
| `String` | Key | Gets the unique key of the feature. | 
| `String` | Name | Gets or sets a human readable name of the feature. | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | <sub>ToString()</sub> | Returns a string represenatation of the FeatureDescriptor | 


###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Dictionary<String, FeatureDescriptor>` | descriptors | The static dictionary of all descriptors. | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor` | <sub>Get(String)</sub> | Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key. | 
| `FeatureDescriptor<T>` | <sub>Get(String)</sub> | Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key. | 
| `Boolean` | <sub>IsRegistered(String)</sub> |  | 
| `FeatureDescriptor` | <sub>Register(String, Type)</sub> |  | 


#### `FeatureDescriptor<T>`

<sub>Represents a feature with the type `type`</sub>
```csharp
public class SigStat.Common.FeatureDescriptor<T>
    : FeatureDescriptor

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<T>` | <sub>Get(String)</sub> | Gets the `SigStat.Common.FeatureDescriptor`1` specified by ``.  If the key is not registered yet, a new `SigStat.Common.FeatureDescriptor`1` is automatically created with the given key and type. | 


#### `Features`

<sub>Standard set of features.</sub>
```csharp
public static class SigStat.Common.Features

```

###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IReadOnlyList<FeatureDescriptor>` | All |  | 
| `FeatureDescriptor<List<Double>>` | Altitude |  | 
| `FeatureDescriptor<List<Double>>` | Azimuth |  | 
| `FeatureDescriptor<RectangleF>` | Bounds |  | 
| `FeatureDescriptor<List<Boolean>>` | Button |  | 
| `FeatureDescriptor<Point>` | Cog |  | 
| `FeatureDescriptor<Int32>` | Dpi |  | 
| `FeatureDescriptor<Image<Rgba32>>` | Image |  | 
| `FeatureDescriptor<List<Double>>` | Pressure |  | 
| `FeatureDescriptor<List<Double>>` | T |  | 
| `FeatureDescriptor<Rectangle>` | TrimmedBounds |  | 
| `FeatureDescriptor<List<Double>>` | X |  | 
| `FeatureDescriptor<List<Double>>` | Y |  | 


#### `IClassification`

<sub>Allows implementing a pipeline classifier item capable of logging, progress tracking and IO rewiring.</sub>
```csharp
public interface SigStat.Common.IClassification
    : ILogger, IProgress, IPipelineIO

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Pair(Signature, Signature)</sub> | Executes the classification by pairing the parameters.  This function gets called by the pipeline. | 


#### `IClassificationMethods`

<sub>Extension methods for `SigStat.Common.IClassification` for convenient IO rewiring.</sub>
```csharp
public static class SigStat.Common.IClassificationMethods

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IClassification` | <sub>Input(this IClassification, FeatureDescriptor[])</sub> | Sets the InputFeatures in a convenient way. | 
| `IClassification` | <sub>Output(this IClassification, FeatureDescriptor[])</sub> | Sets the OutputFeatures in a convenient way. | 


#### `ITransformation`

<sub>Allows implementing a pipeline transform item capable of logging, progress tracking and IO rewiring.</sub>
```csharp
public interface SigStat.Common.ITransformation
    : ILogger, IProgress, IPipelineIO

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> | Executes the transform on the `` parameter.  This function gets called by the pipeline. | 


#### `ITransformationMethods`

<sub>Extension methods for `SigStat.Common.ITransformation` for convenient IO rewiring.</sub>
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
| `RectangleF` | Bounds |  | 
| `PointF` | Center |  | 
| `List<PointF>` | Points |  | 


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

<sub>Represents our knowledge on the origin of a signature.</sub>
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

<sub>TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.  IProgress, ILogger, IPipelineIO default implementacioja.</sub>
```csharp
public abstract class SigStat.Common.PipelineBase

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<FeatureDescriptor>` | InputFeatures |  | 
| `Logger` | Logger |  | 
| `List<FeatureDescriptor>` | OutputFeatures |  | 
| `Int32` | Progress |  | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `EventHandler<Int32>` | ProgressChanged |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Log(LogLevel, String)</sub> | Enqueues a new log entry to be consumed by the attached `SigStat.Common.Helpers.Logger`. Use this when developing new pipeline items. | 
| `void` | <sub>OnProgressChanged(Int32)</sub> | Used to raise base class event in derived classes.  See explanation: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-raise-base-class-events-in-derived-classes">Event docs link</see>. | 


#### `Signature`

<sub>Represents a signature as a collection of features, containing the data that flows in the pipeline.</sub>
```csharp
public class SigStat.Common.Signature

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | ID | An identifier for the Signature. Keep it unique to be useful for logs. | 
| `Object` | Item | Gets or sets the specified feature. | 
| `Object` | Item | Gets or sets the specified feature. | 
| `Origin` | Origin | Represents our knowledge on the origin of the signature. `SigStat.Common.Origin.Unknown` may be used in practice before it is verified. | 
| `Signer` | Signer | A reference to the `SigStat.Common.Signer` who this signature belongs to. (The origin is not constrained to be genuine.) | 


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

<sub>Represents a person as a `SigStat.Common.Signer.ID` and a list of `SigStat.Common.Signer.Signatures`.</sub>
```csharp
public class SigStat.Common.Signer

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `String` | ID | An identifier for the Signer. Keep it unique to be useful for logs. | 
| `List<Signature>` | Signatures | List of signatures that belong to the signer.  (Their origin is not constrained to be genuine.) | 


#### `Vector`

```csharp
public class SigStat.Common.Vector

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | Angle |  | 
| `Double` | B |  | 
| `Rectangle` | BoundingRectangle |  | 
| `Rectangle` | Bounds |  | 
| `Point` | COG |  | 
| `Point` | End |  | 
| `Double` | Length |  | 
| `Point` | Location |  | 
| `Double` | M |  | 
| `Point` | Start |  | 
| `Int32` | Vx |  | 
| `Int32` | Vy |  | 
| `Int32` | X |  | 
| `Int32` | X2 |  | 
| `Int32` | Y |  | 
| `Int32` | Y2 |  | 


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


