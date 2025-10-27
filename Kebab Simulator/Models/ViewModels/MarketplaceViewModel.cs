namespace Kebab_Simulator.Models.ViewModels
{
    public class MarketplaceViewModel
    {
        public List<KebabViewModel> AvailableRecipes { get; set; }
        public List<UpgradeViewModel> AvailableUpgrades { get; set; }
        public List<ChefViewModel> AvailableChefs { get; set; }
        public int PlayerLevel { get; set; }
        public int PlayerMoney { get; set; }
    }
}
