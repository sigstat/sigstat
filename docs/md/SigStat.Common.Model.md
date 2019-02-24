#### `ApproximateLimit`

<sub>Used to approximate the classification limit in the training process.</sub>
```csharp
public class SigStat.Common.Model.ApproximateLimit

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Calculate(List<Signature>)</sub> | Calculate the limit by pairing each signature.  Limit = AverageCost + StdDeviation. | 


#### `BenchmarkResults`

<sub>Contains the benchmark results of every `SigStat.Common.Signer` and the summarized final results.</sub>
```csharp
public struct SigStat.Common.Model.BenchmarkResults

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Result` | FinalResult | Summarized, final result of the benchmark execution. | 
| `List<Result>` | SignerResults | List that contains the `SigStat.Common.Model.Result`s for each `SigStat.Common.Signer` | 


#### `Result`

<sub>Contains the benchmark results of a single `SigStat.Common.Signer`</sub>
```csharp
public class SigStat.Common.Model.Result

```

###### Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | Aer | Average Error Rate | 
| `Double` | Far | False Acceptance Rate | 
| `Double` | Frr | False Rejection Rate | 
| `String` | Signer | Identifier of the `SigStat.Common.Model.Result.Signer` | 


#### `Sampler`

<sub>Takes samples from a set of `SigStat.Common.Signature`s by given sampling strategies.  Use this to fine-tune the `SigStat.Common.Model.VerifierBenchmark`</sub>
```csharp
public class SigStat.Common.Model.Sampler

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Init(Signer)</sub> | Initialize the Sampler with a Signer's Signatures. | 
| `void` | <sub>Init(List<Signature>)</sub> | Initialize the Sampler with a Signer's Signatures. | 
| `List<Signature>` | <sub>SampleForgeryTests()</sub> | Samples a batch of forged signatures to test on. | 
| `List<Signature>` | <sub>SampleGenuineTests()</sub> | Samples a batch of genuine signatures to test on. | 
| `List<Signature>` | <sub>SampleReferences()</sub> | Samples a batch of genuine reference signatures to train on. | 


###### Static Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Sampler` | BasicSampler | Default sampler for SVC2004 database.  10 references, 10 genuine tests, 10 forged tests | 


#### `ThresholdResult`

```csharp
public class SigStat.Common.Model.ThresholdResult
    : Result

```

#### `Verifier`

<sub>Uses pipelines to transform, train on, and classify `SigStat.Common.Signature` objects.</sub>
```csharp
public class SigStat.Common.Model.Verifier
    : ILogger, IProgress

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IClassification` | ClassifierPipeline | Gets or sets the classifier pipeline. Hands over the Logger object. | 
| `Logger` | Logger | Gets or sets the attached `SigStat.Common.Helpers.Logger` object used to log messages. Hands it over to the pipelines. | 
| `Int32` | Progress |  | 
| `ITransformation` | TransformPipeline | Gets or sets the transform pipeline. Hands over the Logger object. | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `EventHandler<Int32>` | ProgressChanged |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Log(LogLevel, String)</sub> | Enqueues a new log entry to be consumed by the attached `SigStat.Common.Helpers.Logger`. Use this when developing new pipeline items. | 
| `void` | <sub>LoggerChanged(Logger, Logger)</sub> |  | 
| `Boolean` | <sub>Test(Signature)</sub> | Verifies the genuinity of ``. | 
| `void` | <sub>Train(Signer)</sub> | Trains the verifier with `SigStat.Common.Signer.Signatures` having `SigStat.Common.Origin.Genuine` property. | 
| `void` | <sub>Train(List<Signature>)</sub> | Trains the verifier with `SigStat.Common.Signer.Signatures` having `SigStat.Common.Origin.Genuine` property. | 


###### Static Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Verifier` | BasicVerifier | Basic `SigStat.Common.Model.Verifier` model with DTW classification of tangent features. | 


#### `VerifierBenchmark`

<sub>Benchmarking class to test error rates of a `SigStat.Common.Model.Verifier`</sub>
```csharp
public class SigStat.Common.Model.VerifierBenchmark
    : ILogger, IProgress

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IDataSetLoader` | Loader |  | 
| `Logger` | Logger | Gets or sets the attached `SigStat.Common.Helpers.Logger` object used to log messages. Hands it over to the verifier. | 
| `Int32` | Progress |  | 
| `Sampler` | Sampler |  | 
| `Verifier` | Verifier | Gets or sets the `SigStat.Common.Model.Verifier` to be benchmarked. | 


###### Events

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `EventHandler<Int32>` | ProgressChanged |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `BenchmarkResults` | <sub>Execute()</sub> | Synchronously execute the benchmarking process. | 
| `BenchmarkResults` | <sub>ExecuteParallel()</sub> | Parallel execute the benchmarking process. | 
| `void` | <sub>Log(LogLevel, String)</sub> |  | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Task<Int32>` | <sub>ExecuteAsync()</sub> | Asynchronously execute the benchmarking process. | 


