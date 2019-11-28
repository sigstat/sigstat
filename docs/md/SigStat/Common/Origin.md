# [Origin](./Origin.md)
Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll


Represents our knowledge on the origin of a signature.

##	Enum

| <span>Value&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Name | <span>Summary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | 
| :--- | :--- | :--- | 
| 0 | Unknown | Use this in practice before a signature is verified. | 
| 1 | Genuine | The [Signature](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signature.md)'s origin is verified to be from [Signer](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signature.md) | 
| 2 | Forged | The [Signature](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signature.md) is a forgery. | 


