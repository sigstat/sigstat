# [BenchmarkConfig](./BenchmarkConfig.md)

Namespace: [SigStat]() > [Common]() > [Helpers]()

Assembly: SigStat.Common.dll


## Constructors

| Name | Summary | 
| --- | --- | 
| BenchmarkConfig (  ) |  | 
| BenchmarkConfig ( [`BenchmarkConfig`](./BenchmarkConfig.md) ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Classifier |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Database |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Distance |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Features |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Interpolation |  | 
| [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double) | ResamplingParam |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ResamplingType_Filter |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | Rotation |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Sampling |  | 
| [ValueTuple](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple-2)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String), [String](https://docs.microsoft.com/en-us/dotnet/api/System.String)> | Translation_Scaling |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [BenchmarkConfig](./BenchmarkConfig.md) | FromJsonFile ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ToJsonString (  ) |  | 
| [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[KeyValuePair](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String), [String](https://docs.microsoft.com/en-us/dotnet/api/System.String)>> | ToKeyValuePairs (  ) |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ToShortString (  ) |  | 


## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [BenchmarkConfig](./BenchmarkConfig.md) | FromJsonString ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) ) |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[BenchmarkConfig](./BenchmarkConfig.md)> | GenerateConfigurations (  ) |  | 


