using FODfinder.Models;
using FODfinder.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FODfinder.Controllers.api
{
    [EnableCors("*","*","*")]
    [RoutePrefix("api/food")]
    public class FoodApiController : ApiController
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

        async private Task<string> GetFoodResults(string query, string pageNumber = "1", string ingredients = null, bool requireAllWords = false)
        {
            UriBuilder uriBuilder = new UriBuilder(USDA_FOOD + "search");
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

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(uriBuilder.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        async private Task<string> GetFoodDetails(string fdcId)
        {
            UriBuilder uriBuilder = new UriBuilder(USDA_FOOD + fdcId);
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParams["api_key"] = _API_key;
            uriBuilder.Query = queryParams.ToString();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(uriBuilder.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
        [Route("search")]
        [HttpGet]
        async public Task<FoodSearchResult> Search([FromUri] string query, string pageNumber = "1", string ingredients = null, bool requireAllWords = false)
        {
            var foodSearchResults = await GetFoodResults(query, pageNumber, ingredients, requireAllWords);
            FoodSearchResult results = new FoodSearchResult(foodSearchResults, ingredients, requireAllWords);
            return results;
        }
        [Route("details")]
        [HttpGet]
        async public Task<FoodDetailsModels> Details([FromUri] int id)
        {
            var json = await GetFoodDetails(id.ToString());
            return new FoodDetailsModels(json);
        }
    }
}
