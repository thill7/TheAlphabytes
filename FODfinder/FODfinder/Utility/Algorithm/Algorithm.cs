using System;
using System.Collections.Generic;
using FODfinder.Models.Food;

namespace FODfinder.Utility.Algorithm
{
    public class Algorithm
    {
        public static void DetermineIngredientAmounts(List<Ingredient> primaryIngredients, List<Ingredient> secondaryIngredients)
        {
            var numberOfPrimaryIngredients = primaryIngredients.Count;
            var numberOfSecondaryIngredients = secondaryIngredients.Count;
            var totalLeft = 100 - numberOfSecondaryIngredients * 0.001;
            for (var i = 0; i < numberOfPrimaryIngredients; i++)
            {
                primaryIngredients[i].MaxAmount = (totalLeft - ((numberOfPrimaryIngredients - 1 - i) * 3)) / (i + 1);
                primaryIngredients[i].MinAmount = i == 0 ? totalLeft / numberOfPrimaryIngredients : 2.001;
            }
            for (var i = 0; i < numberOfSecondaryIngredients; i++)
            {
                secondaryIngredients[i].MaxAmount = 2;
                secondaryIngredients[i].MinAmount = 0.001;
            }
            for (var i = 0; i < numberOfPrimaryIngredients; i++)
            {
                if (primaryIngredients[i].IngredientPosition == Ingredient.Position.Parent)
                {
                    var parentIngredientMax = primaryIngredients[i].MaxAmount;
                    var parentIngredientMin = primaryIngredients[i].MinAmount;
                }
            }
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