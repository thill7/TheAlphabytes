using System;
using System.Collections.Generic;
using FODfinder.Models.Food;
using System.Linq;
using System.Web;

namespace FODfinder.Utility.Algorithm
{
    public class Algorithm
    {
        public static bool CheckListForFodmaps(List<List<Ingredient>> ingredients)
        {
            try
            {
                var temp = ingredients.Select(i => i.Where(si => si.IsFodmap)) != null;
                return temp;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception($"Arguments must not be null: {e}");
            }
        }
        public static Score DetermineLevelOfFodmap(List<List<Ingredient>> primaryIngredients, List<List<Ingredient>> secondaryIngredients)
        {
            try
            {
                var score = CheckListForFodmaps(primaryIngredients) ? Score.High : Score.Low;
                score = CheckListForFodmaps(secondaryIngredients) ? Score.Medium : Score.Low;
                return score;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception($"Arguments must not be null: {e}");
            }
        }
        public enum Score
        {
            Low,
            Medium,
            High
        }
    }
}