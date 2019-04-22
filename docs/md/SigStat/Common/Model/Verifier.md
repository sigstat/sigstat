# [Verifier](./Verifier.md)

Namespace: [SigStat]() > [Common]() > [Model]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md)

## Summary
Uses pipelines to transform, train on, and classify `SigStat.Common.Signature` objects.

## Constructors

| Name | Summary | 
| --- | --- | 
| Verifier ( [`ILogger`](./Verifier.md) ) | Initializes a new instance of the `SigStat.Common.Model.Verifier` class | 
| Verifier (  ) |  | 
| Verifier ( [`Verifier`](./Verifier.md) ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [IClassifier](./../Pipeline/IClassifier.md) | Classifier | Gets or sets the classifier pipeline. Hands over the Logger object. | 
| [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String), [FeatureDescriptor](./../FeatureDescriptor.md)> | Features |  | 
| [ILogger](./Verifier.md) | Logger | Gets or sets the class responsible for logging | 
| [SequentialTransformPipeline](./../Pipeline/SequentialTransformPipeline.md) | Pipeline | Gets or sets the transform pipeline. Hands over the Logger object. | 
| [ISignerModel](./../Pipeline/ISignerModel.md) | SignerModel |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | Test ( [`Signature`](./../Signature.md) ) | Verifies the genuinity of ``. | 
| void | Train ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../Signature.md)> ) | Trains the verifier with a list of signatures. Uses the `SigStat.Common.Model.Verifier.Pipeline` to extract features,  and `SigStat.Common.Model.Verifier.Classifier` to find an optimized limit. | 


