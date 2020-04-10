using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Models.Food
{
    public class Ingredient
    {
        public String Name { get; private set; }
        public bool IsFodmap { get; private set; }
        public string Label { get; private set; }

        public Ingredient(String name, bool isFodmap, string label)
        {
            this.Name = name;
            this.IsFodmap = isFodmap;
            this.Label = label;
        }
    }
}