namespace FODfinder.Models.Food
{
    public class Ingredient
    {
        public string Name { get; set; }
        public bool IsFodmap { get; set; }
        public string Label { get; set; }
        public Position? IngredientPosition { get; set; }

        public Ingredient(string name, bool isFodmap, string label, Position? ingredientPosition)
        {
            Name = name;
            Label = label;
            IsFodmap = label == "Low-Risk" ? false : isFodmap;
            IngredientPosition = ingredientPosition;
        }

        public enum Position
        {
            Parent,
            LastChild
        }
    }
}