# [SigStat.Common.Loaders](./README.md)

## Types

- [`DataSetLoader`](./DataSetLoader.md)
	- Abstract loader class to inherit from. Implements ILogger.
- [`IDataSetLoader`](./IDataSetLoader.md)
	- Exposes a function to enable loading collections of `SigStat.Common.Signer`s.  Base abstract class: `SigStat.Common.Loaders.DataSetLoader`.
- [`ImageLoader`](./ImageLoader.md)
	- DataSetLoader for Image type databases.  Similar format to Svc2004Loader, but finds png images.
- [`ImageSaver`](./ImageSaver.md)
	- Get the `SigStat.Common.Features.Image` of a `SigStat.Common.Signature` and save it as png file.
- [`MCYTLoader`](./MCYTLoader.md)
	- `SigStat.Common.Loaders.DataSetLoader` for the MCYT dataset
- [`SigComp11ChineseLoader`](./SigComp11ChineseLoader.md)
	- `SigStat.Common.Loaders.DataSetLoader` for the SigComp11Chinese dataset
- [`SigComp11DutchLoader`](./SigComp11DutchLoader.md)
	- `SigStat.Common.Loaders.DataSetLoader` for the SigComp11Dutch dataset
- [`SigComp13JapaneseLoader`](./SigComp13JapaneseLoader.md)
	- `SigStat.Common.Loaders.DataSetLoader` for the SigComp13Japanese dataset
- [`SigComp15GermanLoader`](./SigComp15GermanLoader.md)
	- `SigStat.Common.Loaders.DataSetLoader` for the SigComp15German dataset
- [`SigComp19OnlineLoader`](./SigComp19OnlineLoader.md)
	- `SigStat.Common.Loaders.DataSetLoader` for the SigComp19 dataset
- [`Svc2004`](./Svc2004.md)
	- Set of features containing raw data loaded from SVC2004-format database.
- [`Svc2004Loader`](./Svc2004Loader.md)
	- Loads SVC2004-format database from .zip

