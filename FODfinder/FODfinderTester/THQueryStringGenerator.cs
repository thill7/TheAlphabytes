using System;
using System.Collections.Generic;
using FODfinder.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FODfinderTester
{
    [TestClass]
    public class THQueryStringGenerator
    {
        private FoodController _foodController;
        [TestInitialize]
        public void Init()
        {
            _foodController = new FoodController();
        }

        [TestMethod]
        public void GenerateQueryString_CanCombineMultipleKeyValuePairs()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "user","tanner"},
                {"age","23" }
            };
            string result = _foodController.GenerateQueryString(parameters);
            StringAssert.Equals("user=tanner&age=23", result);
        }

        [TestMethod]
        public void GenerateQueryString_WillNotLeaveEntitiesUnencoded()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "user","tanner hill"},
                {"age","23" }
            };
            string result = _foodController.GenerateQueryString(parameters);
            Assert.IsFalse(Equals("user=tanner hill&age=23", result));
        }
    }
}
