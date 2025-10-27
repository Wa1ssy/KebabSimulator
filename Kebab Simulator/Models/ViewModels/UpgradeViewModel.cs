namespace Kebab_Simulator.Models.ViewModels
{
    public class UpgradeViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentLevel { get; set; }
        public int MaxLevel { get; set; }
        public decimal UpgradeCost { get; set; }
        public bool IsMaxedOut => CurrentLevel >= MaxLevel;
    }
}
