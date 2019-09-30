# [FeatureDescriptorJsonConverter](./FeatureDescriptorJsonConverter.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Helpers](./README.md)

Assembly: SigStat.Common.dll

## Summary
Custom serializer for [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor.md) objects

## Constructors

| Name | Summary | 
| --- | --- | 
| FeatureDescriptorJsonConverter (  ) |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | CanConvert ( [`Type`](https://docs.microsoft.com/en-us/dotnet/api/System.Type) objectType ) | Tells if the current object is of the correct type | 
| [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) | ReadJson ( [`JsonReader`](./FeatureDescriptorJsonConverter.md) reader, [`Type`](https://docs.microsoft.com/en-us/dotnet/api/System.Type) objectType, [`Object`](https://docs.microsoft.com/en-us/dotnet/api/System.Object) existingValue, [`JsonSerializer`](./FeatureDescriptorJsonConverter.md) serializer ) |  | 
| void | WriteJson ( [`JsonWriter`](./FeatureDescriptorJsonConverter.md) writer, [`Object`](https://docs.microsoft.com/en-us/dotnet/api/System.Object) value, [`JsonSerializer`](./FeatureDescriptorJsonConverter.md) serializer ) |  | 


