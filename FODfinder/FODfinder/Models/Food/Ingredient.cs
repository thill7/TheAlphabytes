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
            IsFodmap = isFodmap;
            Label = label;
            if (label == "Low-Risk")
            {
                IsFodmap = false;
            }
            IsPartOfSublist = isPartOfSublist;
        }
    }
}