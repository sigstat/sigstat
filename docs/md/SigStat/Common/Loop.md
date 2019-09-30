# [Loop](./Loop.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

## Summary
Represents a loop in a signature

## Constructors

| Name | Summary | 
| --- | --- | 
| Loop (  ) | Creates a [SigStat.Common.Loop](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Loop.md) instance | 
| Loop ( [`Single`](https://docs.microsoft.com/en-us/dotnet/api/System.Single) centerX, [`Single`](https://docs.microsoft.com/en-us/dotnet/api/System.Single) centerY ) | Creates a [SigStat.Common.Loop](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Loop.md) instance and initializes the [SigStat.Common.Loop.Center]() property | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [RectangleF](https://docs.microsoft.com/en-us/dotnet/api/System.Drawing.RectangleF) | Bounds | The bounding rectangle of the loop | 
| [PointF](https://docs.microsoft.com/en-us/dotnet/api/System.Drawing.PointF) | Center | The geometrical center of the looop | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[PointF](https://docs.microsoft.com/en-us/dotnet/api/System.Drawing.PointF)> | Points | A list of defining points of the loop | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ToString (  ) | Returns a string representation of the loop | 


