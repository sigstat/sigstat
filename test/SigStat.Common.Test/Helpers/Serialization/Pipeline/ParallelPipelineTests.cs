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
            TestHelper.AssertJson(parallelPipeline, json);
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
            var parallelPipelineJson = SerializationHelper.JsonSerialize(expectedParallelTransformPipeline);
            var deserializedParallelTransformPipeline= SerializationHelper.Deserialize<ParallelTransformPipeline>(parallelPipelineJson);
            TestHelper.AssertJson(expectedParallelTransformPipeline, deserializedParallelTransformPipeline);
        }
    }
}
