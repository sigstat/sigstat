# [Svc2004Loader](./Svc2004Loader.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Loaders](./README.md)

Assembly: SigStat.Common.dll

Implements [IDataSetLoader](./IDataSetLoader.md), [ILoggerObject](./../ILoggerObject.md)

## Summary
Loads SVC2004-format database from .zip

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>Svc2004Loader ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) )</sub><img width=200/>| <sub>Initializes a new instance of the [Loaders.Svc2004Loader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/Svc2004Loader.md) class with specified database.</sub>| <br>
| <sub>Svc2004Loader ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean), [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)> )</sub><img width=200/>| <sub>Initializes a new instance of the [Loaders.Svc2004Loader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/Svc2004Loader.md) class with specified database.</sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>DatabasePath</sub><img width=200/>| <sub>Gets or sets the database path.</sub>| <br>
| <sub>SignerFilter</sub><img width=200/>| <sub>Ignores any signers during the loading, that do not match the predicate</sub>| <br>
| <sub>StandardFeatures</sub><img width=200/>| <sub>Gets or sets a value indicating whether features are also loaded as [Features](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)</sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[EnumerateSigners](./Methods/Svc2004Loader-100663945.md) ( [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)> )</sub><img width=200/>| <sub></sub>| <br>
| <sub>[LoadSignature](./Methods/Svc2004Loader-100663946.md) ( [`Signature`](./../Signature.md), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) )</sub><img width=200/>| <sub>Loads one signature from specified file path.</sub>| <br>


## Static Methods

| Name | Summary | 
| --- | --- | 
| <sub>[LoadSignature](./Methods/Svc2004Loader-100663947.md) ( [`Signature`](./../Signature.md), [`Stream`](https://docs.microsoft.com/en-us/dotnet/api/System.IO.Stream), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) )</sub><img width=200/>| <sub>Loads one signature from specified stream.</sub>| <br>


