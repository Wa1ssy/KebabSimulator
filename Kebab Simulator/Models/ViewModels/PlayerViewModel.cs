namespace Kebab_Simulator.Models.ViewModels
{
    public class PlayerViewModel
    {
        public Guid PlayerId { get; set; }
        public string Username { get; set; }

        public int Level { get; set; }
        public int CurrentXP { get; set; }
        public int XPToNextLevel { get; set; }

        public decimal Balance { get; set; } // Tenge
        public int TotalKebabsSold { get; set; }

        public List<AchievementViewModel> Achievements { get; set; }
    }
}
