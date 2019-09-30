# [ImageLoader](./ImageLoader.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Loaders](./README.md)

Assembly: SigStat.Common.dll

Implements [IDataSetLoader](./IDataSetLoader.md), [ILoggerObject](./../ILoggerObject.md)

## Summary
DataSetLoader for Image type databases.  Similar format to Svc2004Loader, but finds png images.

## Constructors

| Name | Summary | 
| --- | --- | 
| ImageLoader ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) databasePath ) | Initializes a new instance of the [SigStat.Common.Loaders.ImageLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/ImageLoader.md) class with specified database. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[Signer](./../Signer.md)> | EnumerateSigners ( [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)> signerFilter ) |  | 


## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Signature](./../Signature.md) | LoadSignature ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) file ) |  | 


