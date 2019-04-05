# [Origin](./Origin.md)
Namespace: [SigStat]() > [Common]()

Assembly: SigStat.Common.dll


Represents our knowledge on the origin of a signature.

##	Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| 0 | Unknown | Use this in practice before a signature is verified. | 
| 1 | Genuine | The `SigStat.Common.Signature`'s origin is verified to be from `SigStat.Common.Signature.Signer` | 
| 2 | Forged | The `SigStat.Common.Signature` is a forgery. | 


