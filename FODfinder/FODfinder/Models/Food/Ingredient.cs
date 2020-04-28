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
        public Position IngredientPosition { get; set; }

        public Ingredient(string name, bool isFodmap, string label, Position ingredientPosition)
        {
            Name = name;
            IsFodmap = isFodmap;
            Label = label;
            if (label == "Low-Risk")
            {
                IsFodmap = false;
            }
            IngredientPosition = ingredientPosition;
        }

        public enum Position
        {
            Parent,
            LastChild,
            Other,
        }

        [JsonConstructor]
        public Ingredient()
        {

        }
    }
}