using System;
using System.Collections.Generic;
using System.Text;
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
            var expectedJson = "{\r\n  \"Center\": {\r\n    \"X\": 1.0,\r\n    \"Y\": 2.0\r\n  },\r\n  \"Bounds\": {\r\n    \"X\": 0.0,\r\n    \"Y\": 0.0,\r\n    \"Width\": 0.0,\r\n    \"Height\": 0.0\r\n  }\r\n}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedLoop = new Loop(1.0f, 2.0f);
            var loopJson = "{\r\n  \"Center\": {\r\n    \"X\": 1.0,\r\n    \"Y\": 2.0\r\n  },\r\n  \"Bounds\": {\r\n    \"X\": 0.0,\r\n    \"Y\": 0.0,\r\n    \"Width\": 0.0,\r\n    \"Height\": 0.0\r\n  }\r\n}";
            var deserializedLoop = SerializationHelper.Deserialize<Loop>(loopJson);
            Assert.AreEqual(deserializedLoop.Center, expectedLoop.Center);
            Assert.AreEqual(deserializedLoop.Bounds, expectedLoop.Bounds);
        }
    }
}
