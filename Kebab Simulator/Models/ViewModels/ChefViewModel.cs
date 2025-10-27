namespace Kebab_Simulator.Models.ViewModels
{
    public class ChefViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public decimal HiringCost { get; set; }
        public decimal Salary { get; set; }
        public bool IsHired { get; set; }
    }
}
