using Microsoft.AspNetCore.Mvc;

namespace Kebab_Simulator.Controllers
{
    public class BaseKebabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
