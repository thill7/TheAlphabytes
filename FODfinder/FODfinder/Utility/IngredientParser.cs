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
        private const string MultiIngredientPattern = @"[\w\s-]+(\([\w\s-,]+\))*";
        private static readonly string[] ToRemove = { "ingredients", ":", ";", "made of", "." };
        private static readonly string[] Variations = { "contains less than 2% of", "contains 2% or less of", "less than 2% of", "less than 2%", "less than 2 percent" };

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
            foreach (var toRemove in ToRemove)
            {
                ingredients = ingredients.Replace(toRemove, "");
            }
            var index = -1;
            var length = 0;
            foreach (var item in Variations)
            {
                index = ingredients.IndexOf(item);
                if(index != -1)
                {
                    length = item.Length;
                    break;
                }
            }
            var primaryIngredientsString = ingredients.Substring(0, index);
            var secondaryIngredientsString = ingredients.Substring(index + length);
            var primaryMatches = Regex.Matches(primaryIngredientsString, MultiIngredientPattern);
            var secondaryMatches = Regex.Matches(secondaryIngredientsString, MultiIngredientPattern);
        }
    }
}