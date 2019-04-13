# [Signature](./Signature.md)

Namespace: [SigStat]() > [Common]()

Assembly: SigStat.Common.dll

## Summary
Represents a signature as a collection of features, containing the data that flows in the pipeline.

## Constructors

| Name | Summary | 
| --- | --- | 
| Signature (  ) | Initializes a signature instance | 
| Signature ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Origin`](./Origin.md), [`Signer`](./Signer.md) ) | Initializes a signature instance with the given properties | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ID | An identifier for the Signature. Keep it unique to be useful for logs. | 
| [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) | Item [ [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ] | Gets or sets the specified feature. | 
| [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) | Item [ [`FeatureDescriptor`](./FeatureDescriptor.md) ] | Gets or sets the specified feature. | 
| [Origin](./Origin.md) | Origin | Represents our knowledge on the origin of the signature. `SigStat.Common.Origin.Unknown` may be used in practice before it is verified. | 
| [Signer](./Signer.md) | Signer | A reference to the `SigStat.Common.Signer` who this signature belongs to. (The origin is not constrained to be genuine.) | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[]> | GetAggregateFeature ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`FeatureDescriptor`](./FeatureDescriptor.md)> ) | Aggregate multiple features into one. Example: X, Y features -&gt; P.xy feature.  Use this for example at DTW algorithm input. | 
| [T](./Signature.md) | GetFeature ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Gets the specified feature. | 
| [T](./Signature.md) | GetFeature ( [`FeatureDescriptor`](./FeatureDescriptor-1.md)\<[`T`](./Signature.md)> ) |  | 
| [T](./Signature.md) | GetFeature ( [`FeatureDescriptor`](./FeatureDescriptor.md) ) | Gets the specified feature. This is the preferred way. | 
| [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[FeatureDescriptor](./FeatureDescriptor.md)> | GetFeatureDescriptors (  ) | Gets a collection of `SigStat.Common.FeatureDescriptor`s that are used in this signature. | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | HasFeature ( [`FeatureDescriptor`](./FeatureDescriptor.md) ) | Returns true if the signature contains the specified feature | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | HasFeature ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Returns true if the signature contains the specified feature | 
| void | SetFeature ( [`FeatureDescriptor`](./FeatureDescriptor.md), [`T`](./Signature.md) ) |  | 
| void | SetFeature ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`T`](./Signature.md) ) |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ToString (  ) | Returns a string representation of the signature | 


