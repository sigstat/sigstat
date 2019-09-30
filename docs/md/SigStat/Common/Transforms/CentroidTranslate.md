# [CentroidTranslate](./CentroidTranslate.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [ITransformation](./../ITransformation.md)

## Summary
Sequential pipeline to translate X and Y [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Features.md) to Centroid.  The following Transforms are called: [SigStat.Common.Transforms.CentroidExtraction](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/CentroidExtraction.md), [SigStat.Common.Transforms.Multiply](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/Multiply.md)(-1), [SigStat.Common.Transforms.Translate](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/Translate.md)<br>Default Pipeline Input: [SigStat.Common.Features.X](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md), [SigStat.Common.Features.Y](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md)<br>Default Pipeline Output: (List{double}) Centroid

## Constructors

| Name | Summary | 
| --- | --- | 
| CentroidTranslate (  ) | Initializes a new instance of the [SigStat.Common.Transforms.CentroidTranslate](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/CentroidTranslate.md) class. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputX | Gets or sets the input feature representing the X coordinates of an online signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | InputY | Gets or sets the input feature representing the Y coordinates of an online signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | OutputX | Gets or sets the output feature representing the X coordinates of an online signature | 
| [FeatureDescriptor](./../FeatureDescriptor-1.md)\<[List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)>> | OutputY | Gets or sets the output feature representing the X coordinates of an online signature | 


