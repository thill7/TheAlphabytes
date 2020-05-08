using System;
using System.Collections.Generic;
using FODfinder.Models.Food;

namespace FODfinder.Utility.Algorithm
{
    public class Algorithm
    {
        public static Tuple<int, int> DetermineIngredientAmounts(List<Ingredient> ingredients)
        {
            var numberOfIngredients = ingredients.Count;
            for (var i = 0; i < numberOfIngredients; i++)
            {
                ingredients[i].MaxAmount = (100 - ((numberOfIngredients - 1 - i) * 3)) / (i + 1);
                ingredients[i].MinAmount = i == 0 ? 100 / numberOfIngredients : 3;
            }
            return null;
            
        }
        public static bool ListContainsFodmaps(List<Ingredient> ingredients)
        {
            try
            {
                foreach (var ingredient in ingredients)
                {
                    if (ingredient.IsFodmap)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException($"Arguments for \"ListContainsFodmaps()\" must not be null: {e}");
            }
        }
        public static Score DetermineLevelOfFodmap(List<Ingredient> primaryIngredients, List<Ingredient> secondaryIngredients)
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