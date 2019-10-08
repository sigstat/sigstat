# [VerifierBenchmark](./VerifierBenchmark.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./ILoggerObject.md)

## Summary
Benchmarking class to test error rates of a [Model.Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md)

## Constructors

| Name | Summary | 
| --- | --- | 
| <sub>VerifierBenchmark (  )</sub><img width=200/>| <sub>Initializes a new instance of the [VerifierBenchmark](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/VerifierBenchmark.md) class.  Sets the [Sampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Sampler.md) to the default [Framework.Samplers.FirstNSampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Framework/Samplers/FirstNSampler.md).</sub>| <br>


## Properties

| Name | Summary | 
| --- | --- | 
| <sub>Loader</sub><img width=200/>| <sub>The loader that will provide the database for benchmarking</sub>| <br>
| <sub>Logger</sub><img width=200/>| <sub>Gets or sets the attached [Microsoft.Extensions.Logging.ILogger](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger) object used to log messages. Hands it over to the verifier.</sub>| <br>
| <sub>Parameters</sub><img width=200/>| <sub>A key value store that can be used to store custom information about the benchmark</sub>| <br>
| <sub>Progress</sub><img width=200/>| <sub></sub>| <br>
| <sub>Sampler</sub><img width=200/>| <sub>The [Sampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Sampler.md) to be used for benchmarking</sub>| <br>
| <sub>Verifier</sub><img width=200/>| <sub>Gets or sets the [Model.Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) to be benchmarked.</sub>| <br>


## Methods

| Name | Summary | 
| --- | --- | 
| <sub>[Dump](./Methods/VerifierBenchmark-100663372.md) ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`IEnumerable`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[`KeyValuePair`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2)\<[`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)>> )</sub><img width=200/>| <sub>Dumps the results of the benchmark in a file.</sub>| <br>
| <sub>[Execute](./Methods/VerifierBenchmark-100663384.md) ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) )</sub><img width=200/>| <sub>Execute the benchmarking process.</sub>| <br>
| <sub>[Execute](./Methods/VerifierBenchmark-100663385.md) ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) )</sub><img width=200/>| <sub>Execute the benchmarking process with a degree of parallelism.</sub>| <br>


## Events

| Name | Summary | 
| --- | --- | 
| <sub>ProgressChanged</sub><img width=200/>| <sub></sub>| <br>


