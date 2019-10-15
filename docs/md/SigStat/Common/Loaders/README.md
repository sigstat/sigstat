# [SigStat.Common.Loaders](./README.md)

## Types

- [`DataSetLoader`](./DataSetLoader.md)
	- Abstract loader class to inherit from. Implements ILogger.
- [`IDataSetLoader`](./IDataSetLoader.md)
	- Exposes a function to enable loading collections of [Signer](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signer.md)s.  Base abstract class: [DataSetLoader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/DataSetLoader.md).
- [`ImageLoader`](./ImageLoader.md)
	- DataSetLoader for Image type databases.  Similar format to Svc2004Loader, but finds png images.
- [`ImageSaver`](./ImageSaver.md)
	- Get the [Image](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Features.md) of a [Signature](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Signature.md) and save it as png file.
- [`MCYTLoader`](./MCYTLoader.md)
	- [DataSetLoader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the MCYT dataset
- [`SigComp11ChineseLoader`](./SigComp11ChineseLoader.md)
	- [DataSetLoader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp11Chinese dataset
- [`SigComp11DutchLoader`](./SigComp11DutchLoader.md)
	- [DataSetLoader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp11Dutch dataset
- [`SigComp13JapaneseLoader`](./SigComp13JapaneseLoader.md)
	- [DataSetLoader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp13Japanese dataset
- [`SigComp15GermanLoader`](./SigComp15GermanLoader.md)
	- [DataSetLoader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp15German dataset
- [`SigComp19OnlineLoader`](./SigComp19OnlineLoader.md)
	- [DataSetLoader](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp19 dataset
- [`Svc2004`](./Svc2004.md)
	- Set of features containing raw data loaded from SVC2004-format database.
- [`Svc2004Loader`](./Svc2004Loader.md)
	- Loads SVC2004-format database from .zip

