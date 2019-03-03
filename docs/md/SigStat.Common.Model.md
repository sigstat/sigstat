#### `Verifier`

Uses pipelines to transform, train on, and classify `SigStat.Common.Signature` objects.
```csharp
public class SigStat.Common.Model.Verifier
    : ILoggerObject

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>IClassifier</sub>` | <sub>Classifier</sub> | <sub>Gets or sets the classifier pipeline. Hands over the Logger object.</sub> | 
| `<sub>ILogger</sub>` | <sub>Logger</sub> | <sub>Gets or sets the class responsible for logging</sub> | 
| `<sub>SequentialTransformPipeline</sub>` | <sub>Pipeline</sub> | <sub>Gets or sets the transform pipeline. Hands over the Logger object.</sub> | 
| `<sub>ISignerModel</sub>` | <sub>SignerModel</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Double</sub>` | <sub>Test(Signature)</sub> | <sub>Verifies the genuinity of ``.</sub> | 
| `<sub>void</sub>` | <sub>Train(List<Signature>)</sub> | <sub>Trains the verifier with a list of signatures. Uses the `SigStat.Common.Model.Verifier.Pipeline` to extract features,  and `SigStat.Common.Model.Verifier.Classifier` to find an optimized limit.</sub> | 


