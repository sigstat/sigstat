# [Register](./FeatureDescriptor--Register.md)

Registers a new [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) with a given key.  If the FeatureDescriptor is allready registered, this function will  return a reference to the originally registered FeatureDescriptor.  to the a

| Return<div><a href="#"><img width=225></a></div> | Name<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [FeatureDescriptor](./../FeatureDescriptor.md) | [Register](./FeatureDescriptor--Register.md) ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) featureKey, [`Type`](https://docs.microsoft.com/en-us/dotnet/api/System.Type) type ) | 


#### Parameters
**`featureKey`**  [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)<br>The key for identifying the FeatureDescriptor<br><br>**`type`**  [`Type`](https://docs.microsoft.com/en-us/dotnet/api/System.Type)<br>The type of the actual feature values represented by [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md)
#### Returns
[FeatureDescriptor](./../FeatureDescriptor.md)<br>
A reference to the registered [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) instance