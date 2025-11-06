using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kebab_Simulator.Data;
using Kebab_Simulator.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kebab_Simulator.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly KebabSimulatorContext _context;

        public MarketplaceController(KebabSimulatorContext context)
        {
            _context = context;
        }

        // 🥙 GET: /Marketplace/ — Kebab Shop
        public async Task<IActionResult> Index()
        {
            var player = await _context.Kebabs.FirstOrDefaultAsync();
            if (player == null)
            {
                return NotFound("Player not found in database.");
            }

            int playerLevel = player.KebabLevel;
            int playerMoney = player.KebabBankAccount;

            // 🥩 Load kebab recipes from DB
            var kebabRecipes = await _context.KebabRecipes
                .OrderBy(r => r.LevelRequired)
                .Select(r => new KebabViewModel
                {
                    ID = r.ID,
                    Name = r.Name,
                    KebabType = (Models.ViewModels.KebabType)r.KebabType,
                    Description = r.Description,
                    Ingredients = r.Ingredients,
                    LevelRequired = r.LevelRequired,
                    Status = (Models.ViewModels.KebabStatus)r.Status,
                    CreatedAt = r.CreatedAt,
                    FinishedAt = r.FinishedAt,
                    Price = r.Price
                })
                .ToListAsync();

            // 🧩 Build page ViewModel
            var viewModel = new MarketplaceViewModel
            {
                AvailableRecipes = kebabRecipes,
                AvailableUpgrades = new List<UpgradeViewModel>(),
                AvailableChefs = new List<ChefViewModel>()
            };

            ViewBag.PlayerLevel = playerLevel;
            ViewBag.PlayerMoney = playerMoney;

            return View(viewModel);
        }

        // ⚙️ GET: /Marketplace/Upgrades — Player Upgrades Shop
        [HttpGet]
        public async Task<IActionResult> Upgrades()
        {
            var player = await _context.Kebabs.FirstOrDefaultAsync();
            if (player == null)
                return NotFound("Player not found.");

            int playerMoney = player.KebabBankAccount;

            // ✅ Uued levelid mudelist
            int grillLevel = player.GrillLevel;
            int spiceLevel = player.SpiceLevel;
            int assistantLevel = player.AssistantLevel;
            int houseLevel = player.HouseLevel;
            int carLevel = player.CarLevel;

            var upgrades = new List<UpgradeViewModel>
    {
        new UpgradeViewModel
        {
            Name = "🔥 Faster Grill",
            Description = "Reduces cooking time by 4 sec per level.",
            CurrentLevel = grillLevel,
            MaxLevel = 5,
            UpgradeCost = 200 + (grillLevel * 100)
        },
        new UpgradeViewModel
        {
            Name = "💎 Special Spices",
            Description = "Increases ALL income by +10% per level.",
            CurrentLevel = spiceLevel,
            MaxLevel = 5,
            UpgradeCost = 300 + (spiceLevel * 150)
        },
        new UpgradeViewModel
        {
            Name = "👨‍🍳 Assistant",
            Description = "Passive income, increases every level.",
            CurrentLevel = assistantLevel,
            MaxLevel = 3,
            UpgradeCost = 500 + (assistantLevel * 200)
        },
        new UpgradeViewModel
        {
            Name = "🏠︎ House",
            Description = "Cool house, increases xp gain by 10.",
            CurrentLevel = houseLevel,
            MaxLevel = 3,
            UpgradeCost = 1000 + (houseLevel * 2000)
        },
        new UpgradeViewModel
        {
            Name = "⛐ Car",
            Description = "Car tuning, increases xp gain by 5.",
            CurrentLevel = carLevel,
            MaxLevel = 3,
            UpgradeCost = 500 + (carLevel * 500)
        }
    };

            ViewBag.PlayerMoney = playerMoney;
            return View(upgrades);
        }

    }
}
