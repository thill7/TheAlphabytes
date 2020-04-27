using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using FODfinder.Utility;
using FODfinder.Utility.Algorithm;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace FODfinder.Models.Food
{
    public class FoodDetailsModels
    {
        public int FdcId { private set; get; }
        public string Description { private set; get; }
        public string BrandOwner { private set; get; }
        public List<Ingredient> PrimaryIngredients { private set; get; } = new List<Ingredient>();
        public List<Ingredient> SecondaryIngredients { private set; get; } = new List<Ingredient>();
        public double ServingSize { private set; get; }
        public string ServingSizeUnit { private set; get; }
        public string ServingSizeFullText { private set; get; }
        public string LabelNutrients { private set; get; }
        public string UPC { private set; get; }
        public string FodmapScore { private set; get; }

        public FoodDetailsModels(string jsonString) {
            JObject detailObject = JObject.Parse(jsonString);
            string userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            Description = detailObject.SelectToken("description")?.ToString() ?? "";
            BrandOwner = detailObject.SelectToken("brandOwner")?.ToString() ?? "";
            var ingredientString = detailObject.SelectToken("ingredients")?.ToString() ?? "";
            if (!string.IsNullOrEmpty(ingredientString))
            {
                var primary = new List<Ingredient>();
                var secondary = new List<Ingredient>();
                IngredientParser.Parse(ingredientString, out primary, out secondary);
                PrimaryIngredients = primary;
                SecondaryIngredients = secondary;
            }
            double servingSize;
            ServingSize = double.TryParse(detailObject.SelectToken("servingSize")?.ToString() ?? "", out servingSize) ? servingSize : 0.0;
            ServingSizeUnit = detailObject.SelectToken("servingSizeUnit")?.ToString() ?? "";
            ServingSizeFullText = ServingSizeCleaner.Clean(detailObject.SelectToken("householdServingFullText")?.ToString() ?? "");
            LabelNutrients = detailObject.SelectToken("labelNutrients")?.ToString() ?? "";
            int fdcId;
            FdcId = int.TryParse(detailObject.SelectToken("fdcId")?.ToString() ?? "", out fdcId) ? fdcId : -1;
            UPC = detailObject.SelectToken("gtinUpc")?.ToString() ?? "";
            FodmapScore = Algorithm.DetermineLevelOfFodmap(PrimaryIngredients, SecondaryIngredients).ToString();
        }
    }
}