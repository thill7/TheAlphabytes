using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Models.Food
{
    public class FoodSearchResultItem
    {
        public int FdcId { get; private set; }
        public int GtinUPC { get; private set; }
        public string Description { get; private set; }
        public string PublishedDate { get; private set; }
        public string BrandOwner { get; private set; }
        public string Ingredients { get; private set; }

        public FoodSearchResultItem(string jsonString)
        {
            JObject foodObject = JObject.Parse(jsonString);
            int fdcId;
            FdcId = int.TryParse(foodObject.SelectToken("fdcId")?.ToString() ?? "", out fdcId) ? fdcId : -1;
            int gtinUpc;
            GtinUPC = int.TryParse(foodObject.SelectToken("fdcId")?.ToString() ?? "", out gtinUpc) ? gtinUpc : -1;
            Description = foodObject.SelectToken("description")?.ToString() ?? "";
            PublishedDate = foodObject.SelectToken("publishedData")?.ToString() ?? "";
            BrandOwner = foodObject.SelectToken("brandOwner")?.ToString() ?? "";
            Ingredients = foodObject.SelectToken("ingredients")?.ToString() ?? "";
        }
    }
}