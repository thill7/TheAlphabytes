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
        public void CleanGenericString_ReturnsLowerCase()
        {
            string result = ServingSizeCleaner.Clean("THIs is 1 teST!");
            Assert.AreEqual("this is 1 test!", result);
        }

        [TestMethod]
        public void CleanONZ_ReturnsOZ()
        {
            string result = ServingSizeCleaner.Clean("12 ONZ");
            Assert.AreEqual("12 oz", result);
        }

        [TestMethod]
        public void CleanOZA_Returns_fl_OZ()
        {
            string result = ServingSizeCleaner.Clean("22 OZA");
            Assert.AreEqual("22 fl oz", result);
        }

        [TestMethod]
        public void CleanStringWithParentheses_ReturnsOnlyFirstPart()
        {
            string result = ServingSizeCleaner.Clean("1 can(355ml)");
            Assert.AreEqual("1 can", result);
        }
    }
}
