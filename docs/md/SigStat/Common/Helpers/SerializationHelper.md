# [SerializationHelper](./SerializationHelper.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Helpers](./README.md)

Assembly: SigStat.Common.dll

## Summary
Json serialization and deserialization using the custom resolver  [SigStat.Common.Helpers.Serialization.VerifierResolver]()

## Constructors

| Name | Summary | 
| --- | --- | 
| SerializationHelper (  ) |  | 


## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [T](./SerializationHelper.md) | Deserialize ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) s ) | Constructs object from strings that were serialized previously | 
| [T](./SerializationHelper.md) | DeserializeFromFile ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) path ) | Constructs object from file given by a path | 
| [JsonSerializerSettings](./SerializationHelper.md) | GetSettings (  ) | Settings used for the serialization methods | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | JsonSerialize ( [`T`](./SerializationHelper.md) o ) | Creates json string from object | 
| void | JsonSerializeToFile ( [`T`](./SerializationHelper.md) o, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) path ) | Writes object to file to the given by path in json format | 


