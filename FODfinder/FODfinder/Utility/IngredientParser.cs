using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FODfinder.Models.Ingredients;
using FODfinder.Models;

namespace FODfinder.Utility
{
    public class IngredientParser
    {
        private const string MultiIngredientPattern = @"[\w\s-]+(\([\w\s-,]+\))*";
        private static readonly string[] ToRemove = { "ingredients", ":", ";", "made of", ".", "contains one or more of the following" };
        private static readonly string[] Variations = { "contains less than 2% of", "contains 2% or less of", "less than 2% of", "less than 2%", "less than 2 percent" };
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
        public static List<List<Ingredient>> CreateListOfIngredients(List<List<string>> ingredientsAsStrings)
        {
            FFDBContext db = new FFDBContext();
            var ingredients = new List<List<Ingredient>>();
            foreach (var ingredient in ingredientsAsStrings)
            {
                if (ingredient.Count > 1)
                {
                    List<Ingredient> list = new List<Ingredient>();
                    foreach (var subIngredient in ingredient)
                    {
                        var fodmap = db.FODMAPIngredients.Where(f => subIngredient.Contains(f.Name.ToLower())).FirstOrDefault();
                        list.Add(new Ingredient(subIngredient.Trim(), fodmap != null));
                    }
                    ingredients.Add(list);
                }
                else if (ingredient.Count == 1)
                {
                    var fodmap = db.FODMAPIngredients.Where(f => ingredient.FirstOrDefault().Contains(f.Name.ToLower())).FirstOrDefault();
                    ingredients.Add(new List<Ingredient>() { new Ingredient(ingredient.FirstOrDefault().Trim(), fodmap != null) });
                }
            }
            return ingredients;
        }
    }
}