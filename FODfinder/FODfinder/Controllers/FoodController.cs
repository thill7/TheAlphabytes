using FODfinder.Models;
using FODfinder.Models.Food;
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
        async private Task<string> GetFoodResults(string query, string pageNumber = "1", string ingredients = "", bool requireAllWords = false)
        {
            UriBuilder uriBuilder = new UriBuilder(USDA_FOOD+"search");
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParams["api_key"] = _API_key;
            queryParams["generalSearchInput"] = string.Join("%20",query.Split(' '));
            queryParams["includeDataTypeList"] = "Branded";
            queryParams["pageNumber"] = pageNumber;
            queryParams["ingredients"] = ingredients;
            queryParams["requireAllWords"] = $"{requireAllWords}";
            uriBuilder.Query = queryParams.ToString();

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
        async public Task<ActionResult> Index(string query)
        {
            if (query == null)
            {
                return View();
            }
            var foodSearchResults = await GetFoodResults(query);
            return View(new FoodSearchResult(foodSearchResults));
        }

        async public Task<ActionResult> Details(int id)
        {
            var foodDetails = await GetFoodDetails(id.ToString());
            JObject json = JObject.Parse(foodDetails);
            if (!json.ContainsKey("fdcId"))
            {
                return new HttpNotFoundResult("Invalid FdcId");
            }
            return View(new FoodDetailsModels(foodDetails,ref db));
        }

        async public Task<ContentResult> Get(string query, string page)
        {
            var foodSearchResults = await GetFoodResults(query, page);
            FoodSearchResult results = new FoodSearchResult(foodSearchResults);
            return Content(JObject.FromObject(results).ToString(), "application/json");
        }
    }
}