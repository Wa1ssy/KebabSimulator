using Microsoft.AspNetCore.Mvc;

namespace Kebab_Simulator.Controllers
{
    public class StoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
