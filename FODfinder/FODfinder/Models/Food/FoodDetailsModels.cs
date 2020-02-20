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
        public String Description { private set; get; }
        public String BrandOwner { private set; get; }
        public String Ingredients { private set; get; }
        public Double ServingSize { private set; get; }
        public String ServingSizeUnit { private set; get; }
        public String LabelNutrients { private get; set; }

        public FoodDetailsModels(String jsonString) {
            JObject detailObject = JObject.Parse(jsonString);
            Description = detailObject.SelectToken("description")?.ToString() ?? "";
            BrandOwner = detailObject.SelectToken("brandOwner")?.ToString() ?? "";
            Ingredients = detailObject.SelectToken("ingredients")?.ToString() ?? "";
            ServingSize = Double.TryParse(detailObject.SelectToken("servingSize")?.ToString() ?? "", out Double servingSize) ? servingSize : 0.0;
            ServingSizeUnit = detailObject.SelectToken("servingSizeUnit")?.ToString() ?? "";
            LabelNutrients = detailObject.SelectToken("labelNutrients")?.ToString() ?? "";
            FdcId = int.TryParse(detailObject.SelectToken("fdcId")?.ToString() ?? "", out int fdcId) ? fdcId : -1;
        }
    }
}