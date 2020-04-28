using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Models.Api
{
    public class LabelIngredientRequest
    {
        [JsonProperty("assignLabel")]
        public string AssignLabel { get; set; }
        [JsonProperty("ingredientName")]
        public string IngredientName { get; set; }
    }
}