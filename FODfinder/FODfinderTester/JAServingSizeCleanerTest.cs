using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FODfinder.Utility;
using System.Threading.Tasks;

namespace FODfinderTester
{
    [TestClass]
    public class JAServingSizeCleanerTest
    {
        //private ServingSizeCleaner _servingSizeCleaner;
        [TestMethod]
        public void CleanEmptyString_ReturnsEmpty()
        {
            string result = ServingSizeCleaner.Clean("");
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void CleanRegular_ReturnsLowerCase()
        {
            string result = ServingSizeCleaner.Clean("THIs is 1 teST!");
            Assert.AreEqual("this is 1 test!", result);
        }
    }
}
