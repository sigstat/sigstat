# [EnumerateSigners](./IDataSetLoader--EnumerateSigners.md)

Enumerates all [Signer](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signer.md)s that match the `signerFilter`.

| Return<div><a href="#"><img width=225></a></div> | Name<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[Signer](./../../Signer.md)> | [EnumerateSigners](./IDataSetLoader--EnumerateSigners.md) ([`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../../Signer.md)> signerFilter ) | 


#### Parameters
**`signerFilter`**  [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../../Signer.md)><br>Filter to specify which Signers to load. Example: (p=&gt;p=="01")
#### Returns
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[Signer](./../../Signer.md)><br>
