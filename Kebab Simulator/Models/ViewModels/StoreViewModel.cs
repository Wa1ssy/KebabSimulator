namespace Kebab_Simulator.Models.ViewModels
{
    using Kebab_Simulator.Models;
    public class StoreViewModel
    {
        public List<KebabViewModel> KebabsInFreezer { get; set; }
        public List<KebabViewModel> KebabsOnGrill { get; set; }
        public List<KebabViewModel> ReadyToSell { get; set; }
        public List<KebabViewModel> SoldKebabs { get; set; }

        public EquipmentStatusViewModel EquipmentStatus { get; set; }

        public decimal TotalRevenue { get; set; }
        public int KebabsSoldToday { get; set; }
    }
}
