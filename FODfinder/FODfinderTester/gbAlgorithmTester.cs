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
            var ingredients = new List<List<Ingredient>>() 
            { 
                new List<Ingredient>() { new Ingredient("water", false, null) },
                new List<Ingredient>() { new Ingredient("salt", false, null) },
                new List<Ingredient>() { new Ingredient("beef", false, null) }
            };
            var result = Algorithm.ListContainsFodmaps(ingredients);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ListContainsFodmaps_containsFodmaps_returnsTrue()
        {
            var ingredients = new List<List<Ingredient>>()
            {
                new List<Ingredient>() { new Ingredient("sorbitol", true, null) },
                new List<Ingredient>() { new Ingredient("onions", true, null) },
                new List<Ingredient>() { new Ingredient("garlic powder", true, null) }
            };
            var result = Algorithm.ListContainsFodmaps(ingredients);
            Assert.IsTrue(result);
        }
    }
}
