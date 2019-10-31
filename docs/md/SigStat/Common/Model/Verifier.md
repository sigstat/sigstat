# [Verifier](./Verifier.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Model](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md)

## Summary
Uses pipelines to transform, train on, and classify [Signature](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signature.md) objects.

## Constructors

| <span>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Summary | 
| :--- | :--- | 
| Verifier ([`ILogger`](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger)) | Initializes a new instance of the [Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) class | 
| Verifier () | Initializes a new instance of the [Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) class. | 
| Verifier ([`Verifier`](./Verifier.md)) | Initializes a new instance of the [Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) class based on another Verifier instance | 


## Properties

| <span>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Summary | 
| :--- | :--- | 
| AllFeatures | This property is used by the Serializer to access a list of all FeatureDescriptors | 
| Classifier | Gets or sets the classifier pipeline. Hands over the Logger object. | 
| Logger | Gets or sets the class responsible for logging | 
| Pipeline | Gets or sets the transform pipeline. Hands over the Logger object. | 
| SignerModel | Gets or sets the signer model. | 


## Methods

| <span>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> | Summary | 
| :--- | :--- | 
| [Test](./Methods/Verifier--Test.md) ([`Signature`](./../Signature.md)) | Verifies the genuinity of `signature`. | 
| [Train](./Methods/Verifier--Train.md) ([`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../Signature.md)>) | Trains the verifier with a list of signatures. Uses the [Pipeline](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) to extract features,  and [Classifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) to find an optimized limit. | 


