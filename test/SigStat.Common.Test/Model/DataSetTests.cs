using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Test.Model
{
    [TestClass]
    public class DataSetTests
    {

        [TestMethod]
        public void TestGetterSetter()
        {
            DataSet ds = new DataSet();

            Assert.IsNotNull(ds.Signers);
        }
    }
}
