using System;
using System.Collections.Generic;
using FODfinder.Models.Food;
using System.Linq;
using System.Web;

namespace FODfinder.Utility.Algorithm
{
    public class Algorithm
    {
        public static bool ListContainsFodmaps(List<List<Ingredient>> ingredients)
        {
            try
            {
                foreach (var ingredient in ingredients)
                {
                    if (ingredient.Count == 1)
                    {
                        if (ingredient.FirstOrDefault().IsFodmap)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        foreach (var subingredient in ingredient)
                        {
                            if(subingredient.IsFodmap)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
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
                var temp = ListContainsFodmaps(primaryIngredients) ? Score.High : ListContainsFodmaps(secondaryIngredients) ? Score.Medium : Score.Low;
                return temp;
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