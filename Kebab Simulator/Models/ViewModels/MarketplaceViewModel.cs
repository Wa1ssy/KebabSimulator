namespace Kebab_Simulator.Models.ViewModels
{
    public class MarketplaceViewModel
    {
        public List<KebabRecipeViewModel> AvailableRecipes { get; set; }
        public List<UpgradeViewModel> AvailableUpgrades { get; set; }
        public List<ChefViewModel> AvailableChefs { get; set; }
    }
}
