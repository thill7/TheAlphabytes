using System;
using System.Collections.Generic;
using FODfinder.Utility.Algorithm;
using FODfinder.Models.Food;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FODfinderTester
{
    [TestClass]
    public class GbAlgorithmTester
    {
        [TestMethod]
        public void ListContainsFodmaps_nullArgument_throwException()
        {
            Assert.ThrowsException<NullReferenceException>(() => Algorithm.ListContainsFodmaps(null));
        }
        [TestMethod]
        public void ListContainsFodmaps_containsNoFodmaps_returnsFalse()
        {
            var ingredients = new List<Ingredient>() 
            { 
                new Ingredient("water", false, null, Ingredient.Position.Other),
                new Ingredient("salt", false, null, Ingredient.Position.Other),
                new Ingredient("beef", false, null, Ingredient.Position.Other)
            };
            var result = Algorithm.ListContainsFodmaps(ingredients);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ListContainsFodmaps_containsFodmaps_returnsTrue()
        {
            var ingredients = new List<Ingredient>()
            {
                new Ingredient("sorbitol", true, null, Ingredient.Position.Other),
                new Ingredient("onions", true, null, Ingredient.Position.Other),
                new Ingredient("garlic powder", true, null, Ingredient.Position.Other)
            };
            var result = Algorithm.ListContainsFodmaps(ingredients);
            Assert.IsTrue(result);
        }
    }
}
