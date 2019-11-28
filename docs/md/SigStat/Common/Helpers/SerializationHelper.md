# [SerializationHelper](./SerializationHelper.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Helpers](./README.md)

Assembly: SigStat.Common.dll

## Summary
Json serialization and deserialization using the custom resolver  [VerifierResolver](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Helpers/Serialization/VerifierResolver.md)

## Constructors

| <span>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Summary | 
| :--- | :--- | 
| SerializationHelper () |  | 


## Static Methods

| <span>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Summary | 
| :--- | :--- | 
| [Deserialize](./Methods/SerializationHelper--Deserialize.md) ([`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)) | Constructs object from strings that were serialized previously | 
| [DeserializeFromFile](./Methods/SerializationHelper--DeserializeFromFile.md) ([`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)) | Constructs object from file given by a path | 
| [GetSettings](./Methods/SerializationHelper--GetSettings.md) () | Settings used for the serialization methods | 
| [JsonSerialize](./Methods/SerializationHelper--JsonSerialize.md) ([`T`](./SerializationHelper.md)) | Creates json string from object | 
| [JsonSerializeToFile](./Methods/SerializationHelper--JsonSerializeToFile.md) ([`T`](./SerializationHelper.md), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)) | Writes object to file to the given by path in json format | 


