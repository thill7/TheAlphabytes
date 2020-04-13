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
            catch (NullReferenceException e)
            {
                throw new NullReferenceException($"Arguments for \"ListContainsFodmaps\" must not be null: {e}");
            }
        }
        public static Score DetermineLevelOfFodmap(List<List<Ingredient>> primaryIngredients, List<List<Ingredient>> secondaryIngredients)
        {
            try
            {
                return ListContainsFodmaps(primaryIngredients) ? Score.High : ListContainsFodmaps(secondaryIngredients) ? Score.Medium : Score.Low;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException($"Arguments for \"DetermineLevelOfFodmap()\" must not be null: {e}");
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