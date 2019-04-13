# [VerifierBenchmark](./VerifierBenchmark.md)

Namespace: [SigStat]() > [Common]()

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./ILoggerObject.md)

## Summary
Benchmarking class to test error rates of a `SigStat.Common.Model.Verifier`

## Constructors

| Name | Summary | 
| --- | --- | 
| VerifierBenchmark (  ) | Initializes a new instance of the `SigStat.Common.VerifierBenchmark` class.  Sets the `SigStat.Common.Sampler` to the default `SigStat.Common.SVC2004Sampler1`. | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [IDataSetLoader](./Loaders/IDataSetLoader.md) | Loader | The loader that will provide the database for benchmarking | 
| [ILogger](./VerifierBenchmark.md) | Logger | Gets or sets the attached `Microsoft.Extensions.Logging.ILogger` object used to log messages. Hands it over to the verifier. | 
| [Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) | Progress |  | 
| [Sampler](./Sampler.md) | Sampler | The `SigStat.Common.Sampler` to be used for benchmarking | 
| [Verifier](./Model/Verifier.md) | Verifier | Gets or sets the `SigStat.Common.Model.Verifier` to be benchmarked. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Dump ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`IEnumerable`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[`KeyValuePair`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2)\<[`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)>> ) |  | 
| [BenchmarkResults](./BenchmarkResults.md) | Execute ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) ) |  | 


## Events

| Type | Name | Summary | 
| --- | --- | --- | 
| [EventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.EventHandler-1)\<[Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)> | ProgressChanged |  | 


