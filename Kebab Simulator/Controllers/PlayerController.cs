using Microsoft.AspNetCore.Mvc;
using Kebab_Simulator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kebab_Simulator.Controllers
{
    public class PlayerController : Controller
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
                new AchievementViewModel { Title = "First Steps", Description = "Cook your first kebab", IsUnlocked = false },
                new AchievementViewModel { Title = "Level Up!", Description = "Reach level 2", IsUnlocked = false },
                new AchievementViewModel { Title = "Kebab Master", Description = "Sell 50 kebabs", IsUnlocked = false }
            }
        };

        public IActionResult Index()
        {
            return View(_player);
        }

        [HttpPost]
        public IActionResult CookKebab()
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

            return RedirectToAction("Index");
        }

        private void UnlockAchievement(string title)
        {
            var achievement = _player.Achievements.FirstOrDefault(a => a.Title == title);
            if (achievement != null && !achievement.IsUnlocked)
            {
                achievement.IsUnlocked = true;
                achievement.UnlockedAt = DateTime.Now;
            }
        }

        public IActionResult Marketplace()
        {
            return View();
        }
    }
}
