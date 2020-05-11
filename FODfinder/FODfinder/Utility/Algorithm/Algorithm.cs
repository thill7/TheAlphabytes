using System;
using System.Collections.Generic;
using FODfinder.Models.Food;
using System.Linq;

namespace FODfinder.Utility.Algorithm
{
    public class Algorithm
    {
        public static int CountSublist(List<Ingredient> ingredients, int startIndex)
        {
            var sublistCount = 1;
            while (ingredients[startIndex].IngredientPosition != Ingredient.Position.LastChild)
            {
                sublistCount++;
                startIndex++;
            }
            return sublistCount;
        }
        public static int CountNonParents(List<Ingredient> ingredients) => ingredients.Where(i => i.IngredientPosition != Ingredient.Position.Parent).Count();
        public static void SetIngredientAmounts(List<Ingredient> primaryIngredients, List<Ingredient> secondaryIngredients)
        {
            var numberOfPrimaryIngredients = CountNonParents(primaryIngredients);
            var numberOfSecondaryIngredients = secondaryIngredients.Count;
            var totalLeftAsMin = 100 - CountNonParents(secondaryIngredients) * 0.001;
            var totalLeftAsMax = 100 - CountNonParents(secondaryIngredients) * 2d;
            for (var i = 0; i < primaryIngredients.Count; i++)
            {
                if (primaryIngredients[i].IngredientPosition == Ingredient.Position.Parent)
                {
                    var parentIngredientMax = (totalLeftAsMin - ((numberOfPrimaryIngredients - 1 - i) * 2.001)) / (i + 1);
                    var startIndex = i + 1;
                    var sublistCount = CountSublist(primaryIngredients, startIndex);
                    var k = 0;
                    for (var j = startIndex; j < sublistCount + startIndex; j++)
                    {
                        primaryIngredients[j].MaxAmount = (100 - ((sublistCount - 1 - k) * 0.001)) / (k + 1) * parentIngredientMax / 100;
                        primaryIngredients[j].MinAmount = k == 0 ? 100 / sublistCount * 2.001 / 100 : 0.001;
                        k++;
                    }
                    i += sublistCount;
                }
                else
                {
                    primaryIngredients[i].MaxAmount = (totalLeftAsMin - ((numberOfPrimaryIngredients - 1 - i) * 2.001)) / (i + 1);
                    primaryIngredients[i].MinAmount = i == 0 ? totalLeftAsMax / numberOfPrimaryIngredients : 2.001;
                }
            }
            for (var i = 0; i < numberOfSecondaryIngredients; i++)
            {
                if (secondaryIngredients[i].IngredientPosition == Ingredient.Position.Parent)
                {
                    var startIndex = i + 1;
                    var sublistCount = CountSublist(secondaryIngredients, startIndex);
                    var k = 0;
                    for (var j = startIndex; j < sublistCount + startIndex; j++)
                    {
                        secondaryIngredients[j].MaxAmount = (100 - ((sublistCount - 1 - k) * 0.001)) / (k + 1) * 2 / 100;
                        secondaryIngredients[j].MinAmount = 0.001;
                        k++;
                    }
                }
                else
                {
                    secondaryIngredients[i].MaxAmount = 2;
                    secondaryIngredients[i].MinAmount = 0.001;
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
        public static double GetSumOfMinAmounts(List<Ingredient> ingredients) => ingredients.Where(i => i.IsFodmap).Select(i => i.MinAmount).Sum();
        public static double GetMaxFodmapPercentage(List<Ingredient> primaryIngredients, List<Ingredient> secondaryIngredients)
        {
            SetIngredientAmounts(primaryIngredients, secondaryIngredients);
            var maxFodmapPercentage = 0d;
            if (ListContainsFodmaps(primaryIngredients))
            {
                var highestPercentageIngredient = primaryIngredients.Where(pi => pi.IsFodmap).FirstOrDefault();
                maxFodmapPercentage += highestPercentageIngredient.MaxAmount + GetSumOfMinAmounts(primaryIngredients) - highestPercentageIngredient.MinAmount + GetSumOfMinAmounts(secondaryIngredients); 
            }
            else if (ListContainsFodmaps(secondaryIngredients))
            {
                var numberOfNonParentPrimaryIngredients = primaryIngredients.Where(pi => pi.IngredientPosition != Ingredient.Position.Parent).Count();
                var numberOfNonParentSecondaryIngredients = secondaryIngredients.Where(si => si.IngredientPosition != Ingredient.Position.Parent).Count();
                if (numberOfNonParentPrimaryIngredients * 2.001 + numberOfNonParentSecondaryIngredients * 2 < 100)
                {
                    maxFodmapPercentage = secondaryIngredients.Where(si => si.IsFodmap).Count() * 2;
                }
                else
                {
                    maxFodmapPercentage = 100 - numberOfNonParentPrimaryIngredients * 2.001;
                }
            }
            return maxFodmapPercentage;
        }
    }
}