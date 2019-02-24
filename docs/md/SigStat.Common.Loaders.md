## `DataSetLoader`

Abstract loader class to inherit from. Implements ILogger.
```csharp
public abstract class SigStat.Common.Loaders.DataSetLoader
    : IDataSetLoader, ILogger

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Logger` | Logger |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IEnumerable<Signer>` | EnumerateSigners() |  | 
| `IEnumerable<Signer>` | EnumerateSigners(`Predicate<Signer>`signerFilter) |  | 
| `void` | Log(`LogLevel`level, `String`message) |  | 


## `IDataSetLoader`

Exposes a function to enable loading collections of `SigStat.Common.Signer`s.  Base abstract class: `SigStat.Common.Loaders.DataSetLoader`.
```csharp
public interface SigStat.Common.Loaders.IDataSetLoader

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IEnumerable<Signer>` | EnumerateSigners() | Loads the database and returns the collection of `SigStat.Common.Signer`s that match the ``. | 
| `IEnumerable<Signer>` | EnumerateSigners(`Predicate<Signer>`signerFilter) | Loads the database and returns the collection of `SigStat.Common.Signer`s that match the ``. | 


## `ImageLoader`

DataSetLoader for Image type databases.  Similar format to Svc2004Loader, but finds png images.
```csharp
public class SigStat.Common.Loaders.ImageLoader
    : DataSetLoader, IDataSetLoader, ILogger

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IEnumerable<Signer>` | EnumerateSigners(`Predicate<Signer>`signerFilter) |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | LoadImage(`Signature`signature, `String`file) | Load one image. | 
| `Signature` | LoadSignature(`String`file) |  | 


## `ImageSaver`

Get the `SigStat.Common.Features.Image` of a `SigStat.Common.Signature` and save it as png file.
```csharp
public static class SigStat.Common.Loaders.ImageSaver

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Save(`Signature`signature, `String`path) | Saves a png image file to the specified ``. | 


## `Svc2004`

Set of features containing raw data loaded from SVC2004-format database.
```csharp
public static class SigStat.Common.Loaders.Svc2004

```

Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `FeatureDescriptor<List<Int32>>` | Altitude |  | 
| `FeatureDescriptor<List<Int32>>` | Azimuth |  | 
| `FeatureDescriptor<List<Int32>>` | Button |  | 
| `FeatureDescriptor<List<Int32>>` | Pressure |  | 
| `FeatureDescriptor<List<Int32>>` | T |  | 
| `FeatureDescriptor<List<Int32>>` | X |  | 
| `FeatureDescriptor<List<Int32>>` | Y |  | 


## `Svc2004Loader`

Loads SVC2004-format database from .zip
```csharp
public class SigStat.Common.Loaders.Svc2004Loader
    : DataSetLoader, IDataSetLoader, ILogger

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Predicate<Signer>` | SignerFilter |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IEnumerable<Signer>` | EnumerateSigners(`Predicate<Signer>`signerFilter) |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | LoadSignature(`Signature`signature, `String`path, `Boolean`standardFeatures) | Loads one signature from specified file path. | 
| `void` | LoadSignature(`Signature`signature, `Stream`stream, `Boolean`standardFeatures) | Loads one signature from specified file path. | 


