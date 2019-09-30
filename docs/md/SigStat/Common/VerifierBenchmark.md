# [VerifierBenchmark](./VerifierBenchmark.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./ILoggerObject.md)

## Summary
Benchmarking class to test error rates of a [SigStat.Common.Model.Verifier](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Model/Verifier.md)

## Constructors

| Name | Summary | 
| --- | --- | 
| VerifierBenchmark (  ) | Initializes a new instance of the [SigStat.Common.VerifierBenchmark](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/VerifierBenchmark.md) class.  Sets the [SigStat.Common.Sampler](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Sampler.md) to the default [SigStat.Common.Framework.Samplers.FirstNSampler](). | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [IDataSetLoader](./Loaders/IDataSetLoader.md) | Loader | The loader that will provide the database for benchmarking | 
| [ILogger](./VerifierBenchmark.md) | Logger | Gets or sets the attached [Microsoft.Extensions.Logging.ILogger]() object used to log messages. Hands it over to the verifier. | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[KeyValuePair](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String), [String](https://docs.microsoft.com/en-us/dotnet/api/System.String)>> | Parameters | A key value store that can be used to store custom information about the benchmark | 
| [Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) | Progress |  | 
| [Sampler](./Sampler.md) | Sampler | The [SigStat.Common.Sampler](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Sampler.md) to be used for benchmarking | 
| [Verifier](./Model/Verifier.md) | Verifier | Gets or sets the [SigStat.Common.Model.Verifier](https://github.com/sigstat/sigstat/tree/develop/docs/md/SigStat/Common/Model/Verifier.md) to be benchmarked. | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Dump ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) filename, [`IEnumerable`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[`KeyValuePair`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2)\<[`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)>> parameters ) | Dumps the results of the benchmark in a file. | 
| [BenchmarkResults](./BenchmarkResults.md) | Execute ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) ParallelMode ) | Execute the benchmarking process. | 
| [BenchmarkResults](./BenchmarkResults.md) | Execute ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) degreeOfParallelism ) | Execute the benchmarking process with a degree of parallelism. | 


## Events

| Type | Name | Summary | 
| --- | --- | --- | 
| [EventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.EventHandler-1)\<[Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)> | ProgressChanged |  | 


