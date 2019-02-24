#### `BaseLineExtraction`

```csharp
public class Alairas.Common.BaseLineExtraction
    : PipelineBase, ITransformation, ILogger, IProgress, IPipelineIO

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Run(Signature)</sub> |  | 
| `void` | <sub>Transform(Signature)</sub> |  | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `List<List<Point>>` | <sub>GetComponentLowerEnvelopes(Image<Rgba32>)</sub> | Extracts lower envelope for each component | 
| `Baseline` | <sub>GetLineOfBestFit(List<Point>)</sub> | Megkeresi a megadott pontokra legjobban illeszkedő egyenest. Képes még ennek az egyenesnek különböző  hibamértékeinek kiszámítására, azonban jelenleg ezzel nem foglalkozom, hiszen ebben a speciális esetben  szinte biztosan elég jól illeszkedő egyenest kapunk eredményül.  Az algorimus kimenete nem egy egyenes, hanem egy egy vektor, mely az egyenes egy szakasza. Felteszem,  hogy a pontok X koordináta szerint rendezettek, így az első és utolsó pont X koordinátája közötti  szakaszt adom vissza az egyenesből.  Azaz:  Paraméterként egy előzőleg megtalált komponenst kap, kimenete pedig az adott komponens alapvonala. | 


#### `BasicMetadataExtraction`

```csharp
public class Alairas.Common.BasicMetadataExtraction
    : PipelineBase, ITransformation, ILogger, IProgress, IPipelineIO

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Run(Signature)</sub> |  | 
| `void` | <sub>Transform(Signature)</sub> |  | 


###### Static Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Double` | <sub>Trim</sub> |  | 


#### `SimpleRenderingTransformation`

Renders an image of the signature based on the available online information (X,Y,Dpi)
```csharp
public class Alairas.Common.SimpleRenderingTransformation
    : PipelineBase, ITransformation, ILogger, IProgress, IPipelineIO

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Run(Signature)</sub> |  | 
| `void` | <sub>Transform(Signature)</sub> |  | 


