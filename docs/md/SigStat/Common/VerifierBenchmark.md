# [VerifierBenchmark](./VerifierBenchmark.md)

Namespace: [SigStat]() > [Common](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./ILoggerObject.md)

## Summary
Benchmarking class to test error rates of a [Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md)

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>VerifierBenchmark (  )</sub>| <sub>Initializes a new instance of the [VerifierBenchmark](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/VerifierBenchmark.md) class.  Sets the [Sampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Sampler.md) to the default [FirstNSampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Framework/Samplers/FirstNSampler.md).</sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>Loader</sub>| <sub>The loader that will provide the database for benchmarking</sub>| <br>
| <sub>Logger</sub>| <sub>Gets or sets the attached [Microsoft.Extensions.Logging.ILogger](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger) object used to log messages. Hands it over to the verifier.</sub>| <br>
| <sub>Parameters</sub>| <sub>A key value store that can be used to store custom information about the benchmark</sub>| <br>
| <sub>Progress</sub>| <sub></sub>| <br>
| <sub>Sampler</sub>| <sub>The [Sampler](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Sampler.md) to be used for benchmarking</sub>| <br>
| <sub>Verifier</sub>| <sub>Gets or sets the [Verifier](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Model/Verifier.md) to be benchmarked.</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Dump](./Methods/VerifierBenchmark-100663372.md) ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`IEnumerable`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[`KeyValuePair`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2)\<[`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String), [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)>> )</sub>| <sub>Dumps the results of the benchmark in a file.</sub>| <br>
| <sub>[Execute](./Methods/VerifierBenchmark-100663384.md) ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) )</sub>| <sub>Execute the benchmarking process.</sub>| <br>
| <sub>[Execute](./Methods/VerifierBenchmark-100663385.md) ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) )</sub>| <sub>Execute the benchmarking process with a degree of parallelism.</sub>| <br>


## Events

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>ProgressChanged</sub>| <sub></sub>| <br>


