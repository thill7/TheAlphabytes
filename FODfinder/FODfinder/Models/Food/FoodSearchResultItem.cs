using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Models.Food
{
    public class FoodSearchResultItem
    {
        public int FdcId { get; set; }
        public string GtinUPC { get; set; }
        public string Description { get; set; }
        public string PublishedDate { get; set; }
        public string BrandOwner { get; set; }
        public string Ingredients { get; set; }

        public FoodSearchResultItem(string jsonString)
        {
            JObject foodObject = JObject.Parse(jsonString);
            int fdcId;
            FdcId = int.TryParse(foodObject.SelectToken("fdcId")?.ToString() ?? "", out fdcId) ? fdcId : -1;
            int gtinUpc;
            GtinUPC = foodObject.SelectToken("gtinUpc")?.ToString() ?? "";
            Description = foodObject.SelectToken("description")?.ToString() ?? "";
            PublishedDate = foodObject.SelectToken("publishedData")?.ToString() ?? "";
            BrandOwner = foodObject.SelectToken("brandOwner")?.ToString() ?? "";
            Ingredients = foodObject.SelectToken("ingredients")?.ToString() ?? "";
        }

        [JsonConstructor]
        public FoodSearchResultItem()
        {

        }
    }
}