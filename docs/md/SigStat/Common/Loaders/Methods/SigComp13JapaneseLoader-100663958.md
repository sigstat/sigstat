# [EnumerateSigners](./SigComp13JapaneseLoader-100663958.md)



| Return | Name | 
| --- | --- | 
| <sub>[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[Signer](./../../Signer.md)></sub>| <sub>[EnumerateSigners](./SigComp13JapaneseLoader-100663958.md) ( [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../../Signer.md)> signerFilter )</sub>| <br>


#### Parameters
**`signerFilter`**  [`Predicate`](https://docs.microsoft.com/en-us/dotnet/api/System.Predicate-1)\<[`Signer`](./../../Signer.md)><br>Filter to specify which Signers to load. Example: (p=>p=="01")
#### Returns
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[Signer](./../../Signer.md)>