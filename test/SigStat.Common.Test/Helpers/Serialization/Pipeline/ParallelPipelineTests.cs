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
            };
            var json = SerializationHelper.JsonSerialize(parallelPipeline, true);
            var expectedJson = @"{
              ""Items"": [
                {
                  ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation, SigStat.Common"",
                  ""InputX"":""X"",
                  ""InputY"":""Y"",
                  ""InputT"":""T"",
                  ""OutputX"":""X"",
                  ""OutputY"":""Y""
                },
                {
                  ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale, SigStat.Common"",
                  ""InputFeature"":""X"",
                  ""NewMinValue"": 0.0,
                  ""NewMaxValue"": 1.0,
                  ""OutputFeature"":""X""
                }
              ]
            }";
            JsonAssert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var expectedParallelTransformPipeline = new ParallelTransformPipeline()
            {
                new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},
                new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
            };
            var parallelPipelineJson = SerializationHelper.JsonSerialize(expectedParallelTransformPipeline);
            var deserializedParallelTransformPipeline= SerializationHelper.Deserialize<ParallelTransformPipeline>(parallelPipelineJson);
            JsonAssert.AreEqual(expectedParallelTransformPipeline, deserializedParallelTransformPipeline);
        }
    }
}
