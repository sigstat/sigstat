# [SerializationHelper](./SerializationHelper.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Helpers](./README.md)

Assembly: SigStat.Common.dll

## Summary
Json serialization and deserialization using the custom resolver  [VerifierResolver](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Helpers/Serialization/VerifierResolver.md)

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| SerializationHelper (  ) |  | 


## Static Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Deserialize](./Methods/SerializationHelper--Deserialize.md) ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Constructs object from strings that were serialized previously | 
| [DeserializeFromFile](./Methods/SerializationHelper--DeserializeFromFile.md) ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Constructs object from file given by a path | 
| [GetSettings](./Methods/SerializationHelper--GetSettings.md) (  ) | Settings used for the serialization methods | 
| [JsonSerialize](./Methods/SerializationHelper--JsonSerialize.md) ( [`T`](./SerializationHelper.md) ) | Creates json string from object | 
| [JsonSerializeToFile](./Methods/SerializationHelper--JsonSerializeToFile.md) ( [`T`](./SerializationHelper.md), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) | Writes object to file to the given by path in json format | 


