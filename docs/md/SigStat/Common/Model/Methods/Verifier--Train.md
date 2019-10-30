# [Train](./Verifier--Train.md)

Trains the verifier with a list of signatures. Uses the [Pipeline](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) to extract features,  and [Classifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) to find an optimized limit.

| <span>Return&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Name | 
| :--- | :--- | 
| [Void](https://docs.microsoft.com/en-us/dotnet/api/System.Void) | [Train](./Verifier--Train.md) ([`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)> signatures) | 


#### Parameters
**`signatures`**  [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../../Signature.md)><br>The list of signatures to train on.
#### Returns
[Void](https://docs.microsoft.com/en-us/dotnet/api/System.Void)<br>
