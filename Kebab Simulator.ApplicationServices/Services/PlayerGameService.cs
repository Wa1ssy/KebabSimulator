using Kebab_Simulator.Core.ServiceInterface;
using Kebab_Simulator.Models.ViewModels;


namespace Kebab_Simulator.ApplicationServices.Services
{
    public class PlayerGameService : IPlayerGameService
    {
        private static PlayerViewModel _player = new PlayerViewModel
        {
            PlayerId = Guid.NewGuid(),
            Username = "Aslan",
            Level = 1,
            CurrentXP = 0,
            XPToNextLevel = 100,
            Balance = 0,
            TotalKebabsSold = 0,
            Achievements = new List<AchievementViewModel>
            {
                new AchievementViewModel { Title = "First Steps", Description = "Cook your first kebab" },
                new AchievementViewModel { Title = "Level Up!", Description = "Reach level 2" },
                new AchievementViewModel { Title = "Kebab Master", Description = "Sell 50 kebabs" }
            }
        };

        public PlayerViewModel GetPlayer() => _player;

        public void CookKebab()
        {
            int xpEarned = 5;
            decimal kebabPrice = 10m;

            _player.CurrentXP += xpEarned;
            _player.Balance += kebabPrice;
            _player.TotalKebabsSold++;

            if (_player.CurrentXP >= _player.XPToNextLevel)
            {
                _player.Level++;
                _player.CurrentXP = 0;
                _player.XPToNextLevel += 100;
                UnlockAchievement("Level Up!");
            }

            if (_player.TotalKebabsSold >= 1)
                UnlockAchievement("First Steps");

            if (_player.TotalKebabsSold >= 50)
                UnlockAchievement("Kebab Master");
        }

        private void UnlockAchievement(string title)
        {
            var a = _player.Achievements.FirstOrDefault(x => x.Title == title);
            if (a != null && !a.IsUnlocked)
            {
                a.IsUnlocked = true;
                a.UnlockedAt = DateTime.Now;
            }
        }
    }
}
