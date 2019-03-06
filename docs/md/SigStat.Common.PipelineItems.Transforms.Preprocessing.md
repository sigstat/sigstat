#### `NormalizeRotation`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

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


#### `Scale`

<sub>Maps values of a feature to a specific range.  <para>InputFeature: feature to be scaled.</para><para>OutputFeature: output feature for scaled InputFeature&gt;</para></sub>
```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>Double</sub> | <sub>NewMaxValue</sub> | <sub><para>NewMaxValue: upper bound of the interval, in which the input feature will be scaled</para></sub> | 
| <sub>Double</sub> | <sub>NewMinValue</sub> | <sub><para>NewMinValue: lower bound of the interval, in which the input feature will be scaled</para></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


#### `Translate`

```csharp
public class SigStat.Common.PipelineItems.Transforms.Preprocessing.Translate
    : PipelineBase, ILoggerObject, IProgress, IPipelineIO, ITransformation

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>OriginType</sub> | <sub>GoalOrigin</sub> | <sub></sub> | 
| <sub>Double</sub> | <sub>NewOrigin</sub> | <sub></sub> | 


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
| <sub>Double</sub> | <sub>NewMaxBaseValue</sub> | <sub>Upper bound of the interval, in which the base dimension will be scaled</sub> | 
| <sub>Double</sub> | <sub>NewMinBaseValue</sub> | <sub>Lower bound of the interval, in which the base dimension will be scaled</sub> | 
| <sub>Double</sub> | <sub>NewMinProportionalValue</sub> | <sub>Lower bound of the interval, in which the proportional dimension will be scaled</sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| <sub>void</sub> | <sub>Transform(Signature)</sub> | <sub></sub> | 


