using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class LoopTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var loop = new Loop(1.0f,2.0f);
            var json = SerializationHelper.JsonSerialize(loop);
            var expectedJson = @"{
              ""Center"": {
                ""X"": 1.0,
                ""Y"": 2.0
              },
              ""Bounds"": {
                ""X"": 0.0,
                ""Y"": 0.0,
                ""Width"": 0.0,
                ""Height"": 0.0
              }
            }";
            JsonAssert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedLoop = new Loop(1.0f, 2.0f);
            var expectedLoopJson = SerializationHelper.JsonSerialize(expectedLoop);
            var deserializedLoop = SerializationHelper.Deserialize<Loop>(expectedLoopJson);
            JsonAssert.AreEqual(expectedLoop, deserializedLoop);
        }
    }
}
