using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kebab_Simulator.Controllers
{
    [Authorize(Roles = "Player,Admin")]
    public class GameController : Controller
    {
        public IActionResult Play()
        {
            return View();
        }
    }
}
