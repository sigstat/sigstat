# [Sampler](./Sampler.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

## Summary
Takes samples from a set of [Signature](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signature.md)s by given sampling strategies.  Use this to fine-tune the [VerifierBenchmark](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/VerifierBenchmark.md)

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>Sampler ( [`Func`](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2)\<[`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./Signature.md)>, [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./Signature.md)>>, [`Func`](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2)\<[`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./Signature.md)>, [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./Signature.md)>>, [`Func`](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2)\<[`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./Signature.md)>, [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./Signature.md)>> )</sub><img width=200/>| <sub>Initialize a new instance of the [Sampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Sampler.md) class by given sampling strategies.</sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>ForgeryTestFilter</sub><img width=200/>| <sub></sub>| <br>
| <sub>GenuineTestFilter</sub><img width=200/>| <sub></sub>| <br>
| <sub>TrainingFilter</sub><img width=200/>| <sub></sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[SampleForgeryTests](./Methods/Sampler-100663364.md) ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./Signature.md)> )</sub><img width=200/>| <sub></sub>| <br>
| <sub>[SampleGenuineTests](./Methods/Sampler-100663363.md) ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./Signature.md)> )</sub><img width=200/>| <sub></sub>| <br>
| <sub>[SampleReferences](./Methods/Sampler-100663362.md) ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./Signature.md)> )</sub><img width=200/>| <sub></sub>| <br>


