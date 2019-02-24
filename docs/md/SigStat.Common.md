##`ArrayExtension`

```csharp
public staticclass SigStat.Common.ArrayExtension

```

Static Methods

|Type|Name|Summary|
|---|---|---|
|`T[]`|Clone(this`T[]`array)||
|`T[][]`|CreateNested(`Int32`length1,`Int32`length2)||
|`T[]`|ForEach(this`T[]`array,`Action<T>`action)|Performs a given action on all items of the array and returns the original array.|
|`IEnumerable<T>`|GetColumn(this`T[,]`array,`Int32`colIndex)||
|`T[,]`|GetPart(this`T[,]`source,`Int32`startIndex1,`Int32`startIndex2,`Int32`length1,`Int32`length2)||
|`IEnumerable<T>`|GetRow(this`T[,]`array,`Int32`rowIndex)||
|`Tuple<Int32,Int32>`|IndexOf(this`Int32[,]`array,`Int32`value)||
|`Tuple<Int32,Int32>`|IndexOf(this`Double[,]`array,`Double`value)||
|`Int32`|IndexOf(this`T[]`array,`T`value)||
|`Int32`|Max(this`Int32[,]`array)||
|`Byte`|Max(this`Byte[,]`array)||
|`Double`|Max(this`Double[,]`array)||
|`void`|SetColumn(this`T[,]`array,`Int32`x,`T`value)||
|`void`|SetRow(this`T[,]`array,`Int32`y,`T`value)||
|`T[,]`|SetValues(this`T[,]`array,`T`value)||
|`T[]`|Shuffle(this`T[]`array)||


##`Baseline`

```csharp
public class SigStat.Common.Baseline

```

Properties

|Type|Name|Summary|
|---|---|---|
|`PointF`|End||
|`PointF`|Start||


Methods

|Type|Name|Summary|
|---|---|---|
|`String`|ToString()||


##`Configuration`

```csharp
public class SigStat.Common.Configuration

```

Properties

|Type|Name|Summary|
|---|---|---|
|`String`|DatabaseFolder||
|`Lazy<Configuration>`|Default||


##`DataSet`

```csharp
public class SigStat.Common.DataSet

```

Properties

|Type|Name|Summary|
|---|---|---|
|`List<Signer>`|Signers||


##`FeatureAttribute`

```csharp
public class SigStat.Common.FeatureAttribute
    : Attribute,_Attribute

```

Properties

|Type|Name|Summary|
|---|---|---|
|`String`|FeatureKey||


##`FeatureDescriptor`

Represents a feature with name and type.
```csharp
public class SigStat.Common.FeatureDescriptor

```

Properties

|Type|Name|Summary|
|---|---|---|
|`Type`|FeatureType|Gets or sets the type of the feature.|
|`Boolean`|IsCollection|Gets whether the type of the feature is List.|
|`String`|Key|Gets the unique key of the feature.|
|`String`|Name|Gets or sets a human readable name of the feature.|


Methods

|Type|Name|Summary|
|---|---|---|
|`String`|ToString()|Returns a string represenatation of the FeatureDescriptor|


Static Fields

|Type|Name|Summary|
|---|---|---|
|`Dictionary<String,FeatureDescriptor>`|descriptors|The static dictionary of all descriptors.|


Static Methods

|Type|Name|Summary|
|---|---|---|
|`FeatureDescriptor`|Get(`String`key)|Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key.|
|`FeatureDescriptor<T>`|Get(`String`key)|Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key.|
|`Boolean`|IsRegistered(`String`featureKey)||
|`FeatureDescriptor`|Register(`String`featureKey,`Type`type)||


##`FeatureDescriptor<T>`

Represents a feature with the type `type`
```csharp
public class SigStat.Common.FeatureDescriptor<T>
    : FeatureDescriptor

```

Static Methods

|Type|Name|Summary|
|---|---|---|
|`FeatureDescriptor<T>`|Get(`String`key)|Gets the `SigStat.Common.FeatureDescriptor`1` specified by ``.  If the key is not registered yet, a new `SigStat.Common.FeatureDescriptor`1` is automatically created with the given key and type.|


##`Features`

Standard set of features.
```csharp
public staticclass SigStat.Common.Features

```

Static Fields

|Type|Name|Summary|
|---|---|---|
|`IReadOnlyList<FeatureDescriptor>`|All||
|`FeatureDescriptor<List<Double>>`|Altitude||
|`FeatureDescriptor<List<Double>>`|Azimuth||
|`FeatureDescriptor<RectangleF>`|Bounds||
|`FeatureDescriptor<List<Boolean>>`|Button||
|`FeatureDescriptor<Point>`|Cog||
|`FeatureDescriptor<Int32>`|Dpi||
|`FeatureDescriptor<Image<Rgba32>>`|Image||
|`FeatureDescriptor<List<Double>>`|Pressure||
|`FeatureDescriptor<List<Double>>`|T||
|`FeatureDescriptor<Rectangle>`|TrimmedBounds||
|`FeatureDescriptor<List<Double>>`|X||
|`FeatureDescriptor<List<Double>>`|Y||


##`IClassification`

Allows implementing a pipeline classifier item capable of logging, progress tracking and IO rewiring.
```csharp
public interface SigStat.Common.IClassification
    : ILogger,IProgress,IPipelineIO

```

Methods

|Type|Name|Summary|
|---|---|---|
|`Double`|Pair(`Signature`signature1,`Signature`signature2)|Executes the classification by pairing the parameters.  This function gets called by the pipeline.|


##`IClassificationMethods`

Extension methods for `SigStat.Common.IClassification` for convenient IO rewiring.
```csharp
public staticclass SigStat.Common.IClassificationMethods

```

Static Methods

|Type|Name|Summary|
|---|---|---|
|`IClassification`|Input(this`IClassification`caller,`FeatureDescriptor[]`inputFeatures)|Sets the InputFeatures in a convenient way.|
|`IClassification`|Output(this`IClassification`caller,`FeatureDescriptor[]`outputFeatures)|Sets the OutputFeatures in a convenient way.|


##`ITransformation`

Allows implementing a pipeline transform item capable of logging, progress tracking and IO rewiring.
```csharp
public interface SigStat.Common.ITransformation
    : ILogger,IProgress,IPipelineIO

```

Methods

|Type|Name|Summary|
|---|---|---|
|`void`|Transform(`Signature`signature)|Executes the transform on the `` parameter.  This function gets called by the pipeline.|


##`ITransformationMethods`

Extension methods for `SigStat.Common.ITransformation` for convenient IO rewiring.
```csharp
public staticclass SigStat.Common.ITransformationMethods

```

Static Methods

|Type|Name|Summary|
|---|---|---|
|`ITransformation`|Input(this`ITransformation`caller,`FeatureDescriptor[]`inputFeatures)|Sets the InputFeatures in a convenient way.|
|`ITransformation`|Output(this`ITransformation`caller,`FeatureDescriptor[]`outputFeatures)|Sets the OutputFeatures in a convenient way.|


##`Loop`

```csharp
public class SigStat.Common.Loop

```

Properties

|Type|Name|Summary|
|---|---|---|
|`RectangleF`|Bounds||
|`PointF`|Center||
|`List<PointF>`|Points||


Methods

|Type|Name|Summary|
|---|---|---|
|`String`|ToString()||


##`MathHelper`

```csharp
public staticclass SigStat.Common.MathHelper

```

Static Methods

|Type|Name|Summary|
|---|---|---|
|`Double`|Min(`Double`d1,`Double`d2,`Double`d3)|Returns the smallest of the three double parameters|


##`Matrix`

```csharp
public staticclass SigStat.Common.Matrix

```

Static Methods

|Type|Name|Summary|
|---|---|---|
|`E[,]`|Evaluate(`T[,]`matrix,`ItemEvaluator<E,T>`evaluator)||
|`T[,]`|FromTableRows(`IEnumerable<DataRow>`rows,`Int32`ignoreColumns,`Int32`ignoreRows)|Egy DataRow gyüjteményt átalakít egy kétdimenziós tömbbé.  Az átalakítás során ignoreColumns oszlopot és ignoreRows sort  figyelmen kívül hagy.|
|`Point`|GetCog(`Double[,]`weightMartix)||
|`IEnumerable<Point>`|GetNeighbourPixels(this`Point`p)||
|`IEnumerable<Point>`|GetNeighbours(this`Point`p,`Point`start,`Int32`offset)||
|`Double`|GetSum(`Double[,]`matrix,`Int32`x1,`Int32`y1,`Int32`x2,`Int32`y2)||
|`Double`|GetSumCol(`Double[,]`matrix,`Int32`col)||
|`Double`|GetSumRow(`Double[,]`matrix,`Int32`row)||
|`Boolean[,]`|Invert(this`Boolean[,]`array)|returns a copy of the array with inverted values|
|`Byte[,]`|Neighbours(`T[,]`matrix,`T`emptyValue)|returns a same sized matrix with each item showing the neighbour count for the given position.|
|`T[]`|SetValues(this`T[]`array,`T`value)||
|`T[]`|SetValues(this`T[]`array,`Func<T,T>`transformation)||


##`Origin`

Represents our knowledge on the origin of a signature.
```csharp
public enum SigStat.Common.Origin
    : Enum,IComparable,IFormattable,IConvertible

```

Enum

|Value|Name|Summary|
|---|---|---|
|`0`|Unknown|Use this in practice before a signature is verified.|
|`1`|Genuine|The `SigStat.Common.Signature`'s origin is verified to be from `SigStat.Common.Signature.Signer`|
|`2`|Forged|The `SigStat.Common.Signature` is a forgery.|


##`PipelineBase`

TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.  IProgress, ILogger, IPipelineIO default implementacioja.
```csharp
public abstractclass SigStat.Common.PipelineBase

```

Properties

|Type|Name|Summary|
|---|---|---|
|`List<FeatureDescriptor>`|InputFeatures||
|`Logger`|Logger||
|`List<FeatureDescriptor>`|OutputFeatures||
|`Int32`|Progress||


Events

|Type|Name|Summary|
|---|---|---|
|`EventHandler<Int32>`|ProgressChanged||


Methods

|Type|Name|Summary|
|---|---|---|
|`void`|Log(`LogLevel`level,`String`message)|Enqueues a new log entry to be consumed by the attached `SigStat.Common.Helpers.Logger`. Use this when developing new pipeline items.|
|`void`|OnProgressChanged(`Int32`v)|Used to raise base class event in derived classes.  See explanation: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-raise-base-class-events-in-derived-classes">Event docs link</see>.|


##`Signature`

Represents a signature as a collection of features, containing the data that flows in the pipeline.
```csharp
public class SigStat.Common.Signature

```

Properties

|Type|Name|Summary|
|---|---|---|
|`String`|ID|An identifier for the Signature. Keep it unique to be useful for logs.|
|`Object`|Item|Gets or sets the specified feature.|
|`Object`|Item|Gets or sets the specified feature.|
|`Origin`|Origin|Represents our knowledge on the origin of the signature. `SigStat.Common.Origin.Unknown` may be used in practice before it is verified.|
|`Signer`|Signer|A reference to the `SigStat.Common.Signer` who this signature belongs to. (The origin is not constrained to be genuine.)|


Methods

|Type|Name|Summary|
|---|---|---|
|`List<Double[]>`|GetAggregateFeature(`List<FeatureDescriptor>`fs)|Aggregate multiple features into one. Example: X, Y features -&gt; P.xy feature.  Use this for example at DTW algorithm input.|
|`T`|GetFeature(`String`featureKey)|Gets the specified feature.|
|`T`|GetFeature(`FeatureDescriptor<T>`featureDescriptor)|Gets the specified feature.|
|`T`|GetFeature(`FeatureDescriptor`featureDescriptor)|Gets the specified feature.|
|`IEnumerable<FeatureDescriptor>`|GetFeatureDescriptors()|Gets a collection of `SigStat.Common.FeatureDescriptor`s that are used in this signature.|
|`Boolean`|HasFeature(`FeatureDescriptor`featureDescriptor)|Returns true if the signature contains the specified feature|
|`Boolean`|HasFeature(`String`featureKey)|Returns true if the signature contains the specified feature|
|`void`|SetFeature(`FeatureDescriptor`featureDescriptor,`T`feature)|Sets the specified feature.|
|`void`|SetFeature(`String`featureKey,`T`feature)|Sets the specified feature.|
|`String`|ToString()|Returns a string representation of the signature|


##`Signer`

Represents a person as a `SigStat.Common.Signer.ID` and a list of `SigStat.Common.Signer.Signatures`.
```csharp
public class SigStat.Common.Signer

```

Properties

|Type|Name|Summary|
|---|---|---|
|`String`|ID|An identifier for the Signer. Keep it unique to be useful for logs.|
|`List<Signature>`|Signatures|List of signatures that belong to the signer.  (Their origin is not constrained to be genuine.)|


##`Vector`

```csharp
public class SigStat.Common.Vector

```

Properties

|Type|Name|Summary|
|---|---|---|
|`Double`|Angle||
|`Double`|B||
|`Rectangle`|BoundingRectangle||
|`Rectangle`|Bounds||
|`Point`|COG||
|`Point`|End||
|`Double`|Length||
|`Point`|Location||
|`Double`|M||
|`Point`|Start||
|`Int32`|Vx||
|`Int32`|Vy||
|`Int32`|X||
|`Int32`|X2||
|`Int32`|Y||
|`Int32`|Y2||


Methods

|Type|Name|Summary|
|---|---|---|
|`void`|Add(`Point`p)||
|`Vector`|Clone()||
|`Boolean`|Equals(`Object`obj)|Két vektor akkor egyenlő, ha ugyanabból a pontból indulnak ki és ugyanabban az irányba  mutatnak és hosszuk is megegyezik.|
|`IEnumerator<VectorPoint>`|GetEnumerator()||
|`Int32`|GetHashCode()||
|`Double`|GetLength()||
|`Vector`|GetNormal()|Elofrgatja a vektort 90 fokkal a kezdőpontja körül az óramutató járásával megegyező irányba|
|`Vector`|GetNormal(`Double`length)|Elofrgatja a vektort 90 fokkal a kezdőpontja körül az óramutató járásával megegyező irányba|
|`String`|ToMatlabString()||
|`String`|ToString()||


