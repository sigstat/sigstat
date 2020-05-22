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
| Origin | Represents our knowledge on the origin of the signature. [Unknown](https://github.com/hargitomi97/sigstat/blob/master/docs/md/SigStat/Common/Origin.md) may be used in practice before it is verified. | 
| Signer | A reference to the [Signer](https://github.com/hargitomi97/sigstat/blob/master/docs/md/SigStat/Common/Signer.md) who this signature belongs to. (The origin is not constrained to be genuine.) | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| GetAggregateFeature ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`FeatureDescriptor`](./FeatureDescriptor.md)> ) | Aggregate multiple features into one. Example: X, Y features -&gt; P.xy feature.  Use this for example at DTW algorithm input. | 
| GetEnumerator (  ) | Returns an enumerator that iterates through the features. | 
| GetFeature ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Gets the specified feature. | 
| GetFeature ( [`FeatureDescriptor`](./FeatureDescriptor-1.md)\<[`T`](./Signature.md)> ) | Gets the specified feature. This is the preferred way. | 
| GetFeature ( [`FeatureDescriptor`](./FeatureDescriptor.md) ) | Gets the specified feature. This is the preferred way. | 
| GetFeatureDescriptors (  ) | Gets a collection of [FeatureDescriptor](https://github.com/hargitomi97/sigstat/blob/master/docs/md/SigStat/Common/FeatureDescriptor.md)s that are used in this signature. | 
| HasFeature ( [`FeatureDescriptor`](./FeatureDescriptor.md) ) | Returns true if the signature contains the specified feature | 
| HasFeature ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Returns true if the signature contains the specified feature | 
| SetFeature ( [`FeatureDescriptor`](./FeatureDescriptor.md), [`T`](./Signature.md) ) | Sets the specified feature. | 
| SetFeature ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`T`](./Signature.md) ) | Sets the specified feature. | 
| ToString (  ) | Returns a string representation of the signature | 


