# [FeatureDescriptor](./FeatureDescriptor.md)

Namespace: [SigStat]() > [Common]()

Assembly: SigStat.Common.dll

## Summary
Represents a feature with name and type.

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type) | FeatureType | Gets or sets the type of the feature. | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsCollection | Gets whether the type of the feature is List. | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Key | Gets the unique key of the feature. | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | Gets or sets a human readable name of the feature. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ToString (  ) | Returns a string represenatation of the FeatureDescriptor | 


## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./FeatureDescriptor.md) | Get ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Gets the `SigStat.Common.FeatureDescriptor` specified by ``.  Throws `System.Collections.Generic.KeyNotFoundException` exception if there is no descriptor registered with the given key. | 
| [FeatureDescriptor](./FeatureDescriptor-1.md)\<[T](./FeatureDescriptor.md)> | Get ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Gets the `SigStat.Common.FeatureDescriptor`1` specified by ``.  If the key is not registered yet, a new `SigStat.Common.FeatureDescriptor`1` is automatically created with the given key and type. | 
| [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String), [FeatureDescriptor](./FeatureDescriptor.md)> | GetAll (  ) |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsRegistered ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Returns true, if there is a FeatureDescriptor registered with the given key | 
| [FeatureDescriptor](./FeatureDescriptor.md) | Register ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Type`](https://docs.microsoft.com/en-us/dotnet/api/System.Type) ) | Registers a new `SigStat.Common.FeatureDescriptor` with a given key.  If the FeatureDescriptor is allready registered, this function will  return a reference to the originally registered FeatureDescriptor.  to the a | 


