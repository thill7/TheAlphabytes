using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FODfinder.Utility;
using Newtonsoft.Json.Linq;

namespace FODfinder.Models.Food
{
    public class FoodDetailsModels
    {
        public int FdcId { private set; get; }
        public string Description { private set; get; }
        public string BrandOwner { private set; get; }
        public List<Ingredient> Ingredients { private set; get; } = new List<Ingredient>();
        public double ServingSize { private set; get; }
        public string ServingSizeUnit { private set; get; }
        public string ServingSizeFullText { private set; get; }
        public string LabelNutrients { private set; get; }
        public string UPC { private set; get; }

        public FoodDetailsModels(string jsonString, ref FFDBContext db) {
            JObject detailObject = JObject.Parse(jsonString);
            Description = detailObject.SelectToken("description")?.ToString() ?? "";
            BrandOwner = detailObject.SelectToken("brandOwner")?.ToString() ?? "";
            var ingredientString = detailObject.SelectToken("ingredients")?.ToString() ?? "";
            if(!string.IsNullOrEmpty(ingredientString))
            {
                var parsedIngredients = IngredientParser.Parse(ingredientString);
                foreach(var ingredient in parsedIngredients)
                {
                    if(string.IsNullOrEmpty(ingredient))
                    {
                        continue;
                    }
                    var fodmap = db.FODMAPIngredients.Where(f => ingredient.Contains(f.Name.ToLower())).FirstOrDefault();
                    Ingredients.Add(new Ingredient(ingredient, fodmap != null));
                }
            }
            double servingSize;
            ServingSize = Double.TryParse(detailObject.SelectToken("servingSize")?.ToString() ?? "", out servingSize) ? servingSize : 0.0;
            ServingSizeUnit = detailObject.SelectToken("servingSizeUnit")?.ToString() ?? "";
            ServingSizeFullText = ServingSizeCleaner.Clean(detailObject.SelectToken("householdServingFullText")?.ToString() ?? "");
            LabelNutrients = detailObject.SelectToken("labelNutrients")?.ToString() ?? "";
            int fdcId;
            FdcId = int.TryParse(detailObject.SelectToken("fdcId")?.ToString() ?? "", out fdcId) ? fdcId : -1;
            UPC = detailObject.SelectToken("gtinUpc")?.ToString() ?? "";
        }
    }
}