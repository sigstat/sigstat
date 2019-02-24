## `ApproximateLimit`

Used to approximate the classification limit in the training process.
```csharp
public class SigStat.Common.Model.ApproximateLimit

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | Calculate(`List<Signature>`sigs) | Calculate the limit by pairing each signature.  Limit = AverageCost + StdDeviation. | 


## `BenchmarkResults`

Contains the benchmark results of every `SigStat.Common.Signer` and the summarized final results.
```csharp
public struct SigStat.Common.Model.BenchmarkResults

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Result` | FinalResult | Summarized, final result of the benchmark execution. | 
| `List<Result>` | SignerResults | List that contains the `SigStat.Common.Model.Result`s for each `SigStat.Common.Signer` | 


## `Result`

Contains the benchmark results of a single `SigStat.Common.Signer`
```csharp
public class SigStat.Common.Model.Result

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | Aer | Average Error Rate | 
| `Double` | Far | False Acceptance Rate | 
| `Double` | Frr | False Rejection Rate | 
| `String` | Signer | Identifier of the `SigStat.Common.Model.Result.Signer` | 


## `Sampler`

Takes samples from a set of `SigStat.Common.Signature`s by given sampling strategies.  Use this to fine-tune the `SigStat.Common.Model.VerifierBenchmark`
```csharp
public class SigStat.Common.Model.Sampler

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Init(`Signer`s) | Initialize the Sampler with a Signer's Signatures. | 
| `void` | Init(`List<Signature>`s) | Initialize the Sampler with a Signer's Signatures. | 
| `List<Signature>` | SampleForgeryTests() | Samples a batch of forged signatures to test on. | 
| `List<Signature>` | SampleGenuineTests() | Samples a batch of genuine signatures to test on. | 
| `List<Signature>` | SampleReferences() | Samples a batch of genuine reference signatures to train on. | 


Static Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Sampler` | BasicSampler | Default sampler for SVC2004 database.  10 references, 10 genuine tests, 10 forged tests | 


## `ThresholdResult`

```csharp
public class SigStat.Common.Model.ThresholdResult
    : Result

```

## `Verifier`

Uses pipelines to transform, train on, and classify `SigStat.Common.Signature` objects.
```csharp
public class SigStat.Common.Model.Verifier
    : ILogger, IProgress

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IClassification` | ClassifierPipeline | Gets or sets the classifier pipeline. Hands over the Logger object. | 
| `Logger` | Logger | Gets or sets the attached `SigStat.Common.Helpers.Logger` object used to log messages. Hands it over to the pipelines. | 
| `Int32` | Progress |  | 
| `ITransformation` | TransformPipeline | Gets or sets the transform pipeline. Hands over the Logger object. | 


Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `EventHandler<Int32>` | ProgressChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Log(`LogLevel`level, `String`message) | Enqueues a new log entry to be consumed by the attached `SigStat.Common.Helpers.Logger`. Use this when developing new pipeline items. | 
| `void` | LoggerChanged(`Logger`oldLogger, `Logger`newLogger) |  | 
| `Boolean` | Test(`Signature`sig) | Verifies the genuinity of ``. | 
| `void` | Train(`Signer`signer) | Trains the verifier with `SigStat.Common.Signer.Signatures` having `SigStat.Common.Origin.Genuine` property. | 
| `void` | Train(`List<Signature>`sigs) | Trains the verifier with `SigStat.Common.Signer.Signatures` having `SigStat.Common.Origin.Genuine` property. | 


Static Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Verifier` | BasicVerifier | Basic `SigStat.Common.Model.Verifier` model with DTW classification of tangent features. | 


## `VerifierBenchmark`

Benchmarking class to test error rates of a `SigStat.Common.Model.Verifier`
```csharp
public class SigStat.Common.Model.VerifierBenchmark
    : ILogger, IProgress

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IDataSetLoader` | Loader |  | 
| `Logger` | Logger | Gets or sets the attached `SigStat.Common.Helpers.Logger` object used to log messages. Hands it over to the verifier. | 
| `Int32` | Progress |  | 
| `Sampler` | Sampler |  | 
| `Verifier` | Verifier | Gets or sets the `SigStat.Common.Model.Verifier` to be benchmarked. | 


Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `EventHandler<Int32>` | ProgressChanged |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `BenchmarkResults` | Execute() | Synchronously execute the benchmarking process. | 
| `BenchmarkResults` | ExecuteParallel() | Parallel execute the benchmarking process. | 
| `void` | Log(`LogLevel`level, `String`message) |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Task<Int32>` | ExecuteAsync() | Asynchronously execute the benchmarking process. | 


