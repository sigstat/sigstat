using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            TestHelper.AssertJson(loop,json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedLoop = new Loop(1.0f, 2.0f);
            var expectedLoopJson = SerializationHelper.JsonSerialize(expectedLoop);
            var deserializedLoop = SerializationHelper.Deserialize<Loop>(expectedLoopJson);
            TestHelper.AssertJson(expectedLoop, deserializedLoop);
        }
    }
}
