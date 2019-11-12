# [TangentExtraction](./TangentExtraction.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Extracts tangent values of the standard X, Y [Features](https://github.com/hargitomi97/sigstat/blob/master/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Input: X, Y [Features](https://github.com/hargitomi97/sigstat/blob/master/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: (List{double})  Tangent

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| TangentExtraction (  ) |  | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| OutputTangent | Gets or sets the output feature representing the tangent angles of an online signature | 
| X | Gets or sets the input feature representing the X coordinates of an online signature | 
| Y | Gets or sets the input feature representing the Y coordinates of an online signature | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| Transform ( [`Signature`](./../Signature.md) ) |  | 


