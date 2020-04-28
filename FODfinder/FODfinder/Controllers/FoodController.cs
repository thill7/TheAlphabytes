using FODfinder.Models;
using FODfinder.Models.Food;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FODfinder.Controllers
{
    public class FoodController : Controller
    {
        private string _API_key = WebConfigurationManager.AppSettings.Get("USDA_KEY");
        private const string USDA_FOOD = "https://api.nal.usda.gov/fdc/v1/";
        private FFDBContext db = new FFDBContext();

        public string GenerateQueryString(Dictionary<string, string> keyValuePairs)
        {
            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            foreach (var param in keyValuePairs)
            {
                queryParams[param.Key] = param.Value;
            }
            return queryParams.ToString();
        }

        async Task<string> GetAsync(string url, Dictionary<string, string> parameters)
        {
            string json;
            UriBuilder builder = new UriBuilder(url);
            builder.Query = GenerateQueryString(parameters);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(builder.ToString());
                json = await response.Content.ReadAsStringAsync();
            }
            return json;
        }

        async private Task<string> GetFoodResults(string query, string pageNumber = "1", string ingredients = null, bool requireAllWords = false)
        {
            UriBuilder uriBuilder = new UriBuilder(USDA_FOOD+"search");
            var paramCollection = new Dictionary<string, string>
            {
                ["api_key"] = _API_key,
                ["generalSearchInput"] = query,
                ["includeDataTypeList"] = "Branded",
                ["pageNumber"] = pageNumber,
                ["requireAllWords"] = requireAllWords.ToString().ToLower()
            };
            if (ingredients != null)
            {
                paramCollection["ingredients"] = ingredients;
            }
            var queryParams = GenerateQueryString(paramCollection);
            uriBuilder.Query = queryParams;

            Debug.WriteLine(queryParams.ToString());

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(uriBuilder.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        async private Task<string> GetFoodDetails(string fdcId)
        {
            UriBuilder uriBuilder = new UriBuilder(USDA_FOOD+fdcId);
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParams["api_key"] = _API_key;
            uriBuilder.Query = queryParams.ToString();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(uriBuilder.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
        // GET: Food
        async public Task<ActionResult> Index(string query, string ingredients = null, bool requireAllWords = false)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View();
            }
            string url = $"{Request.Url.Scheme}{Uri.SchemeDelimiter}{Request.Url.Authority}/api/food/search";
            var parameters = new Dictionary<string, string>
            {
                {"query",query },
                {"requireAllWords",requireAllWords.ToString() }
            };
            if(!string.IsNullOrEmpty(ingredients))
            {
                parameters.Add("ingredients", ingredients);
            }
            var foodSearchResults = await GetAsync(url, parameters);
            var model = JsonConvert.DeserializeObject<FoodSearchResult>(foodSearchResults);
            return View(model);
        }

        async public Task<ActionResult> Details(int id)
        {
            string url = $"{Request.Url.Scheme}{Uri.SchemeDelimiter}{Request.Url.Authority}/api/food/details";
            var parameters = new Dictionary<string, string>
            {
                {"id",id.ToString() }
            };
            var foodDetails = await GetAsync(url, parameters);
            var model = JsonConvert.DeserializeObject<FoodDetailsModels>(foodDetails);
            if (model.FdcId == default(int))
            {
                return new HttpNotFoundResult("Invalid FdcId");
            }
            return View(model);
        }

        async public Task<ContentResult> Get(string query, string pageNumber="1", string ingredients = null, bool requireAllWords = false)
        {
            var foodSearchResults = await GetFoodResults(query, pageNumber, ingredients, requireAllWords);
            FoodSearchResult results = new FoodSearchResult(foodSearchResults,ingredients,requireAllWords);
            return Content(JObject.FromObject(results).ToString(), "application/json");
        }
    }
}