namespace Kebab_Simulator.Models.ViewModels
{
    public class AchievementViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsUnlocked { get; set; }
        public DateTime? UnlockedAt { get; set; }
    }
}
