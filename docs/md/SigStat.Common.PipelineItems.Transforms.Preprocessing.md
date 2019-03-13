#### `CubicInterpolation`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.CubicInterpolation
    : IInterpolation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<Double></sub> | <sub>FeatureValues</sub> | <sub></sub> | 
| <sub>List<Double></sub> | <sub>TimeValues</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>GetValue(Double)</sub> | <sub></sub> | 


#### `FillPenUpDurations`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<FeatureDescriptor<List<Double>>></sub> | <sub>InputFeatures</sub> | <sub></sub> | 
| <sub>Type</sub> | <sub>InterpolationType</sub> | <sub></sub> | 
| <sub>List<FeatureDescriptor<List<Double>>></sub> | <sub>OutputFeatures</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>TimeInputFeature</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>TimeOutputFeature</sub> | <sub></sub> | 
| <sub>List<TimeSlot></sub> | <sub>TimeSlots</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


#### `FilterPoints`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<FeatureDescriptor<List<Double>>></sub> | <sub>InputFeatures</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>KeyFeatureInput</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>KeyFeatureOutput</sub> | <sub></sub> | 
| <sub>List<FeatureDescriptor<List<Double>>></sub> | <sub>OutputFeatures</sub> | <sub></sub> | 
| <sub>Int32</sub> | <sub>Percentile</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


#### `IInterpolation`

```csharp
public interface SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<Double></sub> | <sub>FeatureValues</sub> | <sub></sub> | 
| <sub>List<Double></sub> | <sub>TimeValues</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>GetValue(Double)</sub> | <sub></sub> | 


#### `LinearInterpolation`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.LinearInterpolation
    : IInterpolation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<Double></sub> | <sub>FeatureValues</sub> | <sub></sub> | 
| <sub>List<Double></sub> | <sub>TimeValues</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>GetValue(Double)</sub> | <sub></sub> | 


#### `NormalizeRotation`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>InputT</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>InputX</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>InputY</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>OutputX</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>OutputY</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


#### `OriginType`

```csharp
public enum SigStat.Common.PipelineItems.Transforms.Preprocessing.OriginType
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| <sub>Value</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>0</sub> | <sub>CenterOfGravity</sub> | <sub></sub> | 
| <sub>1</sub> | <sub>Minimum</sub> | <sub></sub> | 
| <sub>2</sub> | <sub>Maximum</sub> | <sub></sub> | 
| <sub>3</sub> | <sub>Predefined</sub> | <sub></sub> | 


#### `ResampleSamplesCountBased`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleSamplesCountBased
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<FeatureDescriptor<List<Double>>></sub> | <sub>InputFeatures</sub> | <sub></sub> | 
| <sub>Type</sub> | <sub>InterpolationType</sub> | <sub></sub> | 
| <sub>Int32</sub> | <sub>NumOfSamples</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>OriginalTFeature</sub> | <sub></sub> | 
| <sub>List<FeatureDescriptor<List<Double>>></sub> | <sub>OutputFeatures</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>ResampledTFeature</sub> | <sub></sub> | 
| <sub>List<Double></sub> | <sub>ResampledTimestamps</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


#### `ResampleTimeBased`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleTimeBased
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>List<FeatureDescriptor<List<Double>>></sub> | <sub>InputFeatures</sub> | <sub></sub> | 
| <sub>Type</sub> | <sub>InterpolationType</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>OriginalTFeature</sub> | <sub></sub> | 
| <sub>List<FeatureDescriptor<List<Double>>></sub> | <sub>OutputFeatures</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>ResampledTFeature</sub> | <sub></sub> | 
| <sub>List<Double></sub> | <sub>ResampledTimestamps</sub> | <sub></sub> | 
| <sub>Double</sub> | <sub>TimeSlot</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


#### `Scale`

<sub>Maps values of a feature to a specific range.  <para>InputFeature: feature to be scaled.</para><para>OutputFeature: output feature for scaled InputFeature&gt;</para></sub>
```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>InputFeature</sub> | <sub></sub> | 
| <sub>Double</sub> | <sub>NewMaxValue</sub> | <sub><para>NewMaxValue: upper bound of the interval, in which the input feature will be scaled</para></sub> | 
| <sub>Double</sub> | <sub>NewMinValue</sub> | <sub><para>NewMinValue: lower bound of the interval, in which the input feature will be scaled</para></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>OutputFeature</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


#### `TranslatePreproc`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>OriginType</sub> | <sub>GoalOrigin</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>InputFeature</sub> | <sub></sub> | 
| <sub>Double</sub> | <sub>NewOrigin</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>OutputFeature</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


#### `UniformScale`

<sub>Maps values of a feature to a specific range and another proportional.  <para>BaseDimension: feature modelled the base dimension of the scaling. </para><para>ProportionalDimension: feature modelled the dimension scaled proportionally to the base dimension. </para><para>BaseDimensionOutput: output feature for scaled BaseDimension&gt;</para><para>ProportionalDimensionOutput: output feature for scaled ProportionalDimension&gt;</para></sub>
```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>BaseDimension</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>BaseDimensionOutput</sub> | <sub></sub> | 
| <sub>Double</sub> | <sub>NewMaxBaseValue</sub> | <sub>Upper bound of the interval, in which the base dimension will be scaled</sub> | 
| <sub>Double</sub> | <sub>NewMinBaseValue</sub> | <sub>Lower bound of the interval, in which the base dimension will be scaled</sub> | 
| <sub>Double</sub> | <sub>NewMinProportionalValue</sub> | <sub>Lower bound of the interval, in which the proportional dimension will be scaled</sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>ProportionalDimension</sub> | <sub></sub> | 
| <sub>FeatureDescriptor<List<Double>></sub> | <sub>ProportionalDimensionOutput</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


