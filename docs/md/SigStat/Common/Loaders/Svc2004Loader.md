# [Svc2004Loader](./Svc2004Loader.md)

Namespace: [SigStat]() > [Common]() > [Loaders]()

Assembly: SigStat.Common.dll

Implements [IDataSetLoader](./IDataSetLoader.md), [ILoggerObject](./../ILoggerObject.md)

## Summary
Loads SVC2004-format database from .zip

## Constructors

| Name | Summary | 
| --- | --- | 
| Svc2004Loader ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean), [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)> ) | Initializes a new instance of the `SigStat.Common.Loaders.Svc2004Loader` class with specified database. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Predicate](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[Signer](./../Signer.md)> | SignerFilter | Ignores any signers during the loading, that do not match the predicate | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[Signer](./../Signer.md)> | EnumerateSigners ( [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)> ) |  | 


## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | LoadSignature ( [`Signature`](./../Signature.md), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) ) | Loads one signature from specified file path. | 
| void | LoadSignature ( [`Signature`](./../Signature.md), [`Stream`](https://docs.microsoft.com/en-us/dotnet/api/System.IO.Stream), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) ) | Loads one signature from specified stream. | 


