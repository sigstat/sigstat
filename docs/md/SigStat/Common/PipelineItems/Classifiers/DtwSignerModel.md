# [DtwSignerModel](./DtwSignerModel.md)

Namespace: [SigStat]() > [Common](./../../README.md) > [PipelineItems]() > [Classifiers](./README.md)

Assembly: SigStat.Common.dll

Implements [ISignerModel](./../../Pipeline/ISignerModel.md)

## Summary
Represents a trained model for [DtwClassifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md)

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>DtwSignerModel (  )</sub>| <sub></sub>| <br>


## Fields

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>DistanceMatrix</sub>| <sub>DTW distance matrix of the genuine signatures</sub>| <br>
| <sub>Threshold</sub>| <sub>A threshold, that will be used for classification. Signatures with  an average DTW distance from the genuines above this threshold will  be classified as forgeries</sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>GenuineSignatures</sub>| <sub>A list a of genuine signatures used for training</sub>| <br>


