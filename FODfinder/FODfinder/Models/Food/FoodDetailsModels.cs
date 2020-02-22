using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace FODfinder.Models.Food
{
    public class FoodDetailsModels
    {
        public int FdcId { private set; get; }
        public string Description { private set; get; }
        public string BrandOwner { private set; get; }
        public string Ingredients { private set; get; }
        public double ServingSize { private set; get; }
        public string ServingSizeUnit { private set; get; }
        public string LabelNutrients { private set; get; }
        public string UPC { private set; get; }

        public FoodDetailsModels(String jsonString) {
            JObject detailObject = JObject.Parse(jsonString);
            Description = detailObject.SelectToken("description")?.ToString() ?? "";
            BrandOwner = detailObject.SelectToken("brandOwner")?.ToString() ?? "";
            Ingredients = detailObject.SelectToken("ingredients")?.ToString() ?? "";
            double servingSize;
            ServingSize = Double.TryParse(detailObject.SelectToken("servingSize")?.ToString() ?? "", out servingSize) ? servingSize : 0.0;
            ServingSizeUnit = detailObject.SelectToken("servingSizeUnit")?.ToString() ?? "";
            LabelNutrients = detailObject.SelectToken("labelNutrients")?.ToString() ?? "";
            int fdcId;
            FdcId = int.TryParse(detailObject.SelectToken("fdcId")?.ToString() ?? "", out fdcId) ? fdcId : -1;
            UPC = detailObject.SelectToken("gtinUpc")?.ToString() ?? "";
        }
    }
}