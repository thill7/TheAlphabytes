using System;
using System.Text;
using System.Collections.Generic;
using FODfinder.Controllers;
using NUnit.Framework;

namespace FODfinderTester
{
    [TestFixture]
    public class THGenerateQueryString
    {
        private FoodController _foodController; 
        [SetUp]
        public void Setup()
        {
            _foodController = new FoodController();
        }

        [Test]
        public void GenerateQueryString_CanCombineMultipleKeyValuePairs()
        {
            Dictionary<string, string> parameters = new Dictionary<string,string>()
            {
                { "user","tanner"},
                {"age","23" }
            };
            string result = _foodController.GenerateQueryString(parameters);
            StringAssert.AreEqualIgnoringCase("user=tanner&age=23", result);
        }

        [Test]
        public void GenerateQueryString_WillEncodeUrlEntities()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "user","tanner hill"},
                {"age","23" }
            };
            string result = _foodController.GenerateQueryString(parameters);
            StringAssert.AreEqualIgnoringCase("user=tanner+hill&age=23", result);
        }
    }
}
