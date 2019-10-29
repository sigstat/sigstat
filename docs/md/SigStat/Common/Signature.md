# [Signature](./Signature.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

Implements [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[KeyValuePair](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2)\<[FeatureDescriptor](./FeatureDescriptor.md), [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)>>, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)

## Summary
Represents a signature as a collection of features, containing the data that flows in the pipeline.

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| Signature (  ) | Initializes a signature instance | 
| Signature ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Origin`](./Origin.md), [`Signer`](./Signer.md) ) | Initializes a signature instance with the given properties | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| ID | An identifier for the Signature. Keep it unique to be useful for logs. | 
| Item [ [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ] | Gets or sets the specified feature. | 
| Item [ [`FeatureDescriptor`](./FeatureDescriptor.md) ] | Gets or sets the specified feature. | 
| Origin | Represents our knowledge on the origin of the signature. [Unknown](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Origin.md) may be used in practice before it is verified. | 
| Signer | A reference to the [Signer](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signer.md) who this signature belongs to. (The origin is not constrained to be genuine.) | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [GetAggregateFeature](./Methods/Signature--GetAggregateFeature.md) ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`FeatureDescriptor`](./FeatureDescriptor.md)> ) | Aggregate multiple features into one. Example: X, Y features -&gt; P.xy feature.  Use this for example at DTW algorithm input. | 
| [GetEnumerator](./Methods/Signature--GetEnumerator.md) (  ) | Returns an enumerator that iterates through the features. | 
| [GetFeature](./Methods/Signature--GetFeature.md) ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Gets the specified feature. | 
| [GetFeature](./Methods/Signature--GetFeature.md) ( [`FeatureDescriptor`](./FeatureDescriptor-1.md)\<[`T`](./Signature.md)> ) | Gets the specified feature. This is the preferred way. | 
| [GetFeature](./Methods/Signature--GetFeature.md) ( [`FeatureDescriptor`](./FeatureDescriptor.md) ) | Gets the specified feature. This is the preferred way. | 
| [GetFeatureDescriptors](./Methods/Signature--GetFeatureDescriptors.md) (  ) | Gets a collection of [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md)s that are used in this signature. | 
| [HasFeature](./Methods/Signature--HasFeature.md) ( [`FeatureDescriptor`](./FeatureDescriptor.md) ) | Returns true if the signature contains the specified feature | 
| [HasFeature](./Methods/Signature--HasFeature.md) ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Returns true if the signature contains the specified feature | 
| [SetFeature](./Methods/Signature--SetFeature.md) ( [`FeatureDescriptor`](./FeatureDescriptor.md), [`T`](./Signature.md) ) | Sets the specified feature. | 
| [SetFeature](./Methods/Signature--SetFeature.md) ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`T`](./Signature.md) ) | Sets the specified feature. | 
| [ToString](./Methods/Signature--ToString.md) (  ) | Returns a string representation of the signature | 


