using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using FODfinder.Utility;
using FODfinder.Utility.Algorithm;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FODfinder.Models.Food
{
    public class FoodDetailsModels
    {
        public int FdcId { set; get; }
        public string Description { set; get; }
        public string BrandOwner { set; get; }
        public List<Ingredient> PrimaryIngredients { set; get; } = new List<Ingredient>();
        public List<Ingredient> SecondaryIngredients { set; get; } = new List<Ingredient>();
        public double ServingSize { set; get; }
        public string ServingSizeUnit { set; get; }
        public string ServingSizeFullText { set; get; }
        public string LabelNutrients { set; get; }
        public string UPC { set; get; }
        public string FodmapScore { set; get; }
        public double MaxFodmapPercentage { get; set; }

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
            MaxFodmapPercentage = Math.Round(Algorithm.GetMaxFodmapPercentage(PrimaryIngredients, SecondaryIngredients), 2);
            FodmapScore = MaxFodmapPercentage > 5 ? $"{Score.High}" : MaxFodmapPercentage > 0 ? $"{Score.Medium}" : $"{Score.Low}";
        }

        public enum Score
        {
            Low,
            Medium,
            High
        }

        [JsonConstructor]
        public FoodDetailsModels()
        {

        }
    }
}