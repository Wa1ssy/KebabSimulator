using Microsoft.AspNetCore.Mvc;

namespace Kebab_Simulator.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
