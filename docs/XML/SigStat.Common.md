<a name='assembly'></a>
# SigStat.Common

## Contents

- [AddConst](#T-SigStat-Common-Transforms-AddConst 'SigStat.Common.Transforms.AddConst')
  - [#ctor(value)](#M-SigStat-Common-Transforms-AddConst-#ctor-System-Double- 'SigStat.Common.Transforms.AddConst.#ctor(System.Double)')
  - [Input](#P-SigStat-Common-Transforms-AddConst-Input 'SigStat.Common.Transforms.AddConst.Input')
  - [Output](#P-SigStat-Common-Transforms-AddConst-Output 'SigStat.Common.Transforms.AddConst.Output')
  - [Transform()](#M-SigStat-Common-Transforms-AddConst-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.AddConst.Transform(SigStat.Common.Signature)')
- [AddVector](#T-SigStat-Common-Transforms-AddVector 'SigStat.Common.Transforms.AddVector')
  - [#ctor(vectorFeature)](#M-SigStat-Common-Transforms-AddVector-#ctor-SigStat-Common-FeatureDescriptor{System-Collections-Generic-List{System-Double}}- 'SigStat.Common.Transforms.AddVector.#ctor(SigStat.Common.FeatureDescriptor{System.Collections.Generic.List{System.Double}})')
  - [Inputs](#P-SigStat-Common-Transforms-AddVector-Inputs 'SigStat.Common.Transforms.AddVector.Inputs')
  - [Outputs](#P-SigStat-Common-Transforms-AddVector-Outputs 'SigStat.Common.Transforms.AddVector.Outputs')
  - [Transform()](#M-SigStat-Common-Transforms-AddVector-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.AddVector.Transform(SigStat.Common.Signature)')
- [ApproximateOnlineFeatures](#T-SigStat-Common-Transforms-ApproximateOnlineFeatures 'SigStat.Common.Transforms.ApproximateOnlineFeatures')
  - [Transform()](#M-SigStat-Common-Transforms-ApproximateOnlineFeatures-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.ApproximateOnlineFeatures.Transform(SigStat.Common.Signature)')
- [ArrayExtension](#T-SigStat-Common-ArrayExtension 'SigStat.Common.ArrayExtension')
  - [GetCog(weightMartix)](#M-SigStat-Common-ArrayExtension-GetCog-System-Double[0-,0-]- 'SigStat.Common.ArrayExtension.GetCog(System.Double[0:,0:])')
  - [GetValues\`\`1(array)](#M-SigStat-Common-ArrayExtension-GetValues``1-``0[0-,0-]- 'SigStat.Common.ArrayExtension.GetValues``1(``0[0:,0:])')
  - [SetValues\`\`1(array,value)](#M-SigStat-Common-ArrayExtension-SetValues``1-``0[0-,0-],``0- 'SigStat.Common.ArrayExtension.SetValues``1(``0[0:,0:],``0)')
  - [Sum(array,x1,y1,x2,y2)](#M-SigStat-Common-ArrayExtension-Sum-System-Double[0-,0-],System-Int32,System-Int32,System-Int32,System-Int32- 'SigStat.Common.ArrayExtension.Sum(System.Double[0:,0:],System.Int32,System.Int32,System.Int32,System.Int32)')
  - [SumCol(array,column)](#M-SigStat-Common-ArrayExtension-SumCol-System-Double[0-,0-],System-Int32- 'SigStat.Common.ArrayExtension.SumCol(System.Double[0:,0:],System.Int32)')
  - [SumRow(array,row)](#M-SigStat-Common-ArrayExtension-SumRow-System-Double[0-,0-],System-Int32- 'SigStat.Common.ArrayExtension.SumRow(System.Double[0:,0:],System.Int32)')
- [AutoSetMode](#T-SigStat-Common-Pipeline-AutoSetMode 'SigStat.Common.Pipeline.AutoSetMode')
  - [Always](#F-SigStat-Common-Pipeline-AutoSetMode-Always 'SigStat.Common.Pipeline.AutoSetMode.Always')
  - [IfNull](#F-SigStat-Common-Pipeline-AutoSetMode-IfNull 'SigStat.Common.Pipeline.AutoSetMode.IfNull')
  - [Never](#F-SigStat-Common-Pipeline-AutoSetMode-Never 'SigStat.Common.Pipeline.AutoSetMode.Never')
- [Baseline](#T-SigStat-Common-Baseline 'SigStat.Common.Baseline')
  - [#ctor()](#M-SigStat-Common-Baseline-#ctor 'SigStat.Common.Baseline.#ctor')
  - [#ctor(x1,y1,x2,y2)](#M-SigStat-Common-Baseline-#ctor-System-Int32,System-Int32,System-Int32,System-Int32- 'SigStat.Common.Baseline.#ctor(System.Int32,System.Int32,System.Int32,System.Int32)')
  - [End](#P-SigStat-Common-Baseline-End 'SigStat.Common.Baseline.End')
  - [Start](#P-SigStat-Common-Baseline-Start 'SigStat.Common.Baseline.Start')
  - [ToString()](#M-SigStat-Common-Baseline-ToString 'SigStat.Common.Baseline.ToString')
- [BasicMetadataExtraction](#T-SigStat-Common-BasicMetadataExtraction 'SigStat.Common.BasicMetadataExtraction')
  - [Trim](#P-SigStat-Common-BasicMetadataExtraction-Trim 'SigStat.Common.BasicMetadataExtraction.Trim')
  - [Transform()](#M-SigStat-Common-BasicMetadataExtraction-Transform-SigStat-Common-Signature- 'SigStat.Common.BasicMetadataExtraction.Transform(SigStat.Common.Signature)')
- [BenchmarkConfig](#T-SigStat-Common-Helpers-BenchmarkConfig 'SigStat.Common.Helpers.BenchmarkConfig')
  - [FromJsonFile(path)](#M-SigStat-Common-Helpers-BenchmarkConfig-FromJsonFile-System-String- 'SigStat.Common.Helpers.BenchmarkConfig.FromJsonFile(System.String)')
  - [FromJsonString(jsonString)](#M-SigStat-Common-Helpers-BenchmarkConfig-FromJsonString-System-String- 'SigStat.Common.Helpers.BenchmarkConfig.FromJsonString(System.String)')
  - [GenerateConfigurations()](#M-SigStat-Common-Helpers-BenchmarkConfig-GenerateConfigurations 'SigStat.Common.Helpers.BenchmarkConfig.GenerateConfigurations')
  - [Samplers(l)](#M-SigStat-Common-Helpers-BenchmarkConfig-Samplers-System-Collections-Generic-List{SigStat-Common-Helpers-BenchmarkConfig}- 'SigStat.Common.Helpers.BenchmarkConfig.Samplers(System.Collections.Generic.List{SigStat.Common.Helpers.BenchmarkConfig})')
  - [ToJsonString()](#M-SigStat-Common-Helpers-BenchmarkConfig-ToJsonString 'SigStat.Common.Helpers.BenchmarkConfig.ToJsonString')
  - [ToKeyValuePairs()](#M-SigStat-Common-Helpers-BenchmarkConfig-ToKeyValuePairs 'SigStat.Common.Helpers.BenchmarkConfig.ToKeyValuePairs')
  - [ToShortString()](#M-SigStat-Common-Helpers-BenchmarkConfig-ToShortString 'SigStat.Common.Helpers.BenchmarkConfig.ToShortString')
- [BenchmarkKeyValueLogState](#T-SigStat-Common-Logging-BenchmarkKeyValueLogState 'SigStat.Common.Logging.BenchmarkKeyValueLogState')
  - [#ctor(group,key,value)](#M-SigStat-Common-Logging-BenchmarkKeyValueLogState-#ctor-System-String,System-String,System-Object- 'SigStat.Common.Logging.BenchmarkKeyValueLogState.#ctor(System.String,System.String,System.Object)')
  - [Group](#P-SigStat-Common-Logging-BenchmarkKeyValueLogState-Group 'SigStat.Common.Logging.BenchmarkKeyValueLogState.Group')
  - [Key](#P-SigStat-Common-Logging-BenchmarkKeyValueLogState-Key 'SigStat.Common.Logging.BenchmarkKeyValueLogState.Key')
  - [Value](#P-SigStat-Common-Logging-BenchmarkKeyValueLogState-Value 'SigStat.Common.Logging.BenchmarkKeyValueLogState.Value')
- [BenchmarkLogModel](#T-SigStat-Common-Logging-BenchmarkLogModel 'SigStat.Common.Logging.BenchmarkLogModel')
  - [#ctor()](#M-SigStat-Common-Logging-BenchmarkLogModel-#ctor 'SigStat.Common.Logging.BenchmarkLogModel.#ctor')
  - [BenchmarkResultsGroupName](#F-SigStat-Common-Logging-BenchmarkLogModel-BenchmarkResultsGroupName 'SigStat.Common.Logging.BenchmarkLogModel.BenchmarkResultsGroupName')
  - [ExecutionGroupName](#F-SigStat-Common-Logging-BenchmarkLogModel-ExecutionGroupName 'SigStat.Common.Logging.BenchmarkLogModel.ExecutionGroupName')
  - [ParametersGroupName](#F-SigStat-Common-Logging-BenchmarkLogModel-ParametersGroupName 'SigStat.Common.Logging.BenchmarkLogModel.ParametersGroupName')
  - [BenchmarkResults](#P-SigStat-Common-Logging-BenchmarkLogModel-BenchmarkResults 'SigStat.Common.Logging.BenchmarkLogModel.BenchmarkResults')
  - [Excecution](#P-SigStat-Common-Logging-BenchmarkLogModel-Excecution 'SigStat.Common.Logging.BenchmarkLogModel.Excecution')
  - [KeyValueGroups](#P-SigStat-Common-Logging-BenchmarkLogModel-KeyValueGroups 'SigStat.Common.Logging.BenchmarkLogModel.KeyValueGroups')
  - [Parameters](#P-SigStat-Common-Logging-BenchmarkLogModel-Parameters 'SigStat.Common.Logging.BenchmarkLogModel.Parameters')
  - [SignerResults](#P-SigStat-Common-Logging-BenchmarkLogModel-SignerResults 'SigStat.Common.Logging.BenchmarkLogModel.SignerResults')
- [BenchmarkResults](#T-SigStat-Common-BenchmarkResults 'SigStat.Common.BenchmarkResults')
  - [FinalResult](#F-SigStat-Common-BenchmarkResults-FinalResult 'SigStat.Common.BenchmarkResults.FinalResult')
  - [SignerResults](#F-SigStat-Common-BenchmarkResults-SignerResults 'SigStat.Common.BenchmarkResults.SignerResults')
- [BenchmarkResultsLogState](#T-SigStat-Common-Logging-BenchmarkResultsLogState 'SigStat.Common.Logging.BenchmarkResultsLogState')
  - [#ctor(aer,far,frr)](#M-SigStat-Common-Logging-BenchmarkResultsLogState-#ctor-System-Double,System-Double,System-Double- 'SigStat.Common.Logging.BenchmarkResultsLogState.#ctor(System.Double,System.Double,System.Double)')
  - [Aer](#P-SigStat-Common-Logging-BenchmarkResultsLogState-Aer 'SigStat.Common.Logging.BenchmarkResultsLogState.Aer')
  - [Far](#P-SigStat-Common-Logging-BenchmarkResultsLogState-Far 'SigStat.Common.Logging.BenchmarkResultsLogState.Far')
  - [Frr](#P-SigStat-Common-Logging-BenchmarkResultsLogState-Frr 'SigStat.Common.Logging.BenchmarkResultsLogState.Frr')
- [Binarization](#T-SigStat-Common-Transforms-Binarization 'SigStat.Common.Transforms.Binarization')
  - [#ctor()](#M-SigStat-Common-Transforms-Binarization-#ctor 'SigStat.Common.Transforms.Binarization.#ctor')
  - [#ctor(foregroundType,binThreshold)](#M-SigStat-Common-Transforms-Binarization-#ctor-SigStat-Common-Transforms-Binarization-ForegroundType,System-Nullable{System-Double}- 'SigStat.Common.Transforms.Binarization.#ctor(SigStat.Common.Transforms.Binarization.ForegroundType,System.Nullable{System.Double})')
  - [InputImage](#P-SigStat-Common-Transforms-Binarization-InputImage 'SigStat.Common.Transforms.Binarization.InputImage')
  - [OutputMask](#P-SigStat-Common-Transforms-Binarization-OutputMask 'SigStat.Common.Transforms.Binarization.OutputMask')
  - [IterativeThreshold(image,maxError)](#M-SigStat-Common-Transforms-Binarization-IterativeThreshold-SixLabors-ImageSharp-Image{SixLabors-ImageSharp-PixelFormats-Rgba32},System-Double- 'SigStat.Common.Transforms.Binarization.IterativeThreshold(SixLabors.ImageSharp.Image{SixLabors.ImageSharp.PixelFormats.Rgba32},System.Double)')
  - [Level(c)](#M-SigStat-Common-Transforms-Binarization-Level-SixLabors-ImageSharp-PixelFormats-Rgba32- 'SigStat.Common.Transforms.Binarization.Level(SixLabors.ImageSharp.PixelFormats.Rgba32)')
  - [Transform()](#M-SigStat-Common-Transforms-Binarization-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.Binarization.Transform(SigStat.Common.Signature)')
- [BinaryRasterizer](#T-SigStat-Common-Transforms-BinaryRasterizer 'SigStat.Common.Transforms.BinaryRasterizer')
  - [#ctor(resolutionX,resolutionY,penWidth)](#M-SigStat-Common-Transforms-BinaryRasterizer-#ctor-System-Int32,System-Int32,System-Single- 'SigStat.Common.Transforms.BinaryRasterizer.#ctor(System.Int32,System.Int32,System.Single)')
  - [InputButton](#P-SigStat-Common-Transforms-BinaryRasterizer-InputButton 'SigStat.Common.Transforms.BinaryRasterizer.InputButton')
  - [InputX](#P-SigStat-Common-Transforms-BinaryRasterizer-InputX 'SigStat.Common.Transforms.BinaryRasterizer.InputX')
  - [InputY](#P-SigStat-Common-Transforms-BinaryRasterizer-InputY 'SigStat.Common.Transforms.BinaryRasterizer.InputY')
  - [Output](#P-SigStat-Common-Transforms-BinaryRasterizer-Output 'SigStat.Common.Transforms.BinaryRasterizer.Output')
  - [Transform()](#M-SigStat-Common-Transforms-BinaryRasterizer-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.BinaryRasterizer.Transform(SigStat.Common.Signature)')
- [CentroidExtraction](#T-SigStat-Common-Transforms-CentroidExtraction 'SigStat.Common.Transforms.CentroidExtraction')
  - [Inputs](#P-SigStat-Common-Transforms-CentroidExtraction-Inputs 'SigStat.Common.Transforms.CentroidExtraction.Inputs')
  - [OutputCentroid](#P-SigStat-Common-Transforms-CentroidExtraction-OutputCentroid 'SigStat.Common.Transforms.CentroidExtraction.OutputCentroid')
  - [Transform()](#M-SigStat-Common-Transforms-CentroidExtraction-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.CentroidExtraction.Transform(SigStat.Common.Signature)')
- [CentroidTranslate](#T-SigStat-Common-Transforms-CentroidTranslate 'SigStat.Common.Transforms.CentroidTranslate')
  - [#ctor()](#M-SigStat-Common-Transforms-CentroidTranslate-#ctor 'SigStat.Common.Transforms.CentroidTranslate.#ctor')
  - [InputX](#P-SigStat-Common-Transforms-CentroidTranslate-InputX 'SigStat.Common.Transforms.CentroidTranslate.InputX')
  - [InputY](#P-SigStat-Common-Transforms-CentroidTranslate-InputY 'SigStat.Common.Transforms.CentroidTranslate.InputY')
  - [OutputX](#P-SigStat-Common-Transforms-CentroidTranslate-OutputX 'SigStat.Common.Transforms.CentroidTranslate.OutputX')
  - [OutputY](#P-SigStat-Common-Transforms-CentroidTranslate-OutputY 'SigStat.Common.Transforms.CentroidTranslate.OutputY')
- [ClassifierDistanceLogState](#T-SigStat-Common-Logging-ClassifierDistanceLogState 'SigStat.Common.Logging.ClassifierDistanceLogState')
  - [#ctor(signer1Id,signer2Id,signature1Id,signature2Id,distance)](#M-SigStat-Common-Logging-ClassifierDistanceLogState-#ctor-System-String,System-String,System-String,System-String,System-Double- 'SigStat.Common.Logging.ClassifierDistanceLogState.#ctor(System.String,System.String,System.String,System.String,System.Double)')
  - [Signature1Id](#P-SigStat-Common-Logging-ClassifierDistanceLogState-Signature1Id 'SigStat.Common.Logging.ClassifierDistanceLogState.Signature1Id')
  - [Signature2Id](#P-SigStat-Common-Logging-ClassifierDistanceLogState-Signature2Id 'SigStat.Common.Logging.ClassifierDistanceLogState.Signature2Id')
  - [Signer1Id](#P-SigStat-Common-Logging-ClassifierDistanceLogState-Signer1Id 'SigStat.Common.Logging.ClassifierDistanceLogState.Signer1Id')
  - [Signer2Id](#P-SigStat-Common-Logging-ClassifierDistanceLogState-Signer2Id 'SigStat.Common.Logging.ClassifierDistanceLogState.Signer2Id')
  - [distance](#P-SigStat-Common-Logging-ClassifierDistanceLogState-distance 'SigStat.Common.Logging.ClassifierDistanceLogState.distance')
- [ComponentExtraction](#T-SigStat-Common-Transforms-ComponentExtraction 'SigStat.Common.Transforms.ComponentExtraction')
  - [#ctor(samplingResolution)](#M-SigStat-Common-Transforms-ComponentExtraction-#ctor-System-Int32- 'SigStat.Common.Transforms.ComponentExtraction.#ctor(System.Int32)')
  - [CrossingPoints](#P-SigStat-Common-Transforms-ComponentExtraction-CrossingPoints 'SigStat.Common.Transforms.ComponentExtraction.CrossingPoints')
  - [EndPoints](#P-SigStat-Common-Transforms-ComponentExtraction-EndPoints 'SigStat.Common.Transforms.ComponentExtraction.EndPoints')
  - [OutputComponents](#P-SigStat-Common-Transforms-ComponentExtraction-OutputComponents 'SigStat.Common.Transforms.ComponentExtraction.OutputComponents')
  - [Skeleton](#P-SigStat-Common-Transforms-ComponentExtraction-Skeleton 'SigStat.Common.Transforms.ComponentExtraction.Skeleton')
  - [SplitCrossings(crs)](#M-SigStat-Common-Transforms-ComponentExtraction-SplitCrossings-System-Collections-Generic-List{System-Drawing-Point}- 'SigStat.Common.Transforms.ComponentExtraction.SplitCrossings(System.Collections.Generic.List{System.Drawing.Point})')
  - [Trace(endPoints)](#M-SigStat-Common-Transforms-ComponentExtraction-Trace-System-Collections-Generic-List{System-Drawing-Point}- 'SigStat.Common.Transforms.ComponentExtraction.Trace(System.Collections.Generic.List{System.Drawing.Point})')
  - [Transform()](#M-SigStat-Common-Transforms-ComponentExtraction-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.ComponentExtraction.Transform(SigStat.Common.Signature)')
- [ComponentSorter](#T-SigStat-Common-Transforms-ComponentSorter 'SigStat.Common.Transforms.ComponentSorter')
  - [Input](#P-SigStat-Common-Transforms-ComponentSorter-Input 'SigStat.Common.Transforms.ComponentSorter.Input')
  - [Output](#P-SigStat-Common-Transforms-ComponentSorter-Output 'SigStat.Common.Transforms.ComponentSorter.Output')
  - [Distance()](#M-SigStat-Common-Transforms-ComponentSorter-Distance-System-Collections-Generic-List{System-Drawing-PointF},System-Collections-Generic-List{System-Drawing-PointF}- 'SigStat.Common.Transforms.ComponentSorter.Distance(System.Collections.Generic.List{System.Drawing.PointF},System.Collections.Generic.List{System.Drawing.PointF})')
  - [Transform()](#M-SigStat-Common-Transforms-ComponentSorter-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.ComponentSorter.Transform(SigStat.Common.Signature)')
- [ComponentsToFeatures](#T-SigStat-Common-Transforms-ComponentsToFeatures 'SigStat.Common.Transforms.ComponentsToFeatures')
  - [Button](#P-SigStat-Common-Transforms-ComponentsToFeatures-Button 'SigStat.Common.Transforms.ComponentsToFeatures.Button')
  - [InputComponents](#P-SigStat-Common-Transforms-ComponentsToFeatures-InputComponents 'SigStat.Common.Transforms.ComponentsToFeatures.InputComponents')
  - [X](#P-SigStat-Common-Transforms-ComponentsToFeatures-X 'SigStat.Common.Transforms.ComponentsToFeatures.X')
  - [Y](#P-SigStat-Common-Transforms-ComponentsToFeatures-Y 'SigStat.Common.Transforms.ComponentsToFeatures.Y')
  - [Transform()](#M-SigStat-Common-Transforms-ComponentsToFeatures-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.ComponentsToFeatures.Transform(SigStat.Common.Signature)')
- [CompositeLogger](#T-SigStat-Common-Logging-CompositeLogger 'SigStat.Common.Logging.CompositeLogger')
  - [Loggers](#P-SigStat-Common-Logging-CompositeLogger-Loggers 'SigStat.Common.Logging.CompositeLogger.Loggers')
  - [BeginScope\`\`1()](#M-SigStat-Common-Logging-CompositeLogger-BeginScope``1-``0- 'SigStat.Common.Logging.CompositeLogger.BeginScope``1(``0)')
  - [IsEnabled()](#M-SigStat-Common-Logging-CompositeLogger-IsEnabled-Microsoft-Extensions-Logging-LogLevel- 'SigStat.Common.Logging.CompositeLogger.IsEnabled(Microsoft.Extensions.Logging.LogLevel)')
  - [Log\`\`1()](#M-SigStat-Common-Logging-CompositeLogger-Log``1-Microsoft-Extensions-Logging-LogLevel,Microsoft-Extensions-Logging-EventId,``0,System-Exception,System-Func{``0,System-Exception,System-String}- 'SigStat.Common.Logging.CompositeLogger.Log``1(Microsoft.Extensions.Logging.LogLevel,Microsoft.Extensions.Logging.EventId,``0,System.Exception,System.Func{``0,System.Exception,System.String})')
- [ConsoleMessageLoggedEventHandler](#T-SigStat-Common-Logging-SimpleConsoleLogger-ConsoleMessageLoggedEventHandler 'SigStat.Common.Logging.SimpleConsoleLogger.ConsoleMessageLoggedEventHandler')
- [CubicInterpolation](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-CubicInterpolation 'SigStat.Common.PipelineItems.Transforms.Preprocessing.CubicInterpolation')
  - [FeatureValues](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-CubicInterpolation-FeatureValues 'SigStat.Common.PipelineItems.Transforms.Preprocessing.CubicInterpolation.FeatureValues')
  - [TimeValues](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-CubicInterpolation-TimeValues 'SigStat.Common.PipelineItems.Transforms.Preprocessing.CubicInterpolation.TimeValues')
  - [GetValue(timestamp)](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-CubicInterpolation-GetValue-System-Double- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.CubicInterpolation.GetValue(System.Double)')
- [DataCleaningHelper](#T-SigStat-Common-Helpers-DataCleaningHelper 'SigStat.Common.Helpers.DataCleaningHelper')
  - [Insert2DPointsForGapBorders(gapIndexes,signature,unitTimeSlot)](#M-SigStat-Common-Helpers-DataCleaningHelper-Insert2DPointsForGapBorders-System-Int32[],SigStat-Common-Signature,System-Double- 'SigStat.Common.Helpers.DataCleaningHelper.Insert2DPointsForGapBorders(System.Int32[],SigStat.Common.Signature,System.Double)')
  - [InsertDuplicatedValuesForGapBorderPoints\`\`1(gapIndexes,featureValues)](#M-SigStat-Common-Helpers-DataCleaningHelper-InsertDuplicatedValuesForGapBorderPoints``1-System-Int32[],System-Collections-Generic-List{``0}- 'SigStat.Common.Helpers.DataCleaningHelper.InsertDuplicatedValuesForGapBorderPoints``1(System.Int32[],System.Collections.Generic.List{``0})')
  - [InsertPenUpValuesForGapBorderPoints(gapIndexes,penDownValues)](#M-SigStat-Common-Helpers-DataCleaningHelper-InsertPenUpValuesForGapBorderPoints-System-Int32[],System-Collections-Generic-List{System-Boolean}- 'SigStat.Common.Helpers.DataCleaningHelper.InsertPenUpValuesForGapBorderPoints(System.Int32[],System.Collections.Generic.List{System.Boolean})')
  - [InsertPressureValuesForGapBorderPoints(gapIndexes,pressureValues)](#M-SigStat-Common-Helpers-DataCleaningHelper-InsertPressureValuesForGapBorderPoints-System-Int32[],System-Collections-Generic-List{System-Double}- 'SigStat.Common.Helpers.DataCleaningHelper.InsertPressureValuesForGapBorderPoints(System.Int32[],System.Collections.Generic.List{System.Double})')
  - [InsertTimestampsForGapBorderPoints(gapIndexes,timestamps,difference)](#M-SigStat-Common-Helpers-DataCleaningHelper-InsertTimestampsForGapBorderPoints-System-Int32[],System-Collections-Generic-List{System-Double},System-Double- 'SigStat.Common.Helpers.DataCleaningHelper.InsertTimestampsForGapBorderPoints(System.Int32[],System.Collections.Generic.List{System.Double},System.Double)')
- [DataSetLoader](#T-SigStat-Common-Loaders-DataSetLoader 'SigStat.Common.Loaders.DataSetLoader')
  - [Logger](#P-SigStat-Common-Loaders-DataSetLoader-Logger 'SigStat.Common.Loaders.DataSetLoader.Logger')
  - [SamplingFrequency](#P-SigStat-Common-Loaders-DataSetLoader-SamplingFrequency 'SigStat.Common.Loaders.DataSetLoader.SamplingFrequency')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-DataSetLoader-EnumerateSigners 'SigStat.Common.Loaders.DataSetLoader.EnumerateSigners')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-DataSetLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.DataSetLoader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
- [DistanceFunctionJsonConverter](#T-SigStat-Common-Helpers-Serialization-DistanceFunctionJsonConverter 'SigStat.Common.Helpers.Serialization.DistanceFunctionJsonConverter')
- [DistanceMatrixConverter](#T-SigStat-Common-Helpers-Serialization-DistanceMatrixConverter 'SigStat.Common.Helpers.Serialization.DistanceMatrixConverter')
  - [ReadJson()](#M-SigStat-Common-Helpers-Serialization-DistanceMatrixConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,SigStat-Common-DistanceMatrix{System-String,System-String,System-Double},System-Boolean,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.DistanceMatrixConverter.ReadJson(Newtonsoft.Json.JsonReader,System.Type,SigStat.Common.DistanceMatrix{System.String,System.String,System.Double},System.Boolean,Newtonsoft.Json.JsonSerializer)')
  - [WriteJson()](#M-SigStat-Common-Helpers-Serialization-DistanceMatrixConverter-WriteJson-Newtonsoft-Json-JsonWriter,SigStat-Common-DistanceMatrix{System-String,System-String,System-Double},Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.DistanceMatrixConverter.WriteJson(Newtonsoft.Json.JsonWriter,SigStat.Common.DistanceMatrix{System.String,System.String,System.Double},Newtonsoft.Json.JsonSerializer)')
- [DistanceMatrix\`3](#T-SigStat-Common-DistanceMatrix`3 'SigStat.Common.DistanceMatrix`3')
  - [Item](#P-SigStat-Common-DistanceMatrix`3-Item-`0,`1- 'SigStat.Common.DistanceMatrix`3.Item(`0,`1)')
  - [ContainsKey(row,column)](#M-SigStat-Common-DistanceMatrix`3-ContainsKey-`0,`1- 'SigStat.Common.DistanceMatrix`3.ContainsKey(`0,`1)')
  - [GetValues()](#M-SigStat-Common-DistanceMatrix`3-GetValues 'SigStat.Common.DistanceMatrix`3.GetValues')
  - [ToArray()](#M-SigStat-Common-DistanceMatrix`3-ToArray 'SigStat.Common.DistanceMatrix`3.ToArray')
  - [TryGetValue(row,column,value)](#M-SigStat-Common-DistanceMatrix`3-TryGetValue-`0,`1,`2@- 'SigStat.Common.DistanceMatrix`3.TryGetValue(`0,`1,`2@)')
- [Dtw](#T-SigStat-Common-Algorithms-Dtw 'SigStat.Common.Algorithms.Dtw')
  - [#ctor()](#M-SigStat-Common-Algorithms-Dtw-#ctor 'SigStat.Common.Algorithms.Dtw.#ctor')
  - [#ctor(distMethod)](#M-SigStat-Common-Algorithms-Dtw-#ctor-System-Func{System-Double[],System-Double[],System-Double}- 'SigStat.Common.Algorithms.Dtw.#ctor(System.Func{System.Double[],System.Double[],System.Double})')
  - [ForwardPath](#P-SigStat-Common-Algorithms-Dtw-ForwardPath 'SigStat.Common.Algorithms.Dtw.ForwardPath')
  - [Compute()](#M-SigStat-Common-Algorithms-Dtw-Compute-System-Double[][],System-Double[][]- 'SigStat.Common.Algorithms.Dtw.Compute(System.Double[][],System.Double[][])')
  - [Distance(p1,p2)](#M-SigStat-Common-Algorithms-Dtw-Distance-System-Double[],System-Double[]- 'SigStat.Common.Algorithms.Dtw.Distance(System.Double[],System.Double[])')
- [DtwClassifier](#T-SigStat-Common-PipelineItems-Classifiers-DtwClassifier 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier')
  - [#ctor()](#M-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-#ctor 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier.#ctor')
  - [#ctor(distanceMethod)](#M-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-#ctor-System-Func{System-Double[],System-Double[],System-Double}- 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier.#ctor(System.Func{System.Double[],System.Double[],System.Double})')
  - [DistanceFunction](#P-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-DistanceFunction 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier.DistanceFunction')
  - [Features](#P-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-Features 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier.Features')
  - [MultiplicationFactor](#P-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-MultiplicationFactor 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier.MultiplicationFactor')
  - [Test()](#M-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-Test-SigStat-Common-Pipeline-ISignerModel,SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier.Test(SigStat.Common.Pipeline.ISignerModel,SigStat.Common.Signature)')
  - [Train()](#M-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}- 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier.Train(System.Collections.Generic.List{SigStat.Common.Signature})')
- [DtwExperiments](#T-SigStat-Common-Algorithms-DtwExperiments 'SigStat.Common.Algorithms.DtwExperiments')
  - [ConstrainedDTw\`\`1(sequence1,sequence2,distance,w)](#M-SigStat-Common-Algorithms-DtwExperiments-ConstrainedDTw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double},System-Int32- 'SigStat.Common.Algorithms.DtwExperiments.ConstrainedDTw``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0},System.Func{``0,``0,System.Double},System.Int32)')
  - [ConstrainedDtwWikipedia\`\`1(sequence1,sequence2,distance,w)](#M-SigStat-Common-Algorithms-DtwExperiments-ConstrainedDtwWikipedia``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double},System-Int32- 'SigStat.Common.Algorithms.DtwExperiments.ConstrainedDtwWikipedia``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0},System.Func{``0,``0,System.Double},System.Int32)')
  - [ExactDTw\`\`1(sequence1,sequence2,distance)](#M-SigStat-Common-Algorithms-DtwExperiments-ExactDTw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double}- 'SigStat.Common.Algorithms.DtwExperiments.ExactDTw``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0},System.Func{``0,``0,System.Double})')
  - [ExactDtwWikipedia\`\`1(sequence1,sequence2,distance)](#M-SigStat-Common-Algorithms-DtwExperiments-ExactDtwWikipedia``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double}- 'SigStat.Common.Algorithms.DtwExperiments.ExactDtwWikipedia``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0},System.Func{``0,``0,System.Double})')
  - [OptimizedDtw\`\`1(sequence1,sequence2,distance,m,r)](#M-SigStat-Common-Algorithms-DtwExperiments-OptimizedDtw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double},System-Int32,System-Int32- 'SigStat.Common.Algorithms.DtwExperiments.OptimizedDtw``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0},System.Func{``0,``0,System.Double},System.Int32,System.Int32)')
- [DtwPy](#T-SigStat-Common-Algorithms-DtwPy 'SigStat.Common.Algorithms.DtwPy')
  - [Dtw\`\`1(sequence1,sequence2,distance)](#M-SigStat-Common-Algorithms-DtwPy-Dtw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double}- 'SigStat.Common.Algorithms.DtwPy.Dtw``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0},System.Func{``0,``0,System.Double})')
  - [EuclideanDistance(vector1,vector2)](#M-SigStat-Common-Algorithms-DtwPy-EuclideanDistance-System-Double[],System-Double[]- 'SigStat.Common.Algorithms.DtwPy.EuclideanDistance(System.Double[],System.Double[])')
- [DtwPyWindow](#T-SigStat-Common-Algorithms-DtwPyWindow 'SigStat.Common.Algorithms.DtwPyWindow')
  - [Dtw\`\`1(sequence1,sequence2,distance)](#M-SigStat-Common-Algorithms-DtwPyWindow-Dtw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double},System-Int32- 'SigStat.Common.Algorithms.DtwPyWindow.Dtw``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0},System.Func{``0,``0,System.Double},System.Int32)')
  - [EuclideanDistance(vector1,vector2)](#M-SigStat-Common-Algorithms-DtwPyWindow-EuclideanDistance-System-Double[],System-Double[]- 'SigStat.Common.Algorithms.DtwPyWindow.EuclideanDistance(System.Double[],System.Double[])')
- [DtwSignerModel](#T-SigStat-Common-PipelineItems-Classifiers-DtwSignerModel 'SigStat.Common.PipelineItems.Classifiers.DtwSignerModel')
  - [DistanceMatrix](#F-SigStat-Common-PipelineItems-Classifiers-DtwSignerModel-DistanceMatrix 'SigStat.Common.PipelineItems.Classifiers.DtwSignerModel.DistanceMatrix')
  - [Threshold](#F-SigStat-Common-PipelineItems-Classifiers-DtwSignerModel-Threshold 'SigStat.Common.PipelineItems.Classifiers.DtwSignerModel.Threshold')
  - [GenuineSignatures](#P-SigStat-Common-PipelineItems-Classifiers-DtwSignerModel-GenuineSignatures 'SigStat.Common.PipelineItems.Classifiers.DtwSignerModel.GenuineSignatures')
- [EndpointExtraction](#T-SigStat-Common-Transforms-EndpointExtraction 'SigStat.Common.Transforms.EndpointExtraction')
  - [OutputCrossingPoints](#P-SigStat-Common-Transforms-EndpointExtraction-OutputCrossingPoints 'SigStat.Common.Transforms.EndpointExtraction.OutputCrossingPoints')
  - [OutputEndpoints](#P-SigStat-Common-Transforms-EndpointExtraction-OutputEndpoints 'SigStat.Common.Transforms.EndpointExtraction.OutputEndpoints')
  - [Skeleton](#P-SigStat-Common-Transforms-EndpointExtraction-Skeleton 'SigStat.Common.Transforms.EndpointExtraction.Skeleton')
  - [Transform()](#M-SigStat-Common-Transforms-EndpointExtraction-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.EndpointExtraction.Transform(SigStat.Common.Signature)')
- [ErrorEventHandler](#T-SigStat-Common-Logging-CompositeLogger-ErrorEventHandler 'SigStat.Common.Logging.CompositeLogger.ErrorEventHandler')
- [ErrorRate](#T-SigStat-Common-ErrorRate 'SigStat.Common.ErrorRate')
  - [Far](#F-SigStat-Common-ErrorRate-Far 'SigStat.Common.ErrorRate.Far')
  - [Frr](#F-SigStat-Common-ErrorRate-Frr 'SigStat.Common.ErrorRate.Frr')
  - [Aer](#P-SigStat-Common-ErrorRate-Aer 'SigStat.Common.ErrorRate.Aer')
- [EvenNSampler](#T-SigStat-Common-Framework-Samplers-EvenNSampler 'SigStat.Common.Framework.Samplers.EvenNSampler')
  - [#ctor(n)](#M-SigStat-Common-Framework-Samplers-EvenNSampler-#ctor-System-Int32- 'SigStat.Common.Framework.Samplers.EvenNSampler.#ctor(System.Int32)')
  - [N](#P-SigStat-Common-Framework-Samplers-EvenNSampler-N 'SigStat.Common.Framework.Samplers.EvenNSampler.N')
- [ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor')
  - [Danger](#F-SigStat-Common-Helpers-Excel-ExcelColor-Danger 'SigStat.Common.Helpers.Excel.ExcelColor.Danger')
  - [Info](#F-SigStat-Common-Helpers-Excel-ExcelColor-Info 'SigStat.Common.Helpers.Excel.ExcelColor.Info')
  - [Primary](#F-SigStat-Common-Helpers-Excel-ExcelColor-Primary 'SigStat.Common.Helpers.Excel.ExcelColor.Primary')
  - [Secondary](#F-SigStat-Common-Helpers-Excel-ExcelColor-Secondary 'SigStat.Common.Helpers.Excel.ExcelColor.Secondary')
  - [Succes](#F-SigStat-Common-Helpers-Excel-ExcelColor-Succes 'SigStat.Common.Helpers.Excel.ExcelColor.Succes')
  - [Transparent](#F-SigStat-Common-Helpers-Excel-ExcelColor-Transparent 'SigStat.Common.Helpers.Excel.ExcelColor.Transparent')
  - [Warning](#F-SigStat-Common-Helpers-Excel-ExcelColor-Warning 'SigStat.Common.Helpers.Excel.ExcelColor.Warning')
- [ExcelHelper](#T-SigStat-Common-Helpers-ExcelHelper 'SigStat.Common.Helpers.ExcelHelper')
  - [FormatAsTable(range,color,showColumnHeader,showRowHeader)](#M-SigStat-Common-Helpers-ExcelHelper-FormatAsTable-OfficeOpenXml-ExcelRange,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-Boolean- 'SigStat.Common.Helpers.ExcelHelper.FormatAsTable(OfficeOpenXml.ExcelRange,SigStat.Common.Helpers.Excel.ExcelColor,System.Boolean,System.Boolean)')
  - [FormatAsTableWithTitle(range,title,color,showColumnHeader,showRowHeader)](#M-SigStat-Common-Helpers-ExcelHelper-FormatAsTableWithTitle-OfficeOpenXml-ExcelRange,System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-Boolean- 'SigStat.Common.Helpers.ExcelHelper.FormatAsTableWithTitle(OfficeOpenXml.ExcelRange,System.String,SigStat.Common.Helpers.Excel.ExcelColor,System.Boolean,System.Boolean)')
  - [InsertColumnChart(ws,range,col,row,name,xLabel,yLabel,serieLabels,width,height,title)](#M-SigStat-Common-Helpers-ExcelHelper-InsertColumnChart-OfficeOpenXml-ExcelWorksheet,OfficeOpenXml-ExcelRange,System-Int32,System-Int32,System-String,System-String,System-String,OfficeOpenXml-ExcelRange,System-Int32,System-Int32,System-String- 'SigStat.Common.Helpers.ExcelHelper.InsertColumnChart(OfficeOpenXml.ExcelWorksheet,OfficeOpenXml.ExcelRange,System.Int32,System.Int32,System.String,System.String,System.String,OfficeOpenXml.ExcelRange,System.Int32,System.Int32,System.String)')
  - [InsertDictionary\`\`2(ws,col,row,data,title,color,Name)](#M-SigStat-Common-Helpers-ExcelHelper-InsertDictionary``2-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-Collections-Generic-IEnumerable{System-Collections-Generic-KeyValuePair{``0,``1}},System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-String- 'SigStat.Common.Helpers.ExcelHelper.InsertDictionary``2(OfficeOpenXml.ExcelWorksheet,System.Int32,System.Int32,System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{``0,``1}},System.String,SigStat.Common.Helpers.Excel.ExcelColor,System.String)')
  - [InsertHierarchicalList(ws,col,row,root,title,color)](#M-SigStat-Common-Helpers-ExcelHelper-InsertHierarchicalList-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,SigStat-Common-Helpers-HierarchyElement,System-String,SigStat-Common-Helpers-Excel-ExcelColor- 'SigStat.Common.Helpers.ExcelHelper.InsertHierarchicalList(OfficeOpenXml.ExcelWorksheet,System.Int32,System.Int32,SigStat.Common.Helpers.HierarchyElement,System.String,SigStat.Common.Helpers.Excel.ExcelColor)')
  - [InsertLegend(range,text,title,color)](#M-SigStat-Common-Helpers-ExcelHelper-InsertLegend-OfficeOpenXml-ExcelRange,System-String,System-String,SigStat-Common-Helpers-Excel-ExcelColor- 'SigStat.Common.Helpers.ExcelHelper.InsertLegend(OfficeOpenXml.ExcelRange,System.String,System.String,SigStat.Common.Helpers.Excel.ExcelColor)')
  - [InsertLineChart(ws,range,col,row,name,xLabel,yLabel,SerieLabels,width,height,title)](#M-SigStat-Common-Helpers-ExcelHelper-InsertLineChart-OfficeOpenXml-ExcelWorksheet,OfficeOpenXml-ExcelRange,System-Int32,System-Int32,System-String,System-String,System-String,OfficeOpenXml-ExcelRange,System-Int32,System-Int32,System-String- 'SigStat.Common.Helpers.ExcelHelper.InsertLineChart(OfficeOpenXml.ExcelWorksheet,OfficeOpenXml.ExcelRange,System.Int32,System.Int32,System.String,System.String,System.String,OfficeOpenXml.ExcelRange,System.Int32,System.Int32,System.String)')
  - [InsertLink(range,sheet)](#M-SigStat-Common-Helpers-ExcelHelper-InsertLink-OfficeOpenXml-ExcelRange,System-String- 'SigStat.Common.Helpers.ExcelHelper.InsertLink(OfficeOpenXml.ExcelRange,System.String)')
  - [InsertLink(range,sheet,cells)](#M-SigStat-Common-Helpers-ExcelHelper-InsertLink-OfficeOpenXml-ExcelRange,System-String,System-String- 'SigStat.Common.Helpers.ExcelHelper.InsertLink(OfficeOpenXml.ExcelRange,System.String,System.String)')
  - [InsertTable(ws,col,row,data,title,color,hasRowHeader,hasColumnHeader,name)](#M-SigStat-Common-Helpers-ExcelHelper-InsertTable-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-Object[0-,0-],System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-Boolean,System-String- 'SigStat.Common.Helpers.ExcelHelper.InsertTable(OfficeOpenXml.ExcelWorksheet,System.Int32,System.Int32,System.Object[0:,0:],System.String,SigStat.Common.Helpers.Excel.ExcelColor,System.Boolean,System.Boolean,System.String)')
  - [InsertTable(ws,col,row,data,title,color,hasRowHeader,hasColumnHeader,name)](#M-SigStat-Common-Helpers-ExcelHelper-InsertTable-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-Double[0-,0-],System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-Boolean,System-String- 'SigStat.Common.Helpers.ExcelHelper.InsertTable(OfficeOpenXml.ExcelWorksheet,System.Int32,System.Int32,System.Double[0:,0:],System.String,SigStat.Common.Helpers.Excel.ExcelColor,System.Boolean,System.Boolean,System.String)')
  - [InsertTable\`\`1(ws,col,row,data,title,color,showHeader,Name)](#M-SigStat-Common-Helpers-ExcelHelper-InsertTable``1-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-Collections-Generic-IEnumerable{``0},System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-String- 'SigStat.Common.Helpers.ExcelHelper.InsertTable``1(OfficeOpenXml.ExcelWorksheet,System.Int32,System.Int32,System.Collections.Generic.IEnumerable{``0},System.String,SigStat.Common.Helpers.Excel.ExcelColor,System.Boolean,System.String)')
  - [InsertText(ws,row,col,text,level)](#M-SigStat-Common-Helpers-ExcelHelper-InsertText-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-String,SigStat-Common-Helpers-Excel-TextLevel- 'SigStat.Common.Helpers.ExcelHelper.InsertText(OfficeOpenXml.ExcelWorksheet,System.Int32,System.Int32,System.String,SigStat.Common.Helpers.Excel.TextLevel)')
  - [Merge(range)](#M-SigStat-Common-Helpers-ExcelHelper-Merge-OfficeOpenXml-ExcelRangeBase- 'SigStat.Common.Helpers.ExcelHelper.Merge(OfficeOpenXml.ExcelRangeBase)')
- [ExcelReportGenerator](#T-SigStat-Common-Logging-ExcelReportGenerator 'SigStat.Common.Logging.ExcelReportGenerator')
  - [GenerateReport(model,fileName)](#M-SigStat-Common-Logging-ExcelReportGenerator-GenerateReport-SigStat-Common-Logging-BenchmarkLogModel,System-String- 'SigStat.Common.Logging.ExcelReportGenerator.GenerateReport(SigStat.Common.Logging.BenchmarkLogModel,System.String)')
- [Extrema](#T-SigStat-Common-Transforms-Extrema 'SigStat.Common.Transforms.Extrema')
  - [Transform()](#M-SigStat-Common-Transforms-Extrema-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.Extrema.Transform(SigStat.Common.Signature)')
- [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor')
  - [#ctor(name,key,featureType)](#M-SigStat-Common-FeatureDescriptor-#ctor-System-String,System-String,System-Type- 'SigStat.Common.FeatureDescriptor.#ctor(System.String,System.String,System.Type)')
  - [descriptors](#F-SigStat-Common-FeatureDescriptor-descriptors 'SigStat.Common.FeatureDescriptor.descriptors')
  - [FeatureType](#P-SigStat-Common-FeatureDescriptor-FeatureType 'SigStat.Common.FeatureDescriptor.FeatureType')
  - [IsCollection](#P-SigStat-Common-FeatureDescriptor-IsCollection 'SigStat.Common.FeatureDescriptor.IsCollection')
  - [Key](#P-SigStat-Common-FeatureDescriptor-Key 'SigStat.Common.FeatureDescriptor.Key')
  - [Name](#P-SigStat-Common-FeatureDescriptor-Name 'SigStat.Common.FeatureDescriptor.Name')
  - [Get(key)](#M-SigStat-Common-FeatureDescriptor-Get-System-String- 'SigStat.Common.FeatureDescriptor.Get(System.String)')
  - [GetAll()](#M-SigStat-Common-FeatureDescriptor-GetAll 'SigStat.Common.FeatureDescriptor.GetAll')
  - [Get\`\`1(key)](#M-SigStat-Common-FeatureDescriptor-Get``1-System-String- 'SigStat.Common.FeatureDescriptor.Get``1(System.String)')
  - [IsRegistered(featureKey)](#M-SigStat-Common-FeatureDescriptor-IsRegistered-System-String- 'SigStat.Common.FeatureDescriptor.IsRegistered(System.String)')
  - [Register(featureKey,type)](#M-SigStat-Common-FeatureDescriptor-Register-System-String,System-Type- 'SigStat.Common.FeatureDescriptor.Register(System.String,System.Type)')
  - [ToString()](#M-SigStat-Common-FeatureDescriptor-ToString 'SigStat.Common.FeatureDescriptor.ToString')
- [FeatureDescriptorDictionaryConverter](#T-SigStat-Common-Helpers-Serialization-FeatureDescriptorDictionaryConverter 'SigStat.Common.Helpers.Serialization.FeatureDescriptorDictionaryConverter')
  - [CanConvert(objectType)](#M-SigStat-Common-Helpers-Serialization-FeatureDescriptorDictionaryConverter-CanConvert-System-Type- 'SigStat.Common.Helpers.Serialization.FeatureDescriptorDictionaryConverter.CanConvert(System.Type)')
  - [ReadJson()](#M-SigStat-Common-Helpers-Serialization-FeatureDescriptorDictionaryConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.FeatureDescriptorDictionaryConverter.ReadJson(Newtonsoft.Json.JsonReader,System.Type,System.Object,Newtonsoft.Json.JsonSerializer)')
  - [WriteJson()](#M-SigStat-Common-Helpers-Serialization-FeatureDescriptorDictionaryConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.FeatureDescriptorDictionaryConverter.WriteJson(Newtonsoft.Json.JsonWriter,System.Object,Newtonsoft.Json.JsonSerializer)')
- [FeatureDescriptorJsonConverter](#T-SigStat-Common-Helpers-FeatureDescriptorJsonConverter 'SigStat.Common.Helpers.FeatureDescriptorJsonConverter')
  - [CanConvert(objectType)](#M-SigStat-Common-Helpers-FeatureDescriptorJsonConverter-CanConvert-System-Type- 'SigStat.Common.Helpers.FeatureDescriptorJsonConverter.CanConvert(System.Type)')
  - [ReadJson()](#M-SigStat-Common-Helpers-FeatureDescriptorJsonConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.FeatureDescriptorJsonConverter.ReadJson(Newtonsoft.Json.JsonReader,System.Type,System.Object,Newtonsoft.Json.JsonSerializer)')
  - [WriteJson()](#M-SigStat-Common-Helpers-FeatureDescriptorJsonConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.FeatureDescriptorJsonConverter.WriteJson(Newtonsoft.Json.JsonWriter,System.Object,Newtonsoft.Json.JsonSerializer)')
- [FeatureDescriptorListJsonConverter](#T-SigStat-Common-Helpers-Serialization-FeatureDescriptorListJsonConverter 'SigStat.Common.Helpers.Serialization.FeatureDescriptorListJsonConverter')
  - [CanConvert(objectType)](#M-SigStat-Common-Helpers-Serialization-FeatureDescriptorListJsonConverter-CanConvert-System-Type- 'SigStat.Common.Helpers.Serialization.FeatureDescriptorListJsonConverter.CanConvert(System.Type)')
  - [ReadJson()](#M-SigStat-Common-Helpers-Serialization-FeatureDescriptorListJsonConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.FeatureDescriptorListJsonConverter.ReadJson(Newtonsoft.Json.JsonReader,System.Type,System.Object,Newtonsoft.Json.JsonSerializer)')
  - [WriteJson()](#M-SigStat-Common-Helpers-Serialization-FeatureDescriptorListJsonConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.FeatureDescriptorListJsonConverter.WriteJson(Newtonsoft.Json.JsonWriter,System.Object,Newtonsoft.Json.JsonSerializer)')
- [FeatureDescriptorTJsonConverter](#T-SigStat-Common-Helpers-FeatureDescriptorTJsonConverter 'SigStat.Common.Helpers.FeatureDescriptorTJsonConverter')
  - [CanConvert(objectType)](#M-SigStat-Common-Helpers-FeatureDescriptorTJsonConverter-CanConvert-System-Type- 'SigStat.Common.Helpers.FeatureDescriptorTJsonConverter.CanConvert(System.Type)')
  - [ReadJson()](#M-SigStat-Common-Helpers-FeatureDescriptorTJsonConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.FeatureDescriptorTJsonConverter.ReadJson(Newtonsoft.Json.JsonReader,System.Type,System.Object,Newtonsoft.Json.JsonSerializer)')
  - [WriteJson()](#M-SigStat-Common-Helpers-FeatureDescriptorTJsonConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.FeatureDescriptorTJsonConverter.WriteJson(Newtonsoft.Json.JsonWriter,System.Object,Newtonsoft.Json.JsonSerializer)')
- [FeatureDescriptor\`1](#T-SigStat-Common-FeatureDescriptor`1 'SigStat.Common.FeatureDescriptor`1')
  - [Get()](#M-SigStat-Common-FeatureDescriptor`1-Get-System-String- 'SigStat.Common.FeatureDescriptor`1.Get(System.String)')
- [FeatureStreamingContextState](#T-SigStat-Common-Helpers-Serialization-FeatureStreamingContextState 'SigStat.Common.Helpers.Serialization.FeatureStreamingContextState')
  - [#ctor()](#M-SigStat-Common-Helpers-Serialization-FeatureStreamingContextState-#ctor-System-Boolean- 'SigStat.Common.Helpers.Serialization.FeatureStreamingContextState.#ctor(System.Boolean)')
  - [KnownFeatureKeys](#P-SigStat-Common-Helpers-Serialization-FeatureStreamingContextState-KnownFeatureKeys 'SigStat.Common.Helpers.Serialization.FeatureStreamingContextState.KnownFeatureKeys')
- [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')
  - [All](#F-SigStat-Common-Features-All 'SigStat.Common.Features.All')
  - [Altitude](#F-SigStat-Common-Features-Altitude 'SigStat.Common.Features.Altitude')
  - [Azimuth](#F-SigStat-Common-Features-Azimuth 'SigStat.Common.Features.Azimuth')
  - [Cog](#F-SigStat-Common-Features-Cog 'SigStat.Common.Features.Cog')
  - [Dpi](#F-SigStat-Common-Features-Dpi 'SigStat.Common.Features.Dpi')
  - [Image](#F-SigStat-Common-Features-Image 'SigStat.Common.Features.Image')
  - [PenDown](#F-SigStat-Common-Features-PenDown 'SigStat.Common.Features.PenDown')
  - [Pressure](#F-SigStat-Common-Features-Pressure 'SigStat.Common.Features.Pressure')
  - [Size](#F-SigStat-Common-Features-Size 'SigStat.Common.Features.Size')
  - [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T')
  - [TrimmedBounds](#F-SigStat-Common-Features-TrimmedBounds 'SigStat.Common.Features.TrimmedBounds')
  - [X](#F-SigStat-Common-Features-X 'SigStat.Common.Features.X')
  - [Y](#F-SigStat-Common-Features-Y 'SigStat.Common.Features.Y')
- [FillPenUpDurations](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations')
  - [InputFeatures](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-InputFeatures 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.InputFeatures')
  - [InterpolationType](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-InterpolationType 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.InterpolationType')
  - [OutputFeatures](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-OutputFeatures 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.OutputFeatures')
  - [PressureInputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-PressureInputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.PressureInputFeature')
  - [PressureOutputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-PressureOutputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.PressureOutputFeature')
  - [TimeInputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeInputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.TimeInputFeature')
  - [TimeOutputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeOutputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.TimeOutputFeature')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.Transform(SigStat.Common.Signature)')
- [FilterPoints](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints')
  - [InputFeatures](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-InputFeatures 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints.InputFeatures')
  - [KeyFeatureInput](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-KeyFeatureInput 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints.KeyFeatureInput')
  - [KeyFeatureOutput](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-KeyFeatureOutput 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints.KeyFeatureOutput')
  - [OutputFeatures](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-OutputFeatures 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints.OutputFeatures')
  - [Percentile](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-Percentile 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints.Percentile')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints.Transform(SigStat.Common.Signature)')
- [FirstNSampler](#T-SigStat-Common-Framework-Samplers-FirstNSampler 'SigStat.Common.Framework.Samplers.FirstNSampler')
  - [#ctor(n)](#M-SigStat-Common-Framework-Samplers-FirstNSampler-#ctor-System-Int32- 'SigStat.Common.Framework.Samplers.FirstNSampler.#ctor(System.Int32)')
  - [N](#P-SigStat-Common-Framework-Samplers-FirstNSampler-N 'SigStat.Common.Framework.Samplers.FirstNSampler.N')
- [ForegroundType](#T-SigStat-Common-Transforms-Binarization-ForegroundType 'SigStat.Common.Transforms.Binarization.ForegroundType')
  - [Bright](#F-SigStat-Common-Transforms-Binarization-ForegroundType-Bright 'SigStat.Common.Transforms.Binarization.ForegroundType.Bright')
  - [Dark](#F-SigStat-Common-Transforms-Binarization-ForegroundType-Dark 'SigStat.Common.Transforms.Binarization.ForegroundType.Dark')
- [HSCPThinning](#T-SigStat-Common-Transforms-HSCPThinning 'SigStat.Common.Transforms.HSCPThinning')
  - [Input](#P-SigStat-Common-Transforms-HSCPThinning-Input 'SigStat.Common.Transforms.HSCPThinning.Input')
  - [Output](#P-SigStat-Common-Transforms-HSCPThinning-Output 'SigStat.Common.Transforms.HSCPThinning.Output')
  - [Transform()](#M-SigStat-Common-Transforms-HSCPThinning-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.HSCPThinning.Transform(SigStat.Common.Signature)')
- [HSCPThinningStep](#T-SigStat-Common-Algorithms-HSCPThinningStep 'SigStat.Common.Algorithms.HSCPThinningStep')
  - [ResultChanged](#P-SigStat-Common-Algorithms-HSCPThinningStep-ResultChanged 'SigStat.Common.Algorithms.HSCPThinningStep.ResultChanged')
  - [Neighbourhood()](#M-SigStat-Common-Algorithms-HSCPThinningStep-Neighbourhood-System-Boolean[0-,0-],System-Int32,System-Int32- 'SigStat.Common.Algorithms.HSCPThinningStep.Neighbourhood(System.Boolean[0:,0:],System.Int32,System.Int32)')
  - [Scan(b)](#M-SigStat-Common-Algorithms-HSCPThinningStep-Scan-System-Boolean[0-,0-]- 'SigStat.Common.Algorithms.HSCPThinningStep.Scan(System.Boolean[0:,0:])')
- [HierarchyElement](#T-SigStat-Common-Helpers-HierarchyElement 'SigStat.Common.Helpers.HierarchyElement')
  - [#ctor()](#M-SigStat-Common-Helpers-HierarchyElement-#ctor 'SigStat.Common.Helpers.HierarchyElement.#ctor')
  - [#ctor(Content)](#M-SigStat-Common-Helpers-HierarchyElement-#ctor-System-Object- 'SigStat.Common.Helpers.HierarchyElement.#ctor(System.Object)')
  - [Children](#P-SigStat-Common-Helpers-HierarchyElement-Children 'SigStat.Common.Helpers.HierarchyElement.Children')
  - [Content](#P-SigStat-Common-Helpers-HierarchyElement-Content 'SigStat.Common.Helpers.HierarchyElement.Content')
  - [Add()](#M-SigStat-Common-Helpers-HierarchyElement-Add-SigStat-Common-Helpers-HierarchyElement- 'SigStat.Common.Helpers.HierarchyElement.Add(SigStat.Common.Helpers.HierarchyElement)')
  - [GetCount()](#M-SigStat-Common-Helpers-HierarchyElement-GetCount 'SigStat.Common.Helpers.HierarchyElement.GetCount')
  - [GetDepth()](#M-SigStat-Common-Helpers-HierarchyElement-GetDepth 'SigStat.Common.Helpers.HierarchyElement.GetDepth')
  - [GetEnumerator()](#M-SigStat-Common-Helpers-HierarchyElement-GetEnumerator 'SigStat.Common.Helpers.HierarchyElement.GetEnumerator')
  - [ToString()](#M-SigStat-Common-Helpers-HierarchyElement-ToString 'SigStat.Common.Helpers.HierarchyElement.ToString')
- [IClassifier](#T-SigStat-Common-Pipeline-IClassifier 'SigStat.Common.Pipeline.IClassifier')
  - [Test(signature,model)](#M-SigStat-Common-Pipeline-IClassifier-Test-SigStat-Common-Pipeline-ISignerModel,SigStat-Common-Signature- 'SigStat.Common.Pipeline.IClassifier.Test(SigStat.Common.Pipeline.ISignerModel,SigStat.Common.Signature)')
  - [Train(signatures)](#M-SigStat-Common-Pipeline-IClassifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}- 'SigStat.Common.Pipeline.IClassifier.Train(System.Collections.Generic.List{SigStat.Common.Signature})')
- [IDataSetLoader](#T-SigStat-Common-Loaders-IDataSetLoader 'SigStat.Common.Loaders.IDataSetLoader')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-IDataSetLoader-EnumerateSigners 'SigStat.Common.Loaders.IDataSetLoader.EnumerateSigners')
  - [EnumerateSigners(signerFilter)](#M-SigStat-Common-Loaders-IDataSetLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.IDataSetLoader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
- [IDistanceClassifier](#T-SigStat-Common-Pipeline-IDistanceClassifier 'SigStat.Common.Pipeline.IDistanceClassifier')
  - [DistanceFunction](#P-SigStat-Common-Pipeline-IDistanceClassifier-DistanceFunction 'SigStat.Common.Pipeline.IDistanceClassifier.DistanceFunction')
- [IInterpolation](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation 'SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation')
  - [FeatureValues](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation-FeatureValues 'SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation.FeatureValues')
  - [TimeValues](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation-TimeValues 'SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation.TimeValues')
  - [GetValue(timestamp)](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation-GetValue-System-Double- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation.GetValue(System.Double)')
- [ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject')
  - [Logger](#P-SigStat-Common-ILoggerObject-Logger 'SigStat.Common.ILoggerObject.Logger')
- [ILoggerObjectExtensions](#T-SigStat-Common-ILoggerObjectExtensions 'SigStat.Common.ILoggerObjectExtensions')
  - [LogCritical(obj,message,args)](#M-SigStat-Common-ILoggerObjectExtensions-LogCritical-SigStat-Common-ILoggerObject,System-String,System-Object[]- 'SigStat.Common.ILoggerObjectExtensions.LogCritical(SigStat.Common.ILoggerObject,System.String,System.Object[])')
  - [LogDebug(obj,message,args)](#M-SigStat-Common-ILoggerObjectExtensions-LogDebug-SigStat-Common-ILoggerObject,System-String,System-Object[]- 'SigStat.Common.ILoggerObjectExtensions.LogDebug(SigStat.Common.ILoggerObject,System.String,System.Object[])')
  - [LogError(obj,message,args)](#M-SigStat-Common-ILoggerObjectExtensions-LogError-SigStat-Common-ILoggerObject,System-String,System-Object[]- 'SigStat.Common.ILoggerObjectExtensions.LogError(SigStat.Common.ILoggerObject,System.String,System.Object[])')
  - [LogError(obj,exception,message,args)](#M-SigStat-Common-ILoggerObjectExtensions-LogError-SigStat-Common-ILoggerObject,System-Exception,System-String,System-Object[]- 'SigStat.Common.ILoggerObjectExtensions.LogError(SigStat.Common.ILoggerObject,System.Exception,System.String,System.Object[])')
  - [LogInformation(obj,message,args)](#M-SigStat-Common-ILoggerObjectExtensions-LogInformation-SigStat-Common-ILoggerObject,System-String,System-Object[]- 'SigStat.Common.ILoggerObjectExtensions.LogInformation(SigStat.Common.ILoggerObject,System.String,System.Object[])')
  - [LogTrace(obj,message,args)](#M-SigStat-Common-ILoggerObjectExtensions-LogTrace-SigStat-Common-ILoggerObject,System-String,System-Object[]- 'SigStat.Common.ILoggerObjectExtensions.LogTrace(SigStat.Common.ILoggerObject,System.String,System.Object[])')
  - [LogTrace\`\`1(obj,state,eventId,exception,formatter)](#M-SigStat-Common-ILoggerObjectExtensions-LogTrace``1-SigStat-Common-ILoggerObject,``0,Microsoft-Extensions-Logging-EventId,System-Exception,System-Func{``0,System-Exception,System-String}- 'SigStat.Common.ILoggerObjectExtensions.LogTrace``1(SigStat.Common.ILoggerObject,``0,Microsoft.Extensions.Logging.EventId,System.Exception,System.Func{``0,System.Exception,System.String})')
  - [LogWarning(obj,message,args)](#M-SigStat-Common-ILoggerObjectExtensions-LogWarning-SigStat-Common-ILoggerObject,System-String,System-Object[]- 'SigStat.Common.ILoggerObjectExtensions.LogWarning(SigStat.Common.ILoggerObject,System.String,System.Object[])')
  - [LogWarning(obj,exception,message,args)](#M-SigStat-Common-ILoggerObjectExtensions-LogWarning-SigStat-Common-ILoggerObject,System-Exception,System-String,System-Object[]- 'SigStat.Common.ILoggerObjectExtensions.LogWarning(SigStat.Common.ILoggerObject,System.Exception,System.String,System.Object[])')
- [IOExtensions](#T-SigStat-Common-IOExtensions 'SigStat.Common.IOExtensions')
  - [GetPath(path)](#M-SigStat-Common-IOExtensions-GetPath-System-String- 'SigStat.Common.IOExtensions.GetPath(System.String)')
- [IPipelineIO](#T-SigStat-Common-Pipeline-IPipelineIO 'SigStat.Common.Pipeline.IPipelineIO')
  - [PipelineInputs](#P-SigStat-Common-Pipeline-IPipelineIO-PipelineInputs 'SigStat.Common.Pipeline.IPipelineIO.PipelineInputs')
  - [PipelineOutputs](#P-SigStat-Common-Pipeline-IPipelineIO-PipelineOutputs 'SigStat.Common.Pipeline.IPipelineIO.PipelineOutputs')
- [IProgress](#T-SigStat-Common-Helpers-IProgress 'SigStat.Common.Helpers.IProgress')
  - [Progress](#P-SigStat-Common-Helpers-IProgress-Progress 'SigStat.Common.Helpers.IProgress.Progress')
- [ISignerModel](#T-SigStat-Common-Pipeline-ISignerModel 'SigStat.Common.Pipeline.ISignerModel')
- [ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')
  - [Transform(signature)](#M-SigStat-Common-ITransformation-Transform-SigStat-Common-Signature- 'SigStat.Common.ITransformation.Transform(SigStat.Common.Signature)')
- [ImageGenerator](#T-SigStat-Common-Transforms-ImageGenerator 'SigStat.Common.Transforms.ImageGenerator')
  - [#ctor()](#M-SigStat-Common-Transforms-ImageGenerator-#ctor 'SigStat.Common.Transforms.ImageGenerator.#ctor')
  - [#ctor(writeToFile)](#M-SigStat-Common-Transforms-ImageGenerator-#ctor-System-Boolean- 'SigStat.Common.Transforms.ImageGenerator.#ctor(System.Boolean)')
  - [#ctor(writeToFile,foregroundColor,backgroundColor)](#M-SigStat-Common-Transforms-ImageGenerator-#ctor-System-Boolean,SixLabors-ImageSharp-PixelFormats-Rgba32,SixLabors-ImageSharp-PixelFormats-Rgba32- 'SigStat.Common.Transforms.ImageGenerator.#ctor(System.Boolean,SixLabors.ImageSharp.PixelFormats.Rgba32,SixLabors.ImageSharp.PixelFormats.Rgba32)')
  - [BackgroundColor](#P-SigStat-Common-Transforms-ImageGenerator-BackgroundColor 'SigStat.Common.Transforms.ImageGenerator.BackgroundColor')
  - [ForegroundColor](#P-SigStat-Common-Transforms-ImageGenerator-ForegroundColor 'SigStat.Common.Transforms.ImageGenerator.ForegroundColor')
  - [Input](#P-SigStat-Common-Transforms-ImageGenerator-Input 'SigStat.Common.Transforms.ImageGenerator.Input')
  - [OutputImage](#P-SigStat-Common-Transforms-ImageGenerator-OutputImage 'SigStat.Common.Transforms.ImageGenerator.OutputImage')
  - [WriteToFile](#P-SigStat-Common-Transforms-ImageGenerator-WriteToFile 'SigStat.Common.Transforms.ImageGenerator.WriteToFile')
  - [Transform()](#M-SigStat-Common-Transforms-ImageGenerator-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.ImageGenerator.Transform(SigStat.Common.Signature)')
- [ImageLoader](#T-SigStat-Common-Loaders-ImageLoader 'SigStat.Common.Loaders.ImageLoader')
  - [#ctor(databasePath)](#M-SigStat-Common-Loaders-ImageLoader-#ctor-System-String- 'SigStat.Common.Loaders.ImageLoader.#ctor(System.String)')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-ImageLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.ImageLoader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
  - [LoadImage(signature,file)](#M-SigStat-Common-Loaders-ImageLoader-LoadImage-SigStat-Common-Signature,System-String- 'SigStat.Common.Loaders.ImageLoader.LoadImage(SigStat.Common.Signature,System.String)')
  - [LoadSignature(file)](#M-SigStat-Common-Loaders-ImageLoader-LoadSignature-System-String- 'SigStat.Common.Loaders.ImageLoader.LoadSignature(System.String)')
- [ImageSaver](#T-SigStat-Common-Loaders-ImageSaver 'SigStat.Common.Loaders.ImageSaver')
  - [Save(signature,path)](#M-SigStat-Common-Loaders-ImageSaver-Save-SigStat-Common-Signature,System-String- 'SigStat.Common.Loaders.ImageSaver.Save(SigStat.Common.Signature,System.String)')
- [Input](#T-SigStat-Common-Pipeline-Input 'SigStat.Common.Pipeline.Input')
  - [#ctor(AutoSetMode)](#M-SigStat-Common-Pipeline-Input-#ctor-SigStat-Common-Pipeline-AutoSetMode- 'SigStat.Common.Pipeline.Input.#ctor(SigStat.Common.Pipeline.AutoSetMode)')
  - [AutoSetMode](#F-SigStat-Common-Pipeline-Input-AutoSetMode 'SigStat.Common.Pipeline.Input.AutoSetMode')
- [KeyValueGroup](#T-SigStat-Common-Logging-KeyValueGroup 'SigStat.Common.Logging.KeyValueGroup')
  - [#ctor(name)](#M-SigStat-Common-Logging-KeyValueGroup-#ctor-System-String- 'SigStat.Common.Logging.KeyValueGroup.#ctor(System.String)')
  - [Items](#P-SigStat-Common-Logging-KeyValueGroup-Items 'SigStat.Common.Logging.KeyValueGroup.Items')
  - [Name](#P-SigStat-Common-Logging-KeyValueGroup-Name 'SigStat.Common.Logging.KeyValueGroup.Name')
- [KeyValueGroupConverter](#T-SigStat-Common-Helpers-Serialization-KeyValueGroupConverter 'SigStat.Common.Helpers.Serialization.KeyValueGroupConverter')
  - [ReadJson()](#M-SigStat-Common-Helpers-Serialization-KeyValueGroupConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,SigStat-Common-Logging-KeyValueGroup,System-Boolean,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.KeyValueGroupConverter.ReadJson(Newtonsoft.Json.JsonReader,System.Type,SigStat.Common.Logging.KeyValueGroup,System.Boolean,Newtonsoft.Json.JsonSerializer)')
  - [WriteJson()](#M-SigStat-Common-Helpers-Serialization-KeyValueGroupConverter-WriteJson-Newtonsoft-Json-JsonWriter,SigStat-Common-Logging-KeyValueGroup,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.KeyValueGroupConverter.WriteJson(Newtonsoft.Json.JsonWriter,SigStat.Common.Logging.KeyValueGroup,Newtonsoft.Json.JsonSerializer)')
- [LastNSampler](#T-SigStat-Common-Framework-Samplers-LastNSampler 'SigStat.Common.Framework.Samplers.LastNSampler')
  - [#ctor(n)](#M-SigStat-Common-Framework-Samplers-LastNSampler-#ctor-System-Int32- 'SigStat.Common.Framework.Samplers.LastNSampler.#ctor(System.Int32)')
  - [N](#P-SigStat-Common-Framework-Samplers-LastNSampler-N 'SigStat.Common.Framework.Samplers.LastNSampler.N')
- [Level](#T-SigStat-Common-Helpers-Excel-Level 'SigStat.Common.Helpers.Excel.Level')
  - [StyleAs(style,level)](#M-SigStat-Common-Helpers-Excel-Level-StyleAs-OfficeOpenXml-Style-ExcelStyle,SigStat-Common-Helpers-Excel-TextLevel- 'SigStat.Common.Helpers.Excel.Level.StyleAs(OfficeOpenXml.Style.ExcelStyle,SigStat.Common.Helpers.Excel.TextLevel)')
- [LinearInterpolation](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-LinearInterpolation 'SigStat.Common.PipelineItems.Transforms.Preprocessing.LinearInterpolation')
  - [FeatureValues](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-LinearInterpolation-FeatureValues 'SigStat.Common.PipelineItems.Transforms.Preprocessing.LinearInterpolation.FeatureValues')
  - [TimeValues](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-LinearInterpolation-TimeValues 'SigStat.Common.PipelineItems.Transforms.Preprocessing.LinearInterpolation.TimeValues')
  - [GetValue(timestamp)](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-LinearInterpolation-GetValue-System-Double- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.LinearInterpolation.GetValue(System.Double)')
- [LogAnalyzer](#T-SigStat-Common-Logging-LogAnalyzer 'SigStat.Common.Logging.LogAnalyzer')
  - [GetBenchmarkLogModel(logs)](#M-SigStat-Common-Logging-LogAnalyzer-GetBenchmarkLogModel-System-Collections-Generic-IEnumerable{SigStat-Common-Logging-SigStatLogState}- 'SigStat.Common.Logging.LogAnalyzer.GetBenchmarkLogModel(System.Collections.Generic.IEnumerable{SigStat.Common.Logging.SigStatLogState})')
- [LogStateLoggedEventHandler](#T-SigStat-Common-Logging-ReportInformationLogger-LogStateLoggedEventHandler 'SigStat.Common.Logging.ReportInformationLogger.LogStateLoggedEventHandler')
- [Loop](#T-SigStat-Common-Loop 'SigStat.Common.Loop')
  - [#ctor()](#M-SigStat-Common-Loop-#ctor 'SigStat.Common.Loop.#ctor')
  - [#ctor(centerX,centerY)](#M-SigStat-Common-Loop-#ctor-System-Single,System-Single- 'SigStat.Common.Loop.#ctor(System.Single,System.Single)')
  - [Bounds](#P-SigStat-Common-Loop-Bounds 'SigStat.Common.Loop.Bounds')
  - [Center](#P-SigStat-Common-Loop-Center 'SigStat.Common.Loop.Center')
  - [Points](#P-SigStat-Common-Loop-Points 'SigStat.Common.Loop.Points')
  - [ToString()](#M-SigStat-Common-Loop-ToString 'SigStat.Common.Loop.ToString')
- [MCYT](#T-SigStat-Common-Loaders-MCYTLoader-MCYT 'SigStat.Common.Loaders.MCYTLoader.MCYT')
  - [Altitude](#F-SigStat-Common-Loaders-MCYTLoader-MCYT-Altitude 'SigStat.Common.Loaders.MCYTLoader.MCYT.Altitude')
  - [Azimuth](#F-SigStat-Common-Loaders-MCYTLoader-MCYT-Azimuth 'SigStat.Common.Loaders.MCYTLoader.MCYT.Azimuth')
  - [Pressure](#F-SigStat-Common-Loaders-MCYTLoader-MCYT-Pressure 'SigStat.Common.Loaders.MCYTLoader.MCYT.Pressure')
  - [X](#F-SigStat-Common-Loaders-MCYTLoader-MCYT-X 'SigStat.Common.Loaders.MCYTLoader.MCYT.X')
  - [Y](#F-SigStat-Common-Loaders-MCYTLoader-MCYT-Y 'SigStat.Common.Loaders.MCYTLoader.MCYT.Y')
- [MCYTLoader](#T-SigStat-Common-Loaders-MCYTLoader 'SigStat.Common.Loaders.MCYTLoader')
  - [#ctor(databasePath,standardFeatures)](#M-SigStat-Common-Loaders-MCYTLoader-#ctor-System-String,System-Boolean- 'SigStat.Common.Loaders.MCYTLoader.#ctor(System.String,System.Boolean)')
  - [DatabasePath](#P-SigStat-Common-Loaders-MCYTLoader-DatabasePath 'SigStat.Common.Loaders.MCYTLoader.DatabasePath')
  - [SamplingFrequency](#P-SigStat-Common-Loaders-MCYTLoader-SamplingFrequency 'SigStat.Common.Loaders.MCYTLoader.SamplingFrequency')
  - [StandardFeatures](#P-SigStat-Common-Loaders-MCYTLoader-StandardFeatures 'SigStat.Common.Loaders.MCYTLoader.StandardFeatures')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-MCYTLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.MCYTLoader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
  - [LoadSignature(signature,stream,standardFeatures)](#M-SigStat-Common-Loaders-MCYTLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean- 'SigStat.Common.Loaders.MCYTLoader.LoadSignature(SigStat.Common.Signature,System.IO.MemoryStream,System.Boolean)')
- [Map](#T-SigStat-Common-Transforms-Map 'SigStat.Common.Transforms.Map')
  - [#ctor(minVal,maxVal)](#M-SigStat-Common-Transforms-Map-#ctor-System-Double,System-Double- 'SigStat.Common.Transforms.Map.#ctor(System.Double,System.Double)')
  - [Input](#P-SigStat-Common-Transforms-Map-Input 'SigStat.Common.Transforms.Map.Input')
  - [Output](#P-SigStat-Common-Transforms-Map-Output 'SigStat.Common.Transforms.Map.Output')
  - [Transform()](#M-SigStat-Common-Transforms-Map-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.Map.Transform(SigStat.Common.Signature)')
- [MathHelper](#T-SigStat-Common-MathHelper 'SigStat.Common.MathHelper')
  - [Median(values)](#M-SigStat-Common-MathHelper-Median-System-Collections-Generic-IEnumerable{System-Double}- 'SigStat.Common.MathHelper.Median(System.Collections.Generic.IEnumerable{System.Double})')
  - [Min(d1,d2,d3)](#M-SigStat-Common-MathHelper-Min-System-Double,System-Double,System-Double- 'SigStat.Common.MathHelper.Min(System.Double,System.Double,System.Double)')
  - [StdDiviation(feature)](#M-SigStat-Common-MathHelper-StdDiviation-System-Collections-Generic-IEnumerable{System-Double}- 'SigStat.Common.MathHelper.StdDiviation(System.Collections.Generic.IEnumerable{System.Double})')
- [Multiply](#T-SigStat-Common-Transforms-Multiply 'SigStat.Common.Transforms.Multiply')
  - [#ctor(byConst)](#M-SigStat-Common-Transforms-Multiply-#ctor-System-Double- 'SigStat.Common.Transforms.Multiply.#ctor(System.Double)')
  - [InputList](#P-SigStat-Common-Transforms-Multiply-InputList 'SigStat.Common.Transforms.Multiply.InputList')
  - [Output](#P-SigStat-Common-Transforms-Multiply-Output 'SigStat.Common.Transforms.Multiply.Output')
  - [Transform()](#M-SigStat-Common-Transforms-Multiply-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.Multiply.Transform(SigStat.Common.Signature)')
- [Normalize](#T-SigStat-Common-Transforms-Normalize 'SigStat.Common.Transforms.Normalize')
  - [Input](#P-SigStat-Common-Transforms-Normalize-Input 'SigStat.Common.Transforms.Normalize.Input')
  - [Output](#P-SigStat-Common-Transforms-Normalize-Output 'SigStat.Common.Transforms.Normalize.Output')
  - [Transform()](#M-SigStat-Common-Transforms-Normalize-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.Normalize.Transform(SigStat.Common.Signature)')
- [NormalizeRotation](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation')
  - [InputT](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-InputT 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation.InputT')
  - [InputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-InputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation.InputX')
  - [InputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-InputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation.InputY')
  - [OutputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-OutputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation.OutputX')
  - [OutputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-OutputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation.OutputY')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation.Transform(SigStat.Common.Signature)')
- [NormalizeRotation2](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation2')
  - [InputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-InputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation2.InputX')
  - [InputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-InputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation2.InputY')
  - [OutputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-OutputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation2.OutputX')
  - [OutputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-OutputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation2.OutputY')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation2.Transform(SigStat.Common.Signature)')
- [NormalizeRotation3](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation3')
  - [InputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-InputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation3.InputX')
  - [InputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-InputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation3.InputY')
  - [OutputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-OutputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation3.OutputX')
  - [OutputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-OutputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation3.OutputY')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation3.Transform(SigStat.Common.Signature)')
- [NormalizeRotationForX](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotationForX')
  - [InputT](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-InputT 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotationForX.InputT')
  - [InputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-InputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotationForX.InputX')
  - [InputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-InputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotationForX.InputY')
  - [OutputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-OutputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotationForX.OutputX')
  - [OutputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-OutputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotationForX.OutputY')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotationForX.Transform(SigStat.Common.Signature)')
- [OddNSampler](#T-SigStat-Common-Framework-Samplers-OddNSampler 'SigStat.Common.Framework.Samplers.OddNSampler')
  - [#ctor(n)](#M-SigStat-Common-Framework-Samplers-OddNSampler-#ctor-System-Int32- 'SigStat.Common.Framework.Samplers.OddNSampler.#ctor(System.Int32)')
  - [N](#P-SigStat-Common-Framework-Samplers-OddNSampler-N 'SigStat.Common.Framework.Samplers.OddNSampler.N')
- [OnePixelThinning](#T-SigStat-Common-Transforms-OnePixelThinning 'SigStat.Common.Transforms.OnePixelThinning')
  - [Input](#P-SigStat-Common-Transforms-OnePixelThinning-Input 'SigStat.Common.Transforms.OnePixelThinning.Input')
  - [Output](#P-SigStat-Common-Transforms-OnePixelThinning-Output 'SigStat.Common.Transforms.OnePixelThinning.Output')
  - [Transform()](#M-SigStat-Common-Transforms-OnePixelThinning-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.OnePixelThinning.Transform(SigStat.Common.Signature)')
- [OnePixelThinningStep](#T-SigStat-Common-Algorithms-OnePixelThinningStep 'SigStat.Common.Algorithms.OnePixelThinningStep')
  - [ResultChanged](#P-SigStat-Common-Algorithms-OnePixelThinningStep-ResultChanged 'SigStat.Common.Algorithms.OnePixelThinningStep.ResultChanged')
  - [Scan(binaryImage)](#M-SigStat-Common-Algorithms-OnePixelThinningStep-Scan-System-Boolean[0-,0-]- 'SigStat.Common.Algorithms.OnePixelThinningStep.Scan(System.Boolean[0:,0:])')
- [OptimalDtwClassifier](#T-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier')
  - [#ctor(distanceFunction)](#M-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-#ctor-System-Func{System-Double[],System-Double[],System-Double}- 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.#ctor(System.Func{System.Double[],System.Double[],System.Double})')
  - [DistanceFunction](#P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-DistanceFunction 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.DistanceFunction')
  - [Features](#P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-Features 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.Features')
  - [Sampler](#P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-Sampler 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.Sampler')
  - [WarpingWindowLength](#P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-WarpingWindowLength 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.WarpingWindowLength')
  - [Test()](#M-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-Test-SigStat-Common-Pipeline-ISignerModel,SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.Test(SigStat.Common.Pipeline.ISignerModel,SigStat.Common.Signature)')
  - [Train()](#M-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}- 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.Train(System.Collections.Generic.List{SigStat.Common.Signature})')
- [OptimalDtwSignerModel](#T-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.OptimalDtwSignerModel')
  - [DistanceMatrix](#P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel-DistanceMatrix 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.OptimalDtwSignerModel.DistanceMatrix')
  - [ErrorRates](#P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel-ErrorRates 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.OptimalDtwSignerModel.ErrorRates')
  - [SignatureDistanceFromTraining](#P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel-SignatureDistanceFromTraining 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.OptimalDtwSignerModel.SignatureDistanceFromTraining')
  - [Threshold](#P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel-Threshold 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.OptimalDtwSignerModel.Threshold')
- [Origin](#T-SigStat-Common-Origin 'SigStat.Common.Origin')
  - [Forged](#F-SigStat-Common-Origin-Forged 'SigStat.Common.Origin.Forged')
  - [Genuine](#F-SigStat-Common-Origin-Genuine 'SigStat.Common.Origin.Genuine')
  - [Unknown](#F-SigStat-Common-Origin-Unknown 'SigStat.Common.Origin.Unknown')
- [OriginType](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OriginType')
  - [CenterOfGravity](#F-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType-CenterOfGravity 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OriginType.CenterOfGravity')
  - [Maximum](#F-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType-Maximum 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OriginType.Maximum')
  - [Minimum](#F-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType-Minimum 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OriginType.Minimum')
  - [Predefined](#F-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType-Predefined 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OriginType.Predefined')
- [OrthognalRotation](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OrthognalRotation')
  - [InputT](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-InputT 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OrthognalRotation.InputT')
  - [InputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-InputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OrthognalRotation.InputX')
  - [InputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-InputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OrthognalRotation.InputY')
  - [OutputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-OutputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OrthognalRotation.OutputX')
  - [OutputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-OutputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OrthognalRotation.OutputY')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OrthognalRotation.Transform(SigStat.Common.Signature)')
- [Output](#T-SigStat-Common-Pipeline-Output 'SigStat.Common.Pipeline.Output')
  - [#ctor(Default)](#M-SigStat-Common-Pipeline-Output-#ctor-System-String- 'SigStat.Common.Pipeline.Output.#ctor(System.String)')
  - [#ctor()](#M-SigStat-Common-Pipeline-Output-#ctor 'SigStat.Common.Pipeline.Output.#ctor')
  - [Default](#F-SigStat-Common-Pipeline-Output-Default 'SigStat.Common.Pipeline.Output.Default')
- [Palette](#T-SigStat-Common-Helpers-Excel-Palette 'SigStat.Common.Helpers.Excel.Palette')
  - [#ctor(main,dark,light)](#M-SigStat-Common-Helpers-Excel-Palette-#ctor-System-Drawing-Color,System-Drawing-Color,System-Drawing-Color- 'SigStat.Common.Helpers.Excel.Palette.#ctor(System.Drawing.Color,System.Drawing.Color,System.Drawing.Color)')
  - [DarkColor](#P-SigStat-Common-Helpers-Excel-Palette-DarkColor 'SigStat.Common.Helpers.Excel.Palette.DarkColor')
  - [LightColor](#P-SigStat-Common-Helpers-Excel-Palette-LightColor 'SigStat.Common.Helpers.Excel.Palette.LightColor')
  - [MainColor](#P-SigStat-Common-Helpers-Excel-Palette-MainColor 'SigStat.Common.Helpers.Excel.Palette.MainColor')
- [PaletteStorage](#T-SigStat-Common-Helpers-Excel-PaletteStorage 'SigStat.Common.Helpers.Excel.PaletteStorage')
  - [GetPalette(excelColor)](#M-SigStat-Common-Helpers-Excel-PaletteStorage-GetPalette-SigStat-Common-Helpers-Excel-ExcelColor- 'SigStat.Common.Helpers.Excel.PaletteStorage.GetPalette(SigStat.Common.Helpers.Excel.ExcelColor)')
- [ParallelTransformPipeline](#T-SigStat-Common-Pipeline-ParallelTransformPipeline 'SigStat.Common.Pipeline.ParallelTransformPipeline')
  - [Items](#F-SigStat-Common-Pipeline-ParallelTransformPipeline-Items 'SigStat.Common.Pipeline.ParallelTransformPipeline.Items')
  - [PipelineInputs](#P-SigStat-Common-Pipeline-ParallelTransformPipeline-PipelineInputs 'SigStat.Common.Pipeline.ParallelTransformPipeline.PipelineInputs')
  - [PipelineOutputs](#P-SigStat-Common-Pipeline-ParallelTransformPipeline-PipelineOutputs 'SigStat.Common.Pipeline.ParallelTransformPipeline.PipelineOutputs')
  - [Add(newItem)](#M-SigStat-Common-Pipeline-ParallelTransformPipeline-Add-SigStat-Common-ITransformation- 'SigStat.Common.Pipeline.ParallelTransformPipeline.Add(SigStat.Common.ITransformation)')
  - [GetEnumerator()](#M-SigStat-Common-Pipeline-ParallelTransformPipeline-GetEnumerator 'SigStat.Common.Pipeline.ParallelTransformPipeline.GetEnumerator')
  - [Transform(signature)](#M-SigStat-Common-Pipeline-ParallelTransformPipeline-Transform-SigStat-Common-Signature- 'SigStat.Common.Pipeline.ParallelTransformPipeline.Transform(SigStat.Common.Signature)')
- [PatternMatching3x3](#T-SigStat-Common-Algorithms-PatternMatching3x3 'SigStat.Common.Algorithms.PatternMatching3x3')
  - [#ctor(pattern)](#M-SigStat-Common-Algorithms-PatternMatching3x3-#ctor-System-Nullable{System-Boolean}[0-,0-]- 'SigStat.Common.Algorithms.PatternMatching3x3.#ctor(System.Nullable{System.Boolean}[0:,0:])')
  - [Match(input)](#M-SigStat-Common-Algorithms-PatternMatching3x3-Match-System-Boolean[0-,0-]- 'SigStat.Common.Algorithms.PatternMatching3x3.Match(System.Boolean[0:,0:])')
  - [RotMatch(input)](#M-SigStat-Common-Algorithms-PatternMatching3x3-RotMatch-System-Boolean[0-,0-]- 'SigStat.Common.Algorithms.PatternMatching3x3.RotMatch(System.Boolean[0:,0:])')
  - [Rotate(input)](#M-SigStat-Common-Algorithms-PatternMatching3x3-Rotate-System-Nullable{System-Boolean}[0-,0-]- 'SigStat.Common.Algorithms.PatternMatching3x3.Rotate(System.Nullable{System.Boolean}[0:,0:])')
- [PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
  - [#ctor()](#M-SigStat-Common-PipelineBase-#ctor 'SigStat.Common.PipelineBase.#ctor')
  - [Logger](#P-SigStat-Common-PipelineBase-Logger 'SigStat.Common.PipelineBase.Logger')
  - [PipelineInputs](#P-SigStat-Common-PipelineBase-PipelineInputs 'SigStat.Common.PipelineBase.PipelineInputs')
  - [PipelineOutputs](#P-SigStat-Common-PipelineBase-PipelineOutputs 'SigStat.Common.PipelineBase.PipelineOutputs')
  - [Progress](#P-SigStat-Common-PipelineBase-Progress 'SigStat.Common.PipelineBase.Progress')
  - [OnProgressChanged()](#M-SigStat-Common-PipelineBase-OnProgressChanged 'SigStat.Common.PipelineBase.OnProgressChanged')
- [PipelineInput](#T-SigStat-Common-Pipeline-PipelineInput 'SigStat.Common.Pipeline.PipelineInput')
  - [#ctor(PipelineItem,PI)](#M-SigStat-Common-Pipeline-PipelineInput-#ctor-System-Object,System-Reflection-PropertyInfo- 'SigStat.Common.Pipeline.PipelineInput.#ctor(System.Object,System.Reflection.PropertyInfo)')
  - [AutoSetMode](#P-SigStat-Common-Pipeline-PipelineInput-AutoSetMode 'SigStat.Common.Pipeline.PipelineInput.AutoSetMode')
  - [FD](#P-SigStat-Common-Pipeline-PipelineInput-FD 'SigStat.Common.Pipeline.PipelineInput.FD')
  - [IsCollectionOfFeatureDescriptors](#P-SigStat-Common-Pipeline-PipelineInput-IsCollectionOfFeatureDescriptors 'SigStat.Common.Pipeline.PipelineInput.IsCollectionOfFeatureDescriptors')
  - [PropName](#P-SigStat-Common-Pipeline-PipelineInput-PropName 'SigStat.Common.Pipeline.PipelineInput.PropName')
  - [Type](#P-SigStat-Common-Pipeline-PipelineInput-Type 'SigStat.Common.Pipeline.PipelineInput.Type')
- [PipelineOutput](#T-SigStat-Common-Pipeline-PipelineOutput 'SigStat.Common.Pipeline.PipelineOutput')
  - [#ctor(PipelineItem,PI)](#M-SigStat-Common-Pipeline-PipelineOutput-#ctor-System-Object,System-Reflection-PropertyInfo- 'SigStat.Common.Pipeline.PipelineOutput.#ctor(System.Object,System.Reflection.PropertyInfo)')
  - [Default](#P-SigStat-Common-Pipeline-PipelineOutput-Default 'SigStat.Common.Pipeline.PipelineOutput.Default')
  - [FD](#P-SigStat-Common-Pipeline-PipelineOutput-FD 'SigStat.Common.Pipeline.PipelineOutput.FD')
  - [IsCollectionOfFeatureDescriptors](#P-SigStat-Common-Pipeline-PipelineOutput-IsCollectionOfFeatureDescriptors 'SigStat.Common.Pipeline.PipelineOutput.IsCollectionOfFeatureDescriptors')
  - [IsTemporary](#P-SigStat-Common-Pipeline-PipelineOutput-IsTemporary 'SigStat.Common.Pipeline.PipelineOutput.IsTemporary')
  - [PropName](#P-SigStat-Common-Pipeline-PipelineOutput-PropName 'SigStat.Common.Pipeline.PipelineOutput.PropName')
  - [Type](#P-SigStat-Common-Pipeline-PipelineOutput-Type 'SigStat.Common.Pipeline.PipelineOutput.Type')
- [RealisticImageGenerator](#T-SigStat-Common-Transforms-RealisticImageGenerator 'SigStat.Common.Transforms.RealisticImageGenerator')
  - [#ctor(resolutionX,resolutionY)](#M-SigStat-Common-Transforms-RealisticImageGenerator-#ctor-System-Int32,System-Int32- 'SigStat.Common.Transforms.RealisticImageGenerator.#ctor(System.Int32,System.Int32)')
  - [Altitude](#P-SigStat-Common-Transforms-RealisticImageGenerator-Altitude 'SigStat.Common.Transforms.RealisticImageGenerator.Altitude')
  - [Azimuth](#P-SigStat-Common-Transforms-RealisticImageGenerator-Azimuth 'SigStat.Common.Transforms.RealisticImageGenerator.Azimuth')
  - [Button](#P-SigStat-Common-Transforms-RealisticImageGenerator-Button 'SigStat.Common.Transforms.RealisticImageGenerator.Button')
  - [OutputImage](#P-SigStat-Common-Transforms-RealisticImageGenerator-OutputImage 'SigStat.Common.Transforms.RealisticImageGenerator.OutputImage')
  - [Pressure](#P-SigStat-Common-Transforms-RealisticImageGenerator-Pressure 'SigStat.Common.Transforms.RealisticImageGenerator.Pressure')
  - [X](#P-SigStat-Common-Transforms-RealisticImageGenerator-X 'SigStat.Common.Transforms.RealisticImageGenerator.X')
  - [Y](#P-SigStat-Common-Transforms-RealisticImageGenerator-Y 'SigStat.Common.Transforms.RealisticImageGenerator.Y')
  - [Lerp(t0,t1,t)](#M-SigStat-Common-Transforms-RealisticImageGenerator-Lerp-System-Single,System-Single,System-Single- 'SigStat.Common.Transforms.RealisticImageGenerator.Lerp(System.Single,System.Single,System.Single)')
  - [Transform()](#M-SigStat-Common-Transforms-RealisticImageGenerator-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.RealisticImageGenerator.Transform(SigStat.Common.Signature)')
- [RectangleFConverter](#T-SigStat-Common-Helpers-Serialization-RectangleFConverter 'SigStat.Common.Helpers.Serialization.RectangleFConverter')
  - [CanConvert(objectType)](#M-SigStat-Common-Helpers-Serialization-RectangleFConverter-CanConvert-System-Type- 'SigStat.Common.Helpers.Serialization.RectangleFConverter.CanConvert(System.Type)')
  - [ReadJson()](#M-SigStat-Common-Helpers-Serialization-RectangleFConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.RectangleFConverter.ReadJson(Newtonsoft.Json.JsonReader,System.Type,System.Object,Newtonsoft.Json.JsonSerializer)')
  - [WriteJson()](#M-SigStat-Common-Helpers-Serialization-RectangleFConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer- 'SigStat.Common.Helpers.Serialization.RectangleFConverter.WriteJson(Newtonsoft.Json.JsonWriter,System.Object,Newtonsoft.Json.JsonSerializer)')
- [RelativeScale](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale 'SigStat.Common.PipelineItems.Transforms.Preprocessing.RelativeScale')
  - [InputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale-InputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.RelativeScale.InputFeature')
  - [OutputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale-OutputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.RelativeScale.OutputFeature')
  - [ReferenceFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale-ReferenceFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.RelativeScale.ReferenceFeature')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.RelativeScale.Transform(SigStat.Common.Signature)')
- [ReportInformationLogger](#T-SigStat-Common-Logging-ReportInformationLogger 'SigStat.Common.Logging.ReportInformationLogger')
  - [#ctor()](#M-SigStat-Common-Logging-ReportInformationLogger-#ctor 'SigStat.Common.Logging.ReportInformationLogger.#ctor')
  - [reportLogs](#F-SigStat-Common-Logging-ReportInformationLogger-reportLogs 'SigStat.Common.Logging.ReportInformationLogger.reportLogs')
  - [ReportLogs](#P-SigStat-Common-Logging-ReportInformationLogger-ReportLogs 'SigStat.Common.Logging.ReportInformationLogger.ReportLogs')
  - [BeginScope\`\`1()](#M-SigStat-Common-Logging-ReportInformationLogger-BeginScope``1-``0- 'SigStat.Common.Logging.ReportInformationLogger.BeginScope``1(``0)')
  - [IsEnabled()](#M-SigStat-Common-Logging-ReportInformationLogger-IsEnabled-Microsoft-Extensions-Logging-LogLevel- 'SigStat.Common.Logging.ReportInformationLogger.IsEnabled(Microsoft.Extensions.Logging.LogLevel)')
  - [Log\`\`1()](#M-SigStat-Common-Logging-ReportInformationLogger-Log``1-Microsoft-Extensions-Logging-LogLevel,Microsoft-Extensions-Logging-EventId,``0,System-Exception,System-Func{``0,System-Exception,System-String}- 'SigStat.Common.Logging.ReportInformationLogger.Log``1(Microsoft.Extensions.Logging.LogLevel,Microsoft.Extensions.Logging.EventId,``0,System.Exception,System.Func{``0,System.Exception,System.String})')
- [ResampleSamplesCountBased](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleSamplesCountBased')
  - [InputFeatures](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-InputFeatures 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleSamplesCountBased.InputFeatures')
  - [InterpolationType](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-InterpolationType 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleSamplesCountBased.InterpolationType')
  - [NumOfSamples](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-NumOfSamples 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleSamplesCountBased.NumOfSamples')
  - [OriginalTFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-OriginalTFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleSamplesCountBased.OriginalTFeature')
  - [OutputFeatures](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-OutputFeatures 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleSamplesCountBased.OutputFeatures')
  - [ResampledTFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-ResampledTFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleSamplesCountBased.ResampledTFeature')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ResampleSamplesCountBased.Transform(SigStat.Common.Signature)')
- [Resize](#T-SigStat-Common-Transforms-Resize 'SigStat.Common.Transforms.Resize')
  - [Height](#P-SigStat-Common-Transforms-Resize-Height 'SigStat.Common.Transforms.Resize.Height')
  - [InputImage](#P-SigStat-Common-Transforms-Resize-InputImage 'SigStat.Common.Transforms.Resize.InputImage')
  - [OutputImage](#P-SigStat-Common-Transforms-Resize-OutputImage 'SigStat.Common.Transforms.Resize.OutputImage')
  - [ResizeFunction](#P-SigStat-Common-Transforms-Resize-ResizeFunction 'SigStat.Common.Transforms.Resize.ResizeFunction')
  - [Width](#P-SigStat-Common-Transforms-Resize-Width 'SigStat.Common.Transforms.Resize.Width')
  - [Transform()](#M-SigStat-Common-Transforms-Resize-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.Resize.Transform(SigStat.Common.Signature)')
- [Result](#T-SigStat-Common-Result 'SigStat.Common.Result')
  - [Model](#F-SigStat-Common-Result-Model 'SigStat.Common.Result.Model')
  - [Aer](#P-SigStat-Common-Result-Aer 'SigStat.Common.Result.Aer')
  - [Far](#P-SigStat-Common-Result-Far 'SigStat.Common.Result.Far')
  - [Frr](#P-SigStat-Common-Result-Frr 'SigStat.Common.Result.Frr')
  - [Signer](#P-SigStat-Common-Result-Signer 'SigStat.Common.Result.Signer')
- [SampleRate](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate 'SigStat.Common.PipelineItems.Transforms.Preprocessing.SampleRate')
  - [InputP](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-InputP 'SigStat.Common.PipelineItems.Transforms.Preprocessing.SampleRate.InputP')
  - [InputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-InputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.SampleRate.InputX')
  - [InputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-InputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.SampleRate.InputY')
  - [OutputX](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-OutputX 'SigStat.Common.PipelineItems.Transforms.Preprocessing.SampleRate.OutputX')
  - [OutputY](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-OutputY 'SigStat.Common.PipelineItems.Transforms.Preprocessing.SampleRate.OutputY')
  - [samplerate](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-samplerate 'SigStat.Common.PipelineItems.Transforms.Preprocessing.SampleRate.samplerate')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.SampleRate.Transform(SigStat.Common.Signature)')
- [SampleRateResults](#T-SigStat-Common-Model-SampleRateResults 'SigStat.Common.Model.SampleRateResults')
  - [AER](#P-SigStat-Common-Model-SampleRateResults-AER 'SigStat.Common.Model.SampleRateResults.AER')
- [Sampler](#T-SigStat-Common-Sampler 'SigStat.Common.Sampler')
  - [#ctor(references,genuineTests,forgeryTests)](#M-SigStat-Common-Sampler-#ctor-System-Func{System-Collections-Generic-List{SigStat-Common-Signature},System-Collections-Generic-List{SigStat-Common-Signature}},System-Func{System-Collections-Generic-List{SigStat-Common-Signature},System-Collections-Generic-List{SigStat-Common-Signature}},System-Func{System-Collections-Generic-List{SigStat-Common-Signature},System-Collections-Generic-List{SigStat-Common-Signature}}- 'SigStat.Common.Sampler.#ctor(System.Func{System.Collections.Generic.List{SigStat.Common.Signature},System.Collections.Generic.List{SigStat.Common.Signature}},System.Func{System.Collections.Generic.List{SigStat.Common.Signature},System.Collections.Generic.List{SigStat.Common.Signature}},System.Func{System.Collections.Generic.List{SigStat.Common.Signature},System.Collections.Generic.List{SigStat.Common.Signature}})')
  - [ForgeryTestFilter](#P-SigStat-Common-Sampler-ForgeryTestFilter 'SigStat.Common.Sampler.ForgeryTestFilter')
  - [GenuineTestFilter](#P-SigStat-Common-Sampler-GenuineTestFilter 'SigStat.Common.Sampler.GenuineTestFilter')
  - [TrainingFilter](#P-SigStat-Common-Sampler-TrainingFilter 'SigStat.Common.Sampler.TrainingFilter')
  - [SampleForgeryTests()](#M-SigStat-Common-Sampler-SampleForgeryTests-System-Collections-Generic-List{SigStat-Common-Signature}- 'SigStat.Common.Sampler.SampleForgeryTests(System.Collections.Generic.List{SigStat.Common.Signature})')
  - [SampleGenuineTests()](#M-SigStat-Common-Sampler-SampleGenuineTests-System-Collections-Generic-List{SigStat-Common-Signature}- 'SigStat.Common.Sampler.SampleGenuineTests(System.Collections.Generic.List{SigStat.Common.Signature})')
  - [SampleReferences()](#M-SigStat-Common-Sampler-SampleReferences-System-Collections-Generic-List{SigStat-Common-Signature}- 'SigStat.Common.Sampler.SampleReferences(System.Collections.Generic.List{SigStat.Common.Signature})')
- [Scale](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale 'SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale')
  - [InputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale-InputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale.InputFeature')
  - [Mode](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale-Mode 'SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale.Mode')
  - [OutputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale-OutputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale.OutputFeature')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale.Transform(SigStat.Common.Signature)')
- [ScalingMode](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-ScalingMode 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ScalingMode')
  - [Scaling1](#F-SigStat-Common-PipelineItems-Transforms-Preprocessing-ScalingMode-Scaling1 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ScalingMode.Scaling1')
  - [ScalingS](#F-SigStat-Common-PipelineItems-Transforms-Preprocessing-ScalingMode-ScalingS 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ScalingMode.ScalingS')
- [SequentialTransformPipeline](#T-SigStat-Common-Pipeline-SequentialTransformPipeline 'SigStat.Common.Pipeline.SequentialTransformPipeline')
  - [Items](#F-SigStat-Common-Pipeline-SequentialTransformPipeline-Items 'SigStat.Common.Pipeline.SequentialTransformPipeline.Items')
  - [PipelineInputs](#P-SigStat-Common-Pipeline-SequentialTransformPipeline-PipelineInputs 'SigStat.Common.Pipeline.SequentialTransformPipeline.PipelineInputs')
  - [PipelineOutputs](#P-SigStat-Common-Pipeline-SequentialTransformPipeline-PipelineOutputs 'SigStat.Common.Pipeline.SequentialTransformPipeline.PipelineOutputs')
  - [Add(newItem)](#M-SigStat-Common-Pipeline-SequentialTransformPipeline-Add-SigStat-Common-ITransformation- 'SigStat.Common.Pipeline.SequentialTransformPipeline.Add(SigStat.Common.ITransformation)')
  - [GetEnumerator()](#M-SigStat-Common-Pipeline-SequentialTransformPipeline-GetEnumerator 'SigStat.Common.Pipeline.SequentialTransformPipeline.GetEnumerator')
  - [Transform(signature)](#M-SigStat-Common-Pipeline-SequentialTransformPipeline-Transform-SigStat-Common-Signature- 'SigStat.Common.Pipeline.SequentialTransformPipeline.Transform(SigStat.Common.Signature)')
- [SerializationHelper](#T-SigStat-Common-Helpers-SerializationHelper 'SigStat.Common.Helpers.SerializationHelper')
  - [DeserializeFromFile\`\`1(path)](#M-SigStat-Common-Helpers-SerializationHelper-DeserializeFromFile``1-System-String- 'SigStat.Common.Helpers.SerializationHelper.DeserializeFromFile``1(System.String)')
  - [Deserialize\`\`1(s)](#M-SigStat-Common-Helpers-SerializationHelper-Deserialize``1-System-String- 'SigStat.Common.Helpers.SerializationHelper.Deserialize``1(System.String)')
  - [GetSettings()](#M-SigStat-Common-Helpers-SerializationHelper-GetSettings-System-Boolean- 'SigStat.Common.Helpers.SerializationHelper.GetSettings(System.Boolean)')
  - [JsonSerializeToFile\`\`1(o,path)](#M-SigStat-Common-Helpers-SerializationHelper-JsonSerializeToFile``1-``0,System-String,System-Boolean- 'SigStat.Common.Helpers.SerializationHelper.JsonSerializeToFile``1(``0,System.String,System.Boolean)')
  - [JsonSerialize\`\`1(o)](#M-SigStat-Common-Helpers-SerializationHelper-JsonSerialize``1-``0,System-Boolean- 'SigStat.Common.Helpers.SerializationHelper.JsonSerialize``1(``0,System.Boolean)')
- [SigComp11](#T-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11 'SigStat.Common.Loaders.SigComp11DutchLoader.SigComp11')
  - [T](#F-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11-T 'SigStat.Common.Loaders.SigComp11DutchLoader.SigComp11.T')
  - [X](#F-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11-X 'SigStat.Common.Loaders.SigComp11DutchLoader.SigComp11.X')
  - [Y](#F-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11-Y 'SigStat.Common.Loaders.SigComp11DutchLoader.SigComp11.Y')
  - [Z](#F-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11-Z 'SigStat.Common.Loaders.SigComp11DutchLoader.SigComp11.Z')
- [SigComp11Ch](#T-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch 'SigStat.Common.Loaders.SigComp11ChineseLoader.SigComp11Ch')
  - [P](#F-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch-P 'SigStat.Common.Loaders.SigComp11ChineseLoader.SigComp11Ch.P')
  - [T](#F-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch-T 'SigStat.Common.Loaders.SigComp11ChineseLoader.SigComp11Ch.T')
  - [X](#F-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch-X 'SigStat.Common.Loaders.SigComp11ChineseLoader.SigComp11Ch.X')
  - [Y](#F-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch-Y 'SigStat.Common.Loaders.SigComp11ChineseLoader.SigComp11Ch.Y')
- [SigComp11ChineseLoader](#T-SigStat-Common-Loaders-SigComp11ChineseLoader 'SigStat.Common.Loaders.SigComp11ChineseLoader')
  - [#ctor(databasePath,standardFeatures)](#M-SigStat-Common-Loaders-SigComp11ChineseLoader-#ctor-System-String,System-Boolean- 'SigStat.Common.Loaders.SigComp11ChineseLoader.#ctor(System.String,System.Boolean)')
  - [DatabasePath](#P-SigStat-Common-Loaders-SigComp11ChineseLoader-DatabasePath 'SigStat.Common.Loaders.SigComp11ChineseLoader.DatabasePath')
  - [SamplingFrequency](#P-SigStat-Common-Loaders-SigComp11ChineseLoader-SamplingFrequency 'SigStat.Common.Loaders.SigComp11ChineseLoader.SamplingFrequency')
  - [StandardFeatures](#P-SigStat-Common-Loaders-SigComp11ChineseLoader-StandardFeatures 'SigStat.Common.Loaders.SigComp11ChineseLoader.StandardFeatures')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-SigComp11ChineseLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.SigComp11ChineseLoader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
  - [LoadSignature(signature,stream,standardFeatures)](#M-SigStat-Common-Loaders-SigComp11ChineseLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean- 'SigStat.Common.Loaders.SigComp11ChineseLoader.LoadSignature(SigStat.Common.Signature,System.IO.MemoryStream,System.Boolean)')
- [SigComp11DutchLoader](#T-SigStat-Common-Loaders-SigComp11DutchLoader 'SigStat.Common.Loaders.SigComp11DutchLoader')
  - [#ctor(databasePath,standardFeatures)](#M-SigStat-Common-Loaders-SigComp11DutchLoader-#ctor-System-String,System-Boolean- 'SigStat.Common.Loaders.SigComp11DutchLoader.#ctor(System.String,System.Boolean)')
  - [DatabasePath](#P-SigStat-Common-Loaders-SigComp11DutchLoader-DatabasePath 'SigStat.Common.Loaders.SigComp11DutchLoader.DatabasePath')
  - [SamplingFrequency](#P-SigStat-Common-Loaders-SigComp11DutchLoader-SamplingFrequency 'SigStat.Common.Loaders.SigComp11DutchLoader.SamplingFrequency')
  - [StandardFeatures](#P-SigStat-Common-Loaders-SigComp11DutchLoader-StandardFeatures 'SigStat.Common.Loaders.SigComp11DutchLoader.StandardFeatures')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-SigComp11DutchLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.SigComp11DutchLoader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
  - [LoadSignature(signature,stream,standardFeatures)](#M-SigStat-Common-Loaders-SigComp11DutchLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean- 'SigStat.Common.Loaders.SigComp11DutchLoader.LoadSignature(SigStat.Common.Signature,System.IO.MemoryStream,System.Boolean)')
- [SigComp13Japanese](#T-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese 'SigStat.Common.Loaders.SigComp13JapaneseLoader.SigComp13Japanese')
  - [P](#F-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese-P 'SigStat.Common.Loaders.SigComp13JapaneseLoader.SigComp13Japanese.P')
  - [T](#F-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese-T 'SigStat.Common.Loaders.SigComp13JapaneseLoader.SigComp13Japanese.T')
  - [X](#F-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese-X 'SigStat.Common.Loaders.SigComp13JapaneseLoader.SigComp13Japanese.X')
  - [Y](#F-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese-Y 'SigStat.Common.Loaders.SigComp13JapaneseLoader.SigComp13Japanese.Y')
- [SigComp13JapaneseLoader](#T-SigStat-Common-Loaders-SigComp13JapaneseLoader 'SigStat.Common.Loaders.SigComp13JapaneseLoader')
  - [#ctor(databasePath,standardFeatures)](#M-SigStat-Common-Loaders-SigComp13JapaneseLoader-#ctor-System-String,System-Boolean- 'SigStat.Common.Loaders.SigComp13JapaneseLoader.#ctor(System.String,System.Boolean)')
  - [DatabasePath](#P-SigStat-Common-Loaders-SigComp13JapaneseLoader-DatabasePath 'SigStat.Common.Loaders.SigComp13JapaneseLoader.DatabasePath')
  - [SamplingFrequency](#P-SigStat-Common-Loaders-SigComp13JapaneseLoader-SamplingFrequency 'SigStat.Common.Loaders.SigComp13JapaneseLoader.SamplingFrequency')
  - [StandardFeatures](#P-SigStat-Common-Loaders-SigComp13JapaneseLoader-StandardFeatures 'SigStat.Common.Loaders.SigComp13JapaneseLoader.StandardFeatures')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-SigComp13JapaneseLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.SigComp13JapaneseLoader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
  - [LoadSignature(signature,stream,standardFeatures)](#M-SigStat-Common-Loaders-SigComp13JapaneseLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean- 'SigStat.Common.Loaders.SigComp13JapaneseLoader.LoadSignature(SigStat.Common.Signature,System.IO.MemoryStream,System.Boolean)')
- [SigComp15](#T-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15 'SigStat.Common.Loaders.SigComp15GermanLoader.SigComp15')
  - [P](#F-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15-P 'SigStat.Common.Loaders.SigComp15GermanLoader.SigComp15.P')
  - [T](#F-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15-T 'SigStat.Common.Loaders.SigComp15GermanLoader.SigComp15.T')
  - [X](#F-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15-X 'SigStat.Common.Loaders.SigComp15GermanLoader.SigComp15.X')
  - [Y](#F-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15-Y 'SigStat.Common.Loaders.SigComp15GermanLoader.SigComp15.Y')
- [SigComp15GermanLoader](#T-SigStat-Common-Loaders-SigComp15GermanLoader 'SigStat.Common.Loaders.SigComp15GermanLoader')
  - [#ctor(databasePath,standardFeatures)](#M-SigStat-Common-Loaders-SigComp15GermanLoader-#ctor-System-String,System-Boolean- 'SigStat.Common.Loaders.SigComp15GermanLoader.#ctor(System.String,System.Boolean)')
  - [DatabasePath](#P-SigStat-Common-Loaders-SigComp15GermanLoader-DatabasePath 'SigStat.Common.Loaders.SigComp15GermanLoader.DatabasePath')
  - [SamplingFrequency](#P-SigStat-Common-Loaders-SigComp15GermanLoader-SamplingFrequency 'SigStat.Common.Loaders.SigComp15GermanLoader.SamplingFrequency')
  - [StandardFeatures](#P-SigStat-Common-Loaders-SigComp15GermanLoader-StandardFeatures 'SigStat.Common.Loaders.SigComp15GermanLoader.StandardFeatures')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-SigComp15GermanLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.SigComp15GermanLoader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
  - [LoadSignature(signature,stream,standardFeatures)](#M-SigStat-Common-Loaders-SigComp15GermanLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean- 'SigStat.Common.Loaders.SigComp15GermanLoader.LoadSignature(SigStat.Common.Signature,System.IO.MemoryStream,System.Boolean)')
- [SigComp19](#T-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19 'SigStat.Common.Loaders.SigComp19OnlineLoader.SigComp19')
  - [Altitude](#F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-Altitude 'SigStat.Common.Loaders.SigComp19OnlineLoader.SigComp19.Altitude')
  - [Azimuth](#F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-Azimuth 'SigStat.Common.Loaders.SigComp19OnlineLoader.SigComp19.Azimuth')
  - [Distance](#F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-Distance 'SigStat.Common.Loaders.SigComp19OnlineLoader.SigComp19.Distance')
  - [EventType](#F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-EventType 'SigStat.Common.Loaders.SigComp19OnlineLoader.SigComp19.EventType')
  - [P](#F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-P 'SigStat.Common.Loaders.SigComp19OnlineLoader.SigComp19.P')
  - [T](#F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-T 'SigStat.Common.Loaders.SigComp19OnlineLoader.SigComp19.T')
  - [X](#F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-X 'SigStat.Common.Loaders.SigComp19OnlineLoader.SigComp19.X')
  - [Y](#F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-Y 'SigStat.Common.Loaders.SigComp19OnlineLoader.SigComp19.Y')
- [SigComp19OnlineLoader](#T-SigStat-Common-Loaders-SigComp19OnlineLoader 'SigStat.Common.Loaders.SigComp19OnlineLoader')
  - [#ctor(databasePath,standardFeatures)](#M-SigStat-Common-Loaders-SigComp19OnlineLoader-#ctor-System-String,System-Boolean- 'SigStat.Common.Loaders.SigComp19OnlineLoader.#ctor(System.String,System.Boolean)')
  - [DatabasePath](#P-SigStat-Common-Loaders-SigComp19OnlineLoader-DatabasePath 'SigStat.Common.Loaders.SigComp19OnlineLoader.DatabasePath')
  - [SamplingFrequency](#P-SigStat-Common-Loaders-SigComp19OnlineLoader-SamplingFrequency 'SigStat.Common.Loaders.SigComp19OnlineLoader.SamplingFrequency')
  - [StandardFeatures](#P-SigStat-Common-Loaders-SigComp19OnlineLoader-StandardFeatures 'SigStat.Common.Loaders.SigComp19OnlineLoader.StandardFeatures')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-SigComp19OnlineLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.SigComp19OnlineLoader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
  - [LoadSignature(signature,stream,standardFeatures)](#M-SigStat-Common-Loaders-SigComp19OnlineLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean- 'SigStat.Common.Loaders.SigComp19OnlineLoader.LoadSignature(SigStat.Common.Signature,System.IO.MemoryStream,System.Boolean)')
- [SigStatEvents](#T-SigStat-Common-SigStatEvents 'SigStat.Common.SigStatEvents')
  - [BenchmarkEvent](#F-SigStat-Common-SigStatEvents-BenchmarkEvent 'SigStat.Common.SigStatEvents.BenchmarkEvent')
  - [VerifierEvent](#F-SigStat-Common-SigStatEvents-VerifierEvent 'SigStat.Common.SigStatEvents.VerifierEvent')
- [SigStatLogState](#T-SigStat-Common-Logging-SigStatLogState 'SigStat.Common.Logging.SigStatLogState')
  - [Source](#P-SigStat-Common-Logging-SigStatLogState-Source 'SigStat.Common.Logging.SigStatLogState.Source')
- [Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature')
  - [#ctor()](#M-SigStat-Common-Signature-#ctor 'SigStat.Common.Signature.#ctor')
  - [#ctor(signatureID,origin,signer)](#M-SigStat-Common-Signature-#ctor-System-String,SigStat-Common-Origin,SigStat-Common-Signer- 'SigStat.Common.Signature.#ctor(System.String,SigStat.Common.Origin,SigStat.Common.Signer)')
  - [ID](#P-SigStat-Common-Signature-ID 'SigStat.Common.Signature.ID')
  - [Item](#P-SigStat-Common-Signature-Item-System-String- 'SigStat.Common.Signature.Item(System.String)')
  - [Item](#P-SigStat-Common-Signature-Item-SigStat-Common-FeatureDescriptor- 'SigStat.Common.Signature.Item(SigStat.Common.FeatureDescriptor)')
  - [Origin](#P-SigStat-Common-Signature-Origin 'SigStat.Common.Signature.Origin')
  - [Signer](#P-SigStat-Common-Signature-Signer 'SigStat.Common.Signature.Signer')
  - [GetAggregateFeature(fs)](#M-SigStat-Common-Signature-GetAggregateFeature-System-Collections-Generic-List{SigStat-Common-FeatureDescriptor}- 'SigStat.Common.Signature.GetAggregateFeature(System.Collections.Generic.List{SigStat.Common.FeatureDescriptor})')
  - [GetEnumerator()](#M-SigStat-Common-Signature-GetEnumerator 'SigStat.Common.Signature.GetEnumerator')
  - [GetFeatureDescriptors()](#M-SigStat-Common-Signature-GetFeatureDescriptors 'SigStat.Common.Signature.GetFeatureDescriptors')
  - [GetFeature\`\`1(featureKey)](#M-SigStat-Common-Signature-GetFeature``1-System-String- 'SigStat.Common.Signature.GetFeature``1(System.String)')
  - [GetFeature\`\`1(featureDescriptor)](#M-SigStat-Common-Signature-GetFeature``1-SigStat-Common-FeatureDescriptor{``0}- 'SigStat.Common.Signature.GetFeature``1(SigStat.Common.FeatureDescriptor{``0})')
  - [GetFeature\`\`1(featureDescriptor)](#M-SigStat-Common-Signature-GetFeature``1-SigStat-Common-FeatureDescriptor- 'SigStat.Common.Signature.GetFeature``1(SigStat.Common.FeatureDescriptor)')
  - [HasFeature(featureDescriptor)](#M-SigStat-Common-Signature-HasFeature-SigStat-Common-FeatureDescriptor- 'SigStat.Common.Signature.HasFeature(SigStat.Common.FeatureDescriptor)')
  - [HasFeature(featureKey)](#M-SigStat-Common-Signature-HasFeature-System-String- 'SigStat.Common.Signature.HasFeature(System.String)')
  - [SetFeature\`\`1(featureDescriptor,feature)](#M-SigStat-Common-Signature-SetFeature``1-SigStat-Common-FeatureDescriptor,``0- 'SigStat.Common.Signature.SetFeature``1(SigStat.Common.FeatureDescriptor,``0)')
  - [SetFeature\`\`1(featureKey,feature)](#M-SigStat-Common-Signature-SetFeature``1-System-String,``0- 'SigStat.Common.Signature.SetFeature``1(System.String,``0)')
  - [ToString()](#M-SigStat-Common-Signature-ToString 'SigStat.Common.Signature.ToString')
- [SignatureHelper](#T-SigStat-Common-SignatureHelper 'SigStat.Common.SignatureHelper')
  - [GetSignatureLength(signature)](#M-SigStat-Common-SignatureHelper-GetSignatureLength-SigStat-Common-Signature- 'SigStat.Common.SignatureHelper.GetSignatureLength(SigStat.Common.Signature)')
  - [SaveImage(sig,fileName)](#M-SigStat-Common-SignatureHelper-SaveImage-SigStat-Common-Signature,System-String- 'SigStat.Common.SignatureHelper.SaveImage(SigStat.Common.Signature,System.String)')
- [SignatureLogState](#T-SigStat-Common-Logging-SignatureLogState 'SigStat.Common.Logging.SignatureLogState')
  - [SignatureID](#P-SigStat-Common-Logging-SignatureLogState-SignatureID 'SigStat.Common.Logging.SignatureLogState.SignatureID')
  - [SignerID](#P-SigStat-Common-Logging-SignatureLogState-SignerID 'SigStat.Common.Logging.SignatureLogState.SignerID')
- [Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer')
  - [ID](#P-SigStat-Common-Signer-ID 'SigStat.Common.Signer.ID')
  - [Signatures](#P-SigStat-Common-Signer-Signatures 'SigStat.Common.Signer.Signatures')
  - [ToString()](#M-SigStat-Common-Signer-ToString 'SigStat.Common.Signer.ToString')
- [SignerLogState](#T-SigStat-Common-Logging-SignerLogState 'SigStat.Common.Logging.SignerLogState')
  - [SignerID](#P-SigStat-Common-Logging-SignerLogState-SignerID 'SigStat.Common.Logging.SignerLogState.SignerID')
- [SignerResults](#T-SigStat-Common-Logging-SignerResults 'SigStat.Common.Logging.SignerResults')
  - [#ctor(signerId)](#M-SigStat-Common-Logging-SignerResults-#ctor-System-String- 'SigStat.Common.Logging.SignerResults.#ctor(System.String)')
  - [Aer](#F-SigStat-Common-Logging-SignerResults-Aer 'SigStat.Common.Logging.SignerResults.Aer')
  - [Far](#F-SigStat-Common-Logging-SignerResults-Far 'SigStat.Common.Logging.SignerResults.Far')
  - [Frr](#F-SigStat-Common-Logging-SignerResults-Frr 'SigStat.Common.Logging.SignerResults.Frr')
  - [DistanceMatrix](#P-SigStat-Common-Logging-SignerResults-DistanceMatrix 'SigStat.Common.Logging.SignerResults.DistanceMatrix')
  - [SignerID](#P-SigStat-Common-Logging-SignerResults-SignerID 'SigStat.Common.Logging.SignerResults.SignerID')
- [SignerResultsLogState](#T-SigStat-Common-Logging-SignerResultsLogState 'SigStat.Common.Logging.SignerResultsLogState')
  - [#ctor(signerId,aer,far,frr)](#M-SigStat-Common-Logging-SignerResultsLogState-#ctor-System-String,System-Double,System-Double,System-Double- 'SigStat.Common.Logging.SignerResultsLogState.#ctor(System.String,System.Double,System.Double,System.Double)')
  - [Aer](#P-SigStat-Common-Logging-SignerResultsLogState-Aer 'SigStat.Common.Logging.SignerResultsLogState.Aer')
  - [Far](#P-SigStat-Common-Logging-SignerResultsLogState-Far 'SigStat.Common.Logging.SignerResultsLogState.Far')
  - [Frr](#P-SigStat-Common-Logging-SignerResultsLogState-Frr 'SigStat.Common.Logging.SignerResultsLogState.Frr')
- [SignerStatisticsHelper](#T-SigStat-Common-Helpers-SignerStatisticsHelper 'SigStat.Common.Helpers.SignerStatisticsHelper')
  - [GetHeightAvg(signer)](#M-SigStat-Common-Helpers-SignerStatisticsHelper-GetHeightAvg-SigStat-Common-Signer- 'SigStat.Common.Helpers.SignerStatisticsHelper.GetHeightAvg(SigStat.Common.Signer)')
  - [GetLengthAverage(signer)](#M-SigStat-Common-Helpers-SignerStatisticsHelper-GetLengthAverage-SigStat-Common-Signer- 'SigStat.Common.Helpers.SignerStatisticsHelper.GetLengthAverage(SigStat.Common.Signer)')
  - [GetMaxSignaturePoints(signer)](#M-SigStat-Common-Helpers-SignerStatisticsHelper-GetMaxSignaturePoints-SigStat-Common-Signer- 'SigStat.Common.Helpers.SignerStatisticsHelper.GetMaxSignaturePoints(SigStat.Common.Signer)')
  - [GetMinSignaturePoints(signer)](#M-SigStat-Common-Helpers-SignerStatisticsHelper-GetMinSignaturePoints-SigStat-Common-Signer- 'SigStat.Common.Helpers.SignerStatisticsHelper.GetMinSignaturePoints(SigStat.Common.Signer)')
  - [GetPointsAvg(signer)](#M-SigStat-Common-Helpers-SignerStatisticsHelper-GetPointsAvg-SigStat-Common-Signer- 'SigStat.Common.Helpers.SignerStatisticsHelper.GetPointsAvg(SigStat.Common.Signer)')
  - [GetWidthAvg(signer)](#M-SigStat-Common-Helpers-SignerStatisticsHelper-GetWidthAvg-SigStat-Common-Signer- 'SigStat.Common.Helpers.SignerStatisticsHelper.GetWidthAvg(SigStat.Common.Signer)')
- [SimpleConsoleLogger](#T-SigStat-Common-Logging-SimpleConsoleLogger 'SigStat.Common.Logging.SimpleConsoleLogger')
  - [#ctor()](#M-SigStat-Common-Logging-SimpleConsoleLogger-#ctor 'SigStat.Common.Logging.SimpleConsoleLogger.#ctor')
  - [#ctor(logLevel)](#M-SigStat-Common-Logging-SimpleConsoleLogger-#ctor-Microsoft-Extensions-Logging-LogLevel- 'SigStat.Common.Logging.SimpleConsoleLogger.#ctor(Microsoft.Extensions.Logging.LogLevel)')
  - [LogLevel](#P-SigStat-Common-Logging-SimpleConsoleLogger-LogLevel 'SigStat.Common.Logging.SimpleConsoleLogger.LogLevel')
  - [BeginScope\`\`1()](#M-SigStat-Common-Logging-SimpleConsoleLogger-BeginScope``1-``0- 'SigStat.Common.Logging.SimpleConsoleLogger.BeginScope``1(``0)')
  - [IsEnabled()](#M-SigStat-Common-Logging-SimpleConsoleLogger-IsEnabled-Microsoft-Extensions-Logging-LogLevel- 'SigStat.Common.Logging.SimpleConsoleLogger.IsEnabled(Microsoft.Extensions.Logging.LogLevel)')
  - [Log\`\`1()](#M-SigStat-Common-Logging-SimpleConsoleLogger-Log``1-Microsoft-Extensions-Logging-LogLevel,Microsoft-Extensions-Logging-EventId,``0,System-Exception,System-Func{``0,System-Exception,System-String}- 'SigStat.Common.Logging.SimpleConsoleLogger.Log``1(Microsoft.Extensions.Logging.LogLevel,Microsoft.Extensions.Logging.EventId,``0,System.Exception,System.Func{``0,System.Exception,System.String})')
- [SimpleRenderingTransformation](#T-SigStat-Common-SimpleRenderingTransformation 'SigStat.Common.SimpleRenderingTransformation')
  - [Transform()](#M-SigStat-Common-SimpleRenderingTransformation-Transform-SigStat-Common-Signature- 'SigStat.Common.SimpleRenderingTransformation.Transform(SigStat.Common.Signature)')
- [StrokeHelper](#T-SigStat-Common-StrokeHelper 'SigStat.Common.StrokeHelper')
  - [GetStroke(startIndex,pressure)](#M-SigStat-Common-StrokeHelper-GetStroke-System-Int32,System-Double- 'SigStat.Common.StrokeHelper.GetStroke(System.Int32,System.Double)')
  - [GetStrokes(signature)](#M-SigStat-Common-StrokeHelper-GetStrokes-SigStat-Common-Signature- 'SigStat.Common.StrokeHelper.GetStrokes(SigStat.Common.Signature)')
- [StrokeInterval](#T-SigStat-Common-StrokeInterval 'SigStat.Common.StrokeInterval')
  - [#ctor(startIndex,endIndex,strokeType)](#M-SigStat-Common-StrokeInterval-#ctor-System-Int32,System-Int32,SigStat-Common-StrokeType- 'SigStat.Common.StrokeInterval.#ctor(System.Int32,System.Int32,SigStat.Common.StrokeType)')
  - [EndIndex](#F-SigStat-Common-StrokeInterval-EndIndex 'SigStat.Common.StrokeInterval.EndIndex')
  - [StartIndex](#F-SigStat-Common-StrokeInterval-StartIndex 'SigStat.Common.StrokeInterval.StartIndex')
  - [StrokeType](#F-SigStat-Common-StrokeInterval-StrokeType 'SigStat.Common.StrokeInterval.StrokeType')
- [StrokeType](#T-SigStat-Common-StrokeType 'SigStat.Common.StrokeType')
  - [Down](#F-SigStat-Common-StrokeType-Down 'SigStat.Common.StrokeType.Down')
  - [Unknown](#F-SigStat-Common-StrokeType-Unknown 'SigStat.Common.StrokeType.Unknown')
  - [Up](#F-SigStat-Common-StrokeType-Up 'SigStat.Common.StrokeType.Up')
- [Svc2004](#T-SigStat-Common-Loaders-Svc2004 'SigStat.Common.Loaders.Svc2004')
  - [All](#F-SigStat-Common-Loaders-Svc2004-All 'SigStat.Common.Loaders.Svc2004.All')
  - [Altitude](#F-SigStat-Common-Loaders-Svc2004-Altitude 'SigStat.Common.Loaders.Svc2004.Altitude')
  - [Azimuth](#F-SigStat-Common-Loaders-Svc2004-Azimuth 'SigStat.Common.Loaders.Svc2004.Azimuth')
  - [Button](#F-SigStat-Common-Loaders-Svc2004-Button 'SigStat.Common.Loaders.Svc2004.Button')
  - [Pressure](#F-SigStat-Common-Loaders-Svc2004-Pressure 'SigStat.Common.Loaders.Svc2004.Pressure')
  - [T](#F-SigStat-Common-Loaders-Svc2004-T 'SigStat.Common.Loaders.Svc2004.T')
  - [X](#F-SigStat-Common-Loaders-Svc2004-X 'SigStat.Common.Loaders.Svc2004.X')
  - [Y](#F-SigStat-Common-Loaders-Svc2004-Y 'SigStat.Common.Loaders.Svc2004.Y')
- [Svc2004Loader](#T-SigStat-Common-Loaders-Svc2004Loader 'SigStat.Common.Loaders.Svc2004Loader')
  - [#ctor(databasePath,standardFeatures)](#M-SigStat-Common-Loaders-Svc2004Loader-#ctor-System-String,System-Boolean- 'SigStat.Common.Loaders.Svc2004Loader.#ctor(System.String,System.Boolean)')
  - [#ctor(databasePath,standardFeatures,signerFilter)](#M-SigStat-Common-Loaders-Svc2004Loader-#ctor-System-String,System-Boolean,System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.Svc2004Loader.#ctor(System.String,System.Boolean,System.Predicate{SigStat.Common.Signer})')
  - [DatabasePath](#P-SigStat-Common-Loaders-Svc2004Loader-DatabasePath 'SigStat.Common.Loaders.Svc2004Loader.DatabasePath')
  - [SamplingFrequency](#P-SigStat-Common-Loaders-Svc2004Loader-SamplingFrequency 'SigStat.Common.Loaders.Svc2004Loader.SamplingFrequency')
  - [SignerFilter](#P-SigStat-Common-Loaders-Svc2004Loader-SignerFilter 'SigStat.Common.Loaders.Svc2004Loader.SignerFilter')
  - [StandardFeatures](#P-SigStat-Common-Loaders-Svc2004Loader-StandardFeatures 'SigStat.Common.Loaders.Svc2004Loader.StandardFeatures')
  - [EnumerateSigners()](#M-SigStat-Common-Loaders-Svc2004Loader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}- 'SigStat.Common.Loaders.Svc2004Loader.EnumerateSigners(System.Predicate{SigStat.Common.Signer})')
  - [LoadSignature(signature,path,standardFeatures)](#M-SigStat-Common-Loaders-Svc2004Loader-LoadSignature-SigStat-Common-Signature,System-String,System-Boolean- 'SigStat.Common.Loaders.Svc2004Loader.LoadSignature(SigStat.Common.Signature,System.String,System.Boolean)')
  - [LoadSignature(signature,stream,standardFeatures)](#M-SigStat-Common-Loaders-Svc2004Loader-LoadSignature-SigStat-Common-Signature,System-IO-Stream,System-Boolean- 'SigStat.Common.Loaders.Svc2004Loader.LoadSignature(SigStat.Common.Signature,System.IO.Stream,System.Boolean)')
- [TangentExtraction](#T-SigStat-Common-Transforms-TangentExtraction 'SigStat.Common.Transforms.TangentExtraction')
  - [OutputTangent](#P-SigStat-Common-Transforms-TangentExtraction-OutputTangent 'SigStat.Common.Transforms.TangentExtraction.OutputTangent')
  - [X](#P-SigStat-Common-Transforms-TangentExtraction-X 'SigStat.Common.Transforms.TangentExtraction.X')
  - [Y](#P-SigStat-Common-Transforms-TangentExtraction-Y 'SigStat.Common.Transforms.TangentExtraction.Y')
  - [Transform()](#M-SigStat-Common-Transforms-TangentExtraction-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.TangentExtraction.Transform(SigStat.Common.Signature)')
- [TextLevel](#T-SigStat-Common-Helpers-Excel-TextLevel 'SigStat.Common.Helpers.Excel.TextLevel')
  - [Heading1](#F-SigStat-Common-Helpers-Excel-TextLevel-Heading1 'SigStat.Common.Helpers.Excel.TextLevel.Heading1')
  - [Heading2](#F-SigStat-Common-Helpers-Excel-TextLevel-Heading2 'SigStat.Common.Helpers.Excel.TextLevel.Heading2')
  - [Heading3](#F-SigStat-Common-Helpers-Excel-TextLevel-Heading3 'SigStat.Common.Helpers.Excel.TextLevel.Heading3')
  - [Normal](#F-SigStat-Common-Helpers-Excel-TextLevel-Normal 'SigStat.Common.Helpers.Excel.TextLevel.Normal')
  - [Title](#F-SigStat-Common-Helpers-Excel-TextLevel-Title 'SigStat.Common.Helpers.Excel.TextLevel.Title')
- [TimeReset](#T-SigStat-Common-Transforms-TimeReset 'SigStat.Common.Transforms.TimeReset')
  - [#ctor()](#M-SigStat-Common-Transforms-TimeReset-#ctor 'SigStat.Common.Transforms.TimeReset.#ctor')
- [TimeSlot](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.TimeSlot')
  - [EndTime](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot-EndTime 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.TimeSlot.EndTime')
  - [Length](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot-Length 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.TimeSlot.Length')
  - [PenDown](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot-PenDown 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.TimeSlot.PenDown')
  - [StartTime](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot-StartTime 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations.TimeSlot.StartTime')
- [Translate](#T-SigStat-Common-Transforms-Translate 'SigStat.Common.Transforms.Translate')
  - [#ctor(xAdd,yAdd)](#M-SigStat-Common-Transforms-Translate-#ctor-System-Double,System-Double- 'SigStat.Common.Transforms.Translate.#ctor(System.Double,System.Double)')
  - [#ctor(vectorFeature)](#M-SigStat-Common-Transforms-Translate-#ctor-SigStat-Common-FeatureDescriptor{System-Collections-Generic-List{System-Double}}- 'SigStat.Common.Transforms.Translate.#ctor(SigStat.Common.FeatureDescriptor{System.Collections.Generic.List{System.Double}})')
  - [InputX](#P-SigStat-Common-Transforms-Translate-InputX 'SigStat.Common.Transforms.Translate.InputX')
  - [InputY](#P-SigStat-Common-Transforms-Translate-InputY 'SigStat.Common.Transforms.Translate.InputY')
  - [OutputX](#P-SigStat-Common-Transforms-Translate-OutputX 'SigStat.Common.Transforms.Translate.OutputX')
  - [OutputY](#P-SigStat-Common-Transforms-Translate-OutputY 'SigStat.Common.Transforms.Translate.OutputY')
- [TranslatePreproc](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc')
  - [#ctor()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-#ctor 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc.#ctor')
  - [#ctor(goalOrigin)](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-#ctor-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc.#ctor(SigStat.Common.PipelineItems.Transforms.Preprocessing.OriginType)')
  - [GoalOrigin](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-GoalOrigin 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc.GoalOrigin')
  - [InputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-InputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc.InputFeature')
  - [NewOrigin](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-NewOrigin 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc.NewOrigin')
  - [OutputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-OutputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc.OutputFeature')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc.Transform(SigStat.Common.Signature)')
- [Trim](#T-SigStat-Common-Transforms-Trim 'SigStat.Common.Transforms.Trim')
  - [#ctor(framewidth)](#M-SigStat-Common-Transforms-Trim-#ctor-System-Int32- 'SigStat.Common.Transforms.Trim.#ctor(System.Int32)')
  - [Input](#P-SigStat-Common-Transforms-Trim-Input 'SigStat.Common.Transforms.Trim.Input')
  - [Output](#P-SigStat-Common-Transforms-Trim-Output 'SigStat.Common.Transforms.Trim.Output')
  - [Transform()](#M-SigStat-Common-Transforms-Trim-Transform-SigStat-Common-Signature- 'SigStat.Common.Transforms.Trim.Transform(SigStat.Common.Signature)')
- [UniformScale](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale 'SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale')
  - [BaseDimension](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-BaseDimension 'SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale.BaseDimension')
  - [BaseDimensionOutput](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-BaseDimensionOutput 'SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale.BaseDimensionOutput')
  - [NewMaxBaseValue](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-NewMaxBaseValue 'SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale.NewMaxBaseValue')
  - [NewMinBaseValue](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-NewMinBaseValue 'SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale.NewMinBaseValue')
  - [NewMinProportionalValue](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-NewMinProportionalValue 'SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale.NewMinProportionalValue')
  - [ProportionalDimension](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-ProportionalDimension 'SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale.ProportionalDimension')
  - [ProportionalDimensionOutput](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-ProportionalDimensionOutput 'SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale.ProportionalDimensionOutput')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.UniformScale.Transform(SigStat.Common.Signature)')
- [UniversalSampler](#T-SigStat-Common-Framework-Samplers-UniversalSampler 'SigStat.Common.Framework.Samplers.UniversalSampler')
  - [#ctor(trainingCount,testCount)](#M-SigStat-Common-Framework-Samplers-UniversalSampler-#ctor-System-Int32,System-Int32- 'SigStat.Common.Framework.Samplers.UniversalSampler.#ctor(System.Int32,System.Int32)')
  - [TestCount](#P-SigStat-Common-Framework-Samplers-UniversalSampler-TestCount 'SigStat.Common.Framework.Samplers.UniversalSampler.TestCount')
  - [TrainingCount](#P-SigStat-Common-Framework-Samplers-UniversalSampler-TrainingCount 'SigStat.Common.Framework.Samplers.UniversalSampler.TrainingCount')
- [Verifier](#T-SigStat-Common-Model-Verifier 'SigStat.Common.Model.Verifier')
  - [#ctor(logger)](#M-SigStat-Common-Model-Verifier-#ctor-Microsoft-Extensions-Logging-ILogger- 'SigStat.Common.Model.Verifier.#ctor(Microsoft.Extensions.Logging.ILogger)')
  - [#ctor()](#M-SigStat-Common-Model-Verifier-#ctor 'SigStat.Common.Model.Verifier.#ctor')
  - [#ctor(baseVerifier)](#M-SigStat-Common-Model-Verifier-#ctor-SigStat-Common-Model-Verifier- 'SigStat.Common.Model.Verifier.#ctor(SigStat.Common.Model.Verifier)')
  - [AllFeatures](#P-SigStat-Common-Model-Verifier-AllFeatures 'SigStat.Common.Model.Verifier.AllFeatures')
  - [Classifier](#P-SigStat-Common-Model-Verifier-Classifier 'SigStat.Common.Model.Verifier.Classifier')
  - [Logger](#P-SigStat-Common-Model-Verifier-Logger 'SigStat.Common.Model.Verifier.Logger')
  - [Pipeline](#P-SigStat-Common-Model-Verifier-Pipeline 'SigStat.Common.Model.Verifier.Pipeline')
  - [SignerModel](#P-SigStat-Common-Model-Verifier-SignerModel 'SigStat.Common.Model.Verifier.SignerModel')
  - [Test(signature)](#M-SigStat-Common-Model-Verifier-Test-SigStat-Common-Signature- 'SigStat.Common.Model.Verifier.Test(SigStat.Common.Signature)')
  - [Train(signatures)](#M-SigStat-Common-Model-Verifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}- 'SigStat.Common.Model.Verifier.Train(System.Collections.Generic.List{SigStat.Common.Signature})')
- [VerifierBenchmark](#T-SigStat-Common-VerifierBenchmark 'SigStat.Common.VerifierBenchmark')
  - [#ctor()](#M-SigStat-Common-VerifierBenchmark-#ctor 'SigStat.Common.VerifierBenchmark.#ctor')
  - [loader](#F-SigStat-Common-VerifierBenchmark-loader 'SigStat.Common.VerifierBenchmark.loader')
  - [sampler](#F-SigStat-Common-VerifierBenchmark-sampler 'SigStat.Common.VerifierBenchmark.sampler')
  - [Loader](#P-SigStat-Common-VerifierBenchmark-Loader 'SigStat.Common.VerifierBenchmark.Loader')
  - [Logger](#P-SigStat-Common-VerifierBenchmark-Logger 'SigStat.Common.VerifierBenchmark.Logger')
  - [Parameters](#P-SigStat-Common-VerifierBenchmark-Parameters 'SigStat.Common.VerifierBenchmark.Parameters')
  - [Progress](#P-SigStat-Common-VerifierBenchmark-Progress 'SigStat.Common.VerifierBenchmark.Progress')
  - [Sampler](#P-SigStat-Common-VerifierBenchmark-Sampler 'SigStat.Common.VerifierBenchmark.Sampler')
  - [Verifier](#P-SigStat-Common-VerifierBenchmark-Verifier 'SigStat.Common.VerifierBenchmark.Verifier')
  - [Dump(filename,parameters)](#M-SigStat-Common-VerifierBenchmark-Dump-System-String,System-Collections-Generic-IEnumerable{System-Collections-Generic-KeyValuePair{System-String,System-String}}- 'SigStat.Common.VerifierBenchmark.Dump(System.String,System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}})')
  - [Execute(ParallelMode)](#M-SigStat-Common-VerifierBenchmark-Execute-System-Boolean- 'SigStat.Common.VerifierBenchmark.Execute(System.Boolean)')
  - [Execute(degreeOfParallelism)](#M-SigStat-Common-VerifierBenchmark-Execute-System-Int32- 'SigStat.Common.VerifierBenchmark.Execute(System.Int32)')
- [VerifierResolver](#T-SigStat-Common-Helpers-Serialization-VerifierResolver 'SigStat.Common.Helpers.Serialization.VerifierResolver')
  - [CreateProperties(type,memberSerialization)](#M-SigStat-Common-Helpers-Serialization-VerifierResolver-CreateProperties-System-Type,Newtonsoft-Json-MemberSerialization- 'SigStat.Common.Helpers.Serialization.VerifierResolver.CreateProperties(System.Type,Newtonsoft.Json.MemberSerialization)')
  - [CreateProperty(member,memberSerialization)](#M-SigStat-Common-Helpers-Serialization-VerifierResolver-CreateProperty-System-Reflection-MemberInfo,Newtonsoft-Json-MemberSerialization- 'SigStat.Common.Helpers.Serialization.VerifierResolver.CreateProperty(System.Reflection.MemberInfo,Newtonsoft.Json.MemberSerialization)')
- [WeightedClassifier](#T-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier 'SigStat.Common.PipelineItems.Classifiers.WeightedClassifier')
  - [Items](#F-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-Items 'SigStat.Common.PipelineItems.Classifiers.WeightedClassifier.Items')
  - [Add(newItem)](#M-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-Add-System-ValueTuple{SigStat-Common-Pipeline-IClassifier,System-Double}- 'SigStat.Common.PipelineItems.Classifiers.WeightedClassifier.Add(System.ValueTuple{SigStat.Common.Pipeline.IClassifier,System.Double})')
  - [GetEnumerator()](#M-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-GetEnumerator 'SigStat.Common.PipelineItems.Classifiers.WeightedClassifier.GetEnumerator')
  - [Test()](#M-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-Test-SigStat-Common-Pipeline-ISignerModel,SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Classifiers.WeightedClassifier.Test(SigStat.Common.Pipeline.ISignerModel,SigStat.Common.Signature)')
  - [Train()](#M-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}- 'SigStat.Common.PipelineItems.Classifiers.WeightedClassifier.Train(System.Collections.Generic.List{SigStat.Common.Signature})')
- [ZNormalization](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-ZNormalization 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ZNormalization')
  - [InputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ZNormalization-InputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ZNormalization.InputFeature')
  - [OutputFeature](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ZNormalization-OutputFeature 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ZNormalization.OutputFeature')
  - [Transform()](#M-SigStat-Common-PipelineItems-Transforms-Preprocessing-ZNormalization-Transform-SigStat-Common-Signature- 'SigStat.Common.PipelineItems.Transforms.Preprocessing.ZNormalization.Transform(SigStat.Common.Signature)')

<a name='T-SigStat-Common-Transforms-AddConst'></a>
## AddConst `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Adds a constant value to a feature. Works with collection features too.

Default Pipeline Output: Pipeline Input

<a name='M-SigStat-Common-Transforms-AddConst-#ctor-System-Double-'></a>
### #ctor(value) `constructor`

##### Summary

Initializes a new instance of the [AddConst](#T-SigStat-Common-Transforms-AddConst 'SigStat.Common.Transforms.AddConst') class with specified settings.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The value to be added to the input feature. |

<a name='P-SigStat-Common-Transforms-AddConst-Input'></a>
### Input `property`

##### Summary

Input values for trasformation

<a name='P-SigStat-Common-Transforms-AddConst-Output'></a>
### Output `property`

##### Summary

Output feature to store results

<a name='M-SigStat-Common-Transforms-AddConst-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-AddVector'></a>
## AddVector `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Adds a vector feature's elements to other features.

Default Pipeline Output: Pipeline Input

##### Example

Inputs are: Centroid.xy, X, Y .
Adds Centroid.x to each element of X.
Adds Centroid.y to each element of Y.

<a name='M-SigStat-Common-Transforms-AddVector-#ctor-SigStat-Common-FeatureDescriptor{System-Collections-Generic-List{System-Double}}-'></a>
### #ctor(vectorFeature) `constructor`

##### Summary

Initializes a new instance of the [AddVector](#T-SigStat-Common-Transforms-AddVector 'SigStat.Common.Transforms.AddVector') class with a vector feature.
Don't forget to add as many Inputs as the vector's dimension.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vectorFeature | [SigStat.Common.FeatureDescriptor{System.Collections.Generic.List{System.Double}}](#T-SigStat-Common-FeatureDescriptor{System-Collections-Generic-List{System-Double}} 'SigStat.Common.FeatureDescriptor{System.Collections.Generic.List{System.Double}}') | A collection-type feature where each element represents a dimension of the vector. |

<a name='P-SigStat-Common-Transforms-AddVector-Inputs'></a>
### Inputs `property`

##### Summary

Inputs

<a name='P-SigStat-Common-Transforms-AddVector-Outputs'></a>
### Outputs `property`

##### Summary

Outputs

<a name='M-SigStat-Common-Transforms-AddVector-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-ApproximateOnlineFeatures'></a>
## ApproximateOnlineFeatures `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

init Pressure, Altitude, Azimuth features with default values.

Default Pipeline Output: Features.Pressure, Features.Altitude, Features.Azimuth

<a name='M-SigStat-Common-Transforms-ApproximateOnlineFeatures-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-ArrayExtension'></a>
## ArrayExtension `type`

##### Namespace

SigStat.Common

##### Summary

Helper methods for processing arrays

<a name='M-SigStat-Common-ArrayExtension-GetCog-System-Double[0-,0-]-'></a>
### GetCog(weightMartix) `method`

##### Summary

Calculates the center of gravity, assuming that each cell contains
a weight value

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| weightMartix | [System.Double[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[0: 'System.Double[0:') |  |

<a name='M-SigStat-Common-ArrayExtension-GetValues``1-``0[0-,0-]-'></a>
### GetValues\`\`1(array) `method`

##### Summary

Enumerates all values in a two dimensional array

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| array | [\`\`0[0:](#T-``0[0- '``0[0:') | The array to enumerate |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Array type |

<a name='M-SigStat-Common-ArrayExtension-SetValues``1-``0[0-,0-],``0-'></a>
### SetValues\`\`1(array,value) `method`

##### Summary

Sets all values in a two dimensional array to `value`

##### Returns

A reference to `array` (allows chaining)

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| array | [\`\`0[0:](#T-``0[0- '``0[0:') | Array |
| value | [0:]](#T-0-] '0:]') | New value for the array elements |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Array type |

<a name='M-SigStat-Common-ArrayExtension-Sum-System-Double[0-,0-],System-Int32,System-Int32,System-Int32,System-Int32-'></a>
### Sum(array,x1,y1,x2,y2) `method`

##### Summary

Calculates the sum of the values in the given sub-array

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| array | [System.Double[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[0: 'System.Double[0:') | A two dimensional array with double values |
| x1 | [0:]](#T-0-] '0:]') | First index of the starting point for the region to summarize |
| y1 | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Second index of the starting point for the region to summarize |
| x2 | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | First index of the endpoint for the region to summarize |
| y2 | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Second index of the endpoint for the region to summarize |

<a name='M-SigStat-Common-ArrayExtension-SumCol-System-Double[0-,0-],System-Int32-'></a>
### SumCol(array,column) `method`

##### Summary

Returns the sum of column values in a two dimensional array

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| array | [System.Double[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[0: 'System.Double[0:') | A two dimensional array with double values |
| column | [0:]](#T-0-] '0:]') | The column, to sum |

<a name='M-SigStat-Common-ArrayExtension-SumRow-System-Double[0-,0-],System-Int32-'></a>
### SumRow(array,row) `method`

##### Summary

Returns the sum of row values in a two dimensional array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| array | [System.Double[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[0: 'System.Double[0:') | A two dimensional array with double values |
| row | [0:]](#T-0-] '0:]') | The row, to sum |

<a name='T-SigStat-Common-Pipeline-AutoSetMode'></a>
## AutoSetMode `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Default strategy to set the value of a property

<a name='F-SigStat-Common-Pipeline-AutoSetMode-Always'></a>
### Always `constants`

##### Summary

Always set the value

<a name='F-SigStat-Common-Pipeline-AutoSetMode-IfNull'></a>
### IfNull `constants`

##### Summary

Set the value if it is null

<a name='F-SigStat-Common-Pipeline-AutoSetMode-Never'></a>
### Never `constants`

##### Summary

Never set the value

<a name='T-SigStat-Common-Baseline'></a>
## Baseline `type`

##### Namespace

SigStat.Common

##### Summary



<a name='M-SigStat-Common-Baseline-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a Baseline instance

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-Baseline-#ctor-System-Int32,System-Int32,System-Int32,System-Int32-'></a>
### #ctor(x1,y1,x2,y2) `constructor`

##### Summary

Initializes a Baseline instance with the given startpoint and endpoint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x1 | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | x coordinate for the start point |
| y1 | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | y coordinate for the start point |
| x2 | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | x coordinate for the endpoint |
| y2 | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | y coordinate for the endpoint |

<a name='P-SigStat-Common-Baseline-End'></a>
### End `property`

##### Summary

Endpoint of the baseline

<a name='P-SigStat-Common-Baseline-Start'></a>
### Start `property`

##### Summary

Starting point of the baseline

<a name='M-SigStat-Common-Baseline-ToString'></a>
### ToString() `method`

##### Summary

Returns a string representation of the baseline

##### Returns



##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-BasicMetadataExtraction'></a>
## BasicMetadataExtraction `type`

##### Namespace

SigStat.Common

##### Summary

Extracts basic statistical signature (like [](#!-Features-Bounds 'Features.Bounds') or [Cog](#F-SigStat-Common-Features-Cog 'SigStat.Common.Features.Cog')) information from an Image

<a name='P-SigStat-Common-BasicMetadataExtraction-Trim'></a>
### Trim `property`

##### Summary

Represents theratio of significant pixels that should be trimmed
from each side while calculating [TrimmedBounds](#F-SigStat-Common-Features-TrimmedBounds 'SigStat.Common.Features.TrimmedBounds')

<a name='M-SigStat-Common-BasicMetadataExtraction-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Helpers-BenchmarkConfig'></a>
## BenchmarkConfig `type`

##### Namespace

SigStat.Common.Helpers

##### Summary

Represents a configuration for a benchmark

<a name='M-SigStat-Common-Helpers-BenchmarkConfig-FromJsonFile-System-String-'></a>
### FromJsonFile(path) `method`

##### Summary

Helper

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-SigStat-Common-Helpers-BenchmarkConfig-FromJsonString-System-String-'></a>
### FromJsonString(jsonString) `method`

##### Summary

helper

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| jsonString | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-SigStat-Common-Helpers-BenchmarkConfig-GenerateConfigurations'></a>
### GenerateConfigurations() `method`

##### Summary

Helper

##### Returns



##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-BenchmarkConfig-Samplers-System-Collections-Generic-List{SigStat-Common-Helpers-BenchmarkConfig}-'></a>
### Samplers(l) `method`

##### Summary

Helper

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| l | [System.Collections.Generic.List{SigStat.Common.Helpers.BenchmarkConfig}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{SigStat.Common.Helpers.BenchmarkConfig}') |  |

<a name='M-SigStat-Common-Helpers-BenchmarkConfig-ToJsonString'></a>
### ToJsonString() `method`

##### Summary

Helper

##### Returns



##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-BenchmarkConfig-ToKeyValuePairs'></a>
### ToKeyValuePairs() `method`

##### Summary

Helper

##### Returns



##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-BenchmarkConfig-ToShortString'></a>
### ToShortString() `method`

##### Summary

Helper

##### Returns



##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Logging-BenchmarkKeyValueLogState'></a>
## BenchmarkKeyValueLogState `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Specific state used for Benchmarks key-value information transiting

<a name='M-SigStat-Common-Logging-BenchmarkKeyValueLogState-#ctor-System-String,System-String,System-Object-'></a>
### #ctor(group,key,value) `constructor`

##### Summary

Creates a BenchmarkKeyValueLogState

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| group | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Group |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Key |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Value |

<a name='P-SigStat-Common-Logging-BenchmarkKeyValueLogState-Group'></a>
### Group `property`

##### Summary

Group of the key-value pair

<a name='P-SigStat-Common-Logging-BenchmarkKeyValueLogState-Key'></a>
### Key `property`

##### Summary

Key

<a name='P-SigStat-Common-Logging-BenchmarkKeyValueLogState-Value'></a>
### Value `property`

##### Summary

Value

<a name='T-SigStat-Common-Logging-BenchmarkLogModel'></a>
## BenchmarkLogModel `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Represents the results of a benchmark

<a name='M-SigStat-Common-Logging-BenchmarkLogModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Default constructor creating a blank model.

##### Parameters

This constructor has no parameters.

<a name='F-SigStat-Common-Logging-BenchmarkLogModel-BenchmarkResultsGroupName'></a>
### BenchmarkResultsGroupName `constants`

##### Summary

Name of the "BenchmarkResults" group

<a name='F-SigStat-Common-Logging-BenchmarkLogModel-ExecutionGroupName'></a>
### ExecutionGroupName `constants`

##### Summary

Name of the "Excecution" group

<a name='F-SigStat-Common-Logging-BenchmarkLogModel-ParametersGroupName'></a>
### ParametersGroupName `constants`

##### Summary

Name of the "Parameters" group

<a name='P-SigStat-Common-Logging-BenchmarkLogModel-BenchmarkResults'></a>
### BenchmarkResults `property`

##### Summary

Benchmark results group

<a name='P-SigStat-Common-Logging-BenchmarkLogModel-Excecution'></a>
### Excecution `property`

##### Summary

Excecution group

<a name='P-SigStat-Common-Logging-BenchmarkLogModel-KeyValueGroups'></a>
### KeyValueGroups `property`

##### Summary

Benchmark results stored in Key-Value groups

<a name='P-SigStat-Common-Logging-BenchmarkLogModel-Parameters'></a>
### Parameters `property`

##### Summary

Parameters group

<a name='P-SigStat-Common-Logging-BenchmarkLogModel-SignerResults'></a>
### SignerResults `property`

##### Summary

Results belonging to signers

<a name='T-SigStat-Common-BenchmarkResults'></a>
## BenchmarkResults `type`

##### Namespace

SigStat.Common

##### Summary

Contains the benchmark results of every [Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer') and the summarized final results.

<a name='F-SigStat-Common-BenchmarkResults-FinalResult'></a>
### FinalResult `constants`

##### Summary

Summarized, final result of the benchmark execution.

<a name='F-SigStat-Common-BenchmarkResults-SignerResults'></a>
### SignerResults `constants`

##### Summary

List that contains the [Result](#T-SigStat-Common-Result 'SigStat.Common.Result')s for each [Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer')

<a name='T-SigStat-Common-Logging-BenchmarkResultsLogState'></a>
## BenchmarkResultsLogState `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Specific state used for Benchmark result transiting

<a name='M-SigStat-Common-Logging-BenchmarkResultsLogState-#ctor-System-Double,System-Double,System-Double-'></a>
### #ctor(aer,far,frr) `constructor`

##### Summary

Creates a BenchmarkResultsLogState

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aer | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Aer |
| far | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Far |
| frr | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Frr |

<a name='P-SigStat-Common-Logging-BenchmarkResultsLogState-Aer'></a>
### Aer `property`

##### Summary

Average error rate

<a name='P-SigStat-Common-Logging-BenchmarkResultsLogState-Far'></a>
### Far `property`

##### Summary

False accaptance rate

<a name='P-SigStat-Common-Logging-BenchmarkResultsLogState-Frr'></a>
### Frr `property`

##### Summary

False rejection rate

<a name='T-SigStat-Common-Transforms-Binarization'></a>
## Binarization `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Generates a binary raster version of the input image with the iterative threshold method.

Pipeline Input type: Image{Rgba32}

Default Pipeline Output: (bool[,]) Binarized

<a name='M-SigStat-Common-Transforms-Binarization-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [Binarization](#T-SigStat-Common-Transforms-Binarization 'SigStat.Common.Transforms.Binarization') class with default settings: Iterative threshold and [Dark](#F-SigStat-Common-Transforms-Binarization-ForegroundType-Dark 'SigStat.Common.Transforms.Binarization.ForegroundType.Dark').

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-Transforms-Binarization-#ctor-SigStat-Common-Transforms-Binarization-ForegroundType,System-Nullable{System-Double}-'></a>
### #ctor(foregroundType,binThreshold) `constructor`

##### Summary

Initializes a new instance of the [Binarization](#T-SigStat-Common-Transforms-Binarization 'SigStat.Common.Transforms.Binarization') class with specified settings.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| foregroundType | [SigStat.Common.Transforms.Binarization.ForegroundType](#T-SigStat-Common-Transforms-Binarization-ForegroundType 'SigStat.Common.Transforms.Binarization.ForegroundType') |  |
| binThreshold | [System.Nullable{System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Double}') | Use this threshold value instead of iteratively calculating it. Range from 0 to 1 |

<a name='P-SigStat-Common-Transforms-Binarization-InputImage'></a>
### InputImage `property`

##### Summary

Gets or sets the featuredescriptor of the input image.

<a name='P-SigStat-Common-Transforms-Binarization-OutputMask'></a>
### OutputMask `property`

##### Summary

Gets or sets the featuredescriptor of a the binarized image.

<a name='M-SigStat-Common-Transforms-Binarization-IterativeThreshold-SixLabors-ImageSharp-Image{SixLabors-ImageSharp-PixelFormats-Rgba32},System-Double-'></a>
### IterativeThreshold(image,maxError) `method`

##### Summary

http://accord-framework.net/docs/html/T_Accord_Imaging_Filters_IterativeThreshold.htm

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| image | [SixLabors.ImageSharp.Image{SixLabors.ImageSharp.PixelFormats.Rgba32}](#T-SixLabors-ImageSharp-Image{SixLabors-ImageSharp-PixelFormats-Rgba32} 'SixLabors.ImageSharp.Image{SixLabors.ImageSharp.PixelFormats.Rgba32}') |  |
| maxError | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | pl 0.008 |

<a name='M-SigStat-Common-Transforms-Binarization-Level-SixLabors-ImageSharp-PixelFormats-Rgba32-'></a>
### Level(c) `method`

##### Summary

Extracts the brightness of the input color. Ranges from 0.0 to 1.0

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| c | [SixLabors.ImageSharp.PixelFormats.Rgba32](#T-SixLabors-ImageSharp-PixelFormats-Rgba32 'SixLabors.ImageSharp.PixelFormats.Rgba32') |  |

<a name='M-SigStat-Common-Transforms-Binarization-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-BinaryRasterizer'></a>
## BinaryRasterizer `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Converts standard features to a binary raster.

Default Pipeline Input: Standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

Default Pipeline Output: (bool[,]) Binarized

<a name='M-SigStat-Common-Transforms-BinaryRasterizer-#ctor-System-Int32,System-Int32,System-Single-'></a>
### #ctor(resolutionX,resolutionY,penWidth) `constructor`

##### Summary

Initializes a new instance of the [BinaryRasterizer](#T-SigStat-Common-Transforms-BinaryRasterizer 'SigStat.Common.Transforms.BinaryRasterizer') class with specified raster size and pen width.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resolutionX | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Raster width. |
| resolutionY | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Raster height. |
| penWidth | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') |  |

<a name='P-SigStat-Common-Transforms-BinaryRasterizer-InputButton'></a>
### InputButton `property`

##### Summary

Gets or sets the [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') representing the stroke endings of an online signature

<a name='P-SigStat-Common-Transforms-BinaryRasterizer-InputX'></a>
### InputX `property`

##### Summary

Gets or sets the [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') representing the X coordinates of an online signature

<a name='P-SigStat-Common-Transforms-BinaryRasterizer-InputY'></a>
### InputY `property`

##### Summary

Gets or sets the [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') representing the Y coordinates of an online signature

<a name='P-SigStat-Common-Transforms-BinaryRasterizer-Output'></a>
### Output `property`

##### Summary

Gets or sets the [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') representing the output of the transformation

<a name='M-SigStat-Common-Transforms-BinaryRasterizer-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-CentroidExtraction'></a>
## CentroidExtraction `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Extracts the Centroid (aka. Center Of Gravity) of the input features.

Default Pipeline Output: (List{double}) Centroid.

<a name='P-SigStat-Common-Transforms-CentroidExtraction-Inputs'></a>
### Inputs `property`

##### Summary

List of features to process

<a name='P-SigStat-Common-Transforms-CentroidExtraction-OutputCentroid'></a>
### OutputCentroid `property`

##### Summary

List of centroid values

<a name='M-SigStat-Common-Transforms-CentroidExtraction-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-CentroidTranslate'></a>
## CentroidTranslate `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Sequential pipeline to translate X and Y [Features](#T-SigStat-Common-Features 'SigStat.Common.Features') to Centroid.
The following Transforms are called: [CentroidExtraction](#T-SigStat-Common-Transforms-CentroidExtraction 'SigStat.Common.Transforms.CentroidExtraction'), [Multiply](#T-SigStat-Common-Transforms-Multiply 'SigStat.Common.Transforms.Multiply')(-1), [Translate](#T-SigStat-Common-Transforms-Translate 'SigStat.Common.Transforms.Translate')

Default Pipeline Input: [X](#F-SigStat-Common-Features-X 'SigStat.Common.Features.X'), [Y](#F-SigStat-Common-Features-Y 'SigStat.Common.Features.Y')

Default Pipeline Output: (List{double}) Centroid

##### Remarks

This is a special case of [Translate](#T-SigStat-Common-Transforms-Translate 'SigStat.Common.Transforms.Translate')

<a name='M-SigStat-Common-Transforms-CentroidTranslate-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [CentroidTranslate](#T-SigStat-Common-Transforms-CentroidTranslate 'SigStat.Common.Transforms.CentroidTranslate') class.

##### Parameters

This constructor has no parameters.

<a name='P-SigStat-Common-Transforms-CentroidTranslate-InputX'></a>
### InputX `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-Transforms-CentroidTranslate-InputY'></a>
### InputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='P-SigStat-Common-Transforms-CentroidTranslate-OutputX'></a>
### OutputX `property`

##### Summary

Gets or sets the output feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-Transforms-CentroidTranslate-OutputY'></a>
### OutputY `property`

##### Summary

Gets or sets the output feature representing the X coordinates of an online signature

<a name='T-SigStat-Common-Logging-ClassifierDistanceLogState'></a>
## ClassifierDistanceLogState `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Specific state for signature distance information transiting

<a name='M-SigStat-Common-Logging-ClassifierDistanceLogState-#ctor-System-String,System-String,System-String,System-String,System-Double-'></a>
### #ctor(signer1Id,signer2Id,signature1Id,signature2Id,distance) `constructor`

##### Summary

Creates a ClassifierDistanceLogState

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signer1Id | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the first signature's signer |
| signer2Id | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the second signature's signer |
| signature1Id | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the first signature |
| signature2Id | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the second signature |
| distance | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Distance values between the signatures |

<a name='P-SigStat-Common-Logging-ClassifierDistanceLogState-Signature1Id'></a>
### Signature1Id `property`

##### Summary

Id of the first signature

<a name='P-SigStat-Common-Logging-ClassifierDistanceLogState-Signature2Id'></a>
### Signature2Id `property`

##### Summary

Id of the second signature

<a name='P-SigStat-Common-Logging-ClassifierDistanceLogState-Signer1Id'></a>
### Signer1Id `property`

##### Summary

Id of the first signature's signer

<a name='P-SigStat-Common-Logging-ClassifierDistanceLogState-Signer2Id'></a>
### Signer2Id `property`

##### Summary

/// Id of the second signature's signer

<a name='P-SigStat-Common-Logging-ClassifierDistanceLogState-distance'></a>
### distance `property`

##### Summary

Distance values between the signatures

<a name='T-SigStat-Common-Transforms-ComponentExtraction'></a>
## ComponentExtraction `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Extracts unsorted components by tracing through the binary Skeleton raster.

Default Pipeline Input: (bool[,]) Skeleton, (List{Point}) EndPoints, (List{Point}) CrossingPoints

Default Pipeline Output: (List{List{PointF}}) Components

<a name='M-SigStat-Common-Transforms-ComponentExtraction-#ctor-System-Int32-'></a>
### #ctor(samplingResolution) `constructor`

##### Summary

Initializes a new instance of the [ComponentExtraction](#T-SigStat-Common-Transforms-ComponentExtraction 'SigStat.Common.Transforms.ComponentExtraction') class with specified sampling resolution.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| samplingResolution | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Steps to trace before a new point is sampled. Smaller values result in a more precise tracing. Provide a positive value. |

<a name='P-SigStat-Common-Transforms-ComponentExtraction-CrossingPoints'></a>
### CrossingPoints `property`

##### Summary

crossing points

<a name='P-SigStat-Common-Transforms-ComponentExtraction-EndPoints'></a>
### EndPoints `property`

##### Summary

endpoints

<a name='P-SigStat-Common-Transforms-ComponentExtraction-OutputComponents'></a>
### OutputComponents `property`

##### Summary

Output components

<a name='P-SigStat-Common-Transforms-ComponentExtraction-Skeleton'></a>
### Skeleton `property`

##### Summary

binary representation of a signature image

<a name='M-SigStat-Common-Transforms-ComponentExtraction-SplitCrossings-System-Collections-Generic-List{System-Drawing-Point}-'></a>
### SplitCrossings(crs) `method`

##### Summary

Unite crossingpoints into crossings (list of its endpoints), and
split all crossings into neighbouring endpoints.

##### Returns

List of crossings (1 crossing: List of endpoints)

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| crs | [System.Collections.Generic.List{System.Drawing.Point}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.Drawing.Point}') | List of crossingpoints |

<a name='M-SigStat-Common-Transforms-ComponentExtraction-Trace-System-Collections-Generic-List{System-Drawing-Point}-'></a>
### Trace(endPoints) `method`

##### Summary

lekoveti a szakaszokat. Ebbe mar ne legyenek crossingpointok

##### Returns

List of sections

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| endPoints | [System.Collections.Generic.List{System.Drawing.Point}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.Drawing.Point}') |  |

<a name='M-SigStat-Common-Transforms-ComponentExtraction-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-ComponentSorter'></a>
## ComponentSorter `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Sorts Component order by comparing each starting X value, and finding nearest components.

Default Pipeline Input: (bool[,]) Components

Default Pipeline Output: (bool[,]) Components

<a name='P-SigStat-Common-Transforms-ComponentSorter-Input'></a>
### Input `property`

##### Summary

Gets or sets the input.

<a name='P-SigStat-Common-Transforms-ComponentSorter-Output'></a>
### Output `property`

##### Summary

Gets or sets the output.

<a name='M-SigStat-Common-Transforms-ComponentSorter-Distance-System-Collections-Generic-List{System-Drawing-PointF},System-Collections-Generic-List{System-Drawing-PointF}-'></a>
### Distance() `method`

##### Summary

Calculates distance between two components by comparing last and first points.
Components that are left behind are in advantage.

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Transforms-ComponentSorter-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-ComponentsToFeatures'></a>
## ComponentsToFeatures `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Extracts standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features') from sorted Components.

Default Pipeline Input: (List{List{PointF}}) Components

Default Pipeline Output: X, Y, Button [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

<a name='P-SigStat-Common-Transforms-ComponentsToFeatures-Button'></a>
### Button `property`

##### Summary

Button

<a name='P-SigStat-Common-Transforms-ComponentsToFeatures-InputComponents'></a>
### InputComponents `property`

##### Summary

Components

<a name='P-SigStat-Common-Transforms-ComponentsToFeatures-X'></a>
### X `property`

##### Summary

X

<a name='P-SigStat-Common-Transforms-ComponentsToFeatures-Y'></a>
### Y `property`

##### Summary

Y

<a name='M-SigStat-Common-Transforms-ComponentsToFeatures-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Logging-CompositeLogger'></a>
## CompositeLogger `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Forwards messages to [ILogger](#T-Microsoft-Extensions-Logging-ILogger 'Microsoft.Extensions.Logging.ILogger') components.

<a name='P-SigStat-Common-Logging-CompositeLogger-Loggers'></a>
### Loggers `property`

##### Summary

The list of [ILogger](#T-Microsoft-Extensions-Logging-ILogger 'Microsoft.Extensions.Logging.ILogger') components that messages are forwarded to. Empty by default.

<a name='M-SigStat-Common-Logging-CompositeLogger-BeginScope``1-``0-'></a>
### BeginScope\`\`1() `method`

##### Summary

Calls [BeginScope\`\`1](#M-Microsoft-Extensions-Logging-ILogger-BeginScope``1-``0- 'Microsoft.Extensions.Logging.ILogger.BeginScope``1(``0)') on each component.

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Logging-CompositeLogger-IsEnabled-Microsoft-Extensions-Logging-LogLevel-'></a>
### IsEnabled() `method`

##### Summary

Returns true if any of the [ILogger](#T-Microsoft-Extensions-Logging-ILogger 'Microsoft.Extensions.Logging.ILogger') components are enabled on the specified [LogLevel](#T-Microsoft-Extensions-Logging-LogLevel 'Microsoft.Extensions.Logging.LogLevel').

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Logging-CompositeLogger-Log``1-Microsoft-Extensions-Logging-LogLevel,Microsoft-Extensions-Logging-EventId,``0,System-Exception,System-Func{``0,System-Exception,System-String}-'></a>
### Log\`\`1() `method`

##### Summary

Forwards the message to each [ILogger](#T-Microsoft-Extensions-Logging-ILogger 'Microsoft.Extensions.Logging.ILogger') component.

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Logging-SimpleConsoleLogger-ConsoleMessageLoggedEventHandler'></a>
## ConsoleMessageLoggedEventHandler `type`

##### Namespace

SigStat.Common.Logging.SimpleConsoleLogger

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| consoleMessage | [T:SigStat.Common.Logging.SimpleConsoleLogger.ConsoleMessageLoggedEventHandler](#T-T-SigStat-Common-Logging-SimpleConsoleLogger-ConsoleMessageLoggedEventHandler 'T:SigStat.Common.Logging.SimpleConsoleLogger.ConsoleMessageLoggedEventHandler') |  |

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-CubicInterpolation'></a>
## CubicInterpolation `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Cubic interpolation algorithm

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-CubicInterpolation-FeatureValues'></a>
### FeatureValues `property`

##### Summary

FeatureValues

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-CubicInterpolation-TimeValues'></a>
### TimeValues `property`

##### Summary

TimeValues

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-CubicInterpolation-GetValue-System-Double-'></a>
### GetValue(timestamp) `method`

##### Summary

Gets the value.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| timestamp | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The timestamp. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NullReferenceException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NullReferenceException 'System.NullReferenceException') | List of timestamps is null
or
List of feature values is null |
| [System.ArgumentOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') | The given timestamp is not in the range of TimeValues |

<a name='T-SigStat-Common-Helpers-DataCleaningHelper'></a>
## DataCleaningHelper `type`

##### Namespace

SigStat.Common.Helpers

##### Summary

Helper class for cleaning online signature data in loaders

<a name='M-SigStat-Common-Helpers-DataCleaningHelper-Insert2DPointsForGapBorders-System-Int32[],SigStat-Common-Signature,System-Double-'></a>
### Insert2DPointsForGapBorders(gapIndexes,signature,unitTimeSlot) `method`

##### Summary

Inserts two points as border points of gaps in an online signature.
The inserted values are X- and Y-coordinates and a calculated timestamp.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gapIndexes | [System.Int32[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32[] 'System.Int32[]') | Indexes of points where the gaps end in the signature |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | The online signature in which the points are inserted |
| unitTimeSlot | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The unit time slot between two points of the signature |

<a name='M-SigStat-Common-Helpers-DataCleaningHelper-InsertDuplicatedValuesForGapBorderPoints``1-System-Int32[],System-Collections-Generic-List{``0}-'></a>
### InsertDuplicatedValuesForGapBorderPoints\`\`1(gapIndexes,featureValues) `method`

##### Summary

Insert feature values for border points of gaps in an online signature using duplicated neighbour values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gapIndexes | [System.Int32[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32[] 'System.Int32[]') | Indexes of points where the gaps end in the signature |
| featureValues | [System.Collections.Generic.List{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{``0}') | Feature values of the signature |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Type of the values in featureValues |

<a name='M-SigStat-Common-Helpers-DataCleaningHelper-InsertPenUpValuesForGapBorderPoints-System-Int32[],System-Collections-Generic-List{System-Boolean}-'></a>
### InsertPenUpValuesForGapBorderPoints(gapIndexes,penDownValues) `method`

##### Summary

Insert PenUp values for border points of gaps in an online signature

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gapIndexes | [System.Int32[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32[] 'System.Int32[]') | Indexes of points where the gaps end in the signature |
| penDownValues | [System.Collections.Generic.List{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.Boolean}') | PenDown values of the signature |

<a name='M-SigStat-Common-Helpers-DataCleaningHelper-InsertPressureValuesForGapBorderPoints-System-Int32[],System-Collections-Generic-List{System-Double}-'></a>
### InsertPressureValuesForGapBorderPoints(gapIndexes,pressureValues) `method`

##### Summary

Insert zero pressure values for border points of gaps in an online signature

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gapIndexes | [System.Int32[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32[] 'System.Int32[]') | Indexes of points where the gaps end in the signature |
| pressureValues | [System.Collections.Generic.List{System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.Double}') | Pressure values of the signature |

<a name='M-SigStat-Common-Helpers-DataCleaningHelper-InsertTimestampsForGapBorderPoints-System-Int32[],System-Collections-Generic-List{System-Double},System-Double-'></a>
### InsertTimestampsForGapBorderPoints(gapIndexes,timestamps,difference) `method`

##### Summary

Inserts timestamps for border points of gaps in an online signature

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gapIndexes | [System.Int32[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32[] 'System.Int32[]') | Indexes of points where the gaps end in the signature |
| timestamps | [System.Collections.Generic.List{System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.Double}') | Timestamps of the signature |
| difference | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Defines the difference between the inserted timestamps and their neighbour timestamp |

<a name='T-SigStat-Common-Loaders-DataSetLoader'></a>
## DataSetLoader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

Abstract loader class to inherit from. Implements ILogger.

<a name='P-SigStat-Common-Loaders-DataSetLoader-Logger'></a>
### Logger `property`

##### Summary

*Inherit from parent.*

<a name='P-SigStat-Common-Loaders-DataSetLoader-SamplingFrequency'></a>
### SamplingFrequency `property`

##### Summary

Sampling frequency for each database

<a name='M-SigStat-Common-Loaders-DataSetLoader-EnumerateSigners'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-DataSetLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Helpers-Serialization-DistanceFunctionJsonConverter'></a>
## DistanceFunctionJsonConverter `type`

##### Namespace

SigStat.Common.Helpers.Serialization

##### Summary

Helper class for serializing distance functions

##### See Also

- [Newtonsoft.Json.JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter')

<a name='T-SigStat-Common-Helpers-Serialization-DistanceMatrixConverter'></a>
## DistanceMatrixConverter `type`

##### Namespace

SigStat.Common.Helpers.Serialization

##### Summary

Serializes/Deserializes a [](#!-DistanceMatrix<string,string,double> 'DistanceMatrix<string,string,double>') object using its ToArray() and FromArray() methods.

<a name='M-SigStat-Common-Helpers-Serialization-DistanceMatrixConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,SigStat-Common-DistanceMatrix{System-String,System-String,System-Double},System-Boolean,Newtonsoft-Json-JsonSerializer-'></a>
### ReadJson() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-Serialization-DistanceMatrixConverter-WriteJson-Newtonsoft-Json-JsonWriter,SigStat-Common-DistanceMatrix{System-String,System-String,System-Double},Newtonsoft-Json-JsonSerializer-'></a>
### WriteJson() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-DistanceMatrix`3'></a>
## DistanceMatrix\`3 `type`

##### Namespace

SigStat.Common

##### Summary

A Sparse Matrix representation of a distance graph.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TRowKey | Type to represent the row indexes |
| TColumnKey | Type to represent the column indexes |
| TValue | Type to represent the distances |

<a name='P-SigStat-Common-DistanceMatrix`3-Item-`0,`1-'></a>
### Item `property`

##### Summary

Gets or sets a distance for a given row and column

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| row | [\`0](#T-`0 '`0') | row |
| column | [\`1](#T-`1 '`1') | column |

<a name='M-SigStat-Common-DistanceMatrix`3-ContainsKey-`0,`1-'></a>
### ContainsKey(row,column) `method`

##### Summary

Determines whether the Matrix contains the specified key pair

##### Returns

true if the Matrix contains an element with the specified keys; otherwise, false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| row | [\`0](#T-`0 '`0') |  |
| column | [\`1](#T-`1 '`1') |  |

<a name='M-SigStat-Common-DistanceMatrix`3-GetValues'></a>
### GetValues() `method`

##### Summary

Enumerates all values stored on the Matrix

##### Returns



##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-DistanceMatrix`3-ToArray'></a>
### ToArray() `method`

##### Summary

Generates a two dimensional array representation of the Matrix

##### Returns

a two dimensional array representation of the Matrix, where the first row and column contain the corresponding row and column indexes

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-DistanceMatrix`3-TryGetValue-`0,`1,`2@-'></a>
### TryGetValue(row,column,value) `method`

##### Summary

Gets the value associated with the specified keys.

##### Returns

true if the Matrix contains an element with the specified keys; otherwise, false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| row | [\`0](#T-`0 '`0') |  |
| column | [\`1](#T-`1 '`1') |  |
| value | [\`2@](#T-`2@ '`2@') |  |

<a name='T-SigStat-Common-Algorithms-Dtw'></a>
## Dtw `type`

##### Namespace

SigStat.Common.Algorithms

##### Summary

Dynamic Time Warping algorithm

<a name='M-SigStat-Common-Algorithms-Dtw-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initialize the DTW algorithm with the default Euclidean distance method.

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-Algorithms-Dtw-#ctor-System-Func{System-Double[],System-Double[],System-Double}-'></a>
### #ctor(distMethod) `constructor`

##### Summary

Initialize the DTW algorithm with given distance method.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| distMethod | [System.Func{System.Double[],System.Double[],System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Double[],System.Double[],System.Double}') | Accord.Math.Distance.* |

<a name='P-SigStat-Common-Algorithms-Dtw-ForwardPath'></a>
### ForwardPath `property`

##### Summary

Gets the list of points representing the shortest path.

<a name='M-SigStat-Common-Algorithms-Dtw-Compute-System-Double[][],System-Double[][]-'></a>
### Compute() `method`

##### Summary

Generate shortest path between the two sequences.

##### Returns

Cost of the path.

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Algorithms-Dtw-Distance-System-Double[],System-Double[]-'></a>
### Distance(p1,p2) `method`

##### Summary

Calculates distance between two points.
Distance method can be set in ctor.

##### Returns

Distance between `p1` and `p2`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| p1 | [System.Double[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[] 'System.Double[]') | Point 1 |
| p2 | [System.Double[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[] 'System.Double[]') | Point 2 |

<a name='T-SigStat-Common-PipelineItems-Classifiers-DtwClassifier'></a>
## DtwClassifier `type`

##### Namespace

SigStat.Common.PipelineItems.Classifiers

##### Summary

Classifies Signatures with the [Dtw](#T-SigStat-Common-Algorithms-Dtw 'SigStat.Common.Algorithms.Dtw') algorithm.

<a name='M-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [DtwClassifier](#T-SigStat-Common-PipelineItems-Classifiers-DtwClassifier 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier') class with the default Manhattan distance method.

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-#ctor-System-Func{System-Double[],System-Double[],System-Double}-'></a>
### #ctor(distanceMethod) `constructor`

##### Summary

Initializes a new instance of the [DtwClassifier](#T-SigStat-Common-PipelineItems-Classifiers-DtwClassifier 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier') class with a specified distance method.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| distanceMethod | [System.Func{System.Double[],System.Double[],System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Double[],System.Double[],System.Double}') | Accord.Math.Distance.* |

<a name='P-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-DistanceFunction'></a>
### DistanceFunction `property`

##### Summary

The function used to calculate the distance between two data points during DTW calculation

<a name='P-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-Features'></a>
### Features `property`

##### Summary

Gets or sets the features to consider during distance calculation

<a name='P-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-MultiplicationFactor'></a>
### MultiplicationFactor `property`

##### Summary

Gets or sets the multiplication factor to be used during threshold calculation

<a name='M-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-Test-SigStat-Common-Pipeline-ISignerModel,SigStat-Common-Signature-'></a>
### Test() `method`

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-PipelineItems-Classifiers-DtwClassifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}-'></a>
### Train() `method`

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Algorithms-DtwExperiments'></a>
## DtwExperiments `type`

##### Namespace

SigStat.Common.Algorithms

##### Summary

A simple implementation of the DTW algorithm.

<a name='M-SigStat-Common-Algorithms-DtwExperiments-ConstrainedDTw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double},System-Int32-'></a>
### ConstrainedDTw\`\`1(sequence1,sequence2,distance,w) `method`

##### Summary

Constrained DTW implementation  (Abdullah Mueen, Eamonn J. Keogh)

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sequence1 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | The sequence1. |
| sequence2 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | The sequence2. |
| distance | [System.Func{\`\`0,\`\`0,System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,``0,System.Double}') | The distance. |
| w | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The w. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| P |  |

##### Remarks

Bases on: Abdullah Mueen, Eamonn J. Keogh: Extracting Optimal
Performance from Dynamic Time Warping.KDD 2016: 2129-2130

<a name='M-SigStat-Common-Algorithms-DtwExperiments-ConstrainedDtwWikipedia``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double},System-Int32-'></a>
### ConstrainedDtwWikipedia\`\`1(sequence1,sequence2,distance,w) `method`

##### Summary

Constrained DTW implementation  (Wikipedia)

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sequence1 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | The sequence1. |
| sequence2 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | The sequence2. |
| distance | [System.Func{\`\`0,\`\`0,System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,``0,System.Double}') | The distance. |
| w | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The w. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| P |  |

##### Remarks

https://en.wikipedia.org/wiki/Dynamic_time_warping

<a name='M-SigStat-Common-Algorithms-DtwExperiments-ExactDTw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double}-'></a>
### ExactDTw\`\`1(sequence1,sequence2,distance) `method`

##### Summary

Exact DTW implementation (Abdullah Mueen, Eamonn J. Keogh)

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sequence1 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | The sequence1. |
| sequence2 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | The sequence2. |
| distance | [System.Func{\`\`0,\`\`0,System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,``0,System.Double}') | The distance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| P |  |

##### Remarks

Bases on: Abdullah Mueen, Eamonn J. Keogh: Extracting Optimal
Performance from Dynamic Time Warping.KDD 2016: 2129-2130

<a name='M-SigStat-Common-Algorithms-DtwExperiments-ExactDtwWikipedia``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double}-'></a>
### ExactDtwWikipedia\`\`1(sequence1,sequence2,distance) `method`

##### Summary

Exact DTW implementation (Wikipedia)

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sequence1 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | The sequence1. |
| sequence2 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | The sequence2. |
| distance | [System.Func{\`\`0,\`\`0,System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,``0,System.Double}') | The distance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| P |  |

##### Remarks

https://en.wikipedia.org/wiki/Dynamic_time_warping

<a name='M-SigStat-Common-Algorithms-DtwExperiments-OptimizedDtw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double},System-Int32,System-Int32-'></a>
### OptimizedDtw\`\`1(sequence1,sequence2,distance,m,r) `method`

##### Summary

Complex, optimized DTW calculation (Abdullah Mueen, Eamonn J. Keogh)

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sequence1 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') |  |
| sequence2 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') |  |
| distance | [System.Func{\`\`0,\`\`0,System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,``0,System.Double}') |  |
| m | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |
| r | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| P |  |

##### Remarks

Bases on: Abdullah Mueen, Eamonn J. Keogh: Extracting Optimal
Performance from Dynamic Time Warping.KDD 2016: 2129-2130

<a name='T-SigStat-Common-Algorithms-DtwPy'></a>
## DtwPy `type`

##### Namespace

SigStat.Common.Algorithms

##### Summary

A simple implementation of the DTW algorithm.

<a name='M-SigStat-Common-Algorithms-DtwPy-Dtw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double}-'></a>
### Dtw\`\`1(sequence1,sequence2,distance) `method`

##### Summary

Calculates the distance between two time sequences

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sequence1 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | time sequence 1 |
| sequence2 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | time sequence 2 |
| distance | [System.Func{\`\`0,\`\`0,System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,``0,System.Double}') | a function to calculate the distance between two points |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| P | the type of data points |

<a name='M-SigStat-Common-Algorithms-DtwPy-EuclideanDistance-System-Double[],System-Double[]-'></a>
### EuclideanDistance(vector1,vector2) `method`

##### Summary

Calculates the euclidean distance of two vectors

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vector1 | [System.Double[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[] 'System.Double[]') | vector1 |
| vector2 | [System.Double[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[] 'System.Double[]') | vector2 |

##### Remarks

The two vectors must have the same length

<a name='T-SigStat-Common-Algorithms-DtwPyWindow'></a>
## DtwPyWindow `type`

##### Namespace

SigStat.Common.Algorithms

##### Summary

A simple implementation of the DTW algorithm.

<a name='M-SigStat-Common-Algorithms-DtwPyWindow-Dtw``1-System-Collections-Generic-IEnumerable{``0},System-Collections-Generic-IEnumerable{``0},System-Func{``0,``0,System-Double},System-Int32-'></a>
### Dtw\`\`1(sequence1,sequence2,distance) `method`

##### Summary

Calculates the distance between two time sequences

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sequence1 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | time sequence 1 |
| sequence2 | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | time sequence 2 |
| distance | [System.Func{\`\`0,\`\`0,System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,``0,System.Double}') | a function to calculate the distance between two points |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| P | the type of data points |

<a name='M-SigStat-Common-Algorithms-DtwPyWindow-EuclideanDistance-System-Double[],System-Double[]-'></a>
### EuclideanDistance(vector1,vector2) `method`

##### Summary

Calculates the euclidean distance of two vectors

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vector1 | [System.Double[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[] 'System.Double[]') | vector1 |
| vector2 | [System.Double[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[] 'System.Double[]') | vector2 |

##### Remarks

The two vectors must have the same length

<a name='T-SigStat-Common-PipelineItems-Classifiers-DtwSignerModel'></a>
## DtwSignerModel `type`

##### Namespace

SigStat.Common.PipelineItems.Classifiers

##### Summary

Represents a trained model for [DtwClassifier](#T-SigStat-Common-PipelineItems-Classifiers-DtwClassifier 'SigStat.Common.PipelineItems.Classifiers.DtwClassifier')

<a name='F-SigStat-Common-PipelineItems-Classifiers-DtwSignerModel-DistanceMatrix'></a>
### DistanceMatrix `constants`

##### Summary

DTW distance matrix of the genuine signatures

<a name='F-SigStat-Common-PipelineItems-Classifiers-DtwSignerModel-Threshold'></a>
### Threshold `constants`

##### Summary

A threshold, that will be used for classification. Signatures with
an average DTW distance from the genuines above this threshold will
be classified as forgeries

<a name='P-SigStat-Common-PipelineItems-Classifiers-DtwSignerModel-GenuineSignatures'></a>
### GenuineSignatures `property`

##### Summary

A list a of genuine signatures used for training

<a name='T-SigStat-Common-Transforms-EndpointExtraction'></a>
## EndpointExtraction `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Extracts EndPoints and CrossingPoints from Skeleton.

Default Pipeline Input: (bool[,]) Skeleton

Default Pipeline Output: (List{Point}) EndPoints, (List{Point}) CrossingPoints

<a name='P-SigStat-Common-Transforms-EndpointExtraction-OutputCrossingPoints'></a>
### OutputCrossingPoints `property`

##### Summary

OutputCrossingPoints

<a name='P-SigStat-Common-Transforms-EndpointExtraction-OutputEndpoints'></a>
### OutputEndpoints `property`

##### Summary

OutputEndpoints

<a name='P-SigStat-Common-Transforms-EndpointExtraction-Skeleton'></a>
### Skeleton `property`

##### Summary

Binary representation of an image

<a name='M-SigStat-Common-Transforms-EndpointExtraction-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Logging-CompositeLogger-ErrorEventHandler'></a>
## ErrorEventHandler `type`

##### Namespace

SigStat.Common.Logging.CompositeLogger

##### Summary

The event is raised whenever an error is logged.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [T:SigStat.Common.Logging.CompositeLogger.ErrorEventHandler](#T-T-SigStat-Common-Logging-CompositeLogger-ErrorEventHandler 'T:SigStat.Common.Logging.CompositeLogger.ErrorEventHandler') | The message. |

<a name='T-SigStat-Common-ErrorRate'></a>
## ErrorRate `type`

##### Namespace

SigStat.Common

##### Summary

Represents the ErrorRates achieved in a benchmark

<a name='F-SigStat-Common-ErrorRate-Far'></a>
### Far `constants`

##### Summary

False Acceptance Rate

<a name='F-SigStat-Common-ErrorRate-Frr'></a>
### Frr `constants`

##### Summary

False Rejection Rate

<a name='P-SigStat-Common-ErrorRate-Aer'></a>
### Aer `property`

##### Summary

Average Error Rate (calculated from Far and Frr)

<a name='T-SigStat-Common-Framework-Samplers-EvenNSampler'></a>
## EvenNSampler `type`

##### Namespace

SigStat.Common.Framework.Samplers

##### Summary

Selects the first N signatures with even index for training

<a name='M-SigStat-Common-Framework-Samplers-EvenNSampler-#ctor-System-Int32-'></a>
### #ctor(n) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| n | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | count of signatures used for training |

<a name='P-SigStat-Common-Framework-Samplers-EvenNSampler-N'></a>
### N `property`

##### Summary

Count of signatures used for training

<a name='T-SigStat-Common-Helpers-Excel-ExcelColor'></a>
## ExcelColor `type`

##### Namespace

SigStat.Common.Helpers.Excel

##### Summary

Predefined color schemes for Excel

<a name='F-SigStat-Common-Helpers-Excel-ExcelColor-Danger'></a>
### Danger `constants`

##### Summary

Danger color

<a name='F-SigStat-Common-Helpers-Excel-ExcelColor-Info'></a>
### Info `constants`

##### Summary

Info color

<a name='F-SigStat-Common-Helpers-Excel-ExcelColor-Primary'></a>
### Primary `constants`

##### Summary

Primary color

<a name='F-SigStat-Common-Helpers-Excel-ExcelColor-Secondary'></a>
### Secondary `constants`

##### Summary

Secondary color

<a name='F-SigStat-Common-Helpers-Excel-ExcelColor-Succes'></a>
### Succes `constants`

##### Summary

Succes color

<a name='F-SigStat-Common-Helpers-Excel-ExcelColor-Transparent'></a>
### Transparent `constants`

##### Summary

Transparent color

<a name='F-SigStat-Common-Helpers-Excel-ExcelColor-Warning'></a>
### Warning `constants`

##### Summary

Warning color

<a name='T-SigStat-Common-Helpers-ExcelHelper'></a>
## ExcelHelper `type`

##### Namespace

SigStat.Common.Helpers

##### Summary

Extension methods for common EPPlus tasks

<a name='M-SigStat-Common-Helpers-ExcelHelper-FormatAsTable-OfficeOpenXml-ExcelRange,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-Boolean-'></a>
### FormatAsTable(range,color,showColumnHeader,showRowHeader) `method`

##### Summary

Format cells in the range into a table

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [OfficeOpenXml.ExcelRange](#T-OfficeOpenXml-ExcelRange 'OfficeOpenXml.ExcelRange') | The table's cells |
| color | [SigStat.Common.Helpers.Excel.ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor') | Color palette of the table |
| showColumnHeader | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Defines if the table has column header |
| showRowHeader | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Defines if the table has row header |

<a name='M-SigStat-Common-Helpers-ExcelHelper-FormatAsTableWithTitle-OfficeOpenXml-ExcelRange,System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-Boolean-'></a>
### FormatAsTableWithTitle(range,title,color,showColumnHeader,showRowHeader) `method`

##### Summary

Format cells in the range into a table with possible title

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [OfficeOpenXml.ExcelRange](#T-OfficeOpenXml-ExcelRange 'OfficeOpenXml.ExcelRange') | The table's cells |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The table's title, if null, the table won't have title |
| color | [SigStat.Common.Helpers.Excel.ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor') | Color palette of the table |
| showColumnHeader | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Defines if the table has column header |
| showRowHeader | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Defines if the table has row header |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertColumnChart-OfficeOpenXml-ExcelWorksheet,OfficeOpenXml-ExcelRange,System-Int32,System-Int32,System-String,System-String,System-String,OfficeOpenXml-ExcelRange,System-Int32,System-Int32,System-String-'></a>
### InsertColumnChart(ws,range,col,row,name,xLabel,yLabel,serieLabels,width,height,title) `method`

##### Summary

Draws a column chart for the given data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ws | [OfficeOpenXml.ExcelWorksheet](#T-OfficeOpenXml-ExcelWorksheet 'OfficeOpenXml.ExcelWorksheet') | Worksheet in wich the graph is inserted |
| range | [OfficeOpenXml.ExcelRange](#T-OfficeOpenXml-ExcelRange 'OfficeOpenXml.ExcelRange') | Range containing the data (first row for x axis other rows for series) |
| col | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The graph inserted starts at this column |
| row | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The graph inserted starts at this row |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id and default title of the graph |
| xLabel | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Label for x axis of the graph |
| yLabel | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Label for y axis of the graph |
| serieLabels | [OfficeOpenXml.ExcelRange](#T-OfficeOpenXml-ExcelRange 'OfficeOpenXml.ExcelRange') | If the graph hase more than one series, each can be named separately |
| width | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Graph's width in px |
| height | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Graph's height in px |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Title of the graph if the defauolt name has to be overwritten |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertDictionary``2-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-Collections-Generic-IEnumerable{System-Collections-Generic-KeyValuePair{``0,``1}},System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-String-'></a>
### InsertDictionary\`\`2(ws,col,row,data,title,color,Name) `method`

##### Summary

Insert table from key-value pairs

##### Returns

Range of the inserted data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ws | [OfficeOpenXml.ExcelWorksheet](#T-OfficeOpenXml-ExcelWorksheet 'OfficeOpenXml.ExcelWorksheet') | Worksheet in wich the table is created |
| col | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting column of the table |
| row | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting row of the table |
| data | [System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{\`\`0,\`\`1}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{``0,``1}}') | IEnumerable of key-value pairs in wich the data to insert is stored |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The table's title |
| color | [SigStat.Common.Helpers.Excel.ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor') | The table's color |
| Name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | If given, creates a named range, with this name |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TKey |  |
| TValue |  |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertHierarchicalList-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,SigStat-Common-Helpers-HierarchyElement,System-String,SigStat-Common-Helpers-Excel-ExcelColor-'></a>
### InsertHierarchicalList(ws,col,row,root,title,color) `method`

##### Summary

Insert a hierarchical list in tree style into the worksheet

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ws | [OfficeOpenXml.ExcelWorksheet](#T-OfficeOpenXml-ExcelWorksheet 'OfficeOpenXml.ExcelWorksheet') | Worksheet in wich the list is inserted |
| col | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting column of the list |
| row | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting row of the list |
| root | [SigStat.Common.Helpers.HierarchyElement](#T-SigStat-Common-Helpers-HierarchyElement 'SigStat.Common.Helpers.HierarchyElement') | Root element of the list |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Title of the list |
| color | [SigStat.Common.Helpers.Excel.ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor') | color of the list |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertLegend-OfficeOpenXml-ExcelRange,System-String,System-String,SigStat-Common-Helpers-Excel-ExcelColor-'></a>
### InsertLegend(range,text,title,color) `method`

##### Summary

Insert legend

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [OfficeOpenXml.ExcelRange](#T-OfficeOpenXml-ExcelRange 'OfficeOpenXml.ExcelRange') | Range of the legend |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Text of the legend |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Title of the legend (can be null) |
| color | [SigStat.Common.Helpers.Excel.ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor') | Color of the legend |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertLineChart-OfficeOpenXml-ExcelWorksheet,OfficeOpenXml-ExcelRange,System-Int32,System-Int32,System-String,System-String,System-String,OfficeOpenXml-ExcelRange,System-Int32,System-Int32,System-String-'></a>
### InsertLineChart(ws,range,col,row,name,xLabel,yLabel,SerieLabels,width,height,title) `method`

##### Summary

Draws a line chart for the given data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ws | [OfficeOpenXml.ExcelWorksheet](#T-OfficeOpenXml-ExcelWorksheet 'OfficeOpenXml.ExcelWorksheet') | Worksheet in wich the graph is inserted |
| range | [OfficeOpenXml.ExcelRange](#T-OfficeOpenXml-ExcelRange 'OfficeOpenXml.ExcelRange') | Range containing the data (first row for x axis other rows for series) |
| col | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The graph inserted starts at this column |
| row | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The graph inserted starts at this row |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id and default title of the graph |
| xLabel | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Label for x axis of the graph |
| yLabel | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Label for y axis of the graph |
| SerieLabels | [OfficeOpenXml.ExcelRange](#T-OfficeOpenXml-ExcelRange 'OfficeOpenXml.ExcelRange') | Label of the series |
| width | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Graph's width in px |
| height | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Graph's height in px |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Title of the graph if the defauolt name has to be overwritten |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertLink-OfficeOpenXml-ExcelRange,System-String-'></a>
### InsertLink(range,sheet) `method`

##### Summary

Creates a link to given sheet

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [OfficeOpenXml.ExcelRange](#T-OfficeOpenXml-ExcelRange 'OfficeOpenXml.ExcelRange') | Cells to place the link in |
| sheet | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Destination sheet's name |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertLink-OfficeOpenXml-ExcelRange,System-String,System-String-'></a>
### InsertLink(range,sheet,cells) `method`

##### Summary

Creates a link to selected cells in given sheet

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [OfficeOpenXml.ExcelRange](#T-OfficeOpenXml-ExcelRange 'OfficeOpenXml.ExcelRange') | Cells to place the link in |
| sheet | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Destination sheet's name |
| cells | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Destination cells' address |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertTable-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-Object[0-,0-],System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-Boolean,System-String-'></a>
### InsertTable(ws,col,row,data,title,color,hasRowHeader,hasColumnHeader,name) `method`

##### Summary

Insert table filled with data from a 2D array

##### Returns

Range of the inserted data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ws | [OfficeOpenXml.ExcelWorksheet](#T-OfficeOpenXml-ExcelWorksheet 'OfficeOpenXml.ExcelWorksheet') | Worksheet in wich the table is created |
| col | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting column of the table |
| row | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting row of the table |
| data | [System.Object[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[0: 'System.Object[0:') | 2D array in wich the data to insert is stored |
| title | [0:]](#T-0-] '0:]') | The table's title |
| color | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The table's color |
| hasRowHeader | [SigStat.Common.Helpers.Excel.ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor') | Defines if the table has row header |
| hasColumnHeader | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Defines if the table has column header |
| name | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If given, creates a named range, with this name |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertTable-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-Double[0-,0-],System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-Boolean,System-String-'></a>
### InsertTable(ws,col,row,data,title,color,hasRowHeader,hasColumnHeader,name) `method`

##### Summary

Insert table filled with data from a 2D array

##### Returns

Range of the inserted data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ws | [OfficeOpenXml.ExcelWorksheet](#T-OfficeOpenXml-ExcelWorksheet 'OfficeOpenXml.ExcelWorksheet') | Worksheet in wich the table is created |
| col | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting column of the table |
| row | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting row of the table |
| data | [System.Double[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double[0: 'System.Double[0:') | 2D array in wich the data to insert is stored (double values) |
| title | [0:]](#T-0-] '0:]') | The table's title |
| color | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The table's color |
| hasRowHeader | [SigStat.Common.Helpers.Excel.ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor') | Defines if the table has row header |
| hasColumnHeader | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Defines if the table has column header |
| name | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If given, creates a named range, with this name |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertTable``1-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-Collections-Generic-IEnumerable{``0},System-String,SigStat-Common-Helpers-Excel-ExcelColor,System-Boolean,System-String-'></a>
### InsertTable\`\`1(ws,col,row,data,title,color,showHeader,Name) `method`

##### Summary

Insert a table filled with data from an IEnumerable

##### Returns

Range of the inserted data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ws | [OfficeOpenXml.ExcelWorksheet](#T-OfficeOpenXml-ExcelWorksheet 'OfficeOpenXml.ExcelWorksheet') | Worksheet in wich the table is created |
| col | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting column of the table |
| row | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Starting row of the table |
| data | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | IEnumerable in wich the data to insert is stored |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The table's title |
| color | [SigStat.Common.Helpers.Excel.ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor') | The table's color |
| showHeader | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Defines if the table has header |
| Name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | If given, creates a named range, with this name |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Type of inserted objects |

<a name='M-SigStat-Common-Helpers-ExcelHelper-InsertText-OfficeOpenXml-ExcelWorksheet,System-Int32,System-Int32,System-String,SigStat-Common-Helpers-Excel-TextLevel-'></a>
### InsertText(ws,row,col,text,level) `method`

##### Summary

Inserts text into the defined cell, and format to match text level

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ws | [OfficeOpenXml.ExcelWorksheet](#T-OfficeOpenXml-ExcelWorksheet 'OfficeOpenXml.ExcelWorksheet') | Worksheet in wich the text is inserted |
| row | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Row of the cell |
| col | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Column of the cell |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Text to insert |
| level | [SigStat.Common.Helpers.Excel.TextLevel](#T-SigStat-Common-Helpers-Excel-TextLevel 'SigStat.Common.Helpers.Excel.TextLevel') | Level of text |

<a name='M-SigStat-Common-Helpers-ExcelHelper-Merge-OfficeOpenXml-ExcelRangeBase-'></a>
### Merge(range) `method`

##### Summary

Merge all cells into one in the range.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [OfficeOpenXml.ExcelRangeBase](#T-OfficeOpenXml-ExcelRangeBase 'OfficeOpenXml.ExcelRangeBase') | Cells to merge |

<a name='T-SigStat-Common-Logging-ExcelReportGenerator'></a>
## ExcelReportGenerator `type`

##### Namespace

SigStat.Common.Logging

##### Summary

This class is used to generate a report in Excel file format, form a Benchmark model.

<a name='M-SigStat-Common-Logging-ExcelReportGenerator-GenerateReport-SigStat-Common-Logging-BenchmarkLogModel,System-String-'></a>
### GenerateReport(model,fileName) `method`

##### Summary

Generates an Excel file that contains the report.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| model | [SigStat.Common.Logging.BenchmarkLogModel](#T-SigStat-Common-Logging-BenchmarkLogModel 'SigStat.Common.Logging.BenchmarkLogModel') | The model of the report |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the generated excel file |

<a name='T-SigStat-Common-Transforms-Extrema'></a>
## Extrema `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Extracts minimum and maximum values of given feature.

Default Pipeline Output: (List{double}) Min, (List{double}) Max

##### Remarks

Output features are lists, containing only one value each.

<a name='M-SigStat-Common-Transforms-Extrema-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-FeatureDescriptor'></a>
## FeatureDescriptor `type`

##### Namespace

SigStat.Common

##### Summary

Represents a feature with name and type.

<a name='M-SigStat-Common-FeatureDescriptor-#ctor-System-String,System-String,System-Type-'></a>
### #ctor(name,key,featureType) `constructor`

##### Summary

Initializes a new instance of the [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') class, and adds it to the static [descriptors](#F-SigStat-Common-FeatureDescriptor-descriptors 'SigStat.Common.FeatureDescriptor.descriptors').
Therefore, the `key` parameter must be unique.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| featureType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') |  |

<a name='F-SigStat-Common-FeatureDescriptor-descriptors'></a>
### descriptors `constants`

##### Summary

The static dictionary of all descriptors.

<a name='P-SigStat-Common-FeatureDescriptor-FeatureType'></a>
### FeatureType `property`

##### Summary

Gets or sets the type of the feature.

<a name='P-SigStat-Common-FeatureDescriptor-IsCollection'></a>
### IsCollection `property`

##### Summary

Gets whether the type of the feature is List.

<a name='P-SigStat-Common-FeatureDescriptor-Key'></a>
### Key `property`

##### Summary

Gets the unique key of the feature.

<a name='P-SigStat-Common-FeatureDescriptor-Name'></a>
### Name `property`

##### Summary

Gets or sets a human readable name of the feature.

<a name='M-SigStat-Common-FeatureDescriptor-Get-System-String-'></a>
### Get(key) `method`

##### Summary

Gets the [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') specified by `key`.
Throws [KeyNotFoundException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException') exception if there is no descriptor registered with the given key.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-SigStat-Common-FeatureDescriptor-GetAll'></a>
### GetAll() `method`

##### Summary

Gets a dictionary of all registered feature descriptors

##### Returns



##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-FeatureDescriptor-Get``1-System-String-'></a>
### Get\`\`1(key) `method`

##### Summary

Gets the [FeatureDescriptor\`1](#T-SigStat-Common-FeatureDescriptor`1 'SigStat.Common.FeatureDescriptor`1') specified by `key`.
If the key is not registered yet, a new [FeatureDescriptor\`1](#T-SigStat-Common-FeatureDescriptor`1 'SigStat.Common.FeatureDescriptor`1') is automatically created with the given key and type.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-SigStat-Common-FeatureDescriptor-IsRegistered-System-String-'></a>
### IsRegistered(featureKey) `method`

##### Summary

Returns true, if there is a FeatureDescriptor registered with the given key

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key to search for |

<a name='M-SigStat-Common-FeatureDescriptor-Register-System-String,System-Type-'></a>
### Register(featureKey,type) `method`

##### Summary

Registers a new [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') with a given key.
If the FeatureDescriptor is allready registered, this function will
return a reference to the originally registered FeatureDescriptor.
to the a

##### Returns

A reference to the registered [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') instance

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key for identifying the FeatureDescriptor |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the actual feature values represented by [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') |

<a name='M-SigStat-Common-FeatureDescriptor-ToString'></a>
### ToString() `method`

##### Summary

Returns a string represenatation of the FeatureDescriptor

##### Returns



##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Helpers-Serialization-FeatureDescriptorDictionaryConverter'></a>
## FeatureDescriptorDictionaryConverter `type`

##### Namespace

SigStat.Common.Helpers.Serialization

##### Summary

Custom serializer for a Dictionary of [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor')

<a name='M-SigStat-Common-Helpers-Serialization-FeatureDescriptorDictionaryConverter-CanConvert-System-Type-'></a>
### CanConvert(objectType) `method`

##### Summary

Tells if the current object is of the correct type

##### Returns

If the object can be converted or not

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the object |

<a name='M-SigStat-Common-Helpers-Serialization-FeatureDescriptorDictionaryConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### ReadJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Deserializes the dictionary of [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') created by the this class

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-Serialization-FeatureDescriptorDictionaryConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### WriteJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Serializes the dictionary [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') with type of the descriptor

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Helpers-FeatureDescriptorJsonConverter'></a>
## FeatureDescriptorJsonConverter `type`

##### Namespace

SigStat.Common.Helpers

##### Summary

Custom serializer for [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') objects

<a name='M-SigStat-Common-Helpers-FeatureDescriptorJsonConverter-CanConvert-System-Type-'></a>
### CanConvert(objectType) `method`

##### Summary

Tells if the current object is of the correct type

##### Returns

If the object can be converted or not

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the object |

<a name='M-SigStat-Common-Helpers-FeatureDescriptorJsonConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### ReadJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Deserializes the [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') json created by the this class

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-FeatureDescriptorJsonConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### WriteJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Serializes the [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') to json with type depending on if it was serialized earlier or not

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Helpers-Serialization-FeatureDescriptorListJsonConverter'></a>
## FeatureDescriptorListJsonConverter `type`

##### Namespace

SigStat.Common.Helpers.Serialization

##### Summary

Custom serializer for lists containing [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') or  [FeatureDescriptor\`1](#T-SigStat-Common-FeatureDescriptor`1 'SigStat.Common.FeatureDescriptor`1') objects

<a name='M-SigStat-Common-Helpers-Serialization-FeatureDescriptorListJsonConverter-CanConvert-System-Type-'></a>
### CanConvert(objectType) `method`

##### Summary

Tells if the current object is of the correct type

##### Returns

If the object can be converted or not

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the object |

<a name='M-SigStat-Common-Helpers-Serialization-FeatureDescriptorListJsonConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### ReadJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Deserializes the list of [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') objects

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-Serialization-FeatureDescriptorListJsonConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### WriteJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Serializes the list of [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') objects to json

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Helpers-FeatureDescriptorTJsonConverter'></a>
## FeatureDescriptorTJsonConverter `type`

##### Namespace

SigStat.Common.Helpers

##### Summary

Custom serializer for [FeatureDescriptor\`1](#T-SigStat-Common-FeatureDescriptor`1 'SigStat.Common.FeatureDescriptor`1') objects

<a name='M-SigStat-Common-Helpers-FeatureDescriptorTJsonConverter-CanConvert-System-Type-'></a>
### CanConvert(objectType) `method`

##### Summary

Tells if the current object is of the correct type

##### Returns

If the object can be converted or not

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the object |

<a name='M-SigStat-Common-Helpers-FeatureDescriptorTJsonConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### ReadJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Deserializes the [FeatureDescriptor\`1](#T-SigStat-Common-FeatureDescriptor`1 'SigStat.Common.FeatureDescriptor`1') json created by the this class

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-FeatureDescriptorTJsonConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### WriteJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Serializes the [FeatureDescriptor\`1](#T-SigStat-Common-FeatureDescriptor`1 'SigStat.Common.FeatureDescriptor`1') to json with type depending on if it was serialized earlier or not

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-FeatureDescriptor`1'></a>
## FeatureDescriptor\`1 `type`

##### Namespace

SigStat.Common

##### Summary

Represents a feature with the type `T`

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Type of the feature. |

<a name='M-SigStat-Common-FeatureDescriptor`1-Get-System-String-'></a>
### Get() `method`

##### Summary

Gets the [FeatureDescriptor\`1](#T-SigStat-Common-FeatureDescriptor`1 'SigStat.Common.FeatureDescriptor`1') specified by `key`.
If the key is not registered yet, a new [FeatureDescriptor\`1](#T-SigStat-Common-FeatureDescriptor`1 'SigStat.Common.FeatureDescriptor`1') is automatically created with the given key and type.

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Helpers-Serialization-FeatureStreamingContextState'></a>
## FeatureStreamingContextState `type`

##### Namespace

SigStat.Common.Helpers.Serialization

##### Summary

SerializationContext for serializing SigStat objects

<a name='M-SigStat-Common-Helpers-Serialization-FeatureStreamingContextState-#ctor-System-Boolean-'></a>
### #ctor() `constructor`

##### Summary

Constructor

##### Parameters

This constructor has no parameters.

<a name='P-SigStat-Common-Helpers-Serialization-FeatureStreamingContextState-KnownFeatureKeys'></a>
### KnownFeatureKeys `property`

##### Summary

A list of already serialized FeatureDescriptor keys

<a name='T-SigStat-Common-Features'></a>
## Features `type`

##### Namespace

SigStat.Common

##### Summary

Standard set of features.

<a name='F-SigStat-Common-Features-All'></a>
### All `constants`

##### Summary

Returns a readonly list of all [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor')s defined in [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

<a name='F-SigStat-Common-Features-Altitude'></a>
### Altitude `constants`

##### Summary

Altitude of an online signature as a function of [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T')

<a name='F-SigStat-Common-Features-Azimuth'></a>
### Azimuth `constants`

##### Summary

Azimuth of an online signature as a function of [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T')

<a name='F-SigStat-Common-Features-Cog'></a>
### Cog `constants`

##### Summary

Center of gravity in a signature

<a name='F-SigStat-Common-Features-Dpi'></a>
### Dpi `constants`

##### Summary

Dots per inch

<a name='F-SigStat-Common-Features-Image'></a>
### Image `constants`

##### Summary

The visaul representation of a signature

<a name='F-SigStat-Common-Features-PenDown'></a>
### PenDown `constants`

##### Summary

Pen position of an online signature as a function of [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T').
It is true when the pen touches the paper.

<a name='F-SigStat-Common-Features-Pressure'></a>
### Pressure `constants`

##### Summary

Pressure of an online signature as a function of [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T')

<a name='F-SigStat-Common-Features-Size'></a>
### Size `constants`

##### Summary

Actual bounds of the signature

<a name='F-SigStat-Common-Features-T'></a>
### T `constants`

##### Summary

Timestamps for online signatures

<a name='F-SigStat-Common-Features-TrimmedBounds'></a>
### TrimmedBounds `constants`

##### Summary

Represents the main body of the signature [BasicMetadataExtraction](#T-SigStat-Common-BasicMetadataExtraction 'SigStat.Common.BasicMetadataExtraction')

<a name='F-SigStat-Common-Features-X'></a>
### X `constants`

##### Summary

X coordinates of an online signature as a function of [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T')

<a name='F-SigStat-Common-Features-Y'></a>
### Y `constants`

##### Summary

Y coordinates of an online signature as a function of [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T')

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations'></a>
## FillPenUpDurations `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

This transformation fills gaps of online signature by interpolating the last known
feature values. Gaps should be represented in the signature with two zero pressure border points.

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-InputFeatures'></a>
### InputFeatures `property`

##### Summary

Gets or sets the features of an online signature that need to be altered

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-InterpolationType'></a>
### InterpolationType `property`

##### Summary

An implementation of [IInterpolation](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation 'SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-OutputFeatures'></a>
### OutputFeatures `property`

##### Summary

Gets or sets the features of an online signature that were altered

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-PressureInputFeature'></a>
### PressureInputFeature `property`

##### Summary

Gets or sets the feature representing pressure in an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-PressureOutputFeature'></a>
### PressureOutputFeature `property`

##### Summary

Gets or sets the feature representing the modified pressure values of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeInputFeature'></a>
### TimeInputFeature `property`

##### Summary

Gets or sets the feature representing the timestamps of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeOutputFeature'></a>
### TimeOutputFeature `property`

##### Summary

Gets or sets the feature representing the modified timestamps of an online signature

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints'></a>
## FilterPoints `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Removes samples based on a criteria from online signature time series

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-InputFeatures'></a>
### InputFeatures `property`

##### Summary

[FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') list of all features to resample

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-KeyFeatureInput'></a>
### KeyFeatureInput `property`

##### Summary

[FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') that controls the removal of samples (e.g. [Pressure](#F-SigStat-Common-Features-Pressure 'SigStat.Common.Features.Pressure'))

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-KeyFeatureOutput'></a>
### KeyFeatureOutput `property`

##### Summary

Resampled output for [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') that controls the removal of samples (e.g. [Pressure](#F-SigStat-Common-Features-Pressure 'SigStat.Common.Features.Pressure'))

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-OutputFeatures'></a>
### OutputFeatures `property`

##### Summary

Resampled output for all input features

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-Percentile'></a>
### Percentile `property`

##### Summary

The lowes percentile of the [KeyFeatureInput](#P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-KeyFeatureInput 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FilterPoints.KeyFeatureInput') will be removed during filtering

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-FilterPoints-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Framework-Samplers-FirstNSampler'></a>
## FirstNSampler `type`

##### Namespace

SigStat.Common.Framework.Samplers

##### Summary

Selects the first N signatures for training

<a name='M-SigStat-Common-Framework-Samplers-FirstNSampler-#ctor-System-Int32-'></a>
### #ctor(n) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| n | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | count of signatures used for training |

<a name='P-SigStat-Common-Framework-Samplers-FirstNSampler-N'></a>
### N `property`

##### Summary

Count of signatures used for training

<a name='T-SigStat-Common-Transforms-Binarization-ForegroundType'></a>
## ForegroundType `type`

##### Namespace

SigStat.Common.Transforms.Binarization

##### Summary

Represents the type of the input image.

<a name='F-SigStat-Common-Transforms-Binarization-ForegroundType-Bright'></a>
### Bright `constants`

##### Summary

Foreground is brighter than background. (for non-signature images)

<a name='F-SigStat-Common-Transforms-Binarization-ForegroundType-Dark'></a>
### Dark `constants`

##### Summary

(default) Foreground is darker than background. (eg. ink on paper)

<a name='T-SigStat-Common-Transforms-HSCPThinning'></a>
## HSCPThinning `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Iteratively thins the input binary raster with the [HSCPThinningStep](#T-SigStat-Common-Algorithms-HSCPThinningStep 'SigStat.Common.Algorithms.HSCPThinningStep') algorithm.

Pipeline Input type: bool[,]

Default Pipeline Output: (bool[,]) HSCPThinningResult

<a name='P-SigStat-Common-Transforms-HSCPThinning-Input'></a>
### Input `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') for the binary image of the signature

<a name='P-SigStat-Common-Transforms-HSCPThinning-Output'></a>
### Output `property`

##### Summary

Output [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') for the binary image of the signature

<a name='M-SigStat-Common-Transforms-HSCPThinning-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Algorithms-HSCPThinningStep'></a>
## HSCPThinningStep `type`

##### Namespace

SigStat.Common.Algorithms

##### Summary

HSCP thinning algorithm
http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf

<a name='P-SigStat-Common-Algorithms-HSCPThinningStep-ResultChanged'></a>
### ResultChanged `property`

##### Summary

Gets whether the last [Scan](#M-SigStat-Common-Algorithms-HSCPThinningStep-Scan-System-Boolean[0-,0-]- 'SigStat.Common.Algorithms.HSCPThinningStep.Scan(System.Boolean[0:,0:])') call was effective.

<a name='M-SigStat-Common-Algorithms-HSCPThinningStep-Neighbourhood-System-Boolean[0-,0-],System-Int32,System-Int32-'></a>
### Neighbourhood() `method`

##### Summary

Gets neighbour pixels in order.

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Algorithms-HSCPThinningStep-Scan-System-Boolean[0-,0-]-'></a>
### Scan(b) `method`

##### Summary

Does one step of the thinning. Call it iteratively while ResultChanged.

##### Returns

Thinned binary raster.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| b | [System.Boolean[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean[0: 'System.Boolean[0:') | Binary raster. |

<a name='T-SigStat-Common-Helpers-HierarchyElement'></a>
## HierarchyElement `type`

##### Namespace

SigStat.Common.Helpers

##### Summary

Hierarchical structure to store object

<a name='M-SigStat-Common-Helpers-HierarchyElement-#ctor'></a>
### #ctor() `constructor`

##### Summary

Create an emty element

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-Helpers-HierarchyElement-#ctor-System-Object-'></a>
### #ctor(Content) `constructor`

##### Summary

Create a new element with content

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Content | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Content of the new element |

<a name='P-SigStat-Common-Helpers-HierarchyElement-Children'></a>
### Children `property`

##### Summary

Gets the children.

<a name='P-SigStat-Common-Helpers-HierarchyElement-Content'></a>
### Content `property`

##### Summary

Gets or sets the content.

<a name='M-SigStat-Common-Helpers-HierarchyElement-Add-SigStat-Common-Helpers-HierarchyElement-'></a>
### Add() `method`

##### Summary

Adds the specified element as a child

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-HierarchyElement-GetCount'></a>
### GetCount() `method`

##### Summary

Returns number of elements under this node and itself

##### Returns



##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-HierarchyElement-GetDepth'></a>
### GetDepth() `method`

##### Summary

Return the hierarchy's depth from this node

##### Returns



##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-HierarchyElement-GetEnumerator'></a>
### GetEnumerator() `method`

##### Summary

Returns an enumerator that iterates through the collection.

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-HierarchyElement-ToString'></a>
### ToString() `method`

##### Summary

Converts to string.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that represents this instance.

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Pipeline-IClassifier'></a>
## IClassifier `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Trains classification models based on reference signatures

<a name='M-SigStat-Common-Pipeline-IClassifier-Test-SigStat-Common-Pipeline-ISignerModel,SigStat-Common-Signature-'></a>
### Test(signature,model) `method`

##### Summary

Returns a double value in the range [0..1], representing the probability of the given signature belonging to the trained model.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Pipeline.ISignerModel](#T-SigStat-Common-Pipeline-ISignerModel 'SigStat.Common.Pipeline.ISignerModel') | The signature to test |
| model | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | The model aquired from the [Train](#M-SigStat-Common-Pipeline-IClassifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}- 'SigStat.Common.Pipeline.IClassifier.Train(System.Collections.Generic.List{SigStat.Common.Signature})') method |

<a name='M-SigStat-Common-Pipeline-IClassifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}-'></a>
### Train(signatures) `method`

##### Summary

Trains a model based on the signatures and returns the trained model

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signatures | [System.Collections.Generic.List{SigStat.Common.Signature}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{SigStat.Common.Signature}') |  |

<a name='T-SigStat-Common-Loaders-IDataSetLoader'></a>
## IDataSetLoader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

Exposes a function to enable loading collections of [Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer')s.
Base abstract class: [DataSetLoader](#T-SigStat-Common-Loaders-DataSetLoader 'SigStat.Common.Loaders.DataSetLoader').

<a name='M-SigStat-Common-Loaders-IDataSetLoader-EnumerateSigners'></a>
### EnumerateSigners() `method`

##### Summary

Enumerates all signers of the database

##### Returns



##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-IDataSetLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners(signerFilter) `method`

##### Summary

Enumerates all [Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer')s that match the `signerFilter`.

##### Returns

Collection of [Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer')s that match the `signerFilter`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signerFilter | [System.Predicate{SigStat.Common.Signer}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Predicate 'System.Predicate{SigStat.Common.Signer}') | Filter to specify which Signers to load. Example: (p=>p=="01") |

<a name='T-SigStat-Common-Pipeline-IDistanceClassifier'></a>
## IDistanceClassifier `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Trains classification models based on reference signatures, by calculating the distances between signature pairs

<a name='P-SigStat-Common-Pipeline-IDistanceClassifier-DistanceFunction'></a>
### DistanceFunction `property`

##### Summary

A function to calculate the distance between two online signature points

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation'></a>
## IInterpolation `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Represents an interploation algorithm

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation-FeatureValues'></a>
### FeatureValues `property`

##### Summary

Gets or sets the feature values.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation-TimeValues'></a>
### TimeValues `property`

##### Summary

Timestamps

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation-GetValue-System-Double-'></a>
### GetValue(timestamp) `method`

##### Summary

Gets the interpolated value at a given timestamp

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| timestamp | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The timestamp. |

<a name='T-SigStat-Common-ILoggerObject'></a>
## ILoggerObject `type`

##### Namespace

SigStat.Common

##### Summary

Represents a type, that contains an ILogger property that can be used to perform logging.

<a name='P-SigStat-Common-ILoggerObject-Logger'></a>
### Logger `property`

##### Summary

Gets or sets the ILogger implementation used to perform logging

<a name='T-SigStat-Common-ILoggerObjectExtensions'></a>
## ILoggerObjectExtensions `type`

##### Namespace

SigStat.Common

##### Summary

ILoggerObject extension methods for common scenarios.

##### Remarks

Note to framework developers: you may extend this class with additional overloads if they are required

<a name='M-SigStat-Common-ILoggerObjectExtensions-LogCritical-SigStat-Common-ILoggerObject,System-String,System-Object[]-'></a>
### LogCritical(obj,message,args) `method`

##### Summary

Formats and writes an critical error log message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [SigStat.Common.ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject') | The SigStat.Common.ILoggerObject containing the Logger to write to. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format string of the log message in message template format. Example: "User {User} logged in from {Address}" |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | An object array that contains zero or more objects to format. |

<a name='M-SigStat-Common-ILoggerObjectExtensions-LogDebug-SigStat-Common-ILoggerObject,System-String,System-Object[]-'></a>
### LogDebug(obj,message,args) `method`

##### Summary

Formats and writes an debug log message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [SigStat.Common.ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject') | The SigStat.Common.ILoggerObject containing the Logger to write to. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format string of the log message in message template format. Example: "User {User} logged in from {Address}" |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | An object array that contains zero or more objects to format. |

<a name='M-SigStat-Common-ILoggerObjectExtensions-LogError-SigStat-Common-ILoggerObject,System-String,System-Object[]-'></a>
### LogError(obj,message,args) `method`

##### Summary

Formats and writes an error log message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [SigStat.Common.ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject') | The SigStat.Common.ILoggerObject containing the Logger to write to. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format string of the log message in message template format. Example: "User {User} logged in from {Address}" |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | An object array that contains zero or more objects to format. |

<a name='M-SigStat-Common-ILoggerObjectExtensions-LogError-SigStat-Common-ILoggerObject,System-Exception,System-String,System-Object[]-'></a>
### LogError(obj,exception,message,args) `method`

##### Summary

Formats and writes an error log message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [SigStat.Common.ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject') | The SigStat.Common.ILoggerObject containing the Logger to write to. |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The exception to log. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format string of the log message in message template format. Example: "User {User} logged in from {Address}" |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | An object array that contains zero or more objects to format. |

<a name='M-SigStat-Common-ILoggerObjectExtensions-LogInformation-SigStat-Common-ILoggerObject,System-String,System-Object[]-'></a>
### LogInformation(obj,message,args) `method`

##### Summary

Formats and writes an informational log message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [SigStat.Common.ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject') | The SigStat.Common.ILoggerObject containing the Logger to write to. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format string of the log message in message template format. Example: "User {User} logged in from {Address}" |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | An object array that contains zero or more objects to format. |

<a name='M-SigStat-Common-ILoggerObjectExtensions-LogTrace-SigStat-Common-ILoggerObject,System-String,System-Object[]-'></a>
### LogTrace(obj,message,args) `method`

##### Summary

Formats and writes a trace log message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [SigStat.Common.ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject') | The SigStat.Common.ILoggerObject containing the Logger to write to. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format string of the log message in message template format. Example: "User {User} logged in from {Address}" |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | An object array that contains zero or more objects to format. |

<a name='M-SigStat-Common-ILoggerObjectExtensions-LogTrace``1-SigStat-Common-ILoggerObject,``0,Microsoft-Extensions-Logging-EventId,System-Exception,System-Func{``0,System-Exception,System-String}-'></a>
### LogTrace\`\`1(obj,state,eventId,exception,formatter) `method`

##### Summary

Formats and writes a trace log message with state.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [SigStat.Common.ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject') | The SigStat.Common.ILoggerObject containing the Logger to write to. |
| state | [\`\`0](#T-``0 '``0') | The entry to be written. |
| eventId | [Microsoft.Extensions.Logging.EventId](#T-Microsoft-Extensions-Logging-EventId 'Microsoft.Extensions.Logging.EventId') | Id of the event. |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The exception related to this entry. |
| formatter | [System.Func{\`\`0,System.Exception,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,System.Exception,System.String}') | Function to create a String message of the state and exception. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TState | The type of the object to be written (preferably a descendant of SigstatLogState). |

<a name='M-SigStat-Common-ILoggerObjectExtensions-LogWarning-SigStat-Common-ILoggerObject,System-String,System-Object[]-'></a>
### LogWarning(obj,message,args) `method`

##### Summary

Formats and writes an warning log message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [SigStat.Common.ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject') | The SigStat.Common.ILoggerObject containing the Logger to write to. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format string of the log message in message template format. Example: "User {User} logged in from {Address}" |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | An object array that contains zero or more objects to format. |

<a name='M-SigStat-Common-ILoggerObjectExtensions-LogWarning-SigStat-Common-ILoggerObject,System-Exception,System-String,System-Object[]-'></a>
### LogWarning(obj,exception,message,args) `method`

##### Summary

Formats and writes an warning log message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [SigStat.Common.ILoggerObject](#T-SigStat-Common-ILoggerObject 'SigStat.Common.ILoggerObject') | The SigStat.Common.ILoggerObject containing the Logger to write to. |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The exception to log. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format string of the log message in message template format. Example: "User {User} logged in from {Address}" |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | An object array that contains zero or more objects to format. |

<a name='T-SigStat-Common-IOExtensions'></a>
## IOExtensions `type`

##### Namespace

SigStat.Common

##### Summary

Extension methods for common IO operations

<a name='M-SigStat-Common-IOExtensions-GetPath-System-String-'></a>
### GetPath(path) `method`

##### Summary

Gets the given relative or absolute path in a platform neutral form

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-SigStat-Common-Pipeline-IPipelineIO'></a>
## IPipelineIO `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Supports the definition of [PipelineInput](#T-SigStat-Common-Pipeline-PipelineInput 'SigStat.Common.Pipeline.PipelineInput') and [PipelineOutput](#T-SigStat-Common-Pipeline-PipelineOutput 'SigStat.Common.Pipeline.PipelineOutput')

<a name='P-SigStat-Common-Pipeline-IPipelineIO-PipelineInputs'></a>
### PipelineInputs `property`

##### Summary

A collection of inputs for the pipeline elements

<a name='P-SigStat-Common-Pipeline-IPipelineIO-PipelineOutputs'></a>
### PipelineOutputs `property`

##### Summary

A collection of outputs for the pipeline elements

<a name='T-SigStat-Common-Helpers-IProgress'></a>
## IProgress `type`

##### Namespace

SigStat.Common.Helpers

##### Summary

Enables progress tracking by expsoing the [Progress](#P-SigStat-Common-Helpers-IProgress-Progress 'SigStat.Common.Helpers.IProgress.Progress') property and the [](#E-SigStat-Common-Helpers-IProgress-ProgressChanged 'SigStat.Common.Helpers.IProgress.ProgressChanged') event.

<a name='P-SigStat-Common-Helpers-IProgress-Progress'></a>
### Progress `property`

##### Summary

Gets the current progress in percentage.

<a name='T-SigStat-Common-Pipeline-ISignerModel'></a>
## ISignerModel `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Analyzes signatures based on their similiarity to the trained model

<a name='T-SigStat-Common-ITransformation'></a>
## ITransformation `type`

##### Namespace

SigStat.Common

##### Summary

Allows implementing a pipeline transform item capable of logging, progress tracking and IO rewiring.

<a name='M-SigStat-Common-ITransformation-Transform-SigStat-Common-Signature-'></a>
### Transform(signature) `method`

##### Summary

Executes the transform on the `signature` parameter.
This function gets called by the pipeline.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | The [Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') with a set of features to be transformed. |

<a name='T-SigStat-Common-Transforms-ImageGenerator'></a>
## ImageGenerator `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Generates an image feature out of a binary raster.
Optionally, saves the image to a png file.
Useful for debugging pipeline steps.

Pipeline Input type: bool[,]

Default Pipeline Output: (bool[,]) Input, (Image{Rgba32}) InputImage

<a name='M-SigStat-Common-Transforms-ImageGenerator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [ImageGenerator](#T-SigStat-Common-Transforms-ImageGenerator 'SigStat.Common.Transforms.ImageGenerator') class with default settings: skip file writing, Blue ink on white paper.

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-Transforms-ImageGenerator-#ctor-System-Boolean-'></a>
### #ctor(writeToFile) `constructor`

##### Summary

Initializes a new instance of the [ImageGenerator](#T-SigStat-Common-Transforms-ImageGenerator 'SigStat.Common.Transforms.ImageGenerator') class with default settings.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| writeToFile | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to save the generated image into a png file. |

<a name='M-SigStat-Common-Transforms-ImageGenerator-#ctor-System-Boolean,SixLabors-ImageSharp-PixelFormats-Rgba32,SixLabors-ImageSharp-PixelFormats-Rgba32-'></a>
### #ctor(writeToFile,foregroundColor,backgroundColor) `constructor`

##### Summary

Initializes a new instance of the [ImageGenerator](#T-SigStat-Common-Transforms-ImageGenerator 'SigStat.Common.Transforms.ImageGenerator') class with specified settings.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| writeToFile | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to save the generated image into a png file. |
| foregroundColor | [SixLabors.ImageSharp.PixelFormats.Rgba32](#T-SixLabors-ImageSharp-PixelFormats-Rgba32 'SixLabors.ImageSharp.PixelFormats.Rgba32') | Ink color. |
| backgroundColor | [SixLabors.ImageSharp.PixelFormats.Rgba32](#T-SixLabors-ImageSharp-PixelFormats-Rgba32 'SixLabors.ImageSharp.PixelFormats.Rgba32') | Paper color. |

<a name='P-SigStat-Common-Transforms-ImageGenerator-BackgroundColor'></a>
### BackgroundColor `property`

##### Summary

Gets or sets the color of the backgroung used to render the signature

<a name='P-SigStat-Common-Transforms-ImageGenerator-ForegroundColor'></a>
### ForegroundColor `property`

##### Summary

Gets or sets the color of the foreground used to render the signature

<a name='P-SigStat-Common-Transforms-ImageGenerator-Input'></a>
### Input `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') for the binary image of a signature

<a name='P-SigStat-Common-Transforms-ImageGenerator-OutputImage'></a>
### OutputImage `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') for the binary image of a signature

<a name='P-SigStat-Common-Transforms-ImageGenerator-WriteToFile'></a>
### WriteToFile `property`

##### Summary

Gets or sets a value indicating whether the results should be saved to a file or not.

<a name='M-SigStat-Common-Transforms-ImageGenerator-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Loaders-ImageLoader'></a>
## ImageLoader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

DataSetLoader for Image type databases.
Similar format to Svc2004Loader, but finds png images.

<a name='M-SigStat-Common-Loaders-ImageLoader-#ctor-System-String-'></a>
### #ctor(databasePath) `constructor`

##### Summary

Initializes a new instance of the [ImageLoader](#T-SigStat-Common-Loaders-ImageLoader 'SigStat.Common.Loaders.ImageLoader') class with specified database.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | File path to the database. |

<a name='M-SigStat-Common-Loaders-ImageLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-ImageLoader-LoadImage-SigStat-Common-Signature,System-String-'></a>
### LoadImage(signature,file) `method`

##### Summary

Load one image.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | The signature that receives the new [Image](#F-SigStat-Common-Features-Image 'SigStat.Common.Features.Image') |
| file | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | File path to the image to be loaded. |

<a name='M-SigStat-Common-Loaders-ImageLoader-LoadSignature-System-String-'></a>
### LoadSignature(file) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| file | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-SigStat-Common-Loaders-ImageSaver'></a>
## ImageSaver `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

Get the [Image](#F-SigStat-Common-Features-Image 'SigStat.Common.Features.Image') of a [Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') and save it as png file.

<a name='M-SigStat-Common-Loaders-ImageSaver-Save-SigStat-Common-Signature,System-String-'></a>
### Save(signature,path) `method`

##### Summary

Saves a png image file to the specified `path`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Input signature containing [Image](#F-SigStat-Common-Features-Image 'SigStat.Common.Features.Image'). |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Output file path of the png image. |

<a name='T-SigStat-Common-Pipeline-Input'></a>
## Input `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Annotates an input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') in a transformation pipeline

##### See Also

- [System.Attribute](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Attribute 'System.Attribute')

<a name='M-SigStat-Common-Pipeline-Input-#ctor-SigStat-Common-Pipeline-AutoSetMode-'></a>
### #ctor(AutoSetMode) `constructor`

##### Summary

Initializes a new instance of the [Input](#T-SigStat-Common-Pipeline-Input 'SigStat.Common.Pipeline.Input') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| AutoSetMode | [SigStat.Common.Pipeline.AutoSetMode](#T-SigStat-Common-Pipeline-AutoSetMode 'SigStat.Common.Pipeline.AutoSetMode') | The automatic set mode. |

<a name='F-SigStat-Common-Pipeline-Input-AutoSetMode'></a>
### AutoSetMode `constants`

##### Summary

The automatic set mode

<a name='T-SigStat-Common-Logging-KeyValueGroup'></a>
## KeyValueGroup `type`

##### Namespace

SigStat.Common.Logging

##### Summary

A group of key-value pairs

<a name='M-SigStat-Common-Logging-KeyValueGroup-#ctor-System-String-'></a>
### #ctor(name) `constructor`

##### Summary

Creates an emty key-value group

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name if the new group |

<a name='P-SigStat-Common-Logging-KeyValueGroup-Items'></a>
### Items `property`

##### Summary

Key-Value pairs in the group

<a name='P-SigStat-Common-Logging-KeyValueGroup-Name'></a>
### Name `property`

##### Summary

Name of the group

<a name='T-SigStat-Common-Helpers-Serialization-KeyValueGroupConverter'></a>
## KeyValueGroupConverter `type`

##### Namespace

SigStat.Common.Helpers.Serialization

##### Summary

Serializes / Deserializes a logging dictionary [KeyValueGroup](#T-SigStat-Common-Logging-KeyValueGroup 'SigStat.Common.Logging.KeyValueGroup')

<a name='M-SigStat-Common-Helpers-Serialization-KeyValueGroupConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,SigStat-Common-Logging-KeyValueGroup,System-Boolean,Newtonsoft-Json-JsonSerializer-'></a>
### ReadJson() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-Serialization-KeyValueGroupConverter-WriteJson-Newtonsoft-Json-JsonWriter,SigStat-Common-Logging-KeyValueGroup,Newtonsoft-Json-JsonSerializer-'></a>
### WriteJson() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Framework-Samplers-LastNSampler'></a>
## LastNSampler `type`

##### Namespace

SigStat.Common.Framework.Samplers

##### Summary

Selects the first N signatures for training

<a name='M-SigStat-Common-Framework-Samplers-LastNSampler-#ctor-System-Int32-'></a>
### #ctor(n) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| n | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Count of signatures used for training |

<a name='P-SigStat-Common-Framework-Samplers-LastNSampler-N'></a>
### N `property`

##### Summary

Count of signatures used for training

<a name='T-SigStat-Common-Helpers-Excel-Level'></a>
## Level `type`

##### Namespace

SigStat.Common.Helpers.Excel

##### Summary

Helper class for applying text styling

<a name='M-SigStat-Common-Helpers-Excel-Level-StyleAs-OfficeOpenXml-Style-ExcelStyle,SigStat-Common-Helpers-Excel-TextLevel-'></a>
### StyleAs(style,level) `method`

##### Summary

Set the style according to text level

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| style | [OfficeOpenXml.Style.ExcelStyle](#T-OfficeOpenXml-Style-ExcelStyle 'OfficeOpenXml.Style.ExcelStyle') |  |
| level | [SigStat.Common.Helpers.Excel.TextLevel](#T-SigStat-Common-Helpers-Excel-TextLevel 'SigStat.Common.Helpers.Excel.TextLevel') |  |

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-LinearInterpolation'></a>
## LinearInterpolation `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Performs linear interpolation on the input

##### See Also

- [SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation 'SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-LinearInterpolation-FeatureValues'></a>
### FeatureValues `property`

##### Summary

*Inherit from parent.*

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-LinearInterpolation-TimeValues'></a>
### TimeValues `property`

##### Summary

*Inherit from parent.*

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-LinearInterpolation-GetValue-System-Double-'></a>
### GetValue(timestamp) `method`

##### Summary

Gets the interpolated value at a given timestamp

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| timestamp | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The timestamp. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | TimeValues is not initialized |
| [System.NullReferenceException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NullReferenceException 'System.NullReferenceException') | FeatureValues is not initialized |
| [System.ArgumentOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') | The given timestamp is not in the range of TimeValues |

<a name='T-SigStat-Common-Logging-LogAnalyzer'></a>
## LogAnalyzer `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Analizes logs and creates a model from the gained information

<a name='M-SigStat-Common-Logging-LogAnalyzer-GetBenchmarkLogModel-System-Collections-Generic-IEnumerable{SigStat-Common-Logging-SigStatLogState}-'></a>
### GetBenchmarkLogModel(logs) `method`

##### Summary

Creates a BenchmarkLogModel from previous logs

##### Returns

The Benchmark model filled with information according to the logs

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| logs | [System.Collections.Generic.IEnumerable{SigStat.Common.Logging.SigStatLogState}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{SigStat.Common.Logging.SigStatLogState}') | The collection of logs, that contains the required information for a BenchmarkLogModel |

<a name='T-SigStat-Common-Logging-ReportInformationLogger-LogStateLoggedEventHandler'></a>
## LogStateLoggedEventHandler `type`

##### Namespace

SigStat.Common.Logging.ReportInformationLogger

##### Summary

The event is raised whenever a SigStatLogState is logged.

<a name='T-SigStat-Common-Loop'></a>
## Loop `type`

##### Namespace

SigStat.Common

##### Summary

Represents a loop in a signature

<a name='M-SigStat-Common-Loop-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates a [Loop](#T-SigStat-Common-Loop 'SigStat.Common.Loop') instance

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-Loop-#ctor-System-Single,System-Single-'></a>
### #ctor(centerX,centerY) `constructor`

##### Summary

Creates a [Loop](#T-SigStat-Common-Loop 'SigStat.Common.Loop') instance and initializes the [Center](#P-SigStat-Common-Loop-Center 'SigStat.Common.Loop.Center') property

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| centerX | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') |  |
| centerY | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') |  |

<a name='P-SigStat-Common-Loop-Bounds'></a>
### Bounds `property`

##### Summary

The bounding rectangle of the loop

<a name='P-SigStat-Common-Loop-Center'></a>
### Center `property`

##### Summary

The geometrical center of the looop

<a name='P-SigStat-Common-Loop-Points'></a>
### Points `property`

##### Summary

A list of defining points of the loop

<a name='M-SigStat-Common-Loop-ToString'></a>
### ToString() `method`

##### Summary

Returns a string representation of the loop

##### Returns



##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Loaders-MCYTLoader-MCYT'></a>
## MCYT `type`

##### Namespace

SigStat.Common.Loaders.MCYTLoader

##### Summary

Set of features containing raw data loaded from MCYT-format database.

<a name='F-SigStat-Common-Loaders-MCYTLoader-MCYT-Altitude'></a>
### Altitude `constants`

##### Summary

Altitude values from the online signature imported from the MCYT database

<a name='F-SigStat-Common-Loaders-MCYTLoader-MCYT-Azimuth'></a>
### Azimuth `constants`

##### Summary

Azimuth values from the online signature imported from the MCYT database

<a name='F-SigStat-Common-Loaders-MCYTLoader-MCYT-Pressure'></a>
### Pressure `constants`

##### Summary

Pressure values from the online signature imported from the MCYT database

<a name='F-SigStat-Common-Loaders-MCYTLoader-MCYT-X'></a>
### X `constants`

##### Summary

X cooridnates from the online signature imported from the MCYT database

<a name='F-SigStat-Common-Loaders-MCYTLoader-MCYT-Y'></a>
### Y `constants`

##### Summary

Y cooridnates from the online signature imported from the MCYT database

<a name='T-SigStat-Common-Loaders-MCYTLoader'></a>
## MCYTLoader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

[DataSetLoader](#T-SigStat-Common-Loaders-DataSetLoader 'SigStat.Common.Loaders.DataSetLoader') for the MCYT dataset

<a name='M-SigStat-Common-Loaders-MCYTLoader-#ctor-System-String,System-Boolean-'></a>
### #ctor(databasePath,standardFeatures) `constructor`

##### Summary

Initializes a new instance of the [MCYTLoader](#T-SigStat-Common-Loaders-MCYTLoader 'SigStat.Common.Loaders.MCYTLoader') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The database path. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` features will be also stored in [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

<a name='P-SigStat-Common-Loaders-MCYTLoader-DatabasePath'></a>
### DatabasePath `property`

##### Summary

Gets or sets the database path.

<a name='P-SigStat-Common-Loaders-MCYTLoader-SamplingFrequency'></a>
### SamplingFrequency `property`

##### Summary

Set MCYT sampling frequenct to 100hz

<a name='P-SigStat-Common-Loaders-MCYTLoader-StandardFeatures'></a>
### StandardFeatures `property`

##### Summary

Gets or sets a value indicating whether features are also loaded as [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

<a name='M-SigStat-Common-Loaders-MCYTLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-MCYTLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean-'></a>
### LoadSignature(signature,stream,standardFeatures) `method`

##### Summary

Loads one signature from specified stream.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to write features to. |
| stream | [System.IO.MemoryStream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.MemoryStream 'System.IO.MemoryStream') | Stream to read MCYT data from. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

##### Remarks

Based on Mohammad's MCYT reader.

<a name='T-SigStat-Common-Transforms-Map'></a>
## Map `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Maps values of a feature to a specified range.

Pipeline Input type: List{double}

Default Pipeline Output: (List{double}) MapResult

<a name='M-SigStat-Common-Transforms-Map-#ctor-System-Double,System-Double-'></a>
### #ctor(minVal,maxVal) `constructor`

##### Summary

Initializes a new instance of the [Map](#T-SigStat-Common-Transforms-Map 'SigStat.Common.Transforms.Map') class with specified settings.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| minVal | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | New minimum value. |
| maxVal | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | New maximum value. |

<a name='P-SigStat-Common-Transforms-Map-Input'></a>
### Input `property`

##### Summary

Input

<a name='P-SigStat-Common-Transforms-Map-Output'></a>
### Output `property`

##### Summary

Output

<a name='M-SigStat-Common-Transforms-Map-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-MathHelper'></a>
## MathHelper `type`

##### Namespace

SigStat.Common

##### Summary

Common mathematical functions used by the SigStat framework

<a name='M-SigStat-Common-MathHelper-Median-System-Collections-Generic-IEnumerable{System-Double}-'></a>
### Median(values) `method`

##### Summary

Calculates the median of the given data series

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| values | [System.Collections.Generic.IEnumerable{System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Double}') | The data series |

<a name='M-SigStat-Common-MathHelper-Min-System-Double,System-Double,System-Double-'></a>
### Min(d1,d2,d3) `method`

##### Summary

Returns the smallest of the three double parameters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d1 | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |
| d2 | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |
| d3 | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-SigStat-Common-MathHelper-StdDiviation-System-Collections-Generic-IEnumerable{System-Double}-'></a>
### StdDiviation(feature) `method`

##### Summary

return standard diviation of a feature values

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| feature | [System.Collections.Generic.IEnumerable{System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Double}') |  |

<a name='T-SigStat-Common-Transforms-Multiply'></a>
## Multiply `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Multiplies the values of a feature with a given constant.

Pipeline Input type: List{double}

Default Pipeline Output: (List{double}) Input

<a name='M-SigStat-Common-Transforms-Multiply-#ctor-System-Double-'></a>
### #ctor(byConst) `constructor`

##### Summary

Initializes a new instance of the [Multiply](#T-SigStat-Common-Transforms-Multiply 'SigStat.Common.Transforms.Multiply') class with specified settings.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| byConst | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The value to multiply the input feature by. |

<a name='P-SigStat-Common-Transforms-Multiply-InputList'></a>
### InputList `property`

##### Summary

Input

<a name='P-SigStat-Common-Transforms-Multiply-Output'></a>
### Output `property`

##### Summary

Output

<a name='M-SigStat-Common-Transforms-Multiply-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-Normalize'></a>
## Normalize `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Maps values of a feature to 0.0 - 1.0 range.

Pipeline Input type: List{double}

Default Pipeline Output: (List{double}) NormalizationResult

##### Remarks

This is a specific case of the [Map](#T-SigStat-Common-Transforms-Map 'SigStat.Common.Transforms.Map') transform.

<a name='P-SigStat-Common-Transforms-Normalize-Input'></a>
### Input `property`

##### Summary

Input

<a name='P-SigStat-Common-Transforms-Normalize-Output'></a>
### Output `property`

##### Summary

Output

<a name='M-SigStat-Common-Transforms-Normalize-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation'></a>
## NormalizeRotation `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Performs rotation normalization on the online signature

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-InputT'></a>
### InputT `property`

##### Summary

Gets or sets the input feature representing the timestamps of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-InputX'></a>
### InputX `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-InputY'></a>
### InputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-OutputX'></a>
### OutputX `property`

##### Summary

Gets or sets the output feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-OutputY'></a>
### OutputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2'></a>
## NormalizeRotation2 `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Performs rotation normalization on the online signature

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-InputX'></a>
### InputX `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-InputY'></a>
### InputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-OutputX'></a>
### OutputX `property`

##### Summary

Gets or sets the output feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-OutputY'></a>
### OutputY `property`

##### Summary

Gets or sets the output feature representing the Y coordinates of an online signature

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation2-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3'></a>
## NormalizeRotation3 `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Performs rotation normalization on the online signature

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-InputX'></a>
### InputX `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-InputY'></a>
### InputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-OutputX'></a>
### OutputX `property`

##### Summary

Gets or sets the output feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-OutputY'></a>
### OutputY `property`

##### Summary

Gets or sets the output feature representing the Y coordinates of an online signature

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotation3-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX'></a>
## NormalizeRotationForX `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Performs rotation normalization on the online signature

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-InputT'></a>
### InputT `property`

##### Summary

Gets or sets the input feature representing the timestamps of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-InputX'></a>
### InputX `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-InputY'></a>
### InputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-OutputX'></a>
### OutputX `property`

##### Summary

Gets or sets the output feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-OutputY'></a>
### OutputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-NormalizeRotationForX-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Framework-Samplers-OddNSampler'></a>
## OddNSampler `type`

##### Namespace

SigStat.Common.Framework.Samplers

##### Summary

Selects the first N signatures with odd index for training

<a name='M-SigStat-Common-Framework-Samplers-OddNSampler-#ctor-System-Int32-'></a>
### #ctor(n) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| n | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Count of signatures used for training |

<a name='P-SigStat-Common-Framework-Samplers-OddNSampler-N'></a>
### N `property`

##### Summary

Count of signatures used for training

<a name='T-SigStat-Common-Transforms-OnePixelThinning'></a>
## OnePixelThinning `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Iteratively thins the input binary raster with the [OnePixelThinningStep](#T-SigStat-Common-Algorithms-OnePixelThinningStep 'SigStat.Common.Algorithms.OnePixelThinningStep') algorithm.

Pipeline Input type: bool[,]

Default Pipeline Output: (bool[,]) OnePixelThinningResult

<a name='P-SigStat-Common-Transforms-OnePixelThinning-Input'></a>
### Input `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') for the binary image of the signature

<a name='P-SigStat-Common-Transforms-OnePixelThinning-Output'></a>
### Output `property`

##### Summary

Output [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') for the binary image of the signature

<a name='M-SigStat-Common-Transforms-OnePixelThinning-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Algorithms-OnePixelThinningStep'></a>
## OnePixelThinningStep `type`

##### Namespace

SigStat.Common.Algorithms

##### Summary

One pixel thinning algorithm.
Use this after [HSCPThinningStep](#T-SigStat-Common-Algorithms-HSCPThinningStep 'SigStat.Common.Algorithms.HSCPThinningStep') to generate final skeleton.

<a name='P-SigStat-Common-Algorithms-OnePixelThinningStep-ResultChanged'></a>
### ResultChanged `property`

##### Summary

Gets whether the last [Scan](#M-SigStat-Common-Algorithms-OnePixelThinningStep-Scan-System-Boolean[0-,0-]- 'SigStat.Common.Algorithms.OnePixelThinningStep.Scan(System.Boolean[0:,0:])') call was effective.

<a name='M-SigStat-Common-Algorithms-OnePixelThinningStep-Scan-System-Boolean[0-,0-]-'></a>
### Scan(binaryImage) `method`

##### Summary

Does one step of the thinning. Call it iteratively while ResultChanged.
Scans the input matrix and generates a 1-pixel thinned version.

##### Returns

Thinned binary raster.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binaryImage | [System.Boolean[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean[0: 'System.Boolean[0:') | Binary raster. |

<a name='T-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier'></a>
## OptimalDtwClassifier `type`

##### Namespace

SigStat.Common.PipelineItems.Classifiers

##### Summary

This [IDistanceClassifier](#T-SigStat-Common-Pipeline-IDistanceClassifier 'SigStat.Common.Pipeline.IDistanceClassifier') implementation will consider both test and 
training samples and claculate the threshold to separate the original and forged
signatures to approximate EER. Note that this classifier is not applicable for 
real world scenarios. It was developed to test the theoratical boundaries of 
threshold based classification

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.Pipeline.IDistanceClassifier](#T-SigStat-Common-Pipeline-IDistanceClassifier 'SigStat.Common.Pipeline.IDistanceClassifier')

<a name='M-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-#ctor-System-Func{System-Double[],System-Double[],System-Double}-'></a>
### #ctor(distanceFunction) `constructor`

##### Summary

Initializes a new instance of the [OptimalDtwClassifier](#T-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| distanceFunction | [System.Func{System.Double[],System.Double[],System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Double[],System.Double[],System.Double}') | The distance function. |

<a name='P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-DistanceFunction'></a>
### DistanceFunction `property`

##### Summary

The function used to calculate the distance between two data points during DTW calculation

<a name='P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-Features'></a>
### Features `property`

##### Summary

[FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor')s to consider during classification

<a name='P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-Sampler'></a>
### Sampler `property`

##### Summary

[Sampler](#P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-Sampler 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier.Sampler') used for selecting training and test sets during a benchmark

<a name='P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-WarpingWindowLength'></a>
### WarpingWindowLength `property`

##### Summary

Length of the warping window to be used with DTW

<a name='M-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-Test-SigStat-Common-Pipeline-ISignerModel,SigStat-Common-Signature-'></a>
### Test() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}-'></a>
### Train() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel'></a>
## OptimalDtwSignerModel `type`

##### Namespace

SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier

##### Summary

Represents a trained model for [OptimalDtwClassifier](#T-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier 'SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier')

<a name='P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel-DistanceMatrix'></a>
### DistanceMatrix `property`

##### Summary

DTW distance matrix of the signatures

<a name='P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel-ErrorRates'></a>
### ErrorRates `property`

##### Summary

Gets or sets the error rates corresponding to specific thresholds

<a name='P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel-SignatureDistanceFromTraining'></a>
### SignatureDistanceFromTraining `property`

##### Summary

Gets or sets the signature distance from training.

<a name='P-SigStat-Common-PipelineItems-Classifiers-OptimalDtwClassifier-OptimalDtwSignerModel-Threshold'></a>
### Threshold `property`

##### Summary

A threshold, that will be used for classification. Signatures with
an average DTW distance from the genuines above this threshold will
be classified as forgeries

<a name='T-SigStat-Common-Origin'></a>
## Origin `type`

##### Namespace

SigStat.Common

##### Summary

Represents our knowledge on the origin of a signature.

<a name='F-SigStat-Common-Origin-Forged'></a>
### Forged `constants`

##### Summary

The [Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') is a forgery.

<a name='F-SigStat-Common-Origin-Genuine'></a>
### Genuine `constants`

##### Summary

The [Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature')'s origin is verified to be from [Signer](#P-SigStat-Common-Signature-Signer 'SigStat.Common.Signature.Signer')

<a name='F-SigStat-Common-Origin-Unknown'></a>
### Unknown `constants`

##### Summary

Use this in practice before a signature is verified.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType'></a>
## OriginType `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Origin specification for [TranslatePreproc](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc')

<a name='F-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType-CenterOfGravity'></a>
### CenterOfGravity `constants`

##### Summary

Center of gravity

<a name='F-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType-Maximum'></a>
### Maximum `constants`

##### Summary

Maximum

<a name='F-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType-Minimum'></a>
### Minimum `constants`

##### Summary

Minimum

<a name='F-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType-Predefined'></a>
### Predefined `constants`

##### Summary

Predefined

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation'></a>
## OrthognalRotation `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Performs rotation normalization on the online signature

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-InputT'></a>
### InputT `property`

##### Summary

Gets or sets the input feature representing the timestamps of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-InputX'></a>
### InputX `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-InputY'></a>
### InputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-OutputX'></a>
### OutputX `property`

##### Summary

Gets or sets the output feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-OutputY'></a>
### OutputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-OrthognalRotation-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Pipeline-Output'></a>
## Output `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Annotates an output [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') in a transformation pipeline

<a name='M-SigStat-Common-Pipeline-Output-#ctor-System-String-'></a>
### #ctor(Default) `constructor`

##### Summary

Initializes a new instance of the [Output](#T-SigStat-Common-Pipeline-Output 'SigStat.Common.Pipeline.Output') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Default | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The default. |

<a name='M-SigStat-Common-Pipeline-Output-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [Output](#T-SigStat-Common-Pipeline-Output 'SigStat.Common.Pipeline.Output') class.

##### Parameters

This constructor has no parameters.

<a name='F-SigStat-Common-Pipeline-Output-Default'></a>
### Default `constants`

##### Summary

The default value for the property

<a name='T-SigStat-Common-Helpers-Excel-Palette'></a>
## Palette `type`

##### Namespace

SigStat.Common.Helpers.Excel

##### Summary



<a name='M-SigStat-Common-Helpers-Excel-Palette-#ctor-System-Drawing-Color,System-Drawing-Color,System-Drawing-Color-'></a>
### #ctor(main,dark,light) `constructor`

##### Summary

Initializes a new instance of the [Palette](#T-SigStat-Common-Helpers-Excel-Palette 'SigStat.Common.Helpers.Excel.Palette') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| main | [System.Drawing.Color](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Drawing.Color 'System.Drawing.Color') | The main color |
| dark | [System.Drawing.Color](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Drawing.Color 'System.Drawing.Color') | The dark color |
| light | [System.Drawing.Color](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Drawing.Color 'System.Drawing.Color') | The light color |

<a name='P-SigStat-Common-Helpers-Excel-Palette-DarkColor'></a>
### DarkColor `property`

##### Summary

Gets or sets the color for rendering darker elements

<a name='P-SigStat-Common-Helpers-Excel-Palette-LightColor'></a>
### LightColor `property`

##### Summary

Gets or sets the color for rendering bright elements

<a name='P-SigStat-Common-Helpers-Excel-Palette-MainColor'></a>
### MainColor `property`

##### Summary

Gets or sets the main color used in the palette

<a name='T-SigStat-Common-Helpers-Excel-PaletteStorage'></a>
## PaletteStorage `type`

##### Namespace

SigStat.Common.Helpers.Excel

##### Summary

Stores color information for every ExcelColor

<a name='M-SigStat-Common-Helpers-Excel-PaletteStorage-GetPalette-SigStat-Common-Helpers-Excel-ExcelColor-'></a>
### GetPalette(excelColor) `method`

##### Summary

Get the Palette assigned to the color

##### Returns

The specified palette

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| excelColor | [SigStat.Common.Helpers.Excel.ExcelColor](#T-SigStat-Common-Helpers-Excel-ExcelColor 'SigStat.Common.Helpers.Excel.ExcelColor') |  |

<a name='T-SigStat-Common-Pipeline-ParallelTransformPipeline'></a>
## ParallelTransformPipeline `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Runs pipeline items in parallel.

Default Pipeline Output: Range of all the Item outputs.

<a name='F-SigStat-Common-Pipeline-ParallelTransformPipeline-Items'></a>
### Items `constants`

##### Summary

List of transforms to be run parallel.

<a name='P-SigStat-Common-Pipeline-ParallelTransformPipeline-PipelineInputs'></a>
### PipelineInputs `property`

##### Summary

Gets the pipeline inputs.

<a name='P-SigStat-Common-Pipeline-ParallelTransformPipeline-PipelineOutputs'></a>
### PipelineOutputs `property`

##### Summary

Gets the pipeline outputs.

<a name='M-SigStat-Common-Pipeline-ParallelTransformPipeline-Add-SigStat-Common-ITransformation-'></a>
### Add(newItem) `method`

##### Summary

Add new transform to the list.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newItem | [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation') |  |

<a name='M-SigStat-Common-Pipeline-ParallelTransformPipeline-GetEnumerator'></a>
### GetEnumerator() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Pipeline-ParallelTransformPipeline-Transform-SigStat-Common-Signature-'></a>
### Transform(signature) `method`

##### Summary

Executes transform [Items](#F-SigStat-Common-Pipeline-ParallelTransformPipeline-Items 'SigStat.Common.Pipeline.ParallelTransformPipeline.Items') parallel.
Passes input features for each.
Output is a range of all the Item outputs.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to execute transform on. |

<a name='T-SigStat-Common-Algorithms-PatternMatching3x3'></a>
## PatternMatching3x3 `type`

##### Namespace

SigStat.Common.Algorithms

##### Summary

Binary 3x3 pattern matcher with rotating option.

<a name='M-SigStat-Common-Algorithms-PatternMatching3x3-#ctor-System-Nullable{System-Boolean}[0-,0-]-'></a>
### #ctor(pattern) `constructor`

##### Summary

Initializes a new instance of the [PatternMatching3x3](#T-SigStat-Common-Algorithms-PatternMatching3x3 'SigStat.Common.Algorithms.PatternMatching3x3') class with given pattern.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| pattern | [System.Nullable{System.Boolean}[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}[0:') | 3x3 pattern. null: don't care. |

<a name='M-SigStat-Common-Algorithms-PatternMatching3x3-Match-System-Boolean[0-,0-]-'></a>
### Match(input) `method`

##### Summary

Match the 3x3 input with the 3x3 pattern.

##### Returns

True if the pattern matches.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.Boolean[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean[0: 'System.Boolean[0:') |  |

<a name='M-SigStat-Common-Algorithms-PatternMatching3x3-RotMatch-System-Boolean[0-,0-]-'></a>
### RotMatch(input) `method`

##### Summary

Match the 3x3 input with the 3x3 pattern from all 4 directions.

##### Returns

True if the pattern matches from at least one direction.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.Boolean[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean[0: 'System.Boolean[0:') |  |

<a name='M-SigStat-Common-Algorithms-PatternMatching3x3-Rotate-System-Nullable{System-Boolean}[0-,0-]-'></a>
### Rotate(input) `method`

##### Summary

Rotate a 3x3 pattern by 90d.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.Nullable{System.Boolean}[0:](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}[0:') |  |

<a name='T-SigStat-Common-PipelineBase'></a>
## PipelineBase `type`

##### Namespace

SigStat.Common

##### Summary

TODO: Ideiglenes osztaly, C# 8.0 ban ezt atalakitani default implementacios interface be.
ILoggerObject, IProgress, IPipelineIO default implementacioja.

<a name='M-SigStat-Common-PipelineBase-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase') class.

##### Parameters

This constructor has no parameters.

<a name='P-SigStat-Common-PipelineBase-Logger'></a>
### Logger `property`

##### Summary

*Inherit from parent.*

<a name='P-SigStat-Common-PipelineBase-PipelineInputs'></a>
### PipelineInputs `property`

##### Summary

A collection of inputs for the pipeline elements

<a name='P-SigStat-Common-PipelineBase-PipelineOutputs'></a>
### PipelineOutputs `property`

##### Summary

A collection of outputs for the pipeline elements

<a name='P-SigStat-Common-PipelineBase-Progress'></a>
### Progress `property`

##### Summary

*Inherit from parent.*

<a name='M-SigStat-Common-PipelineBase-OnProgressChanged'></a>
### OnProgressChanged() `method`

##### Summary

Raises the [](#E-SigStat-Common-PipelineBase-ProgressChanged 'SigStat.Common.PipelineBase.ProgressChanged') event

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Pipeline-PipelineInput'></a>
## PipelineInput `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Represents an input for a [PipelineItem](#F-SigStat-Common-Pipeline-PipelineInput-PipelineItem 'SigStat.Common.Pipeline.PipelineInput.PipelineItem')

<a name='M-SigStat-Common-Pipeline-PipelineInput-#ctor-System-Object,System-Reflection-PropertyInfo-'></a>
### #ctor(PipelineItem,PI) `constructor`

##### Summary

Initializes a new instance of the [PipelineInput](#T-SigStat-Common-Pipeline-PipelineInput 'SigStat.Common.Pipeline.PipelineInput') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| PipelineItem | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The pipeline item. |
| PI | [System.Reflection.PropertyInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.PropertyInfo 'System.Reflection.PropertyInfo') | The pi. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Pipeline Input '{PropName}' of '{PipelineItem.ToString()}' not public |

<a name='P-SigStat-Common-Pipeline-PipelineInput-AutoSetMode'></a>
### AutoSetMode `property`

##### Summary

Gets the AutoSetMode

<a name='P-SigStat-Common-Pipeline-PipelineInput-FD'></a>
### FD `property`

##### Summary

Gets or sets the fd.

<a name='P-SigStat-Common-Pipeline-PipelineInput-IsCollectionOfFeatureDescriptors'></a>
### IsCollectionOfFeatureDescriptors `property`

##### Summary

Gets a value indicating whether this instance is collection of feature descriptors.

<a name='P-SigStat-Common-Pipeline-PipelineInput-PropName'></a>
### PropName `property`

##### Summary

Gets the name of the property.

<a name='P-SigStat-Common-Pipeline-PipelineInput-Type'></a>
### Type `property`

##### Summary

Gets the type of the property

<a name='T-SigStat-Common-Pipeline-PipelineOutput'></a>
## PipelineOutput `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Represents an output for a [PipelineItem](#F-SigStat-Common-Pipeline-PipelineOutput-PipelineItem 'SigStat.Common.Pipeline.PipelineOutput.PipelineItem')

<a name='M-SigStat-Common-Pipeline-PipelineOutput-#ctor-System-Object,System-Reflection-PropertyInfo-'></a>
### #ctor(PipelineItem,PI) `constructor`

##### Summary

Initializes a new instance of the [PipelineOutput](#T-SigStat-Common-Pipeline-PipelineOutput 'SigStat.Common.Pipeline.PipelineOutput') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| PipelineItem | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The pipeline item. |
| PI | [System.Reflection.PropertyInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.PropertyInfo 'System.Reflection.PropertyInfo') | The pi. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Pipeline Output '{PropName}' of '{PipelineItem.ToString()}' not public |

<a name='P-SigStat-Common-Pipeline-PipelineOutput-Default'></a>
### Default `property`

##### Summary

Gets the default value

<a name='P-SigStat-Common-Pipeline-PipelineOutput-FD'></a>
### FD `property`

##### Summary

Gets or sets the fd.

<a name='P-SigStat-Common-Pipeline-PipelineOutput-IsCollectionOfFeatureDescriptors'></a>
### IsCollectionOfFeatureDescriptors `property`

##### Summary

Gets a value indicating whether this instance is collection of feature descriptors.

<a name='P-SigStat-Common-Pipeline-PipelineOutput-IsTemporary'></a>
### IsTemporary `property`

##### Summary

Gets a value indicating whether this instance is temporary.

<a name='P-SigStat-Common-Pipeline-PipelineOutput-PropName'></a>
### PropName `property`

##### Summary

Gets the name of the property.

<a name='P-SigStat-Common-Pipeline-PipelineOutput-Type'></a>
### Type `property`

##### Summary

Gets the type of the property

<a name='T-SigStat-Common-Transforms-RealisticImageGenerator'></a>
## RealisticImageGenerator `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Generates a realistic looking image of the Signature based on standard features. Uses blue ink and white paper. It does NOT save the image to file.

Default Pipeline Input: X, Y, Button, Pressure, Azimuth, Altitude [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

Default Pipeline Output: [Image](#F-SigStat-Common-Features-Image 'SigStat.Common.Features.Image')

<a name='M-SigStat-Common-Transforms-RealisticImageGenerator-#ctor-System-Int32,System-Int32-'></a>
### #ctor(resolutionX,resolutionY) `constructor`

##### Summary

Initializes a new instance of the [RealisticImageGenerator](#T-SigStat-Common-Transforms-RealisticImageGenerator 'SigStat.Common.Transforms.RealisticImageGenerator') class with specified settings.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resolutionX | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Output image width. |
| resolutionY | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Output image height. |

<a name='P-SigStat-Common-Transforms-RealisticImageGenerator-Altitude'></a>
### Altitude `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the altitude values of an online signature

<a name='P-SigStat-Common-Transforms-RealisticImageGenerator-Azimuth'></a>
### Azimuth `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the azimuth values of an online signature

<a name='P-SigStat-Common-Transforms-RealisticImageGenerator-Button'></a>
### Button `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the stroke endings of an online signature

<a name='P-SigStat-Common-Transforms-RealisticImageGenerator-OutputImage'></a>
### OutputImage `property`

##### Summary

Output [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the generated image of the signature

<a name='P-SigStat-Common-Transforms-RealisticImageGenerator-Pressure'></a>
### Pressure `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the pressure values of an online signature

<a name='P-SigStat-Common-Transforms-RealisticImageGenerator-X'></a>
### X `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the X coordinates of an online signature

<a name='P-SigStat-Common-Transforms-RealisticImageGenerator-Y'></a>
### Y `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the Y coordinates of an online signature

<a name='M-SigStat-Common-Transforms-RealisticImageGenerator-Lerp-System-Single,System-Single,System-Single-'></a>
### Lerp(t0,t1,t) `method`

##### Summary

Basic linear interpolation

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| t0 | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') |  |
| t1 | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') |  |
| t | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | 0.0f to 1.0f |

<a name='M-SigStat-Common-Transforms-RealisticImageGenerator-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Helpers-Serialization-RectangleFConverter'></a>
## RectangleFConverter `type`

##### Namespace

SigStat.Common.Helpers.Serialization

##### Summary

Custom serializer for [RectangleF](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Drawing.RectangleF 'System.Drawing.RectangleF') objects

<a name='M-SigStat-Common-Helpers-Serialization-RectangleFConverter-CanConvert-System-Type-'></a>
### CanConvert(objectType) `method`

##### Summary

Tells if the current object is of the correct type

##### Returns

If the object can be converted or not

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the object |

<a name='M-SigStat-Common-Helpers-Serialization-RectangleFConverter-ReadJson-Newtonsoft-Json-JsonReader,System-Type,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### ReadJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Deserializes the [RectangleF](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Drawing.RectangleF 'System.Drawing.RectangleF') json created by the same class

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-Serialization-RectangleFConverter-WriteJson-Newtonsoft-Json-JsonWriter,System-Object,Newtonsoft-Json-JsonSerializer-'></a>
### WriteJson() `method`

##### Summary

Overwrite of the [JsonConverter](#T-Newtonsoft-Json-JsonConverter 'Newtonsoft.Json.JsonConverter') method
Serializes the [RectangleF](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Drawing.RectangleF 'System.Drawing.RectangleF') to json

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale'></a>
## RelativeScale `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Maps values of a feature to a specific range.

InputFeature: feature to be scaled.

OutputFeature: output feature for scaled InputFeature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale-InputFeature'></a>
### InputFeature `property`

##### Summary

Gets or sets the input feature.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale-OutputFeature'></a>
### OutputFeature `property`

##### Summary

Gets or sets the output feature.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale-ReferenceFeature'></a>
### ReferenceFeature `property`

##### Summary

Gets or sets the reference feature.

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-RelativeScale-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Logging-ReportInformationLogger'></a>
## ReportInformationLogger `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Logger for logging report informations.

<a name='M-SigStat-Common-Logging-ReportInformationLogger-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of [ReportInformationLogger](#T-SigStat-Common-Logging-ReportInformationLogger 'SigStat.Common.Logging.ReportInformationLogger').

##### Parameters

This constructor has no parameters.

<a name='F-SigStat-Common-Logging-ReportInformationLogger-reportLogs'></a>
### reportLogs `constants`

##### Summary

Stored logs that contain information for the report.

<a name='P-SigStat-Common-Logging-ReportInformationLogger-ReportLogs'></a>
### ReportLogs `property`

##### Summary

Public read-only interface to reach logged states.

<a name='M-SigStat-Common-Logging-ReportInformationLogger-BeginScope``1-``0-'></a>
### BeginScope\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Logging-ReportInformationLogger-IsEnabled-Microsoft-Extensions-Logging-LogLevel-'></a>
### IsEnabled() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Logging-ReportInformationLogger-Log``1-Microsoft-Extensions-Logging-LogLevel,Microsoft-Extensions-Logging-EventId,``0,System-Exception,System-Func{``0,System-Exception,System-String}-'></a>
### Log\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased'></a>
## ResampleSamplesCountBased `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Resamples an online signature to a specific sample count using the specified [IInterpolation](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-IInterpolation 'SigStat.Common.PipelineItems.Transforms.Preprocessing.IInterpolation') algorithm

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-InputFeatures'></a>
### InputFeatures `property`

##### Summary

Gets or sets the input features.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-InterpolationType'></a>
### InterpolationType `property`

##### Summary

Gets or sets the type of the interpolation.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-NumOfSamples'></a>
### NumOfSamples `property`

##### Summary

Gets or sets the number of samples.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-OriginalTFeature'></a>
### OriginalTFeature `property`

##### Summary

Gets or sets the input timestamp feature.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-OutputFeatures'></a>
### OutputFeatures `property`

##### Summary

Gets or sets the resampled  features.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-ResampledTFeature'></a>
### ResampledTFeature `property`

##### Summary

Gets or sets the resampled timestamp feature.

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-ResampleSamplesCountBased-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-Resize'></a>
## Resize `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Resizes the image to a specified width and height

<a name='P-SigStat-Common-Transforms-Resize-Height'></a>
### Height `property`

##### Summary

The new height. Leave it as null, if you do not want to explicitly specify a given height

<a name='P-SigStat-Common-Transforms-Resize-InputImage'></a>
### InputImage `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the image of the signature

<a name='P-SigStat-Common-Transforms-Resize-OutputImage'></a>
### OutputImage `property`

##### Summary

Output [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the resized image of the signature

<a name='P-SigStat-Common-Transforms-Resize-ResizeFunction'></a>
### ResizeFunction `property`

##### Summary

Set a resize function if you want to dynamically calculate the new width and height of the image

<a name='P-SigStat-Common-Transforms-Resize-Width'></a>
### Width `property`

##### Summary

The new width. Leave it as null, if you do not want to explicitly specify a given width

<a name='M-SigStat-Common-Transforms-Resize-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Result'></a>
## Result `type`

##### Namespace

SigStat.Common

##### Summary

Contains the benchmark results of a single [Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer')

<a name='F-SigStat-Common-Result-Model'></a>
### Model `constants`

<a name='P-SigStat-Common-Result-Aer'></a>
### Aer `property`

##### Summary

Average Error Rate

<a name='P-SigStat-Common-Result-Far'></a>
### Far `property`

##### Summary

False Acceptance Rate

<a name='P-SigStat-Common-Result-Frr'></a>
### Frr `property`

##### Summary

False Rejection Rate

<a name='P-SigStat-Common-Result-Signer'></a>
### Signer `property`

##### Summary

Identifier of the [Signer](#P-SigStat-Common-Result-Signer 'SigStat.Common.Result.Signer')

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate'></a>
## SampleRate `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Performs rotation normalization on the online signature

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-InputP'></a>
### InputP `property`

##### Summary

Gets or sets the input feature representing the timestamps of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-InputX'></a>
### InputX `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-InputY'></a>
### InputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-OutputX'></a>
### OutputX `property`

##### Summary

Gets or sets the output feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-OutputY'></a>
### OutputY `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-samplerate'></a>
### samplerate `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-SampleRate-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Model-SampleRateResults'></a>
## SampleRateResults `type`

##### Namespace

SigStat.Common.Model

<a name='P-SigStat-Common-Model-SampleRateResults-AER'></a>
### AER `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='T-SigStat-Common-Sampler'></a>
## Sampler `type`

##### Namespace

SigStat.Common

##### Summary

Takes samples from a set of [Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature')s by given sampling strategies.
Use this to fine-tune the [VerifierBenchmark](#T-SigStat-Common-VerifierBenchmark 'SigStat.Common.VerifierBenchmark')

<a name='M-SigStat-Common-Sampler-#ctor-System-Func{System-Collections-Generic-List{SigStat-Common-Signature},System-Collections-Generic-List{SigStat-Common-Signature}},System-Func{System-Collections-Generic-List{SigStat-Common-Signature},System-Collections-Generic-List{SigStat-Common-Signature}},System-Func{System-Collections-Generic-List{SigStat-Common-Signature},System-Collections-Generic-List{SigStat-Common-Signature}}-'></a>
### #ctor(references,genuineTests,forgeryTests) `constructor`

##### Summary

Initialize a new instance of the [Sampler](#T-SigStat-Common-Sampler 'SigStat.Common.Sampler') class by given sampling strategies.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| references | [System.Func{System.Collections.Generic.List{SigStat.Common.Signature},System.Collections.Generic.List{SigStat.Common.Signature}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Collections.Generic.List{SigStat.Common.Signature},System.Collections.Generic.List{SigStat.Common.Signature}}') | Strategy to sample genuine signatures to be used for training. |
| genuineTests | [System.Func{System.Collections.Generic.List{SigStat.Common.Signature},System.Collections.Generic.List{SigStat.Common.Signature}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Collections.Generic.List{SigStat.Common.Signature},System.Collections.Generic.List{SigStat.Common.Signature}}') | Strategy to sample genuine signatures to be used for testing. |
| forgeryTests | [System.Func{System.Collections.Generic.List{SigStat.Common.Signature},System.Collections.Generic.List{SigStat.Common.Signature}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Collections.Generic.List{SigStat.Common.Signature},System.Collections.Generic.List{SigStat.Common.Signature}}') | Strategy to sample forged signatures to be used for testing. |

<a name='P-SigStat-Common-Sampler-ForgeryTestFilter'></a>
### ForgeryTestFilter `property`

##### Summary



<a name='P-SigStat-Common-Sampler-GenuineTestFilter'></a>
### GenuineTestFilter `property`

##### Summary



<a name='P-SigStat-Common-Sampler-TrainingFilter'></a>
### TrainingFilter `property`

##### Summary



<a name='M-SigStat-Common-Sampler-SampleForgeryTests-System-Collections-Generic-List{SigStat-Common-Signature}-'></a>
### SampleForgeryTests() `method`

##### Summary

Samples a batch of forged signatures to test on.

##### Returns

Forged signatures to test on.

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Sampler-SampleGenuineTests-System-Collections-Generic-List{SigStat-Common-Signature}-'></a>
### SampleGenuineTests() `method`

##### Summary

Samples a batch of genuine test signatures to test on.

##### Returns

Genuine signatures to test on.

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Sampler-SampleReferences-System-Collections-Generic-List{SigStat-Common-Signature}-'></a>
### SampleReferences() `method`

##### Summary

Samples a batch of genuine reference signatures to train on.

##### Returns

Genuine reference signatures to train on.

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale'></a>
## Scale `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Maps values of a feature to a specific range.

InputFeature: feature to be scaled.

OutputFeature: output feature for scaled InputFeature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale-InputFeature'></a>
### InputFeature `property`

##### Summary

Gets or sets the input feature.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale-Mode'></a>
### Mode `property`

##### Summary

Type of the scaling which defines the scaling behavior

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale-OutputFeature'></a>
### OutputFeature `property`

##### Summary

Gets or sets the output feature.

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-ScalingMode'></a>
## ScalingMode `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Mode specification for [Scale](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-Scale 'SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale')

<a name='F-SigStat-Common-PipelineItems-Transforms-Preprocessing-ScalingMode-Scaling1'></a>
### Scaling1 `constants`

##### Summary

Values are scaled into an interval, where the difference between the lower and upper bounds is 1

<a name='F-SigStat-Common-PipelineItems-Transforms-Preprocessing-ScalingMode-ScalingS'></a>
### ScalingS `constants`

##### Summary

Values are scaled based on their standard deviation

<a name='T-SigStat-Common-Pipeline-SequentialTransformPipeline'></a>
## SequentialTransformPipeline `type`

##### Namespace

SigStat.Common.Pipeline

##### Summary

Runs pipeline items in a sequence.

Default Pipeline Output: Output of the last Item in the sequence.

<a name='F-SigStat-Common-Pipeline-SequentialTransformPipeline-Items'></a>
### Items `constants`

##### Summary

List of transforms to be run in sequence.

<a name='P-SigStat-Common-Pipeline-SequentialTransformPipeline-PipelineInputs'></a>
### PipelineInputs `property`

##### Summary

Gets the pipeline inputs.

<a name='P-SigStat-Common-Pipeline-SequentialTransformPipeline-PipelineOutputs'></a>
### PipelineOutputs `property`

##### Summary

Gets the pipeline outputs.

<a name='M-SigStat-Common-Pipeline-SequentialTransformPipeline-Add-SigStat-Common-ITransformation-'></a>
### Add(newItem) `method`

##### Summary

Add new transform to the list.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newItem | [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation') |  |

<a name='M-SigStat-Common-Pipeline-SequentialTransformPipeline-GetEnumerator'></a>
### GetEnumerator() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Pipeline-SequentialTransformPipeline-Transform-SigStat-Common-Signature-'></a>
### Transform(signature) `method`

##### Summary

Executes transform [Items](#F-SigStat-Common-Pipeline-SequentialTransformPipeline-Items 'SigStat.Common.Pipeline.SequentialTransformPipeline.Items') in sequence.
Passes input features for each.
Output is the output of the last Item in the sequence.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to execute transform on. |

<a name='T-SigStat-Common-Helpers-SerializationHelper'></a>
## SerializationHelper `type`

##### Namespace

SigStat.Common.Helpers

##### Summary

Json serialization and deserialization using the custom resolver  [VerifierResolver](#T-SigStat-Common-Helpers-Serialization-VerifierResolver 'SigStat.Common.Helpers.Serialization.VerifierResolver')

<a name='M-SigStat-Common-Helpers-SerializationHelper-DeserializeFromFile``1-System-String-'></a>
### DeserializeFromFile\`\`1(path) `method`

##### Summary

Constructs object from file given by a path

##### Returns

The object that was serialized to the file

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Relative path to the file |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | A type which has a public parameterless constructor |

<a name='M-SigStat-Common-Helpers-SerializationHelper-Deserialize``1-System-String-'></a>
### Deserialize\`\`1(s) `method`

##### Summary

Constructs object from strings that were serialized previously

##### Returns

The object that was serialized

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| s | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The serialized string |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | A type which has a public parameterless constructor |

<a name='M-SigStat-Common-Helpers-SerializationHelper-GetSettings-System-Boolean-'></a>
### GetSettings() `method`

##### Summary

Settings used for the serialization methods

##### Returns

A new settings object

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Helpers-SerializationHelper-JsonSerializeToFile``1-``0,System-String,System-Boolean-'></a>
### JsonSerializeToFile\`\`1(o,path) `method`

##### Summary

Writes object to file to the given by path in json format

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| o | [\`\`0](#T-``0 '``0') | The object |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Relative path |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the object |

<a name='M-SigStat-Common-Helpers-SerializationHelper-JsonSerialize``1-``0,System-Boolean-'></a>
### JsonSerialize\`\`1(o) `method`

##### Summary

Creates json string from object

##### Returns

The json string constructed from the object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| o | [\`\`0](#T-``0 '``0') | The object |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the object |

<a name='T-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11'></a>
## SigComp11 `type`

##### Namespace

SigStat.Common.Loaders.SigComp11DutchLoader

##### Summary

Set of features containing raw data loaded from MCYT-format database.

<a name='F-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11-T'></a>
### T `constants`

##### Summary

T values from the online signature imported from the SVC2004 database

<a name='F-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11-X'></a>
### X `constants`

##### Summary

X cooridnates from the online signature imported from the MCYT database

<a name='F-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11-Y'></a>
### Y `constants`

##### Summary

Y cooridnates from the online signature imported from the MCYT database

<a name='F-SigStat-Common-Loaders-SigComp11DutchLoader-SigComp11-Z'></a>
### Z `constants`

##### Summary

Z cooridnates from the online signature imported from the MCYT database

<a name='T-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch'></a>
## SigComp11Ch `type`

##### Namespace

SigStat.Common.Loaders.SigComp11ChineseLoader

##### Summary

Set of features containing raw data loaded from SigComp11Chinese-format database.

<a name='F-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch-P'></a>
### P `constants`

##### Summary

Z cooridnates from the online signature imported from the SigComp11Chinese database

<a name='F-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch-T'></a>
### T `constants`

##### Summary

T values from the online signature imported from the SigComp11Chinese database

<a name='F-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch-X'></a>
### X `constants`

##### Summary

X cooridnates from the online signature imported from the SigComp11Chinese database

<a name='F-SigStat-Common-Loaders-SigComp11ChineseLoader-SigComp11Ch-Y'></a>
### Y `constants`

##### Summary

Y cooridnates from the online signature imported from the SigComp11Chinese database

<a name='T-SigStat-Common-Loaders-SigComp11ChineseLoader'></a>
## SigComp11ChineseLoader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

[DataSetLoader](#T-SigStat-Common-Loaders-DataSetLoader 'SigStat.Common.Loaders.DataSetLoader') for the SigComp11Chinese dataset

<a name='M-SigStat-Common-Loaders-SigComp11ChineseLoader-#ctor-System-String,System-Boolean-'></a>
### #ctor(databasePath,standardFeatures) `constructor`

##### Summary

Initializes a new instance of the [SigComp11ChineseLoader](#T-SigStat-Common-Loaders-SigComp11ChineseLoader 'SigStat.Common.Loaders.SigComp11ChineseLoader') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The database path. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` features will be also stored in [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

<a name='P-SigStat-Common-Loaders-SigComp11ChineseLoader-DatabasePath'></a>
### DatabasePath `property`

##### Summary

Gets or sets the database path.

<a name='P-SigStat-Common-Loaders-SigComp11ChineseLoader-SamplingFrequency'></a>
### SamplingFrequency `property`

##### Summary

Sampling Frequency of this database

<a name='P-SigStat-Common-Loaders-SigComp11ChineseLoader-StandardFeatures'></a>
### StandardFeatures `property`

##### Summary

Gets or sets a value indicating whether features are also loaded as [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

<a name='M-SigStat-Common-Loaders-SigComp11ChineseLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-SigComp11ChineseLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean-'></a>
### LoadSignature(signature,stream,standardFeatures) `method`

##### Summary

Loads one signature from specified stream.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to write features to. |
| stream | [System.IO.MemoryStream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.MemoryStream 'System.IO.MemoryStream') | Stream to read MCYT data from. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

##### Remarks

Based on Mohammad's MCYT reader.

<a name='T-SigStat-Common-Loaders-SigComp11DutchLoader'></a>
## SigComp11DutchLoader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

[DataSetLoader](#T-SigStat-Common-Loaders-DataSetLoader 'SigStat.Common.Loaders.DataSetLoader') for the SigComp11Dutch dataset

<a name='M-SigStat-Common-Loaders-SigComp11DutchLoader-#ctor-System-String,System-Boolean-'></a>
### #ctor(databasePath,standardFeatures) `constructor`

##### Summary

Initializes a new instance of the [SigComp11DutchLoader](#T-SigStat-Common-Loaders-SigComp11DutchLoader 'SigStat.Common.Loaders.SigComp11DutchLoader') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The database path. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` features will be also stored in [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

<a name='P-SigStat-Common-Loaders-SigComp11DutchLoader-DatabasePath'></a>
### DatabasePath `property`

##### Summary

Gets or sets the database path.

<a name='P-SigStat-Common-Loaders-SigComp11DutchLoader-SamplingFrequency'></a>
### SamplingFrequency `property`

##### Summary

Sampling Frequency of this database

<a name='P-SigStat-Common-Loaders-SigComp11DutchLoader-StandardFeatures'></a>
### StandardFeatures `property`

##### Summary

Gets or sets a value indicating whether features are also loaded as [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

<a name='M-SigStat-Common-Loaders-SigComp11DutchLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-SigComp11DutchLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean-'></a>
### LoadSignature(signature,stream,standardFeatures) `method`

##### Summary

Loads one signature from specified stream.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to write features to. |
| stream | [System.IO.MemoryStream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.MemoryStream 'System.IO.MemoryStream') | Stream to read MCYT data from. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

##### Remarks

Based on Mohammad's MCYT reader.

<a name='T-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese'></a>
## SigComp13Japanese `type`

##### Namespace

SigStat.Common.Loaders.SigComp13JapaneseLoader

##### Summary

Set of features containing raw data loaded from SigComp13Japanese-format database.

<a name='F-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese-P'></a>
### P `constants`

##### Summary

Z cooridnates from the online signature imported from the SigComp13Japanese database (100 - pen down, 0 - pen up)

<a name='F-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese-T'></a>
### T `constants`

##### Summary

Generated T values from the online signature imported from the SigComp13Japanese database

<a name='F-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese-X'></a>
### X `constants`

##### Summary

X cooridnates from the online signature imported from the SigComp13Japanese database

<a name='F-SigStat-Common-Loaders-SigComp13JapaneseLoader-SigComp13Japanese-Y'></a>
### Y `constants`

##### Summary

Y cooridnates from the online signature imported from the SigComp13Japanese database

<a name='T-SigStat-Common-Loaders-SigComp13JapaneseLoader'></a>
## SigComp13JapaneseLoader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

[DataSetLoader](#T-SigStat-Common-Loaders-DataSetLoader 'SigStat.Common.Loaders.DataSetLoader') for the SigComp13Japanese dataset

<a name='M-SigStat-Common-Loaders-SigComp13JapaneseLoader-#ctor-System-String,System-Boolean-'></a>
### #ctor(databasePath,standardFeatures) `constructor`

##### Summary

Initializes a new instance of the [SigComp13JapaneseLoader](#T-SigStat-Common-Loaders-SigComp13JapaneseLoader 'SigStat.Common.Loaders.SigComp13JapaneseLoader') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The database path. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` features will be also stored in [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

<a name='P-SigStat-Common-Loaders-SigComp13JapaneseLoader-DatabasePath'></a>
### DatabasePath `property`

##### Summary

Gets or sets the database path.

<a name='P-SigStat-Common-Loaders-SigComp13JapaneseLoader-SamplingFrequency'></a>
### SamplingFrequency `property`

##### Summary

Sampling Frequency of this database

<a name='P-SigStat-Common-Loaders-SigComp13JapaneseLoader-StandardFeatures'></a>
### StandardFeatures `property`

##### Summary

Gets or sets a value indicating whether features are also loaded as [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

<a name='M-SigStat-Common-Loaders-SigComp13JapaneseLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-SigComp13JapaneseLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean-'></a>
### LoadSignature(signature,stream,standardFeatures) `method`

##### Summary

Loads one signature from specified stream.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to write features to. |
| stream | [System.IO.MemoryStream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.MemoryStream 'System.IO.MemoryStream') | Stream to read SigComp13Japanese data from. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

##### Remarks

Based on Mohammad's MCYT reader.

<a name='T-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15'></a>
## SigComp15 `type`

##### Namespace

SigStat.Common.Loaders.SigComp15GermanLoader

##### Summary

Set of features containing raw data loaded from SigComp15German-format database.

<a name='F-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15-P'></a>
### P `constants`

##### Summary

Z cooridnates from the online signature imported from the SigComp15German database

<a name='F-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15-T'></a>
### T `constants`

##### Summary

T values from the online signature imported from the SigComp15German database

<a name='F-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15-X'></a>
### X `constants`

##### Summary

X cooridnates from the online signature imported from the SigComp15German database

<a name='F-SigStat-Common-Loaders-SigComp15GermanLoader-SigComp15-Y'></a>
### Y `constants`

##### Summary

Y cooridnates from the online signature imported from the SigComp15German database

<a name='T-SigStat-Common-Loaders-SigComp15GermanLoader'></a>
## SigComp15GermanLoader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

[DataSetLoader](#T-SigStat-Common-Loaders-DataSetLoader 'SigStat.Common.Loaders.DataSetLoader') for the SigComp15German dataset

<a name='M-SigStat-Common-Loaders-SigComp15GermanLoader-#ctor-System-String,System-Boolean-'></a>
### #ctor(databasePath,standardFeatures) `constructor`

##### Summary

Initializes a new instance of the [SigComp15GermanLoader](#T-SigStat-Common-Loaders-SigComp15GermanLoader 'SigStat.Common.Loaders.SigComp15GermanLoader') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The database path. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` features will be also stored in [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

<a name='P-SigStat-Common-Loaders-SigComp15GermanLoader-DatabasePath'></a>
### DatabasePath `property`

##### Summary

Gets or sets the database path.

<a name='P-SigStat-Common-Loaders-SigComp15GermanLoader-SamplingFrequency'></a>
### SamplingFrequency `property`

##### Summary

Sampling Frequency of this database

<a name='P-SigStat-Common-Loaders-SigComp15GermanLoader-StandardFeatures'></a>
### StandardFeatures `property`

##### Summary

Gets or sets a value indicating whether features are also loaded as [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

<a name='M-SigStat-Common-Loaders-SigComp15GermanLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-SigComp15GermanLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean-'></a>
### LoadSignature(signature,stream,standardFeatures) `method`

##### Summary

Loads one signature from specified stream.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to write features to. |
| stream | [System.IO.MemoryStream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.MemoryStream 'System.IO.MemoryStream') | Stream to read MCYT data from. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

##### Remarks

Based on Mohammad's MCYT reader.

<a name='T-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19'></a>
## SigComp19 `type`

##### Namespace

SigStat.Common.Loaders.SigComp19OnlineLoader

##### Summary

Set of features containing raw data loaded from SigComp19-format database.

<a name='F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-Altitude'></a>
### Altitude `constants`

##### Summary

Altitude from the online signature imported from the SigComp19 database

<a name='F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-Azimuth'></a>
### Azimuth `constants`

##### Summary

Azimuth from the online signature imported from the SigComp19 database

<a name='F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-Distance'></a>
### Distance `constants`

##### Summary

Distance from the surface of the tablet from the online signature imported from the SigComp19 database

<a name='F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-EventType'></a>
### EventType `constants`

##### Summary

EventType (pen up) values from the online signature imported from the SigComp19 database

<a name='F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-P'></a>
### P `constants`

##### Summary

Pressure from the online signature imported from the SigComp19 database

<a name='F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-T'></a>
### T `constants`

##### Summary

T values from the online signature imported from the SigComp19 database

<a name='F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-X'></a>
### X `constants`

##### Summary

X cooridnates from the online signature imported from the SigComp19 database

<a name='F-SigStat-Common-Loaders-SigComp19OnlineLoader-SigComp19-Y'></a>
### Y `constants`

##### Summary

Y cooridnates from the online signature imported from the SigComp19 database

<a name='T-SigStat-Common-Loaders-SigComp19OnlineLoader'></a>
## SigComp19OnlineLoader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

[DataSetLoader](#T-SigStat-Common-Loaders-DataSetLoader 'SigStat.Common.Loaders.DataSetLoader') for the SigComp19 dataset

<a name='M-SigStat-Common-Loaders-SigComp19OnlineLoader-#ctor-System-String,System-Boolean-'></a>
### #ctor(databasePath,standardFeatures) `constructor`

##### Summary

Initializes a new instance of the [SigComp19OnlineLoader](#T-SigStat-Common-Loaders-SigComp19OnlineLoader 'SigStat.Common.Loaders.SigComp19OnlineLoader') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The database path. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` features will be also stored in [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

<a name='P-SigStat-Common-Loaders-SigComp19OnlineLoader-DatabasePath'></a>
### DatabasePath `property`

##### Summary

Gets or sets the database path.

<a name='P-SigStat-Common-Loaders-SigComp19OnlineLoader-SamplingFrequency'></a>
### SamplingFrequency `property`

##### Summary

sampling frequency for this database

<a name='P-SigStat-Common-Loaders-SigComp19OnlineLoader-StandardFeatures'></a>
### StandardFeatures `property`

##### Summary

Gets or sets a value indicating whether features are also loaded as [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

<a name='M-SigStat-Common-Loaders-SigComp19OnlineLoader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-SigComp19OnlineLoader-LoadSignature-SigStat-Common-Signature,System-IO-MemoryStream,System-Boolean-'></a>
### LoadSignature(signature,stream,standardFeatures) `method`

##### Summary

Loads one signature from specified stream.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to write features to. |
| stream | [System.IO.MemoryStream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.MemoryStream 'System.IO.MemoryStream') | Stream to read SigComp19 data from. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

##### Remarks

Based on Mohammad's MCYT reader.

<a name='T-SigStat-Common-SigStatEvents'></a>
## SigStatEvents `type`

##### Namespace

SigStat.Common

##### Summary

Standard event identifiers used by the SigStat system

<a name='F-SigStat-Common-SigStatEvents-BenchmarkEvent'></a>
### BenchmarkEvent `constants`

##### Summary

Events originating from a benchmark

<a name='F-SigStat-Common-SigStatEvents-VerifierEvent'></a>
### VerifierEvent `constants`

##### Summary

Events originating from a verifier

<a name='T-SigStat-Common-Logging-SigStatLogState'></a>
## SigStatLogState `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Base state used in report information logging.

<a name='P-SigStat-Common-Logging-SigStatLogState-Source'></a>
### Source `property`

##### Summary

Object from which the state originates.

<a name='T-SigStat-Common-Signature'></a>
## Signature `type`

##### Namespace

SigStat.Common

##### Summary

Represents a signature as a collection of features, containing the data that flows in the pipeline.

<a name='M-SigStat-Common-Signature-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a signature instance

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-Signature-#ctor-System-String,SigStat-Common-Origin,SigStat-Common-Signer-'></a>
### #ctor(signatureID,origin,signer) `constructor`

##### Summary

Initializes a signature instance with the given properties

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signatureID | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| origin | [SigStat.Common.Origin](#T-SigStat-Common-Origin 'SigStat.Common.Origin') |  |
| signer | [SigStat.Common.Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer') |  |

<a name='P-SigStat-Common-Signature-ID'></a>
### ID `property`

##### Summary

An identifier for the Signature. Keep it unique to be useful for logs.

<a name='P-SigStat-Common-Signature-Item-System-String-'></a>
### Item `property`

##### Summary

Gets or sets the specified feature.

##### Returns

The feature object without cast.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='P-SigStat-Common-Signature-Item-SigStat-Common-FeatureDescriptor-'></a>
### Item `property`

##### Summary

Gets or sets the specified feature.

##### Returns

The feature object without cast.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureDescriptor | [SigStat.Common.FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') |  |

<a name='P-SigStat-Common-Signature-Origin'></a>
### Origin `property`

##### Summary

Represents our knowledge on the origin of the signature. [Unknown](#F-SigStat-Common-Origin-Unknown 'SigStat.Common.Origin.Unknown') may be used in practice before it is verified.

<a name='P-SigStat-Common-Signature-Signer'></a>
### Signer `property`

##### Summary

A reference to the [Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer') who this signature belongs to. (The origin is not constrained to be genuine.)

<a name='M-SigStat-Common-Signature-GetAggregateFeature-System-Collections-Generic-List{SigStat-Common-FeatureDescriptor}-'></a>
### GetAggregateFeature(fs) `method`

##### Summary

Aggregate multiple features into one. Example: X, Y features -> P.xy feature.
Use this for example at DTW algorithm input.

##### Returns

Aggregated feature value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fs | [System.Collections.Generic.List{SigStat.Common.FeatureDescriptor}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{SigStat.Common.FeatureDescriptor}') | List of features to aggregate. |

<a name='M-SigStat-Common-Signature-GetEnumerator'></a>
### GetEnumerator() `method`

##### Summary

Returns an enumerator that iterates through the features.

##### Returns

An enumerator that can be used to iterate through the features.

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Signature-GetFeatureDescriptors'></a>
### GetFeatureDescriptors() `method`

##### Summary

Gets a collection of [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor')s that are used in this signature.

##### Returns

A collection of [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor')s.

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Signature-GetFeature``1-System-String-'></a>
### GetFeature\`\`1(featureKey) `method`

##### Summary

Gets the specified feature.

##### Returns

The casted feature object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-SigStat-Common-Signature-GetFeature``1-SigStat-Common-FeatureDescriptor{``0}-'></a>
### GetFeature\`\`1(featureDescriptor) `method`

##### Summary

Gets the specified feature. This is the preferred way.

##### Returns

The casted feature object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureDescriptor | [SigStat.Common.FeatureDescriptor{\`\`0}](#T-SigStat-Common-FeatureDescriptor{``0} 'SigStat.Common.FeatureDescriptor{``0}') |  |

<a name='M-SigStat-Common-Signature-GetFeature``1-SigStat-Common-FeatureDescriptor-'></a>
### GetFeature\`\`1(featureDescriptor) `method`

##### Summary

Gets the specified feature. This is the preferred way.

##### Returns

The casted feature object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureDescriptor | [SigStat.Common.FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') |  |

<a name='M-SigStat-Common-Signature-HasFeature-SigStat-Common-FeatureDescriptor-'></a>
### HasFeature(featureDescriptor) `method`

##### Summary

Returns true if the signature contains the specified feature

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureDescriptor | [SigStat.Common.FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') |  |

<a name='M-SigStat-Common-Signature-HasFeature-System-String-'></a>
### HasFeature(featureKey) `method`

##### Summary

Returns true if the signature contains the specified feature

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-SigStat-Common-Signature-SetFeature``1-SigStat-Common-FeatureDescriptor,``0-'></a>
### SetFeature\`\`1(featureDescriptor,feature) `method`

##### Summary

Sets the specified feature.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureDescriptor | [SigStat.Common.FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') | The feature to put the new value in. |
| feature | [\`\`0](#T-``0 '``0') | The value to set. |

<a name='M-SigStat-Common-Signature-SetFeature``1-System-String,``0-'></a>
### SetFeature\`\`1(featureKey,feature) `method`

##### Summary

Sets the specified feature.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The unique key of the feature. |
| feature | [\`\`0](#T-``0 '``0') | The value to set. |

<a name='M-SigStat-Common-Signature-ToString'></a>
### ToString() `method`

##### Summary

Returns a string representation of the signature

##### Returns



##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-SignatureHelper'></a>
## SignatureHelper `type`

##### Namespace

SigStat.Common

<a name='M-SigStat-Common-SignatureHelper-GetSignatureLength-SigStat-Common-Signature-'></a>
### GetSignatureLength(signature) `method`

##### Summary

Return the signature length using Eculidan distance

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') |  |

<a name='M-SigStat-Common-SignatureHelper-SaveImage-SigStat-Common-Signature,System-String-'></a>
### SaveImage(sig,fileName) `method`

##### Summary

Save online signature as file

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sig | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') |  |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-SigStat-Common-Logging-SignatureLogState'></a>
## SignatureLogState `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Specific state used for signature information transiting

<a name='P-SigStat-Common-Logging-SignatureLogState-SignatureID'></a>
### SignatureID `property`

##### Summary

Id of the signature

<a name='P-SigStat-Common-Logging-SignatureLogState-SignerID'></a>
### SignerID `property`

##### Summary

Id of the owning signer

<a name='T-SigStat-Common-Signer'></a>
## Signer `type`

##### Namespace

SigStat.Common

##### Summary

Represents a person as an [ID](#P-SigStat-Common-Signer-ID 'SigStat.Common.Signer.ID') and a list of [Signatures](#P-SigStat-Common-Signer-Signatures 'SigStat.Common.Signer.Signatures').

<a name='P-SigStat-Common-Signer-ID'></a>
### ID `property`

##### Summary

An identifier for the Signer. Keep it unique to be useful for logs.

<a name='P-SigStat-Common-Signer-Signatures'></a>
### Signatures `property`

##### Summary

List of signatures that belong to the signer. 
(Their origin is not constrained to be genuine.)

<a name='M-SigStat-Common-Signer-ToString'></a>
### ToString() `method`

##### Summary

Returns a string representation of a Signer

##### Returns



##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Logging-SignerLogState'></a>
## SignerLogState `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Specific state used for signer information transiting

<a name='P-SigStat-Common-Logging-SignerLogState-SignerID'></a>
### SignerID `property`

##### Summary

Id of the signer

<a name='T-SigStat-Common-Logging-SignerResults'></a>
## SignerResults `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Informations of a signer

<a name='M-SigStat-Common-Logging-SignerResults-#ctor-System-String-'></a>
### #ctor(signerId) `constructor`

##### Summary

Creates a signer result with emty result values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signerId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The id of the signer |

<a name='F-SigStat-Common-Logging-SignerResults-Aer'></a>
### Aer `constants`

##### Summary

Average Error Rate of the signer

<a name='F-SigStat-Common-Logging-SignerResults-Far'></a>
### Far `constants`

##### Summary

False Acceptance Rate of the signer

<a name='F-SigStat-Common-Logging-SignerResults-Frr'></a>
### Frr `constants`

##### Summary

False Rejection Rate of the signer

<a name='P-SigStat-Common-Logging-SignerResults-DistanceMatrix'></a>
### DistanceMatrix `property`

##### Summary

Distacne matrix of the signers signatures

<a name='P-SigStat-Common-Logging-SignerResults-SignerID'></a>
### SignerID `property`

##### Summary

The ID of the signer

<a name='T-SigStat-Common-Logging-SignerResultsLogState'></a>
## SignerResultsLogState `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Specific state used for Signer result transiting

<a name='M-SigStat-Common-Logging-SignerResultsLogState-#ctor-System-String,System-Double,System-Double,System-Double-'></a>
### #ctor(signerId,aer,far,frr) `constructor`

##### Summary

Creates a SignerResultsLogState

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signerId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the signer |
| aer | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Aer |
| far | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Far |
| frr | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Frr |

<a name='P-SigStat-Common-Logging-SignerResultsLogState-Aer'></a>
### Aer `property`

##### Summary

Average error rate

<a name='P-SigStat-Common-Logging-SignerResultsLogState-Far'></a>
### Far `property`

##### Summary

False accaptance rate

<a name='P-SigStat-Common-Logging-SignerResultsLogState-Frr'></a>
### Frr `property`

##### Summary

False rejection rate

<a name='T-SigStat-Common-Helpers-SignerStatisticsHelper'></a>
## SignerStatisticsHelper `type`

##### Namespace

SigStat.Common.Helpers

<a name='M-SigStat-Common-Helpers-SignerStatisticsHelper-GetHeightAvg-SigStat-Common-Signer-'></a>
### GetHeightAvg(signer) `method`

##### Summary

return signer height average

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signer | [SigStat.Common.Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer') |  |

<a name='M-SigStat-Common-Helpers-SignerStatisticsHelper-GetLengthAverage-SigStat-Common-Signer-'></a>
### GetLengthAverage(signer) `method`

##### Summary

Return the average od signatures points number

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signer | [SigStat.Common.Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer') |  |

<a name='M-SigStat-Common-Helpers-SignerStatisticsHelper-GetMaxSignaturePoints-SigStat-Common-Signer-'></a>
### GetMaxSignaturePoints(signer) `method`

##### Summary

return the min signature points number of a signer

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signer | [SigStat.Common.Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer') |  |

<a name='M-SigStat-Common-Helpers-SignerStatisticsHelper-GetMinSignaturePoints-SigStat-Common-Signer-'></a>
### GetMinSignaturePoints(signer) `method`

##### Summary

return the min signature points number of a signer

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signer | [SigStat.Common.Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer') |  |

<a name='M-SigStat-Common-Helpers-SignerStatisticsHelper-GetPointsAvg-SigStat-Common-Signer-'></a>
### GetPointsAvg(signer) `method`

##### Summary

return signer points average

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signer | [SigStat.Common.Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer') |  |

<a name='M-SigStat-Common-Helpers-SignerStatisticsHelper-GetWidthAvg-SigStat-Common-Signer-'></a>
### GetWidthAvg(signer) `method`

##### Summary

Return signer width average

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signer | [SigStat.Common.Signer](#T-SigStat-Common-Signer 'SigStat.Common.Signer') |  |

<a name='T-SigStat-Common-Logging-SimpleConsoleLogger'></a>
## SimpleConsoleLogger `type`

##### Namespace

SigStat.Common.Logging

##### Summary

Logs messages to [Console](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Console 'System.Console'). 
The font color is determined by the severity level.

<a name='M-SigStat-Common-Logging-SimpleConsoleLogger-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of [SimpleConsoleLogger](#T-SigStat-Common-Logging-SimpleConsoleLogger 'SigStat.Common.Logging.SimpleConsoleLogger') with LogLevel set to [Information](#F-Microsoft-Extensions-Logging-LogLevel-Information 'Microsoft.Extensions.Logging.LogLevel.Information').

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-Logging-SimpleConsoleLogger-#ctor-Microsoft-Extensions-Logging-LogLevel-'></a>
### #ctor(logLevel) `constructor`

##### Summary

Initializes a new instance of [SimpleConsoleLogger](#T-SigStat-Common-Logging-SimpleConsoleLogger 'SigStat.Common.Logging.SimpleConsoleLogger') with a custom [LogLevel](#T-Microsoft-Extensions-Logging-LogLevel 'Microsoft.Extensions.Logging.LogLevel').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| logLevel | [Microsoft.Extensions.Logging.LogLevel](#T-Microsoft-Extensions-Logging-LogLevel 'Microsoft.Extensions.Logging.LogLevel') | Initial value for LogLevel. |

<a name='P-SigStat-Common-Logging-SimpleConsoleLogger-LogLevel'></a>
### LogLevel `property`

##### Summary

All events below this level will be filtered

<a name='M-SigStat-Common-Logging-SimpleConsoleLogger-BeginScope``1-``0-'></a>
### BeginScope\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Logging-SimpleConsoleLogger-IsEnabled-Microsoft-Extensions-Logging-LogLevel-'></a>
### IsEnabled() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Logging-SimpleConsoleLogger-Log``1-Microsoft-Extensions-Logging-LogLevel,Microsoft-Extensions-Logging-EventId,``0,System-Exception,System-Func{``0,System-Exception,System-String}-'></a>
### Log\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-SimpleRenderingTransformation'></a>
## SimpleRenderingTransformation `type`

##### Namespace

SigStat.Common

##### Summary

Renders an image of the signature based on the available online information (X,Y,Dpi)

<a name='M-SigStat-Common-SimpleRenderingTransformation-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-StrokeHelper'></a>
## StrokeHelper `type`

##### Namespace

SigStat.Common

##### Summary

Helper class for locating and manipulating strokes in an online signature

<a name='M-SigStat-Common-StrokeHelper-GetStroke-System-Int32,System-Double-'></a>
### GetStroke(startIndex,pressure) `method`

##### Summary

Creates a [StrokeInterval](#T-SigStat-Common-StrokeInterval 'SigStat.Common.StrokeInterval') and initializes it with the given parameters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| startIndex | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The start index. |
| pressure | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The pressure. |

<a name='M-SigStat-Common-StrokeHelper-GetStrokes-SigStat-Common-Signature-'></a>
### GetStrokes(signature) `method`

##### Summary

Gets the strokes from an online signature with standard features. Note that
the signature has to contain [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T') and [Pressure](#F-SigStat-Common-Features-Pressure 'SigStat.Common.Features.Pressure')

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | An online signature with standard features |

<a name='T-SigStat-Common-StrokeInterval'></a>
## StrokeInterval `type`

##### Namespace

SigStat.Common

##### Summary

Represents a stroke in an online signature

<a name='M-SigStat-Common-StrokeInterval-#ctor-System-Int32,System-Int32,SigStat-Common-StrokeType-'></a>
### #ctor(startIndex,endIndex,strokeType) `constructor`

##### Summary

Initializes a new instance of the [StrokeInterval](#T-SigStat-Common-StrokeInterval 'SigStat.Common.StrokeInterval') struct.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| startIndex | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index of the firs element |
| endIndex | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index of the last element |
| strokeType | [SigStat.Common.StrokeType](#T-SigStat-Common-StrokeType 'SigStat.Common.StrokeType') | Type of the stroke. |

<a name='F-SigStat-Common-StrokeInterval-EndIndex'></a>
### EndIndex `constants`

##### Summary

The index of the last element

<a name='F-SigStat-Common-StrokeInterval-StartIndex'></a>
### StartIndex `constants`

##### Summary

The index of the firs element

<a name='F-SigStat-Common-StrokeInterval-StrokeType'></a>
### StrokeType `constants`

##### Summary

The [StrokeType](#F-SigStat-Common-StrokeInterval-StrokeType 'SigStat.Common.StrokeInterval.StrokeType') of the stroke.

<a name='T-SigStat-Common-StrokeType'></a>
## StrokeType `type`

##### Namespace

SigStat.Common

##### Summary

Describes the type of a stroke

<a name='F-SigStat-Common-StrokeType-Down'></a>
### Down `constants`

##### Summary

The stroke was made on the writing surface (tablet, paper etc.)

<a name='F-SigStat-Common-StrokeType-Unknown'></a>
### Unknown `constants`

##### Summary

The type of the stroke is not known

<a name='F-SigStat-Common-StrokeType-Up'></a>
### Up `constants`

##### Summary

The stroke was made in the air (the pen did not tuch the tablet/paper)

<a name='T-SigStat-Common-Loaders-Svc2004'></a>
## Svc2004 `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

Set of features containing raw data loaded from SVC2004-format database.

<a name='F-SigStat-Common-Loaders-Svc2004-All'></a>
### All `constants`

##### Summary

A list of all Svc2004 feature descriptors

<a name='F-SigStat-Common-Loaders-Svc2004-Altitude'></a>
### Altitude `constants`

##### Summary

Altitude values from the online signature imported from the SVC2004 database

<a name='F-SigStat-Common-Loaders-Svc2004-Azimuth'></a>
### Azimuth `constants`

##### Summary

Azimuth values from the online signature imported from the SVC2004 database

<a name='F-SigStat-Common-Loaders-Svc2004-Button'></a>
### Button `constants`

##### Summary

Button values from the online signature imported from the SVC2004 database

<a name='F-SigStat-Common-Loaders-Svc2004-Pressure'></a>
### Pressure `constants`

##### Summary

Pressure values from the online signature imported from the SVC2004 database

<a name='F-SigStat-Common-Loaders-Svc2004-T'></a>
### T `constants`

##### Summary

T values from the online signature imported from the SVC2004 database

<a name='F-SigStat-Common-Loaders-Svc2004-X'></a>
### X `constants`

##### Summary

X cooridnates from the online signature imported from the SVC2004 database

<a name='F-SigStat-Common-Loaders-Svc2004-Y'></a>
### Y `constants`

##### Summary

Y cooridnates from the online signature imported from the SVC2004 database

<a name='T-SigStat-Common-Loaders-Svc2004Loader'></a>
## Svc2004Loader `type`

##### Namespace

SigStat.Common.Loaders

##### Summary

Loads SVC2004-format database from .zip

<a name='M-SigStat-Common-Loaders-Svc2004Loader-#ctor-System-String,System-Boolean-'></a>
### #ctor(databasePath,standardFeatures) `constructor`

##### Summary

Initializes a new instance of the [Svc2004Loader](#T-SigStat-Common-Loaders-Svc2004Loader 'SigStat.Common.Loaders.Svc2004Loader') class with specified database.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Represents the path, to load the signatures from. It supports two basic approaches: |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data ([Svc2004](#T-SigStat-Common-Loaders-Svc2004 'SigStat.Common.Loaders.Svc2004')) to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

<a name='M-SigStat-Common-Loaders-Svc2004Loader-#ctor-System-String,System-Boolean,System-Predicate{SigStat-Common-Signer}-'></a>
### #ctor(databasePath,standardFeatures,signerFilter) `constructor`

##### Summary

Initializes a new instance of the [Svc2004Loader](#T-SigStat-Common-Loaders-Svc2004Loader 'SigStat.Common.Loaders.Svc2004Loader') class with specified database.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Represents the path, to load the signatures from. It supports two basic approaches: |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data ([Svc2004](#T-SigStat-Common-Loaders-Svc2004 'SigStat.Common.Loaders.Svc2004')) to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |
| signerFilter | [System.Predicate{SigStat.Common.Signer}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Predicate 'System.Predicate{SigStat.Common.Signer}') | Sets the [SignerFilter](#P-SigStat-Common-Loaders-Svc2004Loader-SignerFilter 'SigStat.Common.Loaders.Svc2004Loader.SignerFilter') property |

<a name='P-SigStat-Common-Loaders-Svc2004Loader-DatabasePath'></a>
### DatabasePath `property`

##### Summary

Gets or sets the database path.

<a name='P-SigStat-Common-Loaders-Svc2004Loader-SamplingFrequency'></a>
### SamplingFrequency `property`

##### Summary

Sampling Frequency of the SVC database

<a name='P-SigStat-Common-Loaders-Svc2004Loader-SignerFilter'></a>
### SignerFilter `property`

##### Summary

Ignores any signers during the loading, that do not match the predicate

<a name='P-SigStat-Common-Loaders-Svc2004Loader-StandardFeatures'></a>
### StandardFeatures `property`

##### Summary

Gets or sets a value indicating whether features are also loaded as [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

<a name='M-SigStat-Common-Loaders-Svc2004Loader-EnumerateSigners-System-Predicate{SigStat-Common-Signer}-'></a>
### EnumerateSigners() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-Loaders-Svc2004Loader-LoadSignature-SigStat-Common-Signature,System-String,System-Boolean-'></a>
### LoadSignature(signature,path,standardFeatures) `method`

##### Summary

Loads one signature from specified file path.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to write features to. |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Path to a file of format "U*S*.txt" |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

<a name='M-SigStat-Common-Loaders-Svc2004Loader-LoadSignature-SigStat-Common-Signature,System-IO-Stream,System-Boolean-'></a>
### LoadSignature(signature,stream,standardFeatures) `method`

##### Summary

Loads one signature from specified stream.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') | Signature to write features to. |
| stream | [System.IO.Stream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream 'System.IO.Stream') | Stream to read svc2004 data from. |
| standardFeatures | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Convert loaded data to standard [Features](#T-SigStat-Common-Features 'SigStat.Common.Features'). |

<a name='T-SigStat-Common-Transforms-TangentExtraction'></a>
## TangentExtraction `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Extracts tangent values of the standard X, Y [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

Default Pipeline Input: X, Y [Features](#T-SigStat-Common-Features 'SigStat.Common.Features')

Default Pipeline Output: (List{double})  Tangent

<a name='P-SigStat-Common-Transforms-TangentExtraction-OutputTangent'></a>
### OutputTangent `property`

##### Summary

Gets or sets the output feature representing the tangent angles of an online signature

<a name='P-SigStat-Common-Transforms-TangentExtraction-X'></a>
### X `property`

##### Summary

Gets or sets the input feature representing the X coordinates of an online signature

<a name='P-SigStat-Common-Transforms-TangentExtraction-Y'></a>
### Y `property`

##### Summary

Gets or sets the input feature representing the Y coordinates of an online signature

<a name='M-SigStat-Common-Transforms-TangentExtraction-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Helpers-Excel-TextLevel'></a>
## TextLevel `type`

##### Namespace

SigStat.Common.Helpers.Excel

##### Summary

Paragraph style setting

<a name='F-SigStat-Common-Helpers-Excel-TextLevel-Heading1'></a>
### Heading1 `constants`

##### Summary

Level 1 heading

<a name='F-SigStat-Common-Helpers-Excel-TextLevel-Heading2'></a>
### Heading2 `constants`

##### Summary

Level 2 heading

<a name='F-SigStat-Common-Helpers-Excel-TextLevel-Heading3'></a>
### Heading3 `constants`

##### Summary

Level 3 heading

<a name='F-SigStat-Common-Helpers-Excel-TextLevel-Normal'></a>
### Normal `constants`

##### Summary

Normal document body style

<a name='F-SigStat-Common-Helpers-Excel-TextLevel-Title'></a>
### Title `constants`

##### Summary

Main title

<a name='T-SigStat-Common-Transforms-TimeReset'></a>
## TimeReset `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Sequential pipeline to reset time values to begin at 0.
The following Transforms are called: Extrema, Multiply, AddVector.

Default Pipeline Input: [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T')

Default Pipeline Output: [T](#F-SigStat-Common-Features-T 'SigStat.Common.Features.T')

<a name='M-SigStat-Common-Transforms-TimeReset-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [TimeReset](#T-SigStat-Common-Transforms-TimeReset 'SigStat.Common.Transforms.TimeReset') class.

##### Parameters

This constructor has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot'></a>
## TimeSlot `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations

##### Summary

Helper class for [FillPenUpDurations](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations 'SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations')

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot-EndTime'></a>
### EndTime `property`

##### Summary

Gets or sets the end time of the slot

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot-Length'></a>
### Length `property`

##### Summary

Gets the length of the slot

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot-PenDown'></a>
### PenDown `property`

##### Summary

This indicates whether the pen touches the paper during the time slot

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-FillPenUpDurations-TimeSlot-StartTime'></a>
### StartTime `property`

##### Summary

Gets or sets the start time of the slot

<a name='T-SigStat-Common-Transforms-Translate'></a>
## Translate `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Sequential pipeline to translate X and Y [Features](#T-SigStat-Common-Features 'SigStat.Common.Features') by specified vector (constant or feature).
The following Transforms are called: [AddConst](#T-SigStat-Common-Transforms-AddConst 'SigStat.Common.Transforms.AddConst') twice, or [AddVector](#T-SigStat-Common-Transforms-AddVector 'SigStat.Common.Transforms.AddVector').

Default Pipeline Input: [X](#F-SigStat-Common-Features-X 'SigStat.Common.Features.X'), [Y](#F-SigStat-Common-Features-Y 'SigStat.Common.Features.Y')

Default Pipeline Output: [X](#F-SigStat-Common-Features-X 'SigStat.Common.Features.X'), [Y](#F-SigStat-Common-Features-Y 'SigStat.Common.Features.Y')

<a name='M-SigStat-Common-Transforms-Translate-#ctor-System-Double,System-Double-'></a>
### #ctor(xAdd,yAdd) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| xAdd | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Value to translate [X](#F-SigStat-Common-Features-X 'SigStat.Common.Features.X') by. |
| yAdd | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Value to translate [Y](#F-SigStat-Common-Features-Y 'SigStat.Common.Features.Y') by. |

<a name='M-SigStat-Common-Transforms-Translate-#ctor-SigStat-Common-FeatureDescriptor{System-Collections-Generic-List{System-Double}}-'></a>
### #ctor(vectorFeature) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vectorFeature | [SigStat.Common.FeatureDescriptor{System.Collections.Generic.List{System.Double}}](#T-SigStat-Common-FeatureDescriptor{System-Collections-Generic-List{System-Double}} 'SigStat.Common.FeatureDescriptor{System.Collections.Generic.List{System.Double}}') | Feature to translate X and Y by. |

<a name='P-SigStat-Common-Transforms-Translate-InputX'></a>
### InputX `property`

##### Summary

The feature representing the horizontal coordinates of an online signature

<a name='P-SigStat-Common-Transforms-Translate-InputY'></a>
### InputY `property`

##### Summary

The feature representing the vertical coordinates of an online signature

<a name='P-SigStat-Common-Transforms-Translate-OutputX'></a>
### OutputX `property`

##### Summary

Target feature for storing the transformed horizontal coordinates

<a name='P-SigStat-Common-Transforms-Translate-OutputY'></a>
### OutputY `property`

##### Summary

Target feature for storing the transformed vertical coordinates

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc'></a>
## TranslatePreproc `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

This transformations can be used to translate the coordinates of an online signature

##### See Also

- [SigStat.Common.PipelineBase](#T-SigStat-Common-PipelineBase 'SigStat.Common.PipelineBase')
- [SigStat.Common.ITransformation](#T-SigStat-Common-ITransformation 'SigStat.Common.ITransformation')

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [TranslatePreproc](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc') class.

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-#ctor-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType-'></a>
### #ctor(goalOrigin) `constructor`

##### Summary

Initializes a new instance of the [TranslatePreproc](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc 'SigStat.Common.PipelineItems.Transforms.Preprocessing.TranslatePreproc') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| goalOrigin | [SigStat.Common.PipelineItems.Transforms.Preprocessing.OriginType](#T-SigStat-Common-PipelineItems-Transforms-Preprocessing-OriginType 'SigStat.Common.PipelineItems.Transforms.Preprocessing.OriginType') | The goal origin. |

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-GoalOrigin'></a>
### GoalOrigin `property`

##### Summary

Goal origin of the translation

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-InputFeature'></a>
### InputFeature `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') (e.g. [X](#F-SigStat-Common-Features-X 'SigStat.Common.Features.X'))

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-NewOrigin'></a>
### NewOrigin `property`

##### Summary

New origin after the translation

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-OutputFeature'></a>
### OutputFeature `property`

##### Summary

Output [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') (e.g. [X](#F-SigStat-Common-Features-X 'SigStat.Common.Features.X'))

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-TranslatePreproc-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Transforms-Trim'></a>
## Trim `type`

##### Namespace

SigStat.Common.Transforms

##### Summary

Trims unnecessary empty space from a binary raster.

Pipeline Input type: bool[,]

Default Pipeline Output: (bool[,]) Trimmed

<a name='M-SigStat-Common-Transforms-Trim-#ctor-System-Int32-'></a>
### #ctor(framewidth) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| framewidth | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Leave a border around the trimmed area. framewidth > 0 |

<a name='P-SigStat-Common-Transforms-Trim-Input'></a>
### Input `property`

##### Summary

Input [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the image of the signature

<a name='P-SigStat-Common-Transforms-Trim-Output'></a>
### Output `property`

##### Summary

Output [FeatureDescriptor](#T-SigStat-Common-FeatureDescriptor 'SigStat.Common.FeatureDescriptor') describing the trimed image of the signature

<a name='M-SigStat-Common-Transforms-Trim-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale'></a>
## UniformScale `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Maps values of a feature to a specific range and another proportional.

BaseDimension: feature modelled the base dimension of the scaling.

ProportionalDimension: feature modelled the dimension scaled proportionally to the base dimension.

BaseDimensionOutput: output feature for scaled BaseDimension

ProportionalDimensionOutput: output feature for scaled ProportionalDimension

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-BaseDimension'></a>
### BaseDimension `property`

##### Summary

Gets or sets the base dimension.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-BaseDimensionOutput'></a>
### BaseDimensionOutput `property`

##### Summary

Gets or sets the output base dimension output.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-NewMaxBaseValue'></a>
### NewMaxBaseValue `property`

##### Summary

Upper bound of the interval, in which the base dimension will be scaled

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-NewMinBaseValue'></a>
### NewMinBaseValue `property`

##### Summary

Lower bound of the interval, in which the base dimension will be scaled

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-NewMinProportionalValue'></a>
### NewMinProportionalValue `property`

##### Summary

Lower bound of the interval, in which the proportional dimension will be scaled

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-ProportionalDimension'></a>
### ProportionalDimension `property`

##### Summary

Gets or sets the ProportionalDimension.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-ProportionalDimensionOutput'></a>
### ProportionalDimensionOutput `property`

##### Summary

Gets or sets the output proportional dimension output.

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-UniformScale-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-Framework-Samplers-UniversalSampler'></a>
## UniversalSampler `type`

##### Namespace

SigStat.Common.Framework.Samplers

##### Summary

Selects a given number of signatures for training and testing

<a name='M-SigStat-Common-Framework-Samplers-UniversalSampler-#ctor-System-Int32,System-Int32-'></a>
### #ctor(trainingCount,testCount) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| trainingCount | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Count of signatures to use for training |
| testCount | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Count of signatures to use for testing |

<a name='P-SigStat-Common-Framework-Samplers-UniversalSampler-TestCount'></a>
### TestCount `property`

##### Summary

Count of signatures to use for testing

<a name='P-SigStat-Common-Framework-Samplers-UniversalSampler-TrainingCount'></a>
### TrainingCount `property`

##### Summary

Count of signatures to use for training

<a name='T-SigStat-Common-Model-Verifier'></a>
## Verifier `type`

##### Namespace

SigStat.Common.Model

##### Summary

Uses pipelines to transform, train on, and classify [Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') objects.

<a name='M-SigStat-Common-Model-Verifier-#ctor-Microsoft-Extensions-Logging-ILogger-'></a>
### #ctor(logger) `constructor`

##### Summary

Initializes a new instance of the [Verifier](#T-SigStat-Common-Model-Verifier 'SigStat.Common.Model.Verifier') class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| logger | [Microsoft.Extensions.Logging.ILogger](#T-Microsoft-Extensions-Logging-ILogger 'Microsoft.Extensions.Logging.ILogger') | Initializes the Logger property of the [Verifier](#T-SigStat-Common-Model-Verifier 'SigStat.Common.Model.Verifier') |

<a name='M-SigStat-Common-Model-Verifier-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [Verifier](#T-SigStat-Common-Model-Verifier 'SigStat.Common.Model.Verifier') class.

##### Parameters

This constructor has no parameters.

<a name='M-SigStat-Common-Model-Verifier-#ctor-SigStat-Common-Model-Verifier-'></a>
### #ctor(baseVerifier) `constructor`

##### Summary

Initializes a new instance of the [Verifier](#T-SigStat-Common-Model-Verifier 'SigStat.Common.Model.Verifier') class based on another Verifier instance

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| baseVerifier | [SigStat.Common.Model.Verifier](#T-SigStat-Common-Model-Verifier 'SigStat.Common.Model.Verifier') | The reference verifier |

<a name='P-SigStat-Common-Model-Verifier-AllFeatures'></a>
### AllFeatures `property`

##### Summary

This property is used by the Serializer to access a list of all FeatureDescriptors

<a name='P-SigStat-Common-Model-Verifier-Classifier'></a>
### Classifier `property`

##### Summary

Gets or sets the classifier pipeline. Hands over the Logger object.

<a name='P-SigStat-Common-Model-Verifier-Logger'></a>
### Logger `property`

##### Summary

Gets or sets the class responsible for logging

<a name='P-SigStat-Common-Model-Verifier-Pipeline'></a>
### Pipeline `property`

##### Summary

Gets or sets the transform pipeline. Hands over the Logger object.

<a name='P-SigStat-Common-Model-Verifier-SignerModel'></a>
### SignerModel `property`

##### Summary

Gets or sets the signer model.

<a name='M-SigStat-Common-Model-Verifier-Test-SigStat-Common-Signature-'></a>
### Test(signature) `method`

##### Summary

Verifies the genuinity of `signature`.

##### Returns

True if `signature` passes the verification test.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signature | [SigStat.Common.Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') |  |

<a name='M-SigStat-Common-Model-Verifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}-'></a>
### Train(signatures) `method`

##### Summary

Trains the verifier with a list of signatures. Uses the [Pipeline](#P-SigStat-Common-Model-Verifier-Pipeline 'SigStat.Common.Model.Verifier.Pipeline') to extract features,
and [Classifier](#P-SigStat-Common-Model-Verifier-Classifier 'SigStat.Common.Model.Verifier.Classifier') to find an optimized limit.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| signatures | [System.Collections.Generic.List{SigStat.Common.Signature}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{SigStat.Common.Signature}') | The list of signatures to train on. |

##### Remarks

Note that `signatures` may contain both genuine and forged signatures.
It's up to the classifier, whether it takes advantage of both classes

<a name='T-SigStat-Common-VerifierBenchmark'></a>
## VerifierBenchmark `type`

##### Namespace

SigStat.Common

##### Summary

Benchmarking class to test error rates of a [Verifier](#T-SigStat-Common-Model-Verifier 'SigStat.Common.Model.Verifier')

<a name='M-SigStat-Common-VerifierBenchmark-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [VerifierBenchmark](#T-SigStat-Common-VerifierBenchmark 'SigStat.Common.VerifierBenchmark') class.
Sets the [Sampler](#T-SigStat-Common-Sampler 'SigStat.Common.Sampler') to the default [FirstNSampler](#T-SigStat-Common-Framework-Samplers-FirstNSampler 'SigStat.Common.Framework.Samplers.FirstNSampler').

##### Parameters

This constructor has no parameters.

<a name='F-SigStat-Common-VerifierBenchmark-loader'></a>
### loader `constants`

##### Summary

The loader to take care of [Signature](#T-SigStat-Common-Signature 'SigStat.Common.Signature') database loading.

<a name='F-SigStat-Common-VerifierBenchmark-sampler'></a>
### sampler `constants`

##### Summary

Defines the sampling strategy for the benchmark.

<a name='P-SigStat-Common-VerifierBenchmark-Loader'></a>
### Loader `property`

##### Summary

The loader that will provide the database for benchmarking

<a name='P-SigStat-Common-VerifierBenchmark-Logger'></a>
### Logger `property`

##### Summary

Gets or sets the attached [ILogger](#T-Microsoft-Extensions-Logging-ILogger 'Microsoft.Extensions.Logging.ILogger') object used to log messages. Hands it over to the verifier.

<a name='P-SigStat-Common-VerifierBenchmark-Parameters'></a>
### Parameters `property`

##### Summary

A key value store that can be used to store custom information about the benchmark

<a name='P-SigStat-Common-VerifierBenchmark-Progress'></a>
### Progress `property`

##### Summary

*Inherit from parent.*

<a name='P-SigStat-Common-VerifierBenchmark-Sampler'></a>
### Sampler `property`

##### Summary

The [Sampler](#T-SigStat-Common-Sampler 'SigStat.Common.Sampler') to be used for benchmarking

<a name='P-SigStat-Common-VerifierBenchmark-Verifier'></a>
### Verifier `property`

##### Summary

Gets or sets the [Verifier](#T-SigStat-Common-Model-Verifier 'SigStat.Common.Model.Verifier') to be benchmarked.

<a name='M-SigStat-Common-VerifierBenchmark-Dump-System-String,System-Collections-Generic-IEnumerable{System-Collections-Generic-KeyValuePair{System-String,System-String}}-'></a>
### Dump(filename,parameters) `method`

##### Summary

Dumps the results of the benchmark in a file.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| filename | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The filename. |
| parameters | [System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}}') | The custom parameters of the benchmark (to be included in the dump) |

<a name='M-SigStat-Common-VerifierBenchmark-Execute-System-Boolean-'></a>
### Execute(ParallelMode) `method`

##### Summary

Execute the benchmarking process.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ParallelMode | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-SigStat-Common-VerifierBenchmark-Execute-System-Int32-'></a>
### Execute(degreeOfParallelism) `method`

##### Summary

Execute the benchmarking process with a degree of parallelism.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| degreeOfParallelism | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Degree of parallelism is the maximum number of concurrently executing tasks. |

<a name='T-SigStat-Common-Helpers-Serialization-VerifierResolver'></a>
## VerifierResolver `type`

##### Namespace

SigStat.Common.Helpers.Serialization

##### Summary

Custom resolver for customizing the json serialization

<a name='M-SigStat-Common-Helpers-Serialization-VerifierResolver-CreateProperties-System-Type,Newtonsoft-Json-MemberSerialization-'></a>
### CreateProperties(type,memberSerialization) `method`

##### Summary

Decides if the current property should be serialized or not

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the current property |
| memberSerialization | [Newtonsoft.Json.MemberSerialization](#T-Newtonsoft-Json-MemberSerialization 'Newtonsoft.Json.MemberSerialization') | The type of member serialization in Json.NET |

<a name='M-SigStat-Common-Helpers-Serialization-VerifierResolver-CreateProperty-System-Reflection-MemberInfo,Newtonsoft-Json-MemberSerialization-'></a>
### CreateProperty(member,memberSerialization) `method`

##### Summary

Selects which JsonConverter should be used for the property

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| member | [System.Reflection.MemberInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.MemberInfo 'System.Reflection.MemberInfo') | A  [MemberInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.MemberInfo 'System.Reflection.MemberInfo') |
| memberSerialization | [Newtonsoft.Json.MemberSerialization](#T-Newtonsoft-Json-MemberSerialization 'Newtonsoft.Json.MemberSerialization') | The type of member serialization in Json.NET |

<a name='T-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier'></a>
## WeightedClassifier `type`

##### Namespace

SigStat.Common.PipelineItems.Classifiers

##### Summary

Classifies Signatures by weighing other Classifier results.

<a name='F-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-Items'></a>
### Items `constants`

##### Summary

List of classifiers and belonging weights.

<a name='M-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-Add-System-ValueTuple{SigStat-Common-Pipeline-IClassifier,System-Double}-'></a>
### Add(newItem) `method`

##### Summary

Add a new classifier with given weight to the list of items.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newItem | [System.ValueTuple{SigStat.Common.Pipeline.IClassifier,System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ValueTuple 'System.ValueTuple{SigStat.Common.Pipeline.IClassifier,System.Double}') | Classifier with belonging weight. |

<a name='M-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-GetEnumerator'></a>
### GetEnumerator() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-Test-SigStat-Common-Pipeline-ISignerModel,SigStat-Common-Signature-'></a>
### Test() `method`

##### Parameters

This method has no parameters.

<a name='M-SigStat-Common-PipelineItems-Classifiers-WeightedClassifier-Train-System-Collections-Generic-List{SigStat-Common-Signature}-'></a>
### Train() `method`

##### Parameters

This method has no parameters.

<a name='T-SigStat-Common-PipelineItems-Transforms-Preprocessing-ZNormalization'></a>
## ZNormalization `type`

##### Namespace

SigStat.Common.PipelineItems.Transforms.Preprocessing

##### Summary

Maps values of a feature to a specific range.

InputFeature: feature to be scaled.

OutputFeature: output feature for scaled InputFeature

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ZNormalization-InputFeature'></a>
### InputFeature `property`

##### Summary

Gets or sets the input feature.

<a name='P-SigStat-Common-PipelineItems-Transforms-Preprocessing-ZNormalization-OutputFeature'></a>
### OutputFeature `property`

##### Summary

Gets or sets the output feature.

<a name='M-SigStat-Common-PipelineItems-Transforms-Preprocessing-ZNormalization-Transform-SigStat-Common-Signature-'></a>
### Transform() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.
