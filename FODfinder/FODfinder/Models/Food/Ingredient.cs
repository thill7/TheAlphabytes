using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Models.Food
{
    public class Ingredient
    {
        public string Name { get; set; }
        public bool IsFodmap { get; set; }
        public string Label { get; set; }

        public Ingredient(String name, bool isFodmap, string label)
        {
            this.Name = name;
            this.IsFodmap = isFodmap;
            this.Label = label;
            if (label == "Low-Risk")
            {
                this.IsFodmap = false;
            }
        }

        [JsonConstructor]
        public Ingredient()
        {

        }
    }
}