using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Models.Api
{
    public class SavedFoodRequest
    {
        [JsonProperty("usdaFoodId")]
        public int UsdaFoodID { get; set; }
        [JsonProperty("ListID")]
        public int ListID { get; set; }
        [JsonProperty("brandOwner")]
        public string BrandOwner { get; set; }
        [JsonProperty("upc")]
        public string Upc { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}