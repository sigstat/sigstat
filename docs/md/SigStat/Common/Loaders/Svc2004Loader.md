# [Svc2004Loader](./Svc2004Loader.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Loaders](./README.md)

Assembly: SigStat.Common.dll

Implements [IDataSetLoader](./IDataSetLoader.md), [ILoggerObject](./../ILoggerObject.md)

## Summary
Loads SVC2004-format database from .zip

## Constructors

| Name | Summary | 
| --- | --- | 
| Svc2004Loader ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) databasePath, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) standardFeatures ) | Initializes a new instance of the [SigStat.Common.Loaders.Svc2004Loader](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Loaders/Svc2004Loader.md) class with specified database. | 
| Svc2004Loader ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) databasePath, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) standardFeatures, [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)> signerFilter ) | Initializes a new instance of the [SigStat.Common.Loaders.Svc2004Loader](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Loaders/Svc2004Loader.md) class with specified database. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | DatabasePath | Gets or sets the database path. | 
| [Predicate](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[Signer](./../Signer.md)> | SignerFilter | Ignores any signers during the loading, that do not match the predicate | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | StandardFeatures | Gets or sets a value indicating whether features are also loaded as [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Features.md) | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[Signer](./../Signer.md)> | EnumerateSigners ( [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)> signerFilter ) |  | 
| void | LoadSignature ( [`Signature`](./../Signature.md) signature, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) path, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) standardFeatures ) | Loads one signature from specified file path. | 


## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | LoadSignature ( [`Signature`](./../Signature.md) signature, [`Stream`](https://docs.microsoft.com/en-us/dotnet/api/System.IO.Stream) stream, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) standardFeatures ) | Loads one signature from specified stream. | 


