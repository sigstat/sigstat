using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;

namespace SigStat.Common.Test.Helpers.Serialization.Pipeline
{
    [TestClass]
    public class ParallelPipelineTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var parallelPipeline = new ParallelTransformPipeline()
            {
                new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},
                new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
            };
            var json = SerializationHelper.JsonSerialize(parallelPipeline);
            var expectedJson = @"{
  ""Items"": [
    {
      ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation, SigStat.Common"",
      ""InputX"":""X | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
      ""InputY"":""Y | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
      ""InputT"":""T | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
      ""OutputX"":""X"",
      ""OutputY"":""Y""
    },
    {
      ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale, SigStat.Common"",
      ""InputFeature"":""X"",
      ""NewMinValue"": 0.0,
      ""NewMaxValue"": 1.0,
      ""OutputFeature"":""X""
    },
    {
      ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale, SigStat.Common"",
      ""InputFeature"":""Y"",
      ""NewMinValue"": 0.0,
      ""NewMaxValue"": 1.0,
      ""OutputFeature"":""Y""
    }
  ]
}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var expectedParallelTransformPipeline = new ParallelTransformPipeline()
            {
                new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},
                new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
            };
            var parallelPipelineJson = @"{
  ""Items"": [
    {
      ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation, SigStat.Common"",
      ""InputX"":""X | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
      ""InputY"":""Y | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
      ""InputT"":""T | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
      ""OutputX"":""X"",
      ""OutputY"":""Y""
    },
    {
      ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale, SigStat.Common"",
      ""InputFeature"":""X"",
      ""NewMinValue"": 0.0,
      ""NewMaxValue"": 1.0,
      ""OutputFeature"":""X""
    },
    {
      ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale, SigStat.Common"",
      ""InputFeature"":""Y"",
      ""NewMinValue"": 0.0,
      ""NewMaxValue"": 1.0,
      ""OutputFeature"":""Y""
    }
  ]
}";
            var deserializedParallelTransformPipeline= SerializationHelper.Deserialize<ParallelTransformPipeline>(parallelPipelineJson);
            Assert.AreEqual(expectedParallelTransformPipeline.Items.Count, deserializedParallelTransformPipeline.Items.Count);
        }
    }
}
