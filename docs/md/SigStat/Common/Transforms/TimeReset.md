# [TimeReset](./TimeReset.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [ITransformation](./../ITransformation.md)

## Summary
Sequential pipeline to reset time values to begin at 0.  The following Transforms are called: Extrema, Multiply, AddVector.  <br>Default Pipeline Input: [SigStat.Common.Features.T](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md)<br>Default Pipeline Output: [SigStat.Common.Features.T](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/FeatureDescriptor-1.md)

## Constructors

| Name | Summary | 
| --- | --- | 
| TimeReset (  ) | Initializes a new instance of the [SigStat.Common.Transforms.TimeReset](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Transforms/TimeReset.md) class. | 


