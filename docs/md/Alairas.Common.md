##`BaseLineExtraction`

```csharp
public class Alairas.Common.BaseLineExtraction
    : PipelineBase,ITransformation,ILogger,IProgress,IPipelineIO

```

Methods

|Type|Name|Summary|
|---|---|---|
|`void`|Run(`Signature`input)||
|`void`|Transform(`Signature`signature)||


Static Methods

|Type|Name|Summary|
|---|---|---|
|`List<List<Point>>`|GetComponentLowerEnvelopes(`Image<Rgba32>`image)|Extracts lower envelope for each component|
|`Baseline`|GetLineOfBestFit(`List<Point>`points)|Megkeresi a megadott pontokra legjobban illeszkedő egyenest. Képes még ennek az egyenesnek különböző  hibamértékeinek kiszámítására, azonban jelenleg ezzel nem foglalkozom, hiszen ebben a speciális esetben  szinte biztosan elég jól illeszkedő egyenest kapunk eredményül.  Az algorimus kimenete nem egy egyenes, hanem egy egy vektor, mely az egyenes egy szakasza. Felteszem,  hogy a pontok X koordináta szerint rendezettek, így az első és utolsó pont X koordinátája közötti  szakaszt adom vissza az egyenesből.  Azaz:  Paraméterként egy előzőleg megtalált komponenst kap, kimenete pedig az adott komponens alapvonala.|


##`BasicMetadataExtraction`

```csharp
public class Alairas.Common.BasicMetadataExtraction
    : PipelineBase,ITransformation,ILogger,IProgress,IPipelineIO

```

Methods

|Type|Name|Summary|
|---|---|---|
|`void`|Run(`Signature`input)||
|`void`|Transform(`Signature`signature)||


Static Properties

|Type|Name|Summary|
|---|---|---|
|`Double`|Trim||


##`SimpleRenderingTransformation`

Renders an image of the signature based on the available online information (X,Y,Dpi)
```csharp
public class Alairas.Common.SimpleRenderingTransformation
    : PipelineBase,ITransformation,ILogger,IProgress,IPipelineIO

```

Methods

|Type|Name|Summary|
|---|---|---|
|`void`|Run(`Signature`input)||
|`void`|Transform(`Signature`signature)||


