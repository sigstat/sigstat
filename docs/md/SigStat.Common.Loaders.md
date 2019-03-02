#### `DataSetLoader`

Abstract loader class to inherit from. Implements ILogger.
```csharp
public abstract class SigStat.Common.Loaders.DataSetLoader
    : IDataSetLoader, ILoggerObject

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `ILogger` | <sub>Logger</sub> |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners()</sub> |  | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> |  | 


#### `IDataSetLoader`

Exposes a function to enable loading collections of `SigStat.Common.Signer`s.  Base abstract class: `SigStat.Common.Loaders.DataSetLoader`.
```csharp
public interface SigStat.Common.Loaders.IDataSetLoader

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners()</sub> | Enumerates all signers of the database | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> | Enumerates all signers of the database | 


#### `ImageLoader`

DataSetLoader for Image type databases.  Similar format to Svc2004Loader, but finds png images.
```csharp
public class SigStat.Common.Loaders.ImageLoader
    : DataSetLoader, IDataSetLoader, ILoggerObject

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> |  | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>LoadImage(Signature, String)</sub> | Load one image. | 
| `Signature` | <sub>LoadSignature(String)</sub> |  | 


#### `ImageSaver`

Get the `SigStat.Common.Features.Image` of a `SigStat.Common.Signature` and save it as png file.
```csharp
public static class SigStat.Common.Loaders.ImageSaver

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Save(Signature, String)</sub> | Saves a png image file to the specified ``. | 


#### `Svc2004`

Set of features containing raw data loaded from SVC2004-format database.
```csharp
public static class SigStat.Common.Loaders.Svc2004

```

###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Int32>>` | <sub>Altitude</sub> | Altitude values from the online signature imported from the SVC2004 database | 
| `FeatureDescriptor<List<Int32>>` | <sub>Azimuth</sub> | Button values from the online signature imported from the SVC2004 database | 
| `FeatureDescriptor<List<Int32>>` | <sub>Button</sub> | Y cooridnates from the online signature imported from the SVC2004 database | 
| `FeatureDescriptor<List<Int32>>` | <sub>Pressure</sub> | Pressure values from the online signature imported from the SVC2004 database | 
| `FeatureDescriptor<List<Int32>>` | <sub>T</sub> | X cooridnates from the online signature imported from the SVC2004 database | 
| `FeatureDescriptor<List<Int32>>` | <sub>X</sub> | X cooridnates from the online signature imported from the SVC2004 database | 
| `FeatureDescriptor<List<Int32>>` | <sub>Y</sub> | X cooridnates from the online signature imported from the SVC2004 database | 


#### `Svc2004Loader`

Loads SVC2004-format database from .zip
```csharp
public class SigStat.Common.Loaders.Svc2004Loader
    : DataSetLoader, IDataSetLoader, ILoggerObject

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Predicate<Signer>` | <sub>SignerFilter</sub> | Ignores any signers during the loading, that do not match the predicate | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> |  | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>LoadSignature(Signature, String, Boolean)</sub> | Loads one signature from specified file path. | 
| `void` | <sub>LoadSignature(Signature, Stream, Boolean)</sub> | Loads one signature from specified file path. | 


