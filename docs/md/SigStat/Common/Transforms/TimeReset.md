# [TimeReset](./TimeReset.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable), [ITransformation](./../ITransformation.md)

## Summary
Sequential pipeline to reset time values to begin at 0.  The following Transforms are called: Extrema, Multiply, AddVector.  <br>Default Pipeline Input: [Features.T](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: [Features.T](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md)

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>TimeReset (  )</sub><img width=200/>| <sub>Initializes a new instance of the [Transforms.TimeReset](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Transforms/TimeReset.md) class.</sub>| <br>


