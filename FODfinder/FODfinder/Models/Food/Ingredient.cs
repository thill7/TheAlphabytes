using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Models.Food
{
    public class Ingredient
    {
        public string Name { get; private set; }
        public bool IsFodmap { get; private set; }

        public Ingredient(string name, bool isFodmap)
        {
            this.Name = name;
            this.IsFodmap = isFodmap;
        }
    }
}