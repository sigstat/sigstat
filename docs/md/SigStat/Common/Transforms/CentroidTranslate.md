# [CentroidTranslate](./CentroidTranslate.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [ITransformation](./../ITransformation.md)

## Summary
Sequential pipeline to translate X and Y [Features](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md) to Centroid.  The following Transforms are called: [CentroidExtraction](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/CentroidExtraction.md), [Multiply](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/Multiply.md)(-1), [Translate](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/Translate.md)<br>Default Pipeline Input: [X](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md), [Y](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: (List{double}) Centroid

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| CentroidTranslate (  ) | Initializes a new instance of the [CentroidTranslate](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/CentroidTranslate.md) class. | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| InputX | Gets or sets the input feature representing the X coordinates of an online signature | 
| InputY | Gets or sets the input feature representing the Y coordinates of an online signature | 
| OutputX | Gets or sets the output feature representing the X coordinates of an online signature | 
| OutputY | Gets or sets the output feature representing the X coordinates of an online signature | 


