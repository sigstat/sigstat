#### `DataSetLoader`

Abstract loader class to inherit from. Implements ILogger.
```csharp
public abstract class SigStat.Common.Loaders.DataSetLoader
    : IDataSetLoader, ILoggerObject

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>ILogger</sub>` | <sub>Logger</sub> | <sub></sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>IEnumerable<Signer></sub>` | <sub>EnumerateSigners()</sub> | <sub></sub> | 
| `<sub>IEnumerable<Signer></sub>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> | <sub></sub> | 


#### `IDataSetLoader`

Exposes a function to enable loading collections of `SigStat.Common.Signer`s.  Base abstract class: `SigStat.Common.Loaders.DataSetLoader`.
```csharp
public interface SigStat.Common.Loaders.IDataSetLoader

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>IEnumerable<Signer></sub>` | <sub>EnumerateSigners()</sub> | <sub>Enumerates all signers of the database</sub> | 
| `<sub>IEnumerable<Signer></sub>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> | <sub>Enumerates all signers of the database</sub> | 


#### `ImageLoader`

DataSetLoader for Image type databases.  Similar format to Svc2004Loader, but finds png images.
```csharp
public class SigStat.Common.Loaders.ImageLoader
    : DataSetLoader, IDataSetLoader, ILoggerObject

```

###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>IEnumerable<Signer></sub>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> | <sub></sub> | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>LoadImage(Signature, String)</sub> | <sub>Load one image.</sub> | 
| `<sub>Signature</sub>` | <sub>LoadSignature(String)</sub> | <sub></sub> | 


#### `ImageSaver`

Get the `SigStat.Common.Features.Image` of a `SigStat.Common.Signature` and save it as png file.
```csharp
public static class SigStat.Common.Loaders.ImageSaver

```

###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>Save(Signature, String)</sub> | <sub>Saves a png image file to the specified ``.</sub> | 


#### `Svc2004`

Set of features containing raw data loaded from SVC2004-format database.
```csharp
public static class SigStat.Common.Loaders.Svc2004

```

###### Static Fields

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>FeatureDescriptor<List<Int32>></sub>` | <sub>Altitude</sub> | <sub>Altitude values from the online signature imported from the SVC2004 database</sub> | 
| `<sub>FeatureDescriptor<List<Int32>></sub>` | <sub>Azimuth</sub> | <sub>Button values from the online signature imported from the SVC2004 database</sub> | 
| `<sub>FeatureDescriptor<List<Int32>></sub>` | <sub>Button</sub> | <sub>Y cooridnates from the online signature imported from the SVC2004 database</sub> | 
| `<sub>FeatureDescriptor<List<Int32>></sub>` | <sub>Pressure</sub> | <sub>Pressure values from the online signature imported from the SVC2004 database</sub> | 
| `<sub>FeatureDescriptor<List<Int32>></sub>` | <sub>T</sub> | <sub>X cooridnates from the online signature imported from the SVC2004 database</sub> | 
| `<sub>FeatureDescriptor<List<Int32>></sub>` | <sub>X</sub> | <sub>X cooridnates from the online signature imported from the SVC2004 database</sub> | 
| `<sub>FeatureDescriptor<List<Int32>></sub>` | <sub>Y</sub> | <sub>X cooridnates from the online signature imported from the SVC2004 database</sub> | 


#### `Svc2004Loader`

Loads SVC2004-format database from .zip
```csharp
public class SigStat.Common.Loaders.Svc2004Loader
    : DataSetLoader, IDataSetLoader, ILoggerObject

```

###### Properties

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>Predicate<Signer></sub>` | <sub>SignerFilter</sub> | <sub>Ignores any signers during the loading, that do not match the predicate</sub> | 


###### Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>IEnumerable<Signer></sub>` | <sub>EnumerateSigners(Predicate<Signer>)</sub> | <sub></sub> | 


###### Static Methods

| <sub>Type</sub> | <sub>Name</sub> | <sub>Summary</sub> | 
| --- | --- | --- | 
| `<sub>void</sub>` | <sub>LoadSignature(Signature, String, Boolean)</sub> | <sub>Loads one signature from specified file path.</sub> | 
| `<sub>void</sub>` | <sub>LoadSignature(Signature, Stream, Boolean)</sub> | <sub>Loads one signature from specified file path.</sub> | 


