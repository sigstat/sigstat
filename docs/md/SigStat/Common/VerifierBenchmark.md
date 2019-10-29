# [VerifierBenchmark](./VerifierBenchmark.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./ILoggerObject.md)

## Summary
Benchmarking class to test error rates of a [Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md)

## Constructors

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| VerifierBenchmark (  ) | Initializes a new instance of the [VerifierBenchmark](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/VerifierBenchmark.md) class.  Sets the [Sampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Sampler.md) to the default [FirstNSampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Framework/Samplers/FirstNSampler.md). | 


## Properties

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| Loader | The loader that will provide the database for benchmarking | 
| Logger | Gets or sets the attached [ILogger](https://github.com/sigstat/sigstat/blob/develop/docs/md/Microsoft/Extensions/Logging/ILogger.md) object used to log messages. Hands it over to the verifier. | 
| Parameters | A key value store that can be used to store custom information about the benchmark | 
| Progress |  | 
| Sampler | The [Sampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Sampler.md) to be used for benchmarking | 
| Verifier | Gets or sets the [Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) to be benchmarked. | 


## Methods

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| [Dump](./Methods/VerifierBenchmark--Dump.md) ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`IEnumerable`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[`KeyValuePair`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2)\<[`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)>> ) | Dumps the results of the benchmark in a file. | 
| [Execute](./Methods/VerifierBenchmark--Execute.md) ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) ) | Execute the benchmarking process. | 
| [Execute](./Methods/VerifierBenchmark--Execute.md) ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) ) | Execute the benchmarking process with a degree of parallelism. | 


## Events

| Name<div><a href="#"><img width=225></a></div> | Summary<div><a href="#"><img width=525></a></div> | 
| --- | --- | 
| ProgressChanged |  | 


