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

        private static MatchCollection MatchRegEx(string ingredientsString) => Regex.Matches(ingredientsString, MultiIngredientPattern);
        private static IEnumerable<string> ConvertToEnumerable(MatchCollection matches) => matches.Cast<Match>().Select(m => $"{m}".Trim());
        private static List<string> ConvertToList(string ingredient) => ingredient.Contains('(') ? ingredient.Replace(")", "").Replace('(', ',').Split(',').ToList() : new List<string>() { ingredient };

        public static void Parse(string ingredients, out List<List<string>> primaryIngredients, out List<List<string>> secondaryIngredients)
        {
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

            var primaryIngredientsEnumerable = ConvertToEnumerable(MatchRegEx(primaryIngredientsString));
            var secondaryIngredientsEnumerable = ConvertToEnumerable(MatchRegEx(secondaryIngredientsString));

            primaryIngredients = new List<List<string>>();
            secondaryIngredients = new List<List<string>>();

            foreach (var primaryIngredient in primaryIngredientsEnumerable)
            {
                var list = ConvertToList(primaryIngredient);
                primaryIngredients.Add(list);
            }
            foreach (var secondaryIngredient in secondaryIngredientsEnumerable)
            {
                var list = ConvertToList(secondaryIngredient);
                secondaryIngredients.Add(list);
            }
        }
    }
}