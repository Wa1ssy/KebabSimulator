using Microsoft.AspNetCore.Mvc;

namespace Kebab_Simulator.Controllers
{
	public class AdminAreasController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
