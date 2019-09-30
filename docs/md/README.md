# [API](./README.md)

## Summary
This is the root summary

## Namespaces

### [SigStat.Common](./SigStat/Common/README.md)

- [`ArrayExtension`](./SigStat/Common/ArrayExtension.md)
	- Helper methods for processing arrays
- [`Baseline`](./SigStat/Common/Baseline.md)
- [`BasicMetadataExtraction`](./SigStat/Common/BasicMetadataExtraction.md)
	- Extracts basic statistical signature (like [SigStat.Common.Features.Bounds]() or [SigStat.Common.Features.Cog]()) information from an Image
- [`BenchmarkResults`](./SigStat/Common/BenchmarkResults.md)
	- Contains the benchmark results of every [SigStat.Common.Signer](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Signer.md) and the summarized final results.
- [`DistanceMatrix`](./SigStat/Common/DistanceMatrix-3.md)\<[`TRowKey`](./README.md), [`TColumnKey`](./README.md), [`TValue`](./README.md)>
	- A Sparse Matrix representation of a distance graph.
- [`ErrorRate`](./SigStat/Common/ErrorRate.md)
	- Represents the ErrorRates achieved in a benchmark
- [`FeatureDescriptor`](./SigStat/Common/FeatureDescriptor.md)
	- Represents a feature with name and type.
- [`FeatureDescriptor`](./SigStat/Common/FeatureDescriptor-1.md)\<[`T`](./README.md)>
	- Represents a feature with the type `type`
- [`Features`](./SigStat/Common/Features.md)
	- Standard set of features.
- [`ILoggerObject`](./SigStat/Common/ILoggerObject.md)
	- Represents a type, that contains an ILogger property that can be used to perform logging.
- [`ILoggerObjectExtensions`](./SigStat/Common/ILoggerObjectExtensions.md)
	- ILoggerObject extension methods for common scenarios.
- [`IOExtensions`](./SigStat/Common/IOExtensions.md)
	- Extension methods for common IO operations
- [`ITransformation`](./SigStat/Common/ITransformation.md)
	- Allows implementing a pipeline transform item capable of logging, progress tracking and IO rewiring.
- [`Loop`](./SigStat/Common/Loop.md)
	- Represents a loop in a signature
- [`MathHelper`](./SigStat/Common/MathHelper.md)
	- Common mathematical functions used by the SigStat framework
- [`PipelineBase`](./SigStat/Common/PipelineBase.md)
	- TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.  ILoggerObject, IProgress, IPipelineIO default implementacioja.
- [`Result`](./SigStat/Common/Result.md)
	- Contains the benchmark results of a single [SigStat.Common.Signer](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Signer.md)
- [`Sampler`](./SigStat/Common/Sampler.md)
	- Takes samples from a set of [SigStat.Common.Signature](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Signature.md)s by given sampling strategies.  Use this to fine-tune the [SigStat.Common.VerifierBenchmark](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/VerifierBenchmark.md)
- [`Signature`](./SigStat/Common/Signature.md)
	- Represents a signature as a collection of features, containing the data that flows in the pipeline.
- [`Signer`](./SigStat/Common/Signer.md)
	- Represents a person as a [SigStat.Common.Signer.ID](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/String.md) and a list of [SigStat.Common.Signer.Signatures]().
- [`SigStatEvents`](./SigStat/Common/SigStatEvents.md)
	- Standard event identifiers used by the SigStat system
- [`SimpleRenderingTransformation`](./SigStat/Common/SimpleRenderingTransformation.md)
	- Renders an image of the signature based on the available online information (X,Y,Dpi)
- [`StrokeHelper`](./SigStat/Common/StrokeHelper.md)
	- Helper class for locating and manipulating strokes in an online signature
- [`StrokeInterval`](./SigStat/Common/StrokeInterval.md)
	- Represents a stroke in an online signature
- [`VerifierBenchmark`](./SigStat/Common/VerifierBenchmark.md)
	- Benchmarking class to test error rates of a [SigStat.Common.Model.Verifier](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Model/Verifier.md)
### [SigStat.Common.Pipeline](./SigStat/Common/Pipeline/README.md)

- [`IClassifier`](./SigStat/Common/Pipeline/IClassifier.md)
	- Trains classification models based on reference signatures
- [`IDistanceClassifier`](./SigStat/Common/Pipeline/IDistanceClassifier.md)
	- Trains classification models based on reference signatures, by calculating the distances between signature pairs
- [`Input`](./SigStat/Common/Pipeline/Input.md)
	- Annotates an input [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor.md) in a transformation pipeline
- [`IPipelineIO`](./SigStat/Common/Pipeline/IPipelineIO.md)
	- Supports the definition of [SigStat.Common.Pipeline.PipelineInput](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Pipeline/PipelineInput.md) and [SigStat.Common.Pipeline.PipelineOutput](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Pipeline/PipelineOutput.md)
- [`ISignerModel`](./SigStat/Common/Pipeline/ISignerModel.md)
	- Analyzes signatures based on their similiarity to the trained model
- [`Output`](./SigStat/Common/Pipeline/Output.md)
	- Annotates an output [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor.md) in a transformation pipeline
- [`ParallelTransformPipeline`](./SigStat/Common/Pipeline/ParallelTransformPipeline.md)
	- Runs pipeline items in parallel.  <br>Default Pipeline Output: Range of all the Item outputs.
- [`PipelineInput`](./SigStat/Common/Pipeline/PipelineInput.md)
	- Represents an input for a [SigStat.Common.Pipeline.PipelineInput.PipelineItem]()
- [`PipelineOutput`](./SigStat/Common/Pipeline/PipelineOutput.md)
	- Represents an output for a [SigStat.Common.Pipeline.PipelineOutput.PipelineItem]()
- [`SequentialTransformPipeline`](./SigStat/Common/Pipeline/SequentialTransformPipeline.md)
	- Runs pipeline items in a sequence.  <br>Default Pipeline Output: Output of the last Item in the sequence.
### [SigStat.Common.Transforms](./SigStat/Common/Transforms/README.md)

- [`AddConst`](./SigStat/Common/Transforms/AddConst.md)
	- Adds a constant value to a feature. Works with collection features too.  <br>Default Pipeline Output: Pipeline Input
- [`AddVector`](./SigStat/Common/Transforms/AddVector.md)
	- Adds a vector feature's elements to other features.  <br>Default Pipeline Output: Pipeline Input
- [`ApproximateOnlineFeatures`](./SigStat/Common/Transforms/ApproximateOnlineFeatures.md)
	- init Pressure, Altitude, Azimuth features with default values.  <br>Default Pipeline Output: Features.Pressure, Features.Altitude, Features.Azimuth
- [`Binarization`](./SigStat/Common/Transforms/Binarization.md)
	- Generates a binary raster version of the input image with the iterative threshold method.  <br>Pipeline Input type: Image{Rgba32}<br>Default Pipeline Output: (bool[,]) Binarized
- [`BinaryRasterizer`](./SigStat/Common/Transforms/BinaryRasterizer.md)
	- Converts standard features to a binary raster.  <br>Default Pipeline Input: Standard [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: (bool[,]) Binarized
- [`CentroidExtraction`](./SigStat/Common/Transforms/CentroidExtraction.md)
	- Extracts the Centroid (aka. Center Of Gravity) of the input features.  <br> Default Pipeline Output: (List{double}) Centroid.
- [`CentroidTranslate`](./SigStat/Common/Transforms/CentroidTranslate.md)
	- Sequential pipeline to translate X and Y [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Features.md) to Centroid.  The following Transforms are called: [SigStat.Common.Transforms.CentroidExtraction](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Transforms/CentroidExtraction.md), [SigStat.Common.Transforms.Multiply](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Transforms/Multiply.md)(-1), [SigStat.Common.Transforms.Translate](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Transforms/Translate.md)<br>Default Pipeline Input: [SigStat.Common.Features.X](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md), [SigStat.Common.Features.Y](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md)<br>Default Pipeline Output: (List{double}) Centroid
- [`ComponentExtraction`](./SigStat/Common/Transforms/ComponentExtraction.md)
	- Extracts unsorted components by tracing through the binary Skeleton raster.  <br>Default Pipeline Input: (bool[,]) Skeleton, (List{Point}) EndPoints, (List{Point}) CrossingPoints<br>Default Pipeline Output: (List{List{PointF}}) Components
- [`ComponentSorter`](./SigStat/Common/Transforms/ComponentSorter.md)
	- Sorts Component order by comparing each starting X value, and finding nearest components.  <br>Default Pipeline Input: (bool[,]) Components<br>Default Pipeline Output: (bool[,]) Components
- [`ComponentsToFeatures`](./SigStat/Common/Transforms/ComponentsToFeatures.md)
	- Extracts standard [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Features.md) from sorted Components.  <br>Default Pipeline Input: (List{List{PointF}}) Components<br>Default Pipeline Output: X, Y, Button [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Features.md)
- [`EndpointExtraction`](./SigStat/Common/Transforms/EndpointExtraction.md)
	- Extracts EndPoints and CrossingPoints from Skeleton.  <br>Default Pipeline Input: (bool[,]) Skeleton<br>Default Pipeline Output: (List{Point}) EndPoints, (List{Point}) CrossingPoints
- [`Extrema`](./SigStat/Common/Transforms/Extrema.md)
	- Extracts minimum and maximum values of given feature.  <br>Default Pipeline Output: (List{double}) Min, (List{double}) Max
- [`HSCPThinning`](./SigStat/Common/Transforms/HSCPThinning.md)
	- Iteratively thins the input binary raster with the [SigStat.Common.Algorithms.HSCPThinningStep](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Algorithms/HSCPThinningStep.md) algorithm.  <br>Pipeline Input type: bool[,]<br>Default Pipeline Output: (bool[,]) HSCPThinningResult
- [`ImageGenerator`](./SigStat/Common/Transforms/ImageGenerator.md)
	- Generates an image feature out of a binary raster.  Optionally, saves the image to a png file.  Useful for debugging pipeline steps.  <br>Pipeline Input type: bool[,]<br>Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage
- [`Map`](./SigStat/Common/Transforms/Map.md)
	- Maps values of a feature to a specified range.  <br>Pipeline Input type: List{double}<br>Default Pipeline Output: (List{double}) MapResult
- [`Multiply`](./SigStat/Common/Transforms/Multiply.md)
	- Multiplies the values of a feature with a given constant.  <br>Pipeline Input type: List{double}<br>Default Pipeline Output: (List{double}) Input
- [`Normalize`](./SigStat/Common/Transforms/Normalize.md)
	- Maps values of a feature to 0.0 - 1.0 range.  <br>Pipeline Input type: List{double}<br>Default Pipeline Output: (List{double}) NormalizationResult
- [`OnePixelThinning`](./SigStat/Common/Transforms/OnePixelThinning.md)
	- Iteratively thins the input binary raster with the [SigStat.Common.Algorithms.OnePixelThinningStep](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Algorithms/OnePixelThinningStep.md) algorithm.  <br>Pipeline Input type: bool[,]<br>Default Pipeline Output: (bool[,]) OnePixelThinningResult
- [`RealisticImageGenerator`](./SigStat/Common/Transforms/RealisticImageGenerator.md)
	- Generates a realistic looking image of the Signature based on standard features. Uses blue ink and white paper. It does NOT save the image to file.  <br>Default Pipeline Input: X, Y, Button, Pressure, Azimuth, Altitude [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: [SigStat.Common.Features.Image]()
- [`Resize`](./SigStat/Common/Transforms/Resize.md)
	- Resizes the image to a specified width and height
- [`TangentExtraction`](./SigStat/Common/Transforms/TangentExtraction.md)
	- Extracts tangent values of the standard X, Y [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Input: X, Y [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Features.md)<br>Default Pipeline Output: (List{double})  Tangent
- [`TimeReset`](./SigStat/Common/Transforms/TimeReset.md)
	- Sequential pipeline to reset time values to begin at 0.  The following Transforms are called: Extrema, Multiply, AddVector.  <br>Default Pipeline Input: [SigStat.Common.Features.T](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md)<br>Default Pipeline Output: [SigStat.Common.Features.T](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md)
- [`Translate`](./SigStat/Common/Transforms/Translate.md)
	- Sequential pipeline to translate X and Y [SigStat.Common.Features](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Features.md) by specified vector (constant or feature).  The following Transforms are called: [SigStat.Common.Transforms.AddConst](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Transforms/AddConst.md) twice, or [SigStat.Common.Transforms.AddVector](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Transforms/AddVector.md).  <br>Default Pipeline Input: [SigStat.Common.Features.X](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md), [SigStat.Common.Features.Y](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md)<br>Default Pipeline Output: [SigStat.Common.Features.X](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md), [SigStat.Common.Features.Y](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor-1.md)
- [`Trim`](./SigStat/Common/Transforms/Trim.md)
	- Trims unnecessary empty space from a binary raster.  <br>Pipeline Input type: bool[,]<br>Default Pipeline Output: (bool[,]) Trimmed
### [SigStat.Common.PipelineItems.Transforms.Preprocessing](./SigStat/Common/PipelineItems/Transforms/Preprocessing/README.md)

- [`CubicInterpolation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/CubicInterpolation.md)
	- Cubic interpolation algorithm
- [`FillPenUpDurations`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/FillPenUpDurations.md)
	- This transformation will fill "holes" in the "Time" feature by interpolating the last known  feature values.
- [`FilterPoints`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/FilterPoints.md)
	- Removes samples based on a criteria from online signature time series
- [`IInterpolation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/IInterpolation.md)
	- Represents an interploation algorithm
- [`LinearInterpolation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/LinearInterpolation.md)
	- Performs linear interpolation on the input
- [`NormalizeRotation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/NormalizeRotation.md)
	- Performs rotation normalization on the online signature
- [`RelativeScale`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/RelativeScale.md)
	- Maps values of a feature to a specific range.  <br>InputFeature: feature to be scaled.<br>OutputFeature: output feature for scaled InputFeature&gt;
- [`ResampleSamplesCountBased`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/ResampleSamplesCountBased.md)
	- Resamples an online signature to a specific sample count using the specified [SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation]() algorithm
- [`Scale`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/Scale.md)
	- Maps values of a feature to a specific range.  <br>InputFeature: feature to be scaled.<br>OutputFeature: output feature for scaled InputFeature&gt;
- [`TranslatePreproc`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/TranslatePreproc.md)
	- This transformations can be used to translate the coordinates of an online signature
- [`UniformScale`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/UniformScale.md)
	- Maps values of a feature to a specific range and another proportional.  <br>BaseDimension: feature modelled the base dimension of the scaling. <br>ProportionalDimension: feature modelled the dimension scaled proportionally to the base dimension. <br>BaseDimensionOutput: output feature for scaled BaseDimension&gt;<br>ProportionalDimensionOutput: output feature for scaled ProportionalDimension&gt;
### [SigStat.Common.PipelineItems.Classifiers](./SigStat/Common/PipelineItems/Classifiers/README.md)

- [`DtwClassifier`](./SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md)
	- Classifies Signatures with the [SigStat.Common.Algorithms.Dtw](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Algorithms/Dtw.md) algorithm.
- [`DtwSignerModel`](./SigStat/Common/PipelineItems/Classifiers/DtwSignerModel.md)
	- Represents a trained model for [SigStat.Common.PipelineItems.Classifiers.DtwClassifier]()
- [`OptimalDtwClassifier`](./SigStat/Common/PipelineItems/Classifiers/OptimalDtwClassifier.md)
	- This [SigStat.Common.Pipeline.IDistanceClassifier]() implementation will consider both test and  training samples and claculate the threshold to separate the original and forged  signatures to approximate EER. Note that this classifier is not applicable for  real world scenarios. It was developed to test the theoratical boundaries of  threshold based classification
- [`WeightedClassifier`](./SigStat/Common/PipelineItems/Classifiers/WeightedClassifier.md)
	- Classifies Signatures by weighing other Classifier results.
### [SigStat.Common.Loaders](./SigStat/Common/Loaders/README.md)

- [`DataSetLoader`](./SigStat/Common/Loaders/DataSetLoader.md)
	- Abstract loader class to inherit from. Implements ILogger.
- [`IDataSetLoader`](./SigStat/Common/Loaders/IDataSetLoader.md)
	- Exposes a function to enable loading collections of [SigStat.Common.Signer](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Signer.md)s.  Base abstract class: [SigStat.Common.Loaders.DataSetLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/DataSetLoader.md).
- [`ImageLoader`](./SigStat/Common/Loaders/ImageLoader.md)
	- DataSetLoader for Image type databases.  Similar format to Svc2004Loader, but finds png images.
- [`ImageSaver`](./SigStat/Common/Loaders/ImageSaver.md)
	- Get the [SigStat.Common.Features.Image]() of a [SigStat.Common.Signature](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Signature.md) and save it as png file.
- [`MCYTLoader`](./SigStat/Common/Loaders/MCYTLoader.md)
	- [SigStat.Common.Loaders.DataSetLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the MCYT dataset
- [`SigComp11ChineseLoader`](./SigStat/Common/Loaders/SigComp11ChineseLoader.md)
	- [SigStat.Common.Loaders.DataSetLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp11Chinese dataset
- [`SigComp11DutchLoader`](./SigStat/Common/Loaders/SigComp11DutchLoader.md)
	- [SigStat.Common.Loaders.DataSetLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp11Dutch dataset
- [`SigComp13JapaneseLoader`](./SigStat/Common/Loaders/SigComp13JapaneseLoader.md)
	- [SigStat.Common.Loaders.DataSetLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp13Japanese dataset
- [`SigComp15GermanLoader`](./SigStat/Common/Loaders/SigComp15GermanLoader.md)
	- [SigStat.Common.Loaders.DataSetLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp15German dataset
- [`SigComp19OnlineLoader`](./SigStat/Common/Loaders/SigComp19OnlineLoader.md)
	- [SigStat.Common.Loaders.DataSetLoader](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Loaders/DataSetLoader.md) for the SigComp19 dataset
- [`Svc2004`](./SigStat/Common/Loaders/Svc2004.md)
	- Set of features containing raw data loaded from SVC2004-format database.
- [`Svc2004Loader`](./SigStat/Common/Loaders/Svc2004Loader.md)
	- Loads SVC2004-format database from .zip
### [SigStat.Common.Helpers](./SigStat/Common/Helpers/README.md)

- [`ExcelHelper`](./SigStat/Common/Helpers/ExcelHelper.md)
	- Extension methods for common EPPlus tasks
- [`FeatureDescriptorJsonConverter`](./SigStat/Common/Helpers/FeatureDescriptorJsonConverter.md)
	- Custom serializer for [SigStat.Common.FeatureDescriptor](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor.md) objects
- [`FeatureDescriptorTJsonConverter`](./SigStat/Common/Helpers/FeatureDescriptorTJsonConverter.md)
	- Custom serializer for [SigStat.Common.FeatureDescriptor`1](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/FeatureDescriptor`1.md) objects
- [`HierarchyElement`](./SigStat/Common/Helpers/HierarchyElement.md)
	- Hierarchical structure to store object
- [`IProgress`](./SigStat/Common/Helpers/IProgress.md)
	- Enables progress tracking by expsoing the [SigStat.Common.Helpers.IProgress.Progress]() property and the [SigStat.Common.Helpers.IProgress.ProgressChanged]() event.
- [`SerializationHelper`](./SigStat/Common/Helpers/SerializationHelper.md)
	- Json serialization and deserialization using the custom resolver  [SigStat.Common.Helpers.Serialization.VerifierResolver]()
- [`SimpleConsoleLogger`](./SigStat/Common/Helpers/SimpleConsoleLogger.md)
	- A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.
### [SigStat.Common.Helpers.Serialization](./SigStat/Common/Helpers/Serialization/README.md)

- [`DistanceFunctionJsonConverter`](./SigStat/Common/Helpers/Serialization/DistanceFunctionJsonConverter.md)
	- Helper class for serializing distance functions
- [`FeatureStreamingContextState`](./SigStat/Common/Helpers/Serialization/FeatureStreamingContextState.md)
	- SerializationContext for serializing SigStat objects
- [`RectangleFConverter`](./SigStat/Common/Helpers/Serialization/RectangleFConverter.md)
	- Custom serializer for [System.Drawing.RectangleF]() objects
### [SigStat.Common.Helpers.Excel](./SigStat/Common/Helpers/Excel/README.md)

- [`Palette`](./SigStat/Common/Helpers/Excel/Palette.md)
### [SigStat.Common.Model](./SigStat/Common/Model/README.md)

- [`Verifier`](./SigStat/Common/Model/Verifier.md)
	- Uses pipelines to transform, train on, and classify [SigStat.Common.Signature](https://github.com/sigstat/sigstat/tree/master/docs/md/SigStat/Common/Signature.md) objects.
### [SigStat.Common.Framework.Samplers](./SigStat/Common/Framework/Samplers/README.md)

- [`EvenNSampler`](./SigStat/Common/Framework/Samplers/EvenNSampler.md)
	- Selects the first N signatures with even index for training
- [`FirstNSampler`](./SigStat/Common/Framework/Samplers/FirstNSampler.md)
	- Selects the first N signatures for training
- [`LastNSampler`](./SigStat/Common/Framework/Samplers/LastNSampler.md)
	- Selects the first N signatures for training
- [`OddNSampler`](./SigStat/Common/Framework/Samplers/OddNSampler.md)
	- Selects the first N signatures with odd index for training
- [`UniversalSampler`](./SigStat/Common/Framework/Samplers/UniversalSampler.md)
	- Selects a given number of signatures for training and testing
### [SigStat.Common.Algorithms](./SigStat/Common/Algorithms/README.md)

- [`Dtw`](./SigStat/Common/Algorithms/Dtw.md)
	- Dynamic Time Warping algorithm
- [`DtwPy`](./SigStat/Common/Algorithms/DtwPy.md)
	- A simple implementation of the DTW algorithm.
- [`HSCPThinningStep`](./SigStat/Common/Algorithms/HSCPThinningStep.md)
	- HSCP thinning algorithm  http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf
- [`PatternMatching3x3`](./SigStat/Common/Algorithms/PatternMatching3x3.md)
	- Binary 3x3 pattern matcher with rotating option.

