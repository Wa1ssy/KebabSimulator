using Kebab_Simulator.Core.Domain;
using Kebab_Simulator.Core.Domain.Dto.AccountsDtos;
using Kebab_Simulator.Data;
using Kebab_Simulator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kebab_Simulator.Controllers
{
    public class PlayerProfilesController : Controller
    {
        private readonly KebabSimulatorContext _context;
        public PlayerProfilesController(KebabSimulatorContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(_context.PlayerProfiles.OrderByDescending(x => x.ScreenName));
        }

        [HttpGet]
        public async Task<IActionResult> NewProfile()
        {

            return View();
        }
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> NewProfile(PlayerProfileDto dto)
        {
            string userid = TempData["NewUserID"].ToString();
            if (userid == null)
            {
                List<string> errordatas =
                    [
                    "Area", "Accounts",
                    "Issue", "Failure",
                    "StatusMessage", "No user id found"
                    ];
                ViewBag.ErrorData = errordatas;
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ??
                    HttpContext.TraceIdentifier
                });
            }
            var newprofile = new PlayerProfile()
            {
                ID = dto.ID,
                ApplicationUserID = TempData["NewUserID"].ToString(),
                ScreenName = dto.ScreenName,
                KebabCredits = 100,
                ScrapResource = 0,
                CurrentStatus = ProfileStatus.Active,
                ProfileType = false,
                ProfileStatusLastChangedAt = DateTime.UtcNow,
                ProfileAttributedToAnAccountUserAt = DateTime.UtcNow,
                ProfileCreatedAt = DateTime.UtcNow,
                ProfileModifiedAt = DateTime.UtcNow,

            };
            var result = await _context.PlayerProfiles.AddAsync(newprofile);
            await _context.SaveChangesAsync();
            if (result == null)
            {
                List<string> errordatas =
                         [
                         "Area", "Accounts",
                       "Issue", "Failure",
                       "StatusMessage", "Creation of Player profile is unsuccessful. \nPlease contact an Administrator.",
                       "UserID", $"{newprofile.ApplicationUserID}",
                       "PlayerProfileID", $"{newprofile.ID}"
                         ];
                ViewBag.ErrorDatas = errordatas;
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return View("~/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> NewPlayerProfile()
        {
            return View();
        }
    }
}
