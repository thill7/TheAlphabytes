using System;
using System.Collections.Generic;
using FODfinder.Controllers;
using FODfinder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FODfinderTester
{
    [TestClass]
    public class CRSavedFoodsControllerTest
    {
        private SavedFoodsController _savedFoodsController;
        [TestInitialize]
        public void Init()
        {
            _savedFoodsController = new SavedFoodsController();
        }
        [TestMethod]
        public void countSavedFoods_CanCount_Regular()
        {
            //Arrange
            List<SavedFood> testList = new List<SavedFood>
            {
                new SavedFood { usdaFoodID = 1, userID = "35", brand = "brand name", desc = "Hello", upc = "003647898" },
                new SavedFood { usdaFoodID = 1, userID = "35", brand = "brand name", desc = "Hello", upc = "003647898" },
                new SavedFood { usdaFoodID = 1, userID = "35", brand = "brand name", desc = "Hello", upc = "003647898" }
            };
            int expectedResult = 3;

            //Act
            int actualResult = _savedFoodsController.countSavedFoods(testList);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    
        [TestMethod]
        public void countSavedFoods_CanCount_Empty()
        {
            //Arrange
            List<SavedFood> testList = new List<SavedFood>();
            int expectedResult = 0;

            //Act
            int actualResult = _savedFoodsController.countSavedFoods(testList);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
