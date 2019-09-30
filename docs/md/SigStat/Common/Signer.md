# [Signer](./Signer.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

## Summary
Represents a person as a [SigStat.Common.Signer.ID](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/String.md) and a list of [SigStat.Common.Signer.Signatures]().

## Constructors

| Name | Summary | 
| --- | --- | 
| Signer (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ID | An identifier for the Signer. Keep it unique to be useful for logs. | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Signature](./Signature.md)> | Signatures | List of signatures that belong to the signer.  (Their origin is not constrained to be genuine.) | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ToString (  ) | Returns a string representation of a Signer | 


