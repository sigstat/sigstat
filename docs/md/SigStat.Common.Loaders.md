#### `DataSetLoader`

<sub>Abstract loader class to inherit from. Implements ILogger.</sub>
```csharp
public abstract class SigStat.Common.Loaders.DataSetLoader
    : IDataSetLoader, ILogger

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Logger` | Logger |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners()</sub> |  | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> |  | 
| `void` | <sub>Log(LogLevel, String)</sub> |  | 


#### `IDataSetLoader`

<sub>Exposes a function to enable loading collections of `SigStat.Common.Signer`s.  Base abstract class: `SigStat.Common.Loaders.DataSetLoader`.</sub>
```csharp
public interface SigStat.Common.Loaders.IDataSetLoader

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners()</sub> | Loads the database and returns the collection of `SigStat.Common.Signer`s that match the ``. | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> | Loads the database and returns the collection of `SigStat.Common.Signer`s that match the ``. | 


#### `ImageLoader`

<sub>DataSetLoader for Image type databases.  Similar format to Svc2004Loader, but finds png images.</sub>
```csharp
public class SigStat.Common.Loaders.ImageLoader
    : DataSetLoader, IDataSetLoader, ILogger

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

<sub>Get the `SigStat.Common.Features.Image` of a `SigStat.Common.Signature` and save it as png file.</sub>
```csharp
public static class SigStat.Common.Loaders.ImageSaver

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>Save(Signature, String)</sub> | Saves a png image file to the specified ``. | 


#### `Svc2004`

<sub>Set of features containing raw data loaded from SVC2004-format database.</sub>
```csharp
public static class SigStat.Common.Loaders.Svc2004

```

###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `FeatureDescriptor<List<Int32>>` | Altitude |  | 
| `FeatureDescriptor<List<Int32>>` | Azimuth |  | 
| `FeatureDescriptor<List<Int32>>` | Button |  | 
| `FeatureDescriptor<List<Int32>>` | Pressure |  | 
| `FeatureDescriptor<List<Int32>>` | T |  | 
| `FeatureDescriptor<List<Int32>>` | X |  | 
| `FeatureDescriptor<List<Int32>>` | Y |  | 


#### `Svc2004Loader`

<sub>Loads SVC2004-format database from .zip</sub>
```csharp
public class SigStat.Common.Loaders.Svc2004Loader
    : DataSetLoader, IDataSetLoader, ILogger

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `Predicate<Signer>` | SignerFilter |  | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `IEnumerable<Signer>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> |  | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| ---- | ---- | ---- | 
| `void` | <sub>LoadSignature(Signature, String, Boolean)</sub> | Loads one signature from specified file path. | 
| `void` | <sub>LoadSignature(Signature, Stream, Boolean)</sub> | Loads one signature from specified file path. | 


