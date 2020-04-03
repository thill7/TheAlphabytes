using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Diagnostics;
using FODfinder.Models;
using FODfinder.Models.Food;

namespace FODfinder.Utility
{
    public class IngredientParser
    {
        //@"(([\w\s-]+\([\w\s-,]+\))|[\w\s-]+)|((, contains|contains|, less|less|than|2|%|percent|of|or|the|following|;|\:|\s)+)|(([\w\s-]+\([\w\s-,]+\))|[\w\s-]+)"
        //private const string OtherPattern = @"(([\w\s-]+\([\w\s-,]+\))|[\w\s-]+)|((, |; |\: )?(contains|less|than|2|%|percent|of|or|the|following|\s)+(;|\:))";
        //private const string PATTERN = @"[\w\s']+";
        private const string MultiIngredientPattern = @"[\w\s-]+(\([\w\s-,]+\))*";
        private static readonly string[] ToRemove = { "ingredients", ":", ";", "made of", ".", "contains one or more of the following" };
        private static readonly string[] Variations = { "contains less than 2% of", "contains 2% or less of", "less than 2% of", "less than 2%", "less than 2 percent" };

        //public static List<string> Parse(string ingredients)
        //{
        //    ingredients = ingredients.ToLower();
        //    foreach (var toRemove in ToRemove)
        //    {
        //        ingredients = ingredients.Replace(toRemove, "");
        //    }
        //    //Console.WriteLine(ingredients);
        //    var matches = Regex.Matches(ingredients, PATTERN);
        //    var parsedIngredients = matches.Cast<Match>().Select(m => m.ToString().Trim()).ToList();
        //    return parsedIngredients;
        //}

        private static MatchCollection MatchRegEx(string ingredientsString) => Regex.Matches(ingredientsString, MultiIngredientPattern);
        private static IEnumerable<string> ConvertToEnumerable(MatchCollection matches) => matches.Cast<Match>().Select(m => $"{m}".Trim());
        private static List<string> ConvertToList(string ingredient) => ingredient.Contains('(') ? ingredient.Replace(")", "").Replace('(', ',').Split(',').ToList() : new List<string>() { ingredient };
        private static List<List<string>> ConvertToListOfLists(IEnumerable<string> ingredients)
        {
            var listOfLists = new List<List<string>>();
            ingredients.ToList().ForEach(i => listOfLists.Add(ConvertToList(i)));
            return listOfLists;
        }
        public static void Parse(string ingredients, out List<List<string>> primaryIngredients, out List<List<string>> secondaryIngredients)
        {
            ToRemove.ToList().ForEach(tr => ingredients = ingredients.ToLower().Replace(tr, ""));
            var index = -1;
            var length = 0;
            foreach (var item in Variations)
            {
                if ((index = ingredients.IndexOf(item)) != -1)
                {
                    length = item.Length;
                    break;
                }
            }
            if (index != -1)
            {
                var primaryIngredientsString = ingredients.Substring(0, index);
                var secondaryIngredientsString = ingredients.Substring(index + length);
                secondaryIngredients = ConvertToListOfLists(ConvertToEnumerable(MatchRegEx(secondaryIngredientsString)));
                primaryIngredients = ConvertToListOfLists(ConvertToEnumerable(MatchRegEx(primaryIngredientsString)));
            }
            else
            {
                primaryIngredients = ConvertToListOfLists(ConvertToEnumerable(MatchRegEx(ingredients)));
                secondaryIngredients = null;
            }
        }
        public static List<List<Ingredient>> CreateListOfIngredients(List<List<string>> ingredientsAsStrings, ref FFDBContext db)
        {
            var ingredients = new List<List<Ingredient>>();
            foreach (var ingredient in ingredientsAsStrings)
            {
                if (ingredient.Count > 1)
                {
                    List<Ingredient> list = new List<Ingredient>();
                    foreach (var subIngredient in ingredient)
                    {
                        var fodmap = db.FODMAPIngredients.Where(f => subIngredient.Contains(f.Name.ToLower())).FirstOrDefault();
                        list.Add(new Ingredient(subIngredient, fodmap != null));
                    }
                    ingredients.Add(list);
                }
                else if (ingredient.Count == 1)
                {
                    var fodmap = db.FODMAPIngredients.Where(f => ingredient.FirstOrDefault().Contains(f.Name.ToLower())).FirstOrDefault();
                    ingredients.Add(new List<Ingredient>() { new Ingredient(ingredient.FirstOrDefault(), fodmap != null) });
                }
            }
            return ingredients;
        }
    }
}