using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FODfinder.Models
{
    public class HighRiskLabelledIngredient
    {
        public HighRiskLabelledIngredient(LabelledIngredient ingredient, int count)
        {
            this.ingredientId = ingredient.ID;
            this.ingredientName = ingredient.Name;
            this.countOfLabelOccurences = count;
        }

        [Key]
        public int ingredientId { get; set; }
        public string ingredientName { get; set; }
        public int countOfLabelOccurences { get; set; }
    }
}