using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Model;

namespace SigStat.Common.Test.Model
{
    [TestClass]
    public class SignerTests
    {
        [TestMethod]
        public void TestGetterSetter()
        {
            Signer signer = new Signer();
            const string ID = "123";

            signer.ID = ID;
            Assert.AreEqual(ID, signer.ID);
        }
    }
}
