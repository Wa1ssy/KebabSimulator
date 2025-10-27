using Kebab_Simulator.ApplicationServices.Services;
using Kebab_Simulator.Core.Domain.Dto;
using Kebab_Simulator.Core.Domain.Serviceinterface;
using Kebab_Simulator.Data;
using Kebab_Simulator.Models.KebabModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using Kebab_Simulator.Models.ViewModels;

namespace Kebab_Simulator.Controllers
{
    public class KebabController : Controller
    {

        private readonly KebabSimulatorContext _context;
        private readonly IKebabSimulatorServices _KebabSimulatorServices;
        private readonly IFileServices _fileServices;
        public KebabController(KebabSimulatorContext context, IKebabSimulatorServices kebabSimulatorServices, IFileServices fileServices)
        {
            _context = context;
            _KebabSimulatorServices = kebabSimulatorServices;
            _fileServices = fileServices;
        }

        public IActionResult Index()
        {
            var resultingInventory = _context.Kebabs
                .OrderByDescending(y => y.KebabLevel)
                .Select(x => new KebabIndexViewModel
                {
                    ID = x.ID,
                    KebabName = x.KebabName,
                    KebabXP = x.KebabXP,
                    KebabXPNextLevel = x.KebabXPNextLevel,
                    KebabLevel = x.KebabLevel,
                    Checkout = x.Checkout,
                    KebabBankAccount = x.KebabBankAccount,
                });
            return View(resultingInventory);
        }
        [HttpGet]
        public IActionResult Create()
        {
            KebabCreateViewModel vm = new();
            return View("Create", vm);
        }



        [HttpPost]
        public async Task<IActionResult> Create(KebabCreateViewModel vm)
        {
            var dto = new KebabDto()
            {
                KebabName = vm.KebabName,
                KebabXP = 0,
                KebabXPNextLevel = 100,
                KebabLevel = 0,
                KebabBankAccount = vm.KebabBankAccount,
                Checkout = vm.Checkout,
                KebabStart = vm.KebabStart,
                KebabDone = vm.KebabDone,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image
                .Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    KebabID = x.KebabID,
                }).ToArray()


            };
            var result = await _KebabSimulatorServices.Create(dto);

            if (result != null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kebab = await _KebabSimulatorServices.DetailsAsync(id);

            if (kebab == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToDatabase
                .Where(t => t.KebabID == id)
                .Select(y => new KebabImageViewModel
                {
                    KebabID = y.KebabID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new KebabDetailsViewModel
            {
                ID = kebab.ID,
                KebabName = kebab.KebabName,
                KebabXP = kebab.KebabXP,
                KebabXPNextLevel = kebab.KebabXPNextLevel,
                KebabLevel = kebab.KebabLevel,
                KebabBankAccount = kebab.KebabBankAccount,
                Checkout = kebab.Checkout,
                KebabStart = kebab.KebabStart,
                KebabDone = kebab.KebabDone,
            };

            vm.Image.AddRange(images);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var kebab = await _KebabSimulatorServices.DetailsAsync(id);

            if (kebab == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToDatabase
                .Where(x => x.KebabID == id)
                .Select(y => new KebabImageViewModel
                {
                    KebabID = y.KebabID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new KebabCreateViewModel
            {
                ID = kebab.ID,
                KebabName = kebab.KebabName,
                KebabXP = kebab.KebabXP,
                KebabXPNextLevel = kebab.KebabXPNextLevel,
                KebabLevel = kebab.KebabLevel,
                KebabBankAccount = kebab.KebabBankAccount,
                Checkout = kebab.Checkout,
                KebabStart = kebab.KebabStart,
                KebabDone = kebab.KebabDone,
                CreatedAt = kebab.CreatedAt,
                UpdatedAt = DateTime.Now
            };

            vm.Image.AddRange(images);

            return View("Update", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(KebabCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var dto = new KebabDto
            {
                ID = (Guid) vm.ID,
                KebabName = vm.KebabName,
                KebabXP = vm.KebabXP,
                KebabXPNextLevel = vm.KebabXPNextLevel,
                KebabLevel = vm.KebabLevel,
                KebabBankAccount = vm.KebabBankAccount,
                Checkout = vm.Checkout,
                KebabStart = vm.KebabStart,
                KebabDone = vm.KebabDone,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    KebabID = x.KebabID,
                }).ToArray()
            };

            var result = await _KebabSimulatorServices.Update(dto);

            if (result != null)
            {
                return RedirectToAction("Index");
            }

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kebab = await _KebabSimulatorServices.DetailsAsync(id);

            if (kebab == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToDatabase
                .Where(x => x.KebabID == id)
                .Select(y => new KebabImageViewModel
                {
                    KebabID = y.KebabID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new KebabDeleteViewModel
            {
                ID = kebab.ID,
                KebabName = kebab.KebabName,
                KebabXP = kebab.KebabXP,
                KebabXPNextLevel = kebab.KebabXPNextLevel,
                KebabLevel = kebab.KebabLevel,
                KebabBankAccount = kebab.KebabBankAccount,
                Checkout = kebab.Checkout,
                KebabStart = kebab.KebabStart,
                KebabDone = kebab.KebabDone,
            };

            vm.Image.AddRange(images);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var kebabToDelete = await _KebabSimulatorServices.Delete(id);

            if (kebabToDelete == null)
            {
                return NotFound(); 
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(KebabImageViewModel vm)
        {
            var dto = new FileToDatabaseDto()
            {
                ID = vm.ImageID,

            };
            var iamge = await _fileServices.RemoveImageFromDatabase(dto);
            if (iamge != null) { return RedirectToAction("Index"); }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Store(Guid recipeId)
        {
            var recipe = await _context.KebabRecipes.FirstOrDefaultAsync(r => r.ID == recipeId);
            if (recipe == null)
            {
                return NotFound("Recipe not found.");
            }

            var player = await _context.Kebabs.FirstOrDefaultAsync();
            if (player == null)
            {
                return NotFound("Player not found.");
            }

            if (player.KebabLevel < recipe.LevelRequired)
            {
                TempData["Error"] = "Your level is too low to cook this kebab!";
                return RedirectToAction("Index", "Marketplace");
            }

            // 🧠 Calculate cook time reduction
            // Each upgrade level = -5 seconds (minimum 10s)
            int baseTime = 30;
            int reduction = player.Checkout * 4;
            int finalCookTime = Math.Max(10, baseTime - reduction);

            // Send everything to the View
            var vm = new KebabViewModel
            {
                ID = recipe.ID,
                Name = recipe.Name,
                KebabType = (Models.ViewModels.KebabType)recipe.KebabType,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                LevelRequired = recipe.LevelRequired,
                Price = recipe.Price
            };

            ViewBag.CookTime = finalCookTime;
            ViewBag.GrillLevel = player.Checkout;

            return View("Store", vm);
        }


        [HttpPost]
        public async Task<IActionResult> Sell(Guid recipeId)
        {
            var recipe = await _context.KebabRecipes.FirstOrDefaultAsync(r => r.ID == recipeId);
            var player = await _context.Kebabs.FirstOrDefaultAsync();

            if (recipe == null || player == null)
            {
                return NotFound();
            }

            // 💎 Calculate spice multiplier (every 1000 XPNextLevel = +10%)
            int spiceLevel = player.KebabXPNextLevel / 1000;
            decimal spiceMultiplier = 1 + (spiceLevel * 0.10m);

            // 🤑 Apply spice bonus to kebab sell price
            var finalPrice = recipe.Price * spiceMultiplier;

            player.KebabBankAccount += (int)finalPrice;

            // XP gain + leveling logic (unchanged)
            player.KebabXP += 20;
            if (player.KebabXP >= player.KebabXPNextLevel)
            {
                player.KebabLevel++;
                player.KebabXP = 0;
                player.KebabXPNextLevel += 100;
            }

            _context.Kebabs.Update(player);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"You sold {recipe.Name} for ${finalPrice:F0} (+{spiceLevel * 10}% bonus!)";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> BuyUpgrade(string upgradeName)
        {
            var player = await _context.Kebabs.FirstOrDefaultAsync();
            if (player == null)
            {
                TempData["Error"] = "Player not found.";
                return RedirectToAction("Upgrades", "Marketplace");
            }

            switch (upgradeName)
            {
                // 🔥 FASTER GRILL — reduces cook time & assistant interval by 4s per level
                case "🔥 Faster Grill":
                    int grillCost = 200 + (player.Checkout * 100);
                    if (player.KebabBankAccount < grillCost)
                    {
                        TempData["Error"] = "Not enough money for Faster Grill!";
                        break;
                    }

                    if (player.Checkout >= 5)
                    {
                        TempData["Error"] = "Faster Grill is already maxed out!";
                        break;
                    }

                    player.KebabBankAccount -= grillCost;
                    player.Checkout++;
                    TempData["Message"] = $"🔥 Faster Grill upgraded to Level {player.Checkout}! Cook and passive income times reduced by 4 seconds!";
                    break;

                // 💎 SPECIAL SPICES — increases ALL income by +10% per level
                case "💎 Special Spices":
                    int spiceLevel = player.KebabXPNextLevel / 1000;
                    int spiceCost = 300 + (spiceLevel * 150);

                    if (player.KebabBankAccount < spiceCost)
                    {
                        TempData["Error"] = "Not enough money for Special Spices!";
                        break;
                    }

                    if (spiceLevel >= 5)
                    {
                        TempData["Error"] = "Special Spices are already maxed out!";
                        break;
                    }

                    player.KebabBankAccount -= spiceCost;
                    player.KebabXPNextLevel += 1000; // each 1000 = +1 level (10% all income)
                    TempData["Message"] = $"💎 Special Spices upgraded to Level {spiceLevel + 1}! All income increased by 10%!";
                    break;

                // 👨‍🍳 ASSISTANT — passive income feature
                case "👨‍🍳 Assistant":
                    int assistantLevel = player.Checkout > 5 ? player.Checkout - 5 : 0;
                    int assistantCost = 500 + (assistantLevel * 200);

                    if (player.KebabBankAccount < assistantCost)
                    {
                        TempData["Error"] = "Not enough money for Assistant!";
                        break;
                    }

                    if (assistantLevel >= 3)
                    {
                        TempData["Error"] = "Assistant is already maxed out!";
                        break;
                    }

                    player.KebabBankAccount -= assistantCost;
                    assistantLevel++;

                    // Assistant stored after grill levels (offset trick)
                    player.Checkout = 5 + assistantLevel;

                    TempData["Message"] = $"👨‍🍳 Assistant hired (Level {assistantLevel})! You now earn passive income faster every few seconds!";
                    break;

                default:
                    TempData["Error"] = "Invalid upgrade selected.";
                    break;
            }

            _context.Kebabs.Update(player);
            await _context.SaveChangesAsync();

            return RedirectToAction("Upgrades", "Marketplace");
        }
        [HttpPost]
        public async Task<IActionResult> CollectPassiveIncome()
        {
            var player = await _context.Kebabs.FirstOrDefaultAsync();
            if (player == null)
                return Json(new { earned = 0, message = "Player not found", interval = 30 });

            // 🎚 Grill level (0–5) — reduces time
            int grillLevel = Math.Min(player.Checkout, 5);

            // 👨‍🍳 Assistant level — stored as offset beyond grill (Checkout > 5)
            int assistantLevel = player.Checkout > 5 ? player.Checkout - 5 : 0;

            // 💎 Spices level — +10% income per level (every +1000 XPNextLevel)
            int spiceLevel = Math.Min(player.KebabXPNextLevel / 1000, 5);

            // ❌ No assistant = no passive income
            if (assistantLevel <= 0)
                return Json(new { earned = 0, message = "No assistant yet!", interval = 30 });

            // 🕒 Base interval = 30s, reduced by 4s per grill level
            int baseTime = 30;
            int interval = Math.Max(10, baseTime - (grillLevel * 4));

            // ⏱ Prevent early collection
            if (player.LastAssistantPayedAt != null)
            {
                double secondsSinceLast = (DateTime.UtcNow - player.LastAssistantPayedAt.Value).TotalSeconds;
                if (secondsSinceLast < interval)
                {
                    return Json(new
                    {
                        earned = 0,
                        message = $"Too early! Wait {Math.Ceiling(interval - secondsSinceLast)}s",
                        interval
                    });
                }
            }

            // 💰 Base income grows with assistant level
            decimal baseIncome = assistantLevel * 50;

            // 💎 Apply spices bonus (+10% per level)
            decimal finalIncome = baseIncome * (1 + (spiceLevel * 0.10m));

            // 💸 Update player data
            player.KebabBankAccount += (int)Math.Round(finalIncome);
            player.LastAssistantPayedAt = DateTime.UtcNow;

            _context.Kebabs.Update(player);
            await _context.SaveChangesAsync();

            // ✅ Return result as JSON
            return Json(new
            {
                earned = (int)Math.Round(finalIncome),
                interval,
                message = $"👨‍🍳 Assistant earned ${(int)Math.Round(finalIncome)} (Lvl {assistantLevel}, {interval}s interval, +{spiceLevel * 10}% bonus)"
            });
        }
    }
}
