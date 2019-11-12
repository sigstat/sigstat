# [SigComp11DutchLoader](./SigComp11DutchLoader.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Loaders](./README.md)

Assembly: SigStat.Common.dll

Implements [IDataSetLoader](./IDataSetLoader.md), [ILoggerObject](./../ILoggerObject.md)

## Summary
[DataSetLoader](https://github.com/hargitomi97/sigstat/blob/master/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp11Dutch dataset

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| SigComp11DutchLoader ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) ) | Initializes a new instance of the [SigComp11DutchLoader](https://github.com/hargitomi97/sigstat/blob/master/docs/md/SigStat/Common/Loaders/SigComp11DutchLoader.md) class. | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| DatabasePath | Gets or sets the database path. | 
| SamplingFrequency | Sampling Frequency of this database | 
| StandardFeatures | Gets or sets a value indicating whether features are also loaded as [Features](https://github.com/hargitomi97/sigstat/blob/master/docs/md/SigStat/Common/Features.md) | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| EnumerateSigners ( [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)> ) |  | 


## Static Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| LoadSignature ( [`Signature`](./../Signature.md), [`MemoryStream`](https://docs.microsoft.com/en-us/dotnet/api/System.IO.MemoryStream), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) ) | Loads one signature from specified stream. | 


