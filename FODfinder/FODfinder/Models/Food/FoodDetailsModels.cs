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
        public List<List<Ingredient>> PrimaryIngredients { private set; get; } = new List<List<Ingredient>>();
        public List<List<Ingredient>> SecondaryIngredients { private set; get; } = new List<List<Ingredient>>();
        public double ServingSize { private set; get; }
        public string ServingSizeUnit { private set; get; }
        public string LabelNutrients { private set; get; }
        public string UPC { private set; get; }

        public FoodDetailsModels(string jsonString, ref FFDBContext db) {
            JObject detailObject = JObject.Parse(jsonString);
            Description = detailObject.SelectToken("description")?.ToString() ?? "";
            BrandOwner = detailObject.SelectToken("brandOwner")?.ToString() ?? "";
            var ingredientString = detailObject.SelectToken("ingredients")?.ToString() ?? "";
            if(!string.IsNullOrEmpty(ingredientString))
            {
                var primaryListofLists = new List<List<string>>();
                var secondaryListofLists = new List<List<string>>();
                IngredientParser.Parse(ingredientString, out primaryListofLists, out secondaryListofLists);
                foreach (var ingredient in primaryListofLists)
                {
                    if(ingredient.Count > 1)
                    {
                        List<Ingredient> list = new List<Ingredient>();
                        foreach (var subIngredient in ingredient)
                        {
                            var fodmap = db.FODMAPIngredients.Where(f => subIngredient.Contains(f.Name.ToLower())).FirstOrDefault();
                            list.Add(new Ingredient(subIngredient, fodmap != null));
                        }
                        PrimaryIngredients.Add(list);
                    }
                    else if (ingredient.Count == 1)
                    {
                        var fodmap = db.FODMAPIngredients.Where(f => ingredient.Contains(f.Name.ToLower())).FirstOrDefault();
                        PrimaryIngredients.Add(new List<Ingredient>() {new Ingredient(ingredient.FirstOrDefault(), fodmap != null) });
                    }
                }
                foreach (var ingredient in secondaryListofLists)
                {
                    if (ingredient.Count > 1)
                    {
                        List<Ingredient> list = new List<Ingredient>();
                        foreach (var subIngredient in ingredient)
                        {
                            var fodmap = db.FODMAPIngredients.Where(f => subIngredient.Contains(f.Name.ToLower())).FirstOrDefault();
                            list.Add(new Ingredient(subIngredient, fodmap != null));
                        }
                        SecondaryIngredients.Add(list);
                    }
                    else if (ingredient.Count == 1)
                    {
                        var fodmap = db.FODMAPIngredients.Where(f => ingredient.Contains(f.Name.ToLower())).FirstOrDefault();
                        SecondaryIngredients.Add(new List<Ingredient>() { new Ingredient(ingredient.FirstOrDefault(), fodmap != null) });
                    }
                }
            }
            //if(!string.IsNullOrEmpty(ingredientString))
            //{
            //    var primary = new List<List<string>>();
            //    var secondary = new List<List<string>>();
            //    IngredientParser.Parse(ingredientString, out primary, out secondary);

            //    var parsedIngredients = IngredientParser.Parse(ingredientString);
            //    foreach(var ingredient in parsedIngredients)
            //    {
            //        if(string.IsNullOrEmpty(ingredient))
            //        {
            //            continue;
            //        }
            //        var fodmap = db.FODMAPIngredients.Where(f => ingredient.Contains(f.Name.ToLower())).FirstOrDefault();
            //        Ingredients.Add(new Ingredient(ingredient, fodmap != null));
            //    }
            //}
            double servingSize;
            ServingSize = double.TryParse(detailObject.SelectToken("servingSize")?.ToString() ?? "", out servingSize) ? servingSize : 0.0;
            ServingSizeUnit = detailObject.SelectToken("servingSizeUnit")?.ToString() ?? "";
            LabelNutrients = detailObject.SelectToken("labelNutrients")?.ToString() ?? "";
            int fdcId;
            FdcId = int.TryParse(detailObject.SelectToken("fdcId")?.ToString() ?? "", out fdcId) ? fdcId : -1;
            UPC = detailObject.SelectToken("gtinUpc")?.ToString() ?? "";
        }
    }
}