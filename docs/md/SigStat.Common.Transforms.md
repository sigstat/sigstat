#### `AddConst`

Adds a constant value to a feature. Works with collection features too.  <para>Default Pipeline Output: Pipeline Input</para>
```csharp
public class SigStat.Common.Transforms.AddConst
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Double>>` | <sub>InputList</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Output</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `AddVector`

Adds a vector feature's elements to other features.  <para>Default Pipeline Output: Pipeline Input</para>
```csharp
public class SigStat.Common.Transforms.AddVector
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<FeatureDescriptor<List<Double>>>` | <sub>Inputs</sub> |  | 
| `FeatureDescriptor<List<List<Double>>>` | <sub>InputsFD</sub> |  | 
| `List<FeatureDescriptor<List<Double>>>` | <sub>Outputs</sub> |  | 
| `FeatureDescriptor<List<List<Double>>>` | <sub>OutputsFD</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `ApproximateOnlineFeatures`

init Pressure, Altitude, Azimuth features with default values.  <para>Default Pipeline Output: Features.Pressure, Features.Altitude, Features.Azimuth</para>
```csharp
public class SigStat.Common.Transforms.ApproximateOnlineFeatures
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `Binarization`

Generates a binary raster version of the input image with the iterative threshold method.  <para>Pipeline Input type: Image{Rgba32}</para><para>Default Pipeline Output: (bool[,]) Binarized</para>
```csharp
public class SigStat.Common.Transforms.Binarization
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<Image<Rgba32>>` | <sub>InputImage</sub> |  | 
| `FeatureDescriptor<Boolean[,]>` | <sub>OutputMask</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `BinaryRasterizer`

Converts standard features to a binary raster.  <para>Default Pipeline Input: Standard `SigStat.Common.Features`</para><para>Default Pipeline Output: (bool[,]) Binarized</para>
```csharp
public class SigStat.Common.Transforms.BinaryRasterizer
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Boolean>>` | <sub>InputButton</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>InputX</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>InputY</sub> |  | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Output</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `CentroidExtraction`

Extracts the Centroid (aka. Center Of Gravity) of the input features.  <para> Default Pipeline Output: (List{double}) Centroid. </para>
```csharp
public class SigStat.Common.Transforms.CentroidExtraction
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<FeatureDescriptor<List<Double>>>` | <sub>Inputs</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>OutputCentroid</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `CentroidTranslate`

Sequential pipeline to translate X and Y `SigStat.Common.Features` to Centroid.  The following Transforms are called: `SigStat.Common.Transforms.CentroidExtraction`, `SigStat.Common.Transforms.Multiply`(-1), `SigStat.Common.Transforms.Translate`<para>Default Pipeline Input: `SigStat.Common.Features.X`, `SigStat.Common.Features.Y`</para><para>Default Pipeline Output: (List{double}) Centroid</para>
```csharp
public class SigStat.Common.Transforms.CentroidTranslate
    : SequentialTransformPipeline, ILoggerObject, IProgress, IPipelineIO, IEnumerable, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Double>>` | <sub>InputX</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>InputY</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>OutputX</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>OutputY</sub> |  | 


#### `ComponentExtraction`

Extracts unsorted components by tracing through the binary Skeleton raster.  <para>Default Pipeline Input: (bool[,]) Skeleton, (List{Point}) EndPoints, (List{Point}) CrossingPoints</para><para>Default Pipeline Output: (List{List{PointF}}) Components</para>
```csharp
public class SigStat.Common.Transforms.ComponentExtraction
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Point>>` | <sub>CrossingPoints</sub> |  | 
| `FeatureDescriptor<List<Point>>` | <sub>EndPoints</sub> |  | 
| `FeatureDescriptor<List<List<PointF>>>` | <sub>OutputComponents</sub> |  | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Skeleton</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `ComponentSorter`

Sorts Component order by comparing each starting X value, and finding nearest components.  <para>Default Pipeline Input: (bool[,]) Components</para><para>Default Pipeline Output: (bool[,]) Components</para>
```csharp
public class SigStat.Common.Transforms.ComponentSorter
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<List<PointF>>>` | <sub>Input</sub> |  | 
| `FeatureDescriptor<List<List<PointF>>>` | <sub>Output</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `ComponentsToFeatures`

Extracts standard `SigStat.Common.Features` from sorted Components.  <para>Default Pipeline Input: (List{List{PointF}}) Components</para><para>Default Pipeline Output: X, Y, Button `SigStat.Common.Features`</para>
```csharp
public class SigStat.Common.Transforms.ComponentsToFeatures
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Boolean>>` | <sub>Button</sub> |  | 
| `FeatureDescriptor<List<List<PointF>>>` | <sub>InputComponents</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>X</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Y</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `EndpointExtraction`

Extracts EndPoints and CrossingPoints from Skeleton.  <para>Default Pipeline Input: (bool[,]) Skeleton</para><para>Default Pipeline Output: (List{Point}) EndPoints, (List{Point}) CrossingPoints </para>
```csharp
public class SigStat.Common.Transforms.EndpointExtraction
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Point>>` | <sub>OutputCrossingPoints</sub> |  | 
| `FeatureDescriptor<List<Point>>` | <sub>OutputEndpoints</sub> |  | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Skeleton</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `Extrema`

Extracts minimum and maximum values of given feature.  <para>Default Pipeline Output: (List{double}) Min, (List{double}) Max </para>
```csharp
public class SigStat.Common.Transforms.Extrema
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `HSCPThinning`

Iteratively thins the input binary raster with the `SigStat.Common.Algorithms.HSCPThinningStep` algorithm.  <para>Pipeline Input type: bool[,]</para><para>Default Pipeline Output: (bool[,]) HSCPThinningResult </para>
```csharp
public class SigStat.Common.Transforms.HSCPThinning
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Input</sub> |  | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Output</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `ImageGenerator`

Generates an image feature out of a binary raster.  Optionally, saves the image to a png file.  Useful for debugging pipeline steps.  <para>Pipeline Input type: bool[,]</para><para>Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage</para>
```csharp
public class SigStat.Common.Transforms.ImageGenerator
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Input</sub> |  | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Output</sub> |  | 
| `FeatureDescriptor<Image<Rgba32>>` | <sub>OutputImage</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `Map`

Maps values of a feature to a specified range.  <para>Pipeline Input type: List{double}</para><para>Default Pipeline Output: (List{double}) MapResult</para>
```csharp
public class SigStat.Common.Transforms.Map
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Double>>` | <sub>Input</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Output</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `Multiply`

Multiplies the values of a feature with a given constant.  <para>Pipeline Input type: List{double}</para><para>Default Pipeline Output: (List{double}) Input</para>
```csharp
public class SigStat.Common.Transforms.Multiply
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Double>>` | <sub>InputList</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Output</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `Normalize`

Maps values of a feature to 0.0 - 1.0 range.  <para>Pipeline Input type: List{double}</para><para>Default Pipeline Output: (List{double}) NormalizationResult</para>
```csharp
public class SigStat.Common.Transforms.Normalize
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Double>>` | <sub>Input</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Output</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `OnePixelThinning`

Iteratively thins the input binary raster with the `SigStat.Common.Algorithms.OnePixelThinningStep` algorithm.  <para>Pipeline Input type: bool[,]</para><para>Default Pipeline Output: (bool[,]) OnePixelThinningResult </para>
```csharp
public class SigStat.Common.Transforms.OnePixelThinning
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Input</sub> |  | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Output</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `RealisticImageGenerator`

Generates a realistic looking image of the Signature based on standard features. Uses blue ink and white paper. It does NOT save the image to file.  <para>Default Pipeline Input: X, Y, Button, Pressure, Azimuth, Altitude `SigStat.Common.Features`</para><para>Default Pipeline Output: `SigStat.Common.Features.Image`</para>
```csharp
public class SigStat.Common.Transforms.RealisticImageGenerator
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `Resize`

Resizes the image to a specified width and height
```csharp
public class SigStat.Common.Transforms.Resize
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<Image<Rgba32>>` | <sub>InputImage</sub> |  | 
| `FeatureDescriptor<Image<Rgba32>>` | <sub>OutputImage</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `TangentExtraction`

Extracts tangent values of the standard X, Y `SigStat.Common.Features`<para>Default Pipeline Input: X, Y `SigStat.Common.Features`</para><para>Default Pipeline Output: (List{double})  Tangent </para>
```csharp
public class SigStat.Common.Transforms.TangentExtraction
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Double>>` | <sub>OutputTangent</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>X</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>Y</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


#### `TimeReset`

Sequential pipeline to reset time values to begin at 0.  The following Transforms are called: Extrema, Multiply, AddVector.  <para>Default Pipeline Input: `SigStat.Common.Features.T`</para><para>Default Pipeline Output: `SigStat.Common.Features.T`</para>
```csharp
public class SigStat.Common.Transforms.TimeReset
    : SequentialTransformPipeline, ILoggerObject, IProgress, IPipelineIO, IEnumerable, ITransformation

```

#### `Translate`

Sequential pipeline to translate X and Y `SigStat.Common.Features` by specified vector (constant or feature).  The following Transforms are called: `SigStat.Common.Transforms.AddConst` twice, or `SigStat.Common.Transforms.AddVector`.  <para>Default Pipeline Input: `SigStat.Common.Features.X`, `SigStat.Common.Features.Y`</para><para>Default Pipeline Output: `SigStat.Common.Features.X`, `SigStat.Common.Features.Y`</para>
```csharp
public class SigStat.Common.Transforms.Translate
    : SequentialTransformPipeline, ILoggerObject, IProgress, IPipelineIO, IEnumerable, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Double>>` | <sub>InputX</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>InputY</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>OutputX</sub> |  | 
| `FeatureDescriptor<List<Double>>` | <sub>OutputY</sub> |  | 


#### `Trim`

Trims unnecessary empty space from a binary raster.  <para>Pipeline Input type: bool[,]</para><para>Default Pipeline Output: (bool[,]) Trimmed</para>
```csharp
public class SigStat.Common.Transforms.Trim
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Input</sub> |  | 
| `FeatureDescriptor<Boolean[,]>` | <sub>Output</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Transform(Signature)</sub> |  | 


