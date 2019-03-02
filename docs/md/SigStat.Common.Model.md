#### `Verifier`

Uses pipelines to transform, train on, and classify `SigStat.Common.Signature` objects.
```csharp
public class SigStat.Common.Model.Verifier
    : ILoggerObject

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `` | <sub>Classifier</sub> | Gets or sets the classifier pipeline. Hands over the Logger object. | 
| `` | <sub>Logger</sub> | Gets or sets the class responsible for logging | 
| `` | <sub>Pipeline</sub> | Gets or sets the transform pipeline. Hands over the Logger object. | 
| `` | <sub>SignerModel</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Test(Signature)</sub> | Verifies the genuinity of ``. | 
| `void` | <sub>Train(List<Signature>)</sub> | Trains the verifier with a list of signatures. Uses the `SigStat.Common.Model.Verifier.Pipeline` to extract features,  and `SigStat.Common.Model.Verifier.Classifier` to find an optimized limit. | 


