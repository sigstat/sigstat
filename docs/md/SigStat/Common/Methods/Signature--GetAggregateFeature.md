# [GetAggregateFeature](./Signature--GetAggregateFeature.md)

Aggregate multiple features into one. Example: X, Y features -&gt; P.xy feature.  Use this for example at DTW algorithm input.

| Return<div><a href="#"><img width=225></a></div> | Name<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[]> | [GetAggregateFeature](./Signature--GetAggregateFeature.md) ([`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`FeatureDescriptor`](./../FeatureDescriptor.md)> fs ) | 


#### Parameters
**`fs`**  [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`FeatureDescriptor`](./../FeatureDescriptor.md)><br>List of features to aggregate.
#### Returns
[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)[]><br>
Aggregated feature value