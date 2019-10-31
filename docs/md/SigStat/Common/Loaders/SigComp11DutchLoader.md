# [SigComp11DutchLoader](./SigComp11DutchLoader.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Loaders](./README.md)

Assembly: SigStat.Common.dll

Implements [IDataSetLoader](./IDataSetLoader.md), [ILoggerObject](./../ILoggerObject.md)

## Summary
[DataSetLoader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp11Dutch dataset

## Constructors

| <span>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Summary | 
| :--- | :--- | 
| SigComp11DutchLoader ([`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)) | Initializes a new instance of the [SigComp11DutchLoader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/SigComp11DutchLoader.md) class. | 


## Properties

| <span>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Summary | 
| :--- | :--- | 
| DatabasePath | Gets or sets the database path. | 
| StandardFeatures | Gets or sets a value indicating whether features are also loaded as [Features](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md) | 


## Methods

| <span>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Summary | 
| :--- | :--- | 
| [EnumerateSigners](./Methods/SigComp11DutchLoader--EnumerateSigners.md) ([`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../Signer.md)>) |  | 


## Static Methods

| <span>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Summary | 
| :--- | :--- | 
| [LoadSignature](./Methods/SigComp11DutchLoader--LoadSignature.md) ([`Signature`](./../Signature.md), [`MemoryStream`](https://docs.microsoft.com/en-us/dotnet/api/System.IO.MemoryStream), [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)) | Loads one signature from specified stream. | 


