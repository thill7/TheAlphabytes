using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace FODfinder.Utility
{
    public class IngredientParser
    {
        //@"(([\w\s-]+\([\w\s-,]+\))|[\w\s-]+)|((, contains|contains|, less|less|than|2|%|percent|of|or|the|following|;|\:|\s)+)|(([\w\s-]+\([\w\s-,]+\))|[\w\s-]+)"
        private const string OtherPattern = @"(([\w\s-]+\([\w\s-,]+\))|[\w\s-]+)|((, |; |\: )?(contains|less|than|2|%|percent|of|or|the|following|\s)+(;|\:))";
        private const string PATTERN = @"[\w\s']+";
        private static readonly string[] ToRemove = { "contains less than 2% of", "ingredients", ":", "contains 2% or less of", "less than 2%", "less than 2 percent" };

        public static List<string> Parse(string ingredients)
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

        public static void Parse(string ingredients, out List<string> primaryIngredients, out List<string> secondaryIngredients)
        {
            primaryIngredients = new List<string>();
            secondaryIngredients = new List<string>();
            ingredients = ingredients.ToLower();
            var matches = Regex.Matches(ingredients, OtherPattern);
        }
    }
}