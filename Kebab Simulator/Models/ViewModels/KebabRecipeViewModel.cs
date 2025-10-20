namespace Kebab_Simulator.Models.ViewModels
{
    public class KebabRecipeViewModel
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LevelRequired { get; set; }
        public decimal Price { get; set; }
        public bool IsUnlocked { get; set; }
    }
}
