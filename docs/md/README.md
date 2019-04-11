# [Api](./README.md)

## Common

- [`ArrayExtension`](./SigStat/Common/ArrayExtension.md)
- [`Baseline`](./SigStat/Common/Baseline.md)
- [`BasicMetadataExtraction`](./SigStat/Common/BasicMetadataExtraction.md)
- [`BenchmarkResults`](./SigStat/Common/BenchmarkResults.md)
- [`DistanceMatrix`](./SigStat/Common/DistanceMatrix-3.md)\<[`TRowKey`](./README.md), [`TColumnKey`](./README.md), [`TValue`](./README.md)>
- [`DutchSampler1`](./SigStat/Common/DutchSampler1.md)
- [`DutchSampler2`](./SigStat/Common/DutchSampler2.md)
- [`DutchSampler3`](./SigStat/Common/DutchSampler3.md)
- [`DutchSampler4`](./SigStat/Common/DutchSampler4.md)
- [`ErrorRate`](./SigStat/Common/ErrorRate.md)
- [`FeatureDescriptor`](./SigStat/Common/FeatureDescriptor.md)
- [`FeatureDescriptor`](./SigStat/Common/FeatureDescriptor-1.md)\<[`T`](./README.md)>
- [`Features`](./SigStat/Common/Features.md)
- [`ILoggerObject`](./SigStat/Common/ILoggerObject.md)
- [`ILoggerObjectExtensions`](./SigStat/Common/ILoggerObjectExtensions.md)
- [`ITransformation`](./SigStat/Common/ITransformation.md)
- [`Loop`](./SigStat/Common/Loop.md)
- [`MathHelper`](./SigStat/Common/MathHelper.md)
- [`McytSampler1`](./SigStat/Common/McytSampler1.md)
- [`McytSampler2`](./SigStat/Common/McytSampler2.md)
- [`McytSampler3`](./SigStat/Common/McytSampler3.md)
- [`McytSampler4`](./SigStat/Common/McytSampler4.md)
- [`PipelineBase`](./SigStat/Common/PipelineBase.md)
- [`Result`](./SigStat/Common/Result.md)
- [`Sampler`](./SigStat/Common/Sampler.md)
- [`Signature`](./SigStat/Common/Signature.md)
- [`Signer`](./SigStat/Common/Signer.md)
- [`SigStatEvents`](./SigStat/Common/SigStatEvents.md)
- [`SimpleRenderingTransformation`](./SigStat/Common/SimpleRenderingTransformation.md)
- [`SVC2004Sampler1`](./SigStat/Common/SVC2004Sampler1.md)
- [`SVC2004Sampler2`](./SigStat/Common/SVC2004Sampler2.md)
- [`SVC2004Sampler3`](./SigStat/Common/SVC2004Sampler3.md)
- [`SVC2004Sampler4`](./SigStat/Common/SVC2004Sampler4.md)
- [`VerifierBenchmark`](./SigStat/Common/VerifierBenchmark.md)
## Pipeline

- [`IClassifier`](./SigStat/Common/Pipeline/IClassifier.md)
- [`Input`](./SigStat/Common/Pipeline/Input.md)
- [`IPipelineIO`](./SigStat/Common/Pipeline/IPipelineIO.md)
- [`ISignerModel`](./SigStat/Common/Pipeline/ISignerModel.md)
- [`Output`](./SigStat/Common/Pipeline/Output.md)
- [`ParallelTransformPipeline`](./SigStat/Common/Pipeline/ParallelTransformPipeline.md)
- [`PipelineInput`](./SigStat/Common/Pipeline/PipelineInput.md)
- [`PipelineOutput`](./SigStat/Common/Pipeline/PipelineOutput.md)
- [`SequentialTransformPipeline`](./SigStat/Common/Pipeline/SequentialTransformPipeline.md)
## Transforms

- [`AddConst`](./SigStat/Common/Transforms/AddConst.md)
- [`AddVector`](./SigStat/Common/Transforms/AddVector.md)
- [`ApproximateOnlineFeatures`](./SigStat/Common/Transforms/ApproximateOnlineFeatures.md)
- [`Binarization`](./SigStat/Common/Transforms/Binarization.md)
- [`BinaryRasterizer`](./SigStat/Common/Transforms/BinaryRasterizer.md)
- [`CentroidExtraction`](./SigStat/Common/Transforms/CentroidExtraction.md)
- [`CentroidTranslate`](./SigStat/Common/Transforms/CentroidTranslate.md)
- [`ComponentExtraction`](./SigStat/Common/Transforms/ComponentExtraction.md)
- [`ComponentSorter`](./SigStat/Common/Transforms/ComponentSorter.md)
- [`ComponentsToFeatures`](./SigStat/Common/Transforms/ComponentsToFeatures.md)
- [`EndpointExtraction`](./SigStat/Common/Transforms/EndpointExtraction.md)
- [`Extrema`](./SigStat/Common/Transforms/Extrema.md)
- [`HSCPThinning`](./SigStat/Common/Transforms/HSCPThinning.md)
- [`ImageGenerator`](./SigStat/Common/Transforms/ImageGenerator.md)
- [`Map`](./SigStat/Common/Transforms/Map.md)
- [`Multiply`](./SigStat/Common/Transforms/Multiply.md)
- [`Normalize`](./SigStat/Common/Transforms/Normalize.md)
- [`OnePixelThinning`](./SigStat/Common/Transforms/OnePixelThinning.md)
- [`RealisticImageGenerator`](./SigStat/Common/Transforms/RealisticImageGenerator.md)
- [`Resize`](./SigStat/Common/Transforms/Resize.md)
- [`TangentExtraction`](./SigStat/Common/Transforms/TangentExtraction.md)
- [`TimeReset`](./SigStat/Common/Transforms/TimeReset.md)
- [`Translate`](./SigStat/Common/Transforms/Translate.md)
- [`Trim`](./SigStat/Common/Transforms/Trim.md)
## Preprocessing

- [`CubicInterpolation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/CubicInterpolation.md)
- [`FillPenUpDurations`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/FillPenUpDurations.md)
- [`FilterPoints`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/FilterPoints.md)
- [`IInterpolation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/IInterpolation.md)
- [`LinearInterpolation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/LinearInterpolation.md)
- [`NormalizeRotation`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/NormalizeRotation.md)
- [`ResampleSamplesCountBased`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/ResampleSamplesCountBased.md)
- [`ResampleTimeBased`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/ResampleTimeBased.md)
- [`Scale`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/Scale.md)
- [`TranslatePreproc`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/TranslatePreproc.md)
- [`UniformScale`](./SigStat/Common/PipelineItems/Transforms/Preprocessing/UniformScale.md)
## Classifiers

- [`DtwClassifier`](./SigStat/Common/PipelineItems/Classifiers/DtwClassifier.md)
- [`DtwSignerModel`](./SigStat/Common/PipelineItems/Classifiers/DtwSignerModel.md)
- [`OptimalDtwClassifier`](./SigStat/Common/PipelineItems/Classifiers/OptimalDtwClassifier.md)
- [`WeightedClassifier`](./SigStat/Common/PipelineItems/Classifiers/WeightedClassifier.md)
## Loaders

- [`BenchmarkBuilder`](./SigStat/Common/Loaders/BenchmarkBuilder.md)
- [`DataSetLoader`](./SigStat/Common/Loaders/DataSetLoader.md)
- [`IDataSetLoader`](./SigStat/Common/Loaders/IDataSetLoader.md)
- [`ImageLoader`](./SigStat/Common/Loaders/ImageLoader.md)
- [`ImageSaver`](./SigStat/Common/Loaders/ImageSaver.md)
- [`MCYTLoader`](./SigStat/Common/Loaders/MCYTLoader.md)
- [`SigComp11DutchLoader`](./SigStat/Common/Loaders/SigComp11DutchLoader.md)
- [`Svc2004`](./SigStat/Common/Loaders/Svc2004.md)
- [`Svc2004Loader`](./SigStat/Common/Loaders/Svc2004Loader.md)
## Helpers

- [`BenchmarkConfig`](./SigStat/Common/Helpers/BenchmarkConfig.md)
- [`FeatureDescriptorJsonConverter`](./SigStat/Common/Helpers/FeatureDescriptorJsonConverter.md)
- [`FeatureDescriptorTJsonConverter`](./SigStat/Common/Helpers/FeatureDescriptorTJsonConverter.md)
- [`IProgress`](./SigStat/Common/Helpers/IProgress.md)
- [`SerializationHelper`](./SigStat/Common/Helpers/SerializationHelper.md)
- [`SimpleConsoleLogger`](./SigStat/Common/Helpers/SimpleConsoleLogger.md)
## Serialization

- [`RectangleFConverter`](./SigStat/Common/Helpers/Serialization/RectangleFConverter.md)
## Excel

- [`CellHandler`](./SigStat/Common/Helpers/Excel/CellHandler.md)
- [`HierarchyElement`](./SigStat/Common/Helpers/Excel/HierarchyElement.md)
## Palette

- [`Palette`](./SigStat/Common/Helpers/Excel/Palette/Palette.md)
## Level

## Model

- [`Verifier`](./SigStat/Common/Model/Verifier.md)
## Exceptions

- [`VerifierTestingException`](./SigStat/Common/Framework/Exceptions/VerifierTestingException.md)
- [`VerifierTrainingException`](./SigStat/Common/Framework/Exceptions/VerifierTrainingException.md)
## Algorithms

- [`Dtw`](./SigStat/Common/Algorithms/Dtw.md)
- [`DtwPy`](./SigStat/Common/Algorithms/DtwPy.md)
- [`HSCPThinningStep`](./SigStat/Common/Algorithms/HSCPThinningStep.md)
- [`PatternMatching3x3`](./SigStat/Common/Algorithms/PatternMatching3x3.md)

