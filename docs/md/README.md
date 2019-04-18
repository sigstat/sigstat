# [API Documentation](./README.md)

## Namespaces

### Common

- [`ArrayExtension`](./SigStat/Common/ArrayExtension.md)
	- Helper methods for processing arrays
- [`Baseline`](./SigStat/Common/Baseline.md)
- [`BasicMetadataExtraction`](./SigStat/Common/BasicMetadataExtraction.md)
	- Extracts basic statistical signature (like `SigStat.Common.Features.Bounds` or `SigStat.Common.Features.Cog`) information from an Image
- [`BenchmarkResults`](./SigStat/Common/BenchmarkResults.md)
	- Contains the benchmark results of every `SigStat.Common.Signer` and the summarized final results.
- [`DistanceMatrix`](./SigStat/Common/DistanceMatrix-3.md)\<[`TRowKey`](./README.md), [`TColumnKey`](./README.md), [`TValue`](./README.md)>
- [`DutchSampler1`](./SigStat/Common/DutchSampler1.md)
- [`DutchSampler2`](./SigStat/Common/DutchSampler2.md)
- [`DutchSampler3`](./SigStat/Common/DutchSampler3.md)
- [`DutchSampler4`](./SigStat/Common/DutchSampler4.md)
- [`ErrorRate`](./SigStat/Common/ErrorRate.md)
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
- [`ITransformation`](./SigStat/Common/ITransformation.md)
	- Allows implementing a pipeline transform item capable of logging, progress tracking and IO rewiring.
- [`Loop`](./SigStat/Common/Loop.md)
	- Represents a loop in a signature
- [`MathHelper`](./SigStat/Common/MathHelper.md)
	- Common mathematical functions used by the SigStat framework
- [`McytSampler1`](./SigStat/Common/McytSampler1.md)
- [`McytSampler2`](./SigStat/Common/McytSampler2.md)
- [`McytSampler3`](./SigStat/Common/McytSampler3.md)
- [`McytSampler4`](./SigStat/Common/McytSampler4.md)
- [`PipelineBase`](./SigStat/Common/PipelineBase.md)
	- TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.  ILoggerObject, IProgress, IPipelineIO default implementacioja.
- [`Result`](./SigStat/Common/Result.md)
	- Contains the benchmark results of a single `SigStat.Common.Signer`
- [`Sampler`](./SigStat/Common/Sampler.md)
	- Takes samples from a set of `SigStat.Common.Signature`s by given sampling strategies.  Use this to fine-tune the `SigStat.Common.VerifierBenchmark`
- [`Signature`](./SigStat/Common/Signature.md)
	- Represents a signature as a collection of features, containing the data that flows in the pipeline.
- [`Signer`](./SigStat/Common/Signer.md)
	- Represents a person as a `SigStat.Common.Signer.ID` and a list of `SigStat.Common.Signer.Signatures`.
- [`SigStatEvents`](./SigStat/Common/SigStatEvents.md)
	- Standard event identifiers used by the SigStat system
- [`SimpleRenderingTransformation`](./SigStat/Common/SimpleRenderingTransformation.md)
	- Renders an image of the signature based on the available online information (X,Y,Dpi)
- [`SVC2004Sampler1`](./SigStat/Common/SVC2004Sampler1.md)
- [`SVC2004Sampler2`](./SigStat/Common/SVC2004Sampler2.md)
- [`SVC2004Sampler3`](./SigStat/Common/SVC2004Sampler3.md)
- [`SVC2004Sampler4`](./SigStat/Common/SVC2004Sampler4.md)
- [`VerifierBenchmark`](./SigStat/Common/VerifierBenchmark.md)
	- Benchmarking class to test error rates of a `SigStat.Common.Model.Verifier`
### Pipeline

- [`IClassifier`](./SigStat/Common/Pipeline/IClassifier.md)
	- Trains classification models based on reference signatures
- [`Input`](./SigStat/Common/Pipeline/Input.md)
- [`IPipelineIO`](./SigStat/Common/Pipeline/IPipelineIO.md)
- [`ISignerModel`](./SigStat/Common/Pipeline/ISignerModel.md)
	- Analyzes signatures based on their similiarity to the trained model
- [`Output`](./SigStat/Common/Pipeline/Output.md)
- [`ParallelTransformPipeline`](./SigStat/Common/Pipeline/ParallelTransformPipeline.md)
	- Runs pipeline items in parallel.  <para>Default Pipeline Output: Range of all the Item outputs.</para>
- [`PipelineInput`](./SigStat/Common/Pipeline/PipelineInput.md)
- [`PipelineOutput`](./SigStat/Common/Pipeline/PipelineOutput.md)
- [`SequentialTransformPipeline`](./SigStat/Common/Pipeline/SequentialTransformPipeline.md)
	- Runs pipeline items in a sequence.  <para>Default Pipeline Output: Output of the last Item in the sequence.</para>
### Transforms

- [`AddConst`](./SigStat/Common/Transforms/AddConst.md)
	- Adds a constant value to a feature. Works with collection features too.  <para>Default Pipeline Output: Pipeline Input</para>
- [`AddVector`](./SigStat/Common/Transforms/AddVector.md)
	- Adds a vector feature's elements to other features.  <para>Default Pipeline Output: Pipeline Input</para>
- [`ApproximateOnlineFeatures`](./SigStat/Common/Transforms/ApproximateOnlineFeatures.md)
	- init Pressure, Altitude, Azimuth features with default values.  <para>Default Pipeline Output: Features.Pressure, Features.Altitude, Features.Azimuth</para>
- [`Binarization`](./SigStat/Common/Transforms/Binarization.md)
	- Generates a binary raster version of the input image with the iterative threshold method.  <para>Pipeline Input type: Image{Rgba32}</para><para>Default Pipeline Output: (bool[,]) Binarized</para>
- [`BinaryRasterizer`](./SigStat/Common/Transforms/BinaryRasterizer.md)
	- Converts standard features to a binary raster.  <para>Default Pipeline Input: Standard `SigStat.Common.Features`</para><para>Default Pipeline Output: (bool[,]) Binarized</para>
- [`CentroidExtraction`](./SigStat/Common/Transforms/CentroidExtraction.md)
	- Extracts the Centroid (aka. Center Of Gravity) of the input features.  <para> Default Pipeline Output: (List{double}) Centroid. </para>
- [`CentroidTranslate`](./SigStat/Common/Transforms/CentroidTranslate.md)
	- Sequential pipeline to translate X and Y `SigStat.Common.Features` to Centroid.  The following Transforms are called: `SigStat.Common.Transforms.CentroidExtraction`, `SigStat.Common.Transforms.Multiply`(-1), `SigStat.Common.Transforms.Translate`<para>Default Pipeline Input: `SigStat.Common.Features.X`, `SigStat.Common.Features.Y`</para><para>Default Pipeline Output: (List{double}) Centroid</para>
- [`ComponentExtraction`](./SigStat/Common/Transforms/ComponentExtraction.md)
	- Extracts unsorted components by tracing through the binary Skeleton raster.  <para>Default Pipeline Input: (bool[,]) Skeleton, (List{Point}) EndPoints, (List{Point}) CrossingPoints</para><para>Default Pipeline Output: (List{List{PointF}}) Components</para>
- [`ComponentSorter`](./SigStat/Common/Transforms/ComponentSorter.md)
	- Sorts Component order by comparing each starting X value, and finding nearest components.  <para>Default Pipeline Input: (bool[,]) Components</para><para>Default Pipeline Output: (bool[,]) Components</para>
- [`ComponentsToFeatures`](./SigStat/Common/Transforms/ComponentsToFeatures.md)
	- Extracts standard `SigStat.Common.Features` from sorted Components.  <para>Default Pipeline Input: (List{List{PointF}}) Components</para><para>Default Pipeline Output: X, Y, Button `SigStat.Common.Features`</para>
- [`EndpointExtraction`](./SigStat/Common/Transforms/EndpointExtraction.md)
	- Extracts EndPoints and CrossingPoints from Skeleton.  <para>Default Pipeline Input: (bool[,]) Skeleton</para><para>Default Pipeline Output: (List{Point}) EndPoints, (List{Point}) CrossingPoints </para>
- [`Extrema`](./SigStat/Common/Transforms/Extrema.md)
	- Extracts minimum and maximum values of given feature.  <para>Default Pipeline Output: (List{double}) Min, (List{double}) Max </para>
- [`HSCPThinning`](./SigStat/Common/Transforms/HSCPThinning.md)
	- Iteratively thins the input binary raster with the `SigStat.Common.Algorithms.HSCPThinningStep` algorithm.  <para>Pipeline Input type: bool[,]</para><para>Default Pipeline Output: (bool[,]) HSCPThinningResult </para>
- [`ImageGenerator`](./SigStat/Common/Transforms/ImageGenerator.md)
	- Generates an image feature out of a binary raster.  Optionally, saves the image to a png file.  Useful for debugging pipeline steps.  <para>Pipeline Input type: bool[,]</para><para>Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage</para>
- [`Map`](./SigStat/Common/Transforms/Map.md)
	- Maps values of a feature to a specified range.  <para>Pipeline Input type: List{double}</para><para>Default Pipeline Output: (List{double}) MapResult</para>
- [`Multiply`](./SigStat/Common/Transforms/Multiply.md)
	- Multiplies the values of a feature with a given constant.  <para>Pipeline Input type: List{double}</para><para>Default Pipeline Output: (List{double}) Input</para>
- [`Normalize`](./SigStat/Common/Transforms/Normalize.md)
	- Maps values of a feature to 0.0 - 1.0 range.  <para>Pipeline Input type: List{double}</para><para>Default Pipeline Output: (List{double}) NormalizationResult</para>
- [`OnePixelThinning`](./SigStat/Common/Transforms/OnePixelThinning.md)
	- Iteratively thins the input binary raster with the `SigStat.Common.Algorithms.OnePixelThinningStep` algorithm.  <para>Pipeline Input type: bool[,]</para><para>Default Pipeline Output: (bool[,]) OnePixelThinningResult </para>
- [`RealisticImageGenerator`](./SigStat/Common/Transforms/RealisticImageGenerator.md)
	- Generates a realistic looking image of the Signature based on standard features. Uses blue ink and white paper. It does NOT save the image to file.  <para>Default Pipeline Input: X, Y, Button, Pressure, Azimuth, Altitude `SigStat.Common.Features`</para><para>Default Pipeline Output: `SigStat.Common.Features.Image`</para>
- [`Resize`](./SigStat/Common/Transforms/Resize.md)
	- Resizes the image to a specified width and height
- [`TangentExtraction`](./SigStat/Common/Transforms/TangentExtraction.md)
	- Extracts tangent values of the standard X, Y `SigStat.Common.Features`<para>Default Pipeline Input: X, Y `SigStat.Common.Features`</para><para>Default Pipeline Output: (List{double})  Tangent </para>
- [`TimeReset`](./SigStat/Common/Transforms/TimeReset.md)
	- Sequential pipeline to reset time values to begin at 0.  The following Transforms are called: Extrema, Multiply, AddVector.  <para>Default Pipeline Input: `SigStat.Common.Features.T`</para><para>Default Pipeline Output: `SigStat.Common.Features.T`</para>
- [`Translate`](./SigStat/Common/Transforms/Translate.md)
	- Sequential pipeline to translate X and Y `SigStat.Common.Features` by specified vector (constant or feature).  The following Transforms are called: `SigStat.Common.Transforms.AddConst` twice, or `SigStat.Common.Transforms.AddVector`.  <para>Default Pipeline Input: `SigStat.Common.Features.X`, `SigStat.Common.Features.Y`</para><para>Default Pipeline Output: `SigStat.Common.Features.X`, `SigStat.Common.Features.Y`</para>
- [`Trim`](./SigStat/Common/Transforms/Trim.md)
	- Trims unnecessary empty space from a binary raster.  <para>Pipeline Input type: bool[,]</para><para>Default Pipeline Output: (bool[,]) Trimmed</para>
### Preprocessing

- [`CubicInterpolation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/CubicInterpolation.md)
- [`FillPenUpDurations`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/FillPenUpDurations.md)
- [`FilterPoints`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/FilterPoints.md)
- [`IInterpolation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/IInterpolation.md)
- [`LinearInterpolation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/LinearInterpolation.md)
- [`NormalizeRotation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/NormalizeRotation.md)
- [`ResampleSamplesCountBased`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/ResampleSamplesCountBased.md)
- [`ResampleTimeBased`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/ResampleTimeBased.md)
- [`Scale`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/Scale.md)
	- Maps values of a feature to a specific range.  <para>InputFeature: feature to be scaled.</para><para>OutputFeature: output feature for scaled InputFeature&gt;</para>
- [`TranslatePreproc`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/TranslatePreproc.md)
- [`UniformScale`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/UniformScale.md)
	- Maps values of a feature to a specific range and another proportional.  <para>BaseDimension: feature modelled the base dimension of the scaling. </para><para>ProportionalDimension: feature modelled the dimension scaled proportionally to the base dimension. </para><para>BaseDimensionOutput: output feature for scaled BaseDimension&gt;</para><para>ProportionalDimensionOutput: output feature for scaled ProportionalDimension&gt;</para>
### Classifiers

- [`DtwClassifier`](./SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md)
	- Classifies Signatures with the `SigStat.Common.Algorithms.Dtw` algorithm.
- [`DtwSignerModel`](./SigStat/Common/PipelineItems/Classifiers/DtwSignerModel.md)
	- Represents a trained model for `SigStat.Common.PipelineItems.Classifiers.DtwClassifier`
- [`OptimalDtwClassifier`](./SigStat/Common/PipelineItems/Classifiers/OptimalDtwClassifier.md)
- [`WeightedClassifier`](./SigStat/Common/PipelineItems/Classifiers/WeightedClassifier.md)
	- Classifies Signatures by weighing other Classifier results.
### Loaders

- [`BenchmarkBuilder`](./SigStat/Common/Loaders/BenchmarkBuilder.md)
- [`DataSetLoader`](./SigStat/Common/Loaders/DataSetLoader.md)
	- Abstract loader class to inherit from. Implements ILogger.
- [`IDataSetLoader`](./SigStat/Common/Loaders/IDataSetLoader.md)
	- Exposes a function to enable loading collections of `SigStat.Common.Signer`s.  Base abstract class: `SigStat.Common.Loaders.DataSetLoader`.
- [`ImageLoader`](./SigStat/Common/Loaders/ImageLoader.md)
	- DataSetLoader for Image type databases.  Similar format to Svc2004Loader, but finds png images.
- [`ImageSaver`](./SigStat/Common/Loaders/ImageSaver.md)
	- Get the `SigStat.Common.Features.Image` of a `SigStat.Common.Signature` and save it as png file.
- [`MCYTLoader`](./SigStat/Common/Loaders/MCYTLoader.md)
- [`SigComp11DutchLoader`](./SigStat/Common/Loaders/SigComp11DutchLoader.md)
- [`Svc2004`](./SigStat/Common/Loaders/Svc2004.md)
	- Set of features containing raw data loaded from SVC2004-format database.
- [`Svc2004Loader`](./SigStat/Common/Loaders/Svc2004Loader.md)
	- Loads SVC2004-format database from .zip
### Helpers

- [`BenchmarkConfig`](./SigStat/Common/Helpers/BenchmarkConfig.md)
- [`FeatureDescriptorJsonConverter`](./SigStat/Common/Helpers/FeatureDescriptorJsonConverter.md)
- [`FeatureDescriptorTJsonConverter`](./SigStat/Common/Helpers/FeatureDescriptorTJsonConverter.md)
- [`IProgress`](./SigStat/Common/Helpers/IProgress.md)
	- Enables progress tracking by expsoing the `SigStat.Common.Helpers.IProgress.Progress` property and the `SigStat.Common.Helpers.IProgress.ProgressChanged` event.
- [`SerializationHelper`](./SigStat/Common/Helpers/SerializationHelper.md)
- [`SimpleConsoleLogger`](./SigStat/Common/Helpers/SimpleConsoleLogger.md)
	- A easy-to-use class to log pipeline messages, complete with filtering levels and multi-thread support.
### Serialization

- [`RectangleFConverter`](./SigStat/Common/Helpers/Serialization/RectangleFConverter.md)
### Excel

- [`CellHandler`](./SigStat/Common/Helpers/Excel/CellHandler.md)
- [`HierarchyElement`](./SigStat/Common/Helpers/Excel/HierarchyElement.md)
	- Hierarchical structure to store object
### Palette

- [`Palette`](./SigStat/Common/Helpers/Excel/Palette/Palette.md)
### Level

### Model

- [`Verifier`](./SigStat/Common/Model/Verifier.md)
	- Uses pipelines to transform, train on, and classify `SigStat.Common.Signature` objects.
### Exceptions

- [`VerifierTestingException`](./SigStat/Common/Framework/Exceptions/VerifierTestingException.md)
- [`VerifierTrainingException`](./SigStat/Common/Framework/Exceptions/VerifierTrainingException.md)
### Algorithms

- [`Dtw`](./SigStat/Common/Algorithms/Dtw.md)
	- Dynamic Time Warping algorithm
- [`DtwPy`](./SigStat/Common/Algorithms/DtwPy.md)
	- A simple implementation of the DTW algorithm.
- [`HSCPThinningStep`](./SigStat/Common/Algorithms/HSCPThinningStep.md)
	- HSCP thinning algorithm  http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf
- [`PatternMatching3x3`](./SigStat/Common/Algorithms/PatternMatching3x3.md)
	- Binary 3x3 pattern matcher with rotating option.

