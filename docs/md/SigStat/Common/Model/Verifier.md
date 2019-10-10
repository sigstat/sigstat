# [Verifier](./Verifier.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Model](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md)

## Summary
Uses pipelines to transform, train on, and classify [Signature](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signature.md) objects.

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>Verifier ( [`ILogger`](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger) )</sub><img width=200/>| <sub>Initializes a new instance of the [Model.Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) class</sub>| <br>
| <sub>Verifier (  )</sub><img width=200/>| <sub>Initializes a new instance of the [Model.Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) class.</sub>| <br>
| <sub>Verifier ( [`Verifier`](./Verifier.md) )</sub><img width=200/>| <sub>Initializes a new instance of the [Model.Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) class based on another Verifier instance</sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>AllFeatures</sub><img width=200/>| <sub>This property is used by the Serializer to access a list of all FeatureDescriptors</sub>| <br>
| <sub>Classifier</sub><img width=200/>| <sub>Gets or sets the classifier pipeline. Hands over the Logger object.</sub>| <br>
| <sub>Logger</sub><img width=200/>| <sub>Gets or sets the class responsible for logging</sub>| <br>
| <sub>Pipeline</sub><img width=200/>| <sub>Gets or sets the transform pipeline. Hands over the Logger object.</sub>| <br>
| <sub>SignerModel</sub><img width=200/>| <sub>Gets or sets the signer model.</sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[Test](./Methods/Verifier-100664119.md) ( [`Signature`](./../Signature.md) )</sub><img width=200/>| <sub>Verifies the genuinity of `signature`.</sub>| <br>
| <sub>[Train](./Methods/Verifier-100664118.md) ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../Signature.md)> )</sub><img width=200/>| <sub>Trains the verifier with a list of signatures. Uses the [Model.Verifier.Pipeline](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) to extract features,  and [Model.Verifier.Classifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) to find an optimized limit.</sub>| <br>


