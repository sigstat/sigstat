# [IClassifier](./IClassifier.md)

Namespace: [SigStat]() > [Common]() > [Pipeline]()

Assembly: SigStat.Common.dll

## Summary
Trains classification models based on reference signatures

## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | Test ( [`ISignerModel`](./ISignerModel.md), [`Signature`](./../Signature.md) ) | Returns a double value in the range [0..1], representing the probability of the given signature belonging to the trained model.  <list type="bullet"><item>0: non-match</item><item>0.5: inconclusive</item><item>1: match</item></list> | 
| [ISignerModel](./ISignerModel.md) | Train ( [`List`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[`Signature`](./../Signature.md)> ) | Trains a model based on the signatures and returns the trained model | 


