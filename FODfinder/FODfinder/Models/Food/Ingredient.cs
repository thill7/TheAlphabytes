namespace FODfinder.Models.Food
{
    public class Ingredient
    {
        public string Name { get; private set; }
        public bool IsFodmap { get; private set; }
        public string Label { get; private set; }
        public bool IsPartOfSublist { get; private set; }

        public Ingredient(string name, bool isFodmap, string label, bool isPartOfSublist)
        {
            Name = name;
            Label = label;
            IsFodmap = label == "Low-Risk" ? false : isFodmap;
            IsPartOfSublist = isPartOfSublist;
        }
    }
}