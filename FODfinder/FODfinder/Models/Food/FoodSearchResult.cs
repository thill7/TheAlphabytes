using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web;

namespace FODfinder.Models.Food
{
    public class FoodSearchResult
    {
        public string Query { get; private set; }
        public int TotalHits { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public List<FoodSearchResultItem> Foods { get; private set; } = new List<FoodSearchResultItem>();

        public FoodSearchResult(string jsonInput)
        {
            JObject jsonObject = JObject.Parse(jsonInput);
            Query = jsonObject.SelectToken("foodSearchCriteria.generalSearchInput")?.ToString() ?? "";
            TotalHits = int.TryParse(jsonObject.SelectToken("totalHits")?.ToString() ?? "", out var totalHits) ? totalHits : 0;
            CurrentPage = int.TryParse(jsonObject.SelectToken("currentPage")?.ToString() ?? "", out var currentPage) ? currentPage : 0;
            TotalPages = int.TryParse(jsonObject.SelectToken("totalPages")?.ToString() ?? "", out var totalPages) ? totalPages : 0;
            JArray foods = JArray.Parse(jsonObject.SelectToken("foods")?.ToString() ?? "");
            foreach(var food in foods)
            {
                string foodResult = food.ToString();
                Foods.Add(new FoodSearchResultItem(foodResult));
            }
        }
    }
}