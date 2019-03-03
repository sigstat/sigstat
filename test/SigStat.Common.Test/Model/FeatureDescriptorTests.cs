using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using System;
using System.Collections.Generic;

namespace SigStat.Common.Test.Model
{
    [TestClass]
    public class FeatureDescriptorTests
    {
        [TestMethod]
        public void TestGet()
        {
            const string key = "Loop";
            var fd1 = FeatureDescriptor.Get<int>(key);

            Assert.IsNotNull(fd1);
            Assert.AreEqual(key, fd1.Key);
            Assert.ThrowsException<KeyNotFoundException>(() => FeatureDescriptor.Get("Loop1"));

            var fd2 = FeatureDescriptor<int>.Get(key);
            Assert.AreSame(fd1, fd2);
        }

        [TestMethod]
        public void TestIsRegistered()
        {
            const string key = "IsRegisteredKey";
            Assert.AreEqual(false, FeatureDescriptor.IsRegistered(key));
            FeatureDescriptor.Register(key, typeof(int));
            Assert.AreEqual(true, FeatureDescriptor.IsRegistered(key));
        }

        [TestMethod]
        public void TestGetterSetters()
        {
            const string key = "Loop";
            var fd = FeatureDescriptor<int>.Get(key);
            const string expectedName = "TestName";
            fd.Name = expectedName;
            Type expectedType = typeof(int);
            Assert.AreEqual(expectedName, fd.Name);
            Assert.AreEqual(expectedType, fd.FeatureType);
        }

        [TestMethod]
        public void TestIsCollection()
        {
            var fd1 = FeatureDescriptor<int>.Get("1");
            var fd2 = FeatureDescriptor<List<int>>.Get("2");

            Assert.AreEqual(false, fd1.IsCollection);
            Assert.AreEqual(true, fd2.IsCollection);
        }
    }
}
