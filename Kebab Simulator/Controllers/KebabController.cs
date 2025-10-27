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
        // ✅ STORE — cooking start
        [HttpGet]
        public async Task<IActionResult> Store(Guid recipeId)
        {
            var recipe = await _context.KebabRecipes.FirstOrDefaultAsync(r => r.ID == recipeId);
            var player = await _context.Kebabs.FirstOrDefaultAsync();

            if (recipe == null || player == null)
                return NotFound();

            if (player.KebabLevel < recipe.LevelRequired)
            {
                TempData["Error"] = "Your level is too low to cook this kebab!";
                return RedirectToAction("Index", "Marketplace");
            }

            int baseTime = 30;
            int reduction = player.GrillLevel * 4;
            int finalCookTime = Math.Max(10, baseTime - reduction);

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
            ViewBag.GrillLevel = player.GrillLevel;

            return View("store", vm); // ✅ Ensure view name is correct
        }



        // ✅ SELL — selling cooked kebab
        [HttpPost]
        public async Task<IActionResult> Sell(Guid recipeId)
        {
            var recipe = await _context.KebabRecipes.FirstOrDefaultAsync(r => r.ID == recipeId);
            var player = await _context.Kebabs.FirstOrDefaultAsync();

            if (recipe == null || player == null)
                return NotFound();

            decimal spiceMultiplier = 1 + (player.SpiceLevel * 0.10m);
            int finalPrice = (int)(recipe.Price * spiceMultiplier);

            player.KebabBankAccount += finalPrice;
            player.KebabXP += 20;

            if (player.KebabXP >= player.KebabXPNextLevel)
            {
                player.KebabLevel++;
                player.KebabXP = 0;
                player.KebabXPNextLevel += 100;
            }

            await _context.SaveChangesAsync();

            TempData["Message"] =
                $"You sold {recipe.Name} for ${finalPrice}! (+{player.SpiceLevel * 10}% bonus)";
            return RedirectToAction("Index", "Marketplace");
        }



        // ✅ BUY UPGRADE — player leveling system
        [HttpPost]
        public async Task<IActionResult> BuyUpgrade(string upgradeName)
        {
            var player = await _context.Kebabs.FirstOrDefaultAsync();
            if (player == null)
                return RedirectToAction("Upgrades", "Marketplace");

            switch (upgradeName)
            {
                case "🔥 Faster Grill":
                    if (player.GrillLevel >= 5)
                    { TempData["Error"] = "Grill already maxed!"; break; }

                    int grillCost = 200 + (player.GrillLevel * 100);
                    if (player.KebabBankAccount < grillCost)
                    { TempData["Error"] = "Not enough $"; break; }

                    player.KebabBankAccount -= grillCost;
                    player.GrillLevel++;
                    TempData["Message"] = $"🔥 Grill Level {player.GrillLevel}!";
                    break;

                case "💎 Special Spices":
                    if (player.SpiceLevel >= 5)
                    { TempData["Error"] = "Spices maxed!"; break; }

                    int spiceCost = 300 + (player.SpiceLevel * 150);
                    if (player.KebabBankAccount < spiceCost)
                    { TempData["Error"] = "Not enough $"; break; }

                    player.KebabBankAccount -= spiceCost;
                    player.SpiceLevel++;
                    TempData["Message"] = $"💎 Spice Level {player.SpiceLevel}!";
                    break;

                case "👨‍🍳 Assistant":
                    if (player.AssistantLevel >= 3)
                    { TempData["Error"] = "Assistant maxed!"; break; }

                    int assistantCost = 500 + (player.AssistantLevel * 200);
                    if (player.KebabBankAccount < assistantCost)
                    { TempData["Error"] = "Not enough $"; break; }

                    player.KebabBankAccount -= assistantCost;
                    player.AssistantLevel++;
                    TempData["Message"] = $"👨‍🍳 Assistant Level {player.AssistantLevel}!";
                    break;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Upgrades", "Marketplace");
        }



        // ✅ PASSIVE INCOME — smart interval calculation
        [HttpPost]
        public async Task<IActionResult> CollectPassiveIncome()
        {
            var player = await _context.Kebabs.FirstOrDefaultAsync();
            if (player == null)
                return Json(new { earned = 0, interval = 30 });

            if (player.AssistantLevel <= 0)
                return Json(new { earned = 0, message = "No assistant yet!" });

            int baseTime = 30;
            int interval = Math.Max(10, baseTime - (player.GrillLevel * 4));

            if (player.LastAssistantPayedAt != null)
            {
                var passed = (DateTime.UtcNow - player.LastAssistantPayedAt.Value).TotalSeconds;
                if (passed < interval)
                {
                    return Json(new
                    {
                        earned = 0,
                        message = $"Wait {(int)(interval - passed)}s",
                        interval
                    });
                }
            }

            decimal income = (50 * player.AssistantLevel) * (1 + player.SpiceLevel * 0.10m);
            int earned = (int)income;

            player.KebabBankAccount += earned;
            player.LastAssistantPayedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Json(new { earned, interval });

        }
    }
    }
