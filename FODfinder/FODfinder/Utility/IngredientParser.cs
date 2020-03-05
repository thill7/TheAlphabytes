using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace FODfinder.Utility
{
    public class IngredientParser
    {
        private const String PATTERN = @"[\w\s']+";
        private static readonly String[] ToRemove = { "contains less than 2% of", "ingredients", ":", "contains 2% or less of", "less than 2%", "less than 2 percent" };

        public static List<String> Parse(String ingredients)
        {
            ingredients = ingredients.ToLower();
            foreach (var toRemove in ToRemove)
            {
                ingredients = ingredients.Replace(toRemove, "");
            }
            //Console.WriteLine(ingredients);
            var matches = Regex.Matches(ingredients, PATTERN);
            var parsedIngredients = matches.Cast<Match>().Select(m => m.ToString().Trim()).ToList();
            return parsedIngredients;
        }
    }
}