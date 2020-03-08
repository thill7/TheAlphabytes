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
        public string Ingredients { get; private set; }
        public bool RequireAllWords { get; private set; }
        public List<FoodSearchResultItem> Foods { get; private set; } = new List<FoodSearchResultItem>();

        public FoodSearchResult(string jsonInput, string ingredients, bool requireAllWords)
        {
            JObject jsonObject = JObject.Parse(jsonInput);
            Query = jsonObject.SelectToken("foodSearchCriteria.generalSearchInput")?.ToString() ?? "";
            int totalHits;
            TotalHits = int.TryParse(jsonObject.SelectToken("totalHits")?.ToString() ?? "", out totalHits) ? totalHits : 0;
            int currentPage;
            CurrentPage = int.TryParse(jsonObject.SelectToken("currentPage")?.ToString() ?? "", out currentPage) ? currentPage : 0;
            int totalPages;
            TotalPages = int.TryParse(jsonObject.SelectToken("totalPages")?.ToString() ?? "", out totalPages) ? totalPages : 0;
            JArray foods = JArray.Parse(jsonObject.SelectToken("foods")?.ToString() ?? "");
            foreach (var food in foods)
            {
                string foodResult = food.ToString();
                Foods.Add(new FoodSearchResultItem(foodResult));
            }
            this.Ingredients = ingredients;
            this.RequireAllWords = requireAllWords;
        }
    }
}