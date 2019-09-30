# [SigComp11ChineseLoader](./SigComp11ChineseLoader.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Loaders](./README.md)

Assembly: SigStat.Common.dll

Implements [IDataSetLoader](./IDataSetLoader.md), [ILoggerObject](./../ILoggerObject.md)

## Summary
[SigStat.Common.Loaders.DataSetLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp11Chinese dataset

## Constructors

| Name | Summary | 
| --- | --- | 
| SigComp11ChineseLoader ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) databasePath, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) standardFeatures ) | Initializes a new instance of the [SigStat.Common.Loaders.SigComp11ChineseLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/SigComp11ChineseLoader.md) class. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | DatabasePath | Gets or sets the database path. | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | StandardFeatures | Gets or sets a value indicating whether features are also loaded as [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Features.md) | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[Signer](./../Signer.md)> | EnumerateSigners ( [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)> signerFilter ) |  | 


## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | LoadSignature ( [`Signature`](./../Signature.md) signature, [`MemoryStream`](https://docs.microsoft.com/en-us/dotnet/api/System.IO.MemoryStream) stream, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) standardFeatures ) | Loads one signature from specified stream. | 


